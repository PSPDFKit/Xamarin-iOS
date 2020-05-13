#addin nuget:?package=Cake.Git&version=0.21.0

var IOSVERSION = Argument("iosversion", "9.3.2");
var IOS_SERVICERELEASE_VERSION = "0"; // This is combined with the IOSVERSION variable for the NuGet Package version

var MACVERSION = Argument("macversion", "4.3.0");
var MACOS_SERVICERELEASE_VERSION = "0"; // This is combined with the MACVERSION variable for the NuGet Package version

var target = Argument ("target", "Default");
var NUGET_API_KEY = EnvironmentVariable("NUGET_API_KEY");

Task ("MacModel")
	.Description ("Builds 'PSPDFKit.Mac.Model.dll', expects 'PSPDFKit.framework' inside './PSPDFKit.Mac.Model/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.Mac.Model.dll ===");
		if (!DirectoryExists ("./PSPDFKit.Mac.Model/PSPDFKit.framework/")) {
			Warning ("Unable to locate 'PSPDFKit.framework' inside './PSPDFKit.Mac' Directory");
			Warning ("Skipping PSPDFKit.Mac.Model.dll");
		} else {
			MSBuild ("./PSPDFKit.Mac.Model/PSPDFKit.Mac.Model.csproj", new MSBuildSettings ()
				.SetConfiguration ("Release")
			);
			if (FileExists ("./PSPDFKit.Mac.Model/bin/Release/PSPDFKit.Mac.Model.dll"))
				CopyFile ("./PSPDFKit.Mac.Model/bin/Release/PSPDFKit.Mac.Model.dll", "./PSPDFKit.Mac.Model.dll");
		}
	}
);

Task ("iOSModel")
	.Description ("Builds 'PSPDFKit.iOS.Model.dll', expects 'PSPDFKit.framework' inside './PSPDFKit.iOS.Model/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.iOS.Model.dll ===");
		if (!DirectoryExists ("./PSPDFKit.iOS.Model/PSPDFKit.framework/"))
			throw new Exception ("Unable to locate 'PSPDFKit.framework' inside './PSPDFKit.iOS.Model' Directory");

		MSBuild ("./PSPDFKit.iOS.Model/PSPDFKit.iOS.Model.csproj", new MSBuildSettings ()
			.SetConfiguration ("Release")
		);
		if (FileExists ("./PSPDFKit.iOS.Model/bin/Release/PSPDFKit.iOS.Model.dll"))
			CopyFile ("./PSPDFKit.iOS.Model/bin/Release/PSPDFKit.iOS.Model.dll", "./PSPDFKit.iOS.Model.dll");
	}
);

Task ("iOSUI")
	.Description ("Builds 'PSPDFKit.iOS.UI.dll', expects 'PSPDFKitUI.framework' inside './PSPDFKit.iOS.UI/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.iOS.UI.dll ===");
		if (!DirectoryExists ("./PSPDFKit.iOS.UI/PSPDFKitUI.framework/")) {
			Warning ("Unable to locate 'PSPDFKitUI.framework' inside './PSPDFKit.iOS.UI' Directory");
			Warning ("Skipping PSPDFKit.iOS.UI.dll");
		} else {
			MSBuild ("./PSPDFKit.iOS.UI/PSPDFKit.iOS.UI.csproj", new MSBuildSettings ()
				.SetConfiguration ("Release")
			);
			if (FileExists ("./PSPDFKit.iOS.UI/bin/Release/PSPDFKit.iOS.UI.dll"))
				CopyFile ("./PSPDFKit.iOS.UI/bin/Release/PSPDFKit.iOS.UI.dll", "./PSPDFKit.iOS.UI.dll");
		}
	}
);

Task ("iOSInstant")
	.Description ("Builds 'PSPDFKit.iOS.Instant.dll', expects 'Instant.framework' inside './PSPDFKit.iOS.Instant/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.iOS.Instant.dll ===");
		if (!DirectoryExists ("./PSPDFKit.iOS.Instant/Instant.framework/")) {
			Warning ("Unable to locate 'Instant.framework' inside './PSPDFKit.iOS.Instant' Directory");
			Warning ("Skipping PSPDFKit.iOS.Instant.dll");
		} else {
			MSBuild ("./PSPDFKit.iOS.Instant/PSPDFKit.iOS.Instant.csproj", new MSBuildSettings ()
				.SetConfiguration ("Release")
			);
			if (FileExists ("./PSPDFKit.iOS.Instant/bin/Release/PSPDFKit.iOS.Instant.dll"))
				CopyFile ("./PSPDFKit.iOS.Instant/bin/Release/PSPDFKit.iOS.Instant.dll", "./PSPDFKit.iOS.Instant.dll");
		}
	}
);

Task ("RestoreNugets")
	.Does (() => {
		Information ("=== Restoring NuGets ===");
		NuGetRestore ("./PSPDFKit.sln");
});

