using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using Foundation;
using ObjCRuntime;
using PSPDFKit.Model;
using UIKit;

namespace PSPDFKit.UI {

	public partial class PSPDFPresentationOptions : DictionaryContainer {

		public PSPDFPresentationStyle? Style {
			get => (PSPDFPresentationStyle) (ulong) GetNUIntValue (PSPDFPresentationKeys.StyleKey);
			set => SetNumberValue (PSPDFPresentationKeys.StyleKey, (nuint) (ulong) value);
		}

		public PSPDFPersistentCloseButtonMode? PersistentCloseButton {
			get => (PSPDFPersistentCloseButtonMode) (ulong) GetNUIntValue (PSPDFPresentationKeys.PersistentCloseButtonKey);
			set => SetNumberValue (PSPDFPresentationKeys.PersistentCloseButtonKey, (nuint) (ulong) value);
		}

		public UIPopoverArrowDirection? PopoverArrowDirections {
			get => (UIPopoverArrowDirection) (ulong) GetNUIntValue (PSPDFPresentationKeys.PopoverArrowDirectionsKey);
			set => SetNumberValue (PSPDFPresentationKeys.PopoverArrowDirectionsKey, (nuint) (ulong) value);
		}
	}

	public partial class PSPDFAnnotationTableViewController {

