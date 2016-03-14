using System;
using UIKit;
using Foundation;

using PSPDFKit.iOS;

namespace PSPDFCatalog {
	
	public class PSCStylusEnabledAnnotationToolbar : PSPDFAnnotationToolbar {

		// Needed ctor by the runtime, it should not contain any initialization logic
		public PSCStylusEnabledAnnotationToolbar (IntPtr handle) : base (handle) { }

		// It is important that this ctor is decorated with its objc ctor name
		[Export ("initWithAnnotationStateManager:")]
		public PSCStylusEnabledAnnotationToolbar (PSPDFAnnotationStateManager annotationStateManager) : base (annotationStateManager)
		{
			AdditionalButtons = new UIButton[] { annotationStateManager.StylusStatusButton };
		}
	}
}

