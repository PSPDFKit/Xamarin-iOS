﻿using System;
using System.Collections.Generic;
using Foundation;
using CoreGraphics;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;

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
			var maxHeight = pageInfo.Size.Height;

			//Note Annotation
			for (int i = 0; i < 5; i++) {
				var note = new PSPDFNoteAnnotation {
					// width/height will be ignored for note annotations.
					BoundingBox = new CGRect (100, (50 + i * maxHeight / 5), 32, 32),
					Contents = string.Format ("Note {0}", (5 - i)) // notes are added bottom-up
				};
				annotationsList.Add (note);
			}

			// Ink Annotation
			var inkAnnot = new PSPDFInkAnnotation ();
			var linesArr = NSArray<NSValue>.FromNSObjects (
				NSValue.FromCGPoint (new CGPoint (300.0f, 300.0f)),
				NSValue.FromCGPoint (new CGPoint (400.0f, 550.0f))
		   	);
			var lines = new NSArray<NSValue> [] { linesArr };

			CGRect viewRect = UIScreen.MainScreen.Bounds;

			inkAnnot.Lines = PSPDFInkAnnotation.ConvertViewLinesToPdfLines (lines, pageInfo, viewRect);
			inkAnnot.LineWidth = 5;
			inkAnnot.Color = UIColor.Red;
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
				//pageView.SelectedAnnotations = new PSPDFAnnotation [] {};
			}
		}
	}
}

