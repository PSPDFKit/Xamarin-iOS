using System;
using System.Runtime.InteropServices;
using ObjCRuntime;
using CoreGraphics;

namespace PSPDFKit.iOS {

	[StructLayout (LayoutKind.Sequential)]
	public struct PSPDFDrawingPoint {
		
		public CGPoint Location;
		public nfloat Intensity;

		public PSPDFDrawingPoint (CGPoint location, nfloat intensity)
		{
			Location = location;
			Intensity = intensity;
		}
	}

	[Flags]
	[Native]
	public enum PSPDFFeatureMask : ulong {
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
		DocumentEditing = 1 << 9,

		All = ulong.MaxValue
	}

	[Flags]
	[Native]
	public enum PSPDFLogLevelMask : ulong {
		Nothing = 0,
		Error = 1 << 0,
		Warning = 1 << 1,
		Info = 1 << 2,
		Debug = 1 << 3,
		Verbose = 1 << 4,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFErrorCode : long {
		OutOfMemory = 10,
		PageInvalid = 100,
		DocumentContainsNoPages = 101,
		DocumentNotValid = 102,
		DocumentLocked = 103,
		DocumentInvalidFormat = 104,
		UnableToOpenPDF = 200,
		UnableToGetPageReference = 210,
		UnableToGetStream = 211,
		DocumentNotSet = 212,
		DocumentProviderNotSet = 213,
		StreamPathNotSet = 214,
		AssetNameNotSet = 215,
		CantCreateStreamFile = 216,
		CantCreateStream = 217,
		CoreAnnotationNotSet = 218,
		PageRenderSizeIsEmpty = 220,
		PageRenderClipRectTooLarge = 230,
		PageRenderGraphicsContextNil = 240,
    	DocumentUnsupportedSecurityScheme = 302,
		FailedToLoadAnnotations = 400,
		FailedToWriteAnnotations = 410,
		WriteAnnotationsCancelled = 411,
		CannotEmbedAnnotations = 420,
		FailedToSaveBookmarks = 460,
		FailedToSaveDocument = 470,
		OutlineParser = 500,
		UnableToConvertToDataRepresentation = 600,
		RemoveCacheError = 700,
		FailedToConvertToPDF = 800,
		FailedToGeneratePDFInvalidArguments = 810,
		FailedToGeneratePDFDocumentInvalid = 820,
		FailedToGeneratePDFCouldNotCreateContext = 830,
		FailedToCopyPages = 840,
		FailedToUpdatePageObject = 850,
		FailedToMemoryMapFile = 860,
		MicPermissionNotGranted = 900,
		XFDFParserLackingInputStream = 1000,
		XFDFParserAlreadyCompleted = 1010,
		XFDFParserAlreadyStarted = 1020,
		XMLParserError = 1100,
		DigitalSignatureVerificationFailed = 1150,
		XFDFWriterCannotWriteToStream = 1200,
		FDFWriterCannotWriteToStream = 1250,
		SoundEncoderInvalidInput = 1300,
		GalleryInvalidManifest = 1400,
		GalleryUnknownItem = 1450,
		InvalidRemoteContent = 1500,
		FailedToSendStatistics = 1600,
		LibraryFailedToInitialize = 1700,
		FormValidationError = 5000,
		ImageProcessorInvalidImage = 6000,
		OpenInNoApplicationsFound = 7000,
		MessageNotSent = 7100,
		EmailNotConfigured = 7200,
		ProcessorAnnotationModificationError = 7300,
    	ProcessorUnableToInsertPage = 7301,
    	ProcessorUnableToFlattenAnnotation = 7302,
    	ProcessorUnableToRemoveAnnotation = 7304,
    	ProcessorUnableToIncludeDrawingBlock = 7305,
		ProcessorUnableToAddItem = 7306,
    	ProcessorUnableToWriteFile = 7307,
    	ProcessorMiscError = 7308,
    	DocumentEditorUnableToWriteFile = 7400,
    	DocumentEditorInvalidDocument = 7401,
		FailedToFetchResource = 8000,
		FailedToSetResource = 8500,
		FileCoordinationBackgroundTaskCreationFailed = 9000,
		JsonDeserializationError = 9500,
		FeatureNotEnabled = 100000,
		SecurityNoPermission = 200000,

		Unknown = long.MaxValue
	}

