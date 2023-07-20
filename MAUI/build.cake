#addin nuget:?package=Cake.Git&version=2.0.0 // Intentional 2.0.0 usage, 3.0.0 is broken.
#addin nuget:?package=Newtonsoft.Json&version=13.0.2

using System.Net.Http;
using System.Linq;
using Newtonsoft.Json.Linq;

var IOSVERSION = Argument("iosversion", "12.3.1");
var IOS_SERVICERELEASE_VERSION = "0"; // This is combined with the IOSVERSION variable for the NuGet Package version

var target = Argument ("target", "Default");
var NUGET_API_KEY = EnvironmentVariable("NUGET_API_KEY");

Task ("DownloadDeps")
	.Description ("Downloads frameworks")
	.Does (async () => {
		Information ("Downloading all the dependencies please wait...");

		if (!IsRunningOnUnix ()) {
			Error ("*** This can only be run in macOS. ***");
			return;
		}

		var iosUrl = $"https://customers.pspdfkit.com/pspdfkit-ios/{IOSVERSION}.podspec.json";
		var instantUrl = $"https://customers.pspdfkit.com/instant/{IOSVERSION}.podspec.json";

		var iosDlUrl = await ResolveDownloadUrl (iosUrl);
		var instantDlUrl = await ResolveDownloadUrl (instantUrl);

		CreateDirectory("./cache");
		DownloadFile (iosDlUrl, "./cache/ios.zip");
		DownloadFile (instantDlUrl, "./cache/instant.zip");

		UnzipFile ("./cache/ios.zip", "./cache/ios");
		UnzipFile ("./cache/instant.zip", "./cache/ios");

		CopyDir ("./cache/ios/PSPDFKit.xcframework", "./PSPDFKit.MAUI.iOS.Model/PSPDFKit.xcframework");
		CopyDir ("./cache/ios/PSPDFKitUI.xcframework", "./PSPDFKit.MAUI.iOS.UI/PSPDFKitUI.xcframework");
		CopyDir ("./cache/ios/Instant.xcframework", "./PSPDFKit.MAUI.iOS.Instant/Instant.xcframework");
	}
);

Task ("Build")
	.Description ("Builds PSPDFKit.MAUI.*.*.dll', expects each of the xcframeworks to be available inside each of the './PSPDFKit.MAUI.iOS.*/' Directories\n")
	.Does (() => {
		var iOSFullVersion = IOS_SERVICERELEASE_VERSION == "0" ? IOSVERSION : $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}";

		(string p, string xc) [] frameworks = {
			("PSPDFKit.MAUI.iOS.Model", "PSPDFKit.xcframework"),
			("PSPDFKit.MAUI.iOS.UI", "PSPDFKitUI.xcframework"),
			("PSPDFKit.MAUI.iOS.Instant", "Instant.xcframework"),
		};

		foreach (var f in frameworks) {
			if (!DirectoryExists ($"{f.p}/{f.xc}/"))
				throw new Exception ($"Unable to locate '{f.xc}' inside './{f.p}' Directory");
		}

		var msBuildSettings = new DotNetMSBuildSettings ()
			.WithProperty ("AssemblyVersion", iOSFullVersion);

		var dotNetBuildSettings = new DotNetBuildSettings { 
			Configuration = "Release",
//			Verbosity = DotNetVerbosity.Diagnostic,
			MSBuildSettings = msBuildSettings
		};
		DotNetBuild ("./PSPDFKit.MAUI.sln", dotNetBuildSettings);
	});

