﻿using System;

using PSPDFKit;

namespace PSPDFCatalog
{
	public class CustomPDFDocument : PSPDFDocument
	{
		public CustomPDFDocument (Foundation.NSUrl url) : base (url)
		{
		}
			
		public override UIKit.UIColor BackgroundColorForPage (nuint page)
		{
			Console.WriteLine("Requesting color for page " + page);
			return UIKit.UIColor.White;
		}

		public override bool AllowsCopying {
			get {
				return false;
			}
		}
	}
}
