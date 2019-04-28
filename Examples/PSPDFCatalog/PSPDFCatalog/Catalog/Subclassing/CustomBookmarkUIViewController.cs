using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog {
	public class BookmarkViewController : PSPDFViewController {
		public BookmarkViewController (PSPDFDocument document) : base (document)
		{
		}

		public BookmarkViewController (NSUrl documentPath) : this (new PSPDFDocument (documentPath))
		{
		}

		public override void CommonInit (PSPDFDocument document, PSPDFConfiguration configuration)
		{
			base.CommonInit (document, configuration);

			// Use our custom method when tapping the bookmark button
			BookmarkButtonItem.Clicked +=  (sender, args) => {
				NameBookmark ();
			};

			NavigationItem.SetRightBarButtonItems (new [] { ThumbnailsButtonItem, OutlineButtonItem, SearchButtonItem, BookmarkButtonItem }, PSPDFViewMode.Document, false);
		}

		public void NameBookmark ()
		{
			// We need to properly handle adding and removing bookmarks
			if (Document.BookmarkManager.GetBookmarkForPage ((uint) PageIndex) == null) {
				var alert = UIAlertController.Create ("", "Please name your bookmark:", UIAlertControllerStyle.Alert);
				var action = new PSPDFGoToAction (PageIndex);
				// We need a mutable copy since we can't edit the name of the bookmark otherwise
				var mutableBookmark = new PSPDFMutableBookmark (action);

				alert.AddTextField (textField => {
					// This will be the dault name of the bookmark if not name is entered into the text field
					textField.Placeholder = string.Format ("Page {0}", PageIndex+1);
				});

				var okAction = UIAlertAction.Create ("OK", UIAlertActionStyle.Default, Handle => {
					// We us the content of the textfield as the name of the bookmark
					mutableBookmark.Name = alert.TextFields[0].Text;
					Document.BookmarkManager.AddBookmark (mutableBookmark);
				});

				var cancelAction = UIAlertAction.Create ("Cancel", UIAlertActionStyle.Destructive, null);

				alert.AddAction (cancelAction);
				alert.AddAction (okAction);
				PresentViewController (alert, false, null);
			} else
				Document.BookmarkManager.RemoveBookmarksForPage ((uint) PageIndex);
		}
	}
}

