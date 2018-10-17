using System;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;

using PSPDFKit.Core;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog {
	public class AnnotationsFromCodeViewController : PSPDFViewController, IPSPDFViewControllerDelegate {
		PSPDFAnnotation [] annotationsArr;

		public AnnotationsFromCodeViewController (NSUrl document) : base (new PSPDFDocument (document))
		{
			// Sets the Delegate to listen for 'IPSPDFViewControllerDelegate' events like 'DidSelectAnnotations'.
			// Any NSObject derived class can be set to 'Delegate' property as long as it implements 'IPSPDFViewControllerDelegate'.
			Delegate = this;

			Document.Title = "Programmatically create annotations";
			Document.AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled;

			var annotationsList = new List<PSPDFAnnotation> ();
			uint targetPage = 0;
			var pageInfo = Document.GetPageInfo (targetPage);
			CGRect viewRect = UIScreen.MainScreen.Bounds;
			var maxHeight = pageInfo.Size.Height;
			for (int i = 0; i < 5; i++) {
				var note = new PSPDFNoteAnnotation {
					// width/height will be ignored for note annotations.
					BoundingBox = new CGRect (100, (50 + i * maxHeight / 5), 32, 32),
					Contents = string.Format ("Note {0}", (5 - i)) // notes are added bottom-up
				};
				annotationsList.Add (note);
			}

			// Ink Annotation sample
			var inkAnnot = new PSPDFInkAnnotation ();
			var linesArr = NSArray<NSValue>.FromNSObjects (
				NSValue.FromCGPoint (new CGPoint (480.93079f, 596.0625f)),
				NSValue.FromCGPoint (new CGPoint (476.8027f, 592.96881f)),
				NSValue.FromCGPoint (new CGPoint (468.54639f, 585.75f)),
				NSValue.FromCGPoint (new CGPoint (456.1619f, 574.40631f)),
				NSValue.FromCGPoint (new CGPoint (436.5531f, 550.6875f)),
				NSValue.FromCGPoint (new CGPoint (357.086f, 434.15631f)),
				NSValue.FromCGPoint (new CGPoint (294.1315f, 359.90631f)),
				NSValue.FromCGPoint (new CGPoint (226.01691f, 284.625f)),
				NSValue.FromCGPoint (new CGPoint (176.4789f, 222.75f))
		   	);
			var lines = new NSArray<NSValue> [] { linesArr };

			inkAnnot.Lines = PSPDFInkAnnotation.ConvertViewLinesToPdfLines (lines, pageInfo, viewRect);
			inkAnnot.LineWidth = 5;
			inkAnnot.Color = UIColor.White;
			annotationsList.Add (inkAnnot);

			annotationsArr = annotationsList.ToArray ();
			Document.AddAnnotations (annotationsArr, options: null);
		}

		// Comes from 'IPSPDFViewControllerDelegate'. This Called after an annotation has been selected by a touch directly on the page view.
		// https://pspdfkit.com/api/ios/Protocols/PSPDFViewControllerDelegate.html#/c:objc(pl)PSPDFViewControllerDelegate(im)pdfViewController:didSelectAnnotations:onPageView:
		[Export ("pdfViewController:didSelectAnnotations:onPageView:")]
		public void DidSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView)
		{
			if (annotations == null)
				return;

			foreach (var annotation in annotations) {
				Console.WriteLine ($"Selected Annotation: {annotation.Name}");
			}
		}
	}
}

