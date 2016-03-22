using System;

namespace PSPDFKit.iOS.StylusSupport {
	public class WacomDriver {
		static WacomDriver () {}

		public static void ForceLoad () {}
	}
}

namespace ApiDefinition {
	partial class Messaging {

		static Messaging ()
		{
			PSPDFKit.iOS.StylusSupport.WacomDriver.ForceLoad ();
		}
	}
}

