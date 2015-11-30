using System;

using PSPDFKit.iOS;

namespace PSPDFCatalog
{
	public class CustomPDFDocument : PSPDFDocument
	{
		public CustomPDFDocument (Foundation.NSUrl url) : base (url)
		{
		}

		// Force-disable all permissions like text copying, even though the PDF permissions would allow this.
		public override PSPDFDocumentPermissions Permissions {
			get {
				Console.WriteLine ("Requesting Permissions");
				return PSPDFDocumentPermissions.NoFlags;
			}
		}
	}
}
