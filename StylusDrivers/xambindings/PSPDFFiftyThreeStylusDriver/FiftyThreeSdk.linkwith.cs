using ObjCRuntime;

[assembly: LinkWith ("FiftyThreeSdk", LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Simulator | LinkTarget.Arm64, IsCxx = true, LinkerFlags = "-ObjC -lc++", SmartLink = true, ForceLoad = true)]
