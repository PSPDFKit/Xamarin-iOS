// WARNING
//
// This file has been generated automatically by Visual Studio to store outlets and
// actions made in the UI designer. If it is removed, they will be lost.
// Manual changes to this file may not be handled correctly.
//
using Foundation;
using System.CodeDom.Compiler;

namespace MacPdfViewer
{
	[Register ("ViewController")]
	partial class ViewController
	{
		[Outlet]
		AppKit.NSTextField PdfCurrentPage { get; set; }

		[Outlet]
		AppKit.NSImageView PdfImageView { get; set; }

		[Outlet]
		AppKit.NSTextView PdfTextInfo { get; set; }

		[Action ("NextPage:")]
		partial void NextPage (AppKit.NSButton sender);

		[Action ("PreviousPage:")]
		partial void PreviousPage (AppKit.NSButton sender);
		
		void ReleaseDesignerOutlets ()
		{
			if (PdfImageView != null) {
				PdfImageView.Dispose ();
				PdfImageView = null;
			}

			if (PdfTextInfo != null) {
				PdfTextInfo.Dispose ();
				PdfTextInfo = null;
			}

			if (PdfCurrentPage != null) {
				PdfCurrentPage.Dispose ();
				PdfCurrentPage = null;
			}
		}
	}
}
