using System;
using Foundation;
using ObjCRuntime;
using PSPDFKit.Core;

namespace PSPDFKit.UI {
	[Native]
	public enum PSPDFAnnotationStateManagerStylusMode : ulong {
		FromStylusManager,
		Direct,
		Stylus
	}

	public enum PSPDFAnnotationStringUI {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFAnnotationStringLink", PSPDFKitGlobal.LibraryPath)]
		Link,
		[Field ("PSPDFAnnotationStringHighlight", PSPDFKitGlobal.LibraryPath)]
		Highlight,
		[Field ("PSPDFAnnotationStringStrikeOut", PSPDFKitGlobal.LibraryPath)]
		StrikeOut,
		[Field ("PSPDFAnnotationStringUnderline", PSPDFKitGlobal.LibraryPath)]
		Underline,
		[Field ("PSPDFAnnotationStringSquiggly", PSPDFKitGlobal.LibraryPath)]
		Squiggly,
		[Field ("PSPDFAnnotationStringNote", PSPDFKitGlobal.LibraryPath)]
		Note,
		[Field ("PSPDFAnnotationStringFreeText", PSPDFKitGlobal.LibraryPath)]
		FreeText,
		[Field ("PSPDFAnnotationStringInk", PSPDFKitGlobal.LibraryPath)]
		Ink,
		[Field ("PSPDFAnnotationStringSquare", PSPDFKitGlobal.LibraryPath)]
		Square,
		[Field ("PSPDFAnnotationStringCircle", PSPDFKitGlobal.LibraryPath)]
		Circle,
		[Field ("PSPDFAnnotationStringLine", PSPDFKitGlobal.LibraryPath)]
		Line,
		[Field ("PSPDFAnnotationStringPolygon", PSPDFKitGlobal.LibraryPath)]
		Polygon,
		[Field ("PSPDFAnnotationStringPolyLine", PSPDFKitGlobal.LibraryPath)]
		PolyLine,
		[Field ("PSPDFAnnotationStringSignature", PSPDFKitGlobal.LibraryPath)]
		Signature,
		[Field ("PSPDFAnnotationStringStamp", PSPDFKitGlobal.LibraryPath)]
		Stamp,
		[Field ("PSPDFAnnotationStringEraser", PSPDFKitGlobal.LibraryPath)]
		Eraser,
		[Field ("PSPDFAnnotationStringSound", PSPDFKitGlobal.LibraryPath)]
		Sound,
		[Field ("PSPDFAnnotationStringImage", PSPDFKitGlobal.LibraryPath)]
		Image,
		[Field ("PSPDFAnnotationStringWidget", PSPDFKitGlobal.LibraryPath)]
		Widget,
		[Field ("PSPDFAnnotationStringFile", PSPDFKitGlobal.LibraryPath)]
		File,
		[Field ("PSPDFAnnotationStringRichMedia", PSPDFKitGlobal.LibraryPath)]
		RichMedia,
		[Field ("PSPDFAnnotationStringScreen", PSPDFKitGlobal.LibraryPath)]
		Screen,
		[Field ("PSPDFAnnotationStringCaret", PSPDFKitGlobal.LibraryPath)]
		Caret,
		[Field ("PSPDFAnnotationStringPopup", PSPDFKitGlobal.LibraryPath)]
		Popup,
		[Field ("PSPDFAnnotationStringWatermark", PSPDFKitGlobal.LibraryPath)]
		Watermark,
		[Field ("PSPDFAnnotationStringTrapNet", PSPDFKitGlobal.LibraryPath)]
		TrapNet,
		[Field ("PSPDFAnnotationString3D", PSPDFKitGlobal.LibraryPath)]
		_3D,
		[Field ("PSPDFAnnotationStringRedact", PSPDFKitGlobal.LibraryPath)]
		Redact,
		[Field ("PSPDFAnnotationStringInkVariantPen", PSPDFKitGlobal.LibraryPath)]
		InkVariantPen,
		[Field ("PSPDFAnnotationStringInkVariantHighlighter", PSPDFKitGlobal.LibraryPath)]
		InkVariantHighlighter,
		[Field ("PSPDFAnnotationStringLineVariantArrow", PSPDFKitGlobal.LibraryPath)]
		LineVariantArrow,
		[Field ("PSPDFAnnotationStringFreeTextVariantCallout", PSPDFKitGlobal.LibraryPath)]
		FreeTextVariantCallout,

