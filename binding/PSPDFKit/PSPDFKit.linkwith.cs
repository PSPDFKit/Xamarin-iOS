using System;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
#endif

[assembly: LinkerSafe]
[assembly: LinkWith ("PSPDFKit", LinkTarget.ArmV7 | LinkTarget.Simulator | LinkTarget.Arm64 | LinkTarget.Simulator64, LinkerFlags = "-lc++ -lz -lxml2 -lsqlite3 -ObjC -fobjc-arc", Frameworks = "CoreText QuartzCore MessageUI ImageIO CoreMedia MediaPlayer CFNetwork AVFoundation AssetsLibrary Security QuickLook AudioToolbox CoreData SystemConfiguration CoreTelephony", SmartLink = true, ForceLoad = true)]
