using System;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog.Catalog.Customization
{
    public class NotificationViewController: PSPDFViewController
    {
        UIAlertController alert = new UIAlertController();
        
        public NotificationViewController()
        {
        }

        public NotificationViewController(PSPDFDocument document) : base(document)
        {
        }

        public override void CommonInit(PSPDFDocument document, PSPDFConfiguration configuration)
        {
            base.CommonInit(document, configuration);

            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationsAddedNotification, null, NSOperationQueue.MainQueue, notification => annotationAddedNotification());
            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationChangedNotification, null, NSOperationQueue.MainQueue, notification => annotationChangedNotification());
            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationsRemovedNotification, null, NSOperationQueue.MainQueue, notification => annotationRemovedNotification());
        }

        private void annotationAddedNotification()
        {
            alert = UIAlertController.Create("", "An Annotation has been created!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }

        private void annotationChangedNotification()
        {
            alert = UIAlertController.Create("", "An Annotation has been changed!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }

        private void annotationRemovedNotification()
        {
            alert = UIAlertController.Create("", "An Annotation has been deleted!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }
    }
}
