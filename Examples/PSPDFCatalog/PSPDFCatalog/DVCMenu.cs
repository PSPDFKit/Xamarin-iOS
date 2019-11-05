
using System;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using MonoTouch.Dialog;

using Foundation;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class DVCMenu : DialogViewController {
		public static readonly string HackerMonthlyFile = "Pdf/hackermonthly-issue.pdf";
		public static readonly string ProtectedFile = "Pdf/protected.pdf";
		public static readonly string PSPDFKitFile = "Pdf/PSPDFKit QuickStart Guide.pdf";
		public static readonly string AnnualReportFile = "Pdf/JKHF-AnnualReport.pdf";
		public static readonly string AnnualReportXFDFFile = "Pdf/XFDFTest.xfdf";
        public static readonly string FormFile = "Pdf/Form_example.pdf";

        UIColor barColor;

		public DVCMenu () : base (UITableViewStyle.Grouped, null)
		{
            Root = new RootElement(PSPDFKitGlobal.VersionString) {
                new Section ("Start here") {
                    new StringElement ("PSPDFViewController Playground", () => {
                        var pdfViewer = new PlayGroundViewController (NSUrl.FromFilename (PSPDFKitFile));
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("PSPDFKit Instant", () => {
                        var instantExample = new InstantExampleViewController ();
                        NavigationController.PushViewController (instantExample, true);
                    }),
                    new StringElement ("Tabbed Bar", () => {
                        var tabbed = new TabbedExampleViewController ();
                        NavigationController.PushViewController (tabbed, true);
                    }),
                    new StringElement ("Analytics Client", () => {
                        var pdfViewer = new AnalyticsClientExample (NSUrl.FromFilename (PSPDFKitFile));
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
                new Section ("Annotations"){
                    new StringElement ("Annotations From Code", () => {
                        var documenturl = NSUrl.FromFilename (HackerMonthlyFile);
                        var pdfViewer = new AnnotationsFromCodeViewController (documenturl);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("XFDF Annotation Provider", () => {
                        var pdfViewer = new PSCXFDFAnnotationProviderExample ();
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Encrypted XFDF Annotation Provider", () => {
                        var pdfViewer = new PSCEncryptedXFDFAnnotationProviderExample ();
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Copy annotations from one file to another", () => {
                        var pdfViewer = new CopyAnnotationsViewController ();
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
                new Section ("Forms"){
                    new StringElement ("Programamtically Fill Form Fields", () => {
                        var documenturl = NSUrl.FromFilename (FormFile);
                        var pdfViewer = new ProgrammaticFormFillingController (documenturl);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
                new Section ("Password / Security", "Password is: test123") {
					new StringElement ("Password Preset", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (ProtectedFile));
						document.Unlock ("test123");
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
						var status = PSPDFStatusHUDItem.CreateIndeterminateProgress ("Preparing");
						await status.PushAsync (true, UIApplication.SharedApplication.Delegate.GetWindow ());
						// Create temp file and password
						var tempPdf = NSUrl.FromFilename (Path.Combine (Path.GetTempPath (), Guid.NewGuid ().ToString () + ".pdf"));
						var password = "test123";

						var configuration = new PSPDFProcessorConfiguration (document);
						var secOptions = new PSPDFDocumentSecurityOptions (password, password, PSPDFDocumentSecurityOptions.KeyLengthAutomatic, out var err);

						// We start a new task so this executes on a separated thread since it is a hevy task and we don't want to block the UI
						await Task.Factory.StartNew (()=> {
							var processor = new PSPDFProcessor (configuration, secOptions);
							processor.WriteToFile (tempPdf, out var error);
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
						var pdfViewer = new LinkEditorViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
							var editableTypes = builder.EditableAnnotationTypes.ToList ();
							editableTypes.Add (PSPDFAnnotationStringUI.Link);
							builder.EditableAnnotationTypes = editableTypes.ToArray ();
						}));
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Capture Bookmarks", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						document.BookmarkManager.Provider = new IPSPDFBookmarkProvider [] { new CustomBookmarkProvider () };
						var pdfViewer = new PSPDFViewController (document);
						pdfViewer.NavigationItem.SetRightBarButtonItems (new [] { pdfViewer.ThumbnailsButtonItem, pdfViewer.OutlineButtonItem, pdfViewer.SearchButtonItem, pdfViewer.BookmarkButtonItem }, PSPDFViewMode.Document, false);
						NavigationController.PushViewController (pdfViewer, true);
					}),
                    new StringElement ("Customize Bookmark UI", () => {
                        // The document needs to be in a writable location so we can properly add and remove bookmarks
                        var tmp = Path.GetTempPath ();
                        var writablePdf = Path.Combine (tmp, "writable.pdf");
                        File.Copy (HackerMonthlyFile, writablePdf, true);

                        var document = new PSPDFDocument (NSUrl.FromFilename (writablePdf));
                        var pdfViewer = new BookmarkViewController (document);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Change link background color to red", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var pdfViewer = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
							builder.OverrideClass (typeof (PSPDFLinkAnnotationView), typeof (CustomLinkAnnotationView));
						}));
						NavigationController.PushViewController (pdfViewer, true);
					}),
                    new StringElement ("Custom AnnotationProvider", () => {
                        var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
                        document.DidCreateDocumentProviderHandler = (documentProvider => {
                            documentProvider.AnnotationManager.AnnotationProviders = new PSPDFContainerAnnotationProvider[] { new CustomAnnotationProvider (documentProvider)};
                        });
                        var pdfViewer = new PSPDFViewController (document);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Customize the Sharing Experience", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
						var sharingConfig = PSPDFDocumentSharingConfiguration.GetDefaultConfiguration (PSPDFDocumentSharingDestination.Activity).GetUpdatedConfiguration (b => {
							b.AnnotationOptions = PSPDFDocumentSharingAnnotationOptions.Embed | PSPDFDocumentSharingAnnotationOptions.Flatten | PSPDFDocumentSharingAnnotationOptions.Remove;
							b.PageSelectionOptions = PSPDFDocumentSharingPagesOptions.Current;
							b.ExcludedActivityTypes = new PSPDFActivityType [] { PSPDFActivityType.AssignToContact, PSPDFActivityType.PostToWeibo, PSPDFActivityType.PostToFacebook, PSPDFActivityType.PostToTwitter };
						});
						var config =  PSPDFConfiguration.FromConfigurationBuilder (builder => {
							builder.OverrideClass (typeof (PSPDFDocumentSharingViewController), typeof (MyCustomDocumentSharingViewController));
							builder.SharingConfigurations = new [] { sharingConfig };
						});
						var pdfViewer = new CustomSharingFileNamesExampleViewController (document, config);
						NavigationController.PushViewController (pdfViewer, true);
					}),
                    new StringElement ("Add notifications for annotation changes", () => {
                        var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new NotificationViewController (document);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
				new Section ("Toolbar Customizations"){
					new StringElement ("Remove Ink from the annotation toolbar", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var pdfController = new PSPDFViewController (document);
						pdfController.NavigationItem.RightBarButtonItems = new [] { pdfController.AnnotationButtonItem };

						// remove ink from the EditableAnnotationTypes array
						var editableTypes = pdfController.Configuration.EditableAnnotationTypes.Where (annotStr => annotStr != PSPDFAnnotationStringUI.Ink).ToArray ();
						pdfController.AnnotationToolbarController.AnnotationToolbar.EditableAnnotationTypes = editableTypes;

						NavigationController.PushViewController (pdfController, true);
					}),
                    new StringElement ("Customize the annotation toolbar", () => {
                        var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
                            builder.OverrideClass (typeof (PSPDFAnnotationToolbar), typeof (Catalog.Customization.CustomizedAnnotationToolbar));
                        }));
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
				new Section ("View Customizations") {
					new StringElement ("Rotate pages", () => {
						// The document needs to be in a writable location
						var tmp = Path.GetTempPath ();
						var writablePdf = Path.Combine (tmp, "writable.pdf");
						File.Copy (PSPDFKitFile, writablePdf, true);

						var document = new PSPDFDocument (NSUrl.FromFilename (writablePdf));
						var pdfViewer = new PSCRotatePagePDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
				},
				new Section ("PSPDFViewController Customization") {
					new StringElement ("Custom Google Text Selection Menu", () => {
						var pdfViewer = new PSCustomTextSelectionMenuController {
							Document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile))
						};
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Simple Drawing Button", () => {
						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile)) {
							AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled
						};
						var pdfViewer = new PSCSimpleDrawingPDFViewController (document);
						NavigationController.PushViewController (pdfViewer, true);
					}),
					new StringElement ("Stylus Support", () => {

						// TODO: Stylus Support
						// Uncomment all the needed driver lines once you added the corresponding Dll's.
						//
						// Please visit PSPDFKit support page for more information
						// https://pspdfkit.com/guides/ios/current/other-languages/xamarin-stylus-support/
						//
						PSPDFKitGlobal.SharedInstance.GetStylusManager ().AvailableDriverClasses = new NSOrderedSet (
							//(INativeObject) new Class (typeof (PSPDFKit.UI.StylusSupport.PSPDFAdonitStylusDriver)),
							//(INativeObject) new Class (typeof (PSPDFKit.UI.StylusSupport.PSPDFFiftyThreeStylusDriver)),
							//(INativeObject) new Class (typeof (PSPDFKit.UI.StylusSupport.PSPDFWacomStylusDriver)),
							//(INativeObject) new Class (typeof (PSPDFKit.UI.StylusSupport.PSPDFPogoStylusDriver))
						);

						var document = new PSPDFDocument (NSUrl.FromFilename (HackerMonthlyFile));
						var pdfViewer = new PSPDFViewController (document);

						pdfViewer.AnnotationToolbarController.AnnotationToolbar.ShowingStylusButton = true;

						NavigationController.PushViewController (pdfViewer, true);
					}),
                },
			};
		}

		public override void ViewWillAppear (bool animated)
		{
			base.ViewWillAppear (animated);

			barColor = GetUIColor (UIColor.White, UIColor.FromWhiteAlpha (0.2f, 1.0f));
			NavigationController.NavigationBar.BarTintColor = barColor;
			NavigationController.Toolbar.TintColor = barColor;
			NavigationController.View.TintColor = GetUIColor (UIColor.FromRGBA (0.25f, 0.25f, 0.92f, 1f), UIColor.FromRGBA (1.0f, 0.00f, 0.50f, 1.0f));
			NavigationController.NavigationBar.TitleTextAttributes = new UIStringAttributes { ForegroundColor = GetUIColor (UIColor.Black, UIColor.White) };
			NavigationController.NavigationBar.BarStyle = UIBarStyle.Black;
			NavigationController.SetToolbarHidden (true, animated);
		}

		UIColor GetUIColor (UIColor lightMode, UIColor darkMode)
		{
			if (UIDevice.CurrentDevice.CheckSystemVersion (13, 0))
				return UIColor.FromDynamicProvider ((t) => t.UserInterfaceStyle == UIUserInterfaceStyle.Dark ? darkMode : lightMode);
			return lightMode;
		}

		public override UIStatusBarStyle PreferredStatusBarStyle ()
		{
			return UIStatusBarStyle.LightContent;
		}
	}
}
