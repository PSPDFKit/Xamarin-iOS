using System;

namespace PSPDFKit.iOS.StylusSupport {
	public class FiftyThreeDriver {
		static FiftyThreeDriver () {}

		public static void ForceLoad () {}
	}
}

namespace ApiDefinition {
	partial class Messaging {

		static Messaging ()
		{
			PSPDFKit.iOS.StylusSupport.FiftyThreeDriver.ForceLoad ();
		}
	}
}