using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit.iOS;

namespace PSPDFCatalog
{
	public class CustomBookmarkParser : PSPDFBookmarkParser
	{
		// MUST HAVE ctor when Subclassing!!! It will crash otherwise.
		public CustomBookmarkParser (IntPtr handle) : base (handle)
		{
		}

		public override bool AddBookmark (nuint page)
		{
			var alert = new UIAlertView ("Bookmarking detected", string.Format ("You added page {0} to bookmarks", page + 1), null, "Ok", null);
			alert.Show ();
			return base.AddBookmark (page);
		}

		public override bool RemoveBookmark (nuint page)
		{
			var alert = new UIAlertView ("Remove Action", string.Format ("You removed page {0} from bookmarks", page + 1), null, "Ok", null);
			alert.Show ();
			return base.RemoveBookmark (page);
		}

		public override bool SaveBookmarks (out NSError error)
		{
			InvokeOnMainThread (() => {
				var alert = new UIAlertView ("Bookmark Subclass Message", string.Format ("Intercepted bookmark saving; current bookmarks are: {0}", Bookmarks.Count ()), null, "Ok", null);
				alert.Show ();
			});
			return base.SaveBookmarks (out error);
		}
	}
}

