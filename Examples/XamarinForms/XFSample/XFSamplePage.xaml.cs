using Xamarin.Forms;

namespace XFSample {
	public partial class XFSamplePage : ContentPage {
		public XFSamplePage ()
		{
			InitializeComponent ();
		}

		async void ShowModalPDF (object sender, System.EventArgs e)
		{
			await Navigation.PushModalAsync (new ModalPDFViewer ());
		}

		async void ShowPDF (object sender, System.EventArgs e)
		{
			await Navigation.PushAsync (new PDFViewer ());
		}
	}
}
