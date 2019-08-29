using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog
{
    public class ProgrammaticFormFillingController : PSPDFViewController
    {
        public ProgrammaticFormFillingController(NSUrl document) : base (new PSPDFDocument(document))
        {
            Document.AnnotationSaveMode = PSPDFAnnotationSaveMode.Disabled;

            CoreFoundation.DispatchQueue.DefaultGlobalQueue.DispatchAsync(() =>
            {
                // Get all form objects and fill them in.
                PSPDFAnnotation[] annotations = Document.GetAnnotations(0, PSPDFAnnotationType.Widget);
                foreach (var formElement in annotations)
                {
                    NSThread.SleepFor(0.8);

                    // Always update the model on the main thread.
                    CoreFoundation.DispatchQueue.MainQueue.DispatchAsync(() =>
                    {
                        if (formElement is PSPDFTextFieldFormElement)
                        {
                            PSPDFTextFieldFormElement textFieldElement = (PSPDFTextFieldFormElement)formElement;
                            NSString fieldName = (NSString)((PSPDFTextFieldFormElement)formElement).FieldName;

                            if (textFieldElement.InputFormat == PSPDFTextInputFormat.Date)
                            {
                                textFieldElement.Contents = "01/01/2001";
                            }
                            else if (fieldName == "Telephone_Home")
                            {
                                textFieldElement.Contents = "0123456";
                            }
                            // Social Security Number needs exactly 9 digits
                            else if (fieldName == "SSN")
                            {
                                textFieldElement.Contents = "012345678";
                              // The other phone numbers need exactly 10 digits
                            }
                            else if (fieldName == "Telephone_Work" || fieldName == "Emergency_Phone")
                            {
                                textFieldElement.Contents = "0123456789";
                            }
                            // All the other form fields don't have any special validation
                            else
                            {
                                textFieldElement.Contents = fieldName;
                            }
                        }
                        else if (formElement is PSPDFButtonFormElement)
                        {
                            ((PSPDFButtonFormElement)formElement).ToggleButtonSelectionState();
                        }
                    });

                }
            });
        }
    }
}