		// UI
		[Field ("PSPDFAnnotationStringSelectionTool", PSPDFKitGlobal.LibraryPath)]
		SelectionTool,
		[Field ("PSPDFAnnotationStringSavedAnnotations", PSPDFKitGlobal.LibraryPath)]
		SavedAnnotations,
	}

	[Native]
	public enum PSPDFPresentationStyle : ulong {
		None,
		HalfModal
	}

	[Native]
	public enum PSPDFPersistentCloseButtonMode : ulong {
		None,
		Left,
		Right
	}

	[Native]
	[Flags]
	public enum PSPDFAppearanceMode : ulong {
		Default = 0,
		Sepia = 1 << 0,
		Night = 1 << 1,
		All = Default | Sepia | Night
	}

	[Native]
	public enum PSPDFBackButtonStyle : ulong {
		Flat,
		Modern
	}

	[Native]
	public enum PSPDFBookmarkIndicatorImageType : long {
		Large,
		Medium,
		Small
	}

	[Native]
	public enum PSPDFIdleTimerManagement : ulong {
		Manual,
		ExtendedTime,
		ExtendedTimeExternalScreenDisablesTimer
	}

	[Native]
	public enum PSPDFCollectionReusableFilterViewStyle : long {
		None,
		LightBlur,
		DarkBlur,
		ExtraLightBlur
	}

	[Native]
	public enum PSPDFColorSet : ulong {
		Default,
		DefaultWithTransparency,
		PageBackgrounds
	}

	public enum PSPDFActivityType {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFActivityTypeGoToPage", PSPDFKitGlobal.LibraryPath)]
		GoToPage,
		[Field ("PSPDFActivityTypeSearch", PSPDFKitGlobal.LibraryPath)]
		Search,
		[Field ("PSPDFActivityTypeOutline", PSPDFKitGlobal.LibraryPath)]
		Outline,
		[Field ("PSPDFActivityTypeBookmarks", PSPDFKitGlobal.LibraryPath)]
		Bookmarks,
		[Field ("PSPDFActivityTypeOpenIn", PSPDFKitGlobal.LibraryPath)]
		OpenIn,
		// TODO: This needs a generator fix
		//[Field ("UIActivityTypePostToFacebook", Constants.UIKitLibrary)]
		//PostToFacebook,
		//[Field ("UIActivityTypePostToTwitter", Constants.UIKitLibrary)]
		//PostToTwitter,
		//[Field ("UIActivityTypePostToWeibo", Constants.UIKitLibrary)]
		//PostToWeibo,
		//[Field ("UIActivityTypeMessage", Constants.UIKitLibrary)]
		//Message,
		//[Field ("UIActivityTypeMail", Constants.UIKitLibrary)]
		//Mail,
		//[Field ("UIActivityTypePrint", Constants.UIKitLibrary)]
		//Print,
		//[Field ("UIActivityTypeCopyToPasteboard", Constants.UIKitLibrary)]
		//CopyToPasteboard,
		//[Field ("UIActivityTypeAssignToContact", Constants.UIKitLibrary)]
		//AssignToContact,
		//[Field ("UIActivityTypeSaveToCameraRoll", Constants.UIKitLibrary)]
		//SaveToCameraRoll,
		//[Field ("UIActivityTypeAddToReadingList", Constants.UIKitLibrary)]
		//AddToReadingList,
		//[Field ("UIActivityTypePostToFlickr", Constants.UIKitLibrary)]
		//PostToFlickr,
		//[Field ("UIActivityTypePostToVimeo", Constants.UIKitLibrary)]
		//PostToVimeo,
		//[Field ("UIActivityTypePostToTencentWeibo", Constants.UIKitLibrary)]
		//PostToTencentWeibo,
		//[Field ("UIActivityTypeAirDrop", Constants.UIKitLibrary)]
		//AirDrop,
		//[Field ("UIActivityTypeOpenInIBooks", Constants.UIKitLibrary)]
		//OpenInIBooks,
		//[Field ("UIActivityTypeMarkupAsPDF", Constants.UIKitLibrary)]
		//MarkupAsPdf,
	}

