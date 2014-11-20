using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit;

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
			var alert = new PSPDFAlertView ("Bookmarking detected", string.Format ("You added page {0} to bookmarks", page + 1));
			alert.AddButton ("Ok");
			alert.Show ();
			return base.AddBookmark (page);
		}

		public override bool RemoveBookmark (nuint page)
		{
			var alert = new PSPDFAlertView ("Remove Action", string.Format ("You removed page {0} from bookmarks", page + 1));
			alert.AddButton ("Ok");
			alert.Show ();
			return base.RemoveBookmark (page);
		}

		public override bool SaveBookmarks (out NSError error)
		{
			InvokeOnMainThread (() => {
				var alert = new PSPDFAlertView ("Bookmark Subclass Message", string.Format ("Intercepted bookmark saving; current bookmarks are: {0}", Bookmarks.Count ()));
				alert.Show ();
			});
			return base.SaveBookmarks (out error);
		}
	}
}