	[Native]
	public enum PSPDFPageTransition : ulong {
		ScrollPerPage,
		ScrollContinuous,
		Curl
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
	public enum PSPDFDocumentOrientation : long {
		Portrait,
		Landscape
	}

	[Native]
	public enum PSPDFTapAction : ulong {
		None,
		Zoom,
		SmartZoom
	}

	[Native]
	public enum PSPDFControllerState : ulong {
		Empty,
		Loading,
		Default,
		Error,
		Locked
	}

	[Native]
	public enum PSPDFLinkAction : ulong {
		None,
		AlertView,
		OpenSafari,
		InlineBrowser,
		InlineBrowserLegacy
	}

	[Native]
	public enum PSPDFTextSelectionMode : ulong {
		Regular,
		Simple
	}

	[Native]
	public enum PSPDFDrawCreateMode : ulong {
		Separate,
		MergeIfPossible
	}

	[Flags]
	[Native]
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
	public enum PSPDFThumbnailGrouping : ulong {
		Automatic,
		Never,
		Always
	}

	[Native]
	public enum PSPDFHUDViewMode : ulong {
		Always,
		Automatic,
		AutomaticNoFirstLastPage,
		Never
	}

	[Native]
	public enum PSPDFHUDViewAnimation : ulong {
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
	public enum PSPDFAnnotationSaveMode : long {
		Disabled,
		ExternalFile,
		Embedded,
		EmbeddedWithExternalFileAsFallback
	}

	[Flags]
	[Native]
	public enum PSPDFTextCheckingType : ulong {
		None = 0,
		Link = 1 << 0,
		PhoneNumber = 1 << 1,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFCacheStatus : long {
		NotCached,
		InMemory,
		OnDisk
	}

	[Native]
	public enum PSPDFDiskCacheStrategy : long {
		Nothing,
		Thumbnails,
		NearPages,
		Everything
	}

	[Flags]
	[Native]
	public enum PSPDFCacheImageSizeMatching : ulong {
		Exact = 0,
		AllowLarger  = 1 << 0,
		AllowSmaller = 1 << 1,
	}

	[Native]
	public enum PSPDFPageTriggerEvent : ulong {
		Open,
		Close
	}

	[Native]
	public enum PSPDFRenderQueuePriority : ulong {
		Unspecified = 0,
		Background = 100,
		Utility = 200,
		UserInitiated = 300,
		UserInteractive = 400,
	}

	[Native]
	public enum PSPDFDownloadManagerObjectState : ulong {
		NotHandled,
		Waiting,
		Loading,
		Failed
	}

	[Native]
	public enum PSPDFColorPickerStyle : ulong {
		Rainbow,
		Modern,
		Vintage,
		Monochrome,
		HsvPicker
	}

	[Flags]
	[Native]
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
	public enum PSPDFSoundAnnotationState : long {
		Stopped = 0,
		Recording,
		RecordingPaused,
		Playing,
		PlaybackPaused
	}

	[Native]
	public enum PSPDFPersistentCloseButtonMode : ulong {
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

	[Native]
	public enum PSPDFEmbeddedGoToActionTarget : ulong {
		ParentOfCurrentDocument,
		ChildOfCurrentDocument
	}

	[Native]
	public enum PSPDFTabbedViewControllerBarHidingMode : long {
		Automatic,
		Show,
		Hide
	}

	[Native]
	public enum PSPDFNewPageType : long {
		EmptyPage,
		TiledPatternPage,
		FromDocument
	}

	[Native]
	public enum PSPDFRectAlignment : long {
		Center = 0,
		Top,
		TopLeft,
		TopRight,
		Left,
		Bottom,
		BottomLeft,
		BottomRight,
		Right
	}

	[Native]
	public enum PSPDFAnnotationChange : long {
		Flatten,
		Remove,
		Embed,
		Print
	}

	[Native]
	public enum PSPDFItemZPosition : long {
		Foreground,
		Background
	}

	[Native]
	public enum PSPDFNamedActionType : ulong {
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
		Unknown = ulong.MaxValue
	}

	[Native]
	public enum PSPDFTabbedBarStyle : ulong {
		Light,
		Dark
	}

	[Native]
	public enum PSPDFSearchBarPinning : ulong {
		Auto,
		Top,
		None
	}

	[Native]
	public enum PSPDFPresentationStyle : ulong {
		None,
		HalfModal
	}

