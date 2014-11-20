using System;
using System.Linq;
using System.Collections.Generic;

using Foundation;
using UIKit;

using PSPDFKit;

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

		public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
		{
			Window = new UIWindow (UIScreen.MainScreen.Bounds);

			// Set your license key here. PSPDFKit is commercial software.
			// Each PSPDFKit license is bound to a specific app bundle id.
			// Visit http://customers.pspdfkit.com to get your demo or commercial license key.
			PSPDFKitGlobal.SetLicenseKey ("YOUR_LICENSE_KEY_GOES_HERE");

			viewController = new DVCMenu ();
			navController = new UINavigationController (viewController);
			Window.RootViewController = navController;

			Window.MakeKeyAndVisible ();
			return true;
		}
		
		// This method is invoked when the application is about to move from active to inactive state.
		// OpenGL applications should use this method to pause.
		public override void OnResignActivation (UIApplication application)
		{
		}
		
		// This method should be used to release shared resources and it should store the application state.
		// If your application supports background exection this method is called instead of WillTerminate
		// when the user quits.
		public override void DidEnterBackground (UIApplication application)
		{
		}
		
		// This method is called as part of the transiton from background to active state.
		public override void WillEnterForeground (UIApplication application)
		{
		}
		
		// This method is called when the application is about to terminate. Save data, if needed.
		public override void WillTerminate (UIApplication application)
		{
		}
	}
}

