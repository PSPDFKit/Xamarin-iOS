using System;
using System.Linq;
using System.Text;

using AppKit;
using CoreGraphics;
using Foundation;

using PSPDFKit.Core;

namespace MacPdfViewer {
	public partial class ViewController : NSViewController, IPSPDFRenderTaskDelegate {

		static readonly string PdfFile = "Pdf/PSPDFKit QuickStart Guide.pdf";
		PSPDFRenderTask shownPageRenderTask;
		PSPDFDocument document;
		nuint indexOfShownPage = 0;

		public ViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();
			document = new PSPDFDocument (NSUrl.FromFilename (PdfFile));
		}

		public override void ViewWillAppear ()
		{
			base.ViewWillAppear ();
			View.Window.Title = $"PSPDFKit for macOS: {document.Title}.pdf";
			PdfCurrentPage.StringValue = $"Page 1 of {document.PageCount}";

			// Get some PDF Information
			PdfTextInfo.Value = GetPdfInformation ();

			// Render first page.
			RenderShownPage ();
		}

		void CancelRenderingShownPage ()
		{
			if (shownPageRenderTask == null)
				return;

			shownPageRenderTask.Cancel ();
			shownPageRenderTask = null;
			Console.WriteLine ("[EXAMPLE] Cancelled rendering shown page.");
		}

		void RenderShownPage ()
		{
			CancelRenderingShownPage ();

			var pageInfo = document.GetPageInfo (indexOfShownPage);
			if (pageInfo == null) {
				PdfImageView.Image = null;
				return;
			}

			var pageRect = pageInfo.Rect;
			var pageImageViewSize = PdfImageView.Bounds.Size;

			var pageImageWidth = pageImageViewSize.Width;
			var pageImageHeight = (nfloat) Math.Ceiling (pageImageWidth * (pageRect.Height / pageRect.Width));

			// Rendering a page requires you to first set up a rendering request and
			// specify what and how exactly we want to render.
			var renderRequest = new PSPDFMutableRenderRequest (document) {
				PageIndex = indexOfShownPage,
				ImageSize = new CGSize (pageImageWidth, pageImageHeight),
				ImageScale = PdfImageView?.Window?.BackingScaleFactor ?? 1,
				CachePolicy = PSPDFRenderRequestCachePolicy.ReloadIgnoringCacheData
			};

			// Having the rendering request, we can then create a rendering task.
			// Think of the rendering request being a configuration object for the
			// render task (once the render task is created, the render request is
			// no longer needed).
			var renderTask = new PSPDFRenderTask (renderRequest);

			if (renderTask == null) {
				Console.WriteLine ($"[EXAMPLE] Couldn't create a render task from render request: {renderRequest}.");
				PdfImageView.Image = null;
				return;
			}

			// We also assign a high priority for our render task because most of
			// the time it is initiated by the user and we therefore need the result
			// as fast as possible.
			renderTask.Priority = PSPDFRenderQueuePriority.UserInteractive;

			// As soon as the task finishes, it notifies its delegate, see
			// implemetation of the delegate protocol below.
			renderTask.Delegate = this;

			// Becuase we display only one page at a time, we can cancel a
			// previously issue rendering request if the rendering hasn't finished
			// in time. For that, we need to keep a reference to the render task.
			shownPageRenderTask = renderTask;

			// Finally, we can schedule a render task on the default render queue.
			PSPDFKitGlobal.SharedInstance.RenderManager.RenderQueue.Schedule (renderTask);
			Console.WriteLine ("[EXAMPLE] Scheduled rendering shown page.");
		}

		[Export ("renderTaskDidFinish:")]
		public void RenderTaskDidFinish (PSPDFRenderTask task)
		{
			if (task.IsEqual (shownPageRenderTask)) {
				// A rendering request might finish but not have an image. This
				// happens for example for password protected but not yet unlocked
				// documents. In that case, we clear the old image and don't show
				// anything.

				var image = task.Image;
				if (image == null) {
					Console.WriteLine ("[EXAMPLE] Finished rendering shown page, but image was 'null'.");
					PdfImageView.Image = null;
					return;
				}

				Console.WriteLine ("[EXAMPLE] Finished rendering shown page, image is valid.");

				// The page is rendered properly, so we can show the result.
				PdfImageView.Image = image;
			}
		}