	[Native]
	public enum PSPDFJavascriptErrorCode : long {
		ScriptExecutionFailed = 100
	}

	[Native]
	public enum PSPDFRenditionActionType : long {
		Unknown = -1,
		PlayStop,
		Stop,
		Pause,
		Resume,
		Play
	}

	[Flags]
	[Native]
	public enum PSPDFSubmitFormActionFlag : ulong {
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

	[Native]
	public enum PSPDFResetFormActionFlag : ulong {
		IncludeExclude = 1 << (1 - 1)
	}

	[Native]
	public enum PSPDFTextLineBorder : ulong {
		Undefined = 0,
		TopDown,
		BottomUp,
		None
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
	public enum PSPDFLibraryIndexStatus : ulong {
		Unknown,
		Queued,
		Partial,
		PartialAndIndexing,
		Finished
	}

	[Flags]
	[Native]
	public enum PSPDFAnnotationType : ulong {
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
		ThreeDimensional = 1 << 24,
		Redact = 1 << 25,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFAnnotationBorderStyle : ulong {
		None,
		Solid,
		Dashed,
		Beveled,
		Inset,
		Underline,
		Unknown
	}


	[Native]
	public enum PSPDFAnnotationBorderEffect : ulong {
		NoEffect = 0,
		Cloudy
	}

	[Flags]
	[Native]
	public enum PSPDFAnnotationFlags : ulong {
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

	[Native]
	public enum PSPDFVerticalAlignment : ulong {
		Top = 0,
		Center = 1,
		Bottom = 2
	}

	[Native]
	public enum PSPDFFreeTextAnnotationIntent : long {
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

	[Native]
	public enum PSPDFMediaScreenWindowType : ulong {
		Floating,
		Fullscreen,
		Hidden,
		UseAnnotationRectangle
	}

	[Native]
	public enum PSPDFPolygonAnnotationIntent : long {
		None = 0,
		PolygonCloud,
		PolygonDimension
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
	public enum PSPDFIconFitScaleMode : ulong {
		Always,
		IfBigger,
		IfSmaller,
		Never
	}

	[Native]
	public enum PSPDFIconFitScaleType : ulong {
		Anamorphic,
		Proportional
	}

	[Native]
	public enum PSPDFThumbnailFlowLayoutLineAlignment : long {
		Left,
		Center,
		Right
	}

	[Flags]
	[Native]
	public enum PSPDFImageQuality : ulong {
		Low = 1 << 0,
		Medium = 1 << 1,
		High = 1 << 2,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFDrawViewInputMode : long {
		Draw,
		Erase
	}

	[Native]
	public enum PSPDFOutlineBarButtonItemOption : ulong {
		Outline,
		Bookmarks,
		Annotations,
		EmbeddedFiles
	}

	[Flags]
	[Native]
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
	public enum PSPDFStatusHUDStyle : ulong {
		None = 0,
		Clear,
		Black,
		Gradient
	}

	[Native]
	public enum PSPDFCryptorErrorCode : long {
		FailedToInitCryptor = 100,
		FailedToProcessFile = 110,
		InvalidIV = 200,
		WritingOutputFile = 600,
		ReadingInputFile = 700
	}

	[Flags]
	[Native]
	public enum PSPDFFormElementFlag : ulong {
		ReadOnly = 1 << (1 - 1),
		Required = 1 << (2 - 1),
		NoExport = 1 << (3 - 1)
	}

	[Native]
	public enum PSPDFSubmitFormActionFormat : ulong {
		Fdf,
		Xfdf,
		Html,
		Pdf
	}

	[Flags]
	[Native]
	public enum PSPDFButtonFlag : ulong {
		NoToggleToOff = 1 << (15 - 1),
		Radio = 1 << (16 - 1),
		PushButton = 1 << (17 - 1),
		RadiosInUnison = 1 << (26 - 1)
	}

	[Flags]
	[Native]
	public enum PSPDFChoiceFlag : ulong {
		Combo = 1 << (18 - 1),
		Edit = 1 << (19 - 1),
		Sort = 1 << (20 - 1),
		MultiSelect = 1 << (22 - 1),
		DoNotSpellCheck = 1 << (23 - 1),
		CommitOnSelChange = 1 << (27 - 1)
	}

