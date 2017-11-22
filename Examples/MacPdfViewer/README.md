Xamarin PSPDFKit for Mac Example
=========================================

- Xamarin.Mac Bindings for PSPDFKit 2.1.0 for macOS

Minimum Requirements
====================

In order to build this binding project you need:

- **Visual Studio for Mac or Windows**
- **Xamarin.Mac 4.0 +**

Build Instructions
==================

## Step 1 - Get the bindings

1. Clone this repository to your computer.
2. Open the `Xamarin-iOS` directory.

## Step 2 - Copy required files

1. Download PSPDFKit for Mac from your [customer portal](https://customers.pspdfkit.com/) if you haven't done so already.
2. Open the dmg file you downloaded in step 1 and copy the following files into the specified directories:

- Copy `PSPDFKit.framework` into [PSPDFKit.Mac](PSPDFKit.Mac/) folder.

## Step 3 - Get your dll

### Using Visual Studio for Mac or Windows

1. Open `PSPDFKit.sln` located in the root folder.
2. Build the `macOS` solution.
3. Get the dll from the `PSPDFKit.Mac/bin` folder.

### Using command line / terminal

We are using [Cake](https://cakebuild.net) as our build system, this allows us to build on both Windows and Mac from a single script.

1. Run `./build.sh --target mac` (Mac) / `.\build.ps1 --target mac` (Windows) command from the root directory.
2. The resulting dll will be inside the root folder.

## Step 4 - Add the reference

1. Open the `MacPDFViewer` project inside `macOS/Examples`. Right-click on `References` and then `Edit References`.
2. Now add the dll from the `PSPDFKit.Mac/bin` folder.

## Step 5 - Run MacPDFViewer

Build the `MacPDFViewer` project. The result should look like this:

<img width="50%" src="Images/macOS.png"/>