		public PSPDFAnnotationStringUI [] VisibleAnnotationTypes {
			get => WeakVisibleAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakVisibleAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFAnnotationTableViewController {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypesUI {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFAnnotationToolbar {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFBaseViewController {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFSafePreferredInterfaceOrientation")]
		static extern nint _SafePreferredInterfaceOrientation (nint requested, nuint supported);

		public static UIInterfaceOrientation GetSafePreferredInterfaceOrientation (UIInterfaceOrientation requested, nuint supported)
			=> (UIInterfaceOrientation) (long) _SafePreferredInterfaceOrientation ((nint) (long) requested, supported);
	}

	public partial class PSPDFConfigurationBuilder {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
			set => WeakEditableAnnotationTypes = value == null ? null : new NSSet<NSString> (value.Select (x => x.GetConstant ())?.ToArray ());
		}
	}

	public partial class PSPDFConfiguration {

		public PSPDFAnnotationStringUI [] EditableAnnotationTypes {
			get => WeakEditableAnnotationTypes?.ToArray ()?.Select (x => PSPDFAnnotationStringUIExtensions.GetValue (x))?.ToArray ();
		}
	}

	public partial class PSPDFDocumentEditorViewController {

		public Type CellType {
			get => CellClass == null ? null : Class.Lookup (CellClass);
			set => CellClass = value == null ? throw new ArgumentNullException (nameof (value)) : new Class (value);
		}
	}

	public partial class PSPDFDocumentInfoCoordinator {

		public delegate void PSPDFDocumentInfoCoordinatorDidCreateControllerHandler (UIViewController controller, PSPDFDocumentInfoOption option);

		public PSPDFDocumentInfoCoordinatorDidCreateControllerHandler DidCreateControllerHandler {
			get {
				var handler = _DidCreateControllerHandler;
				if (handler == null)
					return null;
				return (controller, option) => handler (controller, option.GetConstant ());
			}
			set {
				if (value == null)
					_DidCreateControllerHandler = null;
				else
					_DidCreateControllerHandler = (controller, option) => value (controller, PSPDFDocumentInfoOptionExtensions.GetValue (option));
			}
		}

		public PSPDFDocumentInfoOption [] AvailableControllerOptions {
			get => _AvailableControllerOptions?.Select (x => PSPDFDocumentInfoOptionExtensions.GetValue (x))?.ToArray ();
			set => _AvailableControllerOptions = value?.Select (x => x.GetConstant ())?.ToArray ();
		}
	}

	public partial class PSPDFFlexibleToolbarContainer {

		[DllImport (PSPDFKitGlobal.LibraryPath, EntryPoint = "PSPDFSystemBarForResponder")]
		static extern IntPtr SystemBarForResponder (IntPtr responder);

		[Advice ("'T' must be 'UIToolbar' or 'UINavigationBar' or 'UIView'.")]
		public static T GetSystemBar<T> (UIResponder responder) where T : UIView
		{
			var t = typeof (T);
			if (t == typeof (UIToolbar) || t == typeof (UINavigationBar) || t == typeof (UIView))
				return Runtime.GetNSObject<T> (SystemBarForResponder (responder.Handle));
			throw new InvalidOperationException ("'T' must be 'UIToolbar' or 'UINavigationBar' or 'UIView'.");
		}
	}

	public partial class PSPDFMenuItem {

		public static bool AllowImagesForMenuItems {
			get {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");
				return Convert.ToBoolean (Marshal.ReadByte (ptr));
			}
			set {
				IntPtr RTLD_MAIN_ONLY = Dlfcn.dlopen (PSPDFKitGlobal.DlPath, 0);
				IntPtr ptr = Dlfcn.dlsym (RTLD_MAIN_ONLY, "PSPDFAllowImagesForMenuItems");
				Marshal.WriteByte (ptr, Convert.ToByte (value));
			}
		}
	}

	public partial class PSPDFStylusViewController {

		public Type SelectedDriverType {
			get => SelectedDriverClass == null ? null : Class.Lookup (SelectedDriverClass);
			set => SelectedDriverClass = value == null ? throw new ArgumentNullException (nameof (value)) : new Class (value);
		}
	}

	public partial class PSPDFThumbnailViewController {

		public PSPDFThumbnailViewFilter [] FilterOptions {
			get => WeakFilterOptions?.Select (x => PSPDFThumbnailViewFilterExtensions.GetValue (x))?.ToArray ();
			set => WeakFilterOptions = value?.Select (x => x.GetConstant ())?.ToArray ();
		}

		public PSPDFThumbnailViewFilter ActiveFilter {
			get => PSPDFThumbnailViewFilterExtensions.GetValue (WeakActiveFilter);
			set => WeakActiveFilter = value.GetConstant ();
		}

		public Type CellClassType {
			get => CellClass == null ? null : Class.Lookup (CellClass);
			set => CellClass = value == null ? throw new ArgumentNullException (nameof (value)) : new Class (value);
		}
	}

	public partial class PSPDFDrawView {

		public void ContinueDrawing (PSPDFDrawingPoint [] locations, PSPDFDrawingPoint [] predictedLocationsPoints)
		{
			ContinueDrawing (locations.Select (l => (null as NSValue).FromPSPDFDrawingPoint (l)).ToArray (), predictedLocationsPoints.Select (p => (null as NSValue).FromPSPDFDrawingPoint (p)).ToArray ());
		}

		public void EraseAt (PSPDFDrawingPoint [] locations)
		{
			EraseAt (locations.Select (l => (null as NSValue).FromPSPDFDrawingPoint (l)).ToArray ());
		}
	}

	public partial class PSPDFDocumentSharingConfigurationBuilder {

		public UIActivity [] ApplicationActivitiesAsObjects {
			get => ApplicationActivities?.OfType<UIActivity> ()?.ToArray ();
			set => ApplicationActivities = value?.OfType<NSObject> ()?.ToArray ();
		}

		public PSPDFActivityType [] ApplicationActivitiesAsTypes {
			get => ApplicationActivities?.Select (x => PSPDFActivityTypeExtensions.GetValue ((NSString) x))?.ToArray ();
			set => ApplicationActivities = value?.Select (x => (NSObject) x.GetConstant ())?.ToArray ();
		}

		public PSPDFActivityType [] ExcludedActivityTypes {
			get => WeakExcludedActivityTypes?.Select (x => PSPDFActivityTypeExtensions.GetValue (x))?.ToArray ();
			set => WeakExcludedActivityTypes = value?.Select (x => x.GetConstant ())?.ToArray ();
		}
	}

	public partial class PSPDFDocumentSharingConfiguration {

		public UIActivity[] ApplicationActivitiesAsObjects {
			get => ApplicationActivities?.OfType<UIActivity> ()?.ToArray ();
		}

		public PSPDFActivityType [] ApplicationActivitiesAsTypes {
			get => ApplicationActivities?.Select (x => PSPDFActivityTypeExtensions.GetValue ((NSString) x))?.ToArray ();
		}

		public PSPDFActivityType [] ExcludedActivityTypes {
			get => WeakExcludedActivityTypes?.Select (x => PSPDFActivityTypeExtensions.GetValue (x))?.ToArray ();
		}
	}
}
