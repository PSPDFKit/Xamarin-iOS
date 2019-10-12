using PSPDFKit.UI;
using PSPDFKit.Model;
using UIKit;
using System;
using System.Collections.Generic;
using Foundation;

namespace PSPDFCatalog
{
    public class CopyAnnotationsViewController : PSPDFViewController
    {
        public PSPDFDocument document;
        private NSUrl document1;
        public static readonly string PdfFilePath = "/Pdf/copy.pdf";
        public static readonly string PdfFilePath2 = "/Pdf/copy2.pdf";

        public CopyAnnotationsViewController()
        {
        }

        public CopyAnnotationsViewController(NSUrl document1)
        {
            this.document1 = document1;
        }

        public CopyAnnotationsViewController(PSPDFDocument document) : base(document)
        {
        }

        public override void CommonInit(PSPDFDocument document, PSPDFConfiguration configuration)
        {
            base.CommonInit(document, configuration);

            var saveButton = new UIBarButtonItem("Save", UIBarButtonItemStyle.Plain, SaveButtonPressed);

            //document.AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled;

            NavigationItem.RightBarButtonItems = new[] { saveButton };
        }

        void SaveButtonPressed(object sender, EventArgs e)
        {
            if (document.HasDirtyAnnotations)
            {
                // Need to get all annotations from the first page of the original doc
                var annotList = document.GetAnnotations(0, PSPDFAnnotationType.All);

                // Since we can't copy an array of NSObjects we need to copy them each and add them to a list afterwards
                List<PSPDFAnnotation> annotations = new List<PSPDFAnnotation>();
                foreach (PSPDFAnnotation annotation in annotList)
                {
                    PSPDFAnnotation annot = (PSPDFAnnotation)annotation.Copy();
                    annotations.Add(annot);
                }

                // Back to an array so we can work with it
                PSPDFAnnotation[] annotationArray = annotations.ToArray();

                // Need a dictionary for `addAnnotations` so we just create an empty one to not use any additional options
                NSDictionary dic = null;

                PSPDFDocument newDocument = new PSPDFDocument(NSUrl.FromFilename(PdfFilePath2));

                newDocument.AddAnnotations(annotationArray, dic);

                // Need to create the config for the new document
                PSPDFProcessorConfiguration conf = new PSPDFProcessorConfiguration(newDocument);
                PSPDFProcessor processor = new PSPDFProcessor(conf, null);
                processor.WriteToFile(newDocument.FileUrl);
            }
            
        }

    }
}
