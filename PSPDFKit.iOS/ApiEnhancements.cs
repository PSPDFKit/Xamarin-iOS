using System;
using System.Runtime.InteropServices;
using System.Collections.Generic;

using ObjCRuntime;
using Foundation;
using UIKit;
using CoreGraphics;
using System.Runtime.CompilerServices;

namespace PSPDFKit.iOS {

	public static class PSPDFLocalization {

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

		[Advice ("Set on the main thread omly")]
		public static void SetLocalizationDictionary (NSDictionary<NSString, NSDictionary<NSString, NSString>> localizationDict)
		{
			_SetLocalizationDictionary (localizationDict == null ? IntPtr.Zero : localizationDict.Handle);
		}
	}

	public partial class PSPDFViewController {

		[DllImport  ("__Internal", EntryPoint = "PSPDFChildViewControllerForClass")]
		private static extern IntPtr _ChildViewControllerForClass (IntPtr controller, IntPtr controllerClass);

		public static UIViewController ChildViewControllerForClass (UIViewController controller, Class controllerClass)
		{
			var ret = _ChildViewControllerForClass (controller != null ? controller.Handle : IntPtr.Zero, controllerClass.Handle);
			if (ret == IntPtr.Zero)
				return null;

			var vc = Runtime.GetNSObject<UIViewController> (ret);
			return vc;
		}
	}

	public partial class PSPDFDocumentSharingCoordinator : NSObject {

