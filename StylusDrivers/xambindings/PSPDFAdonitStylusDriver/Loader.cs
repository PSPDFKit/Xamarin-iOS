using System;

namespace PSPDFKit.iOS.StylusSupport {
	public class AdonitDriver {
		static AdonitDriver () {}

		public static void ForceLoad () {}
	}
}

namespace ApiDefinition {
	partial class Messaging {
		
		static Messaging ()
		{
			PSPDFKit.iOS.StylusSupport.AdonitDriver.ForceLoad ();
		}
	}
}

