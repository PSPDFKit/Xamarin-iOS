using Foundation;
using System;
using PSPDFKit.Model;
using UIKit;
using CoreGraphics;

namespace PSPDFCatalog {
	// This class is used for creating sample annotations that usually take a few lines of code to generate.
	// We define some of them here to make our lives easier in other examples, where we just need a generic annotation of some type.
	public class AnnotationHelper {
		public AnnotationHelper ()
		{
		}

		public static PSPDFAnnotation GetAnnotationOfType (PSPDFAnnotationType type, PSPDFDocument document, uint targetPage)
		{
			switch (type) {
			case PSPDFAnnotationType.Ink:
				return GetInkAnnotation (document, targetPage);
			default:
				Console.WriteLine ($"No sample annotation of type {type} available.");
				return null;
			}
		}

		private static PSPDFInkAnnotation GetInkAnnotation (PSPDFDocument document, uint targetPage)
		{
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

			CGRect viewRect = UIScreen.MainScreen.Bounds;
			var pageInfo = document.GetPageInfo (targetPage);

			inkAnnot.Lines = PSPDFInkAnnotation.ConvertViewLinesToPdfLines (lines, pageInfo, viewRect);
			inkAnnot.LineWidth = 5;
			inkAnnot.Color = UIColor.White;
			return inkAnnot;
		}
	}
}
