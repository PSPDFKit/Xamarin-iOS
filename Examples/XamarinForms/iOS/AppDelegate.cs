using System;
using System.Collections.Generic;
using System.Linq;

using Foundation;
using PSPDFKit.Model;
using UIKit;

namespace XFSample.iOS {
	[Register ("AppDelegate")]
	public partial class AppDelegate : global::Xamarin.Forms.Platform.iOS.FormsApplicationDelegate {
		public override bool FinishedLaunching (UIApplication app, NSDictionary options)
		{
			PSPDFKitGlobal.SetLicenseKey ("YOUR_LICENSE_KEY_GOES_HERE");
			global::Xamarin.Forms.Forms.Init ();

			LoadApplication (new App ());

			return base.FinishedLaunching (app, options);
		}
	}
}
