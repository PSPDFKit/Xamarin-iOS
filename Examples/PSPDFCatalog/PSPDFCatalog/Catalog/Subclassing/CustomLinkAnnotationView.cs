using System;
using Foundation;
using UIKit;
using CoreGraphics;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class CustomLinkAnnotationView : PSPDFLinkAnnotationView {
		// MUST HAVE ctor when Subclassing!!! It will crash otherwise.
		public CustomLinkAnnotationView (IntPtr handle) : base (handle)
		{
		}

		// You must manually export Constructors when you need them in your subclasses, this will be called
		// by PSPDFKit when creating LinkAnnotationViews.
		[Export ("initWithFrame:")]
		public CustomLinkAnnotationView (CGRect frame) : base (frame)
		{
			BackgroundColor = UIColor.Red.ColorWithAlpha (0.3f);
		}
	}
}

