using System;
using System.Collections.Generic;
using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	[Register ("CustomBookmarkProvider")]
	public class CustomBookmarkProvider : NSObject, IPSPDFBookmarkProvider {
		List<PSPDFBookmark> bookmarks = new List<PSPDFBookmark> ();

		public CustomBookmarkProvider (IntPtr handle) : base (handle)
		{
		}

		public CustomBookmarkProvider ()
		{
		}

		public PSPDFBookmark [] Bookmarks {
			get {
				return bookmarks.ToArray ();
			}
		}

		public void Save ()
		{
			// Do nothing here, since we are not storing bookmarks anywhere
		}

		public bool AddBookmark (PSPDFBookmark bookmark)
		{
			bookmarks.Add (bookmark);
			InvokeOnMainThread (() => PSPDFStatusHUDItem.CreateSuccess ($"You added page {bookmark.PageIndex + 1} from bookmarks").PushAndPop (1, true, null));
			return true;
		}

		public bool RemoveBookmark (PSPDFBookmark bookmark)
		{
			bookmarks.Remove (bookmark);
			return true;
		}
	}
}

