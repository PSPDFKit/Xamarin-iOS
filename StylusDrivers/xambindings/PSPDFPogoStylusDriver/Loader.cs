using System;

namespace PSPDFKit.iOS.StylusSupport {
	public class PogoDriver {
		static PogoDriver () {}

		public static void ForceLoad () {}
	}
}

namespace ApiDefinition {
	partial class Messaging {

		static Messaging ()
		{
			PSPDFKit.iOS.StylusSupport.PogoDriver.ForceLoad ();
		}
	}
}

