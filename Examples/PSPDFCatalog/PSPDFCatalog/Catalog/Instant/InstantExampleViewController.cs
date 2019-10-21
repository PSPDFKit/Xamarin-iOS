using System;
using System.Threading.Tasks;

using Foundation;
using UIKit;

using PSPDFKit.UI;
using ZXing.Mobile;

namespace PSPDFCatalog {

	// Shows UI to either get a new document code or enter an existing code.
	public class InstantExampleViewController : UITableViewController, IUITextFieldDelegate {

		const string newGroupCellIdentifier = "new group";
		const string codeFieldCellIdentifier = "code field";
		const string barcodeCellIdentifier = "barcode scanner";

		// Interfaces with our web-preview server to create and access documents.
		// In your own app you would connect to your own server backend to get Instant document identifiers and authentication tokens.
		WebPreviewApiClient apiClient = new WebPreviewApiClient ();

		WeakReference<UITextField> codeTextField;

		public InstantExampleViewController () : base (UITableViewStyle.Grouped)
		{
			Title = "PSPDFKit Instant";
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			TableView.KeyboardDismissMode = UIScrollViewKeyboardDismissMode.OnDrag;
			TableView.RegisterClassForCellReuse (typeof (UITableViewCell), newGroupCellIdentifier);
			TableView.RegisterClassForCellReuse (typeof (TextFieldCell), codeFieldCellIdentifier);
			TableView.RegisterClassForCellReuse (typeof (UITableViewCell), barcodeCellIdentifier);
		}

		// Data to show in the table view.
		class Row {
			public string Identifier;
			public bool AllowsHighlight;
		}

		class Section {
			public string Header;
			public Row [] Rows;
			public string Footer;
		}

		Section [] Sections = {
			new Section {
				Header = null,
				Rows = Array.Empty<Row> (),
				Footer = "The PSPDFKit SDKs support Instant out of the box. Just connect your app to an Instant server and document management and syncing is taken care of."
			},
			new Section {
				Header = null,
				Rows = new [] {
					new Row {
						Identifier = newGroupCellIdentifier,
						AllowsHighlight = true
					}
				},
				Footer = "Get a new document link, then collaborate by entering it in PSPDFKit Catalog on another device, or opening the document link in a web browser."
			},
			new Section {
				Header = "Join a group",
				Rows = new [] {
					new Row {
						Identifier = codeFieldCellIdentifier,
						AllowsHighlight = false
					},
					new Row {
						Identifier = barcodeCellIdentifier,
						AllowsHighlight = true
					}
				},
				Footer = "Enter or Scan a document link from PSPDFKit Catalog on another device, or from a web browser showing pspdfkit.com/instant/demo or web-preview.pspdfkit.com."
			}
		};

		public override nint NumberOfSections (UITableView tableView) => Sections.Length;
		public override nint RowsInSection (UITableView tableView, nint section) => Sections [section].Rows.Length;
		public override string TitleForHeader (UITableView tableView, nint section) => Sections [section]?.Header;
		public override string TitleForFooter (UITableView tableView, nint section) => Sections [section]?.Footer;
		public override bool ShouldHighlightRow (UITableView tableView, NSIndexPath rowIndexPath) => Sections [rowIndexPath.Section].Rows [rowIndexPath.Row].AllowsHighlight;

		public override async void RowSelected (UITableView tableView, NSIndexPath indexPath)
		{
			var rowId = Sections [indexPath.Section].Rows [indexPath.Row].Identifier;
			switch (rowId) {
			case newGroupCellIdentifier:
				LoadDocument ("Creating", async () => await apiClient.CreateNewDocument ());
				break;
			case barcodeCellIdentifier:
				var scanner = new MobileBarcodeScanner ();
				var result = await scanner.Scan ();
				if (result is null) return;
				codeTextField.TryGetTarget (out var codefield);
				if (Validate (result.Text))
					LoadDocument ("Joining", async () => await apiClient.ResolveDocument (new Uri (result.Text)));
				break;
			default:
				throw new Exception ($"Unsupported row identifier: {rowId}");
			}
		}

