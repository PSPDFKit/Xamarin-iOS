using System;
using System.Timers;
using System.Collections.Generic;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Linq;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog
{
    public class CustomAnnotationProvider : PSPDFContainerAnnotationProvider
    {

        Dictionary<nuint, PSPDFAnnotation[]> annotationDict;
        PSPDFDocument document;
        Timer timer;
        static readonly Random rnd = new Random();

        public CustomAnnotationProvider(PSPDFDocumentProvider documentProvider) : base(documentProvider)
        {
            document = documentProvider.Document;
            timer = new Timer(1000);
            timer.Elapsed += PickColor;
            timer.Start();
        }

		// You must mannually Export OPTIONAL Messages/Properties from the PSPDFAnnotationProvider Protocol (aka IPSPDFAnnotationProvider)
		IPSPDFAnnotationProviderChangeNotifier providerDelegate;
		public IPSPDFAnnotationProviderChangeNotifier ProviderDelegate {
			[Export ("providerDelegate")]
			get {
				return providerDelegate;
			}
			[Export ("setProviderDelegate:")]
			set {
				if (value != providerDelegate) {
					providerDelegate = value;

					if (providerDelegate == null) {
						timer.Stop ();
						timer.Dispose ();
						timer = null;
					}
				}
			}
		}

		public override PSPDFAnnotation [] GetAnnotations (nuint pageIndex)
		{
			if (annotationDict == null)
                annotationDict = new Dictionary<nuint, PSPDFAnnotation []> ((int)document.PageCount);

            if (annotationDict.ContainsKey(pageIndex))
            {
                return annotationDict[pageIndex];
            }
            // it's important that this method is:
            // - fast
            // - thread safe
            // - and caches annotations (don't always create new objects!)
            lock (this) {
				// create new note annotation and add it to the dict.
				var documentProvider = ProviderDelegate.ParentDocumentProvider;
				var pageInfo = documentProvider.Document.GetPageInfo (pageIndex);
				var noteAnnotation = new PSPDFNoteAnnotation {
					PageIndex = pageIndex,
					Contents = string.Format ("Annotation from the custom annotationProvider for page {0}.", pageIndex + 1),
					// place it top left (PDF coordinate space starts from bottom left)
					BoundingBox = new CGRect (100, pageInfo.Size.Height - 100, 32, 32),
					Editable = false
				};
				if (!annotationDict.ContainsKey (pageIndex))
                    annotationDict.Add (pageIndex, new PSPDFAnnotation [] { noteAnnotation });
			}
			return annotationDict[pageIndex];
		}

        public override PSPDFAnnotation[] AddAnnotations(PSPDFAnnotation[] annotations, NSDictionary<NSString, NSObject> options)
        {
            // Create an annotation list instead of an array because it's easier to work with
            // We want to apply this to all annotation that are added (this only matters when you add 2 or more annotations at the same time via copy/paste etc)
            foreach (var annotation in annotations)
            {
                // Need to add the already existing annotations
                List<PSPDFAnnotation> existingAnnotations = annotationDict[annotation.PageIndex].ToList();
                // Add all the other annotations once they're drawn
                existingAnnotations.Add(annotation);
                var annotationsAray = existingAnnotations.ToArray();
                annotationDict[annotation.PageIndex] = annotationsAray;
            }
            return base.AddAnnotations(annotations, options);
        }

        void PickColor (object o, EventArgs e)
		{
			// Random Color
			UIColor color = UIColor.FromRGBA (rnd.Next (0, 255), rnd.Next (0, 255), rnd.Next (0, 255), 1);
			lock (this) {
				foreach (var annotations in annotationDict) {
                    // We want every annotation to use the custom color
                    foreach (var annotation in annotations.Value)
                    {
                        if (annotation != null)
                        {
                            annotation.Color = color;

                            ProviderDelegate.UpdateAnnotations(annotations.Value, true);
                        }
                    }
				}
			}
		}
	}
}
