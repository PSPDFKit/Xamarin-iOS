using System;

namespace PSPDFKit.iOS.StylusSupport {
	public class StylusDrivers {
		static StylusDrivers ()
		{
			// Uncomment the needed driver line once you add
			// the dll containing it to this binding project
			// also do not forget to add the same dll along
			// with PSPDFKit.iOS.dll to your app project
			//
			// Note: The stylus drivers are not available
			// in the evaluation demo. For more information visit
			// https://pspdfkit.com/guides/ios/current/features/stylus-support/

			//AdonitDriver.ForceLoad ();
			//FiftyThreeDriver.ForceLoad ();
			//PogoDriver.ForceLoad ();
			//WacomDriver.ForceLoad ();
		}

		public static void ForceLoad () {}
	}
}

namespace ApiDefinition {
	partial class Messaging {

		static Messaging ()
		{
			PSPDFKit.iOS.StylusSupport.StylusDrivers.ForceLoad ();
		}
	}
}