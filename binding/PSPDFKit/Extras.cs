using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

#if __UNIFIED__
using ObjCRuntime;
using Foundation;
using UIKit;
using CoreGraphics;
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using System.Drawing;

using CGRect = global::System.Drawing.RectangleF;
using CGSize = global::System.Drawing.SizeF;
using CGPoint = global::System.Drawing.PointF;

using nfloat = global::System.Single;
using nint = global::System.Int32;
using nuint = global::System.UInt32;
#endif

namespace PSPDFKit
{
	public partial class PSPDFLicenseManager
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFSetLicenseKey")]
		private static extern uint _SetLicenseKey (IntPtr licenseKey);

		public static PSPDFFeatureMask SetLicenseKey (string licenseKey)
		{
			IntPtr licensePtr;
			PSPDFFeatureMask result;

			licensePtr = Marshal.StringToHGlobalAnsi (licenseKey);
			result = (PSPDFFeatureMask)_SetLicenseKey (licensePtr);

			Marshal.FreeHGlobal (licensePtr);

			return result;
		}
	}

	public partial class PSPDFLogging
	{
		private static PSPDFLogLevelMask PSPDFLogLevel;
		public static PSPDFLogLevelMask LogLevel
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLogLevel");
				PSPDFLogLevel = (PSPDFLogLevelMask)(uint)Marshal.PtrToStructure (ptr, typeof(uint));

				return PSPDFLogLevel;
			}
			set 
			{
				PSPDFLogLevel = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLogLevel");
				byte [] data = BitConverter.GetBytes ((uint)PSPDFLogLevel);
				Marshal.Copy (data, 0, ptr, data.Length); 
			}
		}
	}

	public static class PSPDFLocalization
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFLocalize")]
		private static extern IntPtr _Localize (IntPtr stringToken);

		public static string Localize (string stringToken)
		{
			var ret = _Localize (new NSString (stringToken).Handle);
			var str = Runtime.GetNSObject<NSString> (ret);
			return (string) str;
		}

		[DllImport  ("__Internal", EntryPoint = "PSPDFSetLocalizationDictionary")]
		private static extern void _SetLocalizationDictionary (IntPtr localizationDict);

		public static void SetLocalizationDictionary (NSDictionary localizationDict)
		{
			_SetLocalizationDictionary (localizationDict.Handle);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFBundleImage")]
		private static extern IntPtr _BundleImage (IntPtr imageName);

		public static UIImage BundleImage (string imageName)
		{
			var ret = _BundleImage (new NSString (imageName).Handle);
			var img = Runtime.GetNSObject<UIImage> (ret);
			return img;
		}
	}

	public partial class PSPDFViewController
	{
		[DllImport  ("__Internal", EntryPoint = "PSPDFChildViewControllerForClass")]
		private static extern IntPtr _ChildViewControllerForClass (IntPtr controller, IntPtr controllerClass, bool onlyReturnIfVisible);

		public static UIViewController ChildViewControllerForClass (NSObject controller, Class controllerClass, bool onlyReturnIfVisible)
		{
			var ret = _ChildViewControllerForClass (controller.Handle, controllerClass.Handle, onlyReturnIfVisible);
			var vc = Runtime.GetNSObject<UIViewController> (ret);
			return vc;
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFSupportsPopover")]
		[return: MarshalAsAttribute (UnmanagedType.Bool)]
		private static extern bool _SupportsPopover ();

		public static bool SupportsPopover {
			get {
				return _SupportsPopover ();
			}
		}
	}

	public partial class PSPDFDocument : NSObject
	{
		public static PSPDFDocument FromDataProvider (CGDataProvider dataProvider)
		{
			return FromDataProvider (dataProvider.Handle);
		}

		public virtual bool EnsureDataDirectoryExists (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _EnsureDataDirectoryExists (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveAnnotations (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotations (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFPageInfo PageInfoForPage (nuint page, CGPDFPage pageRef)
		{
			return PageInfoForPage (page, pageRef.Handle);
		}

		public virtual CGRect BoxRectForPage (CGPDFBox boxType, nuint page, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				CGRect ret = BoxRectForPage (boxType, page, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFDocumentProvider : NSObject
	{
		public PSPDFDocumentProvider (CGDataProvider dataProvider, PSPDFDocument document) : this (dataProvider.Handle, document)
		{
		}

		public CGDataProvider DataProvider
		{
			get 
			{
				IntPtr ptr = this._DataProvider;
				return Runtime.GetINativeObject<CGDataProvider> (ptr, false);
			}
		}

		public virtual NSData DataRepresentation (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSData ret = DataRepresentationWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFPageInfo PageInfoForPage (nuint page, CGPDFPage pageRef)
		{
			return PageInfoForPage (page, pageRef.Handle);
		}

		public virtual bool SaveAnnotations (NSDictionary options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotationsWithOptions (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFCache : NSObject
	{
		private static Class PSPDFCacheClass;
		public static Class CacheClass
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var classPtr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFCacheClass");

				if (classPtr != IntPtr.Zero) {
					PSPDFCacheClass = Runtime.GetINativeObject<Class> (classPtr, false);
					return PSPDFCacheClass;
				} else
					return PSPDFCacheClass = null;

			}
			set 
			{
				PSPDFCacheClass = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFCacheClass");

				Marshal.WriteIntPtr (ptr, PSPDFCacheClass.Handle);
			}
		}
	}

	public partial class PSPDFPageInfo : NSObject
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFConvertViewPointToPDFPoint")]
		private static extern IntPtr PSPDFConvertViewPointToPDFPoint (IntPtr viewPoint, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static CGPoint ConvertViewPointToPdfPoint (CGPoint viewPoint, CGRect cropBox, nuint rotation, CGRect bounds)
		{
			IntPtr viewPointPtr = Marshal.AllocHGlobal (Marshal.SizeOf (viewPoint));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			CGPoint result;

			try {
				Marshal.StructureToPtr ((object)viewPoint, viewPointPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertViewPointToPDFPoint (viewPointPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (CGPoint)Marshal.PtrToStructure (resultPtr, typeof(CGPoint));
			}
			finally {
				Marshal.FreeHGlobal (viewPointPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFConvertPDFPointToViewPoint")]
		private static extern IntPtr PSPDFConvertPDFPointToViewPoint (IntPtr pdfPoint, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static CGPoint ConvertPdfPointToViewPoint (CGPoint pdfPoint, CGRect cropBox, nuint rotation, CGRect bounds)
		{
			IntPtr pdfPointPtr = Marshal.AllocHGlobal (Marshal.SizeOf (pdfPoint));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			CGPoint result;

			try {
				Marshal.StructureToPtr ((object)pdfPoint, pdfPointPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFPointToViewPoint (pdfPointPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (CGPoint)Marshal.PtrToStructure (resultPtr, typeof(CGPoint));
			}
			finally {
				Marshal.FreeHGlobal (pdfPointPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFConvertPDFRectToViewRect")]
		private static extern IntPtr PSPDFConvertPDFRectToViewRect (IntPtr pdfRect, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static CGRect ConvertPDFRectToViewRect (CGRect pdfRect, CGRect cropBox, nuint rotation, CGRect bounds)
		{
			IntPtr pdfRectPtr = Marshal.AllocHGlobal (Marshal.SizeOf (pdfRect));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			CGRect result;

			try {
				Marshal.StructureToPtr ((object)pdfRect, pdfRectPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFRectToViewRect (pdfRectPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (CGRect)Marshal.PtrToStructure (resultPtr, typeof(CGRect));
			}
			finally {
				Marshal.FreeHGlobal (pdfRectPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFConvertViewRectToPDFRect")]
		private static extern IntPtr PSPDFConvertViewRectToPDFRect (IntPtr viewRect, IntPtr cropBox, IntPtr rotation, IntPtr bounds);

		public static CGRect ConvertViewRectToPDFRect (CGRect viewRect, CGRect cropBox, nuint rotation, CGRect bounds)
		{
			IntPtr viewRectPtr = Marshal.AllocHGlobal (Marshal.SizeOf (viewRect));
			IntPtr cropBoxPtr = Marshal.AllocHGlobal (Marshal.SizeOf (cropBox));
			IntPtr rotationPtr = Marshal.AllocHGlobal (Marshal.SizeOf (rotation));
			IntPtr boundsPtr = Marshal.AllocHGlobal (Marshal.SizeOf (bounds));

			IntPtr resultPtr;
			CGRect result;

			try {
				Marshal.StructureToPtr ((object)viewRect, viewRectPtr, true);
				Marshal.StructureToPtr ((object)cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object)rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object)bounds, boundsPtr, true);

				resultPtr = PSPDFConvertPDFRectToViewRect (viewRectPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (CGRect)Marshal.PtrToStructure (resultPtr, typeof(CGRect));
			}
			finally {
				Marshal.FreeHGlobal (viewRectPtr);
				Marshal.FreeHGlobal (cropBoxPtr);
				Marshal.FreeHGlobal (rotationPtr);
				Marshal.FreeHGlobal (boundsPtr);
			}
			return result;
		}
	}

	public partial class PSPDFBookmarkParser : NSObject
	{
		public virtual bool ClearAllBookmarks (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = ClearAllBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFBookmark [] LoadBookmarks (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				PSPDFBookmark [] ret = LoadBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveBookmarks (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveBookmarksWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFGoToAction : PSPDFAction
	{
		public static nuint ResolveActions (PSPDFGoToAction [] actions, CGPDFDocument documentRef)
		{
			return ResolveActionsWithNamedDestinations (actions, documentRef.Handle);
		}
	}

	public partial class PSPDFTextParser : NSObject
	{
		public PSPDFTextParser (CGPDFPage pageRef, nuint page, PSPDFDocumentProvider documentProvider, NSMutableDictionary fontCache, bool hideGlyphsOutsidePageRect, CGPDFBox pdfBox) : this (pageRef.Handle, page, documentProvider, fontCache, hideGlyphsOutsidePageRect, pdfBox)
		{
		}

		public PSPDFTextParser (CGPDFStream stream)
		{
			Handle = PSPDFTextParser.FromStream (stream.Handle);
		}

		private static nuint PSPDFMaxShadowGlyphSearchDepth;
		public static nuint MaxShadowGlyphSearchDepth
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMaxShadowGlyphSearchDepth");
				PSPDFMaxShadowGlyphSearchDepth = (nuint)Marshal.PtrToStructure(ptr, typeof(nuint));

				return PSPDFMaxShadowGlyphSearchDepth;
			}
			set 
			{
				PSPDFMaxShadowGlyphSearchDepth = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMaxShadowGlyphSearchDepth");
				byte [] data = BitConverter.GetBytes (PSPDFMaxShadowGlyphSearchDepth);
				Marshal.Copy (data, 0, ptr, data.Length); 
			}
		}
	}

	public partial class PSPDFGlyph : NSObject
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFGlyphIsOnSameLineSegmentAsGlyph")]
		[return: MarshalAsAttribute (UnmanagedType.Bool)]
		private static extern bool _IsOnSameLineSegmentAsGlyph (IntPtr glyph1, IntPtr glyph2);

		public static bool IsOnSameLineSegmentAsGlyph (PSPDFGlyph glyph1, PSPDFGlyph glyph2)
		{
			return _IsOnSameLineSegmentAsGlyph (glyph1.Handle, glyph2.Handle);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFRectsFromGlyphs")]
		private static extern CGRect [] _RectsFromGlyphs (IntPtr glyphs, CGAffineTransform t, CGRect boundingBox);

		public static CGRect [] RectsFromGlyphs(PSPDFGlyph [] glyphs, CGAffineTransform t, CGRect boundingBox)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return _RectsFromGlyphs(arry.Handle, t, boundingBox);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFBoundingBoxFromGlyphs")]
		private static extern CGRect _BoundingBoxFromGlyphs(IntPtr glyphs, CGAffineTransform t);

		public static CGRect BoundingBoxFromGlyphs(PSPDFGlyph [] glyphs, CGAffineTransform t)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return _BoundingBoxFromGlyphs(arry.Handle, t);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFReduceGlyphsToColumn")]
		private static extern IntPtr _ReduceGlyphsToColumn(IntPtr glyphs);

		public static NSArray ReduceGlyphsToColumn (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects(objs.ToArray());
			return Runtime.GetNSObject<NSArray>(_ReduceGlyphsToColumn(arry.Handle));
		}
	}

	public partial class PSPDFWord : NSObject
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFStringFromGlyphs")]
		private static extern IntPtr _StringFromGlyphs (IntPtr glyphs);

		public static string StringFromGlyphs (PSPDFGlyph [] glyphs)
		{
			var objs = new List<NSObject>();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			var arry = NSArray.FromNSObjects (objs.ToArray());

			var str = Runtime.GetNSObject<NSString> (_StringFromGlyphs (arry.Handle));
			return (string) str;
		}
	}

	public partial class PSPDFTextLine : PSPDFWord
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFSetNextLineIfCloserDistance")]
		private static extern void _SetNextLineIfCloserDistance (IntPtr txt, IntPtr nextLine);

		public static void SetNextLineIfCloserDistance (PSPDFTextLine txt, PSPDFTextLine nextLine)
		{
			_SetNextLineIfCloserDistance (txt.Handle, nextLine.Handle);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFSetPrevLineIfCloserDistance")]
		private static extern void _SetPrevLineIfCloserDistance (IntPtr txt, IntPtr nextLine);

		public static void SetPrevLineIfCloserDistance (PSPDFTextLine txt, PSPDFTextLine nextLine)
		{
			_SetPrevLineIfCloserDistance (txt.Handle, nextLine.Handle);
		}
	}

	public partial class PSPDFImageInfo : NSObject
	{
		public virtual UIImage GetImage (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = GetImage (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual UIImage GetImageInRgbColorSpace (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = GetImageInRgbColorSpace (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFFontInfo : NSObject
	{
		public PSPDFFontInfo (CGPDFDictionary font, string fontKey) : this (font.Handle, fontKey)
		{
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFNormalizeString")]
		private static extern IntPtr _NormalizeString (IntPtr str);

		public static string NormalizeString (string str)
		{
			var ret = _NormalizeString (new NSString (str).Handle);
			var strRes = Runtime.GetNSObject<NSString> (ret);
			return (string) strRes;
		}
	}

	public partial class PSPDFSearchViewController
	{
		private static nuint PSPDFMinimumSearchLength;
		public static nuint MinimumSearchLength
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMinimumSearchLength");
				PSPDFMinimumSearchLength = (nuint)Marshal.PtrToStructure(ptr, typeof(nuint));

				return PSPDFMinimumSearchLength;
			}
			set 
			{
				PSPDFMinimumSearchLength = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFMinimumSearchLength");
				byte [] data = BitConverter.GetBytes (PSPDFMinimumSearchLength);
				Marshal.Copy (data, 0, ptr, data.Length); 
			}
		}
	}

	public partial class PSPDFLibrary : NSObject
	{
		private static nuint PSPDFLibraryVersion;
		public static nuint LibraryVersion
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLibraryVersion");
				PSPDFLibraryVersion = (nuint)Marshal.PtrToStructure(ptr, typeof(nuint));

				return PSPDFLibraryVersion;
			}
		}
	}

	public partial class PSPDFAnnotationManager : NSObject
	{
		public virtual bool SaveAnnotations (PSPDFAnnotation options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotations (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFAnnotation [] AnnotationsForPage (nuint page, PSPDFAnnotationType type, CGPDFPage pageRef)
		{
			return AnnotationsForPage (page, type, pageRef.Handle);
		}
	}

	public partial class PSPDFAnnotation : PSPDFModel
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFStringFromAnnotationType")]
		private static extern IntPtr _StringFromAnnotationType (nuint annotationType);

		public static string StringFromAnnotationType (PSPDFAnnotationType annotationType)
		{
			return (string) Runtime.GetNSObject<NSString> (_StringFromAnnotationType ((nuint)(ulong)annotationType));
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationTypeFromString")]
		private static extern nuint _AnnotationTypeFromString (IntPtr theString);

		public static PSPDFAnnotationType AnnotationTypeFromString (NSString theString)
		{
			return (PSPDFAnnotationType)(ulong)_AnnotationTypeFromString (theString.Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationRegisterOverrideClasses")]
		private static extern void _RegisterOverrideClasses (IntPtr annotationType, IntPtr document);

		public static void RegisterOverrideClasses (NSKeyedUnarchiver unarchiver, PSPDFDocument document)
		{
			_RegisterOverrideClasses (unarchiver.Handle, document.Handle);
		}
	}

	public partial class PSPDFFileAnnotationProvider : PSPDFContainerAnnotationProvider
	{
		public virtual PSPDFAnnotation [] AnnotationsForPage (uint page, CGPDFPage pageRef)
		{
			return AnnotationsForPage (page, pageRef.Handle);
		}

		public virtual bool TryLoadAnnotationsFromFile (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = TryLoadAnnotationsFromFile (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual PSPDFAnnotation [] ParseAnnotations (uint page, CGPDFPage pageRef)
		{
			return ParseAnnotations (page, pageRef.Handle);
		}

		public virtual bool SaveAnnotations (NSDictionary options, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotations (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual NSDictionary LoadAnnotations (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSDictionary ret = LoadAnnotations (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFStampAnnotation : PSPDFAnnotation
	{
		public virtual UIImage LoadImage (CGAffineTransform transform, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = LoadImage (transform, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFSoundAnnotation : PSPDFAnnotation
	{
		public virtual bool LoadAttributesFromAudioFile (out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = LoadAttributesFromAudioFile (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFAppearanceCharacteristics : PSPDFModel
	{
		public static PSPDFAppearanceCharacteristics FromPdfDictionary (CGPDFDictionary apCharacteristicsDict)
		{
			return FromPdfDictionary (apCharacteristicsDict.Handle);
		}
	}

	public partial class PSPDFIconFit : PSPDFModel
	{
		public static PSPDFIconFit IconFitFromPdfDictionary (CGPDFDictionary iconFitDict)
		{
			return IconFitFromPdfDictionary (iconFitDict.Handle);
		}
	}

	public partial class PSPDFAnnotationGroupItem
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFAnnotationStateVariantIdentifier")]
		private static extern IntPtr _VariantIdentifier (IntPtr state, IntPtr variant);

		public static string StateVariantIdentifier (string state, string variant)
		{
			return (string) Runtime.GetNSObject<NSString> (_VariantIdentifier (new NSString (state).Handle, new NSString (variant).Handle));
		}
	}

	public partial class PSPDFMenuItem : UIMenuItem
	{
		private static bool PSPDFAllowImagesForMenuItems;
		public static bool AllowImagesForMenuItems
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				PSPDFAllowImagesForMenuItems = Convert.ToBoolean (Marshal.ReadByte(ptr));

				return PSPDFAllowImagesForMenuItems;
			}
			set 
			{
				PSPDFAllowImagesForMenuItems = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFAllowImagesForMenuItems));
			}
		}
	}

	public partial class PSPDFProcessor : NSObject
	{
		private static CGRect PSPDFPaperSizeA4;
		public static CGRect PaperSizeA4
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeA4");
				PSPDFPaperSizeA4 = (CGRect)Marshal.PtrToStructure(ptr, typeof(CGRect));

				return PSPDFPaperSizeA4;
			}
		}

		private static CGRect PSPDFPaperSizeLetter;
		public static CGRect PaperSizeLetter
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeLetter");
				PSPDFPaperSizeLetter = (CGRect)Marshal.PtrToStructure(ptr, typeof(CGRect));

				return PSPDFPaperSizeLetter;
			}
		}
	}

	public partial class PSPDFXFDFParser : NSObject
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFConvertXFDFSoundEncodingToPDF")]
		private static extern IntPtr _ConvertXfdSoundEncodingToPdf (IntPtr encoding);

		public static string StateVariantIdentifier (string encoding)
		{
			return (string) Runtime.GetNSObject<NSString> (_ConvertXfdSoundEncodingToPdf (new NSString (encoding).Handle));
		}
	}

	public partial class PSPDFAESCryptoDataProvider : NSObject
	{
		public CGDataProvider DataProvider
		{
			get 
			{
				IntPtr ptr = this._DataProvider;
				return Runtime.GetINativeObject<CGDataProvider> (ptr, false);
			}
		}

		public static bool IsAesCryptoDataProvider (CGDataProvider dataProvider)
		{
			return IsAESCryptoDataProvider (dataProvider.Handle);
		}

		private static uint PSPDFDefaultPBKDFNumberOfRounds;
		public static uint DefaultPBKDFNumberOfRounds
		{
			get 
			{
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDefaultPBKDFNumberOfRounds");
				PSPDFDefaultPBKDFNumberOfRounds = (uint)Marshal.PtrToStructure(ptr, typeof(uint));

				return PSPDFDefaultPBKDFNumberOfRounds;
			}
		}
	}

	public partial class PSPDFSigner : NSObject
	{
		public virtual NSData SignHash (NSData hash, PSPDFSigningAlgorithm algorithm, out NSError error)
		{
			unsafe 
			{
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSData ret = SignHash (hash, algorithm, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFDigitalSignatureReference : PSPDFModel
	{
		public static PSPDFDigitalSignatureReference FromDictionary (CGPDFDictionary dict)
		{
			return PSPDFDigitalSignatureReference.FromDictionary (dict.Handle);
		}
	}

	public partial class PSPDFGalleryVideoItem : PSPDFGalleryItem
	{
		[DllImportAttribute("__Internal", EntryPoint = "PSPDFGalleryVideoItemQualityFromString")]
		private static extern nuint _QualityFromString (IntPtr qualityString);

		public static PSPDFGalleryVideoItemQuality QualityFromString (string qualityString)
		{
			return (PSPDFGalleryVideoItemQuality)(ulong) _QualityFromString (new NSString (qualityString).Handle);
		}

		[DllImportAttribute("__Internal", EntryPoint = "PSPDFGalleryVideoItemCoverModeFromString")]
		private static extern nuint _CoverModeFromString (IntPtr qualityString);

		public static PSPDFGalleryVideoItemCoverMode CoverModeFromString (string coverModeString)
		{
			return (PSPDFGalleryVideoItemCoverMode)(ulong) _CoverModeFromString (new NSString (coverModeString).Handle);
		}
	}

	public partial class PSPDFGalleryItem : NSObject
	{
		[DllImportAttribute("__Internal", EntryPoint = "NSStringFromPSPDFGalleryItemContentState")]
		private static extern IntPtr _StringFromPSPDFGalleryItemContentState (nuint state);

		public static string StringFromPsPdfGalleryItemContentState (PSPDFGalleryItemContentState state)
		{
			return (string) Runtime.GetNSObject<NSString> (_StringFromPSPDFGalleryItemContentState ((nuint)(ulong)state));
		}
	}
}

