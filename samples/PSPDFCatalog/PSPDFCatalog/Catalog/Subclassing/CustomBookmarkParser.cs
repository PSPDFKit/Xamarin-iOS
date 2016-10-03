using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit.iOS;

namespace PSPDFCatalog
{
	[Register ("CustomBookmarkProvider")]
	public class CustomBookmarkProvider : NSObject, IPSPDFBookmarkProvider
	{
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

		public void Add (PSPDFBookmark bookmark)
		{
			bookmarks.Add (bookmark);
			InvokeOnMainThread (() => PSPDFStatusHUDItem.GetSuccessHud ($"You added page {bookmark.PageIndex + 1} from bookmarks").PushAndPop (1, true, null));
		}

		public void Remove (PSPDFBookmark bookmark)
		{
			bookmarks.Remove (bookmark);
		}

		public void Save ()
		{
			// Do nothing here, since we are not storing bookmarks anywhere
		}
	}
}

