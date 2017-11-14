using ObjCRuntime;

[assembly: LinkWith ("libPSPDFAdonitStylusDriver.a", LinkTarget.ArmV7 | LinkTarget.Simulator64 | LinkTarget.Simulator | LinkTarget.Arm64, SmartLink = false, ForceLoad = true)]
