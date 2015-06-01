
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.Dialog;

using Foundation;
using UIKit;
using ObjCRuntime;

using PSPDFKit;


namespace PSPDFCatalog
{
	public partial class DVCMenu : DialogViewController
	{
		public static readonly string HackerMonthlyFile = "Pdf/hackermonthly-issue.pdf";
		public static readonly string ProtectedFile = "Pdf/protected.pdf";
		public static readonly string PSPDFKitFile = "Pdf/PSPDFKit QuickStart Guide.pdf";

		UIColor barColor;

		public DVCMenu () : base (UITableViewStyle.Grouped, null)
		{
			Root = new RootElement ("PSPDFKit") {
				new Section (PSPDFKitGlobal.SharedInstance.Version){
					new StringElement ("PSPDFViewController playground", () => {
						var pdfViewer = new PlayGroundViewController (NSUrl.FromFilename (PSPDFKitFile));
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
				new Section ("Annotations"){
					new StringElement ("Annotations From Code", () => {
						// we use a NSData document here but it'll work even better with a file-based variant.
						NSError err;
						var documentData = NSData.FromUrl (NSUrl.FromFilename (HackerMonthlyFile), NSDataReadingOptions.Mapped, out err);
						var pdfViewer = new AnnotationsFromCodeViewController (documentData);
						NavigationController.PushViewController (pdfViewer, true);
					}),
				},
				new Section ("Password / Security", "Password is: test123") {
					new StringElement ("Password Preset", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (ProtectedFile));
						document.UnlockWithPassword ("test123");
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Password Not Preset", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (ProtectedFile));
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Create Password Protected PDF", async () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var status = PSPDFStatusHUDItem.GetProgressHud ("Preparing");
						status.Push (true, null);
						// Create temp file and password
						var tempPdf = NSUrl.FromFilename (Path.Combine (Path.GetTempPath (), Guid.NewGuid ().ToString () + ".pdf"));
						var password = new NSString ("test123");

						// Lets create the dictionary options needed by the PSPDFProcesor
						// With password protected pages, PSPDFProcessor can only add link annotations.
						// We use a helper class to access the CGPDFContextKeys used by the dictionary
						var processorOptions = new NSMutableDictionary ();
						processorOptions.LowlevelSetObject (password, Helper.CGPDFContextUserPassword);
						processorOptions.LowlevelSetObject (password, Helper.CGPDFContextOwnerPassword);
						processorOptions.LowlevelSetObject (NSNumber.FromInt32 (128), Helper.CGPDFContextEncryptionKeyLength);
						processorOptions.LowlevelSetObject (NSNumber.FromBoolean (true), PSPDFProcessorOptionKeys.AnnotationAsDictionaryKey.Handle);
						processorOptions.LowlevelSetObject (NSNumber.FromObject (PSPDFAnnotationType.Link), PSPDFProcessorOptionKeys.AnnotationTypesKey.Handle);

						// We create the page range we want to include in our pdf
						var pageRange = new [] { NSIndexSet.FromNSRange (new NSRange (0, (int)document.PageCount)) };
						// We start a new task so this executes on a separated thread since it is a hevy task and we don't want to block the UI
						await Task.Factory.StartNew (()=> {
							NSError err;
							PSPDFProcessor.DefaultProcessor.GeneratePdf (document: document, 
								pageRange: pageRange,
								fileURL: tempPdf,
								options: (NSDictionary) processorOptions,
								progressHandler: (currentPage, numberOfProcessedPages, totalPages) => InvokeOnMainThread (()=> status.Progress = ((float)numberOfProcessedPages / (float)totalPages)),
								error: out err);
						});
						InvokeOnMainThread (()=> {
							status.Pop (true, null);
							var docToShow = new PSPDFDocument (tempPdf);
							var pdfViewer = new PSPDFViewController (docToShow);
							NavigationController.PushViewController (pdfViewer, true);
						});
					}),
				},
				new Section ("Subclassing", "Examples how to subclass PSPDFKit."){
					new StringElement ("Annotation Link Editor", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						// Need to cast to solve ambiguity between NSObject and string ctor
						var editableTypes = new NSOrderedSet (
							(NSObject) PSPDFAnnotationString.Link, // Important!!
							(NSObject) PSPDFAnnotationString.Highlight,
							(NSObject) PSPDFAnnotationString.Underline,
							(NSObject) PSPDFAnnotationString.Squiggly,
							(NSObject) PSPDFAnnotationString.StrikeOut,
							(NSObject) PSPDFAnnotationString.Note,
							(NSObject) PSPDFAnnotationString.FreeText,
							(NSObject) PSPDFAnnotationString.Ink,
							(NSObject) PSPDFAnnotationString.Square,
							(NSObject) PSPDFAnnotationString.Circle,
							(NSObject) PSPDFAnnotationString.Stamp );

						document.EditableAnnotationTypes = (NSObject)editableTypes;

						var pdfViewer = new LinkEditorViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Capture Bookmarks", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						document.OverrideClass (new Class (typeof (PSPDFBookmarkParser)), new Class (typeof (CustomBookmarkParser)));
						var pdfViewer = new PSPDFViewController (document);
						pdfViewer.RightBarButtonItems = new [] { pdfViewer.BookmarkButtonItem, pdfViewer.SearchButtonItem, pdfViewer.OutlineButtonItem, pdfViewer.ViewModeButtonItem };
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Change link background color to red", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						// Note: You can also globally change the color using:
						// PSPDFLinkAnnotationView.SetGlobalBorderColor = UIColor.Green;
						// We don't use this in the example here since it would change the color globally for all examples.
						var pdfViewer = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
							builder.OverrideClass (new Class (typeof (PSPDFLinkAnnotationView)), new Class (typeof (CustomLinkAnnotationView)));
						}));
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Custom AnnotationProvider", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						document.SetDidCreateDocumentProviderHandler ((documentProvider)=> {
							documentProvider.AnnotationManager.AnnotationProviders = new NSObject[] { new CustomAnnotationProvider ((nint)documentProvider.Document.PageCount), documentProvider.AnnotationManager.FileAnnotationProvider };
						});
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Custom Document", () => {
						var document = new CustomPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var pdfViewer = new PSPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					})
				},
				new Section ("Customization", "Examples how to customize PSPDFKit."){
					new StringElement ("Simple Drawing Button", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile)) {
							AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled
						};
						var pdfViewer = new PSCSimpleDrawingPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					})
				},						
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			if (UIDevice.CurrentDevice.CheckSystemVersion (7,0)) {
				barColor = UIColor.FromRGBA (0.110f, 0.529f, 0.757f, 1f);
				UIApplication.SharedApplication.SetStatusBarStyle (UIStatusBarStyle.LightContent, animated);
				NavigationController.NavigationBar.BarTintColor = barColor;
				NavigationController.Toolbar.TintColor = UIColor.Black;
				NavigationController.View.TintColor = UIColor.White;
				NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes () { ForegroundColor = UIColor.White };
			}
			UIApplication.SharedApplication.SetStatusBarHidden (false, animated ? UIStatusBarAnimation.Fade : UIStatusBarAnimation.None);
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Default;
			NavigationController.SetToolbarHidden (true, animated);
		}
	}
}