Task ("Default")
	.Description ("Builds all PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("DownloadDeps")
	.IsDependentOn ("Build")
	.Does (() => {
		Information (@"DONE! You will find the PSPDFKit.*.dll's inside the 'bin\Release' directory of each project folder.");
	}
);

Task ("NuGet")
	.IsDependentOn ("Default")
	.Does (() =>
{
	if(!DirectoryExists ("./nuget/pkgs/"))
		CreateDirectory ("./nuget/pkgs");

	var head = GitLogTip ("../");
	var commit = head.Sha.Substring (0,7);

	XmlPoke ("Directory.Build.props", "/Project/PropertyGroup/PSVersion", $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}+sha.{commit}");

	var dotNetPackSettings = new DotNetPackSettings {
		Configuration = "Release",
		NoRestore = true,
		NoBuild = true,
		OutputDirectory = "./nuget/pkgs",
		//Verbosity = DotNeVerbosity.Diagnostic,
	};

	DotNetPack($"./PSPDFKit.MAUI.sln", dotNetPackSettings);
});

Task ("NuGet-Push")
	.Does (() =>
{	
	var iOSFullVersion = IOSVERSION;

 	if (IOS_SERVICERELEASE_VERSION != "0") {
 		iOSFullVersion = $"{IOSVERSION}.{IOS_SERVICERELEASE_VERSION}";
 	}
	
	// Get the path to the packages
	var nugetPkgs = new [] {
		$"./nuget/pkgs/PSPDFKit.MAUI.iOS.Model.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.MAUI.iOS.UI.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.MAUI.iOS.Instant.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.MAUI.MacCatalyst.Model.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.MAUI.MacCatalyst.UI.{iOSFullVersion}.nupkg",
		$"./nuget/pkgs/PSPDFKit.MAUI.MacCatalyst.Instant.{iOSFullVersion}.nupkg",
	};

	foreach (var pkg in nugetPkgs) {
		Console.WriteLine ($"Pushing: {pkg}.");
		NuGetPush (pkg, new NuGetPushSettings {
			Source = "https://api.nuget.org/v3/index.json",
			ApiKey = NUGET_API_KEY
		});
	}
});

Task ("Clean")
	.Description ("Cleans the build.\n")
	.Does (() => {
		var nukeFiles = new [] {
			"./PSPDFKit.MAUI.iOS.Model.dll",
			"./PSPDFKit.MAUI.iOS.UI.dll",
			"./PSPDFKit.MAUI.iOS.Instant.dll",
			"./PSPDFKit.MAUI.MacCatalyst.Model.dll",
			"./PSPDFKit.MAUI.MacCatalyst.UI.dll",
			"./PSPDFKit.MAUI.MacCatalyst.Instant.dll",
		};

		foreach (var file in nukeFiles) {
			Console.WriteLine (file);
			if (FileExists ($"{file}"))
				Nuke ($"{file}");
		}

		var projdirs = new [] {
			"./PSPDFKit.MAUI.iOS.Model",
			"./PSPDFKit.MAUI.iOS.UI",
			"./PSPDFKit.MAUI.iOS.Instant",
			"./PSPDFKit.MAUI.MacCatalyst.Model",
			"./PSPDFKit.MAUI.MacCatalyst.UI",
			"./PSPDFKit.MAUI.MacCatalyst.Instant",
			"./Samples/DotNetiOSSample",
			"./Samples/DotNetMacCatalystSample",
		};

		foreach (var proj in projdirs) {
			Console.WriteLine (proj);
			if (DirectoryExists ($"{proj}/bin/"))
				Nuke ($"{proj}/bin");

			if (DirectoryExists ($"{proj}/obj/"))
				Nuke ($"{proj}/obj");
		}

		var nukedirs = new [] {
			"./tools",
			"./packages",
			"./nuget/pkgs",
			"./cache/ios",
			"./cache",
			"./PSPDFKit.MAUI.iOS.Model/PSPDFKit.xcframework",
			"./PSPDFKit.MAUI.iOS.UI/PSPDFKitUI.xcframework",
			"./PSPDFKit.MAUI.iOS.Instant/Instant.xcframework",
		};

		foreach (var dir in nukedirs) {
			Console.WriteLine (dir);
			if (DirectoryExists ($"{dir}/"))
				Nuke ($"{dir}");
		}
	}
);

static HttpClient client = new HttpClient ();
static async Task<string> ResolveDownloadUrl (string url)
{
	var json = await client.GetStringAsync (url);
	var jobj = JObject.Parse (json);
	return (string) jobj["source"]["http"];
}

void LipoCreate (FilePath binaryPath, params FilePath [] thinBinaries)
{
	var args = string.Join (" ", thinBinaries.Select (i => $"\"{i}\""));
	StartProcess ("lipo", new ProcessSettings {
		Arguments = $"-create -output \"{binaryPath}\" {args}"
	});
}

void CopyDir (FilePath origin, FilePath destination)
{
	StartProcess ("cp", new ProcessSettings {
		Arguments = $"-RP \"{origin}\" \"{destination}\""
	});
}

void UnzipFile (FilePath zipFile, DirectoryPath destination)
{
	StartProcess ("unzip", new ProcessSettings {
		Arguments = $"\"{zipFile}\" -d \"{destination}\""
	});
}

void Nuke (string path)
{
	StartProcess ("rm", new ProcessSettings {
		Arguments = $"-rf \"{path}\""
	});
}

RunTarget (target);
