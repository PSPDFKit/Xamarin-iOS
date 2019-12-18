using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;

namespace PSPDFCatalog
{
    public class NotificationViewController: PSPDFViewController
    {
        UIAlertController alert = new UIAlertController();

        public NotificationViewController(PSPDFDocument document) : base(document)
        {
        }

        public override void ViewDidAppear(bool animated)
        {
            base.ViewDidAppear(animated);

            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationsAddedNotification, null, NSOperationQueue.MainQueue, notification => annotationAddedNotification());
            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationChangedNotification, null, NSOperationQueue.MainQueue, notification => annotationChangedNotification());
            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFAnnotationManager.AnnotationsRemovedNotification, null, NSOperationQueue.MainQueue, notification => annotationRemovedNotification());
        }

        private void annotationAddedNotification()
        {
            alert = UIAlertController.Create("", "An annotation has been created!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }

        private void annotationChangedNotification()
        {
            alert = UIAlertController.Create("", "An annotation has been changed!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }

        private void annotationRemovedNotification()
        {
            alert = UIAlertController.Create("", "An annotation has been deleted!", UIAlertControllerStyle.Alert);
            alert.AddAction(UIAlertAction.Create("Ok", UIAlertActionStyle.Default, null));
            PresentViewController(alert, false, null);
        }
    }
}
