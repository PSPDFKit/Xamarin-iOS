﻿
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
        public static readonly string ReaderViewFile = "Pdf/The-Cosmic-Context-for-Life.pdf";

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
                    new StringElement ("Reader View", () => {
                        var readerViewController = new ReaderViewController (NSUrl.FromFilename (ReaderViewFile));
                        NavigationController.PushViewController (readerViewController, true);
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
                    new StringElement ("Disable Ink Annotation Interaction", () => {
                        var pdfViewer = new PSPDFViewController (new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile))) {
                            PageIndex = 15
                        };

                        pdfViewer.Interactions.TransformAnnotation.AddActivationCondition ((context, point, coordinateSpace) => {
                            // Due to a Xamarin.iOS bug we can't do this cast but we can workaround it for now
                            //var ctx = context as PSPDFAnnotationTransformationContext<PSPDFAnnotation>;
                            var ctx = ObjCRuntime.Runtime.GetNSObject<PSPDFAnnotationTransformationContext<PSPDFAnnotation>> (context);
                            return !(ctx?.Annotation is PSPDFInkAnnotation);
                        });

                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Execute JavaScript Actions on Annotation tap", () => {
                        var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new JavaScriptActionsViewController (document);
                        NavigationController.PushViewController (pdfViewer, true);
                    })
                },
                new Section ("Forms"){
                    new StringElement ("Programamtically Fill Form Fields", () => {
                        var documenturl = NSUrl.FromFilename (FormFile);
                        var pdfViewer = new ProgrammaticFormFillingController (documenturl);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                    new StringElement ("Programamtically Create Form Fields", () => {
                        var document = new PSPDFDocument(NSUrl.FromFilename (FormFile));
                        var pdfViewer = new CreateFormFieldFromCodeViewController (document);
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
                new Section ("Interactions"){
                    new StringElement ("Get Touch Coordinates", () => {
                        var document =new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new InteractionsController (document);
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
                new Section("Search")
                {
                    new StringElement ("FTS with Document Picker", () =>
                    {
                        var documentPickerController = new FTSDocumentPickerController ("/Bundle/Pdf", true, PSPDFKitGlobal.SharedInstance.Library);
                        NavigationController.PushViewController(documentPickerController, true);
                    }),
                    new StringElement ("Full Text Search", () =>
                    {
                        var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new IndexedFullTextSearchController(document);
                        NavigationController.PushViewController(pdfViewer, true);

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
                    new StringElement ("Custom Free-Text Annotation Inspector", () => {
                        var document = new PSPDFDocument (NSUrl.FromFilename (PSPDFKitFile));
                        var pdfViewer = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
                            var annotationProperties = new NSMutableDictionary(builder.PropertiesForAnnotations);
                            // Options you want to keep in the inspector.
                            var inspectorOptions = new NSString[] { PSPDFAnnotationStyleKey.Color, PSPDFAnnotationStyleKey.LineWidth };
                            var propertiesArray = NSArray.FromNSObjects(inspectorOptions);

                            // Choose the annotation type for which you want to customize the inspector. 
                            annotationProperties.SetValueForKey(NSArray.FromNSObjects(propertiesArray), (NSString)"FreeText");
                            builder.PropertiesForAnnotations = annotationProperties;
                        }));
                        NavigationController.PushViewController (pdfViewer, true);
                    }),
                },
			};
		}
	}
}