	[Native]
	public enum PSPDFPageTransition : ulong {
		ScrollPerSpread,
		ScrollContinuous,
		Curl,
		ScrollPerPage = ScrollPerSpread
	}

	[Native]
	public enum PSPDFPageMode : ulong {
		Single,
		Double,
		Automatic
	}

	[Native]
	public enum PSPDFScrollDirection : ulong {
		Horizontal,
		Vertical
	}

	[Native]
	public enum PSPDFViewMode : ulong {
		Document,
		Thumbnails,
		DocumentEditor
	}

	[Native]
	public enum PSPDFLinkAction : ulong {
		None,
		AlertView,
		OpenSafari,
		InlineBrowser,
		InlineWebViewController
	}

	[Native]
	public enum PSPDFTextSelectionMode : ulong {
		Regular,
		Simple
	}

	[Native]
	public enum PSPDFDrawCreateMode : ulong {
		Separate,
		MergeIfPossible,
		Automatic,
	}

	[Native]
	[Flags]
	public enum PSPDFTextSelectionMenuAction : ulong {
		None = 0,
		Search = 1 << 0,
		Define = 1 << 1,
		Wikipedia = 1 << 2,
		Speak = 1 << 3,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFThumbnailBarMode : ulong {
		None,
		ScrubberBar,
		Scrollable
	}

	[Native]
	public enum PSPDFScrubberBarType : ulong {
		Horizontal,
		VerticalLeft,
		VerticalRight
	}

	[Native]
	public enum PSPDFThumbnailGrouping : ulong {
		Automatic,
		Never,
		Always
	}

	[Native]
	public enum PSPDFUserInterfaceViewMode : ulong {
		Always,
		Automatic,
		AutomaticNoFirstLastPage,
		Never
	}

	[Native]
	public enum PSPDFUserInterfaceViewAnimation : ulong {
		None,
		Fade,
		Slide
	}

	[Native]
	public enum PSPDFSearchMode : ulong {
		Modal,
		Inline
	}

	[Native]
	public enum PSPDFRenderStatusViewPosition : ulong {
		Top,
		Centered
	}

	[Native]
	public enum PSPDFTapAction : ulong {
		None,
		Zoom,
		SmartZoom
	}

	[Native]
	public enum PSPDFAdaptiveConditional : ulong {
		No,
		Yes,
		Adaptive
	}

	[Native]
	public enum PSPDFScrollInsetAdjustment : ulong {
		None,
		FixedElements,
		AllElements
	}

	[Native]
	public enum PSPDFPageBookmarkIndicatorMode : ulong {
		Off,
		AlwaysOn,
		OnWhenBookmarked
	}

	[Native]
	public enum PSPDFSoundAnnotationPlayerStyle : ulong {
		Inline,
		Bottom
	}

	[Native]
	public enum PSPDFSignatureSavingStrategy : ulong {
		AlwaysSave,
		NeverSave,
		SaveIfSelected
	}

	[Native]
	public enum PSPDFSignatureCertificateSelectionMode : ulong {
		Always,
		Never,
		IfAvailable
	}

	[Native]
	public enum PSPDFConfigurationSpreadFitting : ulong {
		Fit,
		Fill,
		Adaptive
	}

	[Native]
	[Flags]
	public enum PSPDFSignatureBiometricPropertiesOption : ulong {
		None = 0,
		Pressue = 1 << 0,
		TimePoints = 1 << 1,
		TouchRadius = 1 << 2,
		InputMethod = 1 << 3,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFControllerState : ulong {
		Empty,
		Loading,
		Default,
		Error,
		Locked
	}

	public enum PSPDFDocumentAction {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFDocumentActionPrint", PSPDFKitGlobal.LibraryPath)]
		Print,
		[Field ("PSPDFDocumentActionEmail", PSPDFKitGlobal.LibraryPath)]
		Email,
		[Field ("PSPDFDocumentActionOpenIn", PSPDFKitGlobal.LibraryPath)]
		OpenIn,
		[Field ("PSPDFDocumentActionMessage", PSPDFKitGlobal.LibraryPath)]
		Message,
	}

