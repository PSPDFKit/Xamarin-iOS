Xamarin PSPDFKit v4 Bindings
============================

Xamarin.iOS Bindings for PSPDFKit `v4.1.0`

Building PSPDFKit.dll
=====================

**Note:** You must have at least *Xamarin.iOS 8.4.0* to _compile_ this binding project.

This binding provides **[Unified Api](http://developer.xamarin.com/guides/cross-platform/macios/unified/)** and **[Classic Api](http://developer.xamarin.com/guides/cross-platform/macios/)** out of the box. Choose the one you need but we really encourage you to migrate your project to **Unified Api** since [Apple will enforce 64 bits apps soon](http://developer.apple.com/news/?id=10202014a).

You have two options to get it:

###Build from PSPDFKit.sln

1. Open `PSPDFKit.sln` on `Xamarin Studio` or `Visual Studio`
2. Build the project (**Unified** or **Classic**)
3. Get the dll from the `Dll` folder
4. Enjoy 

###Build from terminal

1. Just run `make` command from `root` or `binding` directory
2. Get the dll from the `binding` folder
3. Enjoy

