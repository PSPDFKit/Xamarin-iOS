using System;

#if __UNIFIED__
using ObjCRuntime;
#else
using MonoTouch.ObjCRuntime;
#endif

namespace PSPDFKit
{
	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFFeatureMask : ulong {
#else
	public enum PSPDFFeatureMask : uint {
#endif
		None = 0,
		PDFViewer = 1 << 0,
		TextSelection = 1 << 1,
		StrongEncryption = 1 << 2,
		PDFCreation = 1 << 3,
		AnnotationEditing = 1 << 4,
		AcroForms = 1 << 5,
		IndexedFTS = 1 << 6,
		DigitalSignatures = 1 << 7,
		RequireSignedSource = 1 << 8,

#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFLogLevelMask : ulong {
#else
	public enum PSPDFLogLevelMask : uint {
#endif
		Nothing = 0,
		Error = 1 << 0,
		Warning = 1 << 1,
		Info = 1 << 2,
		Verbose = 1 << 3,
		ExtraVerbose = 1 << 4,
#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFErrorCode : long {
#else
	public enum PSPDFErrorCode : int {
#endif
		OutOfMemory = 10,
		PageInvalid = 100,
		DocumentContainsNoPages = 101,
		UnableToOpenPDF = 200,
		UnableToGetPageReference = 210,
		UnableToGetStream = 211,
		DocumentProviderNotSet = 212,
		PageRenderSizeIsEmpty = 220,
		PageRenderClipRectTooLarge = 230,
		PageRenderGraphicsContextNil = 240,
		DocumentLocked = 300,
		FailedToLoadAnnotations = 400,
		FailedToWriteAnnotations = 410,
		WriteAnnotationsCancelled = 411,
		CannotEmbedAnnotations = 420,
		FailedToLoadBookmarks = 450,
		FailedToSaveBookmarks = 460,
		OutlineParser = 500,
		UnableToConvertToDataRepresentation = 600,
		RemoveCacheError = 700,
		FailedToConvertToPDF = 800,
		FailedToGeneratePDFInvalidArguments = 810,
		FailedToGeneratePDFDocumentInvalid = 820,
		FailedToGeneratePDFCouldNotCreateContext = 830,
		FailedToUpdatePageObject = 850,
		MicPermissionNotGranted = 900,
		XFDFParserLackingInputStream = 1000,
		XFDFParserAlreadyCompleted = 1010,
		XFDFParserAlreadyStarted = 1020,
		XMLParserError = 1100,
		XFDFWriterCannotWriteToStream = 1200,
		FDFWriterCannotWriteToStream = 1250,
		SoundEncoderInvalidInput = 1300,
		GalleryInvalidManifest = 1400,
		GalleryUnknownItem = 1450,
		InvalidRemoteContent = 1500,
		FormValidationError = 5000,
		ImageProcessorInvalidImage = 6000,
		OpenInNoApplicationsFound = 7000,
		CodeMessageNotSent = 7100,
		CodeFeatureNotEnabled = 100000,
		SecurityNoPermission = 200000,

#if __UNIFIED__
		Unknown = long.MaxValue
#else
		Unknown = int.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPageTransition : ulong {
#else
	public enum PSPDFPageTransition : uint {
#endif
		ScrollPerPage,
		ScrollContinuous,
		Curl
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPageMode : ulong {
#else
	public enum PSPDFPageMode : uint {
#endif
		Single,
		Double,
		Automatic
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFScrollDirection : ulong {
#else
	public enum PSPDFScrollDirection : uint {
#endif
		Horizontal,
		Vertical
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFViewMode : ulong {
#else
	public enum PSPDFViewMode : uint {
#endif
		Document,
		Thumbnails
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFLinkAction : ulong {
#else
	public enum PSPDFLinkAction : uint {
#endif
		None,
		AlertView,
		OpenSafari,
		InlineBrowser
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFTextSelectionMode : ulong {
#else
	public enum PSPDFTextSelectionMode : uint {
#endif
		Regular,
		Simple
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPageRenderingMode : ulong {
#else
	public enum PSPDFPageRenderingMode : uint {
#endif
		ThumbnailThenFullPage,
		ThumbnailIfInMemoryThenFullPage,
		FullPage,
		FullPageBlocking,
		ThumbnailThenRender,
		Render
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFTextSelectionMenuAction : ulong {
#else
	public enum PSPDFTextSelectionMenuAction : uint {
#endif
		Search = 1 << 0,
		Define = 1 << 1,
		Wikipedia = 1 << 2,
		Speak = 1 << 3,
#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFThumbnailBarMode : ulong {
#else
	public enum PSPDFThumbnailBarMode : uint {
#endif
		None,
		ScrobbleBar,
		Scrollable
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFThumbnailGrouping : ulong {
#else
	public enum PSPDFThumbnailGrouping : uint {
#endif
		Automatic,
		Never,
		Always
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFHUDViewMode : ulong {
#else
	public enum PSPDFHUDViewMode : uint {
#endif
		Always,
		Automatic,
		AutomaticNoFirstLastPage,
		Never
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFHUDViewAnimation : ulong {
#else
	public enum PSPDFHUDViewAnimation : uint {
#endif
		None,
		Fade,
		Slide
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSearchMode : ulong {
#else
	public enum PSPDFSearchMode : uint {
#endif
		Modal,
		Inline
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFAnnotationSaveMode : long {
#else
	public enum PSPDFAnnotationSaveMode : int {
#endif
		Disabled,
		ExternalFile,
		Embedded,
		EmbeddedWithExternalFileAsFallback
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFTextCheckingType : ulong {
#else
	public enum PSPDFTextCheckingType : uint {
#endif
		None = 0,
		Link = 1 << 0,
		PhoneNumber = 1 << 1,
#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFCacheStatus : ulong {
#else
	public enum PSPDFCacheStatus : uint {
#endif
		NotCached,
		InMemory,
		OnDisk
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFDiskCacheStrategy : long {
#else
	public enum PSPDFDiskCacheStrategy : int {
#endif
		Nothing,
		Thumbnails,
		NearPages,
		Everything
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFCacheOptions : ulong {
#else
	public enum PSPDFCacheOptions : uint {
#endif
		MemoryStoreIfVisible = 0,
		MemoryStoreAlways = 1,
		MemoryStoreNever = 2,
		DiskLoadAsyncAndPreload = 0 << 3,
		DiskLoadAsync = 1 << 3,
		DiskLoadSyncAndPreload = 2 << 3,
		DiskLoadSync = 3 << 3,
		DiskLoadSkip = 4 << 3,
		RenderQueue = 0 << 6,
		RenderQueueBackground = 1 << 6,
		RenderSync = 2 << 6,
		RenderSkip = 3 << 6,
		ActualityCheckAndRequest = 0 << 9,
		ActualityIgnore = 1 << 9,
		SizeRequireAboutExact = 0 << 12,
		SizeRequireExact = 1 << 12,
		SizeAllowLarger = 2 << 12,
		SizeAllowLargerScaleSync = 3 << 12,
		SizeAllowLargerScaleAsync = 4 << 12,
		SizeGetLargestAvailable = 5 << 12,
		SizeAllowSmaller = 6 << 12
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPageTriggerEvent : ulong {
#else
	public enum PSPDFPageTriggerEvent : uint {
#endif
		Open,
		Close
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFRenderQueuePriority : ulong {
#else
	public enum PSPDFRenderQueuePriority : uint {
#endif
		VeryLow,
		Low,
		Normal,
		High,
		VeryHigh
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFDownloadManagerObjectState : ulong {
#else
	public enum PSPDFDownloadManagerObjectState : uint {
#endif
		NotHandled,
		Waiting,
		Loading,
		Failed
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFColorPickerStyle : ulong {
#else
	public enum PSPDFColorPickerStyle : uint {
#endif
		Rainbow,
		Modern,
		Vintage,
		Monochrome,
		HsvPicker
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFWebViewControllerAvailableActions : ulong {
#else
	public enum PSPDFWebViewControllerAvailableActions : uint {
#endif
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

#if __UNIFIED__
	[Native]
	public enum PSPDFSoundAnnotationState : long {
#else
	public enum PSPDFSoundAnnotationState : int {
#endif
		Stopped = 0,
		Recording,
		RecordingPaused,
		Playing,
		PlaybackPaused
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPersistentCloseButtonMode : ulong {
#else
	public enum PSPDFPersistentCloseButtonMode : uint {
#endif
		None,
		Left,
		Right
	}

	public enum PSPDFActionType : byte {
		Url,
		GoTo,
		RemoteGoTo,
		Named,
		Launch,
		JavaScript,
		Rendition,
		RichMediaExecute,
		SubmitForm,
		ResetForm,
		Sound,
		Movie,
		Hide,
		Thread,
		ImportData,
		SetOCGState,
		Trans,
		GoTo3DView,
		GoToEmbedded,
		Unknown = 255
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFEmbeddedGoToActionTarget : ulong {
#else
	public enum PSPDFEmbeddedGoToActionTarget : uint {
#endif
		ParentOfCurrentDocument,
		ChildOfCurrentDocument
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFNamedActionType : ulong {
#else
	public enum PSPDFNamedActionType : uint {
#endif
		None,
		NextPage,
		PreviousPage,
		FirstPage,
		LastPage,
		GoBack,
		GoForward,
		GoToPage,
		Find,
		Print,
		Outline,
		Search,
		Brightness,
		ZoomIn,
		ZoomOut,
		SaveAs,
		Info,
#if __UNIFIED__
		Unknown = ulong.MaxValue
#else
		Unknown = uint.MaxValue
#endif
	}
			
#if __UNIFIED__
	[Native]
	public enum PSPDFJavascriptErrorCode : long {
#else
	public enum PSPDFJavascriptErrorCode : int {
#endif
		ScriptExecutionFailed = 100
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFRenditionActionType : long {
#else
	public enum PSPDFRenditionActionType : int {
#endif
		Unknown = -1,
		PlayStop,
		Stop,
		Pause,
		Resume,
		Play
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFSubmitFormActionFlag : ulong {
#else
	public enum PSPDFSubmitFormActionFlag : uint {
#endif
		IncludeExclude = 1 << (1 - 1),
		IncludeNoValueFields = 1 << (2 - 1),
		ExportFormat = 1 << (3 - 1),
		GetMethod = 1 << (4 - 1),
		SubmitCoordinates = 1 << (5 - 1),
		Xfdf = 1 << (6 - 1),
		IncludeAppendSaves = 1 << (7 - 1),
		IncludeAnnotations = 1 << (8 - 1),
		SubmitPdf = 1 << (9 - 1),
		CanonicalFormat = 1 << (10 - 1),
		ExclNonUserAnnots = 1 << (11 - 1),
		ExclFKey = 1 << (12 - 1),
		EmbedForm = 1 << (14 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFResetFormActionFlag : ulong {
#else
	public enum PSPDFResetFormActionFlag : uint {
#endif
		IncludeExclude = 1 << (1 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFTextLineBorder : ulong {
#else
	public enum PSPDFTextLineBorder : uint {
#endif
		Undefined = 0,
		TopDown,
		BottomUp,
		None
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSearchStatus : long {
#else
	public enum PSPDFSearchStatus : int {
#endif
		Idle,
		Active,
		Finished,
		FinishedNoText,
		Cancelled
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFLibraryIndexStatus : ulong {
#else
	public enum PSPDFLibraryIndexStatus : uint {
#endif
		Unknown,
		Queued,
		Partial,
		PartialAndIndexing,
		Finished
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFAnnotationType : ulong {
#else
	public enum PSPDFAnnotationType : uint {
#endif
		None = 0,
		Undefined = 1 << 0,
		Link = 1 << 1,
		Highlight = 1 << 2,
		StrikeOut = 1 << 17,
		Underline = 1 << 18,
		Squiggly = 1 << 19,
		FreeText = 1 << 3,
		Ink = 1 << 4,
		Square = 1 << 5,
		Circle = 1 << 20,
		Line = 1 << 6,
		Note = 1 << 7,
		Stamp = 1 << 8,
		Caret = 1 << 9,
		RichMedia = 1 << 10,
		Screen = 1 << 11,
		Widget = 1 << 12,
		File = 1 << 13,
		Sound = 1 << 14,
		Polygon = 1 << 15,
		PolyLine = 1 << 16,
		Popup = 1 << 21,
		PSPDFANnotationTypeWatermark = 1 << 22,
		TrapNet = 1 << 23,
		_3D = 1 << 24,
		Redact = 1 << 25,
#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFAnnotationBorderStyle : ulong {
#else
	public enum PSPDFAnnotationBorderStyle : uint {
#endif
		None,
		Solid,
		Dashed,
		Belved,
		Inset,
		Underline,
		Unknown
	}


#if __UNIFIED__
	[Native]
	public enum PSPDFAnnotationBorderEffect : ulong {
#else
	public enum PSPDFAnnotationBorderEffect : uint {
#endif
		NoEffect = 0,
		Cloudy
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFAnnotationFlags : ulong {
#else
	public enum PSPDFAnnotationFlags : uint {
#endif
		Invisible = 1 << 0,
		Hidden = 1 << 1,
		Print = 1 << 2,
		NoZoom = 1 << 3,
		NoRotate = 1 << 4,
		NoView = 1 << 5,
		ReadOnly = 1 << 6,
		Locked = 1 << 7,
		ToggleNoView = 1 << 8,
		LockedContents = 1 << 9
	}

	public enum PSPDFAnnotationTriggerEvent : byte {
		CursorEnters,
		CursorExits,
		MouseDown,
		MouseUp,
		ReceiveFocus,
		LooseFocus,
		PageOpened,
		PageClosed,
		PageVisible,
		FormChanged,
		FieldFormat,
		FormValidate,
		FormCalculate
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFVerticalAlignment : ulong {
#else
	public enum PSPDFVerticalAlignment : uint {
#endif
		Top = 0,
		Center = 1,
		Bottom = 2
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFFreeTextAnnotationIntent : long {
#else
	public enum PSPDFFreeTextAnnotationIntent : int {
#endif
		FreeText,
		FreeTextCallout,
		FreeTextTypeWriter
	}

	public enum PSPDFLinkAnnotationType : byte {
		Page = 0,
		WebURL,
		Document,
		Video,
		YouTube,
		Audio,
		Image,
		Browser,
		Custom
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFMediaScreenWindowType : ulong {
#else
	public enum PSPDFMediaScreenWindowType : uint {
#endif
		Floating,
		Fullscreen,
		Hidden,
		UseAnnotationRectangle
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPolygonAnnotationIntent : long {
#else
	public enum PSPDFPolygonAnnotationIntent : int {
#endif
		None = 0,
		PolygonCloud,
		PolygonDimension
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFAppearanceCharacteristicsTextPosition : ulong {
#else
	public enum PSPDFAppearanceCharacteristicsTextPosition : uint {
#endif
		NoIcon,
		NoCaption,
		CaptionBelowIcon,
		CaptionAboveIcon,
		CaptionLeftFromIcon,
		CaptionRightFromIcon,
		CaptionOverlaid
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFIconFitScaleMode : ulong {
#else
	public enum PSPDFIconFitScaleMode : uint {
#endif
		Always,
		IfBigger,
		IfSmaller,
		Never
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFIconFitScaleType : ulong {
#else
	public enum PSPDFIconFitScaleType : uint {
#endif
		Anamorphic,
		Proportional
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFImageQuality : ulong {
#else
	public enum PSPDFImageQuality : uint {
#endif
		Low = 1 << 0,
		Medium = 1 << 1,
		High = 1 << 2,
#if __UNIFIED__
		All = ulong.MaxValue
#else
		All = uint.MaxValue
#endif
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFDrawViewInputMode : long {
#else
	public enum PSPDFDrawViewInputMode : int {
#endif
		Draw,
		Erase
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFOutlineBarButtonItemOption : ulong {
#else
	public enum PSPDFOutlineBarButtonItemOption : uint {
#endif
		Outline,
		Bookmarks,
		Annotations,
		EmbeddedFiles
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFFlexibleToolbarPosition : ulong {
#else
	public enum PSPDFFlexibleToolbarPosition : uint {
#endif
		None = 0,
		InTopBar = 1 << 0,
		Left = 1 << 1,
		Right = 1 << 2,
		sVertical = Left | Right,
		sAll = InTopBar | sVertical
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFToolbarGroupButtonIndicatorPosition : long {
#else
	public enum PSPDFToolbarGroupButtonIndicatorPosition : int {
#endif
		None = 0,
		BottomLeft,
		BottomRight
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFStatusHUDStyle : ulong {
#else
	public enum PSPDFStatusHUDStyle : uint {
#endif
		None = 0,
		Clear,
		Black,
		Gradient
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFCryptorErrorCode : long {
#else
	public enum PSPDFCryptorErrorCode : int {
#endif
		FailedToInitCryptor = 100,
		FailedToProcessFile = 110,
		InvalidIV = 200,
		WritingOutputFile = 600,
		ReadingInputFile = 700
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFFormElementFlag : ulong {
#else
	public enum PSPDFFormElementFlag : uint {
#endif
		ReadOnly = 1 << (1 - 1),
		Required = 1 << (2 - 1),
		NoExport = 1 << (3 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSubmitFormActionFormat : ulong {
#else
	public enum PSPDFSubmitFormActionFormat : uint {
#endif
		Fdf,
		Xfdf,
		Html,
		Pdf
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFButtonFlag : ulong {
#else
	public enum PSPDFButtonFlag : uint {
#endif
		NoToggleToOff = 1 << (15 - 1),
		Radio = 1 << (16 - 1),
		PushButton = 1 << (17 - 1),
		RadiosInUnison = 1 << (26 - 1)
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFChoiceFlag : ulong {
#else
	public enum PSPDFChoiceFlag : uint {
#endif
		Combo = 1 << (18 - 1),
		Edit = 1 << (19 - 1),
		Sort = 1 << (20 - 1),
		MultiSelect = 1 << (22 - 1),
		DoNotSpellCheck = 1 << (23 - 1),
		CommitOnSelChange = 1 << (27 - 1)
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFTextFieldFlag : ulong {
#else
	public enum PSPDFTextFieldFlag : uint {
#endif
		Multiline = 1 << (13 - 1),
		Password = 1 << (14 - 1),
		FileSelect = 1 << (21 - 1),
		DoNotSpellCheck = 1 << (23 - 1),
		DoNotScroll = 1 << (24 - 1),
		Comb = 1 << (25 - 1),
		RichText = 1 << (26 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFTextInputFormat : ulong {
#else
	public enum PSPDFTextInputFormat : uint {
#endif
		Normal,
		Number,
		Date,
		Time
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFPKCS12Error : ulong {
#else
	public enum PSPDFPKCS12Error : uint {
#endif
		None = 0,
		CannotOpenFile,
		CannotReadFile
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSigningAlgorithm : long {
#else
	public enum PSPDFSigningAlgorithm : int {
#endif
		RSASHA256 = 0
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSignerError : ulong {
#else
	public enum PSPDFSignerError : uint {
#endif
		None = 0,
		NoFormElementSet = 1,
		CannotNotCreatePKCS7 = 256,
		CannotNotAddSignatureToPKCS7 = 257,
		CannotNotInitPKCS7 = 258,
		CannotGeneratePKCS7Signature = 259,
		CannotWritePKCS7Signature = 260,
		OpenSSLCannotVerifySignature = 261
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSignatureStatusSeverity : long {
#else
	public enum PSPDFSignatureStatusSeverity : int {
#endif
		None = 0,
		Warning,
		Error
	}

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFDigitalSignatureReferenceTransformMethod : ulong {
#else
	public enum PSPDFDigitalSignatureReferenceTransformMethod : uint {
#endif
		DocMDP = 1 << (1 - 1),
		UR = 1 << (2 - 1),
		FieldMDP = 1 << (3 - 1),
		Identity = 1 << (4 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryViewControllerState : ulong {
#else
	public enum PSPDFGalleryViewControllerState : uint {
#endif
		Idle,
		Loading,
		Ready,
		Error
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryContainerViewContentState : ulong {
#else
	public enum PSPDFGalleryContainerViewContentState : uint {
#endif
		Loading,
		Ready,
		Error
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryContainerViewPresentationMode : ulong {
#else
	public enum PSPDFGalleryContainerViewPresentationMode : uint {
#endif
		Embedded,
		Fullscreen
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryVideoItemQuality : ulong {
#else
	public enum PSPDFGalleryVideoItemQuality : uint {
#endif
		Unknown,
		_240p,
		_360p,
		_720p,
		_1080p
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryVideoItemCoverMode : ulong {
#else
	public enum PSPDFGalleryVideoItemCoverMode : uint {
#endif
		None,
		Preview,
		Image,
		Clear
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFGalleryItemContentState : ulong {
#else
	public enum PSPDFGalleryItemContentState : uint {
#endif
		Waiting,
		Loading,
		Ready,
		Error
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFStylusTouchClassification : long {
#else
	public enum PSPDFStylusTouchClassification : int {
#endif
		UnknownDisconnected,
		Unknown,
		Finger,
		Palm,
		Pen,
		Eraser
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFStylusConnectionStatus : ulong {
#else
	public enum PSPDFStylusConnectionStatus : uint {
#endif
		Off,
		Scanning,
		Pairing,
		Connected,
		Disconnected
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFMediaPlayerControlStyle : ulong {
#else
	public enum PSPDFMediaPlayerControlStyle : uint {
#endif
		None,
		Default
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFMediaPlayerControllerContentState : ulong {
#else
	public enum PSPDFMediaPlayerControllerContentState : uint {
#endif
		Idle,
		Loading,
		Ready,
		Error
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFMediaPlayerCoverMode : ulong {
#else
	public enum PSPDFMediaPlayerCoverMode : uint {
#endif
		Preview,
		Custom,
		Hidden,
		Clear
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSliderBackgroundStyle : long {
#else
	public enum PSPDFSliderBackgroundStyle : int {
#endif
		Default = 0,
		Grayscale,
		Colorful
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFResizableViewLimitMode : ulong {
#else
	public enum PSPDFResizableViewLimitMode : uint {
#endif
		None,
		ContentFrame,
		BoundingBox,
		ViewFrame
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFResizableViewMode : ulong {
#else
	public enum PSPDFResizableViewMode : uint {
#endif
		Idle,
		Move,
		Resize,
		Adjust
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFResizableViewOuterKnob : ulong {
#else
	public enum PSPDFResizableViewOuterKnob : uint {
#endif
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

	[Flags]
#if __UNIFIED__
	[Native]
	public enum PSPDFDocumentSharingOptions : ulong {
#else
	public enum PSPDFDocumentSharingOptions : uint {
#endif
		None = 0,
		CurrentPageOnly = 1 << 0,
		VisiblePages = 1 << 1,
		AllPages = 1 << 2,
		EmbedAnnotations = 1 << 3,
		FlattenAnnotations = 1 << 4,
		AnnotationsSummary = 1 << 5,
		RemoveAnnotations = 1 << 6,
		OfferMergeFiles = 1 << 8,
		ForceMergeFiles = 2 << 8
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSecurityEvent : ulong {
#else
	public enum PSPDFSecurityEvent : uint {
#endif
		OpenIn,
		Print,
		QuickLook,
		AudioRecording,
		Camera,
		CopyPaste
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFReachability : ulong {
#else
	public enum PSPDFReachability : uint {
#endif
		Unknown,
		Unreachable,
		WiFi,
		WWAN
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFUndoCoalescing : ulong {
#else
	public enum PSPDFUndoCoalescing : uint {
#endif
		None,
		Timed,
		All
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFLineEndType : long {
#else
	public enum PSPDFLineEndType : int {
#endif
		None,
		Square,
		Circle,
		Diamond,
		OpenArrow,
		ClosedArrow,
		Butt,
		ReverseOpenArrow,
		ReverseClosedArrow,
		Slash
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFStatefulTableViewState : ulong {
#else
	public enum PSPDFStatefulTableViewState : uint {
#endif
		Loading,
		Empty,
		Finished
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFLabelStyle : ulong {
#else
	public enum PSPDFLabelStyle : uint {
#endif
		Flat,
		Modern
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSelectableCollectionViewCellStyle : ulong {
#else
	public enum PSPDFSelectableCollectionViewCellStyle : uint {
#endif
		None,
		Checkmark,
		Border
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFSeparatorHidingMode : ulong {
#else
	public enum PSPDFSeparatorHidingMode : uint {
#endif
		None,
		AfterLastCell,
		IncludingLastCell
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFRenderStatusViewPosition : ulong {
#else
	public enum PSPDFRenderStatusViewPosition : uint {
#endif
		Top,
		Centered
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFToolbarButtonControlEvents : ulong {
#else
	public enum PSPDFToolbarButtonControlEvents : uint {
#endif
		Tick = 1 << 24
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFThumbnailFlowLayoutAttributesType : long {
#else
	public enum PSPDFThumbnailFlowLayoutAttributesType : int {
#endif
		Single,
		Left,
		Right
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFScrobbleBarType : ulong {
#else
	public enum PSPDFScrobbleBarType : uint {
#endif
		Horizontal,
		VerticalLeft,
		VerticalRight
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFFontInfoType : ulong {
#else
	public enum PSPDFFontInfoType : uint {
#endif
		Simple = 1 << (1 - 1),
		Composite = 1 << (2 - 1)
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFBackButtonStyle : ulong {
#else
	public enum PSPDFBackButtonStyle : uint {
#endif
		Flat,
		Modern
	}

#if __UNIFIED__
	[Native]
	public enum PSPDFCollectionReusableFilterViewStyle : long {
#else
	public enum PSPDFCollectionReusableFilterViewStyle : int {
#endif
		None,
		LightBlur,
		DarkBlur,
		ExtraLightBlur
	}

}

