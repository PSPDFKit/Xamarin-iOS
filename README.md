Xamarin PSPDFKit v5 Bindings
============================

Xamarin.iOS Bindings for PSPDFKit `v5.0.3`

Building PSPDFKit.iOS.dll
=========================

**Note:** You must have at least *Xamarin.iOS 9.2.1* to _compile_ this binding project, 
also your project minimum version must be 8 or later since PSPDFKit 5 is now a dynamic framework

In order to use the bindings please follow these instructions:

## Step 1 - Get PSPDFKit.framework

1. Download PSPDFKit from your [customer portal](https://customers.pspdfkit.com) if you haven't done so already.
2. Open the dmg file you downloaded in step 1 and copy `PSPDFKit.framework` to [`PSPDFKit.iOS`](PSPDFKit.iOS) folder.

This binding provides **[Unified Api](http://developer.xamarin.com/guides/cross-platform/macios/unified/)** support out of the box.

## Step 2 - Get your Dll

You have two options to get it:

###Build from PSPDFKit.iOS.sln

1. Open `PSPDFKit.iOS.sln` on `Xamarin Studio` or `Visual Studio`.
2. Build the project.
3. Get the dll from the `bin` folder.
4. Add a reference of `PSPDFKit.iOS.dll` to your project.
5. Add the namespace `using PSPDFKit.iOS;`
6. Enjoy 

###Build from terminal

1. Just run `make` command from `root` or `Xamarin.iOS` directory
2. Get the dll from the `Dll` folder
4. Add a reference of `PSPDFKit.iOS.dll` to your project.
5. Add the namespace `using PSPDFKit.iOS;`
6. Enjoy 

### Set the License Key

Call `PSPDFKitGlobal.SetLicenseKey("YOUR_LICENSE_KEY_GOES_HERE");` early on in your AppDelegate, before accessing any other PSPDFKit classes.