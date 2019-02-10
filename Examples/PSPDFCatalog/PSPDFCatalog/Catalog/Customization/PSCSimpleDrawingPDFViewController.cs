using System;
using Foundation;
using UIKit;

using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog {
	public class PSCSimpleDrawingPDFViewController : PSPDFViewController {
		public UIButton DrawButton { get; set; }

		public PSCSimpleDrawingPDFViewController (PSPDFDocument document) : base (document)
		{
		}

		public PSCSimpleDrawingPDFViewController (IntPtr handle) : base (handle)
		{
		}

		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			// Set up global draw button
			DrawButton = UIButton.FromType (UIButtonType.System);
			DrawButton.SetTitle ("Draw", UIControlState.Normal);
			DrawButton.TintColor = UIColor.Black;
			DrawButton.ContentEdgeInsets = new UIEdgeInsets (10, 10, 10, 10);
			DrawButton.Layer.CornerRadius = 5;
			DrawButton.SizeToFit ();
			DrawButton.TouchUpInside += HandleTouchUpInside;

			ContentView.AddSubview (DrawButton);
			UpdateDrawButtonAppearance ();
		}

		public override void ViewWillLayoutSubviews ()
		{
			base.ViewWillLayoutSubviews ();
			DrawButton.Center = View.Center;
		}

		void HandleTouchUpInside (object sender, EventArgs e)
		{
			AnnotationStateManager.DrawColor = UIColor.Red;
			AnnotationStateManager.ToggleState (PSPDFAnnotationStringUI.Ink);
			UpdateDrawButtonAppearance ();
		}

		void UpdateDrawButtonAppearance ()
		{
			if (AnnotationStateManager.State == PSPDFAnnotationStringUI.Ink)
				DrawButton.BackgroundColor = UIColor.FromRGBA (0.846f, 1.0f, 0.871f, 1.0f);
			else
				DrawButton.BackgroundColor = UIColor.Green;
		}
	}
}

