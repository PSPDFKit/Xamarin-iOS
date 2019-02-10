using System;
using UIKit;
using PSPDFKit.Model;
using PSPDFKit.UI;
using Foundation;

namespace PSPDFCatalog {
	public class PSCRotatePagePDFViewController : PSPDFViewController {
		public PSCRotatePagePDFViewController (PSPDFDocument document) : base (document)
		{
		}

		public PSCRotatePagePDFViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			var rotatePageButton = new UIBarButtonItem ("Rotate Page", UIBarButtonItemStyle.Plain, RotateHandler);
			NavigationItem.RightBarButtonItems = new UIBarButtonItem [] { ThumbnailsButtonItem, SearchButtonItem, rotatePageButton };
		}

		async void RotateHandler (object sender, EventArgs e)
		{
			if (!Document.Valid)
				return;

			var editor = new PSPDFDocumentEditor (Document);
			editor.RotatePages (new NSIndexSet (PageIndex), 90);
			await editor.SaveAsync ();
			InvokeOnMainThread (ReloadData);
		}
	}
}
