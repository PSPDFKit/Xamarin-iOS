using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

using PSPDFKit.Core;

namespace PSPDFCatalog
{
	// The UIApplicationDelegate for the application. This class is responsible for launching the
	// User Interface of the application, as well as listening (and optionally responding) to
	// application events from iOS.
	[Register ("AppDelegate")]
	public partial class AppDelegate : UIApplicationDelegate
	{
		// class-level declarations
		public override UIWindow Window {
			get;
			set;
		}

		DVCMenu viewController;
		UINavigationController navController;

		public override bool FinishedLaunching(UIApplication application, NSDictionary launchOptions)
		{
			// Set your license key here. PSPDFKit is commercial software.
			// Each PSPDFKit license is bound to a specific app bundle id.
			// Visit https://customers.pspdfkit.com to get your license key.
			PSPDFKitGlobal.SetLicenseKey("YOUR_LICENSE_KEY_GOES_HERE");

			Window = new UIWindow(UIScreen.MainScreen.Bounds);

			// Apply the PSPDFKit blue
			Window.TintColor = UIColor.FromRGBA(0.110f, 0.529f, 0.757f, 1f);

			viewController = new DVCMenu();
			navController = new UINavigationController(viewController);
			Window.RootViewController = navController;

			Window.MakeKeyAndVisible();
			return true;
		}
	}
}

