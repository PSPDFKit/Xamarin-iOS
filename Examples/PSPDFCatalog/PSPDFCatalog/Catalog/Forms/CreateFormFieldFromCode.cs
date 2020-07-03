using System;
using CoreGraphics;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog {
    public class CreateFormFieldFromCodeViewController : PSPDFViewController {
        public CreateFormFieldFromCodeViewController (PSPDFDocument document) : base (document)
        {
			PSPDFSignatureFormElement signatureFormElement = new PSPDFSignatureFormElement {
				BoundingBox = new CGRect (x: 200, y: 100, width: 200, height: 50),
				PageIndex = 0
			};

			PSPDFSignatureFormField signatureFormField = PSPDFSignatureFormField.Create ("Signature", document.DocumentProviders [0], signatureFormElement, out NSError error);

			if (signatureFormField == null) {
                Console.WriteLine (@"Error: %@", error.LocalizedDescription);
            }
        }
	}
}
