var target = Argument ("target", "Default");

Task ("MacCore")
	.Description ("Builds 'PSPDFKit.Mac.dll', expects 'PSPDFKit.framework' inside './PSPDFKit.Mac/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.Mac.dll ===");
		if (!DirectoryExists ("./PSPDFKit.Mac/PSPDFKit.framework/")) {
			Warning ("Unable to locate 'PSPDFKit.framework' inside './PSPDFKit.Mac' Directory");
			Warning ("Skipping PSPDFKit.Mac.dll");
		} else {
			MSBuild ("./PSPDFKit.Mac/PSPDFKit.Mac.csproj", new MSBuildSettings ()
				.SetConfiguration ("Release")
			);
			if (FileExists ("./PSPDFKit.Mac/bin/Release/PSPDFKit.Mac.dll"))
				CopyFile ("./PSPDFKit.Mac/bin/Release/PSPDFKit.Mac.dll", "./PSPDFKit.Mac.dll");
		}
	}
);

Task ("iOSCore")
	.Description ("Builds 'PSPDFKit.iOS.dll', expects 'PSPDFKit.framework' inside './PSPDFKit.iOS/' Directory\n")
	.Does (() => {
		Information ("=== PSPDFKit.iOS.dll ===");
		if (!DirectoryExists ("./PSPDFKit.iOS/PSPDFKit.framework/"))
			throw new Exception ("Unable to locate 'PSPDFKit.framework' inside './PSPDFKit.iOS' Directory");
		
		MSBuild ("./PSPDFKit.iOS/PSPDFKit.iOS.csproj", new MSBuildSettings ()
			.SetConfiguration ("Release")
		);
		if (FileExists ("./PSPDFKit.iOS/bin/Release/PSPDFKit.iOS.dll"))
			CopyFile ("./PSPDFKit.iOS/bin/Release/PSPDFKit.iOS.dll", "./PSPDFKit.iOS.dll");
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
	.IsDependentOn ("iOSCore")
	.IsDependentOn ("iOSUI")
	.IsDependentOn ("iOSInstant")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("mac")
	.Description ("Builds macOS PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("MacCore")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("Default")
	.Description ("Builds all PSPDFKit dlls.\n")
	.IsDependentOn ("Clean")
	.IsDependentOn ("RestoreNugets")
	.IsDependentOn ("iOSCore")
	.IsDependentOn ("iOSUI")
	.IsDependentOn ("iOSInstant")
	.IsDependentOn ("MacCore")
	.Does (() => {
		Information ("DONE! You will find the PSPDFKit.*.dll's in the root folder.");
	}
);

Task ("Clean")
	.Description ("Cleans the build.\n")
	.Does (() => {
		if (FileExists ("./PSPDFKit.iOS.dll"))
			DeleteFile ("./PSPDFKit.iOS.dll");

		if (FileExists ("./PSPDFKit.iOS.UI.dll"))
			DeleteFile ("./PSPDFKit.iOS.UI.dll");

		if (FileExists ("./PSPDFKit.iOS.Instant.dll"))
			DeleteFile ("./PSPDFKit.iOS.Instant.dll");

		if (FileExists ("./PSPDFKit.Mac.dll"))
			DeleteFile ("./PSPDFKit.Mac.dll");

		var delDirSettings = new DeleteDirectorySettings { Recursive = true, Force = true };

		if (DirectoryExists ("./PSPDFKit.iOS/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.UI/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS.UI/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.UI/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS.UI/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.Instant/bin/"))
			DeleteDirectory ("./PSPDFKit.iOS.Instant/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.iOS.Instant/obj/"))
			DeleteDirectory ("./PSPDFKit.iOS.Instant/obj", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.Mac/bin/"))
			DeleteDirectory ("./PSPDFKit.Mac/bin", delDirSettings);

		if (DirectoryExists ("./PSPDFKit.Mac/obj/"))
			DeleteDirectory ("./PSPDFKit.Mac/obj", delDirSettings);

		if (DirectoryExists ("./packages/"))
			DeleteDirectory ("./packages", delDirSettings);
	}
);

RunTarget (target);