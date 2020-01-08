
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;
using UIKit;
using CoreFoundation;
using System;
using System.IO;

namespace PSPDFCatalog
{
    public class IndexedFullTextSearchController : PSPDFViewController
    {
        public PSPDFLibrary library = PSPDFKitGlobal.SharedInstance.Library;

        public IndexedFullTextSearchController(PSPDFDocument document) : base(document)
        {

        }

        public override void ViewDidAppear(bool animated)
        {
            // The UIAlertController (which we use further down) can only be presented when the view hierarchy is fully set up, which happens in the `ViewDidAppear` method.
            base.ViewDidAppear(animated);

            // Get the direcotry url from the PSPDFDocument you are presenting
            // You can also use another path, like the bundle path where your resources are stored
            var directory = Document.FileUrl.RemoveLastPathComponent();

            // Here you can check if a path actually contains PDF files
            var directories = Directory.GetFiles(NSBundle.MainBundle.BundlePath + "/Pdf", "*.pdf");
            foreach (var dir in directories)
            {
                Console.WriteLine(dir);
            }

            PSPDFLibraryFileSystemDataSource fileDataSource = new PSPDFLibraryFileSystemDataSource(library, directory, (PSPDFDocument document, ref bool stop) =>
            {
                return true;
            });

            library.DataSource = fileDataSource;

            DispatchQueue.GetGlobalQueue(DispatchQueuePriority.Background).DispatchAsync(() =>
            {
                library.UpdateIndex(null);
            });

            // We only start the FTS once all documents are indexed.
            NSNotificationCenter.DefaultCenter.AddObserver(PSPDFLibrary.DidFinishIndexingDocumentNotification, null, NSOperationQueue.MainQueue, notification => libraryDidFinishIndexing());
        }

        public void libraryDidFinishIndexing()
        {
            var searchstring = "";
            NSDictionary dic = null;

            var alert = UIAlertController.Create("", "Start Full-Text Search with String:", UIAlertControllerStyle.Alert);

            alert.AddTextField(textField => {
                textField.Placeholder = "";
            });

            var cancelAction = UIAlertAction.Create("Cancel", UIAlertActionStyle.Destructive, null);
            var okAction = UIAlertAction.Create("Search", UIAlertActionStyle.Default, Handle => {
                // We use the content of the textfield as the search string
                searchstring = alert.TextFields[0].Text;
                // Create a new alert controller for our results (cheap way to display them on the view)
                var resultsAlert = UIAlertController.Create("", "", UIAlertControllerStyle.Alert);

                library.FindDocumentUids(searchstring, dic, (string searchString, NSDictionary<NSString, NSIndexSet> resultSet) =>
                {
                    string results = "";
                    foreach ((var UID, var indexSet) in resultSet)
                    {
                        // Get all the results into a single string so we can display them all at once
                        results += string.Format("Found the following matches for \"{0}\" in document {1}: {2}\n\n", searchstring, UID, indexSet);
                    }
                    // Need to present the alert controller on the UI thread otherwise it crashes
                    DispatchQueue.MainQueue.DispatchAsync(() =>
                    {
                        resultsAlert.Message = results;
                        resultsAlert.AddAction(cancelAction);
                        PresentViewController(resultsAlert, false, null);
                    });

                });

            });

            alert.AddAction(cancelAction);
            alert.AddAction(okAction);
            PresentViewController(alert, false, null);
        }
    }
}