	[Flags]
	[Native]
	public enum PSPDFTextFieldFlag : ulong {
		Multiline = 1 << (13 - 1),
		Password = 1 << (14 - 1),
		FileSelect = 1 << (21 - 1),
		DoNotSpellCheck = 1 << (23 - 1),
		DoNotScroll = 1 << (24 - 1),
		Comb = 1 << (25 - 1),
		RichText = 1 << (26 - 1)
	}

	[Native]
	public enum PSPDFTextInputFormat : ulong {
		Normal,
		Number,
		Date,
		Time
	}

	[Native]
	public enum PSPDFPKCS12Error : ulong {
		None = 0,
		CannotOpenFile,
		CannotReadFile
	}

	[Native]
	public enum PSPDFSigningAlgorithm : long {
		RSASHA256 = 0
	}

	[Native]
	public enum PSPDFSignerError : ulong {
		None = 0,
		NoFormElementSet = 1,
		CannotNotCreatePKCS7 = 256,
		CannotNotAddSignatureToPKCS7 = 257,
		CannotNotInitPKCS7 = 258,
		CannotGeneratePKCS7Signature = 259,
		CannotWritePKCS7Signature = 260,
		OpenSSLCannotVerifySignature = 261
	}

	[Native]
	public enum PSPDFSignatureStatusSeverity : long {
		None = 0,
		Warning,
		Error
	}

	[Flags]
	[Native]
	public enum PSPDFDocumentPermissions : ulong {
		NoFlags = 0,
		Printing = 1 << 0,
		Modification = 1 << 1,
		Extract = 1 << 2,
		AnnotationsAndForms = 1 << 3,
		FillForms = 1 << 4,
		ExtractAccessibility = 1 << 5,
		Assemble = 1 << 6,
		PrintHighQuality = 1 << 7
	}

	[Flags]
	[Native]
	public enum PSPDFDigitalSignatureReferenceTransformMethod : ulong {
		DocMDP = 1 << (1 - 1),
		UR = 1 << (2 - 1),
		FieldMDP = 1 << (3 - 1),
		Identity = 1 << (4 - 1)
	}

	[Native]
	public enum PSPDFGalleryViewControllerState : ulong {
		Idle,
		Loading,
		Ready,
		Error
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
	public enum PSPDFGalleryItemContentState : ulong {
		Waiting,
		Loading,
		Ready,
		Error
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
	public enum PSPDFStylusConnectionStatus : ulong {
		Off,
		Scanning,
		Pairing,
		Connected,
		Disconnected
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

	[Native]
	public enum PSPDFSliderBackgroundStyle : long {
		Default = 0,
		Grayscale,
		Colorful
	}

	[Native]
	public enum PSPDFResizableViewLimitMode : ulong {
		None,
		ContentFrame,
		BoundingBox,
		ViewFrame
	}

	[Native]
	public enum PSPDFResizableViewMode : ulong {
		Idle,
		Move,
		Resize,
		Adjust
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
	public enum PSPDFEditingOperation : ulong
	{
		Remove,
		Move,
		Insert,
		Rotate
	}

	[Flags]
	[Native]
	public enum PSPDFDocumentSharingOptions : ulong {
		None = 0,
		CurrentPageOnly = 1 << 0,
		PageRange = 1 << 1,
		AllPages = 1 << 2,
		AnnotatedPages = 1 << 4,
		VisiblePages = PageRange,
		EmbedAnnotations = 1 << 8,
		FlattenAnnotations = 1 << 9,
		AnnotationsSummary = 1 << 10,
		RemoveAnnotations = 1 << 11,
		FlattenAnnotationsForPrint = 1 << 12,
		OriginalFile = 1 << 16,
		Image = 1 << 17
	}

	[Native]
	public enum PSPDFSecurityEvent : ulong {
		OpenIn,
		Print,
		QuickLook,
		AudioRecording,
		Camera,
		CopyPaste
	}

	[Native]
	public enum PSPDFReachability : ulong {
		Unknown,
		Unreachable,
		WiFi,
		WWAN
	}

	[Native]
	public enum PSPDFUndoCoalescing : ulong {
		None,
		Timed,
		All
	}

	[Native]
	public enum PSPDFLineEndType : long {
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

	[Native]
	public enum PSPDFStatefulTableViewState : ulong {
		Loading,
		Empty,
		Finished
	}

	[Native]
	public enum PSPDFLabelStyle : ulong {
		Flat,
		Modern
	}

	[Native]
	public enum PSPDFSelectableCollectionViewCellStyle : ulong {
		None,
		Checkmark,
		Border
	}

