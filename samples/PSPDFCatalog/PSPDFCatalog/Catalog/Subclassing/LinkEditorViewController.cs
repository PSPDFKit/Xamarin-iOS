using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit.iOS;

namespace PSPDFCatalog
{
	public class LinkEditorViewController : PSPDFViewController
	{
		public LinkEditorViewController (PSPDFDocument document) : base (document)
		{
			Document.EditableAnnotationTypes = new NSOrderedSet (
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
				(NSObject) PSPDFAnnotationString.Stamp 
			);
		}
	}
}

