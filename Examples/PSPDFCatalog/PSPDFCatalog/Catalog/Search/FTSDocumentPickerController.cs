using System;
using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog
{
    public class FTSDocumentPickerController : PSPDFDocumentPickerController, IPSPDFDocumentPickerControllerDelegate
    {
        public FTSDocumentPickerController(string directory, bool includeSubdirectories, PSPDFLibrary library) : base(directory, includeSubdirectories, library)
        {
            Delegate = this;
            FullTextSearchEnabled = true;
        }

        public void DidSelectDocument(PSPDFDocumentPickerController controller, PSPDFDocument document, nuint pageIndex, string searchString)
        {
            var pdfController = new PSPDFViewController(document);
            pdfController.PageIndex = pageIndex;
            controller.NavigationController.PushViewController(pdfController, true);
        }
    }
}