Task ("ios")
	.Description ("Builds iOS PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("RestoreNugets")
	.IsDependentOn ("iOSModel")
	.IsDependentOn ("iOSUI")
	.IsDependentOn ("iOSInstant")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("mac")
	.Description ("Builds macOS PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("MacModel")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("Default")
	.Description ("Builds all PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("RestoreNugets")
	.IsDependentOn ("iOSModel")
	.IsDependentOn ("iOSUI")
	.IsDependentOn ("iOSInstant")
	.IsDependentOn ("MacModel")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("NuGet")
	.IsDependentOn("Default")
	.Does (() =>
{
	if(!DirectoryExists("./nuget/pkgs/"))
		CreateDirectory("./nuget/pkgs");

	var head = GitLogTip("./");
	var commit = head.Sha.Substring (0,7);

	NuGetPack ("./nuget/pspdfkit-ios-model.nuspec", new NuGetPackSettings {
		Version = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}+sha.{commit}",
		OutputDirectory = "./nuget/pkgs/",
		BasePath = "./",
		Properties = new Dictionary<string, string> {
			{"NoWarn", "NU5105"},
		}
	});

	NuGetPack ("./nuget/pspdfkit-ios-ui.nuspec", new NuGetPackSettings {
		Version = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}+sha.{commit}",
		OutputDirectory = "./nuget/pkgs/",
		BasePath = "./",
		Properties = new Dictionary<string, string> {
			{"NoWarn", "NU5105"},
		}
	});

	NuGetPack ("./nuget/pspdfkit-ios-instant.nuspec", new NuGetPackSettings {
		Version = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}+sha.{commit}",
		OutputDirectory = "./nuget/pkgs/",
		BasePath = "./",
		Properties = new Dictionary<string, string> {
			{"NoWarn", "NU5105"},
		}
	});

	// Only build if framework is present
	if (DirectoryExists ("./PSPDFKit.Mac.Model/PSPDFKit.framework/")) {
		NuGetPack ("./nuget/pspdfkit-mac-model.nuspec", new NuGetPackSettings {
			Version = $"{MACVERSION}.{MACOS_SERVICERELEASE_VERSION}+sha.{commit}",
			OutputDirectory = "./nuget/pkgs/",
			BasePath = "./",
			Properties = new Dictionary<string, string> {
				{"NoWarn", "NU5105"},
			}
		});
	}
});

Task ("NuGet-Push")
	.IsDependentOn("Nuget")
	.Does (() =>
{	
	var iOSFullVersion = IOSVERSION;
	var macOSFullVersion = IOSVERSION;

 	if (IOS_SERVICERELEASE_VERSION != "0") {
 		iOSFullVersion = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}";
 	}

	if (MACOS_SERVICERELEASE_VERSION != "0") {
 		macOSFullVersion = $"{MACVERSION}.{MACOS_SERVICERELEASE_VERSION}";
 	}
	
	// Get the path to the packages
	var modelPackage = $"./nuget/pkgs/PSPDFKit.iOS.Model.{iOSFullVersion}.nupkg";
 	var uiPackage = $"./nuget/pkgs/PSPDFKit.iOS.UI.{iOSFullVersion}.nupkg";
 	var instantPackage = $"./nuget/pkgs/PSPDFKit.iOS.Instant.{iOSFullVersion}.nupkg";
 	var macModelPackage = $"./nuget/pkgs/PSPDFKit.Mac.Model.{macOSFullVersion}.nupkg";

	// Push the packages
	NuGetPush(modelPackage, new NuGetPushSettings {
			Source = "https://api.nuget.org/v3/index.json",
			ApiKey = NUGET_API_KEY
	});

	NuGetPush(uiPackage, new NuGetPushSettings {
			Source = "https://api.nuget.org/v3/index.json",
			ApiKey = NUGET_API_KEY
	});

	NuGetPush(instantPackage, new NuGetPushSettings {
			Source = "https://api.nuget.org/v3/index.json",
			ApiKey = NUGET_API_KEY
	});

	// Only push if framework is present
	if (DirectoryExists ("./PSPDFKit.Mac.Model/PSPDFKit.framework/")) {
		NuGetPush(macModelPackage, new NuGetPushSettings {
				Source = "https://api.nuget.org/v3/index.json",
				ApiKey = NUGET_API_KEY
		});
	}
});

Task ("Clean")
	.Description ("Cleans the build.\n")
	.Does (() => {
		if (FileExists ("./PSPDFKit.iOS.Model.dll"))
			DeleteFile ("./PSPDFKit.iOS.Model.dll");

		if (FileExists ("./PSPDFKit.iOS.UI.dll"))
			DeleteFile ("./PSPDFKit.iOS.UI.dll");

		if (FileExists ("./PSPDFKit.iOS.Instant.dll"))
			DeleteFile ("./PSPDFKit.iOS.Instant.dll");

		if (FileExists ("./PSPDFKit.Mac.Model.dll"))
			DeleteFile ("./PSPDFKit.Mac.Model.dll");

		var delDirSettings = new DeleteDirectorySettings { Recursive = true, Force = true };

		if (DirectoryExists ("./PSPDFKit.iOS.Model/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS.Model/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.Model/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS.Model/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.UI/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS.UI/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.UI/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS.UI/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.Instant/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS.Instant/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.Instant/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS.Instant/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.Mac.Model/bin/"))
			DeleteDirectory ("./PSPDFKit.Mac.Model/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.Mac.Model/obj/"))
			DeleteDirectory ("./PSPDFKit.Mac.Model/obj", delDirSettings);

		if (DirectoryExists ("./packages/"))
			DeleteDirectory ("./packages", delDirSettings);

		if(DirectoryExists("./nuget/pkgs/"))
			DeleteDirectory ("./nuget/pkgs", delDirSettings);
	}
);

RunTarget (target);