	[Native]
	public enum PSPDFSeparatorHidingMode : ulong {
		None,
		AfterLastCell,
		IncludingLastCell
	}

	[Native]
	public enum PSPDFRenderStatusViewPosition : ulong {
		Top,
		Centered
	}

	[Native]
	[Flags]
	public enum PSPDFToolbarButtonControlEvents : ulong {
		Tick = 1 << 24,
		TouchUpInsideIfNotTicking = 1 << 25
	}

	[Native]
	public enum PSPDFThumbnailFlowLayoutAttributesType : long {
		Single,
		Left,
		Right
	}

	[Native]
	public enum PSPDFScrubberBarType : ulong {
		Horizontal,
		VerticalLeft,
		VerticalRight
	}

	[Native]
	public enum PSPDFAdaptiveConditional : ulong {
		No,
		Yes,
		Adaptive
	}

	[Native]
	public enum PSPDFFontInfoType : ulong {
		Simple = 1 << (1 - 1),
		Composite = 1 << (2 - 1)
	}

	[Native]
	public enum PSPDFBackButtonStyle : ulong {
		Flat,
		Modern
	}

	[Native]
	public enum PSPDFCollectionReusableFilterViewStyle : long {
		None,
		LightBlur,
		DarkBlur,
		ExtraLightBlur
	}

	[Native]
	public enum PSPDFDataProviderAdditionalOperations : ulong {
		None = 0,
		Write = 1
	}

	[Native]
	public enum PSPDFDataSinkOptions : ulong {
		None = 0,
		Append = 1
	}

	[Flags]
	[Native]
	public enum PSPDFAppearanceMode : ulong {
		Default = 0,
		Sepia = 1 << 0,
		Night = 1 << 1,
		All = Default | Sepia | Night
	}

	[Native]
	public enum PSPDFScrollInsetAdjustment : ulong {
		None,
		FixedElements,
		AllElements
	}

	[Native]
	public enum PSPDFKnobType : ulong {
		Outer,
		Inner
	}

	[Native]
	public enum PSPDFColorSet : ulong {
		Default,
		DefaultWithTransparency,
		PageBackgrounds
	}

	[Native]
	public enum PSPDFRenderType : ulong {
		Page,
		Processor,
		All = ulong.MaxValue
	}

	[Native]
	public enum PSPDFBookmarkManagerSortOrder : ulong {
		Custom,
		PageBased
	}

	[Native]
	public enum PSPDFCacheStoragePolicy : long {
		Automatic = 0,
		Allowed,
		AllowedInMemoryOnly,
		NotAllowed,
	}

	[Native]
	public enum PSPDFRenderRequestCachePolicy : long {
		Default = 0,
		ReloadIgnoreingCacheData,
		ReturnCacheDataElseLoad,
		ReturnCacheDataDontLoad,
	}

	[Native]
	public enum PSPDFBookmarkIndicatorImageType : long {
		Large,
		Medium,
		Small
	}

	[Native]
	public enum PSPDFPageBookmarkIndicatorMode : ulong {
		Off,
		AlwaysOn,
		OnWhenBookmarked
	}

	[Native]
	public enum PSPDFDiskCacheFileFormat : long {
		Jpeg,
		Png
	}

	[Native]
	public enum PSPDFLibrarySpotlightIndexingType : long {
		Disabled = 0,
		Enabled = 1,
		EnabledWithFullText = 2
	}

	[Native]
	public enum PSPDFSearchResultScope : long {
		PageRange,
		Document
	}

	[Native]
	public enum PSPDFTabbedViewControllerCloseMode : long {
		OnlySelectedTab,
		AllTabs,
		SizeDependent,
		Disabled
	}

	[Native]
	public enum PSPDFLibraryFtsVersion : ulong {
		HighestAvailable,
		Version4,
		Version5
	}

	[Native]
	public enum PSPDFAnnotationStateManagerStylusMode : ulong {
		FromStylusManager,
		Direct,
		Stylus
	}

	[Native]
	public enum PSPDFFormFieldType : ulong {
		Unknown,
		PushButton,
		RadioButton,
		CheckBox,
		Text,
		ListBox,
		ComboBox,
		Signature
	}

	[Native]
	public enum PSPDFPrintMode : ulong {
		Interactive,
		ChoosePrinterOnly,
		PrintDirect
	}
}
