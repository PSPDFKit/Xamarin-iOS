Xamarin PSPDFKit for iOS and Mac Bindings
=========================================

- Xamarin.iOS Bindings for PSPDFKit 8.3.2 for iOS
- Xamarin.Mac Bindings for PSPDFKit 3.3.0 for macOS

Xamarin.Android Bindings for PSPDFKit for Android: [PSPDFKit/Xamarin-Android](https://github.com/PSPDFKit/Xamarin-Android)

#### PSPDFKit

The [PSPDFKit SDK](https://pspdfkit.com/) is a framework that allows you to view, annotate, sign, and fill PDF forms on iOS, Android, Windows, macOS, and Web.

[PSPDFKit Instant](https://pspdfkit.com/instant) adds real-time collaboration features to seamlessly share, edit, and annotate PDF documents.

Minimum Requirements
====================

In order to build this binding project you need:

- **Visual Studio for Mac or Windows**
- **Xamarin.iOS 11.12 (d15-7) +**
- **Xamarin.Mac 4.4 (d15-7) +**

Build Instructions
==================

## Step 1 - Get the bindings

1. Clone this repository to your computer.
2. Open the `Xamarin-iOS` directory.

## Step 2 - Copy required files

1. Download PSPDFKit for iOS and/or Mac from your [customer portal](https://customers.pspdfkit.com/) if you haven't done so already, or [request an evaluation version](https://pspdfkit.com/#trynow).
2. Open the dmg file you downloaded in step 1 and copy the following files into the specified directories:

iOS: From **PSPDFKit-for-iOS** dmg.

- Copy `PSPDFKit.framework` into [PSPDFKit.iOS.Model](PSPDFKit.iOS.Model/) folder.
- Copy `PSPDFKitUI.framework` into [PSPDFKit.iOS.UI](PSPDFKit.iOS.UI/) folder. *
- Copy `Instant.framework` into [PSPDFKit.iOS.Instant](PSPDFKit.iOS.Instant/) folder. *

Mac: From **PSPDFKit-for-macOS** dmg.

- Copy `PSPDFKit.framework` into [PSPDFKit.Mac.Model](PSPDFKit.Mac.Model/) folder. *

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
using PSPDFKit.Model;
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

# PSPDFKit Instant

Support for Instant was added with the Xamarin.iOS Bindings for PSPDFKit 7.2.0 for iOS.

With PSPDFKit Instant, it’s easier than ever to add real-time collaboration features to your PSPDFKit-powered app, allowing your users to seamlessly share, edit, and annotate PDF documents across iOS, Android, and web. With just a few lines of code, PSPDFKit Instant gives your users a massive productivity boost.

For more information about Instant, please have a look at our [website](https://pspdfkit.com/instant/).

# Examples

You can find several sample projects in the `Examples` folder, including a catalog, Xamarin.Forms, and macOS example.

## How to Run the Example Projects

1. Complete **Step 2**.
2. Open the `PSPDFKit.sln` solution in Visual Studio.
3. Select the example project and device you want to run it on (alternatively you can also right-click on the project and select "Build `Project Name`").
<img width="60%" src="Images/Project-setup.png"/>
4. Tap on the triangle on the left to run the project.

### PSPDFKit Instant Example

This example is included in the PSPDFCatalog example, but you can also find the code [here][Instant Example].

The PSPDFKit Instant example shows how easy and efficient Instant works. Just go the [Instant demo page](https://pspdfkit.com/instant/demo/) and tap on `Instant on iOS`, this will show a code at step three, which you have to enter in the example on your device. Afterwards you'll be connected to the server and you can start testing!

<div id="image-table">
     <table>
  	    <tr>
      	    <td>
             <img width="80%" src="Images/Instant-device.PNG"/>
           </td>
           <td>
             <img width="80%" src="Images/Instant-desktop.PNG"/>
           </td>
       </tr>
    </table>
</div>

### PSPDFCatalog

The `PSPDFCatalog` project includes various examples for pretty much every use-case and will help you to build your own app with PSPDFKit.

<img width="80%" src="Images/Catalog.png"/>

### Xamarin.Forms

The `XFSample.iOS` project is an example showcasing how to build an app with PSPDFKit using Xamarin.Forms.
<img width="80%" src="Images/XForms.png"/>

### MacPDFViewer

`MacPDFViewer` is an easy example on how build a PDF viewer on Mac.

<img width="80%" src="Images/macOS.png"/>


# Stylus Support

You can find instructions on how to add stylus support [here](StylusDrivers/).


# Contributing

Please ensure [you signed our CLA](https://pspdfkit.com/guides/web/current/miscellaneous/contributing/) so we can accept your contributions.


[Instant Example]: https://github.com/PSPDFKit/Xamarin-iOS/tree/master/Examples/PSPDFCatalog/PSPDFCatalog/Catalog/Instant
