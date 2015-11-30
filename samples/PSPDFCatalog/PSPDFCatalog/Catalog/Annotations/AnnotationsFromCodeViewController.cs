using System;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;

using PSPDFKit.iOS;

namespace PSPDFCatalog
{
	public class AnnotationsFromCodeViewController : PSPDFViewController
	{
		PSPDFAnnotation [] annotationsArr;

		public AnnotationsFromCodeViewController (NSData documentData) : base (new PSPDFDocument (documentData))
		{
			Document.Title = "Programmatically create annotations";
			Document.AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled;

			var annotationsList = new List<PSPDFAnnotation> ();
			var maxHeight = Document.GetPageInfo (0).RotatedRect.Size.Height;
			for (int i = 0; i < 5; i++) {
				var note = new PSPDFNoteAnnotation () {
					// width/height will be ignored for note annotations.
					BoundingBox = new CGRect (100, (50 + i * maxHeight / 5), 32, 32),
					Contents = string.Format ("Note {0}", (5 - i)) // notes are added bottom-up
				};
				annotationsList.Add (note);
			}

			// Ink Annotation sample
			var inkAnnot = new PSPDFInkAnnotation ();
			inkAnnot.Lines = new List<NSValue[]> () {
				new [] {
					NSValue.FromCGPoint (new CGPoint (480.93079f, 596.0625f)),
					NSValue.FromCGPoint (new CGPoint (476.8027f, 592.96881f)),
					NSValue.FromCGPoint (new CGPoint (468.54639f, 585.75f)),
					NSValue.FromCGPoint (new CGPoint (456.1619f, 574.40631f)),
					NSValue.FromCGPoint (new CGPoint (436.5531f, 550.6875f)),
					NSValue.FromCGPoint (new CGPoint (357.086f, 434.15631f)),
					NSValue.FromCGPoint (new CGPoint (294.1315f, 359.90631f)),
					NSValue.FromCGPoint (new CGPoint (226.01691f, 284.625f)),
					NSValue.FromCGPoint (new CGPoint (176.4789f, 222.75f))
				}
			};
			inkAnnot.LineWidth = 5;
			inkAnnot.Color = UIKit.UIColor.White;
			annotationsList.Add (inkAnnot);

			annotationsArr = annotationsList.ToArray ();
			Document.AddAnnotations (annotationsArr, null);
		}
	}
}

