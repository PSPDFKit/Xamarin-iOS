using System;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;

using PSPDFKit;

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
			var maxHeight = Document.PageInfoForPage (0).RotatedRect.Size.Height;
			for (int i = 0; i < 5; i++) {
				var note = new PSPDFNoteAnnotation () {
					// width/height will be ignored for note annotations.
					BoundingBox = new CGRect (100, (50 + i * maxHeight / 5), 32, 32),
					Contents = string.Format ("Note {0}", (5 - i)) // notes are added bottom-up
				};
				annotationsList.Add (note);
			}
			annotationsArr = annotationsList.ToArray ();
			Document.AddAnnotations (annotationsArr);
		}
	}
}