	public enum PSPDFDocumentInfoOption {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFDocumentInfoOptionOutline", PSPDFKitGlobal.LibraryPath)]
		Outline,
		[Field ("PSPDFDocumentInfoOptionBookmarks", PSPDFKitGlobal.LibraryPath)]
		Bookmarks,
		[Field ("PSPDFDocumentInfoOptionAnnotations", PSPDFKitGlobal.LibraryPath)]
		Annotations,
		[Field ("PSPDFDocumentInfoOptionEmbeddedFiles", PSPDFKitGlobal.LibraryPath)]
		EmbeddedFiles,
	}

	[Native]
	[Flags]
	public enum PSPDFDocumentSharingOptions : ulong {
		None = 0,
		CurrentPageOnly = 1 << 0,
		PageRange = 1 << 1,
		AllPages = 1 << 2,
		AnnotatedPages = 1 << 4,
		EmbedAnnotations = 1 << 8,
		FlattenAnnotations = 1 << 9,
		AnnotationsSummary = 1 << 10,
		RemoveAnnotations = 1 << 11,
		FlattenAnnotationsForPrint = 1 << 12,
		OriginalFile = 1 << 16,
		Image = 1 << 17
	}

	[Native]
	public enum PSPDFDocumentViewLayoutSpreadMode : long {
		Single,
		Double,
		Book
	}

	[Native]
	public enum PSPDFDocumentViewLayoutPageMode : long {
		Single,
		Leading,
		Trailing,
		Center
	}

	[Native]
	[Flags]
	public enum PSPDFDragType : ulong {
		None = 0,
		Text = 1 << 0,
		Image = 1 << 1,
		All = ulong.MaxValue
	}

	[Native]
	[Flags]
	public enum PSPDFDropType : ulong {
		None = 0,
		Text = 1 << 0,
		Image = 1 << 1,
		Pdf = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	[Flags]
	public enum PSPDFDropTarget : ulong {
		None = 0,
		ExternalApp = 1 << 0,
		SamePage = 1 << 1,
		OtherPages = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFDrawViewInputMode : long {
		Draw,
		Erase
	}

	[Native]
	[Flags]
	public enum PSPDFFlexibleToolbarPosition : ulong {
		None = 0,
		InTopBar = 1 << 0,
		Left = 1 << 1,
		Right = 1 << 2,
		sVertical = Left | Right,
		sAll = InTopBar | sVertical
	}

	[Native]
	public enum PSPDFToolbarGroupButtonIndicatorPosition : long {
		None = 0,
		BottomLeft,
		BottomRight
	}

	[Native]
	public enum PSPDFSubmitFormActionFormat : ulong {
		Fdf,
		Xfdf,
		Html,
		Pdf
	}

	[Native]
	public enum PSPDFGalleryContainerViewContentState : ulong {
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFGalleryContainerViewPresentationMode : ulong {
		Embedded,
		Fullscreen
	}

	[Native]
	public enum PSPDFGalleryItemContentState : ulong {
		Waiting,
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFGalleryVideoItemQuality : ulong {
		Unknown,
		_240p,
		_360p,
		_720p,
		_1080p
	}

	[Native]
	public enum PSPDFGalleryVideoItemCoverMode : ulong {
		None,
		Preview,
		Image,
		Clear
	}

