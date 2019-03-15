using System;
using Foundation;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;
using ObjCRuntime;

namespace PSPDFCatalog
{



    public class PSCAnalyticsViewController : PSPDFViewController, IPSPDFViewControllerDelegate
    {
        public class AnalyticsClient : IPSPDFAnalyticsClient
        {
            IntPtr INativeObject.Handle => throw new NotImplementedException();

            public void LogEvent(string @event, NSDictionary attributes)
            {
                string output = String.Format("{0}: {1}", @event, attributes);
                Console.WriteLine(output);
                Console.WriteLine("123");
            }

            void IDisposable.Dispose()
            {
                Console.WriteLine("1234");
            }
        }

        public AnalyticsClient AnalyticsStuff { get; set; }

        public PSCAnalyticsViewController(PSPDFDocument document) : base(document)
        {
        }

        public PSCAnalyticsViewController(IntPtr handle) : base(handle)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Console.WriteLine("hi");

            // Use our custom method when tapping the bookmark button
            this.ActivityButtonItem.Clicked += (sender, args) =>
            {
                NameBookmark();
            };
        }

        public void PdfViewControllerDidDismiss(PSPDFViewController pdfController)
        {
            var analytics = PSPDFKitGlobal.SharedInstance.GetAnalytics();
            analytics.RemoveAnalyticsClient(AnalyticsStuff);
            analytics.Enabled = false;
            analytics.LogEvent(PSPDFAnalyticsEventName.ShareKey);
        }

        public void NameBookmark()
        {
            var analytics = PSPDFKitGlobal.SharedInstance.GetAnalytics();
            analytics.AddAnalyticsClient(AnalyticsStuff);
            analytics.Enabled = true;
            analytics.LogEvent(PSPDFAnalyticsEventName.OutlineOpenKey);
            Console.WriteLine("hi");
        }
    }
}
