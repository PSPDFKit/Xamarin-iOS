using System;
using UIKit;
using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;
using MessageUI;

namespace PSPDFCatalog {
	public class CustomSharingFileNamesExampleViewController : PSPDFViewController, IPSPDFViewControllerDelegate, IPSPDFDocumentSharingViewControllerDelegate {
		public CustomSharingFileNamesExampleViewController (PSPDFDocument document, PSPDFConfiguration config) : base (document, config)
		{
		}

		public override void CommonInit (PSPDFDocument document, PSPDFConfiguration configuration)
		{
			base.CommonInit (document, configuration);
			Delegate = this;
			NavigationItem.RightBarButtonItems = new [] { ActivityButtonItem, EmailButtonItem };
		}

		[Export ("pdfViewController:didShowController:options:animated:")]
		public void DidShowController (PSPDFViewController pdfController, UIViewController controller, NSDictionary options, bool animated)
		{
			if (controller is PSPDFDocumentSharingViewController sharingController)
				sharingController.Delegate = this;
		}

		public override string GetFileNameForGeneratedFile (PSPDFDocumentSharingViewController shareController, PSPDFDocument sharingDocument, NSString destination)
		{
			return "MyCustomName";
		}
	}

	public class MyCustomDocumentSharingViewController : PSPDFDocumentSharingViewController {

		public MyCustomDocumentSharingViewController (IntPtr handle) : base (handle)
		{
			// No code here ever
		}

		[Export ("initWithDocuments:")]
		public MyCustomDocumentSharingViewController (PSPDFDocument [] documents) : base (documents)
		{
			// You can customize initialization code here if needed.
			foreach (var doc in documents)
				Console.WriteLine ($"Document to share: {doc.FileName}");
		}

		public override void ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController)
		{
			base.ConfigureMailComposeViewController (mailComposeViewController);
			mailComposeViewController.SetSubject ("Here is my awesome PDF file.");
		}
	}
}
