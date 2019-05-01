using System;
using System.IO;
using UIKit;
using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class PSCXFDFAnnotationProviderExample : PSPDFViewController {

		static readonly string xfdfFile = Path.Combine (Path.GetTempPath (), "XFDFTest.xfdf");
		public override void CommonInit (PSPDFDocument document, PSPDFConfiguration configuration)
		{
			base.CommonInit (document, configuration);

			// Check if XFDFTest.xfdf file already exists and if so leave it alone, else copy from bundle.
			// This file contains a text annotation in the first two pages.
			if (!File.Exists (xfdfFile)) {
				Console.WriteLine ($"XFDFTest.xfdf file does not exist, copying from bundle...");
				File.Copy (DVCMenu.AnnualReportXFDFFile, xfdfFile, true);
			}
			Console.WriteLine ($"Using XFDF file at {xfdfFile}");

			Document = new PSPDFDocument (NSUrl.FromFilename (DVCMenu.AnnualReportFile)) {
				// Set up the XFDF provider.
				AnnotationSaveMode = PSPDFAnnotationSaveMode.ExternalFile,
				DidCreateDocumentProviderHandler = (documentProvider) => {
					var XFDFProvider = new PSPDFXFDFAnnotationProvider (documentProvider, NSUrl.FromFilename (xfdfFile));

					// Note that if the document you're opening has form fields which you wish to be usable when using XFDF,
					// you should also add the file annotation provider to the annotation manager's 'AnnotationProviders' array:
					//
					//var fileProvider = documentProvider.AnnotationManager.FileAnnotationProvider;
					//documentProvider.AnnotationManager.AnnotationProviders = new IPSPDFAnnotationProvider [] { XFDFProvider, fileProvider };
					//

					documentProvider.AnnotationManager.AnnotationProviders = new IPSPDFAnnotationProvider [] { XFDFProvider };
				}
			};

			var saveButton = new UIBarButtonItem ("Save", UIBarButtonItemStyle.Plain, SaveHandler);
			NavigationItem.LeftBarButtonItems = new UIBarButtonItem [] { CloseButtonItem, saveButton };
		}

		async void SaveHandler (object sender, EventArgs e)
		{
			if (!Document.Valid)
				return;

			try {
				Console.WriteLine ($"Saving... (XFDF file size: {GetFileSize (xfdfFile)})");
				await Document.SaveAsync (options: null);
				Console.WriteLine ($"Saving done. (XFDF file size: {GetFileSize (xfdfFile)})");
				Console.WriteLine ($"XFDFTest.xfdf: {xfdfFile}");
			} catch (Exception ex) {
				Console.WriteLine ($"Saving failed: {ex.Message}");
			}

			long GetFileSize (string file) => new FileInfo (file).Length;
		}
	}
}
