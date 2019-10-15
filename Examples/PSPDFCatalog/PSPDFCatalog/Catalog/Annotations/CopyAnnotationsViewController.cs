using PSPDFKit.UI;
using PSPDFKit.Model;
using UIKit;
using System;
using System.Collections.Generic;
using Foundation;

namespace PSPDFCatalog
{
    public class CopyAnnotationsViewController : PSPDFTabbedViewController
    {
        public static readonly string PdfFilePath = "Pdf/original.pdf";
        public static readonly string PdfFilePath2 = "Pdf/copy.pdf";
        // We need a dictionary for the annotation options in the add/remove annotation methods, so we just create an empty one to not add any additional options
        NSDictionary dic = null;

        public override void CommonInit(PSPDFViewController pdfController)
        {
            base.CommonInit(pdfController);
            pdfController = PdfController;

            var saveButton = new UIBarButtonItem("Copy", UIBarButtonItemStyle.Plain, CopyButtonPressed);

            // Needed so we still have our back button
            NavigationItem.LeftItemsSupplementBackButton = true;

            pdfController.BarButtonItemsAlwaysEnabled = new[] { saveButton };

            pdfController.NavigationItem.LeftBarButtonItem = saveButton;

            if (RestoreState || Documents?.Length == 0)
            {
                var original = new PSPDFDocument(NSUrl.FromFilename(PdfFilePath));
                var copy = new PSPDFDocument(NSUrl.FromFilename(PdfFilePath2));
                Documents = new[] { original, copy };
            }
            // Remove all existing annotations on start because we want to work with a clean state
            Documents[0].RemoveAnnotations(Documents[0].GetAnnotations(0, PSPDFAnnotationType.All), dic);
            Documents[1].RemoveAnnotations(Documents[1].GetAnnotations(0, PSPDFAnnotationType.All), dic);

        }

        void CopyButtonPressed(object sender, EventArgs e)
        {
            var alert = UIAlertController.Create("Annotations were copied over sucessfully!", "Switch to the \"Copy\" file to see them.", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, true, copyAnnotations);
        }

        void copyAnnotations()
        {
            // Need to get all annotations from the first page of the original doc
            var annotList = Documents[0].GetAnnotations(0, PSPDFAnnotationType.All);

            // Since we can't copy an array of NSObjects we need to copy them each and add them to a list afterwards
            List<PSPDFAnnotation> annotations = new List<PSPDFAnnotation>();
            foreach (PSPDFAnnotation annotation in annotList)
            {
                PSPDFAnnotation annot = (PSPDFAnnotation)annotation.Copy();
                annotations.Add(annot);
            }

            // Back to an array so we can work with it
            PSPDFAnnotation[] annotationArray = annotations.ToArray();

            // Need a dictionary for the annotation options for `addAnnotations` so we just create an empty one to not use any additional options

            // Add all copied annotations to the second document
            Documents[1].AddAnnotations(annotationArray, dic);
        }

    }
}