	[Native]
	public enum PSPDFGalleryViewControllerState : ulong {
		Idle,
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFImageQuality : ulong {
		Low = 1 << 0,
		Medium = 1 << 1,
		High = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFLabelStyle : ulong {
		Flat,
		Modern
	}

	[Native]
	public enum PSPDFMediaPlayerControlStyle : ulong {
		None,
		Default
	}

	[Native]
	public enum PSPDFMediaPlayerControllerContentState : ulong {
		Idle,
		Loading,
		Ready,
		Error
	}

	[Native]
	public enum PSPDFMediaPlayerCoverMode : ulong {
		Preview,
		Custom,
		Hidden,
		Clear
	}

	public enum PSPDFTextMenu {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFTextMenuCopy", PSPDFKitGlobal.LibraryPath)]
		Copy,
		[Field ("PSPDFTextMenuDefine", PSPDFKitGlobal.LibraryPath)]
		Define,
		[Field ("PSPDFTextMenuSearch", PSPDFKitGlobal.LibraryPath)]
		Search,
		[Field ("PSPDFTextMenuWikipedia", PSPDFKitGlobal.LibraryPath)]
		Wikipedia,
		[Field ("PSPDFTextMenuCreateLink", PSPDFKitGlobal.LibraryPath)]
		CreateLink,
		[Field ("PSPDFTextMenuSpeak", PSPDFKitGlobal.LibraryPath)]
		Speak,
		[Field ("PSPDFTextMenuPause", PSPDFKitGlobal.LibraryPath)]
		Pause
	}

	public enum PSPDFAnnotationMenu {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFAnnotationMenuCancel", PSPDFKitGlobal.LibraryPath)]
		Cancel,
		[Field ("PSPDFAnnotationMenuNote", PSPDFKitGlobal.LibraryPath)]
		Note,
		[Field ("PSPDFAnnotationMenuGroup", PSPDFKitGlobal.LibraryPath)]
		Group,
		[Field ("PSPDFAnnotationMenuUngroup", PSPDFKitGlobal.LibraryPath)]
		Ungroup,
		[Field ("PSPDFAnnotationMenuSave", PSPDFKitGlobal.LibraryPath)]
		Save,
		[Field ("PSPDFAnnotationMenuRemove", PSPDFKitGlobal.LibraryPath)]
		Remove,
		[Field ("PSPDFAnnotationMenuCopy", PSPDFKitGlobal.LibraryPath)]
		Copy,
		[Field ("PSPDFAnnotationMenuPaste", PSPDFKitGlobal.LibraryPath)]
		Paste,
		[Field ("PSPDFAnnotationMenuMerge", PSPDFKitGlobal.LibraryPath)]
		Merge,
		[Field ("PSPDFAnnotationMenuPreviewFile", PSPDFKitGlobal.LibraryPath)]
		PreviewFile,
		[Field ("PSPDFAnnotationMenuInspector", PSPDFKitGlobal.LibraryPath)]
		Inspector,
		[Field ("PSPDFAnnotationMenuStyle", PSPDFKitGlobal.LibraryPath)]
		Style,
		[Field ("PSPDFAnnotationMenuColor", PSPDFKitGlobal.LibraryPath)]
		Color,
		[Field ("PSPDFAnnotationMenuFillColor", PSPDFKitGlobal.LibraryPath)]
		FillColor,
		[Field ("PSPDFAnnotationMenuOpacity", PSPDFKitGlobal.LibraryPath)]
		Opacity,
		[Field ("PSPDFAnnotationMenuCustomColor", PSPDFKitGlobal.LibraryPath)]
		CustomColor,
		[Field ("PSPDFAnnotationMenuHighlightType", PSPDFKitGlobal.LibraryPath)]
		HighlightType,
		[Field ("PSPDFAnnotationMenuHighlight", PSPDFKitGlobal.LibraryPath)]
		Highlight,
		[Field ("PSPDFAnnotationMenuUnderline", PSPDFKitGlobal.LibraryPath)]
		Underline,
		[Field ("PSPDFAnnotationMenuStrikeout", PSPDFKitGlobal.LibraryPath)]
		Strikeout,
		[Field ("PSPDFAnnotationMenuSquiggle", PSPDFKitGlobal.LibraryPath)]
		Squiggle,
		[Field ("PSPDFAnnotationMenuThickness", PSPDFKitGlobal.LibraryPath)]
		Thickness,
		[Field ("PSPDFAnnotationMenuPlay", PSPDFKitGlobal.LibraryPath)]
		Play,
		[Field ("PSPDFAnnotationMenuPause", PSPDFKitGlobal.LibraryPath)]
		Pause,
		[Field ("PSPDFAnnotationMenuPauseRecording", PSPDFKitGlobal.LibraryPath)]
		PauseRecording,
		[Field ("PSPDFAnnotationMenuContinueRecording", PSPDFKitGlobal.LibraryPath)]
		ContinueRecording,
		[Field ("PSPDFAnnotationMenuFinishRecording", PSPDFKitGlobal.LibraryPath)]
		FinishRecording,
		[Field ("PSPDFAnnotationMenuEdit", PSPDFKitGlobal.LibraryPath)]
		Edit,
		[Field ("PSPDFAnnotationMenuSize", PSPDFKitGlobal.LibraryPath)]
		Size,
		[Field ("PSPDFAnnotationMenuFont", PSPDFKitGlobal.LibraryPath)]
		Font,
		[Field ("PSPDFAnnotationMenuAlignment", PSPDFKitGlobal.LibraryPath)]
		Alignment,
		[Field ("PSPDFAnnotationMenuAlignmentLeft", PSPDFKitGlobal.LibraryPath)]
		AlignmentLeft,
		[Field ("PSPDFAnnotationMenuAlignmentCenter", PSPDFKitGlobal.LibraryPath)]
		AlignmentCenter,
		[Field ("PSPDFAnnotationMenuAlignmentRight", PSPDFKitGlobal.LibraryPath)]
		AlignmentRight,
		[Field ("PSPDFAnnotationMenuFitToText", PSPDFKitGlobal.LibraryPath)]
		FitToText,
		[Field ("PSPDFAnnotationMenuLineStart", PSPDFKitGlobal.LibraryPath)]
		LineStart,
		[Field ("PSPDFAnnotationMenuLineEnd", PSPDFKitGlobal.LibraryPath)]
		LineEnd,
		[Field ("PSPDFAnnotationMenuLineTypeNone", PSPDFKitGlobal.LibraryPath)]
		LineTypeNone,
		[Field ("PSPDFAnnotationMenuLineTypeSquare", PSPDFKitGlobal.LibraryPath)]
		LineTypeSquare,
		[Field ("PSPDFAnnotationMenuLineTypeCircle", PSPDFKitGlobal.LibraryPath)]
		LineTypeCircle,
		[Field ("PSPDFAnnotationMenuLineTypeDiamond", PSPDFKitGlobal.LibraryPath)]
		LineTypeDiamond,
		[Field ("PSPDFAnnotationMenuLineTypeOpenArrow", PSPDFKitGlobal.LibraryPath)]
		LineTypeOpenArrow,
		[Field ("PSPDFAnnotationMenuLineTypeClosedArrow", PSPDFKitGlobal.LibraryPath)]
		LineTypeClosedArrow,
		[Field ("PSPDFAnnotationMenuLineTypeButt", PSPDFKitGlobal.LibraryPath)]
		LineTypeButt,
		[Field ("PSPDFAnnotationMenuLineTypeReverseOpenArrow", PSPDFKitGlobal.LibraryPath)]
		LineTypeReverseOpenArrow,
		[Field ("PSPDFAnnotationMenuLineTypeReverseClosedArrow", PSPDFKitGlobal.LibraryPath)]
		LineTypeReverseClosedArrow,
		[Field ("PSPDFAnnotationMenuLineTypeSlash", PSPDFKitGlobal.LibraryPath)]
		LineTypeSlash
	}

	[Native]
	public enum PSPDFPrintMode : ulong {
		Interactive,
		ChoosePrinterOnly,
		PrintDirect
	}

	[Native]
	public enum PSPDFResizableViewOuterKnob : ulong {
		Unknown,
		TopLeft,
		TopMiddle,
		TopRight,
		MiddleLeft,
		MiddleRight,
		BottomLeft,
		BottomMiddle,
		BottomRight
	}

	[Native]
	public enum PSPDFKnobType : ulong {
		Outer,
		Inner
	}

	[Native]
	public enum PSPDFResizableViewMode : ulong {
		Idle,
		Move,
		Resize,
		Adjust
	}

	[Native]
	public enum PSPDFScrollPerSpreadLayoutContentScale : long {
		AspectFit = 0,
		AspectFillWidth,
		AspectFillHeight
	}

	[Native]
	public enum PSPDFSearchStatus : long {
		Idle,
		Active,
		Finished,
		FinishedNoText,
		Cancelled
	}

	[Native]
	public enum PSPDFSearchBarPinning : ulong {
		Auto,
		Top,
		None
	}

	[Native]
	public enum PSPDFSearchResultScope : long {
		PageRange,
		Document
	}

	[Native]
	public enum PSPDFSelectableCollectionViewCellStyle : ulong {
		None,
		Checkmark,
		Border,
		DimmedBackgroundWithCheckmark,
	}

	[Native]
	[Flags]
	public enum PSPDFSettingsOptions : ulong {
		None = 0,
		ScrollDirection = 1 << 0,
		PageTransition = 1 << 1,
		Appearance = 1 << 2,
		Brightness = 1 << 3,
		PageMode = 1 << 4,
		Default = ScrollDirection | PageTransition | Appearance | Brightness,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFStackViewLayoutDirection : long {
		Horizontal,
		Vertical
	}

	[Native]
	public enum PSPDFStatefulViewState : ulong {
		Loading,
		Empty,
		Finished
	}

	[Native]
	public enum PSPDFStatusHUDStyle : ulong {
		None = 0,
		Clear,
		Black
	}

	[Native]
	public enum PSPDFStylusConnectionStatus : ulong {
		Off,
		Scanning,
		Pairing,
		Connected,
		Disconnected
	}

	public enum PSPDFStylusButtonAction {
		[DefaultEnumValue]
		[Field (null)]
		Null,
		[Field ("PSPDFStylusButtonActionUndo", PSPDFKitGlobal.LibraryPath)]
		Undo,
		[Field ("PSPDFStylusButtonActionRedo", PSPDFKitGlobal.LibraryPath)]
		Redo,
		[Field ("PSPDFStylusButtonActionInk", PSPDFKitGlobal.LibraryPath)]
		Ink,
		[Field ("PSPDFStylusButtonActionEraser", PSPDFKitGlobal.LibraryPath)]
		Eraser,
	}

	[Native]
	public enum PSPDFStylusTouchClassification : long {
		UnknownDisconnected,
		Unknown,
		Finger,
		Palm,
		Pen,
		Eraser
	}

	[Native]
	public enum PSPDFTabbedBarStyle : ulong {
		Light,
		Dark
	}

	[Native]
	public enum PSPDFTabbedViewControllerBarHidingMode : long {
		Automatic,
		Show,
		Hide
	}

	[Native]
	public enum PSPDFTabbedViewControllerCloseMode : long {
		OnlySelectedTab,
		AllTabs,
		SizeDependent,
		Disabled
	}

	[Native]
	public enum PSPDFThumbnailFlowLayoutAttributesType : long {
		Single = PSPDFDocumentViewLayoutPageMode.Single,
		Leading = PSPDFDocumentViewLayoutPageMode.Leading,
		Trailing = PSPDFDocumentViewLayoutPageMode.Trailing
	}

	[Native]
	public enum PSPDFThumbnailFlowLayoutLineAlignment : long {
		Left,
		Center,
		Right,
		PageBinding
	}

	[Native]
	[Flags]
	public enum PSPDFToolbarButtonControlEvents : ulong {
		Tick = 1 << 24,
		TouchUpInsideIfNotTicking = 1 << 25
	}

	[Native]
	[Flags]
	public enum PSPDFWebViewControllerAvailableActions : ulong {
		None = 0,
		OpenInSafari = 1 << 0,
		MailLink = 1 << 1,
		CopyLink = 1 << 2,
		Print = 1 << 3,
		StopReload = 1 << 4,
		Back = 1 << 5,
		Forward = 1 << 6,
		Facebook = 1 << 7,
		Twitter = 1 << 8,
		Message = 1 << 9,
		OpenInChrome = 1 << 10,
		All = 16777215
	}

	[Native]
	public enum PSPDFSpreadScrollPosition : long {
		Start,
		Middle,
		End,
	}

	[Native]
	//[ErrorDomain ("PSPDFGalleryManifestErrorDomain")] TODO: enable once generator bug is fixed
	public enum PSPDFGalleryManifestErrorCode : long {
		NoDataSourceProvided
	}

	public enum PSPDFThumbnailViewFilter {
		[DefaultEnumValue]
		[Field ("PSPDFThumbnailViewFilterShowAll", PSPDFKitGlobal.LibraryPath)]
		ShowAll,
		[Field ("PSPDFThumbnailViewFilterBookmarks", PSPDFKitGlobal.LibraryPath)]
		Bookmarks,
		[Field ("PSPDFThumbnailViewFilterAnnotations", PSPDFKitGlobal.LibraryPath)]
		Annotations,
	}
}
