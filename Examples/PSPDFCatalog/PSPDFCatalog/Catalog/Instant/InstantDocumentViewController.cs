using System;

using Foundation;
using UIKit;

using PSPDFKit.Instant;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class InstantDocumentViewController : PSPDFInstantViewController {

		PSPDFInstantClient client;
		IPSPDFInstantDocumentDescriptor documentDescriptor;
		string documentCode;
		NSUrl webUrl;

		public InstantDocumentViewController (InstantDocumentInfo docInfo) : base (document: null)
		{
			//Create the Instant objects with the information from the web-preview server.

			//The `PSPDFDocument` you get from Instant does not retain the objects that create it,
			//so we need to keep references to the client and document descriptor otherwise with no

			//strong references they would deallocate and syncing would stop.
			client = new PSPDFInstantClient (NSUrl.FromString (docInfo.ServerUrl), out var err);
			documentDescriptor = client.GetDocumentDescriptor (docInfo.Jwt, out var error);

			// Store document code and URL (which also contains the code) for sharing later.
			documentCode = docInfo.EncodedDocumentId;
			webUrl = NSUrl.FromString (docInfo.Url);

			// Tell Instant to download the document from web-preview’s PSPDFKit Server instance.
			documentDescriptor.Download (docInfo.Jwt, out var _);

			// Get the `PSPDFDocument` from Instant.
			Document = documentDescriptor.EditableDocument;

			var collaborateItem = new UIBarButtonItem ("Collaborate", UIBarButtonItemStyle.Plain, ShowCollaborationOptions);
			var barButtonItems = new [] { collaborateItem, AnnotationButtonItem };
			NavigationItem.SetRightBarButtonItems (barButtonItems, PSPDFViewMode.Document, false);
		}

		void ShowCollaborationOptions (object sender, EventArgs e)
		{
			var alertController = UIAlertController.Create ($"Document Code: {documentCode}", null, UIAlertControllerStyle.ActionSheet);
			if (alertController.PopoverPresentationController != null)
				alertController.PopoverPresentationController.BarButtonItem = sender as UIBarButtonItem;

			alertController.AddAction (UIAlertAction.Create ("Open in Safari", UIAlertActionStyle.Default, (obj) => UIApplication.SharedApplication.OpenUrl (webUrl)));
			alertController.AddAction (UIAlertAction.Create ("Share Document Link", UIAlertActionStyle.Default, (obj) => ShowActivityViewController (webUrl, sender as UIBarButtonItem)));
			alertController.AddAction (UIAlertAction.Create ("Share Document Code", UIAlertActionStyle.Default, (obj) => ShowActivityViewController ((NSString) documentCode, sender as UIBarButtonItem)));
			alertController.AddAction (UIAlertAction.Create ("Cancel", UIAlertActionStyle.Cancel, null));

			PresentViewController (alertController, true, null);

			void ShowActivityViewController (NSObject items, UIBarButtonItem barButtonItem)
			{
				var activityViewController = new UIActivityViewController (new [] { items }, null);
				if (activityViewController.PopoverPresentationController != null)
					activityViewController.PopoverPresentationController.BarButtonItem = barButtonItem;
				PresentViewController (activityViewController, true, null);
			}
		}

		protected override void Dispose (bool disposing)
		{
			if (disposing) {
				// Since this demo is ephemeral, clean up immediately.
				// Note that this also cancels syncing that is in-progress.
				documentDescriptor.RemoveLocalStorage (out var error);
			}
			base.Dispose (disposing);
		}

        public override void ViewDidDisappear(bool animated)
        {
            base.ViewDidDisappear(animated);

            // Since this demo is ephemeral, clean up immediately.
            // Note that this also cancels syncing that is in-progress.
            documentDescriptor.RemoveLocalStorage(out var error);
        }
	}
}
