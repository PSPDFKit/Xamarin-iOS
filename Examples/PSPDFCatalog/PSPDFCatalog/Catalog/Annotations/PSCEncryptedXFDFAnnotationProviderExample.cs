using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using UIKit;
using Foundation;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class PSCEncryptedXFDFAnnotationProviderExample : PSPDFViewController {

		static readonly string xfdfFile = Path.Combine (Path.GetTempPath (), "XFDFTest-encrypted.xfdf");

		// You should do some magic to get this instead of harcoding it.
		static readonly PSPDFAESCryptoPassphraseProvider passphraseProvider = () => "jJ9A3BiMXoq+rEoYMdqBoBNzgxagTf";

		public override void CommonInit (PSPDFDocument document, PSPDFConfiguration configuration)
		{
			base.CommonInit (document, configuration);

			// Create an example XFDF from the current document if one doesn't already exist.
			if (!File.Exists (xfdfFile)) {
				Console.WriteLine ($"XFDFTest-encrypted.xfdf file does not exist, creating...");
				var tempDocument = new PSPDFDocument (NSUrl.FromFilename (DVCMenu.AnnualReportFile));
				var annotations = new List<PSPDFAnnotation> ();
				foreach (var pageAnnots in tempDocument.GetAllAnnotations (PSPDFAnnotationType.All).Values)
					annotations.AddRange (pageAnnots);

				// Write the file
				var dataSink = new PSPDFAESCryptoDataSink (NSUrl.FromFilename (xfdfFile), passphraseProvider, PSPDFDataSinkOptions.None);
				if (dataSink != null) {
					var writer = new PSPDFXFDFWriter ();
					writer.WriteAnnotations (annotations.ToArray (), dataSink, tempDocument.DocumentProviders [0], out var error);
					if (error != null)
						Console.WriteLine ($"Failed to write XFDF file: {error}");
					Console.WriteLine ($"Using XFDF file at {xfdfFile}");
				} else
					Console.WriteLine ($"Failed to open data sink.");
			} else
				Console.WriteLine ($"Using XFDF file at {xfdfFile}");

			var cryptoDataProvider = new PSPDFAESCryptoDataProvider (NSUrl.FromFilename (xfdfFile), passphraseProvider);
			if (cryptoDataProvider == null) {
				Console.WriteLine ("Error creating crypto data provider.");
				return;
			}

			Document = new PSPDFDocument (NSUrl.FromFilename (DVCMenu.AnnualReportFile)) {
				// Set up the XFDF provider.
				AnnotationSaveMode = PSPDFAnnotationSaveMode.ExternalFile,
				DidCreateDocumentProviderHandler = (documentProvider) => {
					var XFDFProvider = new PSPDFXFDFAnnotationProvider (documentProvider, cryptoDataProvider);

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
				Console.WriteLine ($"XFDFTest-encrypted.xfdf: {xfdfFile}");
			} catch (Exception ex) {
				Console.WriteLine ($"Saving failed: {ex.Message}");
			}

			long GetFileSize (string file) => new FileInfo (file).Length;
		}
	}
}
