using ObjCRuntime;

[assembly: LinkWith ("AdonitSDK", LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Simulator | LinkTarget.Arm64, SmartLink = true, ForceLoad = true)]
