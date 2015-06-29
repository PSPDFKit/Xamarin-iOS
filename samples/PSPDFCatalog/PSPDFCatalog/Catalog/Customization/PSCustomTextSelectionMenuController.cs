using System;
using System.Collections.Generic;
using System.Linq;
using Foundation;
using UIKit;
using CoreGraphics;

using PSPDFKit;
using ObjCRuntime;

namespace PSPDFCatalog {
	
	public class PSCustomTextSelectionMenuController : PSPDFViewController, IPSPDFViewControllerDelegate {
		
		public PSCustomTextSelectionMenuController () : base ()
		{
			Delegate = this;
		}

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedText:inRect:onPageView:")]
		public PSPDFMenuItem[] ShouldShowMenuItemsForSelectedText (PSPDFViewController pdfController, PSPDFMenuItem[] menuItems, CGRect rect, string selectedText, CGRect textRect, PSPDFPageView pageView)
		{
			// Disable Wikipedia
			// Be sure to check for PSPDFMenuItem class; there might also be classic UIMenuItems in the array.
			// Note that for words that are in the iOS dictionary, instead of Wikipedia we show the "Define" menu item with the native dict.
			// There is also a simpler way to disable wikipedia (See PSPDFTextSelectionMenuAction)
			var newMenuItems = menuItems.Where ((item) => !(item.IsKindOfClass (new Class (typeof (PSPDFMenuItem))) && item.Identifier == "Wikipedia")).ToList ();

			// Add option to Google for it.
			newMenuItems.Add (new PSPDFMenuItem ("Google", () => {
				var queryUri = new Uri (string.Format ("http://www.google.com/search?q={0}", selectedText));
				var nsurl = new NSUrl (queryUri.GetComponents (UriComponents.HttpRequestUrl, UriFormat.UriEscaped));

				var browser = new PSPDFWebViewController (nsurl) {
					Delegate = pdfController,
					PreferredContentSize = new CGSize (600, 500)
				};

				var browserOptions = NSDictionary.FromObjectsAndKeys (
					new NSObject[] { NSValue.FromCGRect (rect), NSNumber.FromBoolean (true), NSNumber.FromBoolean (true) },
					new NSObject[] { PSPDFPresentationKeys.RectKey, PSPDFPresentationKeys.InNavigationControllerKey, PSPDFPresentationKeys.CloseButtonKey }
				);

				NSError err;
				pdfController.PresentViewController (browser, browserOptions, true, null, out err, null);

			}, "Google"));

			return newMenuItems.ToArray ();
		}
	}
}

