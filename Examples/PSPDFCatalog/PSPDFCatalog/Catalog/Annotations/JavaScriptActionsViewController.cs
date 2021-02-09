using Foundation;
using PSPDFKit.UI;
using PSPDFKit.Model;
using CoreGraphics;

namespace PSPDFCatalog {
	public class JavaScriptActionsViewController: PSPDFViewController {
		public JavaScriptActionsViewController (PSPDFDocument document) : base (document)
		{
			var pageInfo = document.GetPageInfo (0);
			var stampAnnotation = new PSPDFStampAnnotation ();

			// We place the stamp in the middle of the page.
			stampAnnotation.BoundingBox = new CGRect (pageInfo.Size.Width / 2, pageInfo.Size.Height / 2, 150, 100);
			stampAnnotation.Title = "Tap me!";

			document.AddAnnotations (new [] { stampAnnotation }, options: null);

			// When tapping the stamp annotation, an alert window will appear via the JavaScript action.
			var triggerEventValue = new NSNumber ((int) PSPDFAnnotationTriggerEvent.MouseUp);
			var action = new PSPDFJavaScriptAction ("app.alert(\"Hello, it's me. I was wondering...\");");
			stampAnnotation.AdditionalActions = new NSDictionary<NSNumber, PSPDFAction> (new [] { triggerEventValue }, new [] { action });
		}
	}
}
