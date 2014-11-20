using System;
using Foundation;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class PlayGroundViewController : PSPDFViewController
	{
		public PlayGroundViewController (PSPDFDocument document) : base (document)
		{
		}

		public PlayGroundViewController (NSUrl documentPath) : this (new PSPDFDocument (documentPath))
		{
		}
	}
}

