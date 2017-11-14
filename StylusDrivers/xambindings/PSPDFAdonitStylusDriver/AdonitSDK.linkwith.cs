using ObjCRuntime;

[assembly: LinkWith ("AdonitSDK", LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Simulator | LinkTarget.Arm64, LinkerFlags = "-ObjC", Frameworks = "CoreBluetooth SystemConfiguration CoreMotion", SmartLink = false, ForceLoad = true)]
