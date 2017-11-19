using System;
using Foundation;
using UIKit;

using PSPDFKit.Core;
using PSPDFKit.UI;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using XFSample;
using XFSample.iOS;

[assembly: ExportRenderer (typeof (ModalPDFViewer), typeof (ModalPDFViewerRenderer))]
namespace XFSample.iOS {
	public class ModalPDFViewerRenderer : PageRenderer {

		public static readonly string PdfFilePath = @"pdf/PSPDFKit QuickStart Guide.pdf";

		protected override void OnElementChanged (VisualElementChangedEventArgs e)
		{
			base.OnElementChanged (e);

			if (e.OldElement != null || Element == null)
				return;

			var containerController = ViewController;
			var document = new PSPDFDocument (NSUrl.FromFilename (PdfFilePath));
			var pdfController = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
				builder.UseParentNavigationBar = true;
				builder.ShouldHideStatusBarWithUserInterface = true;
			}));

			var navController = new UINavigationController (pdfController);
			// just style the controller a little
			SetNiceColors (navController);

			// Since we are using Xamarin Forms navigation we need to pop out using it, so we are using `Element` to tell X.F that we are done and pop us out.
			var menuItem = new UIBarButtonItem (UIBarButtonSystemItem.Done, (sender, ev) => {
				// You can add some logic here before the controller is closed
				Console.WriteLine ("Done with the document.");

				// Let PDFViewer object know that we want to pop
				Element.Navigation.PopModalAsync ();
			});
			pdfController.NavigationItem.LeftBarButtonItem = menuItem;

			containerController.AddChildViewController (navController);
			navController.View.Frame = containerController.View.Bounds; // make the controller fullscreen in your container controller
			navController.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight; // ensure the controller resizes along with your container controller
			containerController.View.AddSubview (navController.View);

			pdfController.DidMoveToParentViewController (navController);
		}

		void SetNiceColors (UINavigationController navController)
		{
			var barColor = UIColor.FromRGBA (0.110f, 0.529f, 0.757f, 1f);
			navController.NavigationBar.BarTintColor = barColor;
			navController.Toolbar.TintColor = barColor;
			navController.View.TintColor = UIColor.White;
			navController.NavigationBar.TitleTextAttributes = new UIStringAttributes { ForegroundColor = UIColor.White };
			navController.NavigationBar.BarStyle = UIBarStyle.Black;
		}
	}
}