		public virtual bool IsAvailableUserInvoked (bool userInvoked, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _IsAvailableUserInvoked (userInvoked, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFDocument : NSObject {

		public virtual bool EnsureDataDirectoryExists (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = _EnsureDataDirectoryExists (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFDocumentProvider : NSObject {

		public virtual NSData DataRepresentation (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSData ret = DataRepresentationWithError (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual bool SaveAnnotations (NSDictionary<NSString, NSObject> options, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotationsWithOptions (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFCache : NSObject {

		private static Class PSPDFCacheClass;
		public static Class CacheClass
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				var classPtr = Dlfcn.GetIntPtr (RTLD_MAIN_ONLY, "PSPDFCacheClass");

				if (classPtr != IntPtr.Zero) {
					PSPDFCacheClass = Runtime.GetINativeObject<Class> (classPtr, false);
					return PSPDFCacheClass;
				} else
					return PSPDFCacheClass = null;

			}
			set {
				PSPDFCacheClass = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFCacheClass");

				Marshal.WriteIntPtr (ptr, PSPDFCacheClass.Handle);
			}
		}
	}

	public partial class PSPDFPageInfo : NSObject {

		public static readonly nint PSPDFPageNull = nint.MaxValue;

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
				Marshal.StructureToPtr ((object) viewPoint, viewPointPtr, true);
				Marshal.StructureToPtr ((object) cropBox, cropBoxPtr, true);
				Marshal.StructureToPtr ((object) rotation, rotationPtr, true);
				Marshal.StructureToPtr ((object) bounds, boundsPtr, true);

				resultPtr = PSPDFConvertViewPointToPDFPoint (viewPointPtr, cropBoxPtr, rotationPtr, boundsPtr);
				result = (CGPoint)Marshal.PtrToStructure (resultPtr, typeof (CGPoint));
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
				result = (CGPoint)Marshal.PtrToStructure (resultPtr, typeof (CGPoint));
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
				result = (CGRect)Marshal.PtrToStructure (resultPtr, typeof (CGRect));
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
				result = (CGRect)Marshal.PtrToStructure (resultPtr, typeof (CGRect));
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

	public partial class PSPDFGlyph : NSObject {

		[DllImport ("__Internal", EntryPoint = "PSPDFRectsFromGlyphs")]
		private static extern IntPtr _RectsFromGlyphs (IntPtr glyphs, CGRect boundingBox, nint pageRotation);

		public static NSValue [] RectsFromGlyphs (PSPDFGlyph [] glyphs, CGRect boundingBox, nint pageRotation)
		{
			if (glyphs == null)
				return NSArray.ArrayFromHandle<NSValue> (_RectsFromGlyphs (IntPtr.Zero, boundingBox, pageRotation));

			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add(glyph);

			NSArray arry = NSArray.FromNSObjects (objs.ToArray ());
			return NSArray.ArrayFromHandle<NSValue> (_RectsFromGlyphs (arry.Handle, boundingBox, pageRotation));
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFBoundingBoxFromGlyphs")]
		private static extern CGRect _BoundingBoxFromGlyphs (IntPtr glyphs, nint pageRotation);

		public static CGRect BoundingBoxFromGlyphs (PSPDFGlyph [] glyphs, nint pageRotation)
		{
			var objs = new List<NSObject> ();

			foreach (var glyph in glyphs)
				objs.Add (glyph);

			NSArray arry = NSArray.FromNSObjects (objs.ToArray ());
			return _BoundingBoxFromGlyphs (arry.Handle, pageRotation);
		}
	}

	public partial class PSPDFImageInfo : NSObject {

		public virtual UIImage GetImage (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = GetImage (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual UIImage GetImageInRgbColorSpace (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = GetImageInRgbColorSpace (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFLibrary : NSObject {

		private static nuint PSPDFLibraryVersion;
		public static nuint LibraryVersion
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFLibraryVersion");
				PSPDFLibraryVersion = (nuint)Marshal.PtrToStructure(ptr, typeof (nuint));

				return PSPDFLibraryVersion;
			}
		}
	}

	public partial class PSPDFAnnotationManager : NSObject {

		public virtual bool SaveAnnotations (NSDictionary<NSString, NSObject> options, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotations (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFAnnotation : PSPDFModel {

		[DllImport ("__Internal", EntryPoint = "PSPDFStringFromAnnotationType")]
		private static extern IntPtr _StringFromAnnotationType (nuint annotationType);

		public static string StringFromAnnotationType (PSPDFAnnotationType annotationType)
		{
			return (string) Runtime.GetNSObject<NSString> (_StringFromAnnotationType ((nuint)(ulong)annotationType));
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFAnnotationTypeFromString")]
		private static extern nuint _AnnotationTypeFromString (IntPtr theString);

		public static PSPDFAnnotationType AnnotationTypeFromString (NSString theString)
		{
			return (PSPDFAnnotationType)(ulong)_AnnotationTypeFromString (theString.Handle);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFAnnotationRegisterOverrideClasses")]
		private static extern void _RegisterOverrideClasses (IntPtr annotationType, IntPtr document);

		public static void RegisterOverrideClasses (NSKeyedUnarchiver unarchiver, PSPDFDocument document)
		{
			_RegisterOverrideClasses (unarchiver.Handle, document.Handle);
		}
	}

	public partial class PSPDFFileAnnotationProvider : PSPDFContainerAnnotationProvider {

		public virtual bool TryLoadAnnotationsFromFile (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = TryLoadAnnotationsFromFile (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public new virtual bool SaveAnnotations (NSDictionary<NSString, NSObject> options, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = SaveAnnotations (options, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}

		public virtual NSDictionary<NSNumber, NSArray <PSPDFAnnotation>> LoadAnnotations (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				var ret = LoadAnnotations (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFStampAnnotation : PSPDFAnnotation {

		public virtual UIImage LoadImage (out CGAffineTransform transform, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				UIImage ret = LoadImage (out transform, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFSoundAnnotation : PSPDFAnnotation {

		public virtual bool LoadAttributesFromAudioFile (out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				bool ret = LoadAttributesFromAudioFile (val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFAnnotationGroupItem
	{
		[DllImport ("__Internal", EntryPoint = "PSPDFAnnotationStateVariantIdentifier")]
		private static extern IntPtr _VariantIdentifier (IntPtr state, IntPtr variant);

		public static string StateVariantIdentifier (string state, string variant)
		{
			return (string) Runtime.GetNSObject<NSString> (_VariantIdentifier (
				state == null ? IntPtr.Zero : new NSString (state).Handle, 
				variant == null ? IntPtr.Zero : new NSString (variant).Handle));
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFAnnotationStateFromStateVariantIdentifier")]
		private static extern IntPtr _StateFromStateVariantIdentifier (IntPtr state);

		public static string StateFromStateVariantIdentifier (string stateVariant)
		{
			return (string) Runtime.GetNSObject<NSString>(_StateFromStateVariantIdentifier (
				stateVariant == null ? IntPtr.Zero : new NSString (stateVariant).Handle));
		}
	}

	public partial class PSPDFMenuItem : UIMenuItem {
		private static bool PSPDFAllowImagesForMenuItems;
		public static bool AllowImagesForMenuItems
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				PSPDFAllowImagesForMenuItems = Convert.ToBoolean (Marshal.ReadByte(ptr));

				return PSPDFAllowImagesForMenuItems;
			}
			set {
				PSPDFAllowImagesForMenuItems = value;

				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");

				Marshal.WriteByte (ptr, Convert.ToByte (PSPDFAllowImagesForMenuItems));
			}
		}
	}

	public partial class PSPDFProcessor : NSObject {

		private static CGRect PSPDFPaperSizeA4;
		public static CGRect PaperSizeA4
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeA4");
				PSPDFPaperSizeA4 = (CGRect)Marshal.PtrToStructure(ptr, typeof (CGRect));

				return PSPDFPaperSizeA4;
			}
		}

		private static CGRect PSPDFPaperSizeLetter;
		public static CGRect PaperSizeLetter
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFPaperSizeLetter");
				PSPDFPaperSizeLetter = (CGRect)Marshal.PtrToStructure(ptr, typeof (CGRect));

				return PSPDFPaperSizeLetter;
			}
		}
	}

	public partial class PSPDFXFDFParser : NSObject {

		[DllImport ("__Internal", EntryPoint = "PSPDFConvertXFDFSoundEncodingToPDF")]
		private static extern IntPtr _ConvertXfdSoundEncodingToPdf (IntPtr encoding);

		public static string StateVariantIdentifier (string encoding)
		{
			return (string) Runtime.GetNSObject<NSString> (_ConvertXfdSoundEncodingToPdf (encoding == null ? IntPtr.Zero : new NSString (encoding).Handle));
		}
	}

	public partial class PSPDFAESCryptoDataProvider : NSObject {

		public PSPDFAESCryptoDataProvider (NSUrl url, [BlockProxy (typeof (ObjCRuntime.Trampolines.NIDFuncArity1V0))]Func<NSString> passphraseProvider, bool useLegacyFileFormatUrl = false)
		{
			Handle = useLegacyFileFormatUrl ? 
				InitWithLegacyFileFormatURL (url, passphraseProvider) :
				InitWithURL (url, passphraseProvider);
		}

		private static uint PSPDFDefaultPBKDFNumberOfRounds;
		public static uint DefaultPBKDFNumberOfRounds
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDefaultPBKDFNumberOfRounds");
				PSPDFDefaultPBKDFNumberOfRounds = (uint)Marshal.PtrToStructure(ptr, typeof (uint));

				return PSPDFDefaultPBKDFNumberOfRounds;
			}
		}
	}

	public partial class PSPDFSigner : NSObject {

		public virtual NSData SignHash (NSData hash, PSPDFSigningAlgorithm algorithm, out NSError error)
		{
			unsafe {
				IntPtr val;
				IntPtr val_addr = (IntPtr) ((IntPtr *) &val);

				NSData ret = SignHash (hash, algorithm, val_addr);
				error = (NSError) Runtime.GetNSObject (val);

				return ret;
			}
		}
	}

	public partial class PSPDFGalleryVideoItem : PSPDFGalleryItem {

		[DllImport ("__Internal", EntryPoint = "PSPDFGalleryVideoItemQualityFromString")]
		private static extern nuint _QualityFromString (IntPtr qualityString);

		public static PSPDFGalleryVideoItemQuality QualityFromString (string qualityString)
		{
			return (PSPDFGalleryVideoItemQuality)(ulong) _QualityFromString (new NSString (qualityString).Handle);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFGalleryVideoItemCoverModeFromString")]
		private static extern nuint _CoverModeFromString (IntPtr qualityString);

		public static PSPDFGalleryVideoItemCoverMode CoverModeFromString (string coverModeString)
		{
			return (PSPDFGalleryVideoItemCoverMode)(ulong) _CoverModeFromString (new NSString (coverModeString).Handle);
		}
	}

	public partial class PSPDFGalleryItem : NSObject {

		[DllImport ("__Internal", EntryPoint = "NSStringFromPSPDFGalleryItemContentState")]
		private static extern IntPtr _StringFromPSPDFGalleryItemContentState (nuint state);

		public static string StringFromPsPdfGalleryItemContentState (PSPDFGalleryItemContentState state)
		{
			return (string) Runtime.GetNSObject<NSString> (_StringFromPSPDFGalleryItemContentState ((nuint)(ulong)state));
		}
	}

	public partial class PSPDFInkAnnotation : PSPDFAbstractShapeAnnotation {

		public PSPDFInkAnnotation (List<NSValue[]> lines)
		{
			var arr = new NSMutableArray ();
			foreach (var line in lines)
				arr.Add (NSArray.FromNSObjects (line));
			Handle = InitWithLines (arr);
		}

		public List<NSValue[]> Lines { 
			get {
				var lines = new List<NSValue[]> ();
				var count = _Lines.Count;
				for (nuint i = 0; i < count; i++)
					lines.Add (NSArray.ArrayFromHandle<NSValue> (_Lines.ValueAt (i)));
				return lines;
			}
			set {
				if (value == null) {
					_Lines = null;
				} else {
					var arr = new NSMutableArray ();
					foreach (var line in value)
						arr.Add (NSArray.FromNSObjects (line));
					_Lines = arr;
				}
			}
		}
	}

	public partial class PSPDFCollectionReusableFilterView : UICollectionReusableView {

		public const UILayoutPriority CenterPriority = UILayoutPriority.DefaultHigh - 10;
		public static readonly nfloat DefaultMargin = 8;
	}

	public partial class PSPDFRSAKey : NSObject {

		public static PSPDFRSAKey FromKey (IntPtr key)
		{
			var handle = new PSPDFRSAKey ().InitWithKey (key);
			return Runtime.GetNSObject<PSPDFRSAKey> (handle);
		}
	}

	public partial class PSPDFSignatureDigest : NSObject {

		public static PSPDFSignatureDigest FromBio (IntPtr bio)
		{
			var handle = new PSPDFSignatureDigest ().InitWithBIO (bio);
			return Runtime.GetNSObject<PSPDFSignatureDigest> (handle);
		}
	}

	public partial class PSPDFX509 : NSObject {

		public static PSPDFX509 FromX509 (IntPtr x509)
		{
			var handle = new PSPDFX509 ().InitWithX509 (x509);
			return Runtime.GetNSObject<PSPDFX509> (handle);
		}
	}

	public partial class PSPDFStylusDriver {
		// HACK:
		// - (instancetype)initWithDelegate:(id<PSPDFStylusDriverDelegate>)delegate;
		[Export ("initWithDelegate:")]
		[CompilerGenerated]
		public PSPDFStylusDriver (IPSPDFStylusDriverDelegate del)
			: base (NSObjectFlag.Empty)
		{
			if (del == null)
				throw new ArgumentNullException ("del");
			IsDirectBinding = GetType ().Assembly == global::ApiDefinition.Messaging.this_assembly;
			if (IsDirectBinding) {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSend_IntPtr (this.Handle, Selector.GetHandle ("initWithDelegate:"), del.Handle), "initWithDelegate:");
			} else {
				InitializeHandle (global::ApiDefinition.Messaging.IntPtr_objc_msgSendSuper_IntPtr (this.SuperHandle, Selector.GetHandle ("initWithDelegate:"), del.Handle), "initWithDelegate:");
			}
		}
	}

	public static class PSPDFDrawingPointTools {

		private static PSPDFDrawingPoint PSPDFDrawingPointZero;
		public static PSPDFDrawingPoint Zero
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDrawingPointZero");
				PSPDFDrawingPointZero = (PSPDFDrawingPoint) Marshal.PtrToStructure (ptr, typeof (PSPDFDrawingPoint));

				return PSPDFDrawingPointZero;
			}
		}

		private static PSPDFDrawingPoint PSPDFDrawingPointNull;
		public static PSPDFDrawingPoint Null
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDrawingPointNull");
				PSPDFDrawingPointNull = (PSPDFDrawingPoint) Marshal.PtrToStructure (ptr, typeof (PSPDFDrawingPoint));

				return PSPDFDrawingPointNull;
			}
		}

		private static nfloat PSPDFDefaultIntensity;
		public static nfloat DefaultIntensity
		{
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (null, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFDefaultIntensity");
				PSPDFDefaultIntensity = (nfloat) Marshal.PtrToStructure (ptr, typeof (nfloat));

				return PSPDFDefaultIntensity;
			}
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFDrawingPointIsValid")]
		[return: MarshalAs (UnmanagedType.I1)]
		private static extern bool _IsValid (PSPDFDrawingPoint point);

		public static bool IsValid (PSPDFDrawingPoint point)
		{
			return _IsValid (point);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFDrawingPointIsEqualToPoint")]
		[return: MarshalAs (UnmanagedType.I1)]
		private static extern bool _IsEqualTo (PSPDFDrawingPoint point, PSPDFDrawingPoint otherPoint);

		public static bool IsEqualTo (PSPDFDrawingPoint point, PSPDFDrawingPoint otherPoint)
		{
			return _IsEqualTo (point, otherPoint);
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFDrawingPointToString")]
		private static extern IntPtr _PointToString (PSPDFDrawingPoint point);

		public static string PointToString (PSPDFDrawingPoint point)
		{
			return (string) Runtime.GetNSObject<NSString> (_PointToString (point));
		}

		[DllImport ("__Internal", EntryPoint = "PSPDFDrawingPointFromString")]
		private static extern PSPDFDrawingPoint _FromString (IntPtr str);

		public static PSPDFDrawingPoint FromString (string str)
		{
			return _FromString (str == null ? IntPtr.Zero : new NSString (str).Handle);
		}
	}
}
