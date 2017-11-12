using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;

namespace PSPDFKit.UI.StylusSupport {
	[BaseType (typeof (NSObject))]
	interface PSPDFFiftyThreeStylusDriver : IPSPDFStylusDriver {

		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFStylusDriverDelegate @delegate);

		[Static]
		[Export ("driverInfo")]
		NSDictionary DriverInfo { get; }
	}
}
