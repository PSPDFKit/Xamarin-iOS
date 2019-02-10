using System;
using System.Linq;
using Foundation;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;

using XFSample;
using XFSample.iOS;

using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer (typeof (PDFViewer), typeof (PDFViewerRenderer))]
namespace XFSample.iOS {
	public class PDFViewerRenderer : PageRenderer {

		public static readonly string PdfFilePath = @"pdf/PSPDFKit QuickStart Guide.pdf";

		UIViewController pdfController;

		public override void DidMoveToParentViewController (UIViewController parent)
		{
			if (pdfController == null) {
				var containerController = parent;
				var document = new PSPDFDocument (NSUrl.FromFilename (PdfFilePath));
				pdfController = new PSPDFViewController (document, PSPDFConfiguration.FromConfigurationBuilder ((builder) => {
					builder.UseParentNavigationBar = true;
					builder.ShouldHideStatusBarWithUserInterface = true;
				}));

				containerController.AddChildViewController (pdfController);
				pdfController.View.Frame = containerController.View.Bounds; // make the controller fullscreen in your container controller
				pdfController.View.AutoresizingMask = UIViewAutoresizing.FlexibleWidth | UIViewAutoresizing.FlexibleHeight; // ensure the controller resizes along with your container controller
				containerController.View.AddSubview (pdfController.View);
				return;
			}

			base.DidMoveToParentViewController (parent);
		}
	}
}
