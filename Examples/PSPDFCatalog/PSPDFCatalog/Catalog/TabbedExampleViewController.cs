using System;
using System.Collections.Generic;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog {
	public class TabbedExampleViewController : PSPDFTabbedViewController, IPSPDFTabbedViewControllerDelegate {
		public UIBarButtonItem ClearTabsButtonItem { get; set; }

		public TabbedExampleViewController () => Title = "Tabbed Bar";

		public override void CommonInit (PSPDFViewController pdfController)
		{
			base.CommonInit (pdfController);

			pdfController = PdfController;
			Delegate = this;

			var sharingConfiguration = PSPDFDocumentSharingConfiguration.FromConfigurationBuilder ((builder) => {
				builder.ApplicationActivitiesAsTypes = new [] { PSPDFActivityType.OpenIn, PSPDFActivityType.Message, PSPDFActivityType.AirDrop };
			});

			pdfController.UpdateConfiguration (builder => {
				builder.SharingConfigurations = new[] { sharingConfiguration };
			});

			NavigationItem.LeftItemsSupplementBackButton = true;

			// enable automatic persistance and restore the last state
			EnableAutomaticStatePersistence = true;

			DocumentPickerController = new PSPDFDocumentPickerController ("/Bundle/Pdf", true, PSPDFKitGlobal.SharedInstance.Library);
			ClearTabsButtonItem = new UIBarButtonItem (PSPDFKitGlobal.GetImage ("trash"), UIBarButtonItemStyle.Plain, ClearTabsButtonPressed);
			pdfController.BarButtonItemsAlwaysEnabled = new [] { ClearTabsButtonItem };
			pdfController.NavigationItem.LeftBarButtonItem = ClearTabsButtonItem;
			pdfController.SetUpdateSettingsForBoundsChangeHandler (controller => {
				UpdateToolbarItems ();
			});

			if (RestoreState || Documents?.Length == 0) {
				var start = new PSPDFDocument (NSUrl.FromFilename (DVCMenu.PSPDFKitFile));
				var hmf = new PSPDFDocument (NSUrl.FromFilename (DVCMenu.HackerMonthlyFile));
				Documents = new [] { start, hmf };
			}
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);
			UpdateBarButtonItems ();
		}

		[Export ("multiPDFController:didChangeDocuments:")]
		public void DidChangeDocuments (PSPDFMultiDocumentViewController multiPdfController, PSPDFDocument[] oldDocuments) => UpdateToolbarItems ();

		void UpdateToolbarItems () => ClearTabsButtonItem.Enabled = Documents.Length > 0;

		void ClearTabsButtonPressed (object sender, EventArgs e)
		{
			var sheetController = UIAlertController.Create (null, null, UIAlertControllerStyle.ActionSheet);
			sheetController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));
			sheetController.AddAction (UIAlertAction.Create ("Close all tabs", UIAlertActionStyle.Destructive, (obj) => Documents = Array.Empty<PSPDFDocument> ()));

            // Needed for iPad as you need to specify for which button the popover is displayed
            var presentationPopover = sheetController.PopoverPresentationController;
            if (presentationPopover != null)
            {
                presentationPopover.BarButtonItem = ClearTabsButtonItem;
            }

			PresentViewController (sheetController, true, null);
		}

		void UpdateBarButtonItems ()
		{
			var items = new List<UIBarButtonItem> {
				PdfController.ThumbnailsButtonItem,
				PdfController.ActivityButtonItem,
				PdfController.AnnotationButtonItem
			};

			if (TraitCollection.HorizontalSizeClass == UIUserInterfaceSizeClass.Regular) {
				items.Insert (2, PdfController.OutlineButtonItem);
				items.Insert (2, PdfController.SearchButtonItem);
			}

			PdfController.NavigationItem.SetRightBarButtonItems (items.ToArray (), false);
		}
	}
}
