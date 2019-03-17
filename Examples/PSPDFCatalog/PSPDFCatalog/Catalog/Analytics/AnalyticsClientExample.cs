using System;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	class AnalyticsLogger : NSObject, IPSPDFAnalyticsClient {
		public void LogEvent (string @event, NSDictionary attributes) => Console.WriteLine ($"{@event}: {attributes?.Description}");
	}

	public class AnalyticsClientExample : PSPDFViewController, IPSPDFViewControllerDelegate {
		readonly AnalyticsLogger AnalyticsLogger = new AnalyticsLogger ();

		public AnalyticsClientExample (NSUrl documentPath) : base (new PSPDFDocument (documentPath))
		{
			Title = "Analytics Client";

			var analytics = PSPDFKitGlobal.SharedInstance.GetAnalytics ();
			analytics.AddAnalyticsClient (AnalyticsLogger);
			analytics.Enabled = true;
			Delegate = this;

			// sending custom events.
			analytics.LogEvent ("catalog_analytics_example_open");
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Hijack built-in ActivityButtonItem button to add custom logging.
			var buttons = NavigationItem.RightBarButtonItems.ToList ();
			var shareButton = buttons.FirstOrDefault (b => b.AccessibilityIdentifier == "Share");
			var idx = buttons.IndexOf (shareButton);
			buttons.RemoveAt (idx);

			var tmpButton = new UIBarButtonItem (shareButton.Image, shareButton.Style, (sender, e) => {
				var analytics = PSPDFKitGlobal.SharedInstance.GetAnalytics ();

				// sending custom events.
				analytics.LogEvent ("catalog_analytics_example_ActivityButton");

				// Exec ActivityButtonItem built-in magic.
				UIApplication.SharedApplication.SendAction (shareButton.Action, shareButton.Target, this, null);
			});

			buttons.Insert (idx, tmpButton);
			NavigationItem.RightBarButtonItems = buttons.ToArray ();
		}

		[Export ("pdfViewControllerDidDismiss:")]
		public void PdfViewControllerDidDismiss (PSPDFViewController pdfController)
		{
			var analytics = PSPDFKitGlobal.SharedInstance.GetAnalytics ();

			// sending custom events.
			analytics.LogEvent ("catalog_analytics_example_exit");

			analytics.RemoveAnalyticsClient (AnalyticsLogger);
			analytics.Enabled = false;
		}
	}
}
