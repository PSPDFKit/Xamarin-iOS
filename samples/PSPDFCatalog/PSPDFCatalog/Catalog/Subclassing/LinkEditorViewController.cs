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
		public LinkEditorViewController (PSPDFDocument document, PSPDFConfiguration config) : base (document, config)
		{
		}
	}
}

