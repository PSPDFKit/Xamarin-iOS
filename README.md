Xamarin PSPDFKit for iOS and Mac Bindings
=========================================

- Xamarin.iOS Bindings for PSPDFKit 7.0.3 for iOS
- Xamarin.Mac Bindings for PSPDFKit 2.0.1 for macOS

Xamarin.Android Bindings for PSPDFKit for Android: [PSPDFKit/Xamarin-Android](https://github.com/PSPDFKit/Xamarin-Android)

Minimum Requirements
====================

In order to build this binding project you need:

- **Visual Studio for Mac or Windows**
- **Xamarin.iOS 11.0 +**
- **Xamarin.Mac 4.0 +**

Build Instructions
==================

## Step 1 - Get the bindings

1. Clone this repository to your computer.
2. Open the `Xamarin-iOS` directory.

## Step 2 - Copy required files

1. Download PSPDFKit for iOS and/or Mac from your [customer portal](https://customers.pspdfkit.com/) if you haven't done so already.
2. Open the dmg file you downloaded in step 1 and copy the following files into the specified directories:

iOS: From **PSPDFKit-for-iOS** dmg.

- Copy `PSPDFKit.framework` into [PSPDFKit.iOS](PSPDFKit.iOS/) folder.
- Copy `PSPDFKitUI.framework` into [PSPDFKit.iOS.UI](PSPDFKit.iOS.UI/) folder. *
- Copy `Instant.framework` into [PSPDFKit.iOS.Instant](PSPDFKit.iOS.Instant/) folder. *

Mac: From **PSPDFKit-for-macOS** dmg.

- Copy `PSPDFKit.framework` into [PSPDFKit.Mac](PSPDFKit.Mac/) folder. *

`*` Items with an asterisk are *optional*, if you do not provide a framework, by default we won't generate its bindings, but most of the time you will want to provide `PSPDFKit.framework` and `PSPDFKitUI.framework` at minimum.

## Step 3 - Get your dlls

### Using Visual Studio for Mac or Windows

1. Open `PSPDFKit.sln` located in the root folder.
2. Build the binding projects inside the `iOS` or `Mac` solution folders.
3. Get the dlls from the `bin` folder of each project.
4. Go to **Step 4 - Integrating into your ptoject**.

### Using command line / terminal

We are using [Cake](https://cakebuild.net) as our build system, this allows us to build on both Windows and Mac from a single script.

1. Run `./build.sh` (Mac) / `.\build.ps1` (Windows) command from the root directory.
2. All the resulting dlls will be inside the root folder.
3. Go to **Step 4 - Integrating into your ptoject**.

#### Advanced build

We use the `Default` build task which builds all binding projects as long as the frameworks are present inside each directory (see **Step 2 - Copy required files**), if an optional framework is not present this Task will just skip.

In the case you just want the Mac bits to be built you can do `./build.sh --target mac`. Here are the most common tasks available in the build script, they are particularly useful when you are integrating this into a CI Server.

|            | General Tasks               |
|:----------:|-----------------------------|
|   **Task** | **Description**             |
|    Default | Builds all PSPDFKit dlls.   |
|        ios | Builds iOS PSPDFKit dlls.   |
|        mac | Builds macOS PSPDFKit dlls. |
|      Clean | Cleans the build.           |

To list all available tasks you can do `./build.sh --showdescription`.

## Step 4 - Integrating into your project

Add the generated PSPDFKit dlls as a reference to into your own Xamarin project and add the corresponding using statements depending on the dlls referenced into your project.

```csharp
using PSPDFKit.Core;
using PSPDFKit.UI;
using PSPDFKit.Instant;
```

Also you do need to set your **license key** early on in your `AppDelegate`, before accessing any other PSPDFKit classes.

```csharp
public override bool FinishedLaunching (UIApplication application, NSDictionary launchOptions)
{
	PSPDFKitGlobal.SetLicenseKey ("YOUR_LICENSE_KEY_GOES_HERE");
	// ...
}
```

# Examples

You can find a sample project by opening `PSPDFKit.sln` and find it inside `iOS/Examples` solution folder. A Mac sample is comming soon.

# Stylus Support

You can find instructions on how to add stylus support [here](StylusDrivers/).


# Contributing
  
Please ensure [you signed our CLA](https://pspdfkit.com/guides/web/current/miscellaneous/contributing/) so we can accept your contributions.
