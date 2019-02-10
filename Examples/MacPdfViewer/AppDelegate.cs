using AppKit;
using Foundation;
using PSPDFKit.Model;

namespace MacPdfViewer {
	[Register ("AppDelegate")]
	public class AppDelegate : NSApplicationDelegate {

		public override void DidFinishLaunching (NSNotification notification)
		{
			// Set your license key here. PSPDFKit is commercial software.
			// Each PSPDFKit license is bound to a specific app bundle id.
			// Visit https://customers.pspdfkit.com to get your license key.
			PSPDFKitGlobal.SetLicenseKey ("YOUR_LICENSE_KEY_GOES_HERE");
		}

		public override void WillTerminate (NSNotification notification)
		{
			// Insert code here to tear down your application
		}

		public override bool ApplicationShouldTerminateAfterLastWindowClosed (NSApplication sender) => true;
	}
}
