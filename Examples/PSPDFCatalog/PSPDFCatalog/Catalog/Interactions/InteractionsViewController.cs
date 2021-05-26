using System;
using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog {
	public class InteractionsController : PSPDFViewController {
		public InteractionsController (PSPDFDocument document) : base (document)
		{
			//Set up your gesture recognizer.
			var gestureRecognizer = new UITapGestureRecognizer ();
			var selector = new ObjCRuntime.Selector ("TapGestureRecognizerDidChangeState:");
			gestureRecognizer.AddTarget (this, selector);

			// Make it work simultaneously with all built-in interaction components.
			Interactions.AllInteractions.AllowSimultaneousRecognition (gestureRecognizer);

			// Add your gesture recognizer to the document view controller's view.
			DocumentViewController.View.AddGestureRecognizer (gestureRecognizer);
		}

		// Perform your action.
		[Action ("TapGestureRecognizerDidChangeState:")]
		private void TapGestureRecognizerDidChangeState (UITapGestureRecognizer gestureRecognizer)
		{
			if (gestureRecognizer.State == UIGestureRecognizerState.Ended) {
				var pageView = DocumentViewController.GetVisiblePageView (gestureRecognizer.LocationInView (DocumentViewController.View));
				if (pageView != null) {
					Console.WriteLine ($"Tapped at point: {gestureRecognizer.LocationInView (DocumentViewController.View)} in page view: {pageView}");
				}
			}
		}

	}

}
