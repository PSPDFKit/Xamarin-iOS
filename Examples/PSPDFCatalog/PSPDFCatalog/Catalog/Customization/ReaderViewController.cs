using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class ReaderViewController: PSPDFViewController {
		public ReaderViewController (NSUrl document) : base (new PSPDFDocument (document))
		{
			NavigationItem.SetRightBarButtonItems(new [] { ReaderViewButtonItem }, true);
		}
	}
}
