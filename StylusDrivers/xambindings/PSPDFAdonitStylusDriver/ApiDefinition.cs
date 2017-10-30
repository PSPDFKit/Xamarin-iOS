using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using PSPDFKit.UI;

namespace PSPDFKit.iOS.StylusSupport {
	[BaseType (typeof (NSObject))]
	interface PSPDFAdonitStylusDriver : IPSPDFStylusDriver
	{
	}
}

