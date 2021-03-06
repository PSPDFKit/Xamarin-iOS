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
				NSValue.FromCGPoint (new CGPoint (300.0f, 300.0f)),
				NSValue.FromCGPoint (new CGPoint (400.0f, 550.0f))
		   	);
			var lines = new NSArray<NSValue> [] { linesArr };

			CGRect viewRect = UIScreen.MainScreen.Bounds;
			var pageInfo = document.GetPageInfo (targetPage);

			inkAnnot.Lines = PSPDFInkAnnotation.ConvertViewLinesToPdfLines (lines, pageInfo, viewRect);
			inkAnnot.LineWidth = 5;
			inkAnnot.Color = UIColor.Red;
			return inkAnnot;
		}
	}
}