		partial void NextPage (NSButton sender) => Show (indexOfShownPage + 1);
		partial void PreviousPage (NSButton sender) => Show (indexOfShownPage - 1);

		void Show (nuint pageIndex)
		{
			if (pageIndex >= document.PageCount || pageIndex < 0)
				return;

			indexOfShownPage = pageIndex;
			PdfCurrentPage.StringValue = $"Page {pageIndex + 1} of {document.PageCount}";
			RenderShownPage ();
		}

		// Extracs some information from the PDF
		string GetPdfInformation ()
		{
			var builder = new StringBuilder ();

			builder.AppendLine ("General Info");
			builder.AppendLine ("------------");
			builder.AppendLine ($"Title: {document.Title}");
			builder.AppendLine ($"Number of pages: {document.PageCount}");
			builder.AppendLine ($"Page labels enabled: {document.PageLabelsEnabled}");
			builder.AppendLine ($"Annotations enabled: {document.AnnotationsEnabled}");
			builder.AppendLine ($"Bookmarks enabled: {document.BookmarksEnabled}");
			builder.AppendLine ($"Forms enabled: {document.FormsEnabled}");
			builder.AppendLine ($"IsJavaScriptStatusEnabled enabled: {document.IsJavaScriptStatusEnabled}");
			builder.AppendLine ();

			builder.AppendLine ("Security");
			builder.AppendLine ("--------");
			builder.AppendLine ($"Encrypted: {document.IsEncrypted}");
			builder.AppendLine ($"Locked: {document.IsLocked}");
			builder.AppendLine ($"Annotation changes allowed: {document.AllowAnnotationChanges}");
			builder.AppendLine ();

			builder.AppendLine ("Meta Data");
			builder.AppendLine ("---------");
			var metadata = new PSPDFDocumentPDFMetadata (document);
			foreach (var key in metadata.AllInfoKeys) {
				if (metadata.GetObject (key) is NSObject val)
					builder.AppendLine ($"{key}: {val}");
			}
			builder.AppendLine ();

			builder.AppendLine ("Outline");
			builder.AppendLine ("-------");
			if (document.Outline != null) {
				var outlineElements = document.Outline.AllFlattenedChildren ?? Array.Empty<PSPDFOutlineElement> ();
				foreach (var outlineElement in outlineElements) {
					var level = outlineElement.Level;
					var title = outlineElement.Title ?? "-";
					var pageNumber = outlineElement.PageIndex + 1;

					if (level > 0) {
						var indentation = string.Concat (Enumerable.Repeat ("    ", (int) level - 1));
						builder.AppendLine ($"{indentation}* {title} ({pageNumber})");
					}
				}
			} else
				builder.AppendLine ("The document doesn't have an outline.");
			builder.AppendLine ();

			builder.AppendLine ("Bookmarks");
			builder.AppendLine ("---------");
			var bookmarks = document.Bookmarks ?? Array.Empty<PSPDFBookmark> ();
			if (bookmarks.Length > 0) {
				foreach (var bookmark in bookmarks)
					builder.AppendLine ($"{bookmark.DisplayName}: {bookmark.PageIndex + 1}");
			} else
				builder.AppendLine ("The document doesn't contain any bookmarks.");
			builder.AppendLine ();

			builder.AppendLine ("Annotations");
			builder.AppendLine ("-----------");
			var allAnnotations = document.GetAllAnnotations (PSPDFAnnotationType.All);
			allAnnotations.Keys.OrderBy (kv => kv.NIntValue).ToList ().ForEach (key => {
				var annotationsOnPage = allAnnotations [key];
				builder.AppendLine ($"Page {key.NIntValue + 1}:");

				foreach (var annotation in annotationsOnPage)
					builder.AppendLine ($"- {annotation.LocalizedDescription}");
				builder.AppendLine ();
			});

			return builder.ToString ();
		}
	}
}