		public override UITableViewCell GetCell (UITableView tableView, NSIndexPath indexPath)
		{
			var row = Sections [indexPath.Section].Rows [indexPath.Row];
			var cell = tableView.DequeueReusableCell (row.Identifier, indexPath);

			cell.Accessory = UITableViewCellAccessory.DisclosureIndicator;
			switch (row.Identifier) {
			case newGroupCellIdentifier:
				cell.TextLabel.Text = "Start a New Group";
				cell.TextLabel.TextColor = UIColor.Black;
				cell.TextLabel.Font = UIFont.GetPreferredFontForTextStyle (UIFontTextStyle.Headline);
				return cell;
			case codeFieldCellIdentifier:
				var textField = (cell as TextFieldCell)?.TextField;
				codeTextField = new WeakReference<UITextField> (textField);

				textField.Delegate = this;
				textField.EditingChanged += TextChangedHandler;
				return cell;
			case barcodeCellIdentifier:
				cell.TextLabel.Text = "Scan QR Code";
				cell.TextLabel.TextColor = UIColor.Black;
				cell.TextLabel.Font = UIFont.GetPreferredFontForTextStyle (UIFontTextStyle.Headline);
				return cell;
			default:
				throw new Exception ($"Unsupported row identifier: {row.Identifier}");
			}
		}

		// Text field actions and delegate

		[Export ("textField:shouldChangeCharactersInRange:replacementString:")]
		public bool ShouldChangeCharacters (UITextField textField, NSRange range, string replacementString)
		{
			var currentTextLength = textField?.Text?.Length ?? 0;
			var resultTextlength = currentTextLength - range.Length + replacementString.Length;

			return resultTextlength <= WebPreviewApiClient.CodeLength;
		}

		[Export ("textFieldShouldReturn:")]
		public bool ShouldReturn (UITextField textField)
		{
			var code = textField.Text;
			if (Validate (code)) {
				LoadDocument ("Joining", async () => await apiClient.ResolveDocument (code));
				return true;
			}

			return false;
		}

		void TextChangedHandler (object sender, EventArgs e)
		{
			var code = (sender as UITextField)?.Text;
			if (Validate (code, true))
				LoadDocument ("Joining", async () => await apiClient.ResolveDocument (code));
		}

		// Server API handling

		void LoadDocument (string loadingMessage, Func<Task<InstantDocumentInfo>> apiCall)
		{
			BeginInvokeOnMainThread (async () => {
				var hasTextField = codeTextField.TryGetTarget (out var textField);
				if (hasTextField)
					textField.Enabled = false;

				var progressHudItem = PSPDFStatusHUDItem.CreateIndeterminateProgress (loadingMessage);
				progressHudItem.SetHudStyle (PSPDFStatusHUDStyle.Black);

				await progressHudItem.PushAsync (true, UIApplication.SharedApplication.Delegate.GetWindow ());
				var documentInfo = await apiCall ();

				if (documentInfo == null) {
					Console.WriteLine ("Could not set up Instant");
					var errorHudItem = PSPDFStatusHUDItem.CreateError ("Could not set up Instant");
					await progressHudItem.PopAsync (true);
					await errorHudItem.PushAndPopAsync (2, true, UIApplication.SharedApplication.Delegate.GetWindow ());
				} else {
					var instantViewController = new InstantDocumentViewController (documentInfo);
					NavigationController.PushViewController (instantViewController, true);
					await progressHudItem.PopAsync (true);
				}

				if (hasTextField)
					textField.Enabled = true;
			});
		}

		bool Validate (string url, bool silent = false)
		{
			var valid = Uri.TryCreate (url, UriKind.Absolute, out var uri) && uri.Scheme == Uri.UriSchemeHttps;

			if (!valid && !silent)
				PSPDFStatusHUDItem.CreateError ("Invalid Document Code").PushAndPop (2, true, UIApplication.SharedApplication.Delegate.GetWindow (), null);

			return valid;
		}
	}

	class TextFieldCell : UITableViewCell {

		public UITextField TextField { get; set; }

		[Export ("initWithStyle:reuseIdentifier:")]
		public TextFieldCell (UITableViewCellStyle style, NSString reuseIdentifier) : base (style, reuseIdentifier)
		{
			TextField = new UITextField {
				Placeholder = "Enter Document Link",
				KeyboardType = UIKeyboardType.ASCIICapable,
				AutocorrectionType = UITextAutocorrectionType.No,
				AutocapitalizationType = UITextAutocapitalizationType.None,
				ReturnKeyType = UIReturnKeyType.Done,
				Font = UIFont.GetPreferredFontForTextStyle (UIFontTextStyle.Headline)
			};

			ContentView.AddSubview (TextField);
		}

		public override void LayoutSubviews ()
		{
			base.LayoutSubviews ();

			// To make this cell use standard height to match the new group cell, make it not self-sizing by not using constraints.
			TextField.Frame = ContentView.LayoutMarginsGuide.LayoutFrame;
		}
	}
}
