using System;

using ObjCRuntime;
using Foundation;
using UIKit;
using CoreGraphics;
using AVFoundation;
using CoreFoundation;
using QuickLook;
using MessageUI;
using CoreAnimation;
using CoreMedia;
using MediaPlayer;
using CoreSpotlight;

namespace PSPDFKit.iOS {

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject), Name = "PSPDFKit")]
	interface PSPDFKitGlobal : PSPDFSettings {

		[Field ("PSPDFXCallbackURLStringKey", "__Internal")]
		NSString XCallbackUrlStringKey { get; }

		[Field ("PSPDFApplicationPolicyKey", "__Internal")]
		NSString ApplicationPolicyKey { get; }

		[Field ("PSPDFFileManagerKey", "__Internal")]
		NSString FileManagerKey { get; }

		[Field ("PSPDFCoordinatedFileManagerKey", "__Internal")]
		NSString CoordinatedFileManagerKey { get; }

		[Field ("PSPDFWebKitLegacyModeKey", "__Internal")]
		NSString WebKitLegacyModeKey { get; }

		[Static]
		[Export ("sharedInstance")]
		PSPDFKitGlobal SharedInstance { get; }

		[Static]
		[Export ("setLicenseKey:")]
		void SetLicenseKey (string licenseKey);

		[Static]
		[Export ("setLicenseKey:options:")]
		void SetLicenseKey (string licenseKey, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("logLevel", ArgumentSemantic.Assign)]
		PSPDFLogLevelMask LogLevel { get; set; }

		[Static]
		[Export ("versionString")]
		string VersionString { get; }

		[Static]
		[Export ("versionNumber")]
		string VersionNumber { get; }

		[Static]
		[Export ("compiledAt")]
		NSDate CompiledAt { get; }

		[Static]
		[Export ("buildNumber")]
		uint BuildNumber { get; }

		[Static]
		[Export ("isFeatureEnabled:")]
		bool IsFeatureEnabled (PSPDFFeatureMask feature);

		[Export ("setObject:forKeyedSubscript:")]
		void SetObject (NSObject obj, NSString key);

		[Export ("cache", ArgumentSemantic.Strong)]
		PSPDFCache Cache { get; }

		[Export ("fileManager", ArgumentSemantic.Strong)]
		IPSPDFFileManager FileManager { get; }

		[Export ("renderManager", ArgumentSemantic.Strong)]
		IPSPDFRenderManager RenderManager { get; }

		[Export ("styleManager", ArgumentSemantic.Strong)]
		IPSPDFAnnotationStyleManager StyleManager { get; }

		[Export ("signatureManager", ArgumentSemantic.Strong)]
		PSPDFSignatureManager SignatureManager { get; }

		[Export ("policy", ArgumentSemantic.Strong)]
		IPSPDFApplicationPolicy Policy { get; }

		[Export ("library", ArgumentSemantic.Retain), NullAllowed]
		PSPDFLibrary Library { get; set; }

		[Export ("databaseEncryptionProvider", ArgumentSemantic.Retain), NullAllowed]
		IPSPDFDatabaseEncryptionProvider DatabaseEncryptionProvider { get; set; }

		[Export ("injectDependentProperties:")]
		nuint InjectDependentProperties (NSObject obj);

		// PSPDFKit (Services) PSPDFUI Category

		[Export ("application", ArgumentSemantic.Strong)]
		IPSPDFApplication Application { get; }

		[Export ("speechSynthesizer", ArgumentSemantic.Strong)]
		PSPDFSpeechController SpeechSynthesizer { get; }

		[Export ("stylusManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStylusManager StylusManager { get; }

		// interface PSPDFKit (ImageLoading) Category

		[Export ("imageNamed:")]
		[return: NullAllowed]
		UIImage GetImageNamed (string name);

		[Export ("imageLoadingHandler"), NullAllowed]
		Func<string,UIImage> ImageLoadingHandler { get; set; }

		// interface PSPDFKit (Analytics)

		[Export ("analytics")]
		PSPDFAnalytics Analytics { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFBaseConfiguration {

		[Static]
		[Export ("defaultConfiguration")]
		PSPDFBaseConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFBaseConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFBaseConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFBaseConfigurationBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFBaseConfiguration ConfigurationUpdated ([NullAllowed] Action<PSPDFBaseConfigurationBuilder> builderHandler);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFBaseConfigurationBuilder {

		[Static]
		[Export ("configurationTargetClass")]
		Class ConfigurationTargetClass { get; }

		[Export ("build")]
		PSPDFBaseConfiguration GetBuild ();

		[Export ("reset")]
		void Reset ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFConfiguration : PSPDFOverridable {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFConfigurationBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFConfiguration ConfigurationUpdated ([NullAllowed] Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFPageMode PageMode { get; }

		[Export ("pageTransition", ArgumentSemantic.Assign)]
		PSPDFPageTransition PageTransition { get; }

		[Export ("firstPageAlwaysSingle")]
		bool FirstPageAlwaysSingle { [Bind ("isFirstPageAlwaysSingle")] get; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; }

		[Export ("pageCurlDirectionLeftToRight", ArgumentSemantic.Assign)]
		bool PageCurlDirectionLeftToRight { [Bind ("isPageCurlDirectionLeftToRight")] get; }

		[Export ("fitToWidthEnabled")]
		PSPDFAdaptiveConditional FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; }

		[Export ("fixedVerticalPositionForFitToWidthEnabledMode", ArgumentSemantic.Assign)]
		bool FixedVerticalPositionForFitToWidthEnabledMode { get; }

		[Export ("clipToPageBoundaries", ArgumentSemantic.Assign)]
		bool ClipToPageBoundaries { get; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; }

		[Export ("shadowOpacity", ArgumentSemantic.Assign)]
		nfloat ShadowOpacity { get; }

		[Export ("backgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BackgroundColor { get; }

		[Export ("allowedAppearanceModes")]
		PSPDFAppearanceMode AllowedAppearanceModes { get; }

		[Export ("scrollDirection", ArgumentSemantic.Assign)]
		PSPDFScrollDirection ScrollDirection { get; }

		[Export ("scrollViewInsetAdjustment")]
		PSPDFScrollInsetAdjustment ScrollViewInsetAdjustment { get; }

		[Export ("alwaysBouncePages", ArgumentSemantic.Assign)]
		bool AlwaysBouncePages { get; }

		[Export ("showsHorizontalScrollIndicator", ArgumentSemantic.Assign)]
		bool ShowsHorizontalScrollIndicator { get; }

		[Export ("showsVerticalScrollIndicator", ArgumentSemantic.Assign)]
		bool ShowsVerticalScrollIndicator { get; }

		[Export ("minimumZoomScale", ArgumentSemantic.Assign)]
		float MinimumZoomScale { get; }

		[Export ("maximumZoomScale", ArgumentSemantic.Assign)]
		float MaximumZoomScale { get; }

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; }

		[Export ("padding", ArgumentSemantic.Assign)]
		UIEdgeInsets Padding { get; }

		[Export ("pagePadding", ArgumentSemantic.Assign)]
		nfloat PagePadding { get; }

		[Export ("renderAnimationEnabled", ArgumentSemantic.Assign)]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; }

		[Export ("renderStatusViewPosition", ArgumentSemantic.Assign)]
		PSPDFRenderStatusViewPosition RenderStatusViewPosition { get; }

		[Export ("doubleTapAction")]
		PSPDFTapAction DoubleTapAction { get; }

		[Export ("formElementZoomEnabled", ArgumentSemantic.Assign)]
		bool FormElementZoomEnabled { [Bind ("isFormElementZoomEnabled")] get; }

		[Export ("scrollOnTapPageEndEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndEnabled { [Bind ("isScrollOnTapPageEndEnabled")] get; }

		[Export ("scrollOnTapPageEndAnimationEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndAnimationEnabled { [Bind ("isScrollOnTapPageEndAnimationEnabled")] get; }

		[Export ("scrollOnTapPageEndMargin", ArgumentSemantic.Assign)]
		nfloat ScrollOnTapPageEndMargin { get; }

		[Export ("linkAction", ArgumentSemantic.Assign)]
		PSPDFLinkAction LinkAction { get; }

		[Export ("allowedMenuActions", ArgumentSemantic.Assign)]
		PSPDFTextSelectionMenuAction AllowedMenuActions { get; }

		[Export ("textSelectionEnabled", ArgumentSemantic.Assign)]
		bool TextSelectionEnabled { [Bind ("isTextSelectionEnabled")] get; }

		[Export ("imageSelectionEnabled", ArgumentSemantic.Assign)]
		bool ImageSelectionEnabled { [Bind ("isImageSelectionEnabled")] get; }

		[Export ("textSelectionMode", ArgumentSemantic.Assign)]
		PSPDFTextSelectionMode TextSelectionMode { get; }

		[Export ("textSelectionShouldSnapToWord", ArgumentSemantic.Assign)]
		bool TextSelectionShouldSnapToWord { get; }

		[Export ("editableAnnotationTypes")]
		NSSet<NSString> EditableAnnotationTypes { get; }

		[Export ("typesShowingColorPresets", ArgumentSemantic.Assign)]
		PSPDFAnnotationType TypesShowingColorPresets { get; }

		[Export ("propertiesForAnnotations")]
		NSDictionary<NSString, NSObject> PropertiesForAnnotations { get; }

		[Export ("freeTextAccessoryViewEnabled")]
		bool FreeTextAccessoryViewEnabled { [Bind ("isFreeTextAccessoryViewEnabled")] get; }

		[Export ("bookmarkSortOrder")]
		PSPDFBookmarkManagerSortOrder BookmarkSortOrder { get; }

		[Export ("bookmarkIndicatorMode")]
		PSPDFPageBookmarkIndicatorMode BookmarkIndicatorMode { get; }

		[Export ("bookmarkIndicatorInteractionEnabled")]
		bool BookmarkIndicatorInteractionEnabled { get; }

		[Export ("HUDViewMode", ArgumentSemantic.Assign)]
		PSPDFHUDViewMode HudViewMode { get; }

		[Export ("HUDViewAnimation", ArgumentSemantic.Assign)]
		PSPDFHUDViewAnimation HudViewAnimation { get; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; }

		[Export ("documentLabelEnabled")]
		PSPDFAdaptiveConditional DocumentLabelEnabled { get; }

		[Export ("shouldHideHUDOnPageChange", ArgumentSemantic.Assign)]
		bool ShouldHideHUDOnPageChange { get; }

		[Export ("shouldShowHUDOnViewWillAppear", ArgumentSemantic.Assign)]
		bool ShouldShowHUDOnViewWillAppear { get; }

		[Export ("allowToolbarTitleChange", ArgumentSemantic.Assign)]
		bool AllowToolbarTitleChange { get; }

		[Export ("shouldHideNavigationBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideNavigationBarWithHUD { get; }

		[Export ("shouldHideStatusBar", ArgumentSemantic.Assign)]
		bool ShouldHideStatusBar { get; }

		[Export ("shouldHideStatusBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideStatusBarWithHUD { get; }

		[Export ("showBackActionButton", ArgumentSemantic.Assign)]
		bool ShowBackActionButton { get; }

		[Export ("showForwardActionButton", ArgumentSemantic.Assign)]
		bool ShowForwardActionButton { get; }

		[Export ("showBackForwardActionButtonLabels", ArgumentSemantic.Assign)]
		bool ShowBackForwardActionButtonLabels { get; }

		[Export ("thumbnailBarMode", ArgumentSemantic.Assign)]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; }

		[Export ("scrubberBarType", ArgumentSemantic.Assign)]
		PSPDFScrubberBarType ScrubberBarType { get; }

		[Export ("thumbnailGrouping", ArgumentSemantic.Assign)]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; }

		[Export ("thumbnailInteritemSpacing", ArgumentSemantic.Assign)]
		nfloat ThumbnailInteritemSpacing { get; }

		[Export ("thumbnailLineSpacing", ArgumentSemantic.Assign)]
		nfloat ThumbnailLineSpacing { get; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; }

		[Export ("annotationAnimationDuration", ArgumentSemantic.Assign)]
		nfloat AnnotationAnimationDuration { get; }

		[Export ("annotationGroupingEnabled", ArgumentSemantic.Assign)]
		bool AnnotationGroupingEnabled { get; }

		[Export ("createAnnotationMenuEnabled", ArgumentSemantic.Assign)]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroup [] CreateAnnotationMenuGroups { get; }

		[Export ("naturalDrawingAnnotationEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingAnnotationEnabled { get; }

		[Export ("drawCreateMode", ArgumentSemantic.Assign)]
		PSPDFDrawCreateMode DrawCreateMode { get; }

		[Export ("showAnnotationMenuAfterCreation", ArgumentSemantic.Assign)]
		bool ShowAnnotationMenuAfterCreation { get; }

		[Export ("shouldAskForAnnotationUsername", ArgumentSemantic.Assign)]
		bool ShouldAskForAnnotationUsername { get; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled", ArgumentSemantic.Assign)]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; }

		[Export ("shouldScrollToChangedPage", ArgumentSemantic.Assign)]
		bool ShouldScrollToChangedPage { get; }

		[Export ("autosaveEnabled", ArgumentSemantic.Assign)]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; }

		[Export ("allowBackgroundSaving", ArgumentSemantic.Assign)]
		bool AllowBackgroundSaving { get; }

		[Export ("soundAnnotationTimeLimit", ArgumentSemantic.Assign)]
		double SoundAnnotationTimeLimit { get; }

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; }

		[Export ("searchResultZoomScale")]
		nfloat SearchResultZoomScale { get; }

		[Export ("signatureSavingEnabled", ArgumentSemantic.Assign)]
		bool SignatureSavingEnabled { get; }

		[Export ("customerSignatureFeatureEnabled", ArgumentSemantic.Assign)]
		bool CustomerSignatureFeatureEnabled { get; }

		[Export ("naturalSignatureDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalSignatureDrawingEnabled { get; }

		[Export ("signatureStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFSignatureStore SignatureStore { get; }

		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		NSString [] ApplicationActivities { get; }

		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		NSString [] ExcludedActivityTypes { get; }

		[Export ("printSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions PrintSharingOptions { get; }

		[Export ("printConfiguration")]
		PSPDFPrintConfiguration PrintConfiguration { get; }

		[Export ("openInSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions OpenInSharingOptions { get; }

		[Export ("mailSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions MailSharingOptions { get; }

		[Export ("messageSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions MessageSharingOptions { get; }

		[Export ("settingsOptions", ArgumentSemantic.Assign)]
		PSPDFSettingsOptions SettingsOptions { get; }

		[Export ("internalTapGesturesEnabled", ArgumentSemantic.Assign)]
		bool InternalTapGesturesEnabled { get; }

		[Export ("useParentNavigationBar", ArgumentSemantic.Assign)]
		bool UseParentNavigationBar { get; }

		[Export ("shouldCacheThumbnails", ArgumentSemantic.Assign)]
		bool ShouldCacheThumbnails { get; }

		[Export ("galleryConfiguration", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryConfiguration GalleryConfiguration { get; }
	}

	[Static]
	interface PSPDFActivityType {
		[Field ("PSPDFActivityTypeGoToPage", "__Internal")]
		NSString GoToPage { get; }

		[Field ("PSPDFActivityTypeSearch", "__Internal")]
		NSString Search { get; }

		[Field ("PSPDFActivityTypeOutline", "__Internal")]
		NSString Outline { get; }

		[Field ("PSPDFActivityTypeBookmarks", "__Internal")]
		NSString Bookmarks { get; }

		[Field ("PSPDFActivityTypeOpenIn", "__Internal")]
		NSString OpenIn { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFConfigurationBuilder {

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, [NullAllowed] Class subclass);

		[Wrap ("OverrideClass (new Class (builtinType), subType == null ? null : new Class (subType))")]
		void OverrideClass (Type builtinType, Type subType);

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }

		[Export ("padding", ArgumentSemantic.Assign)]
		UIEdgeInsets Padding { get; set; }

		[Export ("pagePadding", ArgumentSemantic.Assign)]
		nfloat PagePadding { get; set; }

		[Export ("doubleTapAction", ArgumentSemantic.Assign)]
		PSPDFTapAction DoubleTapAction { get; set; }

		[Export ("formElementZoomEnabled", ArgumentSemantic.Assign)]
		bool FormElementZoomEnabled { [Bind ("isFormElementZoomEnabled")] get; set; }

		[Export ("scrollOnTapPageEndEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndEnabled { [Bind ("isScrollOnTapPageEndEnabled")] get; set; }

		[Export ("scrollOnTapPageEndAnimationEnabled", ArgumentSemantic.Assign)]
		bool ScrollOnTapPageEndAnimationEnabled { [Bind ("isScrollOnTapPageEndAnimationEnabled")] get; set; }

		[Export ("scrollOnTapPageEndMargin", ArgumentSemantic.Assign)]
		nfloat ScrollOnTapPageEndMargin { get; set; }

		[Export ("textSelectionEnabled", ArgumentSemantic.Assign)]
		bool TextSelectionEnabled { [Bind ("isTextSelectionEnabled")] get; set; }

		[Export ("imageSelectionEnabled", ArgumentSemantic.Assign)]
		bool ImageSelectionEnabled { [Bind ("isImageSelectionEnabled")] get; set; }

		[Export ("textSelectionMode", ArgumentSemantic.Assign)]
		PSPDFTextSelectionMode TextSelectionMode { get; set; }

		[Export ("textSelectionShouldSnapToWord", ArgumentSemantic.Assign)]
		bool TextSelectionShouldSnapToWord { get; set; }

		[Export ("typesShowingColorPresets", ArgumentSemantic.Assign)]
		PSPDFAnnotationType TypesShowingColorPresets { get; set; }

		[Export ("propertiesForAnnotations", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> PropertiesForAnnotations { get; set; }

		[Export ("freeTextAccessoryViewEnabled", ArgumentSemantic.Assign)]
		bool FreeTextAccessoryViewEnabled { get; set; }

		[Export ("bookmarkSortOrder", ArgumentSemantic.Assign)]
		PSPDFBookmarkManagerSortOrder BookmarkSortOrder { get; set; }

		[Export ("bookmarkIndicatorMode", ArgumentSemantic.Assign)]
		PSPDFPageBookmarkIndicatorMode BookmarkIndicatorMode { get; set; }

		[Export ("bookmarkIndicatorInteractionEnabled", ArgumentSemantic.Assign)]
		bool BookmarkIndicatorInteractionEnabled { get; set; }

		[Export ("internalTapGesturesEnabled", ArgumentSemantic.Assign)]
		bool InternalTapGesturesEnabled { get; set; }

		[Export ("useParentNavigationBar", ArgumentSemantic.Assign)]
		bool UseParentNavigationBar { get; set; }

		[Export ("shouldRestoreNavigationBarStyle", ArgumentSemantic.Assign)]
		bool ShouldRestoreNavigationBarStyle { get; set; }

		[Export ("linkAction", ArgumentSemantic.Assign)]
		PSPDFLinkAction LinkAction { get; set; }

		[Export ("allowedMenuActions", ArgumentSemantic.Assign)]
		PSPDFTextSelectionMenuAction AllowedMenuActions { get; set; }

		[Export ("HUDViewMode", ArgumentSemantic.Assign)]
		PSPDFHUDViewMode HUDViewMode { get; set; }

		[Export ("HUDViewAnimation", ArgumentSemantic.Assign)]
		PSPDFHUDViewAnimation HUDViewAnimation { get; set; }

		[Export ("thumbnailBarMode", ArgumentSemantic.Assign)]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; set; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("documentLabelEnabled", ArgumentSemantic.Assign)]
		PSPDFAdaptiveConditional DocumentLabelEnabled { get; set; }

		[Export ("shouldHideHUDOnPageChange", ArgumentSemantic.Assign)]
		bool ShouldHideHUDOnPageChange { get; set; }

		[Export ("shouldShowHUDOnViewWillAppear", ArgumentSemantic.Assign)]
		bool ShouldShowHUDOnViewWillAppear { get; set; }

		[Export ("allowToolbarTitleChange", ArgumentSemantic.Assign)]
		bool AllowToolbarTitleChange { get; set; }

		[Export ("renderAnimationEnabled", ArgumentSemantic.Assign)]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; set; }

		[Export ("renderStatusViewPosition", ArgumentSemantic.Assign)]
		PSPDFRenderStatusViewPosition RenderStatusViewPosition { get; set; }

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFPageMode PageMode { get; set; }

		[Export ("scrubberBarType", ArgumentSemantic.Assign)]
		PSPDFScrubberBarType ScrubberBarType { get; set; }

		[Export ("thumbnailGrouping", ArgumentSemantic.Assign)]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; set; }

		[Export ("pageTransition", ArgumentSemantic.Assign)]
		PSPDFPageTransition PageTransition { get; set; }

		[Export ("scrollDirection", ArgumentSemantic.Assign)]
		PSPDFScrollDirection ScrollDirection { get; set; }

		[Export ("scrollViewInsetAdjustment", ArgumentSemantic.Assign)]
		PSPDFScrollInsetAdjustment ScrollViewInsetAdjustment { get; set; }

		[Export ("firstPageAlwaysSingle")]
		bool FirstPageAlwaysSingle { [Bind ("isFirstPageAlwaysSingle")] get; set; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; set; }

		[Export ("pageCurlDirectionLeftToRight", ArgumentSemantic.Assign)]
		bool PageCurlDirectionLeftToRight { [Bind ("isPageCurlDirectionLeftToRight")] get; set; }

		[Export ("fitToWidthEnabled", ArgumentSemantic.Assign)]
		PSPDFAdaptiveConditional FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; set; }

		[Export ("showsHorizontalScrollIndicator", ArgumentSemantic.Assign)]
		bool ShowsHorizontalScrollIndicator { get; set; }

		[Export ("showsVerticalScrollIndicator", ArgumentSemantic.Assign)]
		bool ShowsVerticalScrollIndicator { get; set; }

		[Export ("alwaysBouncePages", ArgumentSemantic.Assign)]
		bool AlwaysBouncePages { get; set; }

		[Export ("fixedVerticalPositionForFitToWidthEnabledMode", ArgumentSemantic.Assign)]
		bool FixedVerticalPositionForFitToWidthEnabledMode { get; set; }

		[Export ("clipToPageBoundaries", ArgumentSemantic.Assign)]
		bool ClipToPageBoundaries { get; set; }

		[Export ("minimumZoomScale", ArgumentSemantic.Assign)]
		float MinimumZoomScale { get; set; }

		[Export ("maximumZoomScale", ArgumentSemantic.Assign)]
		float MaximumZoomScale { get; set; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("shadowOpacity", ArgumentSemantic.Assign)]
		nfloat ShadowOpacity { get; set; }

		[Export ("shouldHideNavigationBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideNavigationBarWithHUD { get; set; }

		[Export ("shouldHideStatusBar", ArgumentSemantic.Assign)]
		bool ShouldHideStatusBar { get; set; }

		[Export ("shouldHideStatusBarWithHUD", ArgumentSemantic.Assign)]
		bool ShouldHideStatusBarWithHUD { get; set; }

		[Export ("backgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BackgroundColor { get; set; }

		[Export ("allowedAppearanceModes", ArgumentSemantic.Assign)]
		PSPDFAppearanceMode AllowedAppearanceModes { get; set; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailInteritemSpacing", ArgumentSemantic.Assign)]
		nfloat ThumbnailInteritemSpacing { get; set; }

		[Export ("thumbnailLineSpacing", ArgumentSemantic.Assign)]
		nfloat ThumbnailLineSpacing { get; set; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("annotationAnimationDuration", ArgumentSemantic.Assign)]
		nfloat AnnotationAnimationDuration { get; set; }

		[Export ("annotationGroupingEnabled", ArgumentSemantic.Assign)]
		bool AnnotationGroupingEnabled { get; set; }

		[Export ("createAnnotationMenuEnabled", ArgumentSemantic.Assign)]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; set; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroup [] CreateAnnotationMenuGroups { get; set; }

		[Export ("naturalDrawingAnnotationEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingAnnotationEnabled { get; set; }

		[Export ("drawCreateMode", ArgumentSemantic.Assign)]
		PSPDFDrawCreateMode DrawCreateMode { get; set; }

		[Export ("showAnnotationMenuAfterCreation", ArgumentSemantic.Assign)]
		bool ShowAnnotationMenuAfterCreation { get; set; }

		[Export ("shouldAskForAnnotationUsername", ArgumentSemantic.Assign)]
		bool ShouldAskForAnnotationUsername { get; set; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled", ArgumentSemantic.Assign)]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; set; }

		[NullAllowed, Export ("editableAnnotationTypes", ArgumentSemantic.Copy)]
		NSSet<NSString> EditableAnnotationTypes { get; set; }

		[Export ("autosaveEnabled", ArgumentSemantic.Assign)]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; set; }

		[Export ("allowBackgroundSaving", ArgumentSemantic.Assign)]
		bool AllowBackgroundSaving { get; set; }

		[Export ("soundAnnotationTimeLimit", ArgumentSemantic.Assign)]
		double SoundAnnotationTimeLimit { get; set; }

		[Export ("shouldCacheThumbnails", ArgumentSemantic.Assign)]
		bool ShouldCacheThumbnails { get; set; }

		[Export ("shouldScrollToChangedPage", ArgumentSemantic.Assign)]
		bool ShouldScrollToChangedPage { get; set; }

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; set; }

		[Export ("searchResultZoomScale")]
		nfloat SearchResultZoomScale { get; set; }

		[Export ("signatureSavingEnabled", ArgumentSemantic.Assign)]
		bool SignatureSavingEnabled { get; set; }

		[Export ("customerSignatureFeatureEnabled", ArgumentSemantic.Assign)]
		bool CustomerSignatureFeatureEnabled { get; set; }

		[Export ("naturalSignatureDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalSignatureDrawingEnabled { get; set; }

		[Export ("signatureStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFSignatureStore SignatureStore { get; set; }

		[Export ("galleryConfiguration", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryConfiguration GalleryConfiguration { get; set; }

		[Export ("showBackActionButton", ArgumentSemantic.Assign)]
		bool ShowBackActionButton { get; set; }

		[Export ("showForwardActionButton", ArgumentSemantic.Assign)]
		bool ShowForwardActionButton { get; set; }

		[Export ("showBackForwardActionButtonLabels", ArgumentSemantic.Assign)]
		bool ShowBackForwardActionButtonLabels { get; set; }

		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		NSString [] ApplicationActivities { get; set; }

		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		NSString [] ExcludedActivityTypes { get; set; }

		[Export ("printSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions PrintSharingOptions { get; set; }

		[Export ("printConfiguration", ArgumentSemantic.Strong)]
		PSPDFPrintConfiguration PrintConfiguration { get; set; }

		[Export ("openInSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions OpenInSharingOptions { get; set; }

		[Export ("mailSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions MailSharingOptions { get; set; }

		[Export ("messageSharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions MessageSharingOptions { get; set; }

		[Export ("settingsOptions", ArgumentSemantic.Assign)]
		PSPDFSettingsOptions SettingsOptions { get; set; }
	}

	interface IPSPDFDocumentActionExecutorDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentActionExecutorDelegate {

		[Export ("documentActionExecutor:defaultOptionsForAction:")]
		NSDictionary<NSString, NSObject> DefaultOptionsForAction (PSPDFDocumentActionExecutor documentActionExecutor, string action);
	}

	[Static]
	interface PSPDFDocumentActionKeys {

		[Field ("PSPDFDocumentActionSharingOptionsKey", "__Internal")]
		NSString SharingOptionsKey { get; }

		[Field ("PSPDFDocumentActionVisiblePagesKey", "__Internal")]
		NSString VisiblePagesKey { get; }

		[Field ("PSPDFDocumentActionPrint", "__Internal")]
		NSString Print { get; }

		[Field ("PSPDFDocumentActionEmail", "__Internal")]
		NSString Email { get; }

		[Field ("PSPDFDocumentActionOpenIn", "__Internal")]
		NSString OpenIn { get; }

		[Field ("PSPDFDocumentActionMessage", "__Internal")]
		NSString Message { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentActionExecutor : PSPDFDocumentSharingCoordinatorDelegate {

		[Export ("initWithSourceViewController:")]
		[DesignatedInitializer]
		IntPtr Constructor (IPSPDFPresentationActions sourceViewController);

		[Export ("sourceViewController", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationActions SourceViewController { get; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFDocumentActionExecutorDelegate Delegate { get; set; }

		[Export ("documents", ArgumentSemantic.Retain), NullAllowed]
		PSPDFDocument[] Documents { get; set; }

		[Export ("canExecuteAction:")]
		bool CanExecuteAction (string action);

		[Export ("executeAction:options:sender:animated:completion:")]
		void ExecuteAction (string action, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);
	}

	[Static]
	interface PSPDFStyleManagerKeys
	{
		[Field ("PSPDFStyleManagerLastUsedStylesKey", "__Internal")]
		NSString LastUsedStylesKey { get; }

		[Field ("PSPDFStyleManagerGenericStylesKey", "__Internal")]
		NSString GenericStylesKey { get; }

		[Field ("PSPDFStyleManagerColorPresetKey", "__Internal")]
		NSString ColorPresetKey { get; }
	}

	interface IPSPDFDocumentSharingCoordinatorDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSharingCoordinatorDelegate : PSPDFOverridable {

		[Export ("documentSharingCoordinator:didFinishWithError:")]
		[Abstract]
		void DidFinish (PSPDFDocumentSharingCoordinator coordinator, [NullAllowed] NSError error);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSharingCoordinator : PSPDFDocumentSharingViewControllerDelegate {

		[Export ("initWithDocuments:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument [] documents);

		[Export ("documents")]
		PSPDFDocument [] Documents { get; }

		[Export ("visiblePageIndexes", ArgumentSemantic.Copy), NullAllowed]
		NSOrderedSet<NSNumber> VisiblePageIndexes { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFDocumentSharingCoordinatorDelegate Delegate { get; set; }

		[Export ("sharingOptions", ArgumentSemantic.UnsafeUnretained)]
		PSPDFDocumentSharingOptions SharingOptions { get; set; }

		[Export ("available", ArgumentSemantic.Assign)]
		bool Available { [Bind ("isAvailable")] get; }

		[Export ("executing", ArgumentSemantic.Assign)]
		bool Executing { [Bind ("isExecuting")] get; }

		[Export ("presentToViewController:options:sender:animated:completion:")]
		void PresentToViewController ([NullAllowed] IPSPDFPresentationActions targetController, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		// PSPDFDocumentSharingCoordinator (SubclassingHooks)

		[Export ("title")]
		string Title { get; }

		[Export ("commitButtonTitle")]
		string CommitButtonTitle { get; }

		[Export ("policyEvent")]
		string PolicyEvent { get; }

		[Export ("isAvailableUserInvoked:error:"), Internal]
		bool _IsAvailableUserInvoked (bool userInvoked, IntPtr error);

		[Export ("configureSharingController:")]
		bool ConfigureSharingController (PSPDFDocumentSharingViewController sharingController);

		[Export ("sharingController", ArgumentSemantic.Weak)]
		PSPDFDocumentSharingViewController SharingController { get; }

		[Export ("showActionControllerToViewController:sender:sendOptions:files:annotationSummary:animated:")]
		void ShowActionController (IPSPDFPresentationActions targetController, NSObject sender, PSPDFDocumentSharingOptions sendOptions, NSObject [] files, [NullAllowed] NSAttributedString annotationSummary, bool animated);

		// PSPDFDocumentSharingCoordinator (Dependencies)

		[Export ("policy", ArgumentSemantic.Retain)][NullAllowed]
		IPSPDFApplicationPolicy Policy { get; set; }

		[Export ("fileManager", ArgumentSemantic.Retain)][NullAllowed]
		IPSPDFFileManager FileManager { get; set; }
	}

	interface IPSPDFPageControls { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPageControls {

		[Export ("setDocument:")][Abstract]
		bool SetDocument ([NullAllowed] PSPDFDocument document);

		[Export ("setPageIndex:animated:")][Abstract]
		bool SetPageIndex (nuint pageIndex, bool animated);

		[Export ("setPageIndex:options:animated:")][Abstract]
		bool SetPageIndex (nuint pageIndex, [NullAllowed] NSDictionary<NSString, NSNumber> options, bool animated);

		[Export ("scrollToNextPageAnimated:")][Abstract]
		bool ScrollToNextPage (bool animated);

		[Export ("scrollToPreviousPageAnimated:")][Abstract]
		bool ScrollToPreviousPage (bool animated);

		[Export ("setViewMode:animated:")][Abstract]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("executePDFAction:targetRect:pageIndex:animated:actionContainer:")][Abstract]
		bool ExecutePdfAction ([NullAllowed] PSPDFAction action, CGRect targetRect, nuint pageIndex, bool animated, NSObject actionContainer);

		[Export ("searchForString:options:sender:animated:")][Abstract]
		void SearchForString ([NullAllowed] string searchText, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated);

		[Export ("documentActionExecutor")][Abstract]
		PSPDFDocumentActionExecutor DocumentActionExecutor { get; }

		[return: NullAllowed]
		[Export ("presentDocumentInfoViewControllerWithOptions:sender:animated:completion:")][Abstract]
		UIViewController PresentDocumentInfoViewController ([NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completionHandler);

		[Export ("presentPreviewControllerForURL:title:options:sender:animated:completion:")][Abstract]
		void PresentPreviewController (NSUrl fileUrl, [NullAllowed] string title, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Export ("reloadData")][Abstract]
		void ReloadData ();
	}

	interface IPSPDFHUDControls	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFHUDControls {
		
		[Export ("hideControlsAnimated:")]
		[Abstract]
		bool HideControls (bool animated);

		[Export ("hideControlsForUserScrollActionAnimated:")]
		[Abstract]
		bool HideControlsForUserScrollAction (bool animated);

		[Export ("hideControlsAndPageElementsAnimated:")]
		[Abstract]
		bool HideControlsAndPageElements (bool animated);

		[Export ("toggleControlsAnimated:")]
		[Abstract]
		bool ToggleControls (bool animated);

		[Export ("shouldShowControls")]
		[Abstract]
		bool ShouldShowControls { get; }

		[Export ("showControlsAnimated:")]
		[Abstract]
		bool ShowControls (bool animated);

		[Export ("showMenuIfSelectedAnimated:allowPopovers:")]
		[Abstract]
		void ShowMenuIfSelected (bool animated, bool allowPopovers);
	}

	interface IPSPDFBookmarkViewControllerDelegate	{}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFBookmarkViewControllerDelegate {
		
		[Export ("currentPageForBookmarkViewController:")][Abstract]
		nuint CurrentPageForBookmarkViewController (PSPDFBookmarkViewController bookmarkController);

		[Export ("bookmarkViewController:didSelectBookmark:")][Abstract]
		void DidSelectBookmark (PSPDFBookmarkViewController bookmarkController, PSPDFBookmark bookmark);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFBookmarkViewController {

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Retain), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Export ("sortOrder", ArgumentSemantic.Assign)]
		PSPDFBookmarkManagerSortOrder SortOrder { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFBookmarkViewControllerDelegate Delegate { get; set; }

		// PSPDFBookmarkViewController (SubclassingHooks)

		[Export ("updateBookmarkViewAnimated:")]
		void UpdateBookmark (bool animated);

		[Export ("addBookmarkAction:")]
		void AddBookmarkAction ([NullAllowed] NSObject sender);

		[Export ("doneAction:")]
		void DoneAction ([NullAllowed] NSObject sender);
	}

	interface IPSPDFVisiblePagesDataSource	{}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFVisiblePagesDataSource {

		[Abstract]
		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Abstract]
		[Export ("visiblePageIndexes")]
		NSOrderedSet<NSNumber> VisiblePageIndexes { get; }

		[Abstract]
		[Export ("visiblePageIndexesCalculated")]
		NSOrderedSet<NSNumber> VisiblePageIndexesCalculated { get; }
	}

	interface IPSPDFErrorHandler {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFErrorHandler {

		[Export ("handleError:title:message:")][Abstract]
		void HandleError ([NullAllowed] NSError error, [NullAllowed] string title, [NullAllowed] string message);
	}

	interface IPSPDFPresentationActions {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPresentationActions {

		[Abstract]
		[Export ("presentViewController:options:animated:sender:completion:")]
		bool PresentViewController (UIViewController viewController, [NullAllowed] NSDictionary<NSString, NSObject> options, bool animated, [NullAllowed] NSObject sender, [NullAllowed] Action completion);

		[Abstract]
		[Export ("dismissViewControllerOfClass:animated:completion:")]
		bool DismissViewControllerOfClass ([NullAllowed] Class controllerClass, bool animated, [NullAllowed] Action completion);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDataContainerProvider : PSPDFDataProvider {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSData data);
	}

	[Static]
	interface PSPDFDocumentInfoOptionKeys {
		
		[Field ("PSPDFDocumentInfoOptionOutline", "__Internal")]
		NSString Outline { get; }

		[Field ("PSPDFDocumentInfoOptionBookmarks", "__Internal")]
		NSString Bookmarks { get; }

		[Field ("PSPDFDocumentInfoOptionAnnotations", "__Internal")]
		NSString Annotations { get; }

		[Field ("PSPDFDocumentInfoOptionEmbeddedFiles", "__Internal")]
		NSString EmbeddedFiles { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentInfoCoordinator {

		[Export ("presentToViewController:options:sender:animated:completion:")]
		[return: NullAllowed]
		UIViewController PresentToViewController (PSPDFPresentationActions targetController, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Export ("available", ArgumentSemantic.Assign)]
		bool Available { [Bind ("isAvailable")] get; }

		[Export ("document", ArgumentSemantic.Retain)][NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFOverridable Delegate { get; set; }

		[Export ("availableControllerOptions", ArgumentSemantic.Copy)]
		NSString [] AvailableControllerOptions { get; set; }

		[Export ("setDidCreateControllerBlock:")]
		void SetDidCreateControllerHandler ([NullAllowed] Action<UIViewController, NSString> handler);

		// PSPDFDocumentInfoCoordinator (SubclassingHooks)

		[Export ("controllerForOption:")]
		UIViewController ControllerForOption (NSString option);

		[Export ("isOptionAvailable:")]
		bool IsOptionAvailable (NSString option);
	}

	interface IPSPDFAnnotationStyleManager { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStyleManager {

		[Abstract]
		[Export ("styleKeys", ArgumentSemantic.Copy), NullAllowed]
		NSSet StyleKeys { get; set; }

		[Abstract]
		[Export ("shouldUpdateDefaultsForAnnotationChanges")]
		bool ShouldUpdateDefaultsForAnnotationChanges { get; set; }

		[Export ("setupDefaultStylesIfNeeded")][Abstract]
		void SetupDefaultStylesIfNeeded ();

		[Export ("stylesForKey:")][Abstract]
		[return: NullAllowed]
		PSPDFAnnotationStyle [] StylesForKey (NSString key);

		[Export ("addStyle:forKey:")][Abstract]
		void AddStyle (PSPDFAnnotationStyle style, NSString key);

		[Export ("removeStyle:forKey:")][Abstract]
		void RemoveStyle (PSPDFAnnotationStyle style, NSString key);

		[Export ("lastUsedStyleForKey:")][Abstract]
		PSPDFAnnotationStyle LastUsedStyleForKey (NSString key);

		[Export ("lastUsedProperty:forKey:")][Abstract]
		NSObject LastUsedProperty (string styleProperty, NSString key);

		[Export ("setLastUsedValue:forProperty:forKey:")][Abstract]
		void SetLastUsedValue (NSObject value, string styleProperty, NSString key);

		[Export ("defaultPresetsForKey:type:")][Abstract]
		[return: NullAllowed]
		PSPDFModel [] DefaultPresetsForKey (NSString key, NSString type);

		[Export ("presetsForKey:type:")][Abstract]
		PSPDFModel [] PresetsForKey (NSString key, NSString type);

		[Export ("setPresets:forKey:type:")][Abstract]
		void SetPresets ([NullAllowed] PSPDFModel [] presets, NSString key, NSString type);

		[Export ("isPresetModifiedAtIndex:forKey:type:")][Abstract]
		bool IsPresetModifiedAtIndex (nuint index, NSString key, NSString type);

		[Export ("resetPresetAtIndex:forKey:type:")][Abstract]
		bool ResetPresetAtIndex (nuint index, NSString key, NSString type);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultAnnotationStyleManager : IPSPDFAnnotationStyleManager {

	}

	interface IPSPDFControlDelegate	{}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFControlDelegate : PSPDFPresentationActions, PSPDFPageControls, PSPDFHUDControls, PSPDFErrorHandler {

	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFViewController : PSPDFOverridable, PSPDFTextSearchDelegate, 
	PSPDFInlineSearchManagerDelegate, PSPDFErrorHandler, PSPDFExternalURLHandler, PSPDFOutlineViewControllerDelegate, 
	PSPDFBookmarkViewControllerDelegate, PSPDFWebViewControllerDelegate, PSPDFSearchViewControllerDelegate, 
	PSPDFAnnotationTableViewControllerDelegate, PSPDFFlexibleToolbarContainerDelegate, PSPDFBackForwardActionListDelegate, 
	IMFMailComposeViewControllerDelegate, IMFMessageComposeViewControllerDelegate {

		[Notification]
		[Field ("PSPDFViewControllerDidShowPageViewNotification", "__Internal")]
		NSString DidShowPageViewNotification { get; }

		[Notification]
		[Field ("PSPDFViewControllerDidLoadPageViewNotification", "__Internal")]
		NSString DidLoadPageViewNotification { get; }

		[Export ("initWithDocument:configuration:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document, [NullAllowed] PSPDFConfiguration configuration);

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFViewControllerDelegate Delegate { get; set; }

		[Export ("formSubmissionDelegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFFormSubmissionDelegate FormSubmissionDelegate { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; }

		[Export ("setPageIndex:animated:")]
		bool SetPage (nuint pageIndex, bool animated);

		[Export ("scrollToNextPageAnimated:")]
		bool ScrollToNextPage (bool animated);

		[Export ("scrollToPreviousPageAnimated:")]
		bool ScrollToPreviousPage (bool animated);

		[Export ("scrollingEnabled", ArgumentSemantic.Assign)]
		bool ScrollingEnabled { [Bind ("isScrollingEnabled")] get; set; }

		[Export ("viewLockEnabled", ArgumentSemantic.Assign)]
		bool ViewLockEnabled { [Bind ("isViewLockEnabled")] get; set; }

		[Export ("scrollRectToVisible:animated:")]
		void ScrollRectToVisible (CGRect rect, bool animated);

		[Export ("zoomToRect:pageIndex:animated:")]
		void ZoomToRect (CGRect rect, nuint pageIndex, bool animated);

		[Export ("setZoomScale:animated:")]
		void SetZoomScale (nfloat scale, bool animated);

		[Export ("captureCurrentViewState")]
		PSPDFViewState CaptureCurrentViewState ();

		[Export ("applyViewState:animateIfPossible:")]
		void ApplyViewState (PSPDFViewState viewState, bool animateIfPossible);

		[Field ("PSPDFViewControllerSearchHeadlessKey", "__Internal")]
		NSString SearchHeadlessKey { get; }

		[Export ("searchForString:options:sender:animated:")]
		void SearchForString ([NullAllowed] string searchText, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated);

		[Export ("cancelSearchAnimated:")]
		void CancelSearch (bool animated);

		[Export ("searchActive", ArgumentSemantic.Assign)]
		bool SearchActive { [Bind ("isSearchActive")] get; }

		[Export ("searchHighlightViewManager", ArgumentSemantic.Strong)]
		PSPDFSearchHighlightViewManager SearchHighlightViewManager { get; }

		[Export ("inlineSearchManager", ArgumentSemantic.Strong)]
		PSPDFInlineSearchManager InlineSearchManager { get; }

		[Export ("appearanceModeManager", ArgumentSemantic.Strong)]
		PSPDFAppearanceModeManager AppearanceModeManager { get; }

		[Export ("brightnessManager", ArgumentSemantic.Strong)]
		PSPDFBrightnessManager BrightnessManager { get; }

		[Export ("textSearch", ArgumentSemantic.Strong)]
		PSPDFTextSearch TextSearch { get; }

		[Export ("executePDFAction:targetRect:pageIndex:animated:actionContainer:")]
		bool ExecutePdfAction ([NullAllowed] PSPDFAction action, CGRect targetRect, nuint pageIndex, bool animated, [NullAllowed] NSObject actionContainer);

		[Export ("backForwardList", ArgumentSemantic.Strong)]
		PSPDFBackForwardActionList BackForwardList { get; }

		[Export ("HUDView", ArgumentSemantic.Strong)]
		PSPDFHUDView HudView { get; }

		[Export ("HUDVisible", ArgumentSemantic.Assign)]
		bool HudVisible { [Bind ("isHUDVisible")] get; set; }

		[Export ("setHUDVisible:animated:")]
		bool SetHudVisible (bool show, bool animated);

		[Export ("showControlsAnimated:")]
		bool ShowControls (bool animated);

		[Export ("hideControlsAnimated:")]
		bool HideControls (bool animated);

		[Export ("hideControlsAndPageElementsAnimated:")]
		bool HideControlsAndPageElements (bool animated);

		[Export ("toggleControlsAnimated:")]
		bool ToggleControls (bool animated);

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRelayTouchesView ContentView { get; }

		[Export ("navigationBarHidden", ArgumentSemantic.Assign)]
		bool NavigationBarHidden { [Bind ("isNavigationBarHidden")] get; }

		[Export ("controllerState")]
		PSPDFControllerState ControllerState { get; }

		[Export ("controllerStateError"), NullAllowed]
		NSError ControllerStateError { get; }

		[Export ("overlayViewController"), NullAllowed]
		IPSPDFControllerStateHandling OverlayViewController { get; set; }

		[Export ("pageViewForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageView GetPageView (nuint pageIndex);

		[Export ("pagingScrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Export ("viewMode", ArgumentSemantic.Assign)]
		PSPDFViewMode ViewMode { get; set; }

		[Export ("setViewMode:animated:")]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("thumbnailController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailViewController ThumbnailController { get; }

		[Export ("documentEditorController", ArgumentSemantic.Strong)]
		PSPDFDocumentEditorViewController DocumentEditorController { get; }

		[Export ("visiblePageIndexes"), NullAllowed]
		NSOrderedSet<NSNumber> VisiblePageIndexes { get; }

		[Export ("doublePageMode")]
		bool DoublePageMode { [Bind ("isDoublePageMode")] get; }

		[Export ("lastPage")]
		bool LastPage { [Bind ("isLastPage")] get; }

		[Export ("firstPage")]
		bool FirstPage { [Bind ("isFirstPage")] get; }

		[Export ("setUpdateSettingsForBoundsChangeBlock:")]
		void SetUpdateSettingsForBoundsChange (Action<PSPDFViewController> handler);

		// PSPDFViewController (Configuration) Category

		[Export ("configuration", ArgumentSemantic.Copy)]
		PSPDFConfiguration Configuration { get; }

		[Export ("updateConfigurationWithBuilder:")]
		void UpdateConfiguration (Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("updateConfigurationWithoutReloadingWithBuilder:")]
		void UpdateConfigurationWithoutReloading (Action<PSPDFConfigurationBuilder> builderHandler);

		// PSPDFViewController (Presentation) Category

		[Export ("presentViewController:options:animated:sender:completion:")]
		bool PresentViewController (UIViewController controller, [NullAllowed] NSDictionary<NSString, NSObject> options, bool animated, [NullAllowed] NSObject sender, [NullAllowed] Action completion);

		[Export ("dismissViewControllerOfClass:animated:completion:")]
		bool DismissViewController ([NullAllowed] Class controllerClass, bool animated, [NullAllowed] Action completion);

		[Export ("presentPDFViewControllerWithDocument:options:sender:animated:configurationBlock:completion:")]
		void PresentPdfViewController (PSPDFDocument document, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action<PSPDFViewController> configurationHandler, [NullAllowed] Action completion);

		[Export ("presentPreviewControllerForURL:title:options:sender:animated:completion:")]
		void PresentPreviewController (NSUrl fileUrl, [NullAllowed] string title, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] NSObject sender, bool animated, [NullAllowed] Action completion);

		[Export ("activityViewControllerWithSender:")]
		UIActivityViewController GetActivityViewController (NSObject sender);

		// PSPDFViewController (Annotations) Category

		[Export ("annotationStateManager", ArgumentSemantic.Strong)]
		PSPDFAnnotationStateManager AnnotationStateManager { get; }

		// PSPDFViewController (Toolbar) Category

		[Export ("navigationItem")][New]
		PSPDFNavigationItem NavigationItem { get; }

		[Export ("closeButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem CloseButtonItem { get; }

		[Export ("outlineButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem OutlineButtonItem { get; }

		[Export ("searchButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem SearchButtonItem { get; }

		[Export ("thumbnailsButtonItem")]
		UIBarButtonItem ThumbnailsButtonItem { get; }

		[Export ("documentEditorButtonItem")]
		UIBarButtonItem DocumentEditorButtonItem { get; }

		[Export ("printButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem PrintButtonItem { get; }

		[Export ("openInButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem OpenInButtonItem { get; }

		[Export ("emailButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem EmailButtonItem { get; }

		[Export ("messageButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem MessageButtonItem { get; }

		[Export ("annotationButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem AnnotationButtonItem { get; }

		[Export ("bookmarkButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem BookmarkButtonItem { get; }

		[Export ("brightnessButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem BrightnessButtonItem { get; }

		[Export ("activityButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem ActivityButtonItem { get; }

		[Export ("settingsButtonItem", ArgumentSemantic.Strong)]
		UIBarButtonItem SettingsButtonItem { get; }

		[Export ("barButtonItemsAlwaysEnabled", ArgumentSemantic.Copy)]
		UIBarButtonItem [] BarButtonItemsAlwaysEnabled { get; set; }

		[Export ("documentActionExecutor", ArgumentSemantic.Strong)]
		PSPDFDocumentActionExecutor DocumentActionExecutor { get; }

		[Export ("documentInfoCoordinator", ArgumentSemantic.Strong)]
		PSPDFDocumentInfoCoordinator DocumentInfoCoordinator { get; }

		[Export ("annotationToolbarController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationToolbarController AnnotationToolbarController { get; }

		// PSPDFViewController (SubclassingHooks) Category

		[Advice ("Requires base call if override")]
		[Export ("commonInitWithDocument:configuration:")]
		void CommonInitWithDocument ([NullAllowed] PSPDFDocument document, PSPDFConfiguration configuration);

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("contentRect")]
		CGRect ContentRect { get; }

		[Export ("updatepageIndex:animated:")]
		void UpdatePage (nuint pageIndex, bool animated);

		[Export ("annotationButtonPressed:")]
		void AnnotationButtonPressed ([NullAllowed] NSObject sender);
	}

	interface IPSPDFPresentableViewController { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPresentableViewController {

	}

	interface IPSPDFHostableViewController	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFHostableViewController {

	}

	interface IPSPDFDatabaseEncryptionProvider { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDatabaseEncryptionProvider {

		[Export ("encryptDatabase:withKey:")][Abstract]
		bool EncryptDatabase (IntPtr db, NSData keyData);

		[Export ("reEncryptDatabase:withKey:")][Abstract]
		bool ReEncryptDatabase (IntPtr db, NSData keyData);
	}

	interface IPSPDFViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFViewControllerDelegate {

		[Export ("pdfViewController:shouldChangeDocument:")]
		bool ShouldChangeDocument (PSPDFViewController pdfController, [NullAllowed] PSPDFDocument document);

		[Export ("pdfViewController:didChangeDocument:")]
		void DidChangeDocument (PSPDFViewController pdfController, [NullAllowed] PSPDFDocument document);

		[Export ("pdfViewController:shouldScrollToPageAtIndex:")]
		bool ShouldScrollToPage (PSPDFViewController pdfController, nuint pageIndex);

		[Export ("pdfViewController:didShowPageView:")]
		void DidShowPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didRenderPageView:")]
		void DidRenderPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didLoadPageView:")]
		void DidLoadPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:willUnloadPageView:")]
		void WillUnloadPageView (PSPDFViewController pdfController, PSPDFPageView pageView);

		[Export ("pdfViewController:didBeginPageDragging:")]
		void DidBeginPageDragging (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didEndPageDragging:willDecelerate:withVelocity:targetContentOffset:")]
		void DidEndPageDragging (PSPDFViewController pdfController, UIScrollView scrollView, bool decelerate, CGPoint velocity, CGPoint targetContentOffset);

		[Export ("pdfViewController:didEndPageScrollingAnimation:")]
		void DidEndPageScrollingAnimation (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didBeginPageZooming:")]
		void DidBeginPageZooming (PSPDFViewController pdfController, UIScrollView scrollView);

		[Export ("pdfViewController:didEndPageZooming:atScale:")]
		void DidEndPageZooming (PSPDFViewController pdfController, UIScrollView scrollView, nfloat scale);

		[Export ("pdfViewController:documentForRelativePath:")]
		PSPDFDocument DocumentForRelativePath (PSPDFViewController pdfController, string relativePath);

		[Export ("pdfViewController:didTapOnPageView:atPoint:")]
		bool DidTapOnPageView (PSPDFViewController pdfController, PSPDFPageView pageView, CGPoint viewPoint);

		[Export ("pdfViewController:didLongPressOnPageView:atPoint:gestureRecognizer:")]
		bool DidLongPressOnPageView (PSPDFViewController pdfController, PSPDFPageView pageView, CGPoint viewPoint, UILongPressGestureRecognizer gestureRecognizer);

		[Export ("pdfViewController:shouldSelectText:withGlyphs:atRect:onPageView:")]
		bool ShouldSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectText:withGlyphs:atRect:onPageView:")]
		void DidSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph [] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedText:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedText (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, string selectedText, CGRect textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedImage:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedImage (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, PSPDFImageInfo selectedImage, CGRect textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forAnnotations:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForAnnotations (PSPDFViewController pdfController, PSPDFMenuItem [] menuItems, CGRect rect, [NullAllowed] PSPDFAnnotation [] annotations, CGRect annotationRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldDisplayAnnotation:onPageView:")]
		bool ShouldDisplayAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:didTapOnAnnotation:annotationPoint:annotationView:pageView:viewPoint:")]
		bool DidTapOnAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, CGPoint annotationPoint, [NullAllowed] IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView, CGPoint viewPoint);

		[Export ("pdfViewController:shouldSelectAnnotations:onPageView:")]
		PSPDFAnnotation [] ShouldSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectAnnotations:onPageView:")]
		void DidSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation [] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:annotationView:forAnnotation:onPageView:")]
		IPSPDFAnnotationViewProtocol AnnotationView (PSPDFViewController pdfController, [NullAllowed] IPSPDFAnnotationViewProtocol annotationView, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:willShowAnnotationView:onPageView:")]
		void WillShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:didShowAnnotationView:onPageView:")]
		void DidShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowController:options:animated:")]
		bool ShouldShowController (PSPDFViewController pdfController, UIViewController controller, [NullAllowed] NSDictionary<NSString, NSObject> options, bool animated);

		[Export ("pdfViewController:didShowController:options:animated:")]
		void PdfViewController (PSPDFViewController pdfController, UIViewController controller, [NullAllowed] NSDictionary<NSString, NSObject> options, bool animated);

		[Export ("pdfViewController:didChangeViewMode:")]
		void DidChangeViewMode (PSPDFViewController pdfController, PSPDFViewMode viewMode);

		[Export ("pdfViewControllerWillDismiss:")]
		void PdfViewControllerWillDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewControllerDidDismiss:")]
		void PdfViewControllerDidDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewControllerDidChangeControllerState:")]
		void PdfViewControllerDidChangeControllerState (PSPDFViewController pdfController);

		[Export ("pdfViewController:shouldShowHUD:")]
		bool ShouldShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didShowHUD:")]
		void DidShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldHideHUD:")]
		bool ShouldHideHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didHideHUD:")]
		void DidHideHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldExecuteAction:")]
		void ShouldExecuteAction (PSPDFViewController pdfController, PSPDFAction action);

		[Export ("pdfViewController:didExecuteAction:")]
		void DidExecuteAction (PSPDFViewController pdfController, PSPDFAction action);
	}

	interface PSPDFDocumentUnderlyingFileChangedNotificationEventArgs {
		[Export ("PSPDFDocumentUnderlyingFileURLKey")]
		NSUrl UnderlyingFileUrl { get; }
	}

	delegate void PSPDFDocumentSaveHandler (NSError error, PSPDFAnnotation [] savedAnnotations);

	[BaseType (typeof (NSObject))]
	interface PSPDFDocument : PSPDFDocumentProviderDelegate, PSPDFOverridable, INSCopying, INSSecureCoding {

		[Field ("PSPDFDocumentUnderlyingFileChangedNotification", "__Internal")]
		[Notification (typeof (PSPDFDocumentUnderlyingFileChangedNotificationEventArgs))]
		NSString UnderlyingFileChangedNotification { get; }

		[Static]
		[Export ("document")]
		PSPDFDocument Document { get; }

		[Static]
		[Export ("documentWithURL:")]
		PSPDFDocument FromUrl (NSUrl url);

		[Static]
		[Export ("documentWithData:")]
		PSPDFDocument FromData (NSData data);

		[Static]
		[Export ("documentWithDataArray:")]
		PSPDFDocument FromData (NSData [] data);

		[Static]
		[Export ("documentWithDataProvider:")]
		PSPDFDocument FromDataProvider (IPSPDFDataProvider dataProvider);

		[Static]
		[Export ("documentWithDataProviderArray:")]
		PSPDFDocument FromDataProvider (IPSPDFDataProvider [] dataProviders);

		[Static]
		[Export ("documentWithBaseURL:files:")]
		PSPDFDocument FromBaseUrl ([NullAllowed] NSUrl baseUrl, string [] files);

		[Static]
		[Export ("documentWithBaseURL:fileTemplate:startPage:endPage:")]
		PSPDFDocument FromBaseUrl ([NullAllowed] NSUrl baseUrl, string fileTemplate, nint startPage, nint endPage);

		[Static]
		[Export ("documentWithContent:")]
		PSPDFDocument FromContent (NSObject content);

		[Static]
		[Export ("documentWithContent:signatures:")]
		PSPDFDocument FromContent (NSObject content, [NullAllowed] NSData [] signatures);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		[Export ("initWithDataArray:")]
		IntPtr Constructor (NSData [] data);

		[Export ("initWithDataProvider:")]
		IntPtr Constructor (IPSPDFDataProvider dataProvider);

		[Export ("initWithDataProviderArray:")]
		IntPtr Constructor (IPSPDFDataProvider [] dataProviders);

		[Export ("initWithBaseURL:files:")]
		IntPtr Constructor ([NullAllowed] NSUrl baseUrl, string [] files);

		[Export ("initWithBaseURL:fileTemplate:startPage:endPage:")]
		IntPtr Constructor ([NullAllowed] NSUrl baseUrl, string fileTemplate, nint startPage, nint endPage);

		[Export ("initWithContent:")]
		IntPtr Constructor (NSObject content);

		[DesignatedInitializer]
		[Export ("initWithContent:signatures:")]
		IntPtr Constructor ([NullAllowed] NSObject content, [NullAllowed] NSData [] signatures);

		[Export ("isEqualToDocument:")]
		bool IsEqualToDocument (PSPDFDocument otherDocument);

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFDocumentDelegate Delegate { get; set; }

		[Export ("baseURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl BaseUrl { get; }

		[Export ("files"), NullAllowed]
		string [] Files { get; }

		[Export ("fileURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl FileUrl { get; }

		[Export ("originalFile", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFile OriginalFile { get; }

		[Export ("filesWithBasePath", ArgumentSemantic.Copy)]
		NSUrl [] FilesWithBasePath { get; }

		[Export ("data", ArgumentSemantic.Copy), NullAllowed]
		NSData Data { get; }

		[Export ("dataArray"), NullAllowed]
		NSData [] DataArray { get; }

		[Export ("fileNamesWithDataDictionary")]
		NSDictionary<NSString, NSData> FileNamesWithDataDictionary { get; }

		[Export ("dataProviderArray", ArgumentSemantic.Copy), NullAllowed]
		IPSPDFDataProvider [] DataProviderArray { get; }

		[Export ("contentSignatures"), NullAllowed]
		NSData [] ContentSignatures { get; }

		[Export ("documentByAppendingObjects:")]
		PSPDFDocument DocumentByAppendingObjects (NSObject [] objects);

		[Export ("documentId", ArgumentSemantic.Copy)]
		NSData DocumentId { get; }

		[Export ("documentIdString")]
		string DocumentIdString { get; }

		[Export ("UID"), NullAllowed]
		string Uid { get; set; }

		[Export ("valid", ArgumentSemantic.Assign)]
		bool Valid { [Bind ("isValid")] get; }

		[Export ("error"), NullAllowed]
		NSError Error { get; }

		[Export ("documentProviders")]
		PSPDFDocumentProvider [] DocumentProviders { get; }

		[Export ("documentProviderForPageAtIndex:")]
		PSPDFDocumentProvider DocumentProviderForPage (nuint pageIndex);

		[Export ("pageOffsetForDocumentProvider:")]
		nuint PageOffsetForDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("pathForPageAtIndex:")]
		NSUrl PathForPage (nuint pageIndex);

		[Export ("fileIndexForPageAtIndex:")]
		nint FileIndexForPage (nuint pageIndex);

		[Export ("URLForFileIndex:")]
		NSUrl UrlForFileIndex (nuint fileIndex);

		[Export ("fileNameForPageAtIndex:")]
		string GetFileName (nuint pageIndex);

		[Export ("fileName")]
		string FileName { get; }

		[Export ("pageCount", ArgumentSemantic.Assign)]
		nuint PageCount { get; }

		[Export ("pageInfoForPageAtIndex:")]
		PSPDFPageInfo GetPageInfo (nuint pageIndex);

		[Export ("nearestPageInfoForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageInfo GetNearestPageInfo (nuint pageIndex);

		[Export ("alwaysRewriteOnSave", ArgumentSemantic.Assign)]
		bool AlwaysRewriteOnSave { get; set; }

		[Export ("save:")]
		bool Save (out NSError error);

		[Export ("saveWithCompletionHandler:")]
		void Save ([NullAllowed] PSPDFDocumentSaveHandler completionHandler);

		[Export ("deleteFiles:")]
		bool DeleteFiles (out NSError error);

		// PSPDFDocument (Caching) Category

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("fillCache")]
		void FillCache ();

		[Export ("dataDirectory", ArgumentSemantic.Copy)]
		string DataDirectory { get; set; }

		[Export ("ensureDataDirectoryExistsWithError:")] [Internal]
		bool _EnsureDataDirectoryExists (IntPtr error);

		// PSPDFDocument (Security) Category

		[Export ("unlockWithPassword:")]
		bool Unlock (string password);

		[Export ("lock")]
		void Lock ();

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("encryptionFilter"), NullAllowed]
		string EncryptionFilter { get; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("allowAnnotationChanges", ArgumentSemantic.Assign)]
		bool AllowAnnotationChanges { get; }

		// PSPDFDocument (Bookmarks) Category

		[Export ("bookmarksEnabled", ArgumentSemantic.Assign)]
		bool BookmarksEnabled { [Bind ("isBookmarksEnabled")] get; set; }

		[Export ("bookmarkManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBookmarkManager BookmarkManager { get; set; }

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		// PSPDFDocument (PageLabels) Category

		[Export ("pageLabelsEnabled", ArgumentSemantic.Assign)]
		bool PageLabelsEnabled { [Bind ("isPageLabelsEnabled")] get; set; }

		[Export ("pageLabelForPageAtIndex:substituteWithPlainLabel:")]
		[return: NullAllowed]
		string PageLabelForPage (nuint pageIndex, bool substitute);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint PageForPageLabel (string pageLabel, bool partialMatching);

		// PSPDFDocument (Forms) Category

		[Export ("formsEnabled", ArgumentSemantic.Assign)]
		bool FormsEnabled { [Bind ("isFormsEnabled")] get; set; }

		[Export ("javaScriptEnabled", ArgumentSemantic.Assign)]
		bool JavaScriptEnabled { [Bind ("isJavaScriptEnabled")] get; set; }

		[Export ("formParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFormParser FormParser { get; }

		// PSPDFDocument (EmbeddedFiles) Category

		[Export ("allEmbeddedFiles")]
		PSPDFEmbeddedFile [] AllEmbeddedFiles { get; }

		// PSPDFDocument (Annotations) Category

		[Export ("annotationsEnabled", ArgumentSemantic.Assign)]
		bool AnnotationsEnabled { [Bind ("isAnnotationsEnabled")] get; set; }

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[return: NullAllowed]
		[Export ("annotationsForPageAtIndex:type:")]
		PSPDFAnnotation [] GetAnnotationsForPage (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> AllAnnotationsOfType (PSPDFAnnotationType annotationType);

		[Export ("containsAnnotations")]
		bool ContainsAnnotations { get; }

		// PSPDFDocument (AnnotationSaving) Category

		[Notification]
		[Field ("PSPDFDocumentWillSaveAnnotationsNotification", "__Internal")]
		NSString WillSaveAnnotationsNotification { get; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; }

		[Export ("canSaveAnnotations", ArgumentSemantic.Assign)]
		bool CanSaveAnnotations { get; }

		[Export ("annotationSaveMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationSaveMode AnnotationSaveMode { get; set; }

		[Field ("PSPDFDocumentDefaultAnnotationUsernameKey", "__Internal")]
		NSString DefaultAnnotationUsernameKey { get; }

		[Export ("defaultAnnotationUsername"), NullAllowed]
		string DefaultAnnotationUsername { get; set; }

		[Export ("annotationWritingOptions", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSNumber> AnnotationWritingOptions { get; set; }

		[Export ("hasDirtyAnnotations", ArgumentSemantic.Assign)]
		bool HasDirtyAnnotations { get; }

		// PSPDFDocument (Rendering) Category

		[Export ("imageForPageAtIndex:size:clippedToRect:annotations:options:error:")]
		[return: NullAllowed]
		UIImage ImageForPage (nuint pageIndex, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options, out NSError error);

		[Export ("renderPageAtIndex:context:size:clippedToRect:annotations:options:error:")]
		[return: NullAllowed]
		bool RenderPage (nuint pageIndex, CGContext context, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options, out NSError error);

		[Export ("setRenderOptions:type:")]
		void SetRenderOptions ([NullAllowed] NSDictionary<NSString, NSObject> options, PSPDFRenderType type);

		[Export ("updateRenderOptions:type:")]
		void UpdateRenderOptions ([NullAllowed] NSDictionary<NSString, NSObject> options, PSPDFRenderType type);

		[Export ("renderOptionsForType:context:")]
		NSDictionary<NSString, NSObject> GetRenderOptions (PSPDFRenderType type, [NullAllowed] NSObject context);

		[Export ("renderAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType RenderAnnotationTypes { get; set; }

		// PSPDFDocument (Metadata) Category

		[Export ("title"), NullAllowed]
		string Title { get; set; }

		[Export ("titleLoaded", ArgumentSemantic.Assign)]
		bool TitleLoaded { [Bind ("isTitleLoaded")] get; }

		[Export ("metadata")]
		NSDictionary<NSString, NSObject> Metadata { get; }

		[Export ("searchableItemAttributeSetWithThumbnail:")]
		CSSearchableItemAttributeSet GetSearchableItemAttributeSetWithThumbnail (bool renderThumbnail);

		// PSPDFDocument (SubclassingHooks) Category

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Wrap ("OverrideClass (new Class (builtinType), new Class (subType))")]
		void OverrideClass (Type builtinType, Type subType);

		[Export ("didCreateDocumentProvider:")]
		PSPDFDocumentProvider DidCreateDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("didCreateDocumentProviderBlock", ArgumentSemantic.Copy), NullAllowed]
		Action<PSPDFDocumentProvider> DidCreateDocumentProviderHandler { get; set; }

		[Export ("renderOptionsForPage:")]
		NSDictionary<NSString, NSObject> RenderOptionsForPage (nuint page);

		[Export ("fileNameForIndex:")]
		string FileNameForIndex (nuint fileIndex);

		// PSPDFDocument (Advanced) Category

		[Export ("undoEnabled", ArgumentSemantic.Assign)]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; set; }

		[Export ("undoController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFUndoController UndoController { get; }

		[Export ("relativePageIndexForPageAtIndex:")]
		nuint GetRelativePageIndex (nuint pageIndex);

		[Export ("pspdfkit", ArgumentSemantic.Retain)]
		PSPDFKitGlobal PsPdfKit { get; }

		// PSPDFDocument (DataDetection) Category

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsFromDetectingLinkTypes:pagesInRange:options:progress:error:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> AnnotationsFromDetectingLinkTypes (PSPDFTextCheckingType textLinkTypes, NSIndexSet pageRange, [NullAllowed] NSDictionary<NSString, NSDictionary<NSNumber, NSArray<PSPDFAnnotation>>> options, [NullAllowed] PSPDFDocumentDetectingLinkTypesHandler progressHandler, out NSError error);

		// PSPDFDocument (Library) Category

		[Export ("libraryMetadata", ArgumentSemantic.Copy)]
		NSDictionary LibraryMetadata { get; set; }

		[Static]
		[Export ("serializeLibraryMetadata:error:")]
		NSData SerializeLibraryMetadata (NSDictionary metadata, out NSError error);

		[Static]
		[Export ("deserializeLibraryMetadata:error:")]
		NSDictionary DeserializeLibraryMetadata (NSData data, out NSError error);

		[Static]
		[Export ("validateLibraryMetadata:")]
		bool ValidateLibraryMetadata (NSDictionary metadata);

		// PSPDFDocument (ObjectFinder) Category

		[Export ("objectsAtPDFPoint:pageIndex:options:")]
		NSDictionary<NSString, NSObject> GetObjectsAtPdfPoint (CGPoint pdfPoint, nuint pageIndex, [NullAllowed] NSDictionary<NSString, NSNumber> options);

		[Export ("objectsAtPDFRect:pageIndex:options:")]
		NSDictionary<NSString, NSObject> GetObjectsAtPdfRect (CGRect pdfRect, nuint pageIndex, [NullAllowed] NSDictionary<NSString, NSNumber> options);
	}

	delegate void PSPDFDocumentDetectingLinkTypesHandler (PSPDFAnnotation [] annotations, nuint page, ref bool stop);

	[BaseType (typeof (UIView))]
	interface PSPDFRelayTouchesView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	[Static]
	interface PSPDFObjects {

		// PSPDFDocument (ObjectFinder) Category

		[Field ("PSPDFObjectsGlyphsKey", "__Internal")]
		NSString GlyphsKey { get; }

		[Field ("PSPDFObjectsWordsKey", "__Internal")]
		NSString WordsKey { get; }

		[Field ("PSPDFObjectsTextKey", "__Internal")]
		NSString TextKey { get; }

		[Field ("PSPDFObjectsTextBlocksKey", "__Internal")]
		NSString TextBlocksKey { get; }

		[Field ("PSPDFObjectsImagesKey", "__Internal")]
		NSString ImagesKey { get; }

		[Field ("PSPDFObjectsAnnotationsKey", "__Internal")]
		NSString AnnotationsKey { get; }

		[Field ("PSPDFObjectsIgnoreLargeTextBlocksKey", "__Internal")]
		NSString IgnoreLargeTextBlocksKey { get; }

		[Field ("PSPDFObjectsAnnotationTypesKey", "__Internal")]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFObjectsAnnotationPageBoundsKey", "__Internal")]
		NSString AnnotationPageBoundsKey { get; }

		[Field ("PSPDFObjectsPageZoomLevelKey", "__Internal")]
		NSString PageZoomLevelKey { get; }

		[Field ("PSPDFObjectsAnnotationIncludedGroupedKey", "__Internal")]
		NSString AnnotationIncludedGroupedKey { get; }

		[Field ("PSPDFObjectsSmartSortKey", "__Internal")]
		NSString SmartSortKey { get; }

		[Field ("PSPDFObjectMinDiameterKey", "__Internal")]
		NSString MinDiameterKey { get; }

		[Field ("PSPDFObjectsTextFlowKey", "__Internal")]
		NSString TextFlowKey { get; }

		[Field ("PSPDFObjectsFindFirstOnlyKey", "__Internal")]
		NSString FindFirstOnlyKey { get; }

		[Field ("PSPDFObjectsTestIntersectionKey", "__Internal")]
		NSString TestIntersectionKey { get; }
	}

	[Static]
	interface PSPDFMetadataName {
		
		[Field ("PSPDFMetadataTitleKey", "__Internal")]
		NSString TitleKey { get; }

		[Field ("PSPDFMetadataAuthorKey", "__Internal")]
		NSString AuthorKey { get; }

		[Field ("PSPDFMetadataSubjectKey", "__Internal")]
		NSString SubjectKey { get; }

		[Field ("PSPDFMetadataKeywordsKey", "__Internal")]
		NSString KeywordsKey { get; }

		[Field ("PSPDFMetadataCreatorKey", "__Internal")]
		NSString CreatorKey { get; }

		[Field ("PSPDFMetadataProducerKey", "__Internal")]
		NSString ProducerKey { get; }

		[Field ("PSPDFMetadataCreationDateKey", "__Internal")]
		NSString CreationDateKey { get; }

		[Field ("PSPDFMetadataModDateKey", "__Internal")]
		NSString ModDateKey { get; }

		[Field ("PSPDFMetadataTrappedKey", "__Internal")]
		NSString TrappedKey { get; }
	}

	interface IPSPDFDocumentDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentDelegate	{

		[Export ("pdfDocument:didRenderPageAtIndex:inContext:withSize:clippedToRect:annotations:options:")]
		void DidRenderPage (PSPDFDocument document, nuint pageIndex, CGContext context, CGSize fullSize, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("pdfDocument:resolveCustomAnnotationPathToken:")]
		string ResolveCustomAnnotationPathToken (PSPDFDocument document, string pathToken);

		[Export ("pdfDocument:provider:shouldSaveAnnotations:")]
		bool ShouldSaveAnnotations (PSPDFDocument document, PSPDFDocumentProvider documentProvider, PSPDFAnnotation [] annotations);

		[Export ("pdfDocument:didSaveAnnotations:")]
		void DidSaveAnnotations (PSPDFDocument document, PSPDFAnnotation [] annotations);

		[Export ("pdfDocument:failedToSaveAnnotations:error:")]
		void FailedToSaveAnnotations (PSPDFDocument document, PSPDFAnnotation [] annotations, NSError error);

		[Export ("pdfDocumentDidSave:")]
		void DocumentDidSave (PSPDFDocument document);

		[Export ("pdfDocument:saveDidFailWithError:")]
		void SaveDidFail (PSPDFDocument document, NSError error);

		[Export ("pdfDocument:underlyingFileDidChange:")]
		void UnderlyingFileDidChange (PSPDFDocument document, NSUrl fileUrl);
	}

	interface IPSPDFPageViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPageViewDelegate : PSPDFOverridable {

	}

	[BaseType (typeof (PSPDFRelayTouchesView))]
	interface PSPDFAnnotationContainerView {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFPageView : PSPDFRenderTaskDelegate, PSPDFResizableViewDelegate, PSPDFAnnotationGridViewControllerDelegate, 
	IUIScrollViewDelegate, PSPDFImagePickerControllerDelegate {

		[Notification]
		[Field ("PSPDFPageViewSelectedAnnotationsDidChangeNotification", "__Internal")]
		NSString SelectedAnnotationsDidChangeNotification { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("displayPageIndex:pageRect:scale:presentationContext:")]
		void DisplayPageIndex (nuint pageIndex, CGRect pageRect, nfloat scale, IPSPDFPresentationContext presentationContext);

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; }

		[Advice ("Requires base call if override")]
		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("updateRenderView")]
		void UpdateRenderView ();

		[Export ("updateView")]
		void UpdateView ();

		[Export ("annotationViewForAnnotation:")]
		IPSPDFAnnotationViewProtocol AnnotationViewForAnnotation (PSPDFAnnotation annotation);

		[Export ("contentView", ArgumentSemantic.Strong)]
		UIImageView ContentView { get; }

		[Export ("renderView", ArgumentSemantic.Strong)]
		UIImageView RenderView { get; }

		[Export ("annotationContainerView", ArgumentSemantic.Strong)]
		PSPDFAnnotationContainerView AnnotationContainerView { get; }

		[Export ("selectionView", ArgumentSemantic.Strong)]
		PSPDFTextSelectionView SelectionView { get; }

		[Export ("renderStatusView", ArgumentSemantic.Strong), NullAllowed]
		UIView RenderStatusView { get; set; }

		[Export ("renderStatusViewOffset", ArgumentSemantic.Assign)]
		nfloat RenderStatusViewOffset { get; set; }

		[Export ("PDFScale", ArgumentSemantic.Assign)]
		nfloat PdfScale { get; }

		[Export ("visibleRect", ArgumentSemantic.Assign)]
		CGRect VisibleRect { get; }

		[Export ("highlightColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor HighlightColor { get; set; }

		[Export ("convertViewPointToPDFPoint:")]
		CGPoint ConvertViewPointToPDFPoint (CGPoint viewPoint);

		[Export ("convertPDFPointToViewPoint:")]
		CGPoint ConvertPdfPointToViewPoint (CGPoint pdfPoint);

		[Export ("convertViewRectToPDFRect:")]
		CGRect ConvertViewRectToPdfRect (CGRect viewRect);

		[Export ("convertPDFRectToViewRect:")]
		CGRect ConvertPdfRectToViewRect (CGRect pdfRect);

		[Export ("objectsAtPoint:options:")]
		NSDictionary<NSString, NSObject> ObjectsAtPoint (CGPoint viewPoint, [NullAllowed] NSDictionary<NSString, NSNumber> options);

		[Export ("objectsAtRect:options:")]
		NSDictionary<NSString, NSObject> ObjectsAtRect (CGRect viewRect, [NullAllowed] NSDictionary<NSString, NSNumber> options);

		[Export ("scrollView"), NullAllowed]
		PSPDFScrollView ScrollView { get; }

		[Export ("visibleAnnotationViews")]
		IPSPDFAnnotationViewProtocol [] VisibleAnnotationViews { get; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; }

		[Export ("pageInfo", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageInfo PageInfo { get; }

		[Export ("rightPage", ArgumentSemantic.Assign)]
		bool RightPage { [Bind ("isRightPage")] get; }

		// PSPDFPageView (AnnotationViews) Category

		[Export ("setAnnotation:forAnnotationView:")]
		void SetAnnotation (PSPDFAnnotation annotation, IPSPDFAnnotationViewProtocol annotationView);

		[Export ("annotationForAnnotationView:")]
		PSPDFAnnotation AnnotationForAnnotationView (IPSPDFAnnotationViewProtocol annotationView);

		[Export ("selectedAnnotations", ArgumentSemantic.Copy), NullAllowed]
		PSPDFAnnotation [] SelectedAnnotations { get; set; }

		[Export ("singleTapped:")]
		bool SingleTapped (UITapGestureRecognizer recognizer);

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("addAnnotation:options:animated:")]
		void AddAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSNumber> options, bool animated);

		[Export ("removeAnnotation:options:animated:")]
		bool RemoveAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSNumber> options, bool animated);

		[Export ("selectAnnotation:animated:")]
		void SelectAnnotation (PSPDFAnnotation annotation, bool animated);

		// PSPDFPageView (SubclassingHooks) Category

		[Export ("updateShadowAnimated:")]
		void UpdateShadow (bool animated);

		[Export ("insertAnnotations:forPageAtIndex:inDocument:")]
		void InsertAnnotations (PSPDFAnnotation [] annotations, nuint pageIndex, PSPDFDocument document);

		[Export ("tappableAnnotationsAtPoint:")]
		PSPDFAnnotation [] TappableAnnotations (CGPoint viewPoint);

		[Export ("tappableAnnotationsForLongPressAtPoint:")]
		PSPDFAnnotation [] TappableAnnotationsForLongPress (CGPoint viewPoint);

		[Export ("hitTestRectForPoint:")]
		CGRect HitTestRectForPoint (CGPoint viewPoint);

		[Export ("singleTappedAtViewPoint:")]
		bool SingleTappedAtViewPoint (CGPoint viewPoint);

		[Export ("didDeselectAnnotations:")]
		void DidDeselectAnnotations (PSPDFAnnotation [] annotations);

		[Export ("rectForAnnotations:")]
		CGRect RectForAnnotations (PSPDFAnnotation [] annotations);

		[Export ("renderOptionsDictWithZoomScale:animated:")]
		NSDictionary<NSString, NSObject> RenderOptionsDict (nfloat zoomScale, bool animated);

		[Export ("annotationSelectionView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFResizableView AnnotationSelectionView { get; }

		[Export ("centerAnnotation:aroundPDFPoint:")]
		void CenterAnnotation (PSPDFAnnotation annotation, CGPoint pdfPoint);

		[Export ("loadPageAnnotationsAnimated:blockWhileParsing:")]
		void LoadPageAnnotations (bool animated, bool blockWhileParsing);

		[Export ("scaleForPageView")]
		nfloat ScaleForPageView { get; }

		[Export ("parentViewController")]
		UIViewController ParentViewController { get; }

		[Advice ("Requires base call if overridden")]
		[Export ("annotationsAddedNotification:")]
		void AnnotationsAddedNotification (NSNotification notification);

		[Advice ("Requires base call if overridden")]
		[Export ("annotationsRemovedNotification:")]
		void AnnotationsRemovedNotification (NSNotification notification);

		[Advice ("Requires base call if overridden")]
		[Export ("annotationChangedNotification:")]
		void AnnotationChangedNotification (NSNotification notification);

		[Export ("shouldScaleAnnotationWhenResizing:usesResizeKnob:")]
		bool ShouldScaleAnnotationWhenResizing (PSPDFAnnotation annotation, bool usesResizeKnob);

		[Export ("updateAnnotationSelectionView")]
		void UpdateAnnotationSelectionView ();

		// PSPDFPageView (AnnotationMenu) Category

		[Export ("menuItemsForAnnotations:")]
		PSPDFMenuItem [] MenuItemsForAnnotations ([NullAllowed] PSPDFAnnotation [] annotations);

		[Export ("menuItemsForNewAnnotationAtPoint:")]
		PSPDFMenuItem [] MenuItemsForNewAnnotationAtPoint (CGPoint point);

		[Export ("colorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] ColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("fillColorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] FillColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("opacityMenuItemForAnnotation:withColor:")]
		PSPDFMenuItem OpacityMenuItemForAnnotation (PSPDFAnnotation annotation, [NullAllowed] UIColor color);

		[Export ("showInspectorForAnnotations:options:animated:")]
		[return: NullAllowed]
		PSPDFAnnotationStyleViewController ShowInspectorForAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options, bool animated);

		[Export ("showMenuForAnnotations:targetRect:allowPopovers:animated:")]
		void ShowMenuForAnnotations (PSPDFAnnotation [] annotations, CGRect targetRect, bool allowPopovers, bool animated);

		[Export ("showNoteControllerForAnnotation:showKeyboard:animated:")]
		PSPDFNoteAnnotationViewController ShowNoteControllerForAnnotation (PSPDFAnnotation annotation, bool showKeyboard, bool animated);

		[Export ("showFontPickerForAnnotation:animated:")]
		void ShowFontPickerForAnnotation (PSPDFFreeTextAnnotation annotation, bool animated);

		[Export ("showColorPickerForAnnotation:animated:")]
		void ShowColorPickerForAnnotation (PSPDFAnnotation annotation, bool animated);

		[Export ("showSignatureControllerAtRect:withTitle:shouldSaveSignature:options:animated:")]
		void ShowSignatureController (CGRect viewRect, [NullAllowed] string title, bool shouldSaveSignature, [NullAllowed] NSDictionary options, bool animated);

		[Export ("availableFontSizes")]
		NSNumber [] AvailableFontSizes { get; }

		[Export ("availableLineWidths")]
		NSNumber [] AvailableLineWidths { get; }

		[Export ("passthroughViewsForPopoverController")]
		UIView [] PassthroughViewsForPopoverController { get; }

		// PSPDFPageView (AnnotationMenuSubclassingHooks) Category

		[Export ("showNewSignatureMenuAtRect:options:animated:")]
		void ShowNewSignatureMenuAtRect (CGRect viewRect, [NullAllowed] NSDictionary options, bool animated);

		[Export ("showDigitalSignatureMenuForSignatureField:animated:")]
		bool ShowDigitalSignatureMenuForSignatureField (PSPDFSignatureFormElement signatureField, bool animated);

		[Export ("defaultColorOptionsForAnnotationType:")]
		UIColor [] DefaultColorOptionsForAnnotationType (PSPDFAnnotationType annotationType);

		[Export ("useAnnotationInspectorForAnnotations:")]
		bool UseAnnotationInspectorForAnnotations (PSPDFAnnotation [] annotations);

		[Export ("selectColorForAnnotation:isFillColor:")]
		void SelectColorForAnnotation (PSPDFAnnotation annotation, bool isFillColor);

		[Export ("shouldMoveStyleMenuEntriesIntoSubmenu")]
		bool ShouldMoveStyleMenuEntriesIntoSubmenu { get; }

		[Export ("showLinkPreviewActionSheetForAnnotation:fromRect:animated:")]
		bool ShowLinkPreviewActionSheetForAnnotation (PSPDFLinkAnnotation annotation, CGRect viewRect, bool animated);

		[Export ("showMenuIfSelectedAnimated:")]
		void ShowMenuIfSelected (bool animated);

		[Export ("showMenuIfSelectedAnimated:allowPopovers:")]
		void ShowMenuIfSelected (bool animated, bool allowPopovers);
	}

	[Static]
	interface PSPDFTextMenuItems
	{
		[Field ("PSPDFTextMenuCopy", "__Internal")]
		NSString Copy { get; }

		[Field ("PSPDFTextMenuDefine", "__Internal")]
		NSString Define { get; }

		[Field ("PSPDFTextMenuSearch", "__Internal")]
		NSString Search { get; }

		[Field ("PSPDFTextMenuWikipedia", "__Internal")]
		NSString Wikipedia { get; }

		[Field ("PSPDFTextMenuCreateLink", "__Internal")]
		NSString CreateLink { get; }

		[Field ("PSPDFTextMenuSpeak", "__Internal")]
		NSString Speak { get; }

		[Field ("PSPDFTextMenuPause", "__Internal")]
		NSString Pause { get; }
	}

	[Static]
	interface PSPDFAnnotationMenuStrings {
		
		[Field ("PSPDFAnnotationMenuCancel", "__Internal")]
		NSString Cancel { get; }

		[Field ("PSPDFAnnotationMenuNote", "__Internal")]
		NSString Note { get; }

		[Field ("PSPDFAnnotationMenuGroup", "__Internal")]
		NSString Group { get; }

		[Field ("PSPDFAnnotationMenuUngroup", "__Internal")]
		NSString Ungroup { get; }

		[Field ("PSPDFAnnotationMenuSave", "__Internal")]
		NSString Save { get; }

		[Field ("PSPDFAnnotationMenuRemove", "__Internal")]
		NSString Remove { get; }

		[Field ("PSPDFAnnotationMenuCopy", "__Internal")]
		NSString Copy { get; }

		[Field ("PSPDFAnnotationMenuPaste", "__Internal")]
		NSString Paste { get; }

		[Field ("PSPDFAnnotationMenuMerge", "__Internal")]
		NSString Merge { get; }

		[Field ("PSPDFAnnotationMenuPreviewFile", "__Internal")]
		NSString PreviewFile { get; }

		[Field ("PSPDFAnnotationMenuInspector", "__Internal")]
		NSString Inspector { get; }

		[Field ("PSPDFAnnotationMenuStyle", "__Internal")]
		NSString Style { get; }

		[Field ("PSPDFAnnotationMenuColor", "__Internal")]
		NSString Color { get; }

		[Field ("PSPDFAnnotationMenuFillColor", "__Internal")]
		NSString FillColor { get; }

		[Field ("PSPDFAnnotationMenuOpacity", "__Internal")]
		NSString Opacity { get; }

		[Field ("PSPDFAnnotationMenuCustomColor", "__Internal")]
		NSString CustomColor { get; }

		[Field ("PSPDFAnnotationMenuHighlightType", "__Internal")]
		NSString HighlightType { get; }

		[Field ("PSPDFAnnotationMenuHighlight", "__Internal")]
		NSString Highlight { get; }

		[Field ("PSPDFAnnotationMenuUnderline", "__Internal")]
		NSString Underline { get; }

		[Field ("PSPDFAnnotationMenuStrikeout", "__Internal")]
		NSString Strikeout { get; }

		[Field ("PSPDFAnnotationMenuSquiggle", "__Internal")]
		NSString Squiggle { get; }

		[Field ("PSPDFAnnotationMenuThickness", "__Internal")]
		NSString Thickness { get; }

		[Field ("PSPDFAnnotationMenuPlay", "__Internal")]
		NSString Play { get; }

		[Field ("PSPDFAnnotationMenuPause", "__Internal")]
		NSString Pause { get; }

		[Field ("PSPDFAnnotationMenuPauseRecording", "__Internal")]
		NSString PauseRecording { get; }

		[Field ("PSPDFAnnotationMenuContinueRecording", "__Internal")]
		NSString ContinueRecording { get; }

		[Field ("PSPDFAnnotationMenuFinishRecording", "__Internal")]
		NSString FinishRecording { get; }

		[Field ("PSPDFAnnotationMenuEdit", "__Internal")]
		NSString Edit { get; }

		[Field ("PSPDFAnnotationMenuSize", "__Internal")]
		NSString Size { get; }

		[Field ("PSPDFAnnotationMenuFont", "__Internal")]
		NSString Font { get; }

		[Field ("PSPDFAnnotationMenuAlignment", "__Internal")]
		NSString Alignment { get; }

		[Field ("PSPDFAnnotationMenuAlignmentLeft", "__Internal")]
		NSString AlignmentLeft { get; }

		[Field ("PSPDFAnnotationMenuAlignmentCenter", "__Internal")]
		NSString AlignmentCenter { get; }

		[Field ("PSPDFAnnotationMenuAlignmentRight", "__Internal")]
		NSString AlignmentRight { get; }

		[Field ("PSPDFAnnotationMenuFitToText", "__Internal")]
		NSString FitToText { get; }

		[Field ("PSPDFAnnotationMenuLineStart", "__Internal")]
		NSString LineStart { get; }

		[Field ("PSPDFAnnotationMenuLineEnd", "__Internal")]
		NSString LineEnd { get; }

		[Field ("PSPDFAnnotationMenuLineTypeNone", "__Internal")]
		NSString LineTypeNone { get; }

		[Field ("PSPDFAnnotationMenuLineTypeSquare", "__Internal")]
		NSString LineTypeSquare { get; }

		[Field ("PSPDFAnnotationMenuLineTypeCircle", "__Internal")]
		NSString LineTypeCircle { get; }

		[Field ("PSPDFAnnotationMenuLineTypeDiamond", "__Internal")]
		NSString LineTypeDiamond { get; }

		[Field ("PSPDFAnnotationMenuLineTypeOpenArrow", "__Internal")]
		NSString LineTypeOpenArrow { get; }

		[Field ("PSPDFAnnotationMenuLineTypeClosedArrow", "__Internal")]
		NSString LineTypeClosedArrow { get; }

		[Field ("PSPDFAnnotationMenuLineTypeButt", "__Internal")]
		NSString LineTypeButt { get; }

		[Field ("PSPDFAnnotationMenuLineTypeReverseOpenArrow", "__Internal")]
		NSString LineTypeReverseOpenArrow { get; }

		[Field ("PSPDFAnnotationMenuLineTypeReverseClosedArrow", "__Internal")]
		NSString LineTypeReverseClosedArrow { get; }

		[Field ("PSPDFAnnotationMenuLineTypeSlash", "__Internal")]
		NSString LineTypeSlash { get; }

		[Field ("PSPDFAnnotationMenuMySignature", "__Internal")]
		NSString MySignature { get; }

		[Field ("PSPDFAnnotationMenuCustomerSignature", "__Internal")]
		NSString CustomerSignature { get; }
	}

	interface IPSPDFTextSelectionViewDataSource	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSelectionViewDataSource : PSPDFOverridable {

		[Export ("document")]
		PSPDFDocument GetDocument ();

		[Export ("page")]
		nuint GetPage ();

		[Export ("zoomScale")]
		nfloat GetZoomScale ();

		[Export ("convertViewPointToPDFPoint:")]
		[Abstract]
		CGPoint ConvertViewPointToPdfPoint (CGPoint viewPoint);

		[Export ("convertPDFPointToViewPoint:")]
		[Abstract]
		CGPoint ConvertPdfPointToViewPoint (CGPoint pdfPoint);

		[Export ("convertViewRectToPDFRect:")]
		[Abstract]
		CGRect ConvertViewRectToPdfRect (CGRect viewRect);

		[Export ("convertPDFRectToViewRect:")]
		[Abstract]
		CGRect ConvertPdfRectToViewRect (CGRect pdfRect);

		[Export ("convertGlyphRectToViewRect:")]
		[Abstract]
		CGRect ConvertGlyphRectToViewRect (CGRect glyphRect);

		[Export ("convertViewRectToGlyphRect:")]
		[Abstract]
		CGRect ConvertViewRectToGlyphRect (CGRect viewRect);

		[Export ("isTextSelectionEnabled")]
		bool GetIsTextSelectionEnabled ();

		[Export ("isImageSelectionEnabled")]
		bool GetIsImageSelectionEnabled ();

		[Export ("textSelectionMode")]
		PSPDFTextSelectionMode GetTextSelectionMode ();

		[Export ("textSelectionShouldSnapToWord")]
		bool GetTextSelectionShouldSnapToWord ();
	}

	[BaseType (typeof (PSPDFDocumentSharingCoordinator))]
	interface PSPDFPrintCoordinator : IUIPrintInteractionControllerDelegate, IUIPrinterPickerControllerDelegate {

		[Export ("printConfiguration", ArgumentSemantic.Strong)]
		PSPDFPrintConfiguration PrintConfiguration { get; set; }

		// PSPDFPrintCoordinator (SubclassingHooks)

		[Export ("printInfo", ArgumentSemantic.Copy)]
		UIPrintInfo PrintInfo { get; }

		[Export ("printInteractionController", ArgumentSemantic.Weak), NullAllowed]
		UIPrintInteractionController PrintInteractionController { get; }
	}

	interface IPSPDFTextSelectionViewDelegate {	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSelectionViewDelegate {

		[Export ("textSelectionView:updateMenuAnimated:")][Abstract]
		bool UpdateMenuAnimated (PSPDFTextSelectionView textSelectionView, bool animated);

		[Export ("textSelectionView:shouldSelectText:withGlyphs:atRect:")]
		bool ShouldSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph [] glyphs, CGRect rect);

		[Export ("textSelectionView:didSelectText:withGlyphs:atRect:")]
		void DidSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph [] glyphs, CGRect rect);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFTextSelectionView : IAVSpeechSynthesizerDelegate	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFTextSelectionViewDelegate Delegate { get; set; }

		[Export ("selectedGlyphs", ArgumentSemantic.Copy), NullAllowed]
		PSPDFGlyph [] SelectedGlyphs { get; set; }

		[Export ("selectedText"), NullAllowed]
		string SelectedText { get; }

		[Export ("selectedImage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFImageInfo SelectedImage { get; set; }

		[Export ("selectionAlpha", ArgumentSemantic.Assign)]
		nfloat SelectionAlpha { get; set; }

		[Export ("trimmedSelectedText"), NullAllowed]
		string TrimmedSelectedText { get; }

		[Export ("selectionHitTestExtension", ArgumentSemantic.Assign)]
		nfloat SelectionHitTestExtension { get; set; }

		[Export ("firstLineRect", ArgumentSemantic.Assign)]
		CGRect FirstLineRect { get; }

		[Export ("lastLineRect", ArgumentSemantic.Assign)]
		CGRect LastLineRect { get; }

		[Export ("selectionRect", ArgumentSemantic.Assign)]
		CGRect SelectionRect { get; }

		[Export ("updateMenuAnimated:")]
		bool UpdateMenu (bool animated);

		[Export ("updateSelectionAnimated:")]
		void UpdateSelection (bool animated);

		[Export ("discardSelectionAnimated:")]
		void DiscardSelection (bool animated);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("hasSelection", ArgumentSemantic.Assign)]
		bool HasSelection { get; }

		// PSPDFTextSelectionView (Advanced) Category

		[Export ("sortedGlyphs:")]
		PSPDFGlyph [] SortedGlyphs (PSPDFGlyph [] glyphs);

		// PSPDFTextSelectionView (SubclassingHooks) Category

		[Export ("showTextFlowData:animated:")]
		void ShowTextFlowData (bool show, bool animated);
	}

	interface IPSPDFPresentationContext { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPresentationContext : PSPDFOverridable, PSPDFVisiblePagesDataSource, PSPDFErrorHandler {

		[Abstract]
		[Export ("configuration", ArgumentSemantic.Copy)]
		PSPDFConfiguration Configuration { get; }

		[Abstract]
		[Export ("pspdfkit")]
		PSPDFKitGlobal Pspdfkit { get; }

		[Abstract]
		[Export ("displayingViewController")]
		UIViewController DisplayingViewController { get; }

		[Abstract]
		[Export ("document"), NullAllowed]
		PSPDFDocument Document { get; }

		[Abstract]
		[Export ("viewMode")]
		PSPDFViewMode ViewMode { get; }

		[Abstract]
		[Export ("contentRect")]
		CGRect ContentRect { get; }

		[Abstract]
		[Export ("scrollViewInsets")]
		UIEdgeInsets ScrollViewInsets { get; }

		[Abstract]
		[Export ("shouldAdjustFrameWhenHUDIsPersistent")]
		bool ShouldAdjustFrameWhenHudIsPersistent { get; }

		[Abstract]
		[Export ("doublePageMode")]
		bool DoublePageMode { [Bind ("isDoublePageMode")] get; }

		[Abstract]
		[Export ("scrollingEnabled")]
		bool ScrollingEnabled { [Bind ("isScrollingEnabled")] get; }

		[Abstract]
		[Export ("viewLockEnabled")]
		bool ViewLockEnabled { [Bind ("isViewLockEnabled")] get; }

		[Abstract]
		[Export ("rotationActive")]
		bool RotationActive { [Bind ("isRotationActive")] get; }

		[Abstract]
		[Export ("HUDVisible")]
		bool HudVisible { [Bind ("isHUDVisible")] get; }

		[Abstract]
		[Export ("viewWillAppearing")]
		bool ViewWillAppearing { [Bind ("isViewWillAppearing")] get; }

		[Abstract]
		[Export ("reloading")]
		bool Reloading { [Bind ("isReloading")] get; }

		[Abstract]
		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }

		[Abstract]
		[Export ("visiblePageViewsForcingLayout:")]
		PSPDFPageView [] GetVisiblePageViewsForcingLayout (bool forcingLayout);

		[Abstract]
		[Export ("pageViewForPageAtIndex:")]
		PSPDFPageView GetPageViewForPage (nuint pageIndex);

		[Abstract]
		[Export ("isRightPageInDoublePageMode:")]
		bool IsRightPageInDoublePageMode (nuint pageIndex);

		[Abstract]
		[Export ("isDoublePageModeForViewSize:")]
		bool IsDoublePageModeForViewSize (CGSize viewSize);

		[Abstract]
		[Export ("isDoublePageModeForPageAtIndex:")]
		bool IsDoublePageModeForPage (nuint pageIndex);

		[Abstract]
		[Export ("portraitPageSpreadForLandscapePageSpread:")]
		nuint GetPortraitPageSpreadForLandscapePageSpread (nuint pageSpread);

		[Abstract]
		[Export ("landscapePageSpreadForPortraitPageSpread:")]
		nuint GetLandscapePageSpreadForPortraitPageSpread (nuint pageSpread);

		[Abstract]
		[Export ("pagingScrollView"), NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Abstract]
		[Export ("annotationStateManager")]
		PSPDFAnnotationStateManager AnnotationStateManager { get; }

		[Abstract]
		[Export ("annotationToolbarController")]
		PSPDFAnnotationToolbarController AnnotationToolbarController { get; }

		[Abstract]
		[Export ("actionDelegate"), NullAllowed]
		IPSPDFControlDelegate ActionDelegate { get; }

		[Abstract]
		[Export ("pdfController")]
		PSPDFViewController PdfController { get; }
	}

	interface IPSPDFExternalURLHandler { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFExternalURLHandler {

		[Export ("handleExternalURL:completionBlock:")][Abstract]
		bool HandleExternalUrl (NSUrl url, [NullAllowed] Action<bool> completionHandler);
	}

	delegate void PSPDFDocumentEditorSaveHandler ([NullAllowed] PSPDFDocument document, [NullAllowed] NSError error);

	interface IPSPDFDocumentEditorDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditorDelegate {

		[Export ("documentEditor:didPerformChanges:")]
		void DidPerformChanges (PSPDFDocumentEditor editor, PSPDFEditingChange [] changes);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditor {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("saveOptions", ArgumentSemantic.Strong), NullAllowed]
		PSPDFProcessorSaveOptions SaveOptions { get; set; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFDocumentEditorDelegate editorDelegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFDocumentEditorDelegate editorDelegate);

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageSizeForPageAtIndex:")]
		CGSize GetPageSize (nuint pageIndex);

		[Export ("addPageAt:withConfiguration:")]
		PSPDFEditingChange [] AddPageAt (nuint index, PSPDFNewPageConfiguration configuration);

		[Export ("movePages:to:")]
		PSPDFEditingChange [] MovePages (NSIndexSet pageIndexes, nuint destination);

		[Export ("removePages:")]
		PSPDFEditingChange [] RemovePages (NSIndexSet pageIndexes);

		[Export ("duplicatePages:")]
		PSPDFEditingChange [] DuplicatePages (NSIndexSet pageIndexes);

		[Export ("rotatePages:rotation:")]
		PSPDFEditingChange [] RotatePages (NSIndexSet pageIndexes, nint rotation);

		[Export ("undo")]
		[return: NullAllowed]
		PSPDFEditingChange [] Undo ();

		[Export ("redo")]
		[return: NullAllowed]
		PSPDFEditingChange [] Redo ();

		[Export ("canRedo", ArgumentSemantic.Assign)]
		bool CanRedo { get; }

		[Export ("canUndo", ArgumentSemantic.Assign)]
		bool CanUndo { get; }

		[Export ("canSave", ArgumentSemantic.Assign)]
		bool CanSave { get; }

		[Export ("saveWithCompletionBlock:")]
		void SaveWithCompletionBlock ([NullAllowed] PSPDFDocumentEditorSaveHandler block);

		[Export ("saveToPath:withCompletionBlock:")]
		void Save (string path, [NullAllowed] PSPDFDocumentEditorSaveHandler block);

		[Export ("exportPages:toPath:withCompletionBlock:")]
		void ExportPages (NSIndexSet pageIndexes, string path, [NullAllowed] PSPDFDocumentEditorSaveHandler block);

		[return: NullAllowed]
		[Export ("imageForPageAtIndex:size:scale:")]
		UIImage GetImage (nuint pageIndex, CGSize size, nfloat scale);
	}

	[BaseType (typeof (PSPDFPageCell))]
	interface PSPDFDocumentEditorCell {
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditorConfiguration {

		[Export ("pagePatterns"), NullAllowed]
		PSPDFPagePattern [] PagePatterns { get; set; }

		[Export ("currentDocumentPageSize"), NullAllowed]
		PSPDFPageSize CurrentDocumentPageSize { get; set; }

		[Export ("pageSizes", ArgumentSemantic.Assign), NullAllowed]
		PSPDFPageSize [] PageSizes { get; set; }

		[Export ("currentDocumentDirectory"), NullAllowed]
		PSPDFDirectory CurrentDocumentDirectory { get; set; }

		[Export ("saveDirectories"), NullAllowed]
		PSPDFDirectory [] SaveDirectories { get; set; }

		[Export ("compressions"), NullAllowed]
		PSPDFCompression [] Compressions { get; set; }

		[Export ("selectedPagePattern"), NullAllowed]
		PSPDFPagePattern SelectedPagePattern { get; set; }

		[Export ("selectedPageSize"), NullAllowed]
		PSPDFPageSize SelectedPageSize { get; set; }

		[Export ("selectedOrientation", ArgumentSemantic.Assign)]
		PSPDFDocumentOrientation SelectedOrientation { get; set; }

		[Export ("selectedColor"), NullAllowed]
		UIColor SelectedColor { get; set; }

		[Export ("selectedImage"), NullAllowed]
		UIImage SelectedImage { get; set; }

		[Export ("selectedImagePageSize"), NullAllowed]
		PSPDFPageSize SelectedImagePageSize { get; set; }

		[Export ("selectedCompression"), NullAllowed]
		PSPDFCompression SelectedCompression { get; set; }

		[Export ("selectedSaveDirectory"), NullAllowed]
		PSPDFDirectory SelectedSaveDirectory { get; set; }

		[Export ("userFacingCompressionEnabled", ArgumentSemantic.Assign)]
		bool UserFacingCompressionEnabled { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFCompression {

		[Static]
		[Export ("compression:name:")]
		PSPDFCompression Create (nfloat compression, string name);

		[Export ("initWithCompression:name:")]
		[DesignatedInitializer]
		IntPtr Constructor (nfloat compression, string name);

		[Export ("compression", ArgumentSemantic.Assign)]
		nfloat Compression { get; }

		[Export ("name", ArgumentSemantic.Assign)]
		string Name { get; }

		[Export ("localizedName", ArgumentSemantic.Assign)]
		string LocalizedName { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFPagePattern {

		[Export ("initWithIdentifier:")]
		[DesignatedInitializer]
		IntPtr Constructor (string identifier);

		[Export ("identifier")]
		string Identifier { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }

		[Export ("thumbnail"), NullAllowed]
		UIImage Thumbnail { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDataContainerSink : PSPDFDataSink {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] NSData data);

		[Export ("data")]
		NSData Data { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFPageSize {

		[Static]
		[Export ("size:name:")]
		PSPDFPageSize FromSize (CGSize size, string name);

		[Export ("initWithSize:name:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGSize size, string name);

		[Export ("size")]
		CGSize Size { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }

		[Export ("localizedSize")]
		string LocalizedSize { get; }

		[Export ("sizeForOrientation:")]
		CGSize GetSizeForOrientation (PSPDFDocumentOrientation orientation);
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFDirectory {

		[Static]
		[Export ("directoryWithPath:")]
		PSPDFDirectory FromPath (string path);

		[Static]
		[Export ("directoryWithPath:name:")]
		PSPDFDirectory FromPath (string path, [NullAllowed] string name);

		[Export ("initWithPath:name:")]
		[DesignatedInitializer]
		IntPtr Constructor (string path, [NullAllowed] string name);

		[Export ("path")]
		string Path { get; }

		[Export ("name"), NullAllowed]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }
	}

	[BaseType (typeof (PSPDFFlexibleToolbar))]
	interface PSPDFDocumentEditorToolbar {

		[Export ("addPageButton")]
		PSPDFToolbarButton AddPageButton { get; }

		[Export ("deletePagesButton")]
		PSPDFToolbarButton DeletePagesButton { get; }

		[Export ("duplicatePagesButton")]
		PSPDFToolbarButton DuplicatePagesButton { get; }

		[Export ("rotatePagesButton")]
		PSPDFToolbarButton RotatePagesButton { get; }

		[Export ("exportPagesButton")]
		PSPDFToolbarButton ExportPagesButton { get; }

		[Export ("selectAllPagesButton")]
		PSPDFToolbarButton SelectAllPagesButton { get; }

		[Export ("undoButton")]
		PSPDFToolbarButton UndoButton { get; }

		[Export ("redoButton")]
		PSPDFToolbarButton RedoButton { get; }

		[Export ("doneButton")]
		PSPDFToolbarButton DoneButton { get; }

		[Export ("allPagesSelected")]
		bool AllPagesSelected { get; set; }

		// SubclassingHooks (PSPDFDocumentEditorToolbar) category

		[Export ("buttonsForWidth:")]
		PSPDFToolbarButton [] GetButtonsForWidth (nfloat width);
	}

	interface IPSPDFDocumentEditorToolbarControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))] 
	interface PSPDFDocumentEditorToolbarControllerDelegate : PSPDFFlexibleToolbarContainerDelegate {

		[Abstract]
		[Export ("documentEditorToolbarController:didSelectPages:")]
		void DidSelectPages (PSPDFDocumentEditorToolbarController controller, NSIndexSet pages);

		[Export ("documentEditorToolbarController:indexForNewPageWithConfiguration:")]
		nuint GetIndexForNewPageWithConfiguration (PSPDFDocumentEditorToolbarController controller, PSPDFNewPageConfiguration configuration);
	}

	[BaseType (typeof (PSPDFFlexibleToolbarController))]
	interface PSPDFDocumentEditorToolbarController : PSPDFDocumentEditorDelegate, PSPDFNewPageViewControllerDelegate, PSPDFSaveViewControllerDelegate {

		[Field ("PSPDFDocumentEditorToolbarControllerVisibilityDidChangeNotification", "__Internal")]
		[Notification]
		NSString VisibilityDidChangeNotification { get; }

		[Field ("PSPDFDocumentEditorToolbarControllerVisibilityAnimatedKey", "__Internal")]
		NSString VisibilityAnimatedKey { get; }

		[Export ("initWithDocumentEditorToolbar:")]
		IntPtr Constructor (PSPDFDocumentEditorToolbar documentEditorToolbar);

		[Export ("documentEditorToolbar")]
		PSPDFDocumentEditorToolbar DocumentEditorToolbar { get; }

		[Export ("documentEditor"), NullAllowed]
		PSPDFDocumentEditor DocumentEditor { get; set; }

		[Export ("selectedPages", ArgumentSemantic.Copy)]
		NSIndexSet SelectedPages { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentEditorToolbarControllerDelegate Delegate { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }

		[Export ("toggleNewPageController:presentationOptions:")]
		[return: NullAllowed]
		PSPDFNewPageViewController ToggleNewPageController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("toggleSaveActionSheet:presentationOptions:completionHandler:")]
		[return: NullAllowed]
		UIAlertController ToggleSaveActionSheet ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<bool> completionHandler);

		[Export ("toggleSaveController:presentationOptions:completionHandler:")]
		[return: NullAllowed]
		PSPDFSaveViewController ToggleSaveController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<bool> completionHandler);
	}

	[BaseType (typeof (UICollectionViewController))]
	interface PSPDFDocumentEditorViewController : PSPDFDocumentEditorDelegate {

		[Export ("cellClass")]
		Class CellClass { get; set; }

		[Export ("documentEditor"), NullAllowed]
		PSPDFDocumentEditor DocumentEditor { get; }

		[Export ("toolbarController")]
		PSPDFDocumentEditorToolbarController ToolbarController { get; }

		// From PSPDFViewModePresenter Interface

		[Export ("initWithCollectionViewLayout:")]
		IntPtr Constructor ([NullAllowed] UICollectionViewLayout layout);

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document"), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		PSPDFPresentationContext PresentationContext { get; set; }

		[Export ("fixedItemSizeEnabled")]
		bool FixedItemSizeEnabled { get; set; }

		[Export ("updateInsetsForTopOverlapHeight:")]
		void UpdateInsetsForTopOverlapHeight (nfloat overlapHeight);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAvoidingScrollView))]
	interface PSPDFScrollView : IUIScrollViewDelegate, IUIGestureRecognizerDelegate	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("spreadIndex", ArgumentSemantic.Assign)]
		nuint SpreadIndex { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; }

		[Export ("leftPage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageView LeftPage { get; }

		[Export ("rightPage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageView RightPage { get; }

		[Export ("zoomingEnabled", ArgumentSemantic.Assign)]
		bool ZoomingEnabled { [Bind ("isZoomingEnabled")] get; set; }

		// PSPDFScrollView (PSPDFAdvanced) Category

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; }

		// PSPDFScrollView (PSPDFSubclassing) Category

		[Export ("singleTapGesture", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer SingleTapGesture { get; }

		[Export ("doubleTapGesture", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer DoubleTapGesture { get; }

		[Export ("longPressGesture", ArgumentSemantic.Strong), NullAllowed]
		UILongPressGestureRecognizer LongPressGesture { get; }

		[Export ("singleTapped:")]
		void SingleTapped (UITapGestureRecognizer recognizer);

		[Export ("doubleTapped:")]
		void DoubleTapped (UITapGestureRecognizer recognizer);

		[Export ("longPress:")]
		void LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("updateScrollViewIndicator")]
		void UpdateScrollViewIndicator ();

		[Export ("ensureContentIsCentered")]
		void EnsureContentIsCentered ();

		[Export ("createDoubleTapGesture")]
		UITapGestureRecognizer CreateDoubleTapGesture ();

		[Export ("compoundView", ArgumentSemantic.Strong), NullAllowed]
		UIView CompoundView { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProvider	{

		[Export ("fileURL"), NullAllowed]
		NSUrl FileUrl { get; }

		[Export ("dataProvider"), NullAllowed]
		IPSPDFDataProvider DataProvider { get; }

		[Export ("dataRepresentationWithError:"), Internal]
		[return: NullAllowed]
		NSData DataRepresentationWithError (IntPtr error);

		[Export ("fileSize", ArgumentSemantic.Assign)]
		ulong FileSize { get; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentProviderDelegate Delegate { get; set; }

		[Export ("pageInfoForPageAtIndex:")]
		PSPDFPageInfo GetPageInfoForPage (nuint pageIndex);

		[Export ("pageCount", ArgumentSemantic.Assign)]
		nuint PageCount { get; set; }

		[Export ("pageOffsetForDocument")]
		nuint PageOffsetForDocument { get; }

		[Export ("password"), NullAllowed]
		string Password { get; }

		[Export ("contentSignature", ArgumentSemantic.Copy), NullAllowed]
		NSData ContentSignature { get; }

		[Export ("permissions", ArgumentSemantic.Assign)]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; }

		[Export ("allowAnnotationChanges", ArgumentSemantic.Assign)]
		bool AllowAnnotationChanges { get; }

		[Export ("fileId"), NullAllowed]
		NSData FileId { get; }

		[Export ("title")]
		string Title { get; }

		[Export ("textParserForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFTextParser GetTextParser (nuint pageIndex);

		[Export ("outlineParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineParser OutlineParser { get; }

		[Export ("formParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFormParser FormParser { get; }

		[Export ("annotationManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationManager AnnotationManager { get; }

		[Export ("labelParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLabelParser LabelParser { get; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Metadata { get; }

		[Export ("XMPMetadata"), NullAllowed]
		string XmpMetadata { get; }

		[Export ("setRotation:forPageAtIndex:")]
		void SetRotation (nuint rotation, nuint pageIndex);

		// PSPDFDocumentProvider (SubclassingHooks) Category

		[Advice ("You shouldn't call this method directly, use the high-level save method in PSPDFDocument instead")]
		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotationsWithOptions ([NullAllowed] NSDictionary<NSString, NSObject> options, IntPtr error);

		[Export ("resolveTokenizedPath:alwaysLocal:")]
		string ResolveTokenizedPath (string path, bool alwaysLocal);
	}

	interface IPSPDFDocumentProviderDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProviderDelegate	{

		[Export ("documentProvider:shouldAppendData:")]
		bool ShouldAppendData (PSPDFDocumentProvider documentProvider, NSData data);

		[Export ("documentProvider:didAppendData:")]
		void DidAppendData (PSPDFDocumentProvider documentProvider, NSData data);
	}

	interface IPSPDFSettings { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSettings {

		[Export ("objectForKeyedSubscript:")]
		[Abstract]
		NSObject ObjectForKeyedSubscript (NSObject key);

		[Export ("boolForKey:")]
		[Abstract]
		bool BoolForKey (string key);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFCache {

		[Export ("initWithSettings:")]
		IntPtr Constructor (PSPDFKitGlobal pspdfkit);

		[Export ("memoryCache")]
		PSPDFMemoryCache MemoryCache { get; }

		[Export ("diskCache")]
		PSPDFDiskCache DiskCache { get; }

		[Export ("cacheStatusForRequest:imageSizeMatching:")]
		PSPDFCacheStatus GetCacheStatus (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching);

		[Export ("imageForRequest:imageSizeMatching:")]
		[return: NullAllowed]
		UIImage GetImageForRequest (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching);

		[Export ("saveImage:forRequest:")]
		void SaveImage (UIImage image, PSPDFRenderRequest request);

		[Export ("cacheDocument:pageSizes:withDiskCacheStrategy:aroundPageAtIndex:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes, PSPDFDiskCacheStrategy strategy, nuint pageIndex);

		[Export ("cacheDocument:pageSizes:withDiskCacheStrategy:aroundPageAtIndex:imageRenderingCompletionBlock:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes, PSPDFDiskCacheStrategy strategy, nuint pageIndex, [NullAllowed] Action<UIImage, PSPDFDocument, nuint, CGSize> pageCompletionHandler);

		[Export ("stopCachingDocument:")]
		void StopCachingDocument ([NullAllowed] PSPDFDocument document);

		[Export ("invalidateImageFromDocument:pageIndex:")]
		void InvalidateImageFromDocument ([NullAllowed] PSPDFDocument document, nuint pageIndex);

		[Export ("removeCacheForDocument:")]
		bool RemoveCacheForDocument ([NullAllowed] PSPDFDocument document);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("pauseCachingForService:")]
		void PauseCachingForService (NSObject service);

		[Export ("resumeCachingForService:")]
		void ResumeCachingForService (NSObject service);
	}

	delegate NSData PSPDFDiskCacheEncryptionHelper (PSPDFRenderRequest request, NSData data);
	delegate NSData PSPDFDiskCacheDecryptionHelper (PSPDFRenderRequest request, NSData encryptedData);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFDiskCache {

		[Export ("initWithCacheDirectory:fileFormat:settings:")]
		IntPtr Constructor (string cacheDirectory, PSPDFDiskCacheFileFormat fileFormat, PSPDFKitGlobal settings);

		[Export ("allowedDiskSpace", ArgumentSemantic.Assign)]
		ulong AllowedDiskSpace { get; set; }

		[Export ("usedDiskSpace", ArgumentSemantic.Assign)]
		ulong UsedDiskSpace { get; }

		[Export ("cacheDirectory")]
		string CacheDirectory { get; set; }

		[Export ("fileFormat", ArgumentSemantic.Assign)]
		PSPDFDiskCacheFileFormat FileFormat { get; set; }

		[Export ("encryptionHelper", ArgumentSemantic.Copy), NullAllowed]
		PSPDFDiskCacheEncryptionHelper EncryptionHelper { get; set; }

		[Export ("decryptionHelper", ArgumentSemantic.Copy), NullAllowed]
		PSPDFDiskCacheDecryptionHelper DecryptionHelper { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFMemoryCache {

		[Export ("initWithSettings:")]
		IntPtr Constructor (PSPDFKitGlobal pspdfkit);

		[Export ("count", ArgumentSemantic.Assign)]
		nuint Count { get; }

		[Export ("numberOfPixels", ArgumentSemantic.Assign)]
		ulong NumberOfPixels { get; }

		[Export ("maxNumberOfPixels", ArgumentSemantic.Assign)]
		ulong MaxNumberOfPixels { get; set; }

		[Export ("maxNumberOfPixelsUnderStress", ArgumentSemantic.Assign)]
		ulong MaxNumberOfPixelsUnderStress { get; set; }
	}

	interface IPSPDFPageRenderer { }

	[Protocol]
	interface PSPDFPageRenderer {

		[Export ("drawPageIndex:inContext:documentProvider:withOptions:error:")]
		[Abstract]
		bool DrawPage (nuint pageIndex, CGContext context, PSPDFDocumentProvider documentProvider, [NullAllowed] NSDictionary options, out NSError error);

		[Export ("renderAppearanceStream:inContext:withOptions:error:")]
		[Abstract]
		bool RenderAppearanceStream (PSPDFAnnotation annotation, CGContext context, [NullAllowed] NSDictionary options, out NSError error);
	}

	interface IPSPDFRenderManager { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderManager {

		[Field ("PSPDFPageRendererPageInfoKey", "__Internal")]
		NSString PageInfoKey { get; }

		[Abstract]
		[Export ("setupGraphicsContext:rectangle:pageInfo:")]
		void SetupGraphicsContext (CGContext context, CGRect displayRectangle, PSPDFPageInfo pageInfo);

		[Abstract]
		[Export ("renderQueue")]
		PSPDFRenderQueue RenderQueue { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFColorPatch {

		[Static]
		[Export ("colorPatchWithColor:")]
		PSPDFColorPatch FromColor (UIColor color);

		[Static]
		[Export ("colorPatchWithColors:")]
		PSPDFColorPatch FromColors (UIColor [] colors);

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; }

	}

	[BaseType (typeof (NSObject))]
	interface PSPDFColorPalette {

		[Static]
		[Export ("colorPaletteWithTitle:colorPatches:")]
		PSPDFColorPalette FromTitle (string title, PSPDFColorPatch [] patches);

		[Static]
		[Export ("hsvColorPaletteWithTitle:")]
		PSPDFColorPalette GetHsvColorPalette (string title);

		[Export ("title")]
		string Title { get; }

		[Export ("colorPatches", ArgumentSemantic.Copy)]
		PSPDFColorPatch [] ColorPatches { get; }

		// PSPDFColorPalette (PSPDFColorPalettes) Category

		[Static]
		[Export ("monochromeColorPalette")]
		PSPDFColorPalette MonochromeColorPalette { get; }

		[Static]
		[Export ("monochromeTransparentPalette")]
		PSPDFColorPalette MonochromeTransparentPalette { get; }

		[Static]
		[Export ("modernColorPalette")]
		PSPDFColorPalette ModernColorPalette { get; }

		[Static]
		[Export ("vintageColorPalette")]
		PSPDFColorPalette VintageColorPalette { get; }

		[Static]
		[Export ("rainbowColorPalette")]
		PSPDFColorPalette RainbowColorPalette { get; }

		[Static]
		[Export ("paperColorPalette")]
		PSPDFColorPalette PaperColorPalette { get; }

		[Static]
		[Export ("hsvColorPalette")]
		PSPDFColorPalette HsvColorPalette { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFColorPickerFactory {

		[Static]
		[Export ("colorPalettesInColorSet:")]
		PSPDFColorPalette [] FromColorSet (PSPDFColorSet colorSet);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFPageInfo {

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("rect", ArgumentSemantic.Assign)]
		CGRect Rect { get; }

		[Export ("rotation", ArgumentSemantic.Assign)]
		nuint Rotation { get; }

		[Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, PSPDFAction> AdditionalActions { get; }

		[Export ("rotatedRect", ArgumentSemantic.Assign)]
		CGRect RotatedRect { get; }

		[Export ("rotationTransform", ArgumentSemantic.Assign)]
		CGAffineTransform RotationTransform { get; }

		[Export ("allowAnnotationCreation", ArgumentSemantic.Assign)]
		bool AllowAnnotationCreation { get; }

		[Export ("mediaBox", ArgumentSemantic.Assign)]
		CGRect MediaBox { get; }

		[Export ("cropBox", ArgumentSemantic.Assign)]
		CGRect CropBox { get; }
	}

	[BaseType (typeof (PSPDFScrollView))]
	interface PSPDFContentScrollView {

		[Export ("initWithTransitionViewController:")]
		IntPtr Constructor (PSPDFTransitionProtocol viewController);

		[Export ("contentController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTransitionProtocol ContentController { get; }
	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTransitionProtocol {

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; }

		[Export ("scrollView", ArgumentSemantic.Weak)]
		PSPDFContentScrollView ScrollView { get; set; }

		[Export ("setPage:animated:")][Abstract]
		void SetPage (nuint page, bool animated);

		[Export ("visiblePages")][Abstract]
		NSOrderedSet VisiblePages ();

		[Export ("pageViewForPage:")][Abstract]
		PSPDFPageView PageViewForPage (nuint page);

		[Export ("visiblePageViewsForcingLayout:")]
		NSObject [] VisiblePageViewsForcingLayout (bool forcingLayout);

		[Export ("compensatedContentOffset")]
		CGPoint CompensatedContentOffset ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFViewState : INSSecureCoding {

		[DesignatedInitializer]
		[Export ("initWithPageIndex:viewPort:selectionState:")]
		IntPtr Constructor (nuint pageIndex, CGRect viewPort, [NullAllowed] PSPDFSelectionState selectionState);

		[Export ("initWithPageIndex:selectionState:")]
		IntPtr Constructor (nuint pageIndex, [NullAllowed] PSPDFSelectionState selectionState);

		[Export ("initWithPageIndex:viewPort:")]
		IntPtr Constructor (nuint pageIndex, CGRect viewPort);

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (nuint pageIndex);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; }

		[Export ("viewPort", ArgumentSemantic.Assign)]
		CGRect ViewPort { get; set; }

		[Export ("hasViewPort", ArgumentSemantic.Assign)]
		bool HasViewPort { get; set; }

		[NullAllowed]
		[Export ("selectionState", ArgumentSemantic.Strong)]
		PSPDFSelectionState SelectionState { get; }

		[Export ("isEqualToViewState:withAccuracy:")]
		bool IsEqualTo (PSPDFViewState other, nfloat leeway);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFBookmark {

		[Export ("initWithAction:")]
		IntPtr Constructor ([NullAllowed] PSPDFAction action);

		[Export ("action", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction Action { get; }

		[Export ("name"), NullAllowed]
		string Name { get; set; }

		[Export ("displayName"), NullAllowed]
		string DisplayName { get; }

		// PSPDFBookmark (GoToAction) Category

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (uint pageIndex);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFBookmarkManager {

		[Notification]
		[Field ("PSPDFBookmarksChangedNotification", "__Internal")]
		NSString BookmarksChangedNotification { get; }

		[DesignatedInitializer]
		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		[Export ("bookmarksWithSortOrder:")]
		PSPDFBookmark [] GetBookmarks (PSPDFBookmarkManagerSortOrder sortOrder);

		[Export ("addBookmark:")]
		void Add (PSPDFBookmark bookmark);

		[Export ("removeBookmark:")]
		void Remove (PSPDFBookmark bookmark);

		[Export ("moveBookmarkAtIndex:toIndex:")]
		void Move (uint sourceIndex, nuint destinationIndex);

		[Export ("performBlock:")]
		void PerformAction (Action handler);

		[Export ("performBlockAndWait:")]
		void PerformActionAndWait (Action handler);

		[Export ("provider")]
		IPSPDFBookmarkProvider [] Provider { get; set; }

		// PSPDFBookmarkManager (GoToAction) Category

		[Export ("addBookmarkForPageAtIndex:")]
		void AddBookmarkForPage (uint pageIndex);

		[Export ("removeBookmarkForPageAtIndex:")]
		void RemoveBookmarkForPage (uint pageIndex);

		[Export ("bookmarkForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFBookmark GetBookmarkForPage (uint pageIndex);
	}

	interface IPSPDFBookmarkProvider { }

	[Protocol]
	interface PSPDFBookmarkProvider {

		[Abstract]
		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		[Abstract]
		[Export ("addBookmark:")]
		void Add (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("removeBookmark:")]
		void Remove (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("save")]
		void Save ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderQueue {

		[Export ("scheduleTask:")]
		void Schedule (PSPDFRenderTask task);

		[Export ("scheduleTasks:")]
		void Schedule (PSPDFRenderTask [] tasks);

		[Export ("concurrentRunningRenderRequests")]
		nuint ConcurrentRunningRenderRequests { get; set; }

		[Export ("cancelAllJobs")]
		void CancelAllJobs ();

		[Export ("minimumProcessPriority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority MinimumProcessPriority { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationStyle {

		[Export ("initWithName:")]
		IntPtr Constructor (string styleName);

		[Export ("styleName")]
		string StyleName { get; set; }

		[Export ("styleDictionary", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> StyleDictionary { get; set; }

		[Export ("setStyle:forKey:")]
		void SetStyle ([NullAllowed] NSObject style, string key);

		[Export ("applyStyleToAnnotation:")]
		void ApplyStyleToAnnotation (PSPDFAnnotation annotation);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFUndoController {

		[Field ("PSPDFUndoControllerAddedUndoActionNotification", "__Internal")]
		[Notification]
		NSString AddedUndoActionNotification { get; }

		[Field ("PSPDFUndoControllerRemovedUndoActionNotification", "__Internal")]
		[Notification]
		NSString RemovedUndoActionNotification { get; }

		[Export ("working")]
		bool Working { [Bind ("isWorking")] get; }

		[Export ("undoing")]
		bool Undoing { [Bind ("isUndoing")] get; }

		[Export ("redoing")]
		bool Redoing { [Bind ("isRedoing")] get; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("undo")]
		void Undo ();

		[Export ("redo")]
		void Redo ();

		[Export ("endUndoGroupingWithProperty:ofObject:")]
		void EndUndoGrouping (string changedProperty, [NullAllowed] NSObject obj);

		[Export ("removeAllActions")]
		void RemoveAllActions ();

		[Export ("removeAllActionsWithTarget:")]
		void RemoveAllActions (NSObject target);

		[Export ("registerObjectForUndo:")]
		void RegisterObject (IPSPDFUndoProtocol obj);

		[Export ("unregisterObjectForUndo:")]
		void UnregisterObject (IPSPDFUndoProtocol obj);

		[Export ("isObjectRegisteredForUndo:")]
		bool IsObjectRegistered (IPSPDFUndoProtocol obj);

		[Export ("prepareWithInvocationTarget:block:")]
		void PrepareWithInvocationTarget (NSObject target, Action<NSObject> block);

		[Export ("undoEnabled")]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; }

		[Export ("undoManager")]
		NSUndoManager UndoManager { get; }

		[Export ("timedCoalescingInterval")]
		double TimedCoalescingInterval { get; set; }

		[Export ("levelsOfUndo")]
		nuint LevelsOfUndo { get; set; }

		[Export ("performUndoAction:")]
		void PerformUndoAction (NSObject action);

		// PSPDFUndoController (TimeCoalescingSupport) Category

		[Export ("commitIncompleteUndoActions")]
		void CommitIncompleteUndoActions ();

		[Export ("hasIncompleteUndoActions")]
		bool HasIncompleteUndoActions { get; }

		[Export ("incompleteUndoActionName")]
		string IncompleteUndoActionName { get; }
	}

	delegate bool PSPDFFileManagerErrorHandler (NSUrl url, NSError error);

	interface IPSPDFFileManager { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileManager {

		[Export ("usesEncryption")]
		[Abstract]
		bool UsesEncryption { get; }

		[Export ("allowsPolicyEvent:")]
		[Abstract]
		bool AllowsPolicyEvent (string policyEvent);

		[Export ("copyFileToUnencryptedLocationIfRequired:policyEvent:error:")]
		[Abstract][return: NullAllowed]
		NSUrl CopyFileToUnencryptedLocationIfRequired ([NullAllowed] NSUrl fileUrl, string policyEvent, out NSError error);

		[Export ("cleanupIfTemporaryFile:")]
		[Abstract]
		bool CleanupIfTemporaryFile (NSUrl url);

		[Export ("createTemporaryWritableDataProviderWithPrefix:")]
		[Abstract]
		IPSPDFDataProvider CreateTemporaryWritableDataProvider ([NullAllowed] string prefix);

		[Export ("libraryDirectory")]
		[Abstract]
		string LibraryDirectory { get; }

		[Export ("applicationSupportDirectory")]
		[Abstract]
		string ApplicationSupportDirectory { get; }

		[Export ("cachesDirectory")]
		[Abstract]
		string CachesDirectory { get; }

		[Export ("documentDirectory")]
		[Abstract]
		string DocumentDirectory { get; }

		[Export ("temporaryDirectoryWithUID:")]
		[Abstract]
		string TemporaryDirectory ([NullAllowed] string uid);

		[Export ("unencryptedTemporaryDirectoryWithUID:")]
		[Abstract][return: NullAllowed]
		string UnencryptedTemporaryDirectory (string uid);

		[Export ("isNativePath:")]
		[Abstract]
		bool IsNativePath ([NullAllowed] string path);

		[Export ("fileExistsAtPath:")]
		[Abstract]
		bool FileExists ([NullAllowed] string path);

		[Export ("fileExistsAtPath:isDirectory:")]
		[Abstract]
		bool FileExistsh ([NullAllowed] string path, bool isDirectory);

		[Export ("fileExistsAtURL:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url);

		[Export ("fileExistsAtURL:isDirectory:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url, bool isDirectory);

		[Export ("createFileAtPath:contents:attributes:")]
		[Abstract]
		bool CreateFile (string path, [NullAllowed] NSData data, [NullAllowed] NSDictionary<NSString, NSObject> attributes);

		[Export ("createDirectoryAtPath:withIntermediateDirectories:attributes:error:")]
		[Abstract]
		bool CreateDirectory (string path, bool createIntermediates, [NullAllowed] NSDictionary<NSString, NSObject> attributes, out NSError error);

		[Export ("writeData:toFile:options:error:")]
		[Abstract]
		bool WriteData (NSData data, string path, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("writeData:toURL:options:error:")]
		[Abstract]
		bool WriteData (NSData data, NSUrl fileUrl, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("dataWithContentsOfFile:options:error:")]
		[Abstract]
		NSData DataWithContentsOfFile (string path, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("dataWithContentsOfURL:options:error:")]
		[Abstract]
		NSData DataWithContentsOfFile (NSUrl fileUrl, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("copyItemAtURL:toURL:error:")]
		[Abstract]
		bool CopyItem (NSUrl sourceUrl, NSUrl destinationUrl, out NSError error);

		[Export ("moveItemAtURL:toURL:error:")]
		[Abstract]
		bool MoveItem (NSUrl sourceUrl, NSUrl destinationUrl, out NSError error);

		[Export ("replaceItemAtURL:withItemAtURL:backupItemName:options:resultingItemURL:error:")]
		[Abstract]
		bool ReplaceItem (NSUrl originalItemUrl, NSUrl newItemUrl, [NullAllowed] string backupItemName, NSFileManagerItemReplacementOptions options, out NSUrl resultingUrl, out NSError error);

		[Export ("removeItemAtPath:error:")]
		[Abstract]
		bool RemoveItem (string path, out NSError error);

		[Export ("removeItemAtURL:error:")]
		[Abstract]
		bool RemoveItem (NSUrl Url, out NSError error);

		[Export ("attributesOfFileSystemForPath:error:")]
		[Abstract][return: NullAllowed]
		NSDictionary<NSString, NSObject> AttributesOfFileSystem (string path, out NSError error);

		[Export ("attributesOfItemAtPath:error:")]
		[Abstract][return: NullAllowed]
		NSDictionary<NSString, NSObject>  AttributesOfItem ([NullAllowed] string path, out NSError error);

		[Export ("isDeletableFileAtPath:")]
		[Abstract]
		bool IsDeletableFile (string path);

		[Export ("isWritableFileAtPath:")]
		[Abstract]
		bool IsWritableFile (string path);

		[Export ("contentsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] ContentsOfDirectory (string path, out NSError error);

		[Export ("subpathsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] SubpathsOfDirectory (string path, out NSError error);

		[Export ("enumeratorAtPath:")]
		[Abstract]
		NSDirectoryEnumerator EnumeratorAtPath (string path);

		[Export ("enumeratorAtURL:includingPropertiesForKeys:options:errorHandler:")]
		[Abstract]
		NSDirectoryEnumerator EnumeratorAtUrl (NSUrl url, string [] keys, NSDirectoryEnumerationOptions options, [NullAllowed] PSPDFFileManagerErrorHandler handler);

		[Export ("destinationOfSymbolicLinkAtPath:error:")]
		[Abstract]
		string DestinationOfSymbolicLink (string path, out NSError error);

		[Export ("fileSystemRepresentationForPath:")]
		[Abstract]
		sbyte FileSystemRepresentation (string path);

		[Export ("fileHandleForReadingFromURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForReading (NSUrl url, out NSError error, Func<NSFileHandle, bool> reader);

		[Export ("fileHandleForWritingToURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForWriting (NSUrl url, out NSError error, Func<NSFileHandle, bool> writer);

		[Export ("fileHandleForUpdatingURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForUpdating (NSUrl url, out NSError error, Func<NSFileHandle, bool> updater);
	}

	[Static]
	interface PSPDFFileManagerOption {

		[Field ("PSPDFFileManagerOptionCoordinatedAccess", "__Internal")]
		NSString CoordinatedAccess { get; }

		[Field ("PSPDFFileManagerOptionFilePresenter", "__Internal")]
		NSString FilePresenter { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultFileManager : PSPDFFileManager {

		[DesignatedInitializer]
		[Export ("initWithOptions:")]
		IntPtr Constructor ([NullAllowed] NSDictionary<NSString, NSObject> options);
	}

	delegate bool PSPDFDownloadManagerPredicateHandler (NSObject obj, nuint index, ref bool stop);

	interface IPSPDFDownloadManagerPolicy { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDownloadManagerPolicy {

		[Abstract]
		[Export ("hasPermissionForNetworkEvent")]
		bool HasPermissionForNetworkEvent { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFDownloadManager {

		[Notification]
		[Field ("PSPDFDownloadManagerDidStartLoadingTaskNotification", "__Internal")]
		NSString DidStartLoadingTaskNotification { get; }

		[Notification]
		[Field ("PSPDFDownloadManagerDidFinishLoadingTaskNotification", "__Internal")]
		NSString DidFinishLoadingTaskNotification { get; }

		[Notification]
		[Field ("PSPDFDownloadManagerDidFailToLoadTaskNotification", "__Internal")]
		NSString DidFailToLoadTaskNotification { get; }

		[Export ("numberOfConcurrentDownloads", ArgumentSemantic.Assign)]
		nuint NumberOfConcurrentDownloads { get; set; }

		[Export ("enableDynamicNumberOfConcurrentDownloads", ArgumentSemantic.Assign)]
		bool EnableDynamicNumberOfConcurrentDownloads { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFDownloadManagerDelegate Delegate { get; set; }

		[Export ("shouldFinishLoadingObjectsInBackground", ArgumentSemantic.Assign)]
		bool ShouldFinishLoadingObjectsInBackground { get; set; }

		[Export ("enqueueObject:")]
		void EnqueueObject (IPSPDFRemoteContentObject obj);

		[Export ("enqueueObject:atFront:")]
		void EnqueueObject (IPSPDFRemoteContentObject obj, bool enqueueAtFront);

		[Export ("enqueueObjects:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects);

		[Export ("enqueueObjects:atFront:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects, bool enqueueAtFront);

		[Export ("cancelObject:")]
		void CancelObject (IPSPDFRemoteContentObject obj);

		[Export ("cancelAllObjects")]
		void CancelAllObjects ();

		[Export ("reachability", ArgumentSemantic.Assign)]
		PSPDFReachability Reachability { get; }

		[Export ("waitingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] WaitingObjects { get; }

		[Export ("loadingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] LoadingObjects { get; }

		[Export ("failedObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] FailedObjects { get; }

		[Export ("objectsPassingTest:")]
		IPSPDFRemoteContentObject [] ObjectsPassingTest (PSPDFDownloadManagerPredicateHandler predicateHandler);

		[Export ("handlesObject:")]
		bool HandlesObject (IPSPDFRemoteContentObject obj);

		[Export ("stateForObject:")]
		PSPDFDownloadManagerObjectState StateForObject (IPSPDFRemoteContentObject obj);
	}

	delegate void PSPDFDownloadManagerDelegateAuthCompletionHandler (NSUrlSessionAuthChallengeDisposition disposition, NSUrlCredential credential);

	interface IPSPDFDownloadManagerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDownloadManagerDelegate {

		[Export ("downloadManager:authenticationChallenge:completionHandler:")]
		void AuthenticationChallenge (PSPDFDownloadManager downloadManager, NSUrlAuthenticationChallenge authenticationChallenge, PSPDFDownloadManagerDelegateAuthCompletionHandler completionHandler);

		[Export ("downloadManager:didChangeObject:")]
		void DidChangeObject (PSPDFDownloadManager downloadManager, IPSPDFRemoteContentObject obj);

		[Export ("downloadManager:reachabilityDidChange:")]
		void ReachabilityDidChange (PSPDFDownloadManager downloadManager, PSPDFReachability reachability);
	}

	delegate void PSPDFRemoteFileObjectRemoteObjectHandler (IPSPDFRemoteContentObject remoteObject);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFRemoteFileObject : IPSPDFRemoteContentObject {

		[Export ("initWithRemoteURL:targetURL:fileManager:")]
		IntPtr Constructor (NSUrl remoteUrl, NSUrl targetFileUrl, IPSPDFFileManager fileManager);

		[Export ("remoteURL", ArgumentSemantic.Copy)]
		NSUrl RemoteUrl { get; }

		[Export ("targetURL", ArgumentSemantic.Copy)]
		NSUrl TargetUrl { get; }

		[Export ("remoteContent", ArgumentSemantic.Strong), NullAllowed]
		NSUrl RemoteContent { get; set; }

		[Export ("loadingRemoteContent", ArgumentSemantic.Assign)]
		bool LoadingRemoteContent { [Bind ("isLoadingRemoteContent")] get; set; }

		[Export ("remoteContentProgress", ArgumentSemantic.Assign)]
		nfloat RemoteContentProgress { get; set; }

		[Export ("remoteContentError", ArgumentSemantic.Strong), NullAllowed]
		NSError RemoteContentError { get; set; }

		[Export ("remoteObject", ArgumentSemantic.Copy), NullAllowed]
		PSPDFRemoteFileObjectRemoteObjectHandler RemoteObject { get; set; }
	}

	interface IPSPDFSearchHighlightViewManagerDataSource { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchHighlightViewManagerDataSource : PSPDFOverridable {

		[Abstract]
		[Export ("shouldHighlightSearchResults")]
		bool ShouldHighlightSearchResults { get; }

		[Abstract]
		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchHighlightViewManager {

		[Export ("initWithDataSource:")]
		IntPtr Constructor (IPSPDFSearchHighlightViewManagerDataSource dataSource);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSearchHighlightViewManagerDataSource DataSource { get; }

		[Export ("hasVisibleSearchResults")]
		bool HasVisibleSearchResults { get; }

		[Export ("clearHighlightedSearchResultsAnimated:")]
		void ClearHighlightedSearchResults (bool animated);

		[Export ("addHighlightSearchResults:animated:")]
		void AddHighlightSearchResults (PSPDFSearchResult [] searchResults, bool animated);

		[Export ("animateSearchHighlight:")]
		void AnimateSearchHighlight (PSPDFSearchResult searchResult);
	}

	interface IPSPDFSearchResultViewable { }

	[Protocol]
	interface PSPDFSearchResultViewable {

		[Abstract]
		[Export ("configureWithSearchResult:")]
		void ConfigureWithSearchResult (PSPDFSearchResult searchResult);
	}

	interface IPSPDFSearchScopeViewable { }

	[Protocol]
	interface PSPDFSearchScopeViewable {

		[Abstract]
		[Export ("configureWithDocument:scope:pageIndexRange:results:")]
		void ConfigureWithDocument (PSPDFDocument document, PSPDFSearchResultScope scope, NSRange pageIndexRange, nuint results);
	}

	interface IPSPDFSearchStatusViewable { }

	[Protocol]
	interface PSPDFSearchStatusViewable {

		[Abstract]
		[Export ("configureWithSearchStatus:results:pageIndex:pageCount:")]
		void ConfigureWithSearchStatus (PSPDFSearchStatus searchStatus, nuint results, nuint pageIndex, nuint pageCount);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFTableViewCell {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}


	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFSpinnerCell {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// PSPDFSpinnerCell (SubclassingHooks) Category

		[Export ("spinner", ArgumentSemantic.Strong), NullAllowed]
		UIActivityIndicatorView Spinner { get; }

		[Export ("alignTextLabel")]
		void AlignTextLabel ();
	}

	interface IPSPDFMultiDocumentViewControllerDelegate	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMultiDocumentViewControllerDelegate {

		[Export ("multiPDFController:willChangeDocuments:")]
		void WillChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument [] newDocuments);

		[Export ("multiPDFController:didChangeDocuments:")]
		void DidChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument [] oldDocuments);

		[Export ("multiPDFController:willChangeVisibleDocument:")]
		void WillChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, [NullAllowed] PSPDFDocument newDocument);

		[Export ("multiPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, [NullAllowed] PSPDFDocument oldDocument);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFMultiDocumentViewController {

		[Export ("initWithPDFViewController:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] PSPDFViewController pdfController);

		[Export ("visibleDocument", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocument VisibleDocument { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFMultiDocumentViewControllerDelegate Delegate { get; set; }

		[Export ("enableAutomaticStatePersistence", ArgumentSemantic.Assign)]
		bool EnableAutomaticStatePersistence { get; set; }

		[Export ("statePersistenceKey")]
		string StatePersistenceKey { get; set; }

		[Export ("pdfController", ArgumentSemantic.Strong)]
		PSPDFViewController PdfController { get; }

		[Export ("thumbnailViewIncludesAllDocuments")]
		bool ThumbnailViewIncludesAllDocuments { get; set; }

		[Export ("showTitle", ArgumentSemantic.Assign)]
		bool ShowTitle { get; set; }

		[Export ("persistState")]
		void PersistState ();

		[Export ("restoreState")]
		bool RestoreState { get; }

		[Export ("restoreStateAndMergeWithDocuments:")]
		bool RestoreStateAndMerge (PSPDFDocument [] documents);

		// PSPDFMultiDocumentViewController (SubclassingHooks) Category

		[Advice ("Requires base call if overridden")]
		[Export ("commonInitWithPDFController:")]
		void CommonInitWithPdfController ([NullAllowed] PSPDFViewController pdfController);

		[Export ("titleForDocumentAtIndex:")]
		string GetTitleForDocument (nuint index);

		[Export ("titleDidChangeForDocumentAtIndex:")]
		string TitleDidChangeForDocument (nuint index);

		[Export ("persistViewStateForCurrentVisibleDocument")]
		void PersistViewStateForCurrentVisibleDocument ();

		[Export ("restoreViewStateForCurrentVisibleDocument")]
		void RestoreViewStateForCurrentVisibleDocument ();
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFBrightnessViewController {

		[Export ("brightnessManager", ArgumentSemantic.Retain), NullAllowed]
		PSPDFBrightnessManager BrightnessManager { get; set; }

		[Export ("appearanceModeManager", ArgumentSemantic.Retain), NullAllowed]
		PSPDFAppearanceModeManager AppearanceModeManager { get; set; }

		[Export ("allowedAppearanceModes", ArgumentSemantic.Assign)]
		PSPDFAppearanceMode AllowedAppearanceModes { get; set; }
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFAnnotationGridViewController : PSPDFStyleable {

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFAnnotationGridViewControllerDelegate Delegate { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFAnnotationGridViewControllerDataSource DataSource { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationGridViewController (SubclassingHooks) Category

		[Export ("close:")]
		void Close ([NullAllowed] NSObject sender);

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFAnnotationSetCell annotationSetCell, NSIndexPath indexPath);

		[Export ("collectionView", ArgumentSemantic.Strong), NullAllowed]
		UICollectionView CollectionView { get; }

		[Export ("updatePopoverSize")]
		void UpdatePopoverSize ();
	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFBaseViewController {

	}

	interface IPSPDFAnnotationGridViewControllerDataSource { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationGridViewControllerDataSource {

		[Export ("numberOfSectionsInAnnotationGridViewController:")]
		[Abstract]
		nint NumberOfSectionsInAnnotationGridViewController (PSPDFAnnotationGridViewController annotationGridController);

		[Export ("annotationGridViewController:numberOfAnnotationsInSection:")]
		[Abstract]
		nint NumberOfAnnotationsInSection (PSPDFAnnotationGridViewController annotationGridController, nint section);

		[Export ("annotationGridViewController:annotationSetForIndexPath:")]
		[Abstract]
		PSPDFAnnotationSet AnnotationSetForIndexPath (PSPDFAnnotationGridViewController annotationGridController, NSIndexPath indexPath);
	}

	interface IPSPDFTextStampViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextStampViewControllerDelegate : PSPDFOverridable {

		[Export ("textStampViewController:didCreateAnnotation:")]
		void DidCreateAnnotation (PSPDFTextStampViewController stampController, PSPDFStampAnnotation stampAnnotation);
	}

	interface IPSPDFAnnotationGridViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationGridViewControllerDelegate : PSPDFOverridable {

		[Export ("annotationGridViewControllerDidCancel:")]
		void AnnotationGridViewControllerDidCancel (PSPDFAnnotationGridViewController annotationGridController);

		[Export ("annotationGridViewController:didSelectAnnotationSet:")]
		void DidSelectAnnotationSet (PSPDFAnnotationGridViewController annotationGridController, PSPDFAnnotationSet annotationSet);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFTextStampViewController {

		[Export ("initWithStampAnnotation:")]
		IntPtr Constructor ([NullAllowed] PSPDFStampAnnotation stampAnnotation);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFTextStampViewControllerDelegate Delegate { get; set; }

		[Export ("stampAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStampAnnotation StampAnnotation { get; }

		[Export ("defaultStampText"), NullAllowed]
		string DefaultStampText { get; set; }
	}


	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFStampViewController : PSPDFAnnotationGridViewControllerDataSource, PSPDFTextStampViewControllerDelegate {

		[Static]
		[Export ("defaultStampAnnotations"), NullAllowed]
		PSPDFStampAnnotation [] DefaultStampAnnotations { get; set; }

		[Export ("stamps", ArgumentSemantic.Copy)]
		PSPDFStampAnnotation [] Stamps { get; set; }

		[Export ("customStampEnabled", ArgumentSemantic.Assign)]
		bool CustomStampEnabled { get; set; }

		[Export ("dateStampsEnabled", ArgumentSemantic.Assign)]
		bool DateStampsEnabled { get; set; }

		// PSPDFStampViewController (SubclassingHooks) Category

		[Export ("defaultDateStamps")]
		PSPDFStampAnnotation [] DefaultDateStamps ();
	}

	interface IPSPDFStyleable { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStyleable {

		[Export ("barStyle")]
		UIBarStyle GetBarStyle ();

		[Export ("setBarStyle:")]
		void SetBarStyle (UIBarStyle style);

		[Export ("isBarTranslucent")]
		bool IsBarTranslucent ();

		[Export ("setIsBarTranslucent:")]
		void SetIsBarTranslucent (bool isBarTranslucent);

		[Export ("forcesStatusBarHidden")]
		bool GetForcesStatusBarHidden ();

		[Export ("setForcesStatusBarHidden:")]
		void SetForcesStatusBarHidden (bool val);
	}


	interface IPSPDFSignatureViewControllerDelegate	{}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureViewControllerDelegate : PSPDFOverridable {

		[Export ("signatureViewControllerDidCancel:")]
		void SignatureViewControllerDidCancel (PSPDFSignatureViewController signatureController);

		[Export ("signatureViewControllerDidSave:")]
		void SignatureViewControllerDidSave (PSPDFSignatureViewController signatureController);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFSignatureViewController : PSPDFStyleable {

		[Export ("lines")]
		NSArray<NSValue>[] Lines { get; }

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("menuColors", ArgumentSemantic.Copy)]
		UIColor [] MenuColors { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSignatureViewControllerDelegate Delegate { get; set; }

		[Export ("keepLandscapeAspectRatio", ArgumentSemantic.Assign)]
		bool KeepLandscapeAspectRatio { get; set; }

		// PSPDFSignatureViewController (SubclassingHooks) Category

		[Export ("cancel:")]
		void Cancel ([NullAllowed] NSObject sender);

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);

		[Export ("clear:")]
		void Clear ([NullAllowed] NSObject sender);

		[Export ("color:")]
		void Color (PSPDFColorButton sender);

		[Export ("colorButtonForColor:")]
		PSPDFColorButton ColorButtonForColor (UIColor color);
	}

	interface IPSPDFFontPickerViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFontPickerViewControllerDelegate : PSPDFOverridable {

		[Export ("fontPickerViewController:didSelectFont:")]
		[Abstract]
		void DidSelectFont (PSPDFFontPickerViewController fontPickerViewController, [NullAllowed] UIFont selectedFont);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFFontPickerViewController {

		[Export ("initWithFontFamilyDescriptors:")]
		IntPtr Constructor ([NullAllowed] UIFontDescriptor [] fontFamilyDescriptors);

		[Export ("fontFamilyDescriptors", ArgumentSemantic.Copy), NullAllowed]
		UIFontDescriptor [] FontFamilyDescriptors { get; }

		[Export ("selectedFont", ArgumentSemantic.Strong), NullAllowed]
		UIFont SelectedFont { get; set; }

		[Export ("highlightedFontFamilyDescriptors", ArgumentSemantic.Assign), NullAllowed]
		UIFontDescriptor [] HighlightedFontFamilyDescriptors { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("showDownloadableFonts", ArgumentSemantic.Assign)]
		bool ShowDownloadableFonts { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFontPickerViewControllerDelegate Delegate { get; set; }

		// UIFontDescriptor (Blacklisting) Category

		[Static]
		[Export ("setPSPDFDefaultBlacklist:")]
		void SetDefaultBlacklist (string [] defaultBlacklist);

		[Static]
		[Export ("pspdf_defaultBlacklist")]
		string [] DefaultBlacklist { get; }
	}

	interface IPSPDFColorSelectionViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFColorSelectionViewControllerDelegate
	{

		[Export ("colorSelectionControllerSelectedColor:context:")]
		[Abstract]
		UIColor SelectedColor (UIViewController controller, NSObject context);

		[Export ("colorSelectionController:didSelectColor:finishedSelection:context:")]
		[Abstract]
		void DidSelectColor (UIViewController controller, UIColor color, bool finished, CGContext context);
	}

	interface IPSPDFNoteAnnotationViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFNoteAnnotationViewControllerDelegate : PSPDFOverridable {

		[Export ("noteAnnotationController:didDeleteAnnotation:")]
		void DidDeleteAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didClearContentsForAnnotation:")]
		void DidClearContentsForAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didChangeAnnotation:")]
		void DidChangeAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:willDismissWithAnnotation:")]
		void WillDismissWithAnnotation (PSPDFNoteAnnotationViewController noteController, [NullAllowed] PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFNoteAnnotationViewController : PSPDFStyleable {

		[Export ("initWithAnnotation:")]
		IntPtr Constructor (PSPDFAnnotation annotation);

		[Export ("annotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		[Export ("allowEditing", ArgumentSemantic.Assign)]
		bool AllowEditing { get; set; }

		[Export ("showColorAndIconOptions", ArgumentSemantic.Assign)]
		bool ShowColorAndIconOptions { get; set; }

		[Export ("showCopyButton", ArgumentSemantic.Assign)]
		bool ShowCopyButton { get; set; }

		[Export ("shouldBeginEditModeWhenPresented", ArgumentSemantic.Assign)]
		bool ShouldBeginEditModeWhenPresented { get; set; }

		[Export ("textView", ArgumentSemantic.Strong), NullAllowed]
		UITextView TextView { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFNoteAnnotationViewControllerDelegate Delegate { get; set; }

		// PSPDFNoteAnnotationViewController (SubclassingHooks) Category

		[Export ("deleteAnnotation:")]
		void DeleteAnnotation (UIBarButtonItem barButtonItem);

		[Export ("deleteOrClearAnnotationWithoutConfirmation")]
		void DeleteOrClearAnnotationWithoutConfirmation ();

		[Export ("deleteAnnotationActionTitle")]
		string DeleteAnnotationActionTitle { get; }

		[Export ("beginEditing", ArgumentSemantic.Assign)]
		bool BeginEditing { get; }

		[Advice ("Requires base call if overridden")]
		[Export ("updateTextView")]
		void UpdateTextView ();

		[Export ("backgroundView", ArgumentSemantic.Strong)]
		UIView BackgroundView { get; }

		[Export ("optionsView", ArgumentSemantic.Strong)]
		UIView OptionsView { get; }

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BorderColor { get; set; }

		[Export ("tapGesture", ArgumentSemantic.Strong)]
		UITapGestureRecognizer TapGesture { get; }

		[Export ("setupToolbar")]
		void SetupToolbar ();

		[Export ("updateToolbar")]
		void UpdateToolbar ();
	}

	[BaseType (typeof (PSPDFDocumentSharingCoordinator))]
	interface PSPDFOpenInCoordinator {

		[Field ("PSPDFDocumentInteractionControllerWillBeginSendingToApplicationNotification", "__Internal")]
		NSString WillBeginSendingToApplicationNotification { get; }

		[Field ("PSPDFDocumentInteractionControllerDidEndSendingToApplicationNotification", "__Internal")]
		NSString DidEndSendingToApplicationNotification { get; }

		// PSPDFOpenInCoordinator (SubclassingHooks)

		[Export ("documentInteractionController", ArgumentSemantic.Weak)]
		UIDocumentInteractionController DocumentInteractionController { get; }
	}

	interface IPSPDFAnnotationTableViewControllerDelegate {	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationTableViewControllerDelegate : PSPDFOverridable	{

		[Export ("annotationTableViewController:didSelectAnnotation:")][Abstract]
		void DidSelectAnnotation (PSPDFAnnotationTableViewController annotationController, PSPDFAnnotation annotation);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStatefulTableViewController {

		[Export ("emptyView", ArgumentSemantic.Strong), NullAllowed]
		UIView EmptyView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		UIView LoadingView { get; set; }

		[Export ("controllerState", ArgumentSemantic.Assign)]
		PSPDFStatefulTableViewState ControllerState { get; set; }

		[Export ("setControllerState:animated:")]
		void SetControllerState (PSPDFStatefulTableViewState controllerState, bool animated);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFAnnotationTableViewController : PSPDFStyleable {

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFAnnotationTableViewControllerDelegate Delegate { get; set; }

		[Export ("visibleAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSSet<NSString> VisibleAnnotationTypes { get; set; }

		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSSet<NSString> EditableAnnotationTypes { get; set; }

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Export ("showDeleteAllOption", ArgumentSemantic.Assign)]
		bool ShowDeleteAllOption { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationTableViewController (SubclassingHooks) Category

		[Export ("annotationsForPageAtIndex:")]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Obsolete ("Use the TableView overload")]
		[return: NullAllowed]
		[Export ("annotationForIndexPath:")]
		PSPDFAnnotation GetAnnotation (NSIndexPath indexPath);

		[return: NullAllowed]
		[Export ("annotationForIndexPath:inTableView:")]
		PSPDFAnnotation GetAnnotation (NSIndexPath indexPath, UITableView tableView);

		[Export ("deleteAllAction:")]
		void DeleteAllAction (NSObject sender);

		[Export ("viewForTableViewFooter")]
		[return: NullAllowed]
		UIView ViewForTableViewFooter ();
	}

	interface IPSPDFAnnotationSetStore { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationSetStore {

		[Abstract]
		[Export ("annotationSets", ArgumentSemantic.Copy)]
		PSPDFAnnotationSet [] AnnotationSets { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFKeychainAnnotationSetsStore : PSPDFAnnotationSetStore {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotationGridViewController))]
	interface PSPDFSavedAnnotationsViewController : PSPDFAnnotationGridViewControllerDataSource, PSPDFStyleable {

		[Static]
		[Export ("sharedAnnotationStore")]
		IPSPDFAnnotationSetStore SharedAnnotationStore { get; }

		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFAnnotationGridViewControllerDelegate aDelegate);

		[Export ("annotationStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFAnnotationSetStore AnnotationStore { get; set; }

		// PSPDFSavedAnnotationsViewController (SubclassingHooks) Category

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbarAnimated (bool animated);
	}

	interface IPSPDFContainerViewControllerDelegate	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFContainerViewControllerDelegate {

		[Export ("containerViewController:didUpdateSelectedIndex:")]
		void DidUpdateSelectedIndex (PSPDFContainerViewController controller, nuint selectedIndex);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFContainerViewController : PSPDFStyleable {

		[Export ("initWithControllers:titles:")]
		IntPtr Constructor ([NullAllowed] UIViewController [] controllers, [NullAllowed] string [] titles);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFContainerViewControllerDelegate Delegate { get; set; }

		[Export ("addViewController:")]
		void AddViewController (UIViewController controller);

		[Export ("removeViewController:")]
		void RemoveViewController (UIViewController controller);

		[Export ("viewControllers", ArgumentSemantic.Copy)]
		UIViewController [] ViewControllers { get; }

		[Export ("visibleViewControllerIndex", ArgumentSemantic.Assign)]
		nuint VisibleViewControllerIndex { get; set; }

		[Export ("setVisibleViewControllerIndex:animated:")]
		void SetVisibleViewControllerIndex (nuint visibleViewControllerIndex, bool animated);

		[Export ("shouldAnimateChanges", ArgumentSemantic.Assign)]
		bool ShouldAnimateChanges { get; set; }

		[Export ("lastVisibleViewControllerTitleKey")]
		string LastVisibleViewControllerTitleKey { get; set; }

		// PSPDFSavedAnnotationsViewController (SubclassingHooks) Category

		[Export ("filterSegment", ArgumentSemantic.Strong), NullAllowed]
		UISegmentedControl FilterSegment { get; }
	}

	interface IPSPDFWebViewControllerDelegate {	}

	delegate void PSPDFWebViewControllerDelegateHandleExternalUrlHandler (bool switchedApplication);

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFWebViewControllerDelegate : PSPDFExternalURLHandler {

		[Export ("webViewControllerDidStartLoad:")]
		void DidStartLoad (PSPDFWebViewController controller);

		[Export ("webViewControllerDidFinishLoad:")]
		void DidFinishLoad (PSPDFWebViewController controller);

		[Export ("webViewController:didFailLoadWithError:")]
		void DidFailLoad (PSPDFWebViewController controller, NSError error);
	}


	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFWebViewController : PSPDFStyleable {

		[Notification]
		[Field ("PSPDFWebViewControllerDidStartLoadingNotification", "__Internal")]
		NSString DidStartLoadingNotification { get; }

		[Notification]
		[Field ("PSPDFWebViewControllerDidFinishLoadingNotification", "__Internal")]
		NSString DidFinishLoadingNotification { get; }

		[Notification]
		[Field ("PSPDFWebViewControllerDidFailToLoadNotification", "__Internal")]
		NSString DidFailToLoadNotification { get; }

		[Static]
		[Export ("modalWebViewWithURL:")]
		UINavigationController GetModalWebView (NSUrl url);

		[Export ("initWithURLRequest:")]
		IntPtr Constructor (NSUrlRequest request);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("availableActions", ArgumentSemantic.Assign)]
		PSPDFWebViewControllerAvailableActions AvailableActions { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFWebViewControllerDelegate Delegate { get; set; }

		[Export ("showProgressIndicator", ArgumentSemantic.Assign)]
		bool ShowProgressIndicator { get; set; }

		[Export ("useCustomErrorPage", ArgumentSemantic.Assign)]
		bool UseCustomErrorPage { get; set; }

		[Export ("shouldUpdateTitleFromWebContent", ArgumentSemantic.Assign)]
		bool ShouldUpdateTitleFromWebContent { get; set; }

		[Export ("useModernWebKit", ArgumentSemantic.Assign)]
		bool UseModernWebKit { get; set; }

		[Advice ("Use values from UIActivityType class")]
		[Export ("excludedActivities", ArgumentSemantic.Copy)]
		NSString [] ExcludedActivities { get; set; }

		[Export ("suppressesIncrementalRendering", ArgumentSemantic.Assign)]
		bool SuppressesIncrementalRendering { get; set; }

		// PSPDFWebViewController (SubclassingHooks) Category

		[Export ("webView", ArgumentSemantic.Strong)]
		UIView WebView { get; }

		[Export ("showHTMLWithError:")]
		void ShowHtml (NSError error);

		[Export ("createDefaultActivityViewController"), NullAllowed]
		UIActivityViewController CreateDefaultActivityViewController { get; }

		[Export ("goBack:")]
		void GoBack ([NullAllowed] NSObject sender);

		[Export ("goForward:")]
		void GoForward ([NullAllowed] NSObject sender);

		[Export ("reload:")]
		void Reload ([NullAllowed] NSObject sender);

		[Export ("stop:")]
		void Stop ([NullAllowed] NSObject sender);

		[Export ("action:")]
		void Action ([NullAllowed] UIBarButtonItem sender);

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSoundAnnotationController {

		[Field ("PSPDFSoundAnnotationChangedStateNotification", "__Internal")]
		[Notification]
		NSString ChangedStateNotification { get; }

		[Field ("PSPDFSoundAnnotationStopAllNotification", "__Internal")]
		[Notification]
		NSString StopAllNotification { get; }

		[Static]
		[Export ("stopRecordingOrPlaybackForAllExcept:")]
		void StopRecordingOrPlaybackForAllExcept ([NullAllowed] NSObject sender);

		[Static]
		[Export ("requestRecordPermission:")]
		void RequestRecordPermission ([NullAllowed] Action<bool> hanlder);

		[Export ("initWithSoundAnnotation:")]
		IntPtr Constructor (PSPDFSoundAnnotation annotation);

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFSoundAnnotation Annotation { get; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFSoundAnnotationState State { get; }

		[Export ("playbackDuration")]
		double PlaybackDuration { get; }

		[Export ("startPlayback:")]
		bool StartPlayback (out NSError error);

		[Export ("pause")]
		void Pause ();

		[Export ("stop:")]
		bool Stop (out NSError error);

		// NO WATCH
		[Export ("audioPlayer", ArgumentSemantic.Strong), NullAllowed]
		AVAudioPlayer AudioPlayer { get; }

		[Export ("startRecording:")]
		bool StartRecording (out NSError error);

		[Export ("discardRecording")]
		void DiscardRecording ();
	}

	[BaseType (typeof (UINavigationController))]
	interface PSPDFNavigationController {

		[Export ("rotationForwardingEnabled", ArgumentSemantic.Assign)]
		bool RotationForwardingEnabled { [Bind ("isRotationForwardingEnabled")] get; set; }

		[Export ("persistentCloseButtonMode", ArgumentSemantic.Assign)]
		PSPDFPersistentCloseButtonMode PersistentCloseButtonMode { get; set; }

		[Export ("persistentCloseButton", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem PersistentCloseButton { get; set; }
	}

	interface IPSPDFJSONSerializing { }

	[Protocol]
	interface PSPDFJSONSerializing {

		[Static]
		[Abstract]
		[Export ("JSONKeyPathsByPropertyKey")]
		NSDictionary<NSString, NSObject> JSONKeyPathsByPropertyKey ();

		[Static]
		[Export ("JSONTransformerForKey:")]
		[return: NullAllowed]
		NSValueTransformer JSONTransformerForKey (string key);

		[Static]
		[Export ("classForParsingJSONDictionary:")]
		[return: NullAllowed]
		Class ClassForParsingJSONDictionary (NSDictionary<NSString, NSObject> JSONDictionary);
	}


	[Static]
	interface PSPDFActionOption
	{
		[Field ("PSPDFActionOptionModalKey", "__Internal")]
		NSString Modal { get; }

		[Field ("PSPDFActionOptionAutoplayKey", "__Internal")]
		NSString Autoplay { get; }

		[Field ("PSPDFActionOptionControlsKey", "__Internal")]
		NSString Controls { get; }

		[Field ("PSPDFActionOptionLoopKey", "__Internal")]
		NSString Loop { get; }

		[Field ("PSPDFActionOptionOffsetKey", "__Internal")]
		NSString Offset { get; }

		[Field ("PSPDFActionOptionSizeKey", "__Internal")]
		NSString Size { get; }

		[Field ("PSPDFActionOptionPopoverKey", "__Internal")]
		NSString Popover { get; }

		[Field ("PSPDFActionOptionCoverKey", "__Internal")]
		NSString Cover { get; }

		[Field ("PSPDFActionOptionPageKey", "__Internal")]
		NSString Page { get; }

		[Field ("PSPDFActionOptionButtonKey", "__Internal")]
		NSString Button { get; }

		[Field ("PSPDFActionOptionCloseButtonKey", "__Internal")]
		NSString CloseButton { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAction : PSPDFJSONSerializing, INativeObject {

		[Field ("PSPDFActionOptionFullscreenKey", "__Internal")]
		NSString OptionFullscreenKey { get; }

		[Field ("PSPDFActionTypeTransformerName", "__Internal")]
		NSString ActionTypeTransformerName { get; }

		[Static]
		[Export ("actionClassForType:")]
		Class ActionClassForType (PSPDFActionType actionType);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFActionType Type { get; }

		[Export ("parentAction", ArgumentSemantic.Weak)]
		PSPDFAction ParentAction { get; }

		[Export ("subActions", ArgumentSemantic.Strong)]
		PSPDFAction [] SubActions { get; set; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; }

		[Export ("localizedDescriptionWithDocumentProvider:")]
		string LocalizedDescription ([NullAllowed] PSPDFDocumentProvider documentProvider);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFGoToAction {

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (nuint pageIndex);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFRemoteGoToAction {

		[Export ("initWithRelativePath:pageIndex:")]
		IntPtr Constructor ([NullAllowed] string remotePath, nuint pageIndex);

		[Export ("relativePath"), NullAllowed]
		string RelativePath { get; }
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFEmbeddedGoToAction {

		[Export ("initWithRelativePath:targetRelationship:openInNewWindow:pageIndex:")]
		IntPtr Constructor (string remotePath, PSPDFEmbeddedGoToActionTarget targetRelationship, bool openInNewWindow, nuint pageIndex);

		[Export ("targetRelationship", ArgumentSemantic.Assign)]
		PSPDFEmbeddedGoToActionTarget TargetRelationship { get; }

		[Export ("relativePath"), NullAllowed]
		string RelativePath { get; }

		[Export ("openInNewWindow", ArgumentSemantic.Assign)]
		bool OpenInNewWindow { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFURLAction {

		[Export ("initWithURLString:")]
		IntPtr Constructor (string urlString);

		[Export ("initWithURL:options:")]
		IntPtr Constructor ([NullAllowed] NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("URL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl Url { get; }

		[Export ("unmodifiedURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl UnmodifiedUrk { get; }

		[Export ("updateURLWithAnnotationManager:")]
		bool UpdateUrl (PSPDFAnnotationManager annotationManager);

		[Export ("isPSPDFPrefixed", ArgumentSemantic.Assign)]
		bool IsPSPDFPrefixed { get; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }

		[Export ("modal", ArgumentSemantic.Assign)]
		bool Modal { [Bind ("isModal")] get; set; }

		[Export ("popover", ArgumentSemantic.Assign)]
		bool Popover { [Bind ("isPopover")] get; set; }

		[Export ("button", ArgumentSemantic.Assign)]
		bool Button { [Bind ("isButton")] get; set; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		[Export ("offset", ArgumentSemantic.Assign)]
		nfloat Offset { get; set; }

		[Export ("prefixedURLStringWithAnnotationManager:")]
		string PrefixedUrlString (PSPDFAnnotationManager annotationManager);

		[Export ("emailURL", ArgumentSemantic.Assign)]
		bool EmailUrl { [Bind ("isEmailURL")] get; }

		[Export ("localPDFURL", ArgumentSemantic.Assign)]
		bool LocalPdfUrl { [Bind ("isLocalPDFURL")] get; }

		[Export ("configureMailComposeViewController:")]
		bool ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFNamedAction {

		[Field ("PSPDFNamedActionTypeTransformerName", "__Internal")]
		NSString TypeTransformerName { get; }

		[Export ("initWithActionNamedString:")]
		IntPtr Constructor ([NullAllowed] string actionNameString);

		[Export ("namedActionType", ArgumentSemantic.Assign)]
		PSPDFNamedActionType NamedActionType { get; }

		[Export ("namedAction"), NullAllowed]
		string NamedAction { get; }

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		nuint PageIndexWithCurrentPage (nuint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFJavaScriptAction {

		[Export ("initWithScript:")]
		IntPtr Constructor (string script);

		[Export ("script"), NullAllowed]
		string Script { get; }

		[Export ("executeScriptAppliedToDocumentProvider:application:eventDictionary:sender:error:")]
		NSDictionary<NSString, NSObject> ExecuteScriptAppliedToDocumentProvider (PSPDFDocumentProvider documentProvider, [NullAllowed] NSObject application, [NullAllowed] NSDictionary<NSString, NSObject> eventDictionary, NSObject sender, out NSError error);
	}

	[Static]
	interface PSPDFActionEvent {
		
		[Field ("PSPDFActionEventValueKey", "__Internal")]
		NSString ValueKey { get; }

		[Field ("PSPDFActionEventNameKey", "__Internal")]
		NSString NameKey { get; }

		[Field ("PSPDFActionEventTypeKey", "__Internal")]
		NSString TypeKey { get; }

		[Field ("PSPDFActionEventSourceKey", "__Internal")]
		NSString SourceKey { get; }

		[Field ("PSPDFActionEventTargetKey", "__Internal")]
		NSString TargetKey { get; }

		[Field ("PSPDFActionEventTargetNameKey", "__Internal")]
		NSString TargetNameKey { get; }

		[Field ("PSPDFActionEventRCKey", "__Internal")]
		NSString RCKey { get; }

		[Field ("PSPDFActionEventChangeKey", "__Internal")]
		NSString ChangeKey { get; }

		[Field ("PSPDFActionEventWillCommitKey", "__Internal")]
		NSString WillCommitKey { get; }

		[Field ("PSPDFActionEventSelStartKey", "__Internal")]
		NSString SelStartKey { get; }

		[Field ("PSPDFActionEventSelEndKey", "__Internal")]
		NSString SelEndKey { get; }

		[Field ("PSPDFActionEventNameValueMouseDown", "__Internal")]
		NSString NameValueMouseDown { get; }

		[Field ("PSPDFActionEventNameValueMouseUp", "__Internal")]
		NSString NameValueMouseUp { get; }

		[Field ("PSPDFActionEventNameValueMouseEnter", "__Internal")]
		NSString ValueMouseEnter { get; }

		[Field ("PSPDFActionEventNameValueMouseExit", "__Internal")]
		NSString NameValueMouseExit { get; }

		[Field ("PSPDFActionEventNameValueFormat", "__Internal")]
		NSString NameValueFormat { get; }

		[Field ("PSPDFActionEventNameValueCalculate", "__Internal")]
		NSString NameValueCalculate { get; }

		[Field ("PSPDFActionEventNameValueValidate", "__Internal")]
		NSString NameValueValidate { get; }

		[Field ("PSPDFActionEventNameValueKeystroke", "__Internal")]
		NSString NameValueKeystroke { get; }

		[Field ("PSPDFActionEventNameValueBlur", "__Internal")]
		NSString NameValueBlur { get; }

		[Field ("PSPDFActionEventNameValueFocus", "__Internal")]
		NSString NameValueFocus { get; }

		[Field ("PSPDFActionEventTypeValueField", "__Internal")]
		NSString TypeValueField { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRenditionAction {

		[Field ("PSPDFRenditionActionTypeTransformerName", "__Internal")]
		NSString TypeTransformerName { get; }

		[Export ("initWithActionType:javaScript:annotation:")]
		IntPtr Constructor (PSPDFRenditionActionType actionType, [NullAllowed] string javaScript, [NullAllowed] PSPDFScreenAnnotation annotation);

		[Export ("actionType", ArgumentSemantic.Assign)]
		PSPDFRenditionActionType ActionType { get; }

		[Export ("annotation", ArgumentSemantic.Weak), NullAllowed]
		PSPDFScreenAnnotation Annotation { get; }

		[Export ("javaScript"), NullAllowed]
		string JavaScript { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRichMediaExecuteAction {

		[Export ("initWithCommand:argument:annotation:")]
		IntPtr Constructor ([NullAllowed] string command, [NullAllowed] NSObject argument, [NullAllowed] PSPDFRichMediaAnnotation annotation);

		[Export ("command"), NullAllowed]
		string Command { get; }

		[Export ("argument"), NullAllowed]
		NSObject Argument { get; }

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFRichMediaAnnotation Annotation { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFAbstractFormAction {

		[Export ("fields", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] Fields { get; set; }
	}


	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFSubmitFormAction {

		[Export ("initWithURL:flags:")]
		IntPtr Constructor ([NullAllowed] NSUrl url, PSPDFSubmitFormActionFlag flags);

		[Export ("URL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl Url { get; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFSubmitFormActionFlag Flags { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFResetFormAction {

		[Export ("initWithFlags:")]
		IntPtr Constructor (PSPDFResetFormActionFlag flags);

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFResetFormActionFlag Flags { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFHideAction {

		[Export ("initWithAssociatedAnnotations:shouldHide:")]
		IntPtr Constructor (PSPDFAnnotation [] annotations, bool shouldHide);

		[Export ("shouldHide", ArgumentSemantic.Assign)]
		bool ShouldHide { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }
	}

	interface IPSPDFEraseOverlayDataSource { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFEraseOverlayDataSource {

		[Export ("zoomScaleForEraseOverlay:")]
		[Abstract]
		nfloat ZoomScaleForEraseOverlay (PSPDFEraseOverlay overlay);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFEraseOverlay {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("circleVisible", ArgumentSemantic.Assign)]
		bool CircleVisible { get; set; }

		[Export ("circleLineWidth", ArgumentSemantic.Assign)]
		nfloat CircleLineWidth { get; set; }

		[Export ("circleRadius", ArgumentSemantic.Assign)]
		nfloat CircleRadius { get; set; }

		[Export ("circleColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor CircleColor { get; set; }

		[Export ("shapeLayer")]
		CAShapeLayer ShapeLayer { get; }

		[Export ("touchPosition", ArgumentSemantic.Assign)]
		CGPoint TouchPosition { get; set; }

		[Export ("tracking", ArgumentSemantic.Assign)]
		bool Tracking { get; set; }

		[Export ("setTracking:animated:")]
		void SetTracking (bool tracking, bool animated);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFEraseOverlayDataSource DataSource { get; set; }
	}

	interface IPSPDFPageLabelViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPageLabelViewDelegate {

		[Export ("pageLabelView:didPressThumbnailGridButton:")]
		[Abstract]
		void DidPressThumbnailGridButton (PSPDFPageLabelView pageLabelView, UIButton sender);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLabelView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("label", ArgumentSemantic.Strong)]
		UILabel Label { get; }

		[Export ("labelMargin", ArgumentSemantic.Assign)]
		nfloat LabelMargin { get; set; }

		[Export ("labelStyle", ArgumentSemantic.Assign)]
		PSPDFLabelStyle LabelStyle { get; set; }

		[Export ("blurEffectStyle", ArgumentSemantic.Assign)]
		UIBlurEffectStyle BlurEffectStyle { get; set; }

		[Export ("textColor", ArgumentSemantic.Strong)]
		UIColor TextColor { get; }
	}

	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFPageLabelView {

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPageLabelViewDelegate Delegate { get; set; }

		[Export ("showThumbnailGridButton", ArgumentSemantic.Assign)]
		bool ShowThumbnailGridButton { get; set; }

		[Export ("thumbnailGridButton", ArgumentSemantic.Strong)]
		UIButton ThumbnailGridButton { get; set; }

		[Export ("thumbnailButtonColor", ArgumentSemantic.Strong)]
		UIColor ThumbnailButtonColor { get; set; }

		[Export ("updateLabelWithDocument:pageIndex:visiblePageIndexes:")]
		bool UpdateLabel (PSPDFDocument document, nuint pageIndex, NSOrderedSet<NSNumber> visiblePageIndexes);

		// Category

		[Export ("pageLabelWithDocument:pageIndex:visiblePageIndexes:")]
		string GetPageLabel (PSPDFDocument document, nuint pageIndex, NSOrderedSet<NSNumber> visiblePageIndexes);

		[Export ("updateFrame")]
		void UpdateFrame ();
	}

	[BaseType (typeof (PSPDFLabelView))]
	interface PSPDFDocumentLabelView {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFRelayTouchesView))]
	interface PSPDFHUDView : PSPDFThumbnailBarDelegate, PSPDFScrubberBarDelegate, PSPDFPageLabelViewDelegate {

		[Export ("initWithFrame:dataSource:")]
		IntPtr Constructor (CGRect frame, [NullAllowed] IPSPDFPresentationContext dataSource);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext DataSource { get; set; }

		[Export ("layoutSubviewsAnimated:")]
		void LayoutSubviews (bool animated);

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("pageLabelInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets PageLabelInsets { get; set; }

		[Export ("documentLabelInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets DocumentLabelInsets { get; set; }

		[Export ("thumbnailBarInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailBarInsets { get; set; }

		[Export ("scrubberBarInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets ScrubberBarInsets { get; set; }

		// PSPDFHUDView (Subviews) Category

		[Export ("documentLabel", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocumentLabelView DocumentLabel { get; }

		[Export ("pageLabel", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageLabelView PageLabel { get; }

		[Export ("scrubberBar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFScrubberBar ScrubberBar { get; }

		[Export ("thumbnailBar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailBar ThumbnailBar { get; }

		[Export ("backButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBackForwardButton BackButton { get; }

		[Export ("forwardButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBackForwardButton ForwardButton { get; }

		// PSPDFHUDView (SubclassingHooks) Category

		[Export ("updateDocumentLabelFrameAnimated:")]
		void UpdateDocumentLabelFrame (bool animated);

		[Export ("updatePageLabelFrameAnimated:")]
		void UpdatePageLabelFrame (bool animated);

		[Export ("updateThumbnailBarFrameAnimated:")]
		void UpdateThumbnailBarFrame (bool animated);

		[Export ("updateScrubberBarFrameAnimated:")]
		void UpdateScrubberBarFrame (bool animated);
	}

	interface IPSPDFTextSearchDelegate {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSearchDelegate {

		[Export ("willStartSearch:term:isFullSearch:")]
		void WillStartSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);

		[Export ("didUpdateSearch:term:newSearchResults:pageIndex:")]
		void DidUpdateSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, nuint pageIndex);

		[Export ("didFinishSearch:term:searchResults:isFullSearch:pageTextFound:")]
		void DidFinishSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, bool isFullSearch, bool pageTextFound);

		[Export ("didCancelSearch:term:isFullSearch:")]
		void DidCancelSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSearch : INSCopying {

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("searchForString:")]
		void SearchForString (string searchTerm);

		[Export ("searchForString:inRanges:rangesOnly:cancelOperations:")]
		void SearchForString (string searchTerm, [NullAllowed] NSIndexSet ranges, bool rangesOnly, bool cancelOperations);

		[Export ("cancelAllOperations")]
		void CancelAllOperations ();

		[Export ("cancelAllOperationsAndWait")]
		void CancelAllOperationsAndWait ();

		[Export ("compareOptions", ArgumentSemantic.Assign)]
		NSStringCompareOptions CompareOptions { get; set; }

		[Export ("previewRange", ArgumentSemantic.Assign)]
		NSRange PreviewRange { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("maximumNumberOfSearchResults", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResults { get; set; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFTextSearchDelegate Delegate { get; set; }

		// PSPDFTextSearch (SubclassingHooks) Category

		[Export ("searchQueue", ArgumentSemantic.Strong), NullAllowed]
		NSOperationQueue SearchQueue { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextParser {

		[Export ("text")]
		string Text { get; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("textBlocks", ArgumentSemantic.Copy)]
		PSPDFTextBlock [] TextBlocks { get; }

		[Export ("images", ArgumentSemantic.Copy)]
		PSPDFImageInfo [] Images { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("textWithGlyphs:")]
		string GetTextWithGlyphs (PSPDFGlyph [] glyphs);

		[Export ("glyphsInRange:")]
		PSPDFGlyph [] GetGlyphsInRange (NSRange range);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGlyph : INSCopying {

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; }

		[Export ("isWordBreaker", ArgumentSemantic.Assign)]
		bool IsWordBreaker { get; }

		[Export ("isWordOrLineBreaker", ArgumentSemantic.Assign)]
		bool IsWordOrLineBreaker { get; }

		[Export ("indexOnPage", ArgumentSemantic.Assign)]
		nint IndexOnPage { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFWord {

		[Export ("initWithGlyphs:pageRotation:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFGlyph [] wordGlyphs, nuint pageRotation);

		[Export ("initWithFrame:pageRotation:")]
		[DesignatedInitializer]
		IntPtr Constructor (CGRect wordFrame, nuint pageRotation);

		[Export ("stringValue")]
		string StringValue { get; }

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; set; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; set; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; set; }

		[Export ("pageRotation")]
		nuint PageRotation { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextBlock {

		[Export ("initWithGlyphs:frame:pageRotation:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFGlyph [] glyphs, CGRect frame, nuint pageRotation);

		[Export ("initWithGlyphs:")]
		IntPtr Constructor (PSPDFGlyph [] glyphs);

		[Export ("frame")]
		CGRect Frame { get; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("pageRotation")]
		nuint PageRotation { get; }

		[Export ("isEqualToTextBlock:")]
		bool IsEqualTo (PSPDFTextBlock otherBlock);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFImageInfo {

		[Export ("index")]
		nuint Index { get; }

		[Export ("pixelSize", ArgumentSemantic.Assign)]
		int PixelSize { get; }

		[Export ("transform", ArgumentSemantic.Assign)]
		CGAffineTransform Transform { get; }

		[Export ("vertices", ArgumentSemantic.Assign)]
		NSValue [] Vertices { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("displaySize", ArgumentSemantic.Assign)]
		CGSize DisplaySize { get; }

		[Export ("horizontalResolution", ArgumentSemantic.Assign)]
		nfloat HorizontalResolution { get; }

		[Export ("verticalResolution", ArgumentSemantic.Assign)]
		nfloat VerticalResolution { get; }

		[Export ("hitTest:")]
		bool HitTest (CGPoint point);

		[Export ("boundingBox")]
		CGRect BoundingBox { get; }

		[Export ("imageWithError:"), Internal]
		UIImage GetImage (IntPtr error);

		[Export ("imageInRGBColorSpaceWithError:"), Internal]
		UIImage GetImageInRgbColorSpace (IntPtr error);
	}

	interface IPSPDFSearchViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchViewControllerDelegate : PSPDFTextSearchDelegate, PSPDFOverridable {

		[Export ("searchViewController:didTapSearchResult:")]
		void DidTapSearchResult (PSPDFSearchViewController searchController, PSPDFSearchResult searchResult);

		[Export ("searchViewControllerDidClearAllSearchResults:")]
		void DidClearAllSearchResults (PSPDFSearchViewController searchController);

		[Export ("searchViewControllerGetVisiblePages:")]
		NSNumber [] GetVisiblePages (PSPDFSearchViewController searchController);

		[return: NullAllowed]
		[Export ("searchViewController:searchRangeForScope:")]
		NSIndexSet SearchRangeForScope (PSPDFSearchViewController searchController, string scope);

		[Export ("searchViewControllerTextSearchObject:")]
		PSPDFTextSearch TextSearchObject (PSPDFSearchViewController searchController);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFSearchViewController : PSPDFTextSearchDelegate, PSPDFStyleable {

		[Static]
		[Export ("resultCellClass")]
		Class GetResultCellClass ();

		[Static]
		[Export ("scopeHeaderClass")]
		Class GetScopeHeaderClass ();

		[Static]
		[Export ("statusFooterClass")]
		Class GetStatusFooterClass ();

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSearchViewControllerDelegate Delegate { get; set; }

		[Export ("searchText"), NullAllowed]
		string SearchText { get; set; }

		[Export ("showsCancelButton", ArgumentSemantic.Assign)]
		bool ShowsCancelButton { get; set; }

		[Export ("searchBar", ArgumentSemantic.Strong)]
		UISearchBar SearchBar { get; }

		[Export ("searchStatus", ArgumentSemantic.Assign)]
		PSPDFSearchStatus SearchStatus { get; }

		[Export ("clearHighlightsWhenClosed", ArgumentSemantic.Assign)]
		bool ClearHighlightsWhenClosed { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("searchVisiblePagesFirst", ArgumentSemantic.Assign)]
		bool SearchVisiblePagesFirst { get; set; }

		[Export ("numberOfPreviewTextLines", ArgumentSemantic.Assign)]
		nuint NumberOfPreviewTextLines { get; set; }

		[Export ("useOutlineForPageNames", ArgumentSemantic.Assign)]
		bool UseOutlineForPageNames { get; set; }

		[Export ("restoreLastSearchResult", ArgumentSemantic.Assign)]
		bool RestoreLastSearchResult { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("searchBarPinning", ArgumentSemantic.Assign)]
		PSPDFSearchBarPinning SearchBarPinning { get; set; }

		[Export ("textSearch", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSearch TextSearch { get; }

		[Export ("restartSearch")]
		void RestartSearch ();

		// PSPDFSearchViewController (SubclassingHooks) Category

		[Export ("filterContentForSearchText:scope:")]
		void FilterContentForSearchText (string searchText, [NullAllowed] string scope);

		[Export ("setSearchStatus:updateTable:")]
		void SetSearchStatus (PSPDFSearchStatus searchStatus, bool updateTable);

		[Export ("searchResultForIndexPath:")]
		PSPDFSearchResult SearchResultForIndexPath (NSIndexPath indexPath);

		[Export ("createSearchBar", ArgumentSemantic.Strong)]
		UISearchBar CreateSearchBar { get; }

		[Export ("searchResults")]
		PSPDFSearchResult [] SearchResults { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSearchResult : INativeObject {

		[Export ("initWithDocumentUID:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		IntPtr Constructor (string documentUid, nuint pageIndex, NSRange range, [NullAllowed] string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("initWithDocument:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		IntPtr Constructor (PSPDFDocument document, nuint pageIndex, NSRange range, [NullAllowed] string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; }

		[Export ("previewText")]
		string PreviewText { get; }

		[Export ("rangeInPreviewText", ArgumentSemantic.Assign)]
		NSRange RangeInPreviewText { get; }

		[Export ("range", ArgumentSemantic.Assign)]
		NSRange Range { get; }

		[Export ("documentUID")]
		string DocumentUid { get; }

		[Export ("selection", ArgumentSemantic.Strong)]
		PSPDFTextBlock Selection { get; }

		[Export ("document", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("annotation", ArgumentSemantic.Weak), NullAllowed]
		PSPDFAnnotation Annotation { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFSearchHighlightView : PSPDFAnnotationViewProtocol {

		[Export ("popupAnimation")]
		void PopupAnimation ();

		[Export ("searchResult", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSearchResult SearchResult { get; set; }

		[Export ("selectionBackgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionBackgroundColor { get; set; }

		[Export ("cornerRadiusProportion", ArgumentSemantic.Assign)]
		nfloat CornerRadiusProportion { get; set; }
	}

	interface IPSPDFInlineSearchManagerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFInlineSearchManagerDelegate : PSPDFTextSearchDelegate, PSPDFOverridable {

		[Export ("inlineSearchManager:didFocusSearchResult:")]
		void DidFocusSearchResult (PSPDFInlineSearchManager manager, PSPDFSearchResult searchResult);

		[Export ("inlineSearchManagerDidClearAllSearchResults:")]
		void DidClearAllSearchResults (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchWillAppear:")]
		void SearchWillAppear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchDidAppear:")]
		void SearchDidAppear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchWillDisappear:")]
		void SearchWillDisappear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerSearchDidDisappear:")]
		void SearchDidDisappear (PSPDFInlineSearchManager manager);

		[Export ("inlineSearchManagerContainerView:")]
		[Abstract]
		UIView GetContainerView (PSPDFInlineSearchManager manager);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFInlineSearchManager {

		[Export ("initWithPresentationContext:")]
		IntPtr Constructor (IPSPDFPresentationContext presentationContext);

		[Export ("presentInlineSearchWithSearchText:animated:")]
		void PresentInlineSearch ([NullAllowed] string text, bool animated);

		[Export ("hideInlineSearchAnimated:")]
		bool HideInlineSearch (bool animated);

		[Export ("hideKeyboard")]
		void HideKeyboard ();

		[Export ("searchVisible", ArgumentSemantic.Assign)]
		bool SearchVisible { [Bind ("isSearchVisible")] get; }

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext PresentationContext { get; }

		[Export ("textSearch", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSearch TextSearch { get; }

		[Export ("searchText")]
		string SearchText { get; }

		[Export ("searchResults", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("searchStatus", ArgumentSemantic.Assign)]
		PSPDFSearchStatus SearchStatus { get; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFInlineSearchManagerDelegate Delegate { get; set; }

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("beingPresented", ArgumentSemantic.Assign)]
		bool BeingPresented { [Bind ("isBeingPresented")] get; }

		[Export ("beingDismissed", ArgumentSemantic.Assign)]
		bool BeingDismissed { [Bind ("isBeingDismissed")] get; }

		[Export ("searchResultsLabelDistance", ArgumentSemantic.Assign)]
		nfloat SearchResultsLabelDistance { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibrary {

		[Notification]
		[Field ("PSPDFLibraryWillStartIndexingDocumentNotification", "__Internal")]
		NSString WillStartIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidFinishIndexingDocumentNotification", "__Internal")]
		NSString DidFinishIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidRemoveDocumentNotification", "__Internal")]
		NSString DidRemoveDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidClearIndexesNotification", "__Internal")]
		NSString DidClearIndexesNotification { get; }

		[Field ("PSPDFLibraryNotificationUIDKey", "__Internal")]
		NSString UidKey { get; }

		[Field ("PSPDFLibraryNotificationSuccessKey", "__Internal")]
		NSString SuccessKey { get; }

		[Field ("PSPDFLibraryInvalidOperationException", "__Internal")]
		NSString InvalidOperationExceptionName { get; }

		[Static]
		[Export ("libraryWithPath:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, out NSError error);

		[Static]
		[Export ("libraryWithPath:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, [NullAllowed] string tokenizer, out NSError error);

		[Static]
		[Export ("libraryWithPath:ftsVersion:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, PSPDFLibraryFtsVersion ftsVersion, [NullAllowed] string tokenizer, out NSError error);

		[Export ("defaultLibraryPath")]
		string DefaultLibraryPath { get; }

		[Export ("path")]
		string Path { get; }

		[Export ("spotlightIndexingType")]
		PSPDFLibrarySpotlightIndexingType SpotlightIndexingType { get; }

		[Export ("tokenizer"), NullAllowed]
		string Tokenizer { get; }

		[Export ("saveReversedPageText", ArgumentSemantic.Assign)]
		bool SaveReversedPageText { get; set; }

		[Export ("suspended", ArgumentSemantic.Assign)]
		bool Suspended { get; set; }

		[Field ("PSPDFLibraryMaximumSearchResultsTotalKey", "__Internal")]
		NSString MaximumSearchResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumSearchResultsPerDocumentKey", "__Internal")]
		NSString MaximumSearchResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryMaximumPreviewResultsTotalKey", "__Internal")]
		NSString MaximumPreviewResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumPreviewResultsPerDocumentKey", "__Internal")]
		NSString MaximumPreviewResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryMatchExactWordsOnlyKey", "__Internal")]
		NSString MatchExactWordsOnlyKey { get; }

		[Field ("PSPDFLibraryPreviewRangeKey", "__Internal")]
		NSString PreviewRangeKey { get; }

		[Export ("documentUIDsMatchingString:options:completionHandler:")]
		void DocumentUidsMatchingString (string searchString, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<string, NSDictionary<NSString, NSIndexSet>> completionHandler);

		[Export ("documentUIDsMatchingString:options:completionHandler:previewTextHandler:")]
		void DocumentUidsMatchingString (string searchString, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<NSString, NSDictionary<NSString, NSIndexSet>> completionHandler, [NullAllowed] Action<NSString, NSDictionary<NSString, NSSet<PSPDFSearchResult>>> previewTextHandler);

		[Export ("indexStatusForUID:withProgress:")]
		PSPDFLibraryIndexStatus IndexStatus (string uid, out nfloat outProgress);

		[Export ("indexing", ArgumentSemantic.Assign)]
		bool Indexing { [Bind ("isIndexing")] get; }

		[Export ("queuedUIDs")]
		NSOrderedSet<NSString> QueuedUids { get; }

		[Export ("indexedUIDs"), NullAllowed]
		NSOrderedSet<NSString> IndexedUids { get; }

		[Export ("indexedUIDCount", ArgumentSemantic.Assign)]
		nint IndexedUidCount { get; }

		[Export ("indexedDocumentWithUID:")]
		[return: NullAllowed]
		PSPDFDocument GetIndexedDocument (string uid);

		[Export ("metadataForUID:")]
		[return: NullAllowed]
		NSDictionary MetadataFor (string uid);

		[Export ("dataSource", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFLibraryDataSource DataSource { get; }

		[Export ("updateIndex")]
		void UpdateIndex ();

		[Export ("removeIndexForUID:")]
		void RemoveIndex (string uid);

		[Export ("clearAllIndexes")]
		void ClearAllIndexes ();

		[Async]
		[Export ("fetchSpotlightIndexedDocumentForUserActivity:completionHandler:")]
		void FetchSpotlightIndexedDocument (NSUserActivity userActivity, Action<PSPDFDocument> completionHandler);

		[Export ("cancelAllPreviewTextOperations")]
		void CancelAllPreviewTextOperations ();

		// PSPDFLibrary (EncryptionSupport)

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:error:")]
		PSPDFLibrary GetEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:tokenizer:error:")]
		PSPDFLibrary GetEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, [NullAllowed] string tokenizer, out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:ftsVersion:tokenizer:error:")]
		PSPDFLibrary GetEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, PSPDFLibraryFtsVersion ftsVersion, [NullAllowed] string tokenizer, out NSError error);

		[Export ("encrypted", ArgumentSemantic.Assign)]
		bool Encrypted { [Bind ("isEncrypted")] get; }
	}

	interface IPSPDFLibraryDataSource { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryDataSource {

		[Export ("libraryWillBeginIndexing:")]
		void WillBeginIndexing (PSPDFLibrary library);

		[Abstract]
		[Export ("uidsOfDocumentsToBeIndexedByLibrary:")]
		string [] GetUidsOfDocumentsToBeIndexed (PSPDFLibrary library);

		[Abstract]
		[Export ("uidsOfDocumentsToBeRemovedFromLibrary:")]
		string [] GetUidsOfDocumentsToBeRemoved (PSPDFLibrary library);

		[Abstract]
		[return: NullAllowed]
		[Export ("library:documentWithUID:")]
		PSPDFDocument Getdocument (PSPDFLibrary library, string uid);
	}

	interface IPSPDFDocumentPickerControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentPickerControllerDelegate : PSPDFOverridable {

		[Export ("documentPickerController:didSelectDocument:pageIndex:searchString:")]
		[Abstract]
		void DidSelectDocument (PSPDFDocumentPickerController controller, PSPDFDocument document, nuint pageIndex, string searchString);

		[Export ("documentPickerControllerWillBeginSearch:")]
		void WillBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidBeginSearch:")]
		void DidBeginSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerWillEndSearch:")]
		void WillEndSearch (PSPDFDocumentPickerController controller);

		[Export ("documentPickerControllerDidEndSearch:")]
		void DidEndSearch (PSPDFDocumentPickerController controller);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFDocumentPickerController {

		[Static]
		[Export ("documentsFromDirectory:includeSubdirectories:")]
		PSPDFDocument [] DocumentsFromDirectory ([NullAllowed] string directoryName, bool includeSubdirectories);

		[Export ("initWithDirectory:includeSubdirectories:library:")]
		IntPtr Constructor ([NullAllowed] string directory, bool includeSubdirectories, [NullAllowed] PSPDFLibrary library);

		[Export ("initWithDocuments:library:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument [] documents, [NullAllowed] PSPDFLibrary library);

		[Export ("enqueueDocumentsIfRequired")]
		void EnqueueDocumentsIfRequired ();

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentPickerControllerDelegate Delegate { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; }

		[Export ("directory"), NullAllowed]
		string Directory { get; }

		[Export ("useDocumentTitles", ArgumentSemantic.Assign)]
		bool UseDocumentTitles { get; set; }

		[Export ("showSectionIndexes", ArgumentSemantic.Assign)]
		bool ShowSectionIndexes { get; set; }

		[Export ("alwaysShowDocuments", ArgumentSemantic.Assign)]
		bool AlwaysShowDocuments { get; set; }

		[Export ("fullTextSearchEnabled", ArgumentSemantic.Assign)]
		bool FullTextSearchEnabled { get; set; }

		[Export ("fullTextSearchExactWordMatch", ArgumentSemantic.Assign)]
		bool FullTextSearchExactWordMatch { get; set; }

		[Export ("isSearchingIndex", ArgumentSemantic.Assign)]
		bool IsSearchingIndex { get; }

		[Export ("showSearchPageResults", ArgumentSemantic.Assign)]
		bool ShowSearchPageResults { get; set; }

		[Export ("showSearchPreviewText", ArgumentSemantic.Assign)]
		bool ShowSearchPreviewText { get; set; }

		[Export ("maximumNumberOfSearchResultsDisplayed", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResultsDisplayed { get; set; }

		[Export ("maximumNumberOfSearchResultsPerDocument", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResultsPerDocument { get; set; }

		[Export ("numberOfSearchPreviewLines", ArgumentSemantic.Assign)]
		nuint NumberOfSearchPreviewLines { get; set; }

		[Export ("library", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLibrary Library { get; }

		// PSPDFDocumentPickerController (SubclassingHooks) Category

		[Export ("updateStatusCell:")]
		void UpdateStatusCell (PSPDFDocumentPickerIndexStatusCell cell);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFDocumentPickerCell {

		[Export ("configureWithDocument:useDocumentTitle:detailText:pageIndex:previewImage:")]
		void Configure (PSPDFDocument document, bool useDocumentTitle, [NullAllowed] NSAttributedString detailText, nuint pageIndex, UIImage previewImage);

		[Export ("rotatedPageRect", ArgumentSemantic.Assign)]
		CGRect RotatedPageRect { get; set; }

		[Export ("pagePreviewImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage PagePreviewImage { get; set; }

		[Export ("setPagePreviewImage:animated:")]
		void SetPagePreviewImage ([NullAllowed] UIImage pagePreviewImage, bool animated);

		[Export ("document", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }
	}

	[BaseType (typeof (PSPDFSpinnerCell))]
	interface PSPDFDocumentPickerIndexStatusCell {

	}

	[BaseType (typeof (UISegmentedControl))]
	interface PSPDFSegmentedControl {

		[Export ("hitTestEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets HitTestEdgeInsets { get; set; }
	}

	[Static]
	interface PSPDFThumbnailViewFilter {
		
		[Field ("PSPDFThumbnailViewFilterShowAll", "__Internal")]
		NSString ShowAll { get; }

		[Field ("PSPDFThumbnailViewFilterBookmarks", "__Internal")]
		NSString Bookmarks { get; }

		[Field ("PSPDFThumbnailViewFilterAnnotations", "__Internal")]
		NSString Annotations { get; }
	}

	[BaseType (typeof (PSPDFSegmentedControl))]
	interface PSPDFThumbnailFilterSegmentedControl {

	}

	interface IPSPDFThumbnailViewControllerDelegate	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFThumbnailViewControllerDelegate {

		[Export ("thumbnailViewController:didSelectPageAtIndex:inDocument:")]
		void DidSelectPage (PSPDFThumbnailViewController thumbnailViewController, nuint pageIndex, PSPDFDocument document);
	}

	[BaseType (typeof (UICollectionViewController))]
	interface PSPDFThumbnailViewController {

		[Export ("initWithCollectionViewLayout:")]
		IntPtr Constructor ([NullAllowed] UICollectionViewLayout layout);

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext DataSource { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFThumbnailViewControllerDelegate Delegate { get; set; }

		[Export ("cellForPageAtIndex:document:")]
		[return: NullAllowed]
		UICollectionViewCell GetCell (nuint pageIndex, [NullAllowed] PSPDFDocument document);

		[Export ("scrollToPageAtIndex:document:animated:")]
		void ScrollToPage (nuint pageIndex, [NullAllowed] PSPDFDocument document, bool animated);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("updateFilterAndVisibleCellsAnimated:")]
		void UpdateFilterAndVisibleCells (bool animated);

		[Export ("updateInsetsForTopOverlapHeight:")]
		void UpdateInsetsForTopOverlapHeight (nfloat overlapHeight);

		[Export ("fixedItemSizeEnabled", ArgumentSemantic.Assign)]
		bool FixedItemSizeEnabled { get; set; }

		[Export ("filterOptions", ArgumentSemantic.Copy), NullAllowed]
		NSString [] FilterOptions { get; set; }

		[Export ("activeFilter", ArgumentSemantic.Assign)]
		NSString ActiveFilter { get; set; }

		[Export ("setActiveFilter:animated:")]
		void SetActiveFilter (NSString activeFilter, bool animated);

		[Export ("cellClass", ArgumentSemantic.Strong)]
		Class CellClass { get; set; }

		[Static]
		[Export ("automaticThumbnailSizeForPageWithSize:referencePageSize:containerSize:interitemSpacing:")]
		CGSize GetAutomaticThumbnailSize (CGSize pageSize, CGSize referencePageSize, CGSize containerSize, nfloat interitemSpacing);

		// PSPDFThumbnailViewController (SubclassingHooks) Category

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFThumbnailGridViewCell cell, NSIndexPath indexPath);

		[Export ("pageForIndexPath:")]
		nint PageForIndexPath (NSIndexPath indexPath);

		[Export ("filterSegment", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailFilterSegmentedControl FilterSegment { get; }

		[Export ("updateFilterSegment")]
		void UpdateFilterSegment ();

		[Export ("pagesForFilter:")]
		NSNumber [] PagesForFilter (NSString filter);

		[Export ("emptyContentLabelForFilter:")]
		string EmptyContentLabelForFilter (NSString filter);

		[Export ("updateEmptyView")]
		void UpdateEmptyView ();

		[Export ("collectionView:viewForSupplementaryElementOfKind:atIndexPath:")]
		[return: NullAllowed]
		UICollectionReusableView GetViewForSupplementaryElementOfKind (UICollectionView collectionView, string kind, NSIndexPath indexPath);
	}

	[BaseType (typeof (UILabel))]
	interface PSPDFRoundedLabel {

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nfloat CornerRadius { get; set; }

		[Export ("rectColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor RectColor { get; set; }
	}

	[BaseType (typeof (PSPDFPageCell))]
	interface PSPDFThumbnailGridViewCell {
		
		[Export ("document"), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("bookmarkImageColor"), NullAllowed]
		UIColor BookmarkImageColor { get; set; }

		// PSPDFThumbnailGridViewCell (SubclassingHooks) Category

		[Export ("bookmarkImageView"), NullAllowed]
		UIImageView BookmarkImageView { get; }

		[Export ("updatePageLabel")]
		void UpdatePageLabel ();

		[Export ("updateBookmarkImage")]
		void UpdateBookmarkImage ();
	}

	interface IPSPDFScrubberBarDelegate	{ }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFScrubberBarDelegate {

		[Export ("scrubberBar:didSelectPageAtIndex:")]
		[Abstract]
		void DidSelectPage (PSPDFScrubberBar scrubberBar, nuint pageIndex);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFScrubberBar {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFScrubberBarDelegate Delegate { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext DataSource { get; set; }

		[Export ("scrubberBarType", ArgumentSemantic.Assign)]
		PSPDFScrubberBarType ScrubberBarType { get; set; }

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("updateToolbarForced")]
		void UpdateToolbarForced ();

		[Export ("updatePageMarker")]
		void UpdatePageMarker ();

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }

		[Export ("allowTapsOutsidePageArea", ArgumentSemantic.Assign)]
		bool AllowTapsOutsidePageArea { get; set; }

		[Export ("barTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BarTintColor { get; set; }

		[Export ("translucent", ArgumentSemantic.Assign)]
		bool Translucent { [Bind ("isTranslucent")] get; set; }

		[Export ("leftBorderMargin", ArgumentSemantic.Assign)]
		nfloat LeftBorderMargin { get; set; }

		[Export ("rightBorderMargin", ArgumentSemantic.Assign)]
		nfloat RightBorderMargin { get; set; }

		[Export ("thumbnailBorderColor", ArgumentSemantic.Strong)][NullAllowed]
		UIColor ThumbnailBorderColor { get; set; }

		[Export ("toolbar", ArgumentSemantic.Strong)]
		UIToolbar Toolbar { get; }

		// PSPDFScrubberBar (SubclassingHooks) Category

		[Export ("smallToolbar")]
		bool SmallToolbar { [Bind ("isSmallToolbar")] get; }

		[Export ("scrubberBarHeight")]
		nfloat ScrubberBarHeight { get; }

		[Export ("scrubberBarThumbSize")]
		CGSize ScrubberBarThumbSize { get; }

		[Export ("emptyThumbnailImageView")]
		UIImageView EmptyThumbnailImageView { get; }

		[Export ("processTouch:")]
		bool ProcessTouch (UITouch touch);

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		nfloat ThumbnailMargin { get; set; }

		[Export ("pageMarkerSizeMultiplier", ArgumentSemantic.Assign)]
		nfloat PageMarkerSizeMultiplier { get; set; }
	}

	interface IPSPDFThumbnailBarDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFThumbnailBarDelegate {

		[Export ("thumbnailBar:didSelectPageAtIndex:")]
		void DidSelectPage (PSPDFThumbnailBar thumbnailBar, nuint pageIndex);
	}

	[BaseType (typeof (UICollectionView))]
	interface PSPDFThumbnailBar {

		[Export ("thumbnailBarDelegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFThumbnailBarDelegate ThumbnailBarDelegate { get; set; }

		[Export ("thumbnailBarDataSource", ArgumentSemantic.Weak)]
		IPSPDFPresentationContext ThumbnailBarDataSource { get; set; }

		[Export ("scrollToPageAtIndex:animated:")]
		void ScrollToPage (nuint pageIndex, bool animated);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("reloadDataAndKeepSelection")]
		void ReloadDataAndKeepSelection ();

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailBarHeight", ArgumentSemantic.Assign)]
		nfloat ThumbnailBarHeight { get; set; }

		[Export ("showPageLabels", ArgumentSemantic.Assign)]
		bool ShowPageLabels { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineParser {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("outlineElementForPageAtIndex:exactPageOnly:")]
		PSPDFOutlineElement OutlineElement (nuint pageIndex, bool exactPageOnly);

		[Export ("outline", ArgumentSemantic.Strong)]
		PSPDFOutlineElement Outline { get; set; }

		[Export ("outlineParsed", ArgumentSemantic.Assign)]
		bool OutlineParsed { [Bind ("isOutlineParsed")] get; }

		[Export ("outlineAvailable", ArgumentSemantic.Assign)]
		bool OutlineAvailable { [Bind ("isOutlineAvailable")] get; }

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocumentProvider DocumentProvider { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFOutlineElement {

		[Export ("initWithTitle:color:fontTraits:action:children:level:")]
		IntPtr Constructor ([NullAllowed] string title, [NullAllowed] UIColor color, UIFontDescriptorSymbolicTraits fontTraits, [NullAllowed] PSPDFAction action, [NullAllowed] PSPDFOutlineElement [] children, nuint level);

		[Export ("flattenedChildren")]
		PSPDFOutlineElement [] FlattenedChildren { get; }

		[Export ("allFlattenedChildren")]
		PSPDFOutlineElement [] AllFlattenedChildren { get; }

		[Export ("title"), NullAllowed]
		string Title { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("action"), NullAllowed]
		PSPDFAction Action { get; }

		[Export ("color", ArgumentSemantic.Strong), NullAllowed]
		UIColor Color { get; }

		[Export ("fontTraits", ArgumentSemantic.Assign)]
		UIFontDescriptorSymbolicTraits FontTraits { get; }

		[Export ("children", ArgumentSemantic.Copy), NullAllowed]
		PSPDFOutlineElement [] Children { get; }

		[Export ("level", ArgumentSemantic.Assign)]
		nuint Level { get; }

		[Export ("expanded", ArgumentSemantic.Assign)]
		bool Expanded { [Bind ("isExpanded")] get; set; }
	}

	interface IPSPDFOutlineViewControllerDelegate {	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineViewControllerDelegate : PSPDFOverridable	{

		[Export ("outlineController:didTapAtElement:")]
		[Abstract]
		bool DidTapAtElement (PSPDFOutlineViewController outlineController, PSPDFOutlineElement outlineElement);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFOutlineViewController : PSPDFStyleable {

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOutlineViewControllerDelegate Delegate { get; set; }

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("showPageLabels", ArgumentSemantic.Assign)]
		bool ShowPageLabels { get; set; }

		[Export ("maximumNumberOfLines", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfLines { get; set; }

		[Export ("outlineIntentLeftOffset", ArgumentSemantic.Assign)]
		nfloat OutlineIntentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier", ArgumentSemantic.Assign)]
		nfloat OutlineIndentMultiplier { get; set; }

		[Export ("document", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocument Document { get; set; }

		// PSPDFOutlineViewController (SubclassingHooks) Category

		[Export ("shouldExpandCollapseOnRowSelection")]
		bool ShouldExpandCollapseOnRowSelection ();

		[Export ("outlineCellDidTapDisclosureButton:")]
		void OutlineCellDidTapDisclosureButton (PSPDFOutlineCell cell);

		[Export ("searchController", ArgumentSemantic.Strong)]
		UISearchController SearchController { get; }
	}

	interface IPSPDFOutlineCellDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFOutlineCellDelegate {

		[Export ("outlineCellDidTapDisclosureButton:")]
		[Abstract]
		void DidTapDisclosureButton (PSPDFOutlineCell outlineCell);
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFOutlineCell {

		[Static]
		[Export ("heightForCellWithOutlineElement:documentProvider:constrainedToSize:outlineIntentLeftOffset:outlineIntentMultiplier:showPageLabel:")]
		nfloat HeightForCellWithOutlineElement (PSPDFOutlineElement outlineElement, [NullAllowed] PSPDFDocumentProvider documentProvider, CGSize constraintSize, nfloat leftOffset, nfloat multiplier, bool showPageLabel);

		[Export ("configureWithOutlineElement:documentProvider:")]
		void Configure (PSPDFOutlineElement outlineElement, [NullAllowed] PSPDFDocumentProvider documentProvider);

		[Export ("outlineElement", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineElement OutlineElement { get; }

		[Export ("pageLabelString"), NullAllowed]
		string PageLabelString { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOutlineCellDelegate Delegate { get; set; }

		[Export ("showExpandCollapseButton", ArgumentSemantic.Assign)]
		bool ShowExpandCollapseButton { get; set; }

		[Export ("showPageLabel", ArgumentSemantic.Assign)]
		bool ShowPageLabel { get; set; }

		// PSPDFOutlineCell (SubclassingHooks) Category

		[Export ("disclosureButton", ArgumentSemantic.Strong)]
		UIButton DisclosureButton { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Strong)]
		UILabel PageLabel { get; set; }

		[Static]
		[Export ("fontForOutlineElement:")]
		UIFont FontForOutlineElement ([NullAllowed] PSPDFOutlineElement outlineElement);

		[Export ("updateDisclosureButton")]
		void UpdateDisclosureButton ();

		[Export ("expandOrCollapse")]
		void ExpandOrCollapse ();

		[Export ("outlineIntentLeftOffset", ArgumentSemantic.Assign)]
		nfloat OutlineIntentLeftOffset { get; set; }

		[Export ("outlineIndentMultiplier", ArgumentSemantic.Assign)]
		nfloat OutlineIndentMultiplier { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFLabelParser {

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageLabelForPageAtIndex:")]
		[return: NullAllowed]
		string GetPageLabelForPage (nuint pageIndex);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		[Export ("labels", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, NSString> Labels { get; }
	}

	interface IPSPDFTabbedViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTabbedViewControllerDelegate : PSPDFMultiDocumentViewControllerDelegate {

		[Export ("tabbedPDFController:shouldChangeVisibleDocument:")]
		bool ShouldChangeVisibleDocument (PSPDFTabbedViewController tabbedPdfController, [NullAllowed] PSPDFDocument newVisibleDocument);

		[Export ("tabbedPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFTabbedViewController tabbedPdfController, [NullAllowed] PSPDFDocument oldVisibleDocument);

		[Export ("tabbedPDFController:shouldCloseDocument:")]
		bool ShouldCloseDocument (PSPDFTabbedViewController tabbedPdfController, PSPDFDocument document);

		[Export ("tabbedPDFController:didCloseDocument:")]
		void DidCloseDocument (PSPDFTabbedViewController tabbedPdfController, PSPDFDocument document);
	}

	[BaseType (typeof (UICollectionViewLayoutAttributes))]
	interface PSPDFThumbnailFlowLayoutAttributes {

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFThumbnailFlowLayoutAttributesType Type { get; set; }
	}

	[BaseType (typeof (UICollectionViewFlowLayout))]
	interface PSPDFThumbnailFlowLayout {

		[Export ("sectionInset", ArgumentSemantic.Assign)][New]
		UIEdgeInsets SectionInset { get; set; }

		[Export ("interitemSpacing")]
		nfloat InteritemSpacing { get; set; }

		[Export ("lineSpacing")]
		nfloat LineSpacing { get; set; }

		[Export ("singleLineMode", ArgumentSemantic.Assign)]
		bool SingleLineMode { get; set; }

		[Export ("incompleteLineAlignment", ArgumentSemantic.Assign)]
		PSPDFThumbnailFlowLayoutLineAlignment IncompleteLineAlignment { get; set; }

		[Export ("stickyHeaderEnabled", ArgumentSemantic.Assign)]
		bool StickyHeaderEnabled { get; set; }

		[Export ("doublePageModeDisabled", ArgumentSemantic.Assign)]
		bool DoublePageModeDisabled { get; set; }

		[Export ("doublePageMode", ArgumentSemantic.Assign)]
		bool DoublePageMode { get; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("typeForIndexPath:usingDoublePageMode:")]
		PSPDFThumbnailFlowLayoutAttributesType TypeForIndexPath (NSIndexPath indexPath, bool usingDoublePageMode);

		[Export ("indexPathForDoublePage:")]
		[return: NullAllowed]
		NSIndexPath IndexPathForDoublePage (NSIndexPath indexPath);
	}

	interface IPSPDFCollectionViewDelegateThumbnailFlowLayout { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFCollectionViewDelegateThumbnailFlowLayout {

		[Export ("collectionView:layout:itemSizeAtIndexPath:")]
		CGSize ItemSizeAtIndexPath (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath);

		[Export ("collectionView:layout:itemSizeAtIndexPath:completionHandler:")]
		CGSize GetItemSizeAtIndexPath (UICollectionView collectionView, UICollectionViewLayout layout, NSIndexPath indexPath, Action<CGSize> completionHandler);

		[Export ("collectionView:layout:referenceSizeForHeaderInSection:")]
		CGSize GetReferenceSizeForHeader (UICollectionView collectionView, UICollectionViewLayout layout, nint section);
	}

	[BaseType (typeof (PSPDFMultiDocumentViewController))]
	interface PSPDFTabbedViewController {
		
		[Export ("addDocument:makeVisible:animated:")]
		void AddDocument (PSPDFDocument document, bool shouldMakeDocumentVisible, bool animated);

		[Export ("insertDocumentAfterVisibleDocument:makeVisible:animated:")]
		void InsertDocument (PSPDFDocument document, bool shouldMakeDocumentVisible, bool animated);

		[Export ("insertDocument:atIndex:makeVisible:animated:")]
		void InsertDocument (PSPDFDocument document, nuint index, bool shouldMakeDocumentVisible, bool animated);

		[Export ("removeDocumentAtIndex:animated:")]
		void RemoveDocument (nuint index, bool animated);

		[Export ("removeDocument:animated:")]
		bool RemoveDocument (PSPDFDocument document, bool animated);

		[Export ("setVisibleDocument:scrollToPosition:animated:")]
		void SetVisibleDocument ([NullAllowed] PSPDFDocument visibleDocument, bool scrollToPosition, bool animated);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed][New]
		IPSPDFTabbedViewControllerDelegate Delegate { get; set; }

		[Export ("statePersistenceKey")][New]
		string StatePersistenceKey { get; set; }

		[Export ("tabbedBar")]
		PSPDFTabbedBar TabbedBar { get; }

		[Export ("documentPickerController", ArgumentSemantic.Assign), NullAllowed]
		PSPDFDocumentPickerController DocumentPickerController { get; set; }

		[Export ("barHidingMode", ArgumentSemantic.Assign)]
		PSPDFTabbedViewControllerBarHidingMode BarHidingMode { get; set; }

		[Export ("closeMode", ArgumentSemantic.Assign)]
		PSPDFTabbedViewControllerCloseMode CloseMode { get; set; }

		[Export ("openDocumentActionInNewTab", ArgumentSemantic.Assign)]
		bool OpenDocumentActionInNewTab { get; set; }

		[Export ("updateTabbedBarFrameAnimated:", ArgumentSemantic.Assign)]
		void UpdateTabbedBarFrame (bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationManager {

		[Notification]
		[Field ("PSPDFAnnotationsAddedNotification", "__Internal")]
		NSString AnnotationsAddedNotification { get; }

		[Notification]
		[Field ("PSPDFAnnotationsRemovedNotification", "__Internal")]
		NSString AnnotationsRemovedNotification { get; }

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("annotationProviders", ArgumentSemantic.Copy), NullAllowed]
		IPSPDFAnnotationProvider [] AnnotationProviders { get; set; }

		[Export ("fileAnnotationProvider", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFileAnnotationProvider FileAnnotationProvider { get; }

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray <PSPDFAnnotation>> GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotations (nuint pageIndex);

		[Export ("annotationViewClassForAnnotation:")]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, string [] keyPaths, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options, IntPtr error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations { get; }

		[Export ("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Export ("annotationsIncludingGroupsFromAnnotations:")]
		PSPDFAnnotation [] AnnotationsIncludingGroups (PSPDFAnnotation [] annotations);

		[Export ("annotationsForPageAtIndex:type:")]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotationsForPage (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("protocolStrings", ArgumentSemantic.Copy)]
		string [] ProtocolStrings { get; set; }

		[Static]
		[Export ("fileTypeTranslationTable")]
		NSDictionary<NSString, NSNumber> FileTypeTranslationTable ();

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFAnnotationManager (SubclassingHooks) Category

		[Export ("dirtyAnnotations"), NullAllowed]
		NSDictionary<NSNumber, NSArray <PSPDFAnnotation>> DirtyAnnotations { get; }

		[Static]
		[Export ("mediaFileTypes")]
		string [] MediaFileTypes ();

		[Export ("defaultAnnotationViewClassForAnnotation:")]
		[return: NullAllowed]
		Class DefaultAnnotationViewClass (PSPDFAnnotation annotation);
	}

	[Static] // Create a Smart Enum when ProcessEnums is available PSPDFAnnotationString
	interface PSPDFAnnotationString
	{
		[Field ("PSPDFAnnotationStringLink", "__Internal")]
		NSString Link { get; }

		[Field ("PSPDFAnnotationStringHighlight", "__Internal")]
		NSString Highlight { get; }

		[Field ("PSPDFAnnotationStringUnderline", "__Internal")]
		NSString Underline { get; }

		[Field ("PSPDFAnnotationStringStrikeOut", "__Internal")]
		NSString StrikeOut { get; }

		[Field ("PSPDFAnnotationStringSquiggly", "__Internal")]
		NSString Squiggly { get; }

		[Field ("PSPDFAnnotationStringNote", "__Internal")]
		NSString Note { get; }

		[Field ("PSPDFAnnotationStringFreeText", "__Internal")]
		NSString FreeText { get; }

		[Field ("PSPDFAnnotationStringInk", "__Internal")]
		NSString Ink { get; }

		[Field ("PSPDFAnnotationStringSquare", "__Internal")]
		NSString Square { get; }

		[Field ("PSPDFAnnotationStringCircle", "__Internal")]
		NSString Circle { get; }

		[Field ("PSPDFAnnotationStringLine", "__Internal")]
		NSString Line { get; }

		[Field ("PSPDFAnnotationStringPolygon", "__Internal")]
		NSString Polygon { get; }

		[Field ("PSPDFAnnotationStringPolyLine", "__Internal")]
		NSString PolyLine { get; }

		[Field ("PSPDFAnnotationStringSignature", "__Internal")]
		NSString Signature { get; }

		[Field ("PSPDFAnnotationStringStamp", "__Internal")]
		NSString Stamp { get; }

		[Field ("PSPDFAnnotationStringEraser", "__Internal")]
		NSString Eraser { get; }

		[Field ("PSPDFAnnotationStringSound", "__Internal")]
		NSString Sound { get; }

		[Field ("PSPDFAnnotationStringImage", "__Internal")]
		NSString Image { get; }

		[Field ("PSPDFAnnotationStringWidget", "__Internal")]
		NSString Widget { get; }

		[Field ("PSPDFAnnotationStringFile", "__Internal")]
		NSString File { get; }

		[Field ("PSPDFAnnotationStringRichMedia", "__Internal")]
		NSString RichMedia { get; }

		[Field ("PSPDFAnnotationStringScreen", "__Internal")]
		NSString Screen { get; }

		[Field ("PSPDFAnnotationStringCaret", "__Internal")]
		NSString Caret { get; }

		[Field ("PSPDFAnnotationStringPopup", "__Internal")]
		NSString Popup { get; }

		[Field ("PSPDFAnnotationStringWatermark", "__Internal")]
		NSString Watermark { get; }

		[Field ("PSPDFAnnotationStringTrapNet", "__Internal")]
		NSString TrapNet { get; }

		[Field ("PSPDFAnnotationString3D", "__Internal")]
		NSString _3D { get; }

		[Field ("PSPDFAnnotationStringRedact", "__Internal")]
		NSString Redact { get; }

		[Field ("PSPDFAnnotationDrawCenteredKey", "__Internal")]
		NSString DrawCenteredKey { get; }

		[Field ("PSPDFAnnotationMarginKey", "__Internal")]
		NSString MarginKey { get; }

		// PSPDFAnnotationStateManager Strings

		[Field ("PSPDFAnnotationStringSelectionTool", "__Internal")]
		NSString SelectionTool { get; }

		[Field ("PSPDFAnnotationStringSavedAnnotations", "__Internal")]
		NSString SavedAnnotations { get; }

		// PSPDFFlexibleAnnotationToolbar Strings

		[Field ("PSPDFAnnotationStringInkVariantPen", "__Internal")]
		NSString InkVariantPen { get; }

		[Field ("PSPDFAnnotationStringInkVariantHighlighter", "__Internal")]
		NSString InkVariantHighlighter { get; }

		[Field ("PSPDFAnnotationStringLineVariantArrow", "__Internal")]
		NSString LineVariantArrow { get; }

		[Field ("PSPDFAnnotationStringFreeTextVariantCallout", "__Internal")]
		NSString FreeTextVariantCallout { get; }
	}

	interface IPSPDFUndoProtocol { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFUndoProtocol {

		[Export ("keysForValuesToObserveForUndo")]
		[Abstract]
		NSSet<NSString> KeysForValuesToObserveForUndo ();

		[Static]
		[Export ("localizedUndoActionNameForKey:")]
		[return: NullAllowed]
		string LocalizedUndoActionNameForKey (string key);

		[Static]
		[Export ("undoCoalescingForKey:")]
		PSPDFUndoCoalescing UndoCoalescingForKey (string key);

		[Export ("insertUndoObjects:forKey:")]
		void InsertUndoObjects (NSSet objects, string key);

		[Export ("removeUndoObjects:forKey:")]
		void RemoveUndoObjects (NSSet objects, string key);

		[Export ("didUndoOrRedoChange:")]
		void DidUndoOrRedoChange (string key);

		[Export ("performUndoAction:")]
		void PerformUndoAction (NSObject action);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotation : PSPDFUndoProtocol, PSPDFJSONSerializing, INativeObject {

		[Field ("PSPDFAnnotationTriggerEventTransformerName", "__Internal")]
		NSString TriggerEventTransformerName { get; }

		[Field ("PSPDFVerticalAlignmentTransformerName", "__Internal")]
		NSString PSPDFVerticalAlignmentTransformerName { get; }

		[Static]
		[return: NullAllowed]
		[Export ("annotationFromJSONDictionary:documentProvider:error:")]
		PSPDFAnnotation FromJsonDictionary (NSDictionary<NSString, NSObject> jsonDictionary, [NullAllowed] PSPDFDocumentProvider documentProvider, out NSError error);

		[Static]
		[Export ("isWriteable")]
		bool IsWriteable { get; }

		[Static]
		[Export ("isDeletable")]
		bool IsDeletable { get; }

		[Static]
		[Export ("isFixedSize")]
		bool IsFixedSize { get; }

		[Static]
		[Export ("fixedSize")]
		CGSize FixedSize { get; }

		[Export ("wantsSelectionBorder", ArgumentSemantic.Assign)]
		bool WantsSelectionBorder { get; }

		[Export ("requiresPopupAnnotation", ArgumentSemantic.Assign)]
		bool RequiresPopupAnnotation { get; }

		[Export ("readOnly", ArgumentSemantic.Assign)]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("locked", ArgumentSemantic.Assign)]
		bool Locked { [Bind ("isLocked")] get; }

		[Export ("movable", ArgumentSemantic.Assign)]
		bool Movable { [Bind ("isMovable")] get; }

		[Export ("resizable", ArgumentSemantic.Assign)]
		bool Resizable { [Bind ("isResizable")] get; }

		[Export ("shouldMaintainAspectRatio", ArgumentSemantic.Assign)]
		bool ShouldMaintainAspectRatio { get; }

		[Export ("minimumSize", ArgumentSemantic.Assign)]
		CGSize MinimumSize { get; }

		[Export ("hitTest:minDiameter:")]
		bool HitTest (CGPoint point, nfloat minDiameter);

		[Export ("boundingBoxForPageRect:")]
		CGRect BoundingBoxForPageRect (CGRect pageRect);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFAnnotationType Type { get; }

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }

		[Export ("absolutePageIndex", ArgumentSemantic.Assign)]
		nuint AbsolutePageIndex { get; set; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("dirty", ArgumentSemantic.Assign)]
		bool Dirty { [Bind ("isDirty")] get; set; }

		[Export ("overlay", ArgumentSemantic.Assign)]
		bool Overlay { [Bind ("isOverlay")] get; set; }

		[Export ("editable", ArgumentSemantic.Assign)]
		bool Editable { [Bind ("isEditable")] get; set; }

		[Export ("deleted", ArgumentSemantic.Assign)]
		bool Deleted { [Bind ("isDeleted")] get; set; }

		[Export ("typeString")]
		string TypeString { get; set; }

		[Export ("alpha", ArgumentSemantic.Assign)]
		nfloat Alpha { get; set; }

		[Export ("color", ArgumentSemantic.Strong), NullAllowed]
		UIColor Color { get; set; }

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BorderColor { get; set; }

		[Export ("fillColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("contents"), NullAllowed]
		string Contents { get; set; }

		[Export ("subject"), NullAllowed]
		string Subject { get; set; }

		[Export ("additionalActions", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary AdditionalActions { get; set; }

		[Export ("value", ArgumentSemantic.Copy), NullAllowed]
		NSObject Value { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFAnnotationFlags Flags { get; set; }

		[Export ("hidden", ArgumentSemantic.Assign)]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[Export ("name"), NullAllowed]
		string Name { get; set; }

		[Export ("user"), NullAllowed]
		string User { get; set; }

		[Export ("group"), NullAllowed]
		string Group { get; set; }

		[Export ("creationDate", ArgumentSemantic.Strong), NullAllowed]
		NSDate CreationDate { get; set; }

		[Export ("lastModified", ArgumentSemantic.Strong), NullAllowed]
		NSDate LastModified { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		nfloat LineWidth { get; set; }

		[Export ("borderStyle", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderStyle BorderStyle { get; set; }

		[Export ("dashArray", ArgumentSemantic.Copy), NullAllowed]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity", ArgumentSemantic.Assign)]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("rotation", ArgumentSemantic.Assign)]
		nuint Rotation { get; set; }

		[Export ("rects", ArgumentSemantic.Copy), NullAllowed]
		NSValue [] Rects { get; set; }

		[Export ("points", ArgumentSemantic.Copy), NullAllowed]
		NSValue [] Points { get; set; }

		[Obsolete ("This property will go away in the near future. You should not rely on an annotation having a certain index on the page it belongs to.")]
		[Export ("indexOnPage")]
		nint IndexOnPage { get; }

		[Export ("objectNumber")]
		nint ObjectNumber { get; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[Export ("annotationIcon"), NullAllowed]
		UIImage AnnotationIcon { get; }

		[Export ("isEqualToAnnotation:")]
		bool IsEqualToAnnotation (PSPDFAnnotation otherAnnotation);

		// PSPDFAnnotation (AppearanceStream) Category

		[Export ("hasAppearanceStream")]
		bool HasAppearanceStream { get; }

		[Export ("extractImageFromAppearanceStreamWithTransform:error:")]
		UIImage ExtractImageFromAppearanceStream ([NullAllowed] CGAffineTransform transform, out NSError error);

		// PSPDFAnnotation (Drawing) Category

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("imageWithSize:withOptions:")]
		UIImage ImageWithSize (CGSize size, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("noteIconPoint")]
		CGPoint NoteIconPoint { get; }

		// PSPDFAnnotation (Advanced) Category

		[Export ("shouldUpdatePropertiesOnBoundsChange")]
		bool ShouldUpdatePropertiesOnBoundsChange { get; }

		[Export ("shouldUpdateOptionalPropertiesOnBoundsChange")]
		bool ShouldUpdateOptionalPropertiesOnBoundsChange { get; }

		[Export ("updatePropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateProperties (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("updateOptionalPropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateOptionalPropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("setBoundingBox:transform:includeOptional:")]
		void SetBoundingBox (CGRect boundingBox, bool transform, bool optionalProperties);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Export ("shouldDeleteAnnotation")]
		bool ShouldDeleteAnnotation { get; }

		// PSPDFAnnotation (Fonts) Category

		[Export ("fontAttributes", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> FontAttributes { get; set; }

		[Export ("fontName"), NullAllowed]
		string FontName { get; set; }

		[Export ("fontSize", ArgumentSemantic.Assign)]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("verticalTextAlignment", ArgumentSemantic.Assign)]
		PSPDFVerticalAlignment VerticalTextAlignment { get; set; }

		[Export ("defaultFontSize")]
		nfloat DefaultFontSize ();

		[Export ("defaultFontName")]
		string DefaultFontName ();

		[Export ("defaultFont")]
		UIFont DefaultFont ();

		[Export ("attributedString"), NullAllowed]
		NSAttributedString AttributedString { get; }

		[Export ("attributedStringWithContents:")]
		NSAttributedString GetAttributedString ([NullAllowed] string contents);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationSet { // NSFastEnumeration

		[Static]
		[return: NullAllowed]
		[Export ("unarchiveFromClipboard")]
		PSPDFAnnotationSet FromClipboard ();

		[Export ("initWithAnnotations:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAnnotation [] annotations);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();
	}

	interface IPSPDFAnnotationProvider { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationProvider {

		[Export ("annotationsForPageAtIndex:")]
		[Abstract]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotationsForPage (nuint pageIndex);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotations (nuint pageIndex);

		[Export ("annotationViewClassForAnnotation:")]
		Class AnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options, out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool GetShouldSaveAnnotations ();

		[Export ("dirtyAnnotations")]
		NSDictionary<NSNumber, NSArray <PSPDFAnnotation>> GetDirtyAnnotations ();

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, NSObject [] keyPaths, NSDictionary options);

		[Export ("providerDelegate")]
		IPSPDFAnnotationProviderChangeNotifier GetProviderDelegate ();

		[Export ("setProviderDelegate:")]
		void SetProviderDelegate ([NullAllowed] IPSPDFAnnotationProviderChangeNotifier provider);
	}

	interface IPSPDFAnnotationProviderChangeNotifier { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationProviderChangeNotifier {

		[Export ("updateAnnotations:animated:")]
		[Abstract]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Abstract]
		[Export ("parentDocumentProvider", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocumentProvider ParentDocumentProvider { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFContainerAnnotationProvider : PSPDFAnnotationProvider, PSPDFUndoProtocol {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("undoController", ArgumentSemantic.Weak)]
		PSPDFUndoController UndoController { get; }

		// PSPDFContainerAnnotationProvider (SubclassingHooks) Category

		[Export ("performBlockForReading:")]
		void PerformAnctionForReading (Action action);

		[Export ("performBlockForWriting:")]
		void PerformActionForWriting (Action action);

		[Export ("performBlockForWritingAndWait:")]
		void PerformActionForWritingAndWait (Action action);

		[Export ("setAnnotations:forPageAtIndex:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, nuint pageIndex, bool append);

		[Export ("setAnnotations:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, bool append);

		[Export ("removeAllAnnotationsWithOptions:")]
		void RemoveAllAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("allAnnotations")]
		PSPDFAnnotation [] AllAnnotations { get; }

		[Export ("annotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> Annotations { get; }

		[Export ("clearNeedsSaveFlag")]
		void ClearNeedsSaveFlag ();

		[Export ("setAnnotationCacheDirect:")]
		void SetAnnotationCacheDirect (NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> annotationCache);

		[Export ("registerAnnotationsForUndo:")]
		void RegisterAnnotationsForUndo (PSPDFAnnotation [] annotations);

		[Export ("annotationCache", ArgumentSemantic.Strong), NullAllowed]
		NSMutableDictionary<NSNumber, NSArray<PSPDFAnnotation>> AnnotationCache { get; }

		[Export ("willInsertAnnotations:")]
		void WillInsertAnnotations (PSPDFAnnotation [] annotations);
	}

	interface IPSPDFControllerStateHandling { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFControllerStateHandling {
		[Abstract]
		[Export ("document", ArgumentSemantic.Assign), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Abstract]
		[Export ("setControllerState:error:animated:")]
		void SetControllerState (PSPDFControllerState state, [NullAllowed] NSError error, bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFFileAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsForPageAtIndex:")][New]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotationsForPage (nuint pageIndex);

		[Export ("addAnnotations:options:")][New]
		[return: NullAllowed]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")][New]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("tryLoadAnnotationsFromFileWithError:"), Internal]
		bool TryLoadAnnotationsFromFile (IntPtr error);

		// PSPDFFileAnnotationProvider (Advanced) Category

		[Export ("saveableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SaveableTypes { get; set; }

		[Export ("parsableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType ParsableTypes { get; set; }

		[Export ("annotationsPath"), NullAllowed]
		string AnnotationsPath { get; set; }

		// PSPDFFileAnnotationProvider (SubclassingHooks) Category

		[Export ("parseAnnotationsForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFAnnotation [] ParseAnnotationsForPage (nuint pageIndex);

		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options, IntPtr error);

		[Export ("loadAnnotationsWithError:"), Internal]
		[return: NullAllowed]
		NSDictionary<NSNumber, NSArray <PSPDFAnnotation>> LoadAnnotations (IntPtr error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractTextOverlayAnnotation {

		[Static]
		[return: NullAllowed]
		[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		PSPDFAbstractTextOverlayAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph [] glyphs, nint pageRotation);

		[Export ("highlightedString")]
		string HighlightedString { get; }
	}


	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFHighlightAnnotation {

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFUnderlineAnnotation {

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFStrikeOutAnnotation {

	}

	[BaseType (typeof (PSPDFAbstractTextOverlayAnnotation))]
	interface PSPDFSquigglyAnnotation {

	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFreeTextAnnotation {

		[Field ("PSPDFFreeTextAnnotationIntentTransformerName", "__Internal")]
		NSString IntentTransformerName { get; }

		[Export ("initWithContents:")]
		IntPtr Constructor (string contents);

		[Export ("initWithContents:calloutPoint1:")]
		IntPtr Constructor (string contents, CGPoint point1);

		[Export ("intentType", ArgumentSemantic.Assign)]
		PSPDFFreeTextAnnotationIntent IntentType { get; set; }

		[Export ("point1", ArgumentSemantic.Assign)]
		CGPoint Point1 { get; set; }

		[Export ("kneePoint", ArgumentSemantic.Assign)]
		CGPoint KneePoint { get; set; }

		[Export ("point2", ArgumentSemantic.Assign)]
		CGPoint Point2 { get; set; }

		[Export ("lineEnd", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd { get; set; }

		[Export ("innerRectInset", ArgumentSemantic.Assign)]
		UIEdgeInsets InnerRectInset { get; set; }

		[Export ("sizeToFit")]
		void SizeToFit ();

		[Export ("sizeWithConstraints:")]
		CGSize SizeWithConstraints (CGSize constraints);

		[Export ("enableVerticalResizing", ArgumentSemantic.Assign)]
		bool EnableVerticalResizing { get; set; }

		[Export ("enableHorizontalResizing", ArgumentSemantic.Assign)]
		bool EnableHorizontalResizing { get; set; }

		[Export ("setBoundingBox:transformSize:")]
		void SetBoundingBox (CGRect boundingBox, bool transformSize);

		[Export ("textBoundingBox", ArgumentSemantic.Assign)]
		CGRect TextBoundingBox { get; set; }

		[Export ("setTextBoundingBoxSize:")]
		void SetTextBoundingBoxSize (CGSize size);

		[Export ("convertToIntentType:")]
		[return: NullAllowed]
		string [] ConvertTo (PSPDFFreeTextAnnotationIntent intentType);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFNoteAnnotation {

		[Export ("initWithContents:")]
		IntPtr Constructor (string contents);

		[Export ("iconName"), NullAllowed]
		string IconName { get; set; }

		// PSPDFNoteAnnotation (SubclassingHooks) Category

		[Export ("renderAnnotationIcon", ArgumentSemantic.Strong), NullAllowed]
		UIImage RenderAnnotationIcon { get; }

		[Export ("drawImageInContext:boundingBox:options:")]
		void DrawImageInContext (CGContext context, CGRect boundingBox, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("boundingBoxIfRenderedAsText")]
		CGRect BoundingBoxIfRenderedAsText { get; }
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFInkAnnotation {

		[Export ("initWithLines:")][Internal]
		IntPtr InitWithLines (NSArray lines);

		[Export ("lines", ArgumentSemantic.Copy), NullAllowed][Internal]
		NSArray _Lines { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Copy)]
		UIBezierPath BezierPath { get; }

		[Export ("empty")]
		bool Empty { [Bind ("isEmpty")] get; }

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("isSignature", ArgumentSemantic.Assign)]
		bool IsSignature { get; set; }

		[Export ("setBoundingBox:transformLines:")]
		void SetBoundingBox (CGRect boundingBox, bool transformLines);

		[Export ("copyLinesByApplyingTransform:")]
		NSArray CopyLinesByApplyingTransform (CGAffineTransform transform);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractShapeAnnotation {

		[Export ("pointSequences", ArgumentSemantic.Strong)]
		NSArray<NSValue> [] PointSequences { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFAbstractLineAnnotation {

		[Export ("initWithPoints:")]
		IntPtr Constructor (NSValue [] points);

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Copy), NullAllowed]
		UIBezierPath BezierPath { get; }

		[Export ("setBoundingBox:transformPoints:")]
		void SetBoundingBox (CGRect boundingBox, bool transformPoints);

		[Export ("recalculateBoundingBox")]
		void RecalculateBoundingBox ();
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFLineAnnotation {

		[Export ("initWithPoint1:point2:")]
		IntPtr Constructor (CGPoint point1, CGPoint point2);

		[Export ("point1", ArgumentSemantic.Assign)]
		CGPoint Point1 { get; set; }

		[Export ("point2", ArgumentSemantic.Assign)]
		CGPoint Point2 { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBrightnessManager {

		[Export ("wantsSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsSoftwareDimming { get; set; }

		[Export ("wantsAdditionalSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsAdditionalSoftwareDimming { get; set; }

		[Export ("additionalBrightnessDimmingFactor")]
		nfloat AdditionalBrightnessDimmingFactor { get; set; }

		[Export ("maximumAdditionalBrightnessDimmingFactor")]
		nfloat MaximumAdditionalBrightnessDimmingFactor { get; set; }

		[Export ("brightness")]
		nfloat Brightness { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFLinkAnnotation {

		[Export ("initWithLinkAnnotationType:")]
		IntPtr Constructor (PSPDFLinkAnnotationType linkAnnotationType);

		[Export ("initWithAction:")]
		IntPtr Constructor (PSPDFAction action);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("linkType", ArgumentSemantic.Assign)]
		PSPDFLinkAnnotationType LinkType { get; set; }

		[Export ("action", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("URL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl url { get; }

		[Export ("showAsLinkView", ArgumentSemantic.Assign)]
		bool ShowAsLinkView { get; }

		[Export ("multimediaExtension", ArgumentSemantic.Assign)]
		bool MultimediaExtension { [Bind ("isMultimediaExtension")] get; }

		[Export ("controlsEnabled", ArgumentSemantic.Assign)]
		bool ControlsEnabled { get; set; }

		[Export ("autoplayEnabled", ArgumentSemantic.Assign)]
		bool AutoplayEnabled { [Bind ("isAutoplayEnabled")] get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }

		[Export ("URLAction"), NullAllowed]
		PSPDFURLAction UrlAction { get; }

		[Export ("fullscreenEnabled", ArgumentSemantic.Assign)]
		bool FullscreenEnabled { [Bind ("isFullscreenEnabled")] get; set; }

		[Export ("targetString"), NullAllowed]
		string TargetString { get; }
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFSquareAnnotation {

		[Export ("bezierPath", ArgumentSemantic.Strong)]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFCircleAnnotation {

		[Export ("bezierPath", ArgumentSemantic.Strong), NullAllowed]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFStampAnnotation {

		[Static]
		[Export ("stampColorForSubject:")]
		UIColor StampColorForSubject ([NullAllowed] string subject);

		[Export ("initWithSubject:")]
		IntPtr Constructor ([NullAllowed] string subject);

		[Export ("initWithImage:")]
		IntPtr Constructor ([NullAllowed] UIImage image);

		[Export ("subtext"), NullAllowed]
		string Subtext { get; set; }

		[Export ("localizedSubject"), NullAllowed]
		string LocalizedSubject { get; set; }

		[Export ("image", ArgumentSemantic.Strong), NullAllowed]
		UIImage Image { get; set; }

		[Export ("loadImageWithTransform:error:"), Internal]
		UIImage LoadImage (out CGAffineTransform transform, IntPtr error);

		[Export ("imageTransform", ArgumentSemantic.Assign)]
		CGAffineTransform ImageTransform { get; set; }

		[Export ("sizeThatFits:")]
		CGSize SizeThatFits (CGSize size);

		[Export ("sizeToFit")]
		void SizeToFit ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCaretAnnotation {

	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFPopupAnnotation {

		[Export ("open", ArgumentSemantic.Assign)]
		bool Open { [Bind ("isOpen")] get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFWidgetAnnotation {

		[Export ("action", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed][New]
		UIColor BorderColor { get; set; }

		[Export ("widgetRotation", ArgumentSemantic.Assign)]
		nint WidgetRotation { get; set; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFScreenAnnotation {

		[Export ("mediaScreenWindowType", ArgumentSemantic.Assign)]
		PSPDFMediaScreenWindowType MediaScreenWindowType { get; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFRichMediaAnnotation {
		
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFileAnnotation {

		[Export ("appearanceName"), NullAllowed]
		string AppearanceName { get; set; }

		[Export ("embeddedFile", ArgumentSemantic.Strong), NullAllowed]
		PSPDFEmbeddedFile EmbeddedFile { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSoundAnnotation {

		[Static]
		[Export ("recordingAnnotationAvailable")]
		bool RecordingAnnotationAvailable { get; }

		[Export ("initWithURL:error:")]
		IntPtr Constructor (NSUrl soundUrl, out NSError error);

		[Export ("controller", ArgumentSemantic.Strong)]
		PSPDFSoundAnnotationController Controller { get; }

		[Export ("iconName"), NullAllowed]
		string IconName { get; set; }

		[Export ("canRecord", ArgumentSemantic.Assign)]
		bool CanRecord { get; }

		[Export ("soundURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl SoundUrl { get; }

		[Export ("bits", ArgumentSemantic.Assign)]
		nuint Bits { get; }

		[Export ("rate", ArgumentSemantic.Assign)]
		nuint Rate { get; }

		[Export ("channels", ArgumentSemantic.Assign)]
		nuint Channels { get; }

		[Export ("encoding"), NullAllowed]
		NSString /*PSPDFSoundAnnotationEncoding*/ Encoding { get; }

		[Export ("loadAttributesFromAudioFile:"), Internal]
		bool LoadAttributesFromAudioFile (IntPtr error);

		[Export ("soundData"), NullAllowed]
		NSData SoundData { get; }
	}

	[Static]
	interface PSPDFSoundAnnotationEncoding {
		
		[Field ("PSPDFSoundAnnotationEncodingRaw", "__Internal")]
		NSString Raw { get; }

		[Field ("PSPDFSoundAnnotationEncodingSigned", "__Internal")]
		NSString Signed { get; }

		[Field ("PSPDFSoundAnnotationEncodingMuLaw", "__Internal")]
		NSString MuLaw { get; }

		[Field ("PSPDFSoundAnnotationEncodingALaw", "__Internal")]
		NSString ALaw { get; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolygonAnnotation {

		[Field ("PSPDFPolygonAnnotationIntentTransformerName", "__Internal")]
		NSString IntentTransformerName { get; }

		[Export ("initWithPoints:intentType:")]
		IntPtr Constructor (NSValue [] points, PSPDFPolygonAnnotationIntent intentType);

		[Export ("intentType", ArgumentSemantic.Assign)]
		PSPDFPolygonAnnotationIntent IntentType { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolyLineAnnotation {

	}

	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFAssetAnnotation {

		[Export ("assetName"), NullAllowed]
		string AssetName { get; }

		[return: NullAllowed]
		[Export ("fileURLWithError:")]
		NSUrl GetFileUrl (out NSError error);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFIconFit {

		[Field ("PSPDFIconFitScaleModeTransformer", "__Internal")]
		NSString ScaleModeTransformer { get; }

		[Field ("PSPDFIconFitScaleTypeTransformer", "__Internal")]
		NSString ScaleTypeTransformer { get; }

		[Export ("scaleMode", ArgumentSemantic.Assign)]
		PSPDFIconFitScaleMode ScaleMode { get; set; }

		[Export ("scaleType", ArgumentSemantic.Assign)]
		PSPDFIconFitScaleType ScaleType { get; set; }

		[Export ("leftoverSpace", ArgumentSemantic.Copy), NullAllowed]
		NSNumber [] LeftoverSpace { get; set; }

		[Export ("scaleButtonAppearanceWithoutConsideringBorder", ArgumentSemantic.Assign)]
		bool ScaleButtonAppearanceWithoutConsideringBorder { get; set; }
	}

	interface IPSPDFIdentifiable { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFIdentifiable	{

		[Export ("uniqueIdentifier")]
		string UniqueIdentifier { get; set; }
	}

	interface IPSPDFAnnotationStateManagerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManagerDelegate {

		[Export ("annotationStateManager:shouldChangeState:to:variant:to:")] // PSPDFAnnotationString
		bool ShouldChangeState (PSPDFAnnotationStateManager manager, [NullAllowed] NSString currentState, [NullAllowed] NSString proposedState, [NullAllowed] NSString currentVariant, [NullAllowed] NSString proposedVariant);

		[Export ("annotationStateManager:didChangeState:to:variant:to:")] // PSPDFAnnotationString
		void DidChangeState (PSPDFAnnotationStateManager manager, [NullAllowed] NSString oldState, [NullAllowed] NSString newState, [NullAllowed] NSString oldVariant, [NullAllowed] NSString newVariant);

		[Export ("annotationStateManager:didChangeUndoState:redoState:")]
		void DidChangeUndoState (PSPDFAnnotationStateManager manager, bool undoEnabled, bool redoEnabled);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManager : PSPDFOverridable {

		[Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFAnnotationStateManagerDelegate aDelegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFAnnotationStateManagerDelegate aDelegate);

		[Export ("state"), NullAllowed] // PSPDFAnnotationString
		NSString State { get; set; }

		[Export ("toggleState:")] // PSPDFAnnotationString
		void ToggleState (NSString state);

		[Export ("variant"), NullAllowed] // PSPDFAnnotationString
		NSString Variant { get; set; }

		[Export ("setState:variant:")] // PSPDFAnnotationString
		void SetState ([NullAllowed] NSString state, [NullAllowed] NSString variant);

		[Export ("toggleState:variant:")]
		void ToggleState ([NullAllowed] NSString state, [NullAllowed] NSString variant);

		[Export ("stateVariantIdentifier")] // PSPDFAnnotationString
		NSString StateVariantIdentifier { get; }

		[Export ("drawingInputMode", ArgumentSemantic.Assign)]
		PSPDFDrawViewInputMode DrawingInputMode { get; set; }

		[Export ("stylusMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationStateManagerStylusMode StylusMode { get; set; }

		[Export ("drawColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor DrawColor { get; set; }

		[Export ("fillColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		nfloat LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("dashArray", ArgumentSemantic.Copy), NullAllowed]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity", ArgumentSemantic.Assign)]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("fontName"), NullAllowed]
		string FontName { get; set; }

		[Export ("fontSize", ArgumentSemantic.Assign)]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("allowedImageQualities", ArgumentSemantic.Assign)]
		PSPDFImageQuality AllowedImageQualities { get; set; }

		[Export ("toggleStylePicker:presentationOptions:")]
		[return: NullAllowed] // Strong options PSPDFPresentationActions.h
		PSPDFAnnotationStyleViewController ToggleStylePicker ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("toggleSignatureController:presentationOptions:")]
		[return: NullAllowed] // Strong options PSPDFPresentationActions.h
		UIViewController ToggleSignatureController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("toggleStampController:includeSavedAnnotations:presentationOptions:")] // Strong options PSPDFPresentationActions.h
		[return: NullAllowed]
		UIViewController ToggleStampController ([NullAllowed] NSObject sender, bool includeSavedAnnotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("toggleImagePickerController:presentationOptions:")] // Strong options PSPDFPresentationActions.h
		[return: NullAllowed]
		UIViewController ToggleImagePickerController ([NullAllowed] NSObject sender, [NullAllowed] NSDictionary<NSString, NSObject> options);

		// PSPDFAnnotationStateManager (StateHelper) Category

		[Export ("isDrawingState:")] // PSPDFAnnotationString
		bool IsDrawingState ([NullAllowed] NSString state);

		[Export ("isHighlightAnnotationState:")] // PSPDFAnnotationString
		bool IsHighlightAnnotationState ([NullAllowed] NSString state);

		[Export ("stateShowsStylePicker:")] // PSPDFAnnotationString
		bool StateShowsStylePicker ([NullAllowed] NSString state);

		// PSPDFAnnotationStateManager (SubclassingHooks) Category

		[Export ("cancelDrawingAnimated:")]
		void CancelDrawing (bool animated);

		[Export ("doneDrawingAnimated:")]
		void DoneDrawing (bool animated);

		[Export ("setLastUsedColor:annotationString:")] // PSPDFAnnotationString
		void SetLastUsedColor ([NullAllowed] UIColor lastUsedDrawColor, NSString annotationString);

		[Export ("lastUsedColorForAnnotationString:")] // PSPDFAnnotationString
		UIColor LastUsedColor (NSString annotationString);

		[Export ("drawViews", ArgumentSemantic.Strong), NullAllowed]
		NSDictionary<NSNumber, PSPDFDrawView> DrawViews { get; }
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNonAnimatingTableViewCell {

	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNeverAnimatingTableViewCell {

	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFAnnotationCell {

		[Static]
		[Export ("heightForAnnotation:inTableView:")]
		nfloat HeightForAnnotation (PSPDFAnnotation annotation, UITableView tableView);

		[Export ("nameLabel", ArgumentSemantic.Strong)]
		UILabel NameLabel { get; }

		[Export ("dateAndUserLabel", ArgumentSemantic.Strong)]
		UILabel DateAndUserLabel { get; }

		[Export ("annotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		// PSPDFAnnotationCell (SubclassingHooks) Category

		[Static]
		[Export ("dateAndUserStringForAnnotation:")]
		string DateAndUserString (PSPDFAnnotation annotation);
	}

	interface IPSPDFSignatureStore {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureStore {

		[Export ("initWithStoreName:")]
		IntPtr Constructor (string storeName);

		[Abstract]
		[Export ("addSignature:")]
		void AddSignature (PSPDFInkAnnotation signature);

		[Abstract]
		[Export ("removeSignature:")]
		bool RemoveSignature (PSPDFInkAnnotation signature);

		[Abstract]
		[Export ("signatures", ArgumentSemantic.Copy)]
		PSPDFInkAnnotation [] Signatures { get; set; }

		[Abstract]
		[Export ("storeName")]
		string StoreName { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFKeychainSignatureStore : PSPDFSignatureStore {

		[Field ("PSPDFKeychainSignatureStoreDefaultStoreName", "__Internal")]
		NSString DefaultStoreName { get; }
	}

	[BaseType (typeof (UICollectionViewCell))]
	interface PSPDFSelectableCollectionViewCell {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("selectableCellStyle", ArgumentSemantic.Assign)]
		PSPDFSelectableCollectionViewCellStyle SelectableCellStyle { get; set; }

		[Export ("selectableCellColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectableCellColor { get; set; }
	}


	[BaseType (typeof (PSPDFSelectableCollectionViewCell))]
	interface PSPDFAnnotationSetCell {

		[Export ("annotationSet", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationSet AnnotationSet { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }
	}

	interface IPSPDFAnnotationViewProtocol { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationViewProtocol {

		[Export ("annotation")]
		PSPDFAnnotation GetAnnotation ();

		[Export ("setAnnotation:")]
		void SetAnnotation ([NullAllowed] PSPDFAnnotation annotation);

		[Export ("zIndex")]
		nuint GetZIndex ();

		[Export ("setZIndex:")]
		void SetZIndex (nuint index);

		[Export ("zoomScale")]
		nfloat GetZoomScale ();

		[Export ("setZoomScale:")]
		void SetZoomScale (nfloat zoomScale);

		[Export ("PDFScale")]
		nfloat GetPdfScale ();

		[Export ("setPDFScale:")]
		void SetPdfScale (nfloat pdfScale);

		[Export ("PageView")]
		PSPDFPageView GetPageView ();

		[Export ("setPageView:")]
		void SetPageView ([NullAllowed] PSPDFPageView PageView);

		[Export ("configuration")]
		PSPDFConfiguration GetConfiguration ();

		[Export ("setConfiguration:")]
		void SetConfiguration (PSPDFConfiguration configuration);

		[Export ("isSelected")]
		bool GetSelected ();

		[Export ("setSelected:")]
		void SetSelected (bool selected);

		[Export ("didShowPageView:")]
		void DidShowPageView (PSPDFPageView pageView);

		[Export ("didHidePageView:")]
		void DidHidePageView (PSPDFPageView pageView);

		[Export ("didChangePageBounds:")]
		void DidChangePageBounds (CGRect bounds);

		[Export ("didTapAtPoint:")]
		void DidTapAtPoint (CGPoint point);

		[Export ("shouldSyncRemovalFromSuperview")]
		bool GetShouldSyncRemovalFromSuperview ();

		[Export ("willRemoveFromSuperview")]
		void WillRemoveFromSuperview ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLinkAnnotationBaseView : PSPDFAnnotationViewProtocol {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("linkAnnotation", ArgumentSemantic.Strong)]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("contentView", ArgumentSemantic.Strong)]
		UIView ContentView { get; }

		// PSPDFLinkAnnotationBaseView (SubclassingHooks)

		[Advice ("You must call base if overriden")]
		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("populateContentView")]
		void PopulateContentView ();

		[Export ("setContentViewVisible:animated:")]
		void SetContentViewVisible (bool visible, bool animated);

		[Export ("contentViewVisible")]
		bool ContentViewVisible { [Bind ("isContentViewVisible")] get; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFLinkAnnotationView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BorderColor { get; set; }

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nfloat CornerRadius { get; set; }

		[Export ("strokeWidth", ArgumentSemantic.Assign)]
		nfloat StrokeWidth { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFNoteAnnotationView {

		[Export ("initWithAnnotation:")]
		IntPtr Constructor (PSPDFAnnotation noteAnnotation);

		[Export ("annotationImageView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView AnnotationImageView { get; set; }

		// PSPDFNoteAnnotationView (SubclassingHooks) Category

		[Export ("renderNoteImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage RenderNoteImage { get; }

		[Export ("updateImageAnimated:")]
		void UpdateImage (bool animated);
	}

	interface IPSPDFResizableTrackedViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFResizableTrackedViewDelegate {

		[Export ("resizableView")]
		PSPDFResizableView GetResizableView ();

		[Export ("setResizableView:")]
		void SetResizableView (PSPDFResizableView view);

		[Export ("annotation")]
		PSPDFAnnotation GetAnnotation ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFResizableView {
		[Field ("PSPDFGuideSnapAllowanceAlways", "__Internal")]
		nfloat GuideSnapAllowanceAlways { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFResizableViewDelegate Delegate { get; set; }

		[Export ("mode", ArgumentSemantic.Assign)]
		PSPDFResizableViewMode Mode { get; set; }

		[Export ("trackedViews", ArgumentSemantic.Copy), NullAllowed]
		NSSet<UIView> TrackedViews { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }

		[Export ("innerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets InnerEdgeInsets { get; set; }

		[Export ("outerEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets OuterEdgeInsets { get; set; }

		[Export ("allowEditing", ArgumentSemantic.Assign)]
		bool AllowEditing { get; set; }

		[Export ("allowResizing", ArgumentSemantic.Assign)]
		bool AllowResizing { get; set; }

		[Export ("allowAdjusting", ArgumentSemantic.Assign)]
		bool AllowAdjusting { get; set; }

		[Export ("enableResizingGuides", ArgumentSemantic.Assign)]
		bool EnableResizingGuides { get; set; }

		[Export ("showBoundingBox", ArgumentSemantic.Assign)]
		bool ShowBoundingBox { get; set; }

		[Export ("guideSnapAllowance", ArgumentSemantic.Assign)]
		nfloat GuideSnapAllowance { get; set; }

		[Export ("minWidth", ArgumentSemantic.Assign)]
		nfloat MinWidth { get; set; }

		[Export ("minHeight", ArgumentSemantic.Assign)]
		nfloat MinHeight { get; set; }

		[Export ("selectionBorderWidth", ArgumentSemantic.Assign)]
		nfloat SelectionBorderWidth { get; set; }

		[Export ("guideBorderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor GuideBorderColor { get; set; }

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nuint CornerRadius { get; set; }

		// PSPDFResizableView (SubclassingHooks)

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("outerKnobOfType:")]
		IPSPDFKnobView OuterKnobOfType (PSPDFResizableViewOuterKnob knobType);

		[Export ("centerPointForOuterKnob:inFrame:")]
		CGPoint CenterPointForOuterKnob (PSPDFResizableViewOuterKnob knobType, CGRect frame);

		[Export ("newKnobViewForType:")]
		IPSPDFKnobView NewKnobViewForType (PSPDFKnobType type);

		[Export ("trackedAnnotations")]
		NSSet<PSPDFAnnotation> TrackedAnnotations { get; }

		[Export ("updateKnobsAnimated:")]
		void UpdateKnobs (bool animated);

		[Advice ("Requires base call if override")]
		[Export ("configureGuideLayer:withZoomScale:")]
		void ConfigureGuideLayer (CAShapeLayer layer, nfloat zoomScale);
	}

	interface IPSPDFResizableViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFResizableViewDelegate {

		[Export ("resizableViewDidBeginEditing:")]
		void ResizableViewDidBeginEditing (PSPDFResizableView resizableView);

		[Export ("resizableViewChangedFrame:outerKnobType:isInitialChange:")]
		void OuterKnobType (PSPDFResizableView resizableView, PSPDFResizableViewOuterKnob outerKnobType, bool isInitialChange);

		[Export ("resizableView:adjustedProperty:ofAnnotation:")]
		void GetAdjustedPropertyOfAnnotation (PSPDFResizableView resizableView, string propertyName, PSPDFAnnotation annotation);

		[Export ("resizableViewDidEndEditing:didChangeFrame:")]
		void ResizableViewDidEndEditing (PSPDFResizableView resizableView, bool didChangeFrame);
	}

	interface IPSPDFKnobView { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFKnobView {

		[Abstract]
		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFKnobType Type { get; set; }

		[Abstract]
		[Export ("knobSize", ArgumentSemantic.Assign)]
		CGSize KnobSize { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFAnnotationView : PSPDFAnnotationViewProtocol {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// Use (Get/Set)Annotation from AnnotationViewProtocol
		//		[Export ("annotation", ArgumentSemantic.Strong), NullAllowed]
		//		PSPDFAnnotation Annotation { get; set; }

		// PSPDFAnnotationView (SubclassingHooks)

		[Export ("annotationChangedNotification:")] // Calling base here is required
		void AnnotationChangedNotification (NSNotification notification);

		[Export ("shouldAnimatedAnnotationChanges", ArgumentSemantic.Assign)]
		bool ShouldAnimatedAnnotationChanges { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFFreeTextAnnotationView : IUITextViewDelegate {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		[Export ("textView", ArgumentSemantic.Strong), NullAllowed]
		UITextView TextView { get; }

		[Export ("resizableView", ArgumentSemantic.Weak), NullAllowed]
		PSPDFResizableView ResizableView { get; set; }

		// PSPDFFreeTextAnnotationView (SubclassingHooks) Category

		[Export ("textViewForEditing", ArgumentSemantic.Strong)]
		UITextView TextViewForEditing { get; }
	}

	interface IPSPDFFreeTextAccessoryViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFreeTextAccessoryViewDelegate {

		[Export ("doneButtonPressedOnFreeTextAccessoryView:")]
		void DoneButtonPressed (PSPDFFreeTextAccessoryView inputView);

		[Export ("clearButtonPressedOnFreeTextAccessoryView:")]
		void ClearButtonPressed (PSPDFFreeTextAccessoryView inputView);

		[return: NullAllowed]
		[Export ("freeTextAccessoryViewDidRequestInspector:")]
		PSPDFAnnotationStyleViewController DidRequestInspector (PSPDFFreeTextAccessoryView inputView);

		[Export ("freeTextAccessoryView:shouldChangeProperty:")]
		bool ShouldChangeProperty (PSPDFFreeTextAccessoryView styleController, string propertyName);

		[Export ("freeTextAccessoryView:didChangeProperty:")]
		void DidChangeProperty (PSPDFFreeTextAccessoryView styleController, string propertyName);
	}

	[BaseType (typeof (PSPDFToolbar))]
	interface PSPDFFreeTextAccessoryView : PSPDFFontPickerViewControllerDelegate, PSPDFAnnotationStyleViewControllerDelegate {

		[Notification]
		[Field ("PSPDFFreeTextAccessoryViewDidPressClearButtonNotification", "__Internal")]
		NSString DidPressClearButtonNotification { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFreeTextAccessoryViewDelegate Delegate { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("annotation", ArgumentSemantic.Retain)]
		PSPDFFreeTextAnnotation Annotation { get; set; }

		[Export ("propertiesForAnnotations", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> PropertiesForAnnotations { get; set; }

		[Export ("borderVisible", ArgumentSemantic.Weak)]
		bool BorderVisible { [Bind ("isBorderVisible")] get; set; }

		[Export ("separatorColor", ArgumentSemantic.Retain)]
		UIColor SeparatorColor { get; set; }

		// PSPDFFreeTextAccessoryView (SubclassingHooks)

		[Export ("buttonsForWidth:")]
		PSPDFToolbarButton [] ButtonsForWidth (nfloat width);

		[Export ("dismissPresentedViewControllersAnimated:")] // requires base call
		void DismissPresentedViewControllers (bool animated);

		[Export ("fontNameButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton FontNameButton { get; }

		[Export ("fontSizeButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton FontSizeButton { get; }

		[Export ("increaseFontSizeButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton IncreaseFontSizeButton { get; }

		[Export ("decreaseFontSizeButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton DecreaseFontSizeButton { get; }

		[Export ("leftAlignButton", ArgumentSemantic.Retain)]
		PSPDFToolbarSelectableButton LeftAlignButton { get; }

		[Export ("centerAlignButton", ArgumentSemantic.Retain)]
		PSPDFToolbarSelectableButton CenterAlignButton { get; }

		[Export ("rightAlignButton", ArgumentSemantic.Retain)]
		PSPDFToolbarSelectableButton RightAlignButton { get; }

		[Export ("colorButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton ColorButton { get; }

		[Export ("clearButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton ClearButton { get; }

		[Export ("doneButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton DoneButton { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFAnnotationStyleViewController : PSPDFFontPickerViewControllerDelegate, PSPDFStyleable {

		[Field ("PSPDFConvertFreeTextAnnotationCalloutActionKey", "__Internal")]
		NSString ConvertFreeTextAnnotationCalloutActionKey { get; }

		[Export ("initWithAnnotations:")]
		IntPtr Constructor ([NullAllowed] PSPDFAnnotation [] annotations);

		[Export ("annotations", ArgumentSemantic.Copy), NullAllowed]
		PSPDFAnnotation [] Annotations { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFAnnotationStyleViewControllerDelegate Delegate { get; }

		[Export ("showPreviewArea", ArgumentSemantic.Assign)]
		bool ShowPreviewArea { get; set; }

		[Static, NullAllowed]
		[Export ("propertiesForAnnotations")]
		NSDictionary<NSString, NSObject> PropertiesForAnnotations { get; set; }

		[Export ("typesShowingColorPresets", ArgumentSemantic.Assign)]
		PSPDFAnnotationType TypesShowingColorPresets { get; set; }

		[Export ("persistsColorPresetChanges", ArgumentSemantic.Assign)]
		PSPDFAnnotationType PersistsColorPresetChanges { get; set; }

		// PSPDFAnnotationStyleViewController (SubclassingHooks)

		[Export ("propertiesForAnnotations:")]
		NSArray<NSArray<NSString>> [] GetPropertiesForAnnotations (PSPDFAnnotation [] annotations);
	}

	interface IPSPDFAnnotationStyleViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStyleViewControllerDelegate : PSPDFOverridable {

		[Export ("annotationStyleController:didChangeProperties:")][Abstract]
		void DidChangeProperties (PSPDFAnnotationStyleViewController styleController, NSString [] propertyNames);

		[Export ("annotationStyleController:willStartChangingProperty:")]
		void WillStartChangingProperty (PSPDFAnnotationStyleViewController styleController, NSString propertyName);

		[Export ("annotationStyleController:didEndChangingProperty:")]
		void DidEndChangingProperty (PSPDFAnnotationStyleViewController styleController, NSString propertyName);

		[Export ("annotationStyleController:annotationViewForAnnotation:")]
		[return: NullAllowed]
		IPSPDFAnnotationViewProtocol AnnotationViewForAnnotation (PSPDFAnnotationStyleViewController styleController, PSPDFAnnotation annotation);
	}

	interface IPSPDFSelectionViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSelectionViewDelegate {

		[Export ("selectionView:shouldStartSelectionAtPoint:")]
		bool ShouldStartSelectionAtPoint (PSPDFSelectionView selectionView, CGPoint point);

		[Export ("selectionView:updateSelectedRect:")]
		void UpdateSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:finishedWithSelectedRect:")]
		void FinishedWithSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:cancelledWithSelectedRect:")]
		void CancelledWithSelectedRect (PSPDFSelectionView selectionView, CGRect rect);

		[Export ("selectionView:singleTappedWithGestureRecognizer:")]
		void SingleTappedWithGestureRecognizer (PSPDFSelectionView selectionView, UITapGestureRecognizer gestureRecognizer);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFSelectionView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSelectionViewDelegate Delegate { get; set; }

		[Export ("selectionAlpha", ArgumentSemantic.Assign)]
		nfloat SelectionAlpha { get; set; }

		[Export ("rects", ArgumentSemantic.Copy), NullAllowed]
		NSValue [] Rects { get; set; }

		[Export ("allowedTouchTypes", ArgumentSemantic.Copy)]
		NSNumber [] AllowedTouchTypes { get; set; }

		// PSPDFSelectionView (SubclassingHooks)

		[Export ("tapGestureRecognizer", ArgumentSemantic.Strong)]
		UITapGestureRecognizer TapGestureRecognizer { get; }
	}

	[BaseType (typeof (PSPDFDocumentSharingCoordinator))]
	interface PSPDFMailCoordinator : IMFMailComposeViewControllerDelegate {

		// PSPDFMailCoordinator (SubclassingHooks)

		[Export ("mailComposeViewController", ArgumentSemantic.Weak)]
		MFMailComposeViewController MailComposeViewController { get; }

		[Export ("addAttachmentData:mimeType:fileName:")]
		void AddAttachmentData (NSData attachment, string mimeType, string fileName);
	}

	[BaseType (typeof (UINavigationItem))]
	interface PSPDFNavigationItem {

		[Export ("closeBarButtonItem", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem CloseBarButtonItem { get; set; }

		[Export ("leftBarButtonItemsForViewMode:")]
		[return: NullAllowed]
		UIBarButtonItem [] GetLeftBarButtonItems (PSPDFViewMode viewMode);

		[Export ("setLeftBarButtonItems:forViewMode:animated:")]
		void SetLeftBarButtonItems (UIBarButtonItem [] barButtonItems, PSPDFViewMode viewMode, bool animated);

		[Export ("setLeftBarButtonItems:animated:")][New]
		void SetLeftBarButtonItems ([NullAllowed] UIBarButtonItem [] items, bool animated);

		[Export ("rightBarButtonItemsForViewMode:")]
		[return: NullAllowed]
		UIBarButtonItem [] GetRightBarButtonItems (PSPDFViewMode viewMode);

		[Export ("setRightBarButtonItems:forViewMode:animated:")]
		void SetRightBarButtonItems (UIBarButtonItem [] barButtonItems, PSPDFViewMode viewMode, bool animated);

		[Export ("setRightBarButtonItems:animated:")][New]
		void SetRightBarButtonItems ([NullAllowed] UIBarButtonItem [] items, bool animated);
	}

	[Static]
	interface PSPDFNewPagePattern {

		[Field ("PSPDFNewPagePatternDot5mm", "__Internal")]
		NSString Dot5mm { get; }

		[Field ("PSPDFNewPagePatternGrid5mm", "__Internal")]
		NSString Grid5mm { get; }

		[Field ("PSPDFNewPagePatternLines5mm", "__Internal")]
		NSString Lines5mm { get; }

		[Field ("PSPDFNewPagePatternLines7mm", "__Internal")]
		NSString Lines7mm { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFNewPageConfiguration {
		
		[Static]
		[Export ("newPageConfigurationWithEmptyPageBuilder:")]
		PSPDFNewPageConfiguration FromEmptyPageBuilder ([NullAllowed] Action<PSPDFNewPageConfigurationBuilder> builderHandler);

		[Static]
		[Export ("newPageConfigurationWithTiledPattern:builderBlock:")]
		PSPDFNewPageConfiguration FromTiledPattern (string tiledPattern, [NullAllowed] Action<PSPDFNewPageConfigurationBuilder> builderHandler);

		[Static]
		[Export ("newPageConfigurationWithDocument:sourcePageIndex:builderBlock:")]
		PSPDFNewPageConfiguration FromDocument (PSPDFDocument sourceDocument, nuint sourcePageIndex, [NullAllowed] Action<PSPDFNewPageConfigurationBuilder> builderHandler);

		[Export ("newPageType")]
		PSPDFNewPageType NewPageType { get; }

		[Export ("pageSize")]
		CGSize PageSize { get; }

		[Export ("pageRotation")]
		nuint PageRotation { get; }

		[Export ("backgroundColor"), NullAllowed]
		UIColor BackgroundColor { get; }

		[Export ("pageMargins")]
		UIEdgeInsets PageMargins { get; }

		[Export ("tiledPattern"), NullAllowed]
		string TiledPattern { get; }

		[Export ("sourceDocument"), NullAllowed]
		PSPDFDocument SourceDocument { get; }

		[Export ("sourcePageIndex")]
		nuint SourcePageIndex { get; }

		[Export ("item"), NullAllowed]
		PSPDFProcessorItem Item { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFNewPageConfigurationBuilder {

		[Export ("pageSize", ArgumentSemantic.Assign)]
		CGSize PageSize { get; set; }

		[Export ("pageRotation")]
		nuint PageRotation { get; set; }

		[Export ("backgroundColor", ArgumentSemantic.Assign), NullAllowed]
		UIColor BackgroundColor { get; set; }

		[Export ("item"), NullAllowed]
		PSPDFProcessorItem Item { get; set; }

		[Export ("pageMargins", ArgumentSemantic.Assign)]
		UIEdgeInsets PageMargins { get; set; }
	}

	interface IPSPDFNewPageViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFNewPageViewControllerDelegate : PSPDFOverridable {
		
		[Abstract]
		[Export ("newPageController:didFinishSelectingConfiguration:")]
		void DidFinishSelectingConfiguration (PSPDFNewPageViewController controller, [NullAllowed] PSPDFNewPageConfiguration configuration);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFNewPageViewController {

		[Export ("initWithDocumentEditorConfiguration:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentEditorConfiguration configuration);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFNewPageViewControllerDelegate Delegate { get; set; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }
	}

	interface IPSPDFPageCellImageRequestToken { }

	[Protocol]
	interface PSPDFPageCellImageRequestToken {

		[Abstract]
		[Export ("expectedSize")]
		CGSize ExpectedSize { get; }

		[return: NullAllowed]
		[Export ("placeholderImage")]
		UIImage GetPlaceholderImage ();

		[Export ("cancel")]
		void Cancel ();
	}

	interface IPSPDFPageCellImageLoading { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPageCellImageLoading {

		[Abstract]
		[Export ("requestImageForPageAtIndex:availableSize:completionHandler:")]
		IPSPDFPageCellImageRequestToken RequestImage (nuint pageIndex, CGSize size, Action<UIImage> completionHandler);
	}

	[BaseType (typeof (UICollectionViewCell))]
	interface PSPDFPageCell {

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[Export ("shadowEnabled")]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("pageLabelEnabled")]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("setNeedsUpdateImage")]
		void SetNeedsUpdateImage ();

		[NullAllowed, Export ("imageLoader", ArgumentSemantic.Assign)]
		IPSPDFPageCellImageLoading ImageLoader { get; set; }

		// category PSPDFPageCell (Subviews)

		[Export ("pageLabel")]
		UILabel PageLabel { get; }

		[Export ("imageView")]
		UIImageView ImageView { get; }

		// category PSPDFPageCell (SubclassingHooks)

		[Export ("image"), NullAllowed]
		UIImage Image { get; set; }

		[Export ("pathShadowForView:")]
		[return: NullAllowed]
		UIBezierPath GetPathShadow (UIView imageView);

		[Export ("contentRectForBounds:")]
		CGRect GetContentRect (CGRect bounds);

		[Export ("imageRectForContentRect:")]
		CGRect GetImageRect (CGRect contentRect);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFDrawView : PSPDFAnnotationViewProtocol, PSPDFOverridable, INativeObject {

		[Export ("annotationType", ArgumentSemantic.Assign)]
		PSPDFAnnotationType AnnotationType { get; set; }

		[Export ("annotationVariant"), NullAllowed]
		string AnnotationVariant { get; set; }

		[Export ("inputMode", ArgumentSemantic.Assign)]
		PSPDFDrawViewInputMode InputMode { get; set; }

		[Export ("allowedTouchTypes", ArgumentSemantic.Copy)]
		NSNumber [] AllowedTouchTypes { get; set; }

		[Export ("currentDrawLayer"), NullAllowed]
		NSObject CurrentDrawLayer { get; }

		[Export ("drawLayers")]
		NSObject [] DrawLayers { get; }

		[Export ("clearAllLayers")]
		void ClearAllLayers ();

		[Export ("annotations")]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("drawCreateMode", ArgumentSemantic.Assign)]
		PSPDFDrawCreateMode DrawCreateMode { get; set; }

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("predictiveTouchesEnabled", ArgumentSemantic.Assign)]
		bool PredictiveTouchesEnabled { get; set; }

		[Export ("strokeColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor StrokeColor { get; set; }

		[Export ("fillColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor FillColor { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		nfloat LineWidth { get; set; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("dashArray", ArgumentSemantic.Retain), NullAllowed]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity", ArgumentSemantic.Assign)]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("guideBorderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor GuideBorderColor { get; set; }

		[Export ("updateActionsForAnnotations:")]
		NSObject [] UpdateActionsForAnnotations (PSPDFInkAnnotation [] annotations);

		[Export ("scale", ArgumentSemantic.Assign)]
		nfloat Scale { get; set; }

//		Use (Get/Set)ZoomScale from interface
//		[Export ("zoomScale", ArgumentSemantic.Assign)]
//		nfloat ZoomScale { get; set; }

		[Export ("startDrawingAtPoint:")]
		void StartDrawingAtPoint (PSPDFDrawingPoint location);

		[Export ("continueDrawingAtPoints:predictedPoints:")]
		void ContinueDrawingAtPoints (NSValue [] locations, NSValue [] predictedLocations);

		[Export ("endDrawing")]
		void EndDrawing ();

		[Export ("cancelDrawing")]
		void CancelDrawing ();

		[Export ("guideSnapAllowance", ArgumentSemantic.Assign)]
		nfloat GuideSnapAllowance { get; set; }

		[Export ("eraseAt:")]
		void EraseAt (NSValue [] locations);

		[Export ("endErase")]
		void EndErase ();

		// PSPDFDrawView (SubclassingHooks) Category

		[Export ("shouldProcessTouches:withEvent:")]
		bool ShouldProcessTouches (NSSet<UITouch> touches, UIEvent uiEvent);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFSignatureCell {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFFile {

		[DesignatedInitializer]
		[Export ("initWithName:URL:data:")]
		IntPtr Constructor (string fileName, [NullAllowed] NSUrl fileUrl, [NullAllowed] NSData fileData);

		[Export ("fileName")]
		string FileName { get; }

		[Export ("fileURL", ArgumentSemantic.Strong), NullAllowed]
		NSUrl FileUrl { get; }

		[Export ("fileData", ArgumentSemantic.Copy), NullAllowed]
		NSData FileData { get; }

		[Export ("mimeType")]
		string MimeType { get; }

		[return: NullAllowed]
		[Export ("fileDataMappedWithError:")]
		NSData GetFileDataMapped (out NSError error);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFEmbeddedFile {

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("fileName")]
		string FileName { get; }

		[Export ("fileSize", ArgumentSemantic.Assign)]
		ulong FileSize { get; }

		[Export ("fileDescription"), NullAllowed]
		string FileDescription { get; }

		[Export ("modificationDate", ArgumentSemantic.Strong), NullAllowed]
		NSDate ModificationDate { get; }

		[Export ("fileURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl FileUrl { get; }

		[return: NullAllowed]
		[Export ("fileURLWithError:")]
		NSUrl GetFileUrl (out NSError error);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileDataProvider : PSPDFDataProvider {

		[Export ("initWithFileURL:error:")]
		IntPtr Constructor (NSUrl fileUrl, out NSError error);

		[Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileDataSink : PSPDFDataSink {

		[DesignatedInitializer]
		[Export ("initWithFileURL:options:error:")]
		IntPtr Constructor (NSUrl fileUrl, PSPDFDataSinkOptions options, out NSError error);

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFDataSinkOptions Options { get; }

		[Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }
	}

	interface IPSPDFEmbeddedFilesViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFEmbeddedFilesViewControllerDelegate {

		[Export ("embeddedFilesController:didSelectFile:sender:")]
		[Abstract]
		void DidSelectFile (PSPDFEmbeddedFilesViewController embeddedFilesController, PSPDFEmbeddedFile embeddedFile, [NullAllowed] NSObject sender);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFEmbeddedFilesViewController {

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFEmbeddedFilesViewControllerDelegate Delegate { get; set; }
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFEmbeddedFileCell {

	}

	interface IPSPDFDocumentSharingViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSharingViewControllerDelegate : PSPDFOverridable {

		[Export ("documentSharingViewController:didFinishWithSelectedOptions:files:annotationSummary:error:")][Abstract]
		void DidFinishWithSelectedOptions (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, PSPDFFile [] files, [NullAllowed] NSAttributedString annotationSummary, [NullAllowed] NSError error);

		[Export ("documentSharingViewControllerDidCancel:")]
		void DocumentSharingViewControllerDidCancel (PSPDFDocumentSharingViewController shareController);

		[Export ("documentSharingViewController:shouldPrepareWithSelectedOptions:selectedPageRange:")]
		bool ShouldPrepareWithSelectedOptions (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, NSRange selectedPageRange);

		[Export ("documentSharingViewController:preparationProgress:")]
		string PreparationProgress (PSPDFDocumentSharingViewController shareController, nfloat progress);

		[Export ("documentSharingViewController:titleForOption:")]
		[return: NullAllowed]
		string GetTitleForOption (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions option);

		[Export ("documentSharingViewController:subtitleForOption:")]
		[return: NullAllowed]
		string GetSubtitleForOption (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions option);

		[Export ("documentSharingViewController:configureCustomProcessorConfigurationOptions:")]
		void ConfigureCustomProcessorConfigurationOptions (PSPDFDocumentSharingViewController shareController, PSPDFProcessorConfiguration processorConfiguration);

		[Export ("processorSaveOptionsForDocumentSharingViewController:")]
		PSPDFProcessorSaveOptions ProcessorSaveOptionsForDocumentSharingViewController (PSPDFDocumentSharingViewController shareController);

		[Export ("temporaryDirectoryForDocumentSharingViewController:")]
		[return: NullAllowed]
		string GetTemporaryDirectoryForDocumentSharingViewController (PSPDFDocumentSharingViewController shareController);

		[Export ("documentSharingViewController:willShareFiles:")]
		PSPDFFile [] WillShareFiles (PSPDFDocumentSharingViewController shareController, PSPDFFile [] files);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFDocumentSharingViewController : PSPDFStyleable {

		[Export ("initWithDocuments:visiblePageRange:allowedSharingOptions:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument [] documents, NSRange visiblePageRange, PSPDFDocumentSharingOptions sharingOptions);

		[Export ("checkIfControllerHasOptionsAvailableAndCallDelegateIfNot", ArgumentSemantic.Assign)]
		bool CheckIfControllerHasOptionsAvailableAndCallDelegateIfNot { get; }

		[Export ("commitWithCurrentSettings", ArgumentSemantic.Assign)]
		bool CommitWithCurrentSettings { get; }

		[Export ("documents")]
		PSPDFDocument [] Documents { get; }

		[Export ("sharingOptions", ArgumentSemantic.Weak)]
		PSPDFDocumentSharingOptions SharingOptions { get; set; }

		[Export ("selectedOptions", ArgumentSemantic.Weak)]
		PSPDFDocumentSharingOptions SelectedOptions { get; set; }

		[Export ("commitButtonTitle")][NullAllowed]
		string CommitButtonTitle { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFDocumentSharingViewControllerDelegate Delegate { get; set; }

		// PSPDFDocumentSharingViewController (SubclassingHooks)

		[Export ("delegateConfigureCustomProcessorConfigurationOptions:")]
		void DelegateConfigureCustomProcessorConfigurationOptions (PSPDFProcessorConfiguration processorConfiguration);

		[Export ("delegateProcessorSaveOptions")]
		PSPDFProcessorSaveOptions DelegateProcessorSaveOptions { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFEditingChange {

		[Export ("initWithOperation:affectedPageIndex:destinationPageIndex:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFEditingOperation operation, nuint affectedPageIndex, nuint destinationPageIndex);

		[Export ("operation")]
		PSPDFEditingOperation Operation { get; }

		[Export ("affectedPageIndex")]
		nuint AffectedPageIndex { get; }

		[Export ("destinationPageIndex")]
		nuint DestinationPageIndex { get; }
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFToolbarButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("setImage:")]
		void SetImage ([NullAllowed] UIImage image);

		[Export ("styleForHighlightedState:")]
		void StyleForHighlightedState (bool highlighted);

		[Export ("userInfo", ArgumentSemantic.Retain)]
		NSObject UserInfo { get; set; }

		[Export ("setEnabled:animated:")]
		void SetEnabled (bool enabled, bool animated);

		[Export ("collapsible", ArgumentSemantic.Weak)]
		bool Collapsible { [Bind ("isCollapsible")] get; set; }

		[Export ("length", ArgumentSemantic.Weak)]
		nfloat Length { get; set; }

		[Export ("setLengthToFit")]
		void SetLengthToFit ();

		[Export ("flexible", ArgumentSemantic.Weak)]
		bool Flexible { [Bind ("isFlexible")] get; set; }

		[Export ("setTintColorDidChangeBlock:")]
		void SetTintColorDidChangeHandler (Action<UIColor> handler);
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarSpacerButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Static]
		[Export ("flexibleSpacerButton")]
		PSPDFToolbarSpacerButton CreateFlexibleSpacerButton ();
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarGroupButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("longPressRecognizer", ArgumentSemantic.Retain)]
		UILongPressGestureRecognizer LongPressRecognizer { get; }

		[Export ("groupIndicatorPosition", ArgumentSemantic.Assign)]
		PSPDFToolbarGroupButtonIndicatorPosition GroupIndicatorPosition { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarGroupButton))]
	interface PSPDFToolbarCollapsedButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("mimickedButton", ArgumentSemantic.Retain), NullAllowed]
		UIButton MimickedButton { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarTickerButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("timeInterval", ArgumentSemantic.Assign)]
		double TimeInterval { get; set; }

		[Export ("accelerate", ArgumentSemantic.Assign)]
		bool Accelerate { get; set; }
	}

	[BaseType (typeof(PSPDFToolbarButton))]
	interface PSPDFToolbarDualButton {
		
		[Export ("longPressRecognizer")]
		UILongPressGestureRecognizer LongPressRecognizer { get; }

		[Export ("primaryImage", ArgumentSemantic.Assign), NullAllowed]
		UIImage PrimaryImage { get; set; }

		[Export ("secondaryImage", ArgumentSemantic.Assign), NullAllowed]
		UIImage SecondaryImage { get; set; }

		[Export ("primaryEnabled")]
		bool PrimaryEnabled { get; set; }

		[Export ("secondaryEnabled")]
		bool SecondaryEnabled { get; set; }
	}

	[BaseType (typeof (PSPDFToolbarSpacerButton))]
	interface PSPDFToolbarSeparatorButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("hairlineView", ArgumentSemantic.Retain)]
		UIView HairlineView { get; }
	}

	[BaseType (typeof (PSPDFToolbarButton))]
	interface PSPDFToolbarSelectableButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("setSelected:animated:")]
		void SetSelected (bool selected, bool animated);

		[Export ("selectedTintColor")]
		UIColor SelectedTintColor { get; set; }

		[Export ("selectedBackgroundColor")]
		UIColor SelectedBackgroundColor { get; set; }

		[Export ("selectionPadding")]
		nfloat SelectionPadding { get; set; }

		[Export ("highlightsSelection")]
		bool HighlightsSelection { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFToolbar {

		[Field ("PSPDFToolbarDefaultFixedDimensionLength", "__Internal")]
		nfloat DefaultFixedDimensionLength { get; }

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("buttons", ArgumentSemantic.Copy)]
		PSPDFToolbarButton [] Buttons { get; set; }

		[Export ("setButtons:animated:")]
		void SetButtons (PSPDFToolbarButton [] buttons, bool animated);

		[Export ("backgroundView", ArgumentSemantic.Retain), NullAllowed]
		UIView BackgroundView { get; set; }

		[Export ("contentView", ArgumentSemantic.Retain)]
		UIView ContentView { get; }

		[Export ("barTintColor", ArgumentSemantic.Retain)]
		UIColor BarTintColor { get; set; }

		[Export ("fixedDimension")]
		nfloat FixedDimension { get; set; }

		[Export ("collapsedButtons", ArgumentSemantic.Copy)]
		PSPDFToolbarButton [] CollapsedButtons { get; }

		[Export ("collapsedButton", ArgumentSemantic.Retain)]
		PSPDFToolbarButton CollapsedButton { get; }

		// PSPDFToolbar (SubclassingHooks)

		[Export ("layoutMainSubviews")]
		void LayoutMainSubviews ();

		[Export ("setCollapsedButtonVisible:")]
		void SetCollapsedButtonVisible (bool visible);

		[Export ("horizontal")]
		bool Horizontal { [Bind ("isHorizontal")] get; }

		[Export ("buttonSpacing")]
		nfloat ButtonSpacing { get; }
	}


	interface IPSPDFFlexibleToolbarDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFlexibleToolbarDelegate {

		[Export ("flexibleToolbarWillShow:")]
		void FlexibleToolbarWillShow (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarDidShow:")]
		void FlexibleToolbarDidShow (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarWillHide:")]
		void FlexibleToolbarWillHide (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbarDidHide:")]
		void FlexibleToolbarDidHide (PSPDFFlexibleToolbar toolbar);

		[Export ("flexibleToolbar:didChangePosition:")]
		void DidChangePosition (PSPDFFlexibleToolbar toolbar, PSPDFFlexibleToolbarPosition position);
	}

	[BaseType (typeof (PSPDFToolbar))]
	interface PSPDFFlexibleToolbar {

		[Export ("supportedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition SupportedToolbarPositions { get; set; }

		[Export ("toolbarPosition", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition ToolbarPosition { get; set; }

		[Export ("setToolbarPosition:animated:")]
		void SetToolbarPosition (PSPDFFlexibleToolbarPosition toolbarPosition, bool animated);

		[Export ("toolbarDelegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFlexibleToolbarDelegate ToolbarDelegate { get; set; }

		[Export ("dragEnabled", ArgumentSemantic.Assign)]
		bool DragEnabled { [Bind ("isDragEnabled")] get; set; }

		[Export ("buttons", ArgumentSemantic.Copy)][New]
		UIButton [] Buttons { get; set; }

		[Export ("selectedButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton SelectedButton { get; set; }

		[Export ("setSelectedButton:animated:")]
		void SetSelectedButton ([NullAllowed] UIButton button, bool animated);

		[Export ("showToolbarAnimated:completion:")]
		void ShowToolbarAnimated (bool animated, [NullAllowed] Action<bool> completionHandler);

		[Export ("hideToolbarAnimated:completion:")]
		void HideToolbarAnimated (bool animated, [NullAllowed] Action<bool> completionHandler);

		[Export ("dragView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFlexibleToolbarDragView DragView { get; }

		[Export ("selectedTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectedTintColor { get; set; }

		[Export ("selectedBackgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectedBackgroundColor { get; set; }

		[Export ("borderedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition BorderedToolbarPositions { get; set; }

		[Export ("shadowedToolbarPositions", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarPosition ShadowedToolbarPositions { get; set; }

		[Export ("matchUIBarAppearance:")]
		void MatchUIBarAppearance (UIView navigationBarOrToolbar);

		[Export ("preferredSizeFitting:forToolbarPosition:")]
		CGSize PreferredSizeFitting (CGSize availableSize, PSPDFFlexibleToolbarPosition position);

		[Export ("showMenuWithItems:target:animated:")]
		void ShowMenuWithItems (PSPDFMenuItem [] menuItems, UIView target, bool animated);

		[Export ("showMenuForCollapsedButtons:fromButton:animated:")]
		void ShowMenuForCollapsedButtons (UIButton [] buttons, UIButton sourceButton, bool animated);

		[Export ("menuItemForButton:")]
		PSPDFMenuItem MenuItemForButton (UIButton button);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFFlexibleToolbarDragView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("barColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BarColor { get; set; }

		[Export ("inverted", ArgumentSemantic.Assign)]
		bool Inverted { get; set; }

		[Export ("setInverted:animated:")]
		void SetInverted (bool inverted, bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFFlexibleToolbar))]
	interface PSPDFAnnotationToolbar : PSPDFAnnotationStateManagerDelegate {

		[Export ("initWithAnnotationStateManager:")]
		IntPtr Constructor (PSPDFAnnotationStateManager annotationStateManager);

		[Export ("annotationStateManager", ArgumentSemantic.Strong)]
		PSPDFAnnotationStateManager AnnotationStateManager { get; set; }

		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSSet<NSString> EditableAnnotationTypes { get; set; }

		[Export ("configurations", ArgumentSemantic.Copy), NullAllowed]
		PSPDFAnnotationToolbarConfiguration [] Configurations { get; set; }

		[Export ("annotationGroups")]
		PSPDFAnnotationGroup [] AnnotationGroups { get; }

		[Export ("buttonWithType:variant:createFromGroup:")] // PSPDFAnnotationString
		UIButton ButtonFromType (NSString type, [NullAllowed] string variant, bool createFromGroup);

		[Export ("additionalButtons", ArgumentSemantic.Copy), NullAllowed]
		UIButton [] AdditionalButtons { get; set; }

		[Export ("collapseUndoButtonsForCompactSizes", ArgumentSemantic.Assign)]
		bool CollapseUndoButtonsForCompactSizes { get; set; }

		[Export ("showsStylusButtonAutomatically", ArgumentSemantic.Assign)]
		bool ShowsStylusButtonAutomatically { get; set; }

		[Export ("showingStylusButton", ArgumentSemantic.Assign)]
		bool ShowingStylusButton { [Bind ("isShowingStylusButton")] get; set; }

		[Export ("setShowingStylusButton:animated:")]
		void SetShowingStylusButton (bool showingStylusButton, bool animated);

		[Export ("saveAfterToolbarHiding", ArgumentSemantic.Assign)]
		bool SaveAfterToolbarHiding { get; set; }

		// PSPDFAnnotationToolbar (SubclassingHooks) Category

		[Export ("doneButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton DoneButton { get; }

		[Export ("stylusButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFToolbarButton StylusButton { get; }

		[Export ("undoButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton UndoButton { get; }

		[Export ("redoButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton RedoButton { get; }

		[Export ("undoRedoButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFToolbarDualButton UndoRedoButton { get; }

		[Export ("strokeColorButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFColorButton StrokeColorButton { get; }

		[Export ("done:")]
		void Done ([NullAllowed] NSObject sender);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationToolbarConfiguration {

		[Export ("initWithAnnotationGroups:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAnnotationGroup [] annotationGroups);

		[Export ("annotationGroups")]
		PSPDFAnnotationGroup [] AnnotationGroups { get; }
	}

	interface IPSPDFFlexibleToolbarContainerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFlexibleToolbarContainerDelegate {

		[Export ("flexibleToolbarContainerWillShow:")]
		void FlexibleToolbarContainerWillShow (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidShow:")]
		void FlexibleToolbarContainerDidShow (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerWillHide:")]
		void FlexibleToolbarContainerWillHide (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidHide:")]
		void FlexibleToolbarContainerDidHide (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerContentRect:forToolbarPosition:")]
		CGRect FlexibleToolbarContainerContentRect (PSPDFFlexibleToolbarContainer container, PSPDFFlexibleToolbarPosition position);

		[Export ("flexibleToolbarContainerWillStartDragging:")]
		void FlexibleToolbarContainerWillStartDragging (PSPDFFlexibleToolbarContainer container);

		[Export ("flexibleToolbarContainerDidEndDragging:withPosition:")]
		void WithPosition (PSPDFFlexibleToolbarContainer container, PSPDFFlexibleToolbarPosition position);
	}

	interface IPSPDFSystemBar { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSystemBar {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFFlexibleToolbarContainer {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("flexibleToolbar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFlexibleToolbar FlexibleToolbar { get; set; }

		[Export ("overlaidBar", ArgumentSemantic.Weak)]
		IPSPDFSystemBar OverlaidBar { get; set; }

		[Export ("dragging", ArgumentSemantic.Assign)]
		bool Dragging { get; }

		[Export ("flickToCloseEnabled", ArgumentSemantic.Assign)]
		bool FlickToCloseEnabled { [Bind ("isFlickToCloseEnabled")] get; set; }

		[Export ("containerDelegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFFlexibleToolbarContainerDelegate ContainerDelegate { get; set; }

		[Export ("anchorViewBackgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor AnchorViewBackgroundColor { get; set; }

		[Export ("showAnimated:completion:")]
		void ShowAnimated (bool animated, [NullAllowed] Action<bool> completionHandler);

		[Export ("hideAnimated:completion:")]
		void HideAnimated (bool animated, [NullAllowed] Action<bool> completionHandler);

		[Export ("hideAndRemoveAnimated:completion:")]
		void HideAndRemove (bool animated, [NullAllowed] Action<bool> completionHandler);

		// PSPDFFlexibleToolbarContainer (SubclassingHooks) Category

		[Export ("rectForToolbarPosition:")]
		CGRect RectForToolbarPosition (PSPDFFlexibleToolbarPosition toolbarPosition);

		[Export ("animateToolbarPositionChangeFrom:to:")]
		void AnimateToolbarPositionChangeFrom (PSPDFFlexibleToolbarPosition currentPosition, PSPDFFlexibleToolbarPosition newPosition);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFlexibleToolbarController : PSPDFFlexibleToolbarContainerDelegate {

		[Export ("initWithToolbar:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFFlexibleToolbar toolbar);

		[Export ("toolbar")]
		PSPDFFlexibleToolbar Toolbar { get; }

		[Export ("flexibleToolbarContainer"), NullAllowed]
		PSPDFFlexibleToolbarContainer FlexibleToolbarContainer { get; }

		[Export ("toolbarVisible")]
		bool ToolbarVisible { [Bind ("isToolbarVisible")] get; }

		[Export ("toggleToolbarAnimated:")]
		void ToggleToolbarAnimated (bool animated);

		[Export ("showToolbarAnimated:")]
		bool ShowToolbarAnimated (bool animated);

		[Export ("hideToolbarAnimated:")]
		bool HideToolbarAnimated (bool animated);

		[Export ("updateHostView:container:viewController:")]
		void UpdateHostView ([NullAllowed] UIView hostView, [NullAllowed] NSObject container, [NullAllowed] UIViewController viewController);

		[Export ("hostView"), NullAllowed]
		UIView HostView { get; }

		[Export ("hostToolbar"), NullAllowed]
		PSPDFSystemBar HostToolbar { get; }

		[Export ("hostViewController", ArgumentSemantic.Weak), NullAllowed]
		UIViewController HostViewController { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFFlexibleToolbarController))]
	interface PSPDFAnnotationToolbarController {

		[Field ("PSPDFAnnotationToolbarControllerVisibilityDidChangeNotification", "__Internal")]
		[Notification]
		NSString VisibilityDidChangeNotification { get; }

		[Field ("PSPDFAnnotationToolbarControllerVisibilityAnimatedKey", "__Internal")]
		NSString VisibilityAnimatedKey { get; }

		[Export ("initWithAnnotationToolbar:")]
		IntPtr Constructor (PSPDFAnnotationToolbar annotationToolbar);

		[Export ("annotationToolbar")]
		PSPDFAnnotationToolbar AnnotationToolbar { get; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFFlexibleToolbarContainerDelegate Delegate { get; set; }
	}


	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationGroup {

		[Static]
		[Export ("groupWithItems:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items);

		[Static]
		[Export ("groupWithItems:choice:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items, nuint choice);

		[Export ("items", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItem [] Items { get; }

		[Export ("editableItems", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItem [] EditableItems { get; }

		[Export ("choice", ArgumentSemantic.Assign)]
		nuint Choice { get; }

		[Export ("updateChoiceToItemWithType:variant:")] // PSPDFAnnotationString both
		bool UpdateChoiceToItem (NSString annotationStringType, [NullAllowed] NSString variant);
	}

	delegate UIImage PSPDFAnnotationGroupItemConfigurationHandler (PSPDFAnnotationGroupItem item, NSObject container, UIColor tintColor);

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationGroupItem {

		[Static]
		[Export ("itemWithType:")]
		PSPDFAnnotationGroupItem FromType (NSString annotationStringType); // PSPDFAnnotationString

		[Static]
		[Export ("itemWithType:variant:")]
		PSPDFAnnotationGroupItem FromType (NSString annotationStringType, [NullAllowed] string variant); // PSPDFAnnotationString

		[Static]
		[Export ("itemWithType:variant:configurationBlock:")]
		PSPDFAnnotationGroupItem FromType (NSString annotationStringType, [NullAllowed] string variant, [NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler); // PSPDFAnnotationString

		[Static]
		[Export ("setDefaultConfigurationBlock:")]
		void SetDefaultConfigurationHanlder ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Export ("type")] // PSPDFAnnotationString
		NSString Type { get; }

		[Export ("variant")]
		string Variant { get; }

		[Export ("setConfigurationBlock:", ArgumentSemantic.Copy)]
		void SetConfigurationHandler ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		// PSPDFAnnotationGroupItem (PSPDFPresets) Category

		[Static]
		[Export ("inkConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler InkConfigurationHandler ();

		[Static]
		[Export ("lineConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler LineConfigurationHandler ();

		[Static]
		[Export ("freeTextConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler FreeTextConfigurationHandler ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFModel : INSCopying, INSCoding {

		[Static]
		[Export ("modelWithDictionary:error:")]
		PSPDFModel FromDictionary ([NullAllowed] NSDictionary<NSString, NSObject> dictionaryValue, out NSError error);

		[Export ("initWithDictionary:error:")]
		IntPtr Constructor ([NullAllowed] NSDictionary<NSString, NSObject> dictionaryValue, out NSError error);

		[Static]
		[Export ("propertyKeys")]
		NSOrderedSet<NSString> GetPropertyKeys ();

		[Static]
		[Export ("cachedPropertyKeys")]
		NSString [] CachedPropertyKeys ();

		[Static]
		[Export ("cachedPropertyKeySet")]
		NSObject CachedPropertyKeySet ();

		[Static]
		[Export ("propertyKeysWithReferentialEquality")]
		NSOrderedSet<NSString> PropertyKeysWithReferentialEquality ();

		[Export ("dictionaryValue", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> DictionaryValue { get; }

		[Export ("mergeValueForKey:fromModel:")]
		void MergeValueForKey (string key, PSPDFModel model);

		[Export ("mergeValuesForKeysFromModel:")]
		void MergeValuesForKeysFromModel (PSPDFModel model);
	}

	interface IPSPDFMultiDocumentListControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMultiDocumentListControllerDelegate {
		
		[Export ("multiDocumentListController:didSelectDocumentAtIndex:")]
		void DidSelectDocument (PSPDFMultiDocumentListController multiDocumentListController, nuint index);

		[Export ("multiDocumentListControllerDidCancel:")]
		void DidCancelSelectingDocument (PSPDFMultiDocumentListController multiDocumentListController);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFMultiDocumentListController {
		
		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFMultiDocumentListControllerDelegate Delegate { get; set; }

		[Export ("tabbedViewController", ArgumentSemantic.Weak), NullAllowed]
		PSPDFTabbedViewController TabbedViewController { get; set; }
	}

	[BaseType (typeof (PSPDFDocumentSharingCoordinator))]
	interface PSPDFMessageCoordinator {

		[Export ("sharingOptions", ArgumentSemantic.Assign)][New]
		PSPDFDocumentSharingOptions SharingOptions { get; set; }

		// PSPDFMessageCoordinator (SubclassingHooks)

		[Export ("messageComposeViewController", ArgumentSemantic.Weak)]
		MFMessageComposeViewController MessageComposeViewController { get; }
	}

	[BaseType (typeof (UIMenuItem))]
	interface PSPDFMenuItem {

		[Export ("initWithTitle:block:")]
		IntPtr Constructor (string title, [NullAllowed] Action handler);

		[Export ("initWithTitle:block:identifier:")]
		IntPtr Constructor (string title, [NullAllowed] Action handler, [NullAllowed] string identifier);

		[Export ("initWithTitle:image:block:identifier:")]
		IntPtr Constructor (string title, [NullAllowed] UIImage image, [NullAllowed] Action handler, [NullAllowed] string identifier);

		[Export ("initWithTitle:image:block:identifier:allowImageColors:")]
		[DesignatedInitializer]
		IntPtr Constructor (string title, [NullAllowed] UIImage image, Action handler, [NullAllowed] string identifier, bool allowImageColors);

		[Export ("enabled", ArgumentSemantic.Assign)]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Export ("shouldInvokeAutomatically", ArgumentSemantic.Assign)]
		bool ShouldInvokeAutomatically { get; set; }

		[Export ("identifier"), NullAllowed]
		string Identifier { get; set; }

		[Export ("ps_image", ArgumentSemantic.Strong), NullAllowed]
		UIImage PsImage { get; set; }

		[Export ("actionBlock", ArgumentSemantic.Copy)]
		Action ActionHandler { get; set; }

		[Static]
		[Export ("installMenuHandlerForObject:")]
		void InstallMenuHandler (NSObject obj);

		// PSPDFMenuItem (Analytics) Category

		[Export ("performBlock")]
		void PerformHandler ();
	}
		
	[Static]
	interface PSPDFPresentationKeys {
		[Field ("PSPDFPresentationStyleKey", "__Internal")]
		NSString StyleKey { get; }

		[Field ("PSPDFPresentationNonAdaptiveKey", "__Internal")]
		NSString NonAdaptiveKey { get; }

		[Field ("PSPDFPresentationRectKey", "__Internal")]
		NSString RectKey { get; }

		[Field ("PSPDFPresentationRectBlockKey", "__Internal")]
		NSString RectBlockKey { get; }

		[Field ("PSPDFPresentationContentSizeKey", "__Internal")]
		NSString ContentSizeKey { get; }

		[Field ("PSPDFPresentationInNavigationControllerKey", "__Internal")]
		NSString InNavigationControllerKey { get; }

		[Field ("PSPDFPresentationCloseButtonKey", "__Internal")]
		NSString CloseButtonKey { get; }

		[Field ("PSPDFPresentationPersistentCloseButtonKey", "__Internal")]
		NSString PersistentCloseButtonKey { get; }

		[Field ("PSPDFPresentationReuseNavigationControllerKey", "__Internal")]
		NSString ReuseNavigationControllerKey { get; }

		[Field ("PSPDFPresentationPopoverArrowDirectionsKey", "__Internal")]
		NSString PopoverArrowDirectionsKey { get; }

		[Field ("PSPDFPresentationPopoverPassthroughViewsKey", "__Internal")]
		NSString PopoverPassthroughViewsKey { get; }

		[Field ("PSPDFPresentationPopoverBackgroundColorKey", "__Internal")]
		NSString PopoverBackgroundColorKey { get; }
	}

	[Static]
	interface PSPDFProcessorOptionKeys {
		
		[Field ("PSPDFProcessorAnnotationTypesKey", "__Internal")]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFProcessorAnnotationDictKey", "__Internal")]
		NSString AnnotationDictKey { get; }

		[Field ("PSPDFProcessorAnnotationAsDictionaryKey", "__Internal")]
		NSString AnnotationAsDictionaryKey { get; }

		[Field ("PSPDFProcessorUserPasswordKey", "__Internal")]
		NSString UserPasswordKey { get; }

		[Field ("PSPDFProcessorOwnerPasswordKey", "__Internal")]
		NSString OwnerPasswordKey { get; }

		[Field ("PSPDFProcessorKeyLengthKey", "__Internal")]
		NSString KeyLengthKey { get; }

		[Field ("PSPDFProcessorPageRectKey", "__Internal")]
		NSString PageRectKey { get; }

		[Field ("PSPDFProcessorNumberOfPagesKey", "__Internal")]
		NSString NumberOfPagesKey { get; }

		[Field ("PSPDFProcessorPageBorderMarginKey", "__Internal")]
		NSString PageBorderMarginKey { get; }

		[Field ("PSPDFProcessorAdditionalDelayKey", "__Internal")]
		NSString AdditionalDelayKey { get; }

		[Field ("PSPDFProcessorStripEmptyPagesKey", "__Internal")]
		NSString StripEmptyPagesKey { get; }

		[Field ("PSPDFProcessorSkipPDFCreationKey", "__Internal")]
		NSString SkipPdfCreationKey { get; }

		[Field ("PSPDFProcessorDocumentTitleKey", "__Internal")]
		NSString DocumentTitleKey { get; }
	}

	delegate void PSPDFProcessorProgressHandler (nuint currentPage, nuint totalPages);
	delegate void PSPDFProcessorPdfFromUrlHandler (NSUrl fileUrl, NSError error);
	delegate void PSPDFProcessorPdfFromUrlNsdataHandler (NSData fileData, NSError error);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFProcessor {
		
		[Static]
		[Export ("generatePDFFromConfiguration:saveOptions:outputFileURL:progressBlock:error:")]
		bool GeneratePdf (PSPDFProcessorConfiguration configuration, [NullAllowed] PSPDFProcessorSaveOptions saveOptions, NSUrl fileUrl, [NullAllowed] PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Static]
		[Export ("generatePDFFromConfiguration:saveOptions:progressBlock:error:")]
		[return: NullAllowed]
		NSData GeneratePdf (PSPDFProcessorConfiguration configuration, [NullAllowed] PSPDFProcessorSaveOptions saveOptions, [NullAllowed] PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Static]
		[Export ("generatePDFFromConfiguration:saveOptions:outputDataSink:progressBlock:error:")]
		bool GeneratePdf (PSPDFProcessorConfiguration configuration, [NullAllowed] PSPDFProcessorSaveOptions saveOptions, IPSPDFDataSink outputDataSink, [NullAllowed] PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Static]
		[Export ("generatePDFFromHTMLString:outputFileURL:options:completionBlock:")]
		void GeneratePdfFromHtml (string html, NSUrl fileUrl, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<NSError> completionHandler);

		[Static]
		[Export ("generatePDFFromHTMLString:options:completionBlock:")]
		void GeneratePdfFromHtml (string html, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] PSPDFProcessorPdfFromUrlNsdataHandler completionHandler);

		[Static]
		[return: NullAllowed]
		[Export ("generatePDFFromURL:outputFileURL:options:completionBlock:")]
		PSPDFConversionOperation GeneratePdfFromUrl (NSUrl inputUrl, NSUrl outputUrl, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] PSPDFProcessorPdfFromUrlHandler completionHandler);

		[Static]
		[Export ("generatePDFFromURL:options:completionBlock:")]
		PSPDFConversionOperation GeneratePdfFromUrl (NSUrl inputUrl, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] PSPDFProcessorPdfFromUrlNsdataHandler completionHandler);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSOperation))]
	interface PSPDFConversionOperation {

		[Export ("inputURL", ArgumentSemantic.Copy)]
		NSUrl InputUrl { get; }

		[Export ("outputFileURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl OutputFileUrl { get; }

		[Export ("outputData", ArgumentSemantic.Strong), NullAllowed]
		NSData OutputData { get; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; }

		[Export ("error", ArgumentSemantic.Strong), NullAllowed]
		NSError Error { get; }
	}

	delegate void PSPDFRenderDrawHandler (CGContext context, nuint page, CGRect cropBox, nuint rotation, [NullAllowed] NSDictionary<NSString, NSObject> options);

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorConfiguration : INSCopying {
		
		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("document"), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("pageCount")]
		nint PageCount { get; }

		[Export ("movePages:toDestinationIndex:")]
		void MovePages (NSIndexSet indexes, nuint destinationPageIndex);

		[Export ("removePages:")]
		void RemovePages (NSIndexSet indexes);

		[Export ("setShouldStripBlankPagesOnGenerate:")]
		void SetShouldStripBlankPagesOnGenerate (bool shouldStrip);

		[Export ("includeOnlyIndexes:")]
		void IncludeOnlyIndexes (NSIndexSet indexes);

		[Export ("rotatePage:by:")]
		void RotatePage (nuint pageIndex, nuint degrees);

		[Export ("scalePage:toSize:")]
		void ScalePage (nuint pageIndex, CGSize size);

		[Export ("scalePage:toSizeInMillimeter:")]
		void ScalePageInMillimeter (nuint pageIndex, CGSize size);

		[Export ("changeCropBoxForPageAtIndex:toRect:")]
		void ChangeCropBox (nuint pageIndex, CGRect rect);

		[Export ("changeMediaBoxForPageAtIndex:toRect:")]
		void ChangeMediaBox (nuint pageIndex, CGRect rect);

		[Export ("addNewPageAtIndex:configuration:")]
		void AddNewPage (nuint destinationPageIndex, [NullAllowed] PSPDFNewPageConfiguration newPageConfiguation);

		[Export ("modifyAnnotationsOfTypes:change:")]
		void ModifyAnnotations (PSPDFAnnotationType annotationTypes, PSPDFAnnotationChange annotationChange);

		[Export ("modifyAnnotations:change:error:")]
		bool ModifyAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationChange annotationChange, out NSError error);

		[Export ("mergeItem:onPage:")]
		void MergeItem (PSPDFProcessorItem item, nuint destinationPageIndex);

		[Export ("drawOnAllCurrentPages:")]
		void DrawOnAllCurrentPages (PSPDFRenderDrawHandler drawHandler);

		// PSPDFProcessorConfiguration (Metadata) Category

		[Export ("metadata")]
		NSDictionary<NSString, NSString> Metadata { get; }

		[Export ("updateMetadata:")]
		void UpdateMetadata (NSDictionary<NSString, NSString> metadata);

		[Export ("clearMetadata")]
		void ClearMetadata ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFProcessorItem {

		[Static]
		[Export ("processorItemWithImage:jpegCompressionQuality:builderBlock:")]
		PSPDFProcessorItem FromImage (UIImage image, nfloat jpegCompressionQuality, [NullAllowed] Action<PSPDFProcessorItemBuilder> builderHandler);

		[Static]
		[Export ("processorItemWithItemURL:builderBlock:")]
		PSPDFProcessorItem FromItemUrl (NSUrl itemUrl, [NullAllowed] Action<PSPDFProcessorItemBuilder> builderHandler);

		[Export ("image"), NullAllowed]
		UIImage Image { get; }

		[Export ("itemURL"), NullAllowed]
		NSUrl ItemUel { get; }

		[Export ("transform")]
		CGAffineTransform Transform { get; }

		[Export ("jpegCompressionQuality")]
		nfloat JpegCompressionQuality { get; }

		[Export ("alignment")]
		PSPDFRectAlignment Alignment { get; }

		[Export ("shouldUseAlignment")]
		bool ShouldUseAlignment { get; }

		[Export ("zPosition")]
		PSPDFItemZPosition ZPosition { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFProcessorItemBuilder {

		[Export ("transform", ArgumentSemantic.Assign)]
		CGAffineTransform Transform { get; set; }

		[Export ("alignment", ArgumentSemantic.Assign)]
		PSPDFRectAlignment Alignment { get; set; }

		[Export ("shouldUseAlignment", ArgumentSemantic.Assign)]
		bool ShouldUseAlignment { get; set; }

		[Export ("zPosition", ArgumentSemantic.Assign)]
		PSPDFItemZPosition ZPosition { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorSaveOptions {

		[Field ("PSPDFProcessorSaveOptionsKeyLengthAutomatic", "__Internal")]
		nuint KeyLengthAutomatic { get; }

		[Export ("initWithOwnerPassword:userPassword:keyLength:")]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, [NullAllowed] nuint keyLength);

		[Export ("initWithOwnerPassword:userPassword:keyLength:permissions:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, [NullAllowed] nuint keyLength, PSPDFDocumentPermissions documentPermissions);

		[Export ("ownerPassword"), NullAllowed]
		string OwnerPassword { get; }

		[Export ("userPassword"), NullAllowed]
		string UserPassword { get; }

		[Export ("keyLength")]
		nuint KeyLength { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }
	}

	interface IPSPDFSaveViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSaveViewControllerDelegate {

		[Abstract]
		[Export ("saveViewControllerDidEnd:shouldSave:")]
		void DidEnd (PSPDFSaveViewController controller, bool shouldSave);

		[Export ("saveViewControllerShouldSave:toPath:error:")]
		bool ShouldSave (PSPDFSaveViewController controller, string path, out NSError error);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	[DisableDefaultCtor]
	interface PSPDFSaveViewController {

		[Export ("initWithDocumentEditorConfiguration:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentEditorConfiguration configuration);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSaveViewControllerDelegate Delegate { get; set; }

		[Export ("fileName"), NullAllowed]
		string FileName { get; set; }

		[Export ("fullFilePath"), NullAllowed]
		string FullFilePath { get; }

		[Export ("showDirectoryPicker")]
		bool ShowDirectoryPicker { get; set; }

		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUD {

		[Static]
		[Export ("items")]
		PSPDFStatusHUDItem [] Items { get; }

		[Static]
		[Export ("popAllItemsAnimated:completion:")]
		void PopAllItems (bool animated, [NullAllowed] Action completion);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUDItem {

		[Export ("title"), NullAllowed]
		string Title { get; set; }

		[Export ("subtitle"), NullAllowed]
		string Subtitle { get; set; }

		[Export ("text"), NullAllowed]
		string Text { get; set; }

		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; set; }

		[Export ("view", ArgumentSemantic.Strong), NullAllowed]
		UIView View { get; set; }

		[Static]
		[Export ("progressWithText:")]
		PSPDFStatusHUDItem GetProgressHud ([NullAllowed] string text);

		[Static]
		[Export ("indeterminateProgressWithText:")]
		PSPDFStatusHUDItem GetIndeterminateProgressHud ([NullAllowed] string text);

		[Static]
		[Export ("successWithText:")]
		PSPDFStatusHUDItem GetSuccessHud ([NullAllowed] string text);

		[Static]
		[Export ("errorWithText:")]
		PSPDFStatusHUDItem GetErrorHud ([NullAllowed] string text);

		[Static]
		[Export ("itemWithText:image:")]
		PSPDFStatusHUDItem GetItemHud ([NullAllowed] string text, [NullAllowed] UIImage image);

		[Export ("setHUDStyle:")]
		void SetHudStyle (PSPDFStatusHUDStyle style);

		[Export ("pushAnimated:completion:")]
		void Push (bool animated, [NullAllowed] Action completionHandler);

		[Export ("pushAndPopWithDelay:animated:completion:")]
		void PushAndPop (double interval, bool animated, [NullAllowed] Action completionHandler);

		[Export ("popAnimated:completion:")]
		void Pop (bool animated, [NullAllowed] Action completionHandler);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFStatusHUDView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("item", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStatusHUDItem Item { get; set; }
	}

	[BaseType (typeof (PSPDFButton))]
	interface PSPDFColorButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("color", ArgumentSemantic.Strong), NullAllowed]
		UIColor Color { get; set; }

		[Export ("displayAsEllipse", ArgumentSemantic.Assign)]
		bool DisplayAsEllipse { get; set; }

		[Export ("borderWidth", ArgumentSemantic.Assign)]
		nfloat BorderWidth { get; set; }

		[Export ("indicatorSize", ArgumentSemantic.Assign)]
		nfloat IndicatorSize { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSpeechController {

		[Field ("PSPDFSpeechSynthesizerAutoDetectLanguage", "__Internal")]
		NSString AutoDetectLanguage { get; }

		[Field ("PSPDFSpeechSynthesizerLanguageKey", "__Internal")]
		NSString LanguageKey { get; }

		[Field ("PSPDFSpeechSynthesizerLanguageHintKey", "__Internal")]
		NSString LanguageHintKey { get; }

		[Export ("speakText:options:delegate:")]
		void Speak (string speechString, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] IAVSpeechSynthesizerDelegate aDelegate);

		[Export ("stopSpeakingForDelegate:")]
		bool StopSpeaking ([NullAllowed] IAVSpeechSynthesizerDelegate aDelegate);

		[Export ("speechSynthesizer", ArgumentSemantic.Strong), NullAllowed]
		AVSpeechSynthesizer SpeechSynthesizer { get; }

		[Export ("selectedLanguage")]
		NSString SelectedLanguage { get; set; }

		[Export ("languageCodes", ArgumentSemantic.Copy)]
		string [] LanguageCodes { get; }

		[Export ("speakRate", ArgumentSemantic.Assign)]
		float SpeakRate { get; set; }

		[Export ("pitchMultiplier", ArgumentSemantic.Assign)]
		float PitchMultiplier { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFParser {

		[Export ("initWithInputStream:documentProvider:")]
		IntPtr Constructor (NSInputStream inputStream, PSPDFDocumentProvider documentProvider);

		[Export ("parseWithError:")]
		[return: NullAllowed]
		PSPDFAnnotation [] Parse (out NSError error);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("parsing", ArgumentSemantic.Assign)]
		bool Parsing { [Bind ("isParsing")] get; }

		[Export ("parsingEnded", ArgumentSemantic.Assign)]
		bool ParsingEnded { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("inputStream", ArgumentSemantic.Strong), NullAllowed]
		NSInputStream InputStream { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFWriter {

		[Export ("writeAnnotations:toOutputStream:documentProvider:error:")]
		bool WriteAnnotations (PSPDFAnnotation [] annotations, NSOutputStream outputStream, [NullAllowed] PSPDFDocumentProvider documentProvider, out NSError error);
	}

	delegate NSInputStream PSPDFXFDFAnnotationProviderStreamHandler (PSPDFXFDFAnnotationProvider provider);

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFXFDFAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, NSUrl XfdfFileUrl);

		[Export ("fileURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl FileUrl { get; }

		[Export ("inputStream", ArgumentSemantic.Strong), NullAllowed]
		NSInputStream InputStream { get; }

		[Export ("outputStream", ArgumentSemantic.Strong), NullAllowed]
		NSOutputStream OutputStream { get; }

		[Export ("loadAllAnnotations")]
		void LoadAllAnnotations ();

		[Export ("createInputStreamBlock", ArgumentSemantic.Copy), NullAllowed]
		PSPDFXFDFAnnotationProviderStreamHandler CreateInputStreamHandler { get; set; }

		[Export ("createOutputStreamBlock", ArgumentSemantic.Copy), NullAllowed]
		PSPDFXFDFAnnotationProviderStreamHandler CreateOutputStreamHandler { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCryptor {

		[Export ("keyFromPassphrase:salt:")]
		NSData GetKey (string passphrase, string salt);

		[Export ("legacyKeyFromPassphrase:salt:")]
		NSData GetLegacyKey (string passphrase, string salt);

		[Export ("encryptFromURL:toURL:key:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, out NSError error);

		[Export ("decryptFromURL:toURL:key:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, out NSError error);

		[Export ("encryptFromURL:toURL:passphrase:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, out NSError error);

		[Export ("decryptFromURL:toURL:passphrase:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, out NSError error);

		[Export ("fileManager", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFFileManager FileManager { get; set; }
	}

	interface IPSPDFDataSink { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDataSink {
		
		[Abstract]
		[Export ("isFinished")]
		bool IsFinished { get; }

		[Abstract]
		[Export ("writeData:")]
		bool WriteData (NSData data);

		[Abstract]
		[Export ("finish")]
		bool Finish ();
	}

	interface IPSPDFDataProvider { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDataProvider : INSSecureCoding {

		[Abstract]
		[Export ("data"), NullAllowed]
		NSData Data { get; }

		[Abstract]
		[Export ("size")]
		ulong Size { get; }

		[Abstract]
		[Export ("UID")]
		string Uid { get; }

		[Abstract]
		[Export ("additionalOperationsSupported")]
		PSPDFDataProviderAdditionalOperations AdditionalOperationsSupported { get; }

		[Abstract]
		[Export ("readDataWithSize:atOffset:")]
		NSData ReadData (ulong size, ulong offset);

		[return: NullAllowed]
		[Export ("createDataSinkWithOptions:error:")]
		IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, out NSError error);

		[Export ("replaceWithDataSink:")]
		bool Replace (IPSPDFDataSink replacementDataSink);

		[Export ("deleteDataWithError:")]
		bool Delete (out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAESCryptoDataProvider : PSPDFDataProvider {

		[Export ("initWithURL:passphraseProvider:salt:rounds:")]
		IntPtr Constructor (NSUrl url, Func<NSString> passphraseProvider, string salt, nuint rounds);

		[Export ("initWithURL:passphraseDataProvider:salt:rounds:")]
		IntPtr Constructor (NSUrl url, Func<NSData> passphraseDataProvider, NSData saltData, nuint rounds);

		[Export ("initWithURL:passphraseProvider:")][Internal]
		IntPtr InitWithURL (NSUrl url, Func<NSString> passphraseProvider);

		[Export ("initWithLegacyFileFormatURL:passphraseProvider:")][Internal]
		IntPtr InitWithLegacyFileFormatURL (NSUrl url, Func<NSString> passphraseProvider);

		[Export ("initWithURL:binaryKeyProvider:")]
		IntPtr Constructor (NSUrl url, Func<NSData> binaryKeyProvider);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormParser {

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("forms", ArgumentSemantic.Copy)]
		PSPDFFormElement [] Forms { get; set; }

		[Export ("formFields"), NullAllowed]
		PSPDFFormField [] FormFields { get; }

		[Export ("dirtyForms", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormElement [] DirtyForms { get; }

		[Export ("findAnnotationWithFieldName:")]
		[return: NullAllowed]
		PSPDFFormElement FindAnnotation (string fieldName);

		[Export ("findFieldWithFullFieldName:")]
		PSPDFFormField FindField (string fullFieldName);
	}

	[BaseType (typeof (PSPDFWidgetAnnotation))]
	interface PSPDFFormElement {

		[Export ("formField", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFormField FormField { get; }

		[Export ("resettable", ArgumentSemantic.Assign)]
		bool Resettable { [Bind ("isResettable")] get; }

		[Export ("defaultValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject DefaultValue { get; }

		[Export ("exportValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject ExportValue { get; }

		[Export ("highlightColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor HighlightColor { get; set; }

		[Export ("next", ArgumentSemantic.Weak), NullAllowed]
		PSPDFFormElement Next { get; set; }

		[Export ("previous", ArgumentSemantic.Weak), NullAllowed]
		PSPDFFormElement Previous { get; set; }

		[Export ("calculationOrderIndex", ArgumentSemantic.Assign)]
		nuint CalculationOrderIndex { get; }

		[Export ("readOnly", ArgumentSemantic.Assign), New]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("required", ArgumentSemantic.Assign)]
		bool Required { [Bind ("isRequired")] get; }

		[Export ("noExport", ArgumentSemantic.Assign)]
		bool NoExport { [Bind ("isNoExport")] get; }

		[Export ("fieldName"), NullAllowed]
		string FieldName { get; }

		[Export ("fullyQualifiedFieldName"), NullAllowed]
		string FullyQualifiedFieldName { get; }

		[Export ("formTypeName")]
		string FormTypeName { get; }

		// PSPDFFormElement (Fonts) Category

		[Export ("maxLength", ArgumentSemantic.Assign)]
		nuint MaxLength { get; set; }

		[Export ("isMultiline", ArgumentSemantic.Assign)]
		bool IsMultiline { get; set; }

		// PSPDFFormElement (Drawing) Category

		[Export ("drawHighlightInContext:options:multiply:")]
		void DrawHighlightInContext (CGContext context, [NullAllowed] NSDictionary options, bool shouldMultiply);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormRequest {

		[Export ("initWithFormat:values:request:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFSubmitFormActionFormat format, NSDictionary<NSString, NSObject> values, NSUrlRequest request);

		[Export ("submissionFormat")]
		PSPDFSubmitFormActionFormat SubmissionFormat { get; }

		[Export ("formValues")]
		NSDictionary<NSString, NSObject> FormValues { get; }

		[Export ("request")]
		NSUrlRequest Request { get; }
	}

	interface IPSPDFFormSubmissionDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormSubmissionDelegate {

		[Export ("formSubmissionController:shouldSubmitFormRequest:")]
		bool ShouldSubmitFormRequest (NSObject formSubmissionController, PSPDFFormRequest formRequest);

		[Export ("formSubmissionController:willSubmitFormValues:")]
		void WillSubmitFormValues (NSObject formSubmissionController, PSPDFFormRequest formRequest);

		[Export ("formSubmissionController:didReceiveResponseData:")]
		void DidReceiveResponseData (NSObject formSubmissionController, NSData responseData);

		[Export ("formSubmissionController:didFailWithError:")]
		void DidFail (NSObject formSubmissionController, NSError error);

		[Export ("formSubmissionControllerShouldPresentResponseInWebView:")]
		bool FormSubmissionControllerShouldPresentResponseInWebView (NSObject formSubmissionController);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFButtonFormElement {

		[Export ("selected", ArgumentSemantic.Assign)]
		bool Selected { [Bind ("isSelected")] get; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormOption [] Options { get; }

		[Export ("onState"), NullAllowed]
		string OnState { get; }

		[Export ("buttonFormField"), NullAllowed]
		PSPDFButtonFormField ButtonFormField { get; }

		[Export ("select")]
		void Select ();

		[Export ("deselect")]
		void Deselect ();

		[Export ("toggleButtonSelectionState")]
		bool ToggleButtonSelectionState ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFFormField))]
	interface PSPDFButtonFormField {

		[Export ("isPushButton", ArgumentSemantic.Assign)]
		bool IsPushButton { get; }

		[Export ("isCheckBox", ArgumentSemantic.Assign)]
		bool IsCheckBox { get; }

		[Export ("isRadioButton", ArgumentSemantic.Assign)]
		bool IsRadioButton { get; }

		[Export ("selectedAnnotationObjectNumbers")]
		NSNumber [] SelectedAnnotationObjectNumbers { get; set; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormOption [] Options { get; }

		[return: NullAllowed]
		[Export ("onStateForButton:")]
		string OnStateForButton (PSPDFWidgetAnnotation annotation);

		[Export ("toggleButton:")]
		void ToggleButton (PSPDFWidgetAnnotation annotation);

		[Export ("isSelected:")]
		bool IsSelected (PSPDFWidgetAnnotation annotation);

		[Export ("selectButton:")]
		void SelectButton (PSPDFWidgetAnnotation annotation);

		[Export ("deselectButton:")]
		void DeselectButton (PSPDFWidgetAnnotation annotation);

		[return: NullAllowed]
		[Export ("valueForButton:")]
		string GetValueForButton (PSPDFWidgetAnnotation annotation);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFChoiceFormElement {

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormOption [] Options { get; }

		[Export ("selectedIndices", ArgumentSemantic.Copy), NullAllowed]
		NSIndexSet SelectedIndices { get; set; }

		[Export ("selectedOptions", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormOption [] SelectedOptions { get; }

		[Export ("customSelection", ArgumentSemantic.Assign)]
		bool CustomSelection { get; }

		[Export ("topIndex", ArgumentSemantic.Assign)]
		nuint TopIndex { get; }

		[Export ("customText"), NullAllowed]
		string CustomText { get; set; }

		[Export ("choiceFormField"), NullAllowed]
		PSPDFChoiceFormField ChoiceFormField { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFFormField))]
	interface PSPDFChoiceFormField {

		[Export ("isCombo", ArgumentSemantic.Assign)]
		bool IsCombo { get; }

		[Export ("isEdit", ArgumentSemantic.Assign)]
		bool IsEdit { get; }

		[Export ("isMultiSelect", ArgumentSemantic.Assign)]
		bool IsMultiSelect { get; }

		[Export ("commitOnSelChange", ArgumentSemantic.Assign)]
		bool CommitOnSelChange { get; }

		[Export ("doNotSpellCheck", ArgumentSemantic.Assign)]
		bool DoNotSpellCheck { get; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		PSPDFFormOption [] Options { get; }

		[Export ("selectedIndices", ArgumentSemantic.Copy), NullAllowed]
		NSIndexSet SelectedIndices { get; set; }

		[Export ("customSelection", ArgumentSemantic.Assign)]
		bool CustomSelection { get; }

		[Export ("customText"), NullAllowed]
		string CustomText { get; set; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFSignatureFormElement {

		[Export ("isSigned")]
		bool IsSigned { get; }

		[Export ("signatureInfo", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSignatureInfo SignatureInfo { get; set; }

		[Export ("overlappingInkSignature"), NullAllowed]
		PSPDFInkAnnotation OverlappingInkSignature { get; }

		// PSPDFSignatureFormElement (SubclassingHooks) Category

		[Export ("drawArrowWithText:andColor:inContext:")]
		void DrawArrowWithText (string text, UIColor color, CGContext context);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignaturePropBuildEntry {

		[Export ("name"), NullAllowed]
		string Name { get; }

		[Export ("date"), NullAllowed]
		string Date { get; }

		[Export ("R", ArgumentSemantic.Assign)]
		nint R { get; }

		[Export ("OS"), NullAllowed]
		string OS { get; }

		[Export ("preRelease", ArgumentSemantic.Strong), NullAllowed]
		NSNumber PreRelease { get; }

		[Export ("nonEFontNoWarn", ArgumentSemantic.Strong), NullAllowed]
		NSNumber NonEFontNoWarn { get; }

		[Export ("trustedMode", ArgumentSemantic.Strong), NullAllowed]
		NSNumber TrustedMode { get; }

		[Export ("V", ArgumentSemantic.Assign)]
		nint V { get; }

		[Export ("REx"), NullAllowed]
		string REx { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignaturePropBuild {

		[Export ("filter", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSignaturePropBuildEntry Filter { get; }

		[Export ("pubSec", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSignaturePropBuildEntry PubSec { get; }

		[Export ("app", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSignaturePropBuildEntry App { get; }

		[Export ("sigQ", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSignaturePropBuildEntry SigQ { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignatureInfo {

		[Export ("placeholderBytes", ArgumentSemantic.Assign)]
		nuint PlaceholderBytes { get; }

		[Export ("contents", ArgumentSemantic.Copy), NullAllowed]
		NSData Contents { get; }

		[Export ("byteRange", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] ByteRange { get; }

		[Export ("filter"), NullAllowed]
		string Filter { get; }

		[Export ("subFilter"), NullAllowed]
		string SubFilter { get; }

		[Export ("name"), NullAllowed]
		string Name { get; }

		[Export ("creationDate", ArgumentSemantic.Copy), NullAllowed]
		NSDate CreationDate { get; }

		[Export ("reason"), NullAllowed]
		string Reason { get; }

		[Export ("propBuild", ArgumentSemantic.Copy), NullAllowed]
		PSPDFSignaturePropBuild PropBuild { get; }

		[Export ("references", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] References { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFTextFieldFormElement {

		[Export ("multiline")]
		bool Multiline { [Bind ("isMultiline")] get; }

		[Export ("password")]
		bool Password { [Bind ("isPassword")] get; }

		[Export ("textFieldChangedWithContents:change:range:isFinal:application:error:")]
		[return: NullAllowed]
		string TextFieldChanged (string contents, string change, NSRange range, bool isFinal, [NullAllowed] NSObject application, out NSError validationError);

		[Export ("formattedContents"), NullAllowed]
		string FormattedContents { get; }

		[Export ("inputFormat", ArgumentSemantic.Assign)]
		PSPDFTextInputFormat InputFormat { get; }

		[Export ("textFormField", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextFormField TextFormField { get; }
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFButtonFormElementView {

	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFChoiceFormElementView {

		[Export ("prepareChoiceEditorController")]
		UIViewController PrepareChoiceEditorController ();
	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFFormElementView : PSPDFFormInputAccessoryViewDelegate {

	}

	interface IPSPDFFormInputAccessoryViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormInputAccessoryViewDelegate {

		[Export ("doneButtonPressedOnFormInputView:")]
		[Abstract]
		void DoneButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Export ("previousButtonPressedOnFormInputView:")]
		[Abstract]
		void PreviousButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Export ("nextButtonPressedOnFormInputView:")]
		[Abstract]
		void NextButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Export ("clearButtonPressedOnFormInputView:")]
		[Abstract]
		void ClearButtonPressed (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnablePreviousButton:")]
		[Abstract]
		bool ShouldEnablePreviousButton (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnableNextButton:")]
		[Abstract]
		bool ShouldEnableNextButton (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnableClearButton:")]
		[Abstract]
		bool ShouldEnableClearButton (PSPDFFormInputAccessoryView inputView);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFFormInputAccessoryView {
		[Export ("displayDoneButton")]
		bool DisplayDoneButton { get; set; }

		[Export ("displayClearButton")]
		bool DisplayClearButton { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFFormInputAccessoryViewDelegate Delegate { get; set; }

		[Export ("updateToolbar")]
		void UpdateToolbar ();

		// Category SubclassingHooks

		[Export ("nextButton", ArgumentSemantic.Strong)]
		UIBarButtonItem NextButton { get; }

		[Export ("prevButton", ArgumentSemantic.Strong)]
		UIBarButtonItem PrevButton { get; }

		[Export ("doneButton", ArgumentSemantic.Strong)]
		UIBarButtonItem DoneButton { get; }

		[Export ("clearButton", ArgumentSemantic.Strong)]
		UIBarButtonItem ClearButton { get; }

		[Export ("nextButtonPressed:")]
		void NextButtonPressed ([NullAllowed] NSObject sender);

		[Export ("prevButtonPressed:")]
		void PrevButtonPressed ([NullAllowed] NSObject sender);

		[Export ("doneButtonPressed:")]
		void DoneButtonPressed ([NullAllowed] NSObject sender);

		[Export ("clearButtonPressed:")]
		void ClearButtonPressed ([NullAllowed] NSObject sender);
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFTextFieldFormElementView {

		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		[Export ("textView", ArgumentSemantic.Strong), NullAllowed]
		UITextView TextView { get; }

		[Export ("textField", ArgumentSemantic.Strong), NullAllowed]
		UITextField TextField { get; set; }

		[Export ("isMultiline")]
		bool IsMultiline { get; }

		[Export ("isPassword")]
		bool IsPassword { get; }

		[Export ("editMode")]
		bool EditMode { get; set; }

		[Export ("resizableView", ArgumentSemantic.Weak), NullAllowed]
		PSPDFResizableView ResizableView { get; set; }

		// PSPDFTextFieldFormElementView (SubclassingHooks) Category

		[Export ("setTextViewForEditing", ArgumentSemantic.Strong)]
		UITextView SetTextViewForEditing { get; }
	}

	delegate void PSPDFPKCS12UnlockHandler (PSPDFX509 x509, PSPDFRSAKey pk, NSError error);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFPKCS12 {

		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		[Export ("unlockWithPassword:done:")]
		void Unlock (string password, [NullAllowed] PSPDFPKCS12UnlockHandler done);
	}

	delegate void PSPDFPKCS12SignerSignHandler (bool success, PSPDFDocument document, NSError error);

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFSigner))]
	interface PSPDFPKCS12Signer {

		[Export ("initWithDisplayName:PKCS12:")]
		IntPtr Constructor (string name, PSPDFPKCS12 p12);

		[Export ("displayName")][New]
		string DisplayName { get; }

		[Export ("p12", ArgumentSemantic.Retain)]
		PSPDFPKCS12 P12 { get; }

		[Export ("pkey", ArgumentSemantic.Retain), NullAllowed]
		PSPDFRSAKey PKey { get; set; }

		[Export ("signFormElement:usingPassword:writeTo:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFPKCS12SignerSignHandler completion);
	}

	[PrivateDefaultCtorAttribute]
	[BaseType (typeof (NSObject))]
	interface PSPDFRSAKey {

		[Export ("initWithKey:")][Internal]
		IntPtr InitWithKey (IntPtr key);

		[Export ("key")]
		IntPtr Key { get; }
	}

	[PrivateDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureDigest {

		[Export ("initWithBIO:")][Internal]
		IntPtr InitWithBIO (IntPtr bio);

		[Export ("bio")]
		IntPtr Bio { get; }

		[Export ("digestRange:fileHandle:")]
		void DigestRange (NSRange range, NSFileHandle fileHandle);

		[Export ("digestData:")]
		void DigestData (NSData data);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureManager {

		[Export ("registeredSigners")]
		PSPDFSigner [] RegisteredSigners { get; }

		[Export ("registerSigner:")]
		void RegisterSigner (PSPDFSigner signer);

		[Export ("trustedCertificates")]
		PSPDFX509 [] TrustedCertificates { get; }

		[Export ("addTrustedCertificate:")]
		void AddTrustedCertificate (PSPDFX509 x509);

		[Export ("clearTrustedCertificates")]
		void ClearTrustedCertificates ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFSignedFormElementViewController {

		[Export ("initWithSignatureFormElement:")]
		IntPtr Constructor (PSPDFSignatureFormElement element);

		[Export ("formElement", ArgumentSemantic.Retain)]
		PSPDFSignatureFormElement FormElement { get; }

		[Export ("verifySignatureWithTrustedCertificates:error:")]
		PSPDFSignatureStatus VerifySignature ([NullAllowed] PSPDFX509 [] trustedCertificates, out NSError error);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSignedFormElementViewControllerDelegate Delegate { get; set; }
	}

	interface IPSPDFSignedFormElementViewControllerDelegate {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignedFormElementViewControllerDelegate {

		[Export ("signedFormElementViewController:removedSignatureFromDocument:")]
		void RemovedSignatureFromDocument (PSPDFSignedFormElementViewController controller, PSPDFDocument document);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSigner {

		[Export ("filter")]
		string Filter { get; }

		[Export ("subFilter")]
		string SubFilter { get; }

		[Export ("displayName")]
		string DisplayName { get; }

		[Export ("signingAlgorithm")]
		PSPDFSigningAlgorithm SigningAlgorithm { get; }

		[Export ("requestSigningCertificate:completionBlock:")]
		void RequestSigningCertificate (UINavigationController controller, [NullAllowed] Action<PSPDFX509, NSError> completionHandler);

		[Advice ("Requires call to base if overridden")]
		[Export ("signFormElement:withCertificate:writeTo:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 x509, string path, [NullAllowed] Action<bool, PSPDFDocument, NSError> done);

		[Export ("signHash:algorithm:error:"), Internal]
		NSData SignHash (NSData hash, PSPDFSigningAlgorithm algorithm, IntPtr error);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFUnsignedFormElementViewController {

		[DesignatedInitializer]
		[Export ("initWithSignatureFormElement:registeredSigners:")]
		IntPtr Constructor (PSPDFSignatureFormElement element, [NullAllowed] PSPDFSigner [] registeredSigners);

		[Export ("formElement", ArgumentSemantic.Strong)]
		PSPDFSignatureFormElement FormElement { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFUnsignedFormElementViewControllerDelegate Delegate { get; set; }

		[Export ("allowInkSignature", ArgumentSemantic.Assign)]
		bool AllowInkSignature { get; set; }
	}

	interface IPSPDFUnsignedFormElementViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFUnsignedFormElementViewControllerDelegate {

		[Export ("unsignedFormElementViewControllerRequestsInkSignature:")]
		[Abstract]
		void RequestsInkSignature (PSPDFUnsignedFormElementViewController controller);

		[Export ("unsignedFormElementViewController:signedDocument:error:")]
		void SignedDocument (PSPDFUnsignedFormElementViewController controller, PSPDFDocument document, NSError error);
	}

	[PrivateDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFX509 {

		[Static]
		[Export ("adobeCA")]
		PSPDFX509 AdobeCA { get; }

		[Static]
		[Export ("certificatesFromPKCS7Data:error:")]
		PSPDFX509 [] CertificatesFromPKCS7Data (NSData data, out NSError error);

		[Export ("initWithX509:")][Internal]
		IntPtr InitWithX509 (IntPtr x509);

		[Export ("cert")]
		IntPtr Cert { get; }

		[Export ("publicKey")]
		PSPDFRSAKey PublicKey { get; }

		[Export ("commonName")]
		string CommonName { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureStatus {

		[Export ("initWithSigner:signingDate:wasModified:")]
		IntPtr Constructor (string signer, NSDate date, bool wasModified);

		[Export ("reportSignatureProblem:")]
		void ReportSignatureProblem (int error);

		[Export ("signer")]
		string Signer { get; }

		[Export ("signingDate", ArgumentSemantic.Copy)]
		NSDate SigningDate { get; }

		[Export ("wasModified")]
		bool WasModified { get; }

		[Export ("problems")]
		string [] Problems { get; }

		[Export ("severity", ArgumentSemantic.Assign)]
		PSPDFSignatureStatusSeverity Severity { get; set; }

		[Export ("summary")]
		string Summary { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFDigitalSignatureReference {

		[Export ("transformMethod", ArgumentSemantic.Assign)]
		PSPDFDigitalSignatureReferenceTransformMethod TransformMethod { get; set; }

		[Export ("transformParams", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> TransformParams { get; set; }

		[Export ("digestMethod"), NullAllowed]
		string DigestMethod { get; set; }

		[Export ("digestValue"), NullAllowed]
		string DigestValue { get; set; }

		[Export ("digestLocation", ArgumentSemantic.Assign)]
		NSRange DigestLocation { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFGalleryConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFGalleryConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFGalleryConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFGalleryConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFGalleryConfigurationBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFGalleryConfiguration ConfigurationUpdated ([NullAllowed] Action<PSPDFGalleryConfigurationBuilder> builderHandler);

		[Export ("maximumConcurrentDownloads", ArgumentSemantic.Assign)]
		nuint MaximumConcurrentDownloads { get; }

		[Export ("maximumPrefetchDownloads", ArgumentSemantic.Assign)]
		nuint MaximumPrefetchDownloads { get; }

		[Export ("displayModeUserInteractionEnabled", ArgumentSemantic.Assign)]
		bool DisplayModeUserInteractionEnabled { get; }

		[Export ("fullscreenDismissPanThreshold", ArgumentSemantic.Assign)]
		nfloat FullscreenDismissPanThreshold { get; }

		[Export ("fullscreenZoomEnabled", ArgumentSemantic.Assign)]
		bool FullscreenZoomEnabled { [Bind ("isFullscreenZoomEnabled")] get; }

		[Export ("maximumFullscreenZoomScale", ArgumentSemantic.Assign)]
		nfloat MaximumFullscreenZoomScale { get; }

		[Export ("minimumFullscreenZoomScale", ArgumentSemantic.Assign)]
		nfloat MinimumFullscreenZoomScale { get; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; }

		[Export ("loopHUDEnabled", ArgumentSemantic.Assign)]
		bool LoopHUDEnabled { [Bind ("isLoopHUDEnabled")] get; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive", ArgumentSemantic.Assign)]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; }

		[Export ("allowPlayingMultipleInstances", ArgumentSemantic.Assign)]
		bool AllowPlayingMultipleInstances { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFGalleryConfigurationBuilder {

		[Export ("maximumConcurrentDownloads", ArgumentSemantic.Assign)]
		nuint MaximumConcurrentDownloads { get; set; }

		[Export ("maximumPrefetchDownloads", ArgumentSemantic.Assign)]
		nuint MaximumPrefetchDownloads { get; set; }

		[Export ("displayModeUserInteractionEnabled", ArgumentSemantic.Assign)]
		bool DisplayModeUserInteractionEnabled { get; set; }

		[Export ("fullscreenDismissPanThreshold", ArgumentSemantic.Assign)]
		nfloat FullscreenDismissPanThreshold { get; set; }

		[Export ("fullscreenZoomEnabled", ArgumentSemantic.Assign)]
		bool FullscreenZoomEnabled { get; set; }

		[Export ("maximumFullscreenZoomScale", ArgumentSemantic.Assign)]
		nfloat MaximumFullscreenZoomScale { get; set; }

		[Export ("minimumFullscreenZoomScale", ArgumentSemantic.Assign)]
		nfloat MinimumFullscreenZoomScale { get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { get; set; }

		[Export ("loopHUDEnabled", ArgumentSemantic.Assign)]
		bool LoopHudEnabled { get; set; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive", ArgumentSemantic.Assign)]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; set; }

		[Export ("allowPlayingMultipleInstances", ArgumentSemantic.Assign)]
		bool AllowPlayingMultipleInstances { get; set; }
	}

	interface IPSPDFGalleryContentViewLoading {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryContentViewLoading {

		[Export ("setProgress:")][Abstract]
		void SetProgress (nfloat progress);

		[Export ("progress")][Abstract]
		nfloat Progress ();

		[Export ("setHasUnspecifiedProgress:")]
		void SetHasUnspecifiedProgress (bool progress);

		[Export ("hasUnspecifiedProgress")]
		bool HasUnspecifiedProgress ();
	}

	interface IPSPDFGalleryContentViewError {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryContentViewError {

		[Export ("setError:")]
		[Abstract]
		void SetError ([NullAllowed] NSError error);

		[Export ("error")]
		[Abstract]
		NSError Error ();
	}

	interface IPSPDFGalleryContentViewCaption {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryContentViewCaption {

		[Export ("setCaption:")]
		[Abstract]
		void SetCaption ([NullAllowed] string caption);

		[Export ("caption")]
		[Abstract]
		string GetCaption ();
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFGalleryView {

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFGalleryViewDataSource DataSource { get; set; }

		[Export ("contentPadding", ArgumentSemantic.Assign)]
		nfloat ContentPadding { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed, New]
		IPSPDFGalleryViewDelegate Delegate { get; set; }

		[Export ("reload")]
		void Reload ();

		[Export ("contentViewForItemAtIndex:")]
		PSPDFGalleryContentView ContentViewForItem (nuint index);

		[Export ("itemIndexForContentView:")]
		nuint ItemIndexForContentView (PSPDFGalleryContentView contentView);

		[Export ("dequeueReusableContentViewWithIdentifier:")]
		PSPDFGalleryContentView DequeueReusableContentView (string identifier);

		[Export ("currentItemIndex", ArgumentSemantic.Assign)]
		nuint CurrentItemIndex { get; set; }

		[Export ("setCurrentItemIndex:animated:")]
		void SetCurrentItemIndex (nuint currentItemIndex, bool animated);

		[Export ("activeContentViews")]
		NSSet ActiveContentViews { get; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }
	}

	interface IPSPDFGalleryViewDataSource {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryViewDataSource {

		[Export ("numberOfItemsInGalleryView:")]
		[Abstract]
		nuint NumberOfItemsInGalleryView (PSPDFGalleryView galleryView);

		[Export ("galleryView:contentViewForItemAtIndex:")]
		[Abstract]
		PSPDFGalleryContentView ContentViewForItemAtIndex (PSPDFGalleryView galleryView, nuint index);
	}

	interface IPSPDFGalleryViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryViewDelegate : IUIScrollViewDelegate {

		[Export ("galleryView:willScrollToItemAtIndex:")]
		void WillScrollToItemAtIndex (PSPDFGalleryView galleryView, nuint index);

		[Export ("galleryView:didScrollToItemAtIndex:")]
		void DidScrollToItemAtIndex (PSPDFGalleryView galleryView, nuint index);

		[Export ("galleryView:willReuseContentView:")]
		void WillReuseContentView (PSPDFGalleryView galleryView, PSPDFGalleryContentView contentView);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFGalleryViewController : PSPDFOverridable, PSPDFMultimediaViewController {

		[DesignatedInitializer]
		[Export ("initWithLinkAnnotation:")]
		IntPtr Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("configuration", ArgumentSemantic.Strong)]
		PSPDFGalleryConfiguration Configuration { get; set; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFGalleryViewControllerState State { get; }

		[Export ("items", ArgumentSemantic.Copy), NullAllowed]
		PSPDFGalleryItem [] Items { get; }

		[Export ("linkAnnotation", ArgumentSemantic.Strong)]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("fullscreen", ArgumentSemantic.Assign)]
		new bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		[Export ("transitioning", ArgumentSemantic.Assign)]
		new bool Transitioning { [Bind ("isTransitioning")] get; set; }

		[Export ("setFullscreen:animated:")]
		new void SetFullscreen (bool fullscreen, bool animated);

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		new nfloat ZoomScale { get; set; }

		[Export ("singleTapGestureRecognizer", ArgumentSemantic.Strong)]
		UITapGestureRecognizer SingleTapGestureRecognizer { get; }

		[Export ("doubleTapGestureRecognizer", ArgumentSemantic.Strong)]
		UITapGestureRecognizer DoubleTapGestureRecognizer { get; }

		[Export ("panGestureRecognizer", ArgumentSemantic.Strong)]
		UIPanGestureRecognizer PanGestureRecognizer { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor ([NullAllowed] string reuseIdentifier);

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		UIView ContentView { get; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewLoading LoadingView { get; }

		[Export ("captionView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewCaption CaptionView { get; }

		[Export ("errorView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewError ErrorView { get; }

		[Export ("reuseIdentifier"), NullAllowed]
		string ReuseIdentifier { get; set; }

		[Export ("content", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryItem Content { get; set; }

		[Export ("shouldHideCaption", ArgumentSemantic.Assign)]
		bool ShouldHideCaption { get; set; }

		// PSPDFGalleryContentView (SubclassingHooks) Category

		[Static]
		[Export ("contentViewClass")]
		Class ContentViewClass { get; }

		[Static]
		[Export ("loadingViewClass")]
		Class LoadingViewClass { get; }

		[Static]
		[Export ("captionViewClass")]
		Class CaptionViewClass { get; }

		[Static]
		[Export ("errorViewClass")]
		Class ErrorViewClass { get; }

		[Export ("contentViewFrame")]
		CGRect ContentViewFrame { get; }

		[Export ("loadingViewFrame")]
		CGRect LoadingViewFrame { get; }

		[Export ("captionViewFrame")]
		CGRect CaptionViewFrame { get; }

		[Export ("errorViewFrame")]
		CGRect ErrorViewFrame { get; }

		[Export ("updateContentView")]
		void UpdateContentView ();

		[Advice ("Requires base call if overridden")]
		[Export ("updateCaptionView")]
		void UpdateCaptionView ();

		[Advice ("Requires base call if overridden")]
		[Export ("updateErrorView")]
		void UpdateErrorView ();

		[Advice ("Requires base call if overridden")]
		[Export ("updateLoadingView")]
		void UpdateLoadingView ();

		[Advice ("Requires base call if overridden")]
		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Advice ("Requires base call if overridden")]
		[Export ("contentDidChange")]
		void ContentDidChange ();

		[Advice ("Requires base call if overridden")]
		[Export ("updateSubviewVisibility")]
		void UpdateSubviewVisibility ();
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryWebItem {
	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryImageContentView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor (string reuseIdentifier);

		[Export ("zoomEnabled", ArgumentSemantic.Assign)]
		bool ZoomEnabled { [Bind ("isZoomEnabled")] get; set; }

		[Export ("maximumZoomScale", ArgumentSemantic.Assign)]
		nfloat MaximumZoomScale { get; set; }

		[Export ("minimumZoomScale", ArgumentSemantic.Assign)]
		nfloat MinimumZoomScale { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }

		[Export ("setZoomScale:animated:")]
		void SetZoomScale (nfloat zoomScale, bool animated);

		[Export ("content", ArgumentSemantic.Strong), NullAllowed][New]
		PSPDFGalleryImageItem Content { get; set; }

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed][New]
		UIImageView ContentView { get; }
	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryVideoContentView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor (string reuseIdentifier);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentCaptionView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("caption"), NullAllowed]
		string Caption { get; set; }

		[Export ("label", ArgumentSemantic.Strong), NullAllowed]
		UILabel Label { get; }

		[Export ("contentInset", ArgumentSemantic.Assign)]
		UIEdgeInsets ContentInset { get; set; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFMultimediaAnnotationView {

		[Export ("multimediaViewController", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFMultimediaViewController MultimediaViewController { get; }
	}

	[BaseType (typeof (PSPDFBlurView))]
	interface PSPDFGalleryEmbeddedBackgroundView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	[BaseType (typeof (PSPDFBlurView))]
	interface PSPDFGalleryFullscreenBackgroundView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFBlurView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("blurEnabled", ArgumentSemantic.Assign)]
		bool BlurEnabled { [Bind ("isBlurEnabled")] get; set; }

		[Export ("renderView", ArgumentSemantic.Weak)]
		UIView RenderView { get; set; }

		[Export ("containerView", ArgumentSemantic.Weak)]
		UIView ContainerView { get; set; }

		[Export ("blurEnabledObject", ArgumentSemantic.Strong), NullAllowed]
		NSNumber BlurEnabledObject { get; set; }
	}

	interface IPSPDFOverridable { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFOverridable {

		[Export ("classForClass:")]
		Class ClassForClass (Class originalClass);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContainerView {

		[Export ("initWithFrame:overrideDelegate:")]
		IntPtr Constructor (CGRect frame, [NullAllowed] IPSPDFOverridable overrideDelegate);

		[Export ("overrideDelegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOverridable OverrideDelegate { get; }

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewContentState ContentState { get; set; }

		[Export ("presentationMode", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewPresentationMode PresentationMode { get; set; }

		[Export ("galleryView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryView GalleryView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewLoading LoadingView { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryEmbeddedBackgroundView BackgroundView { get; set; }

		[Export ("fullscreenBackgroundView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryFullscreenBackgroundView FullscreenBackgroundView { get; set; }

		[Export ("statusHUDView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStatusHUDView StatusHudView { get; set; }

		[Export ("contentContainerView", ArgumentSemantic.Strong), NullAllowed]
		UIView ContentContainerView { get; }

		[Export ("presentStatusHUDWithTimeout:animated:")]
		void PresentStatusHud (double timeout, bool animated);

		[Export ("dismissStatusHUDAnimated:")]
		void DismissStatusHud (bool animated);
	}

	[BaseType (typeof (PSPDFGalleryContentView))]
	interface PSPDFGalleryWebContentView {

		[Export ("webView", ArgumentSemantic.Retain)][NullAllowed]
		UIView WebView { get; }
	}


	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryManifest {

		[Export ("initWithLinkAnnotation:")]
		IntPtr Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("linkAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("loadItemsWithCompletionBlock:")]
		void LoadItems ([NullAllowed] Action<PSPDFGalleryItem [], NSError> completionHandler);

		[Export ("cancel")]
		void Cancel ();

		[Export ("loading", ArgumentSemantic.Assign)]
		bool Loading { [Bind ("isLoading")] get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryVideoItem {

		[Export ("autoplayEnabled", ArgumentSemantic.Assign)]
		bool AutoplayEnabled { get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { get; set; }

		[Export ("preferredVideoQualities", ArgumentSemantic.Copy)]
		NSNumber [] PreferredVideoQualities { get; set; }

		[Export ("seekTime", ArgumentSemantic.Assign)]
		double SeekTime { get; set; }

		[Export ("startTime", ArgumentSemantic.Strong), NullAllowed]
		NSNumber StartTime { get; set; }

		[Export ("endTime", ArgumentSemantic.Strong), NullAllowed]
		NSNumber EndTime { get; set; }

		[Export ("coverMode", ArgumentSemantic.Assign)]
		PSPDFGalleryVideoItemCoverMode CoverMode { get; set; }

		[Export ("coverImageURL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl CoverImageUrl { get; set; }

		[Export ("coverPreviewCaptureTime", ArgumentSemantic.Strong), NullAllowed]
		NSNumber CoverPreviewCaptureTime { get; set; }

		[Export ("playableRange", ArgumentSemantic.Assign)]
		CMTimeRange PlayableRange { get; }

		[Export ("content"), NullAllowed][New]
		NSUrl Content { get; }
	}

	[Static]
	interface PSPDFGalleryOption {
		
		[Field ("PSPDFGalleryOptionAutoplay", "__Internal")]
		NSString Autoplay { get; }

		[Field ("PSPDFGalleryOptionControls", "__Internal")]
		NSString Controls { get; }

		[Field ("PSPDFGalleryOptionLoop", "__Internal")]
		NSString Loop { get; }

		[Field ("PSPDFGalleryOptionFullscreen", "__Internal")]
		NSString Fullscreen { get; }

		[Field ("PSPDFGalleryOptionCoverMode", "__Internal")]
		NSString CoverMode { get; }

		[Field ("PSPDFGalleryOptionCoverImage", "__Internal")]
		NSString CoverImage { get; }

		[Field ("PSPDFGalleryOptionCoverPreviewCaptureTime", "__Internal")]
		NSString CoverPreviewCaptureTime { get; }

		[Field ("PSPDFGalleryOptionPreferredVideoQualities", "__Internal")]
		NSString PreferredVideoQualities { get; }

		[Field ("PSPDFGalleryOptionStartTime", "__Internal")]
		NSString StartTime { get; }

		[Field ("PSPDFGalleryOptionEndTime", "__Internal")]
		NSString EndTime { get; }

		[Field ("PSPDFGalleryOptionCover", "__Internal")]
		NSString Cover { get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryImageItem {

		[Export ("content", ArgumentSemantic.Strong), NullAllowed][New]
		UIImage Content { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryItem {

		[Field ("PSPDFGalleryItemContentStateDidChangeNotification", "__Internal")]
		[Notification]
		NSString ContentStateDidChangeNotification { get; }

		[Export ("contentURL", ArgumentSemantic.Copy)]
		NSUrl ContentUrl { get; }

		[Export ("caption"), NullAllowed]
		string Caption { get; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; }

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFGalleryItemContentState ContentState { get; }

		[Export ("content", ArgumentSemantic.Strong), NullAllowed]
		NSObject Content { get; }

		[Export ("validContent", ArgumentSemantic.Assign)]
		bool ValidContent { [Bind ("hasValidContent")] get; }

		[Export ("error", ArgumentSemantic.Strong), NullAllowed]
		NSError Error { get; }

		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; }

		[Static]
		[Export ("itemsFromJSONData:error:")]
		[return: NullAllowed]
		PSPDFGalleryItem [] FromJsonData (NSData data, out NSError error);

		[Static]
		[Export ("itemFromLinkAnnotation:error:")]
		PSPDFGalleryItem FromLinkAnnotation (PSPDFLinkAnnotation annotation, out NSError error);

		[Export ("initWithDictionary:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSDictionary<NSString, NSObject> dictionary);

		[Export ("initWithContentURL:caption:options:")]
		IntPtr Constructor (NSUrl contentUrl, [NullAllowed] string caption, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("controlsEnabled", ArgumentSemantic.Assign)]
		bool ControlsEnabled { get; set; }

		[Export ("fullscreenEnabled", ArgumentSemantic.Assign)]
		bool FullscreenEnabled { [Bind ("isFullscreenEnabled")] get; set; }
	}

	interface IPSPDFAvoidingScrollViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAvoidingScrollViewDelegate : IUIScrollViewDelegate {

		[Export ("scrollViewShouldAvoidKeyboard:")]
		bool ShouldAvoidKeyboard (PSPDFAvoidingScrollView scrollView);
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFAvoidingScrollView {

		[Export ("avoidingKeyboard", ArgumentSemantic.Assign)]
		bool AvoidingKeyboard { [Bind ("isAvoidingKeyboard")] get; }

		[Export ("isHalfModalVisible", ArgumentSemantic.Assign)]
		bool IsHalfModalVisible { get; }

		[Export ("firstResponderIsTextInput", ArgumentSemantic.Assign)]
		bool FirstResponderIsTextInput { get; }

		[Export ("enableAvoidance", ArgumentSemantic.Assign)]
		bool EnableAvoidance { get; set; }
	}

	[Static]
	interface PSPDFGalleryItemType {
		
		[Field ("PSPDFGalleryItemTypeKey", "__Internal")]
		NSString Key { get; }

		[Field ("PSPDFGalleryItemContentURLKey", "__Internal")]
		NSString ContentUrlKey { get; }

		[Field ("PSPDFGalleryItemCaptionKey", "__Internal")]
		NSString CaptionKey { get; }

		[Field ("PSPDFGalleryItemOptionsKey", "__Internal")]
		NSString OptionsKey { get; }
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryUnknownItem {

	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFHostingAnnotationView : PSPDFRenderTaskDelegate {

		[Export ("annotationImageView", ArgumentSemantic.Strong)]
		UIImageView AnnotationImageView { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFColorPreset {

		[Static]
		[Export ("presetWithColor:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color);

		[Static]
		[Export ("presetWithColor:fillColor:alpha:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color, [NullAllowed] UIColor fillColor, nfloat alpha);

		[Export ("color"), NullAllowed]
		UIColor Color { get; }

		[Export ("colorWithAlpha"), NullAllowed]
		UIColor ColorWithAlpha { get; }

		[Export ("fillColor"), NullAllowed]
		UIColor FillColor { get; }

		[Export ("fillColorWithAlpha"), NullAllowed]
		UIColor FillColorWithAlpha { get; }

		[Export ("alpha")]
		nfloat Alpha { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerCoverView {

		[Export ("playButtonColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor PlayButtonColor { get; set; }

		[Export ("playButtonImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage PlayButtonImage { get; set; }
	}

	interface IPSPDFMultimediaViewController { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMultimediaViewController {

		[Abstract]
		[Export ("fullscreen")]
		bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		[Abstract]
		[Export ("setFullscreen:animated:")]
		void SetFullscreen (bool fullscreen, bool animated);

		[Abstract]
		[Export ("transitioning")]
		bool Transitioning { [Bind ("isTransitioning")] get; set; }

		[Abstract]
		[Export ("zoomScale")]
		nfloat ZoomScale { get; set; }

		[Abstract]
		[Export ("overrideDelegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOverridable OverrideDelegate { get; set; }

		[Abstract]
		[Export ("performAction:")]
		void PerformAction (PSPDFAction action);

		[Export ("configure:")]
		void Configure (PSPDFConfiguration configuration);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStylusManager {

		[Field ("PSPDFStylusManagerConnectionStatusChangedNotification", "__Internal")]
		[Notification]
		NSString ConnectionStatusChangedNotification { get; }

		[Export ("automaticallyEnablesApplePencil", ArgumentSemantic.Assign)]
		bool AutomaticallyEnablesApplePencil { get; set; }

		[Export ("applePencilEnabled", ArgumentSemantic.Assign)]
		bool ApplePencilEnabled { [Bind ("isApplePencilEnabled")] get; set; }

		[Export ("currentDriverClass", ArgumentSemantic.Assign), NullAllowed]
		Class CurrentDriverClass { get; set; }

		[Export ("connectionStatus", ArgumentSemantic.Assign)]
		PSPDFStylusConnectionStatus ConnectionStatus { get; }

		[Export ("stylusName"), NullAllowed]
		string StylusName { get; }

		[Export ("availableDriverClasses", ArgumentSemantic.Copy)]
		NSOrderedSet AvailableDriverClasses { get; set; }

		[Export ("enableLastDriver")]
		bool EnableLastDriver ();

		[Export ("stylusController")]
		PSPDFStylusViewController StylusController { get; }

		[Export ("settingsControllerForCurrentDriver"), NullAllowed]
		UIViewController SettingsControllerForCurrentDriver { get; }

		[Export ("embeddedSizeForSettingsController")]
		CGSize EmbeddedSizeForSettingsController { get; }

		[Export ("buttonActionMapping", ArgumentSemantic.Assign)]
		NSDictionary<NSNumber, NSString> ButtonActionMapping { get; set; }

		[Export ("hasSettingsControllerForDriverClass:")]
		bool HasSettingsControllerForDriverClass ([NullAllowed] Class driver);

		[Export ("registerView:")]
		void RegisterView (UIView view);

		[Export ("unregisterView:")]
		void UnregisterView (UIView view);

		[Export ("driverAllowsClassification")]
		bool DriverAllowsClassification { get; }

		[Export ("touchInfoForTouch:")]
		IPSPDFStylusTouch TouchInfoForTouch (UITouch touch);

		[Export ("showsStatusHUDForConnectionStatusChanges", ArgumentSemantic.Assign)]
		bool ShowsStatusHudForConnectionStatusChanges { get; set; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFStylusDriverDelegate aDelegate);

		[Export ("removeDelegate:")]
		void RemoveDelegate (IPSPDFStylusDriverDelegate aDelegate);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFApplePencilDriver : PSPDFStylusDriver {

		//[Field ("PSPDFApplePencilDetectedNotification", "__Internal")]
		//[Notification]
		//NSString ApplePencilDetectedNotification { get; }

		//[Field ("PSPDFApplePencilDetectedChangedNotification", "__Internal")]
		//[Notification]
		//NSString ApplePencilDetectedChangedNotification { get; }

		[DesignatedInitializer]
		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFStylusDriverDelegate del);

		[Static]
		[Export ("detected", ArgumentSemantic.Assign)]
		bool Detected { [Bind ("wasDetected")] get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		new IPSPDFStylusDriverDelegate Delegate { get; set; }
	}

	interface IPSPDFStylusDriver { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStylusDriver {

		// Unfortunatelly is not possible to represent in Protocols so a hack is implemented in ApiEnhancements
		// - (instancetype)initWithDelegate:(id<PSPDFStylusDriverDelegate>)delegate;

		[Static, Abstract]
		[Export ("driverInfo")]
		NSDictionary<NSString, NSObject> DriverInfo { get; }

		[Abstract]
		[Export ("enableDriverWithOptions:error:")]
		void EnableDriver ([NullAllowed] NSDictionary<NSString, NSObject> options, out NSError error);

		[Abstract]
		[Export ("disableDriver")]
		void DisableDriver ();

		[Abstract]
		[Export ("connectedStylusInfo")]
		NSDictionary<NSString, NSObject> ConnectedStylusInfo { get; }

		[Abstract]
		[Export ("connectionStatus")]
		PSPDFStylusConnectionStatus ConnectionStatus { get; }

		[Abstract]
		[Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFStylusDriverDelegate Delegate { get; }

		[Export ("touchInfoForTouch:")]
		[return: NullAllowed]
		IPSPDFStylusTouch GetTouchInfo (UITouch touch);

		[Export ("settingsController")]
		[NullAllowed]
		UIViewController SettingsController { get; }

		[Export ("settingsControllerInfo")]
		[NullAllowed]
		NSDictionary<NSString, NSObject> SettingsControllerInfo { get; }

		[Export ("registerView:")]
		void RegisterView (UIView view);

		[Export ("unregisterView:")]
		void UnregisterView (UIView view);
	}

	[Static]
	interface PSPDFStylusDriverKeys {

		[Field ("PSPDFStylusDriverIdentifierKey", "__Internal")]
		NSString IdentifierKey { get; }

		[Field ("PSPDFStylusDriverNameKey", "__Internal")]
		NSString NameKey { get; }

		[Field ("PSPDFStylusDriverSDKNameKey", "__Internal")]
		NSString SdkNameKey { get; }

		[Field ("PSPDFStylusDriverSDKVersionKey", "__Internal")]
		NSString SdkVersionKey { get; }

		[Field ("PSPDFStylusDriverProtocolVersionKey", "__Internal")]
		NSString ProtocolVersionKey { get; }

		[Field ("PSPDFStylusDriverPriorityKey", "__Internal")]
		NSString PriorityKey { get; }

		[Field ("PSPDFStylusNameKey", "__Internal")]
		NSString StylusNameKey { get; }

		[Field ("PSPDFStylusSettingsEmbeddedSizeKey", "__Internal")]
		NSString StylusSettingsEmbeddedSizeKey { get; }

		[Field ("PSPDFStylusDriverProtocolVersion_1", "__Internal")]
		NSString ProtocolVersion_1 { get; }
	}

	[Static]
	interface PSPDFStylusButtonAction {

		[Field ("PSPDFStylusButtonActionUndo", "__Internal")]
		NSString UndoKey { get; }

		[Field ("PSPDFStylusButtonActionRedo", "__Internal")]
		NSString RedoKey { get; }

		[Field ("PSPDFStylusButtonActionInk", "__Internal")]
		NSString InkKey { get; }

		[Field ("PSPDFStylusButtonActionEraser", "__Internal")]
		NSString EraserKey { get; }
	}

	interface IPSPDFStylusDriverDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStylusDriverDelegate {

		[Export ("connectionStatusChanged")]
		[Abstract]
		void ConnectionStatusChanged ();

		[Export ("buttonFired:")]
		void ButtonFired (nuint buttonNumber);

		[Export ("classificationsDidChangeForTouches:")]
		void ClassificationsDidChangeForTouches (NSSet touches);

		[Export ("stylusTouchBegan:")]
		void StylusTouchBegan (NSSet touches);

		[Export ("stylusTouchMoved:")]
		void StylusTouchMoved (NSSet touches);

		[Export ("stylusTouchEnded:")]
		void StylusTouchEnded (NSSet touches);

		[Export ("stylusTouchCancelled:")]
		void StylusTouchCancelled (NSSet touches);

		[Export ("stylusSuggestsToDisableGestures")]
		void StylusSuggestsToDisableGestures ();

		[Export ("stylusSuggestsToEnableGestures")]
		void StylusSuggestsToEnableGestures ();
	}

	interface IPSPDFStylusTouch { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStylusTouch {

		[Export ("locationInView:")]
		CGPoint LocationInView (UIView view);

		[Export ("classification")]
		PSPDFStylusTouchClassification GetClassification ();

		[Export ("pressure")]
		nfloat GetPressure ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFStylusViewController {

		[Export ("initWithStylusManager:")]
		IntPtr Constructor (PSPDFStylusManager stylusManager);

		[Export ("selectedDriverClass", ArgumentSemantic.Strong), NullAllowed]
		Class SelectedDriverClass { get; set; }

		[Export ("stylusManager", ArgumentSemantic.Retain)]
		PSPDFStylusManager StylusManager { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFStylusViewControllerDelegate Delegate { get; set; }
	}

	interface IPSPDFStylusViewControllerDelegate {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStylusViewControllerDelegate {

		[Export ("stylusViewControllerDidUpdateSelectedType:")]
		[Abstract]
		void StylusViewControllerDidUpdateSelectedType (PSPDFStylusViewController stylusViewController);

		[Export ("stylusViewControllerDidTapSettingsButton:")]
		[Abstract]
		void StylusViewControllerDidTapSettingsButton (PSPDFStylusViewController stylusViewController);
	}

	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFStaticTableViewController {

	}

	[BaseType (typeof (UITableViewController))]
	interface PSPDFBaseTableViewController {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFMediaPlayerController {

		[Export ("initWithContentURL:")]
		IntPtr Constructor (NSUrl contentUrl);

		[Export ("contentURL", ArgumentSemantic.Copy)]
		NSUrl ContentUrl { get; }

		[Export ("contentError", ArgumentSemantic.Strong), NullAllowed]
		NSError ContentError { get; }

		[Export ("didFinishPlaying", ArgumentSemantic.Assign)]
		bool DidFinishPlaying { get; }

		[Export ("playing", ArgumentSemantic.Assign)]
		bool Playing { [Bind ("isPlaying")] get; }

		[Export ("externalPlaybackActive", ArgumentSemantic.Assign)]
		bool ExternalPlaybackActive { [Bind ("isExternalPlaybackActive")] get; }

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerControllerContentState ContentState { get; }

		[Export ("coverMode", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerCoverMode CoverMode { get; set; }

		[Export ("coverImageURL", ArgumentSemantic.Strong), NullAllowed]
		NSUrl CoverImageUrl { get; set; }

		[Export ("coverImagePreviewCaptureTime", ArgumentSemantic.Assign)]
		CMTime CoverImagePreviewCaptureTime { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFMediaPlayerControllerDelegate Delegate { get; set; }

		[Export ("shouldHideToolbar", ArgumentSemantic.Assign)]
		bool ShouldHideToolbar { get; set; }

		[Export ("didStartPlaying", ArgumentSemantic.Assign)]
		bool DidStartPlaying { get; }

		[Export ("tapGestureRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer TapGestureRecognizer { get; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { get; set; }

		[Export ("usesExternalPlaybackWhileExternalScreenIsActive", ArgumentSemantic.Assign)]
		bool UsesExternalPlaybackWhileExternalScreenIsActive { get; set; }

		[Export ("controlStyle", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerControlStyle ControlStyle { get; set; }

		[Export ("playableRange", ArgumentSemantic.Assign)]
		CMTimeRange PlayableRange { get; set; }

		[Export ("play")]
		void Play ();

		[Export ("pause")]
		void Pause ();

		[Static]
		[Export ("pauseAllInstances")]
		void PauseAllInstances ();

		[Export ("seekToTime:")]
		void SeekToTime (CMTime time);

		[Export ("setShouldHideToolbar:animated:")]
		void SetShouldHideToolbar (bool shouldHideToolbar, bool animated);

		// PSPDFMediaPlayerController (Advanced) Category

		[Export ("internalPlayer", ArgumentSemantic.Strong), NullAllowed]
		AVPlayer InternalPlayer { get; }
	}

	interface IPSPDFMediaPlayerControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMediaPlayerControllerDelegate {

		[Export ("mediaPlayerControllerShouldPauseOtherInstances:")]
		bool ShouldPauseOtherInstances (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidStartPlaying:")]
		void DidStartPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidPause:")]
		void DidPause (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidFinishPlaying:")]
		void DidFinishPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerController:externalPlaybackActiveDidChange:")]
		void ExternalPlaybackActiveDidChange (PSPDFMediaPlayerController controller, bool externalPlaybackActive);

		[Export ("mediaPlayerController:didHideToolbar:")]
		void DidHideToolbar (PSPDFMediaPlayerController controller, bool hidden);

		[Export ("mediaPlayerController:contentStateDidChange:")]
		void ContentStateDidChange (PSPDFMediaPlayerController controller, PSPDFMediaPlayerControllerContentState contentState);
	}

	delegate void PSPDFRemoteContentObjectDispositionHandler (NSUrlSessionAuthChallengeDisposition disposition, NSUrlCredential credential);
	delegate void PSPDFRemoteContentObjectAuthHandler (NSUrlAuthenticationChallenge challenge, [BlockCallback] PSPDFRemoteContentObjectDispositionHandler dispHandler);
	delegate NSObject PSPDFRemoteContentObjectTransformerHandler (NSUrl location);

	interface IPSPDFRemoteContentObject { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFRemoteContentObject {

		[Abstract]
		[Export ("URLRequestForRemoteContent"), NullAllowed]
		NSUrlRequest UrlRequestForRemoteContent { get; }

		[Abstract]
		[Export ("remoteContent", ArgumentSemantic.Assign), NullAllowed]
		NSObject RemoteContent { get; set; }

		[Export ("isLoadingRemoteContent")]
		bool GetLoadingRemoteContent ();

		[Export ("setLoadingRemoteContent:")]
		void SetLoadingRemoteContent (bool isLoadingRemoteContent);

		[Export ("remoteContentProgress")]
		nfloat GetRemoteContentProgress ();

		[Export ("setRemoteContentProgress:")]
		void SetRemoteContentProgress (nfloat remoteContentProgress);

		[Export ("remoteContentError")]
		NSError GetRemoteContentError ();

		[Export ("setRemoteContentError:")]
		void SetRemoteContentError ([NullAllowed] NSError remoteContentError);

		[Export ("shouldCacheRemoteContent", ArgumentSemantic.Assign)]
		bool ShouldCacheRemoteContent { get; }

		[Export ("shouldRetryLoadingRemoteContentOnConnectionFailure", ArgumentSemantic.Assign)]
		bool ShouldRetryLoadingRemoteContentOnConnectionFailure { get; }

		[Export ("remoteContentAuthenticationChallengeBlock")]
		PSPDFRemoteContentObjectAuthHandler RemoteContentAuthenticationChallengeHandler { get; }

		[Export ("remoteContentTransformerBlock")]
		PSPDFRemoteContentObjectTransformerHandler RemoteContentTransformerHandler { get; }

		[Export ("hasRemoteContent", ArgumentSemantic.Assign)]
		bool HasRemoteContent { get; }

		[Export ("completionBlock", ArgumentSemantic.Copy), NullAllowed]
		Action<PSPDFRemoteContentObject> CompletionHandler { get; set; }
	}	 

	interface IPSPDFApplication { }

	[Protocol]
	interface PSPDFApplication {

		[Export ("canOpenURL:")]
		bool CanOpenUrl (NSUrl url);

		[Export ("openURL:options:completionHandler:")]
		void OpenUrl (NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] Action<bool> completionHandler);

		[Export ("networkIndicatorManager")]
		IPSPDFNetworkActivityIndicatorManager GetNetworkIndicatorManager ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultApplication : PSPDFApplication {

	}

	[BaseType (typeof (PSPDFDefaultApplication))]
	interface PSPDFExtensionApplication : PSPDFApplication {

		[Export ("initWithExtensionContext:")]
		IntPtr Constructor (NSExtensionContext extensionContext);
	}

	interface IPSPDFNetworkActivityIndicatorManager { }

	[Protocol]
	interface PSPDFNetworkActivityIndicatorManager {

		[Export ("isEnabled")]
		bool GetIsEnabled ();

		[Export ("setEnabled:")]
		void SetIsEnabled (bool isEnabled);

		[Export ("isNetworkActivityIndicatorVisible")]
		bool GetIsNetworkActivityIndicatorVisible ();

		[Export ("incrementActivityCount")]
		void IncrementActivityCount ();

		[Export ("decrementActivityCount")]
		void DecrementActivityCount ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultNetworkActivityIndicatorManager : PSPDFNetworkActivityIndicatorManager {

		[Notification]
		[Field ("PSPDFNetworkActivityDidStartNotification", "__Internal")]
		NSString DidStartNotification { get; }

		[Notification]
		[Field ("PSPDFNetworkActivityDidFinishNotification", "__Internal")]
		NSString DidFinishNotification { get; }
	}

	interface IPSPDFApplicationPolicy { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFApplicationPolicy {

		[Export ("hasPermissionForEvent:isUserAction:")][Abstract]
		bool HasPermission (NSString aEvent, bool isUserAction);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultApplicationPolicy : PSPDFApplicationPolicy {

	}

	[Static]
	interface PSPDFPolicyEventKeys {

		[Field ("PSPDFPolicyEventOpenIn", "__Internal")]
		NSString OpenIn { get; }

		[Field ("PSPDFPolicyEventPrint", "__Internal")]
		NSString Print { get; }

		[Field ("PSPDFPolicyEventEmail", "__Internal")]
		NSString Email { get; }

		[Field ("PSPDFPolicyEventMessage", "__Internal")]
		NSString Message { get; }

		[Field ("PSPDFPolicyEventQuickLook", "__Internal")]
		NSString QuickLook { get; }

		[Field ("PSPDFPolicyEventAudioRecording", "__Internal")]
		NSString AudioRecording { get; }

		[Field ("PSPDFPolicyEventCamera", "__Internal")]
		NSString Camera { get; }

		[Field ("PSPDFPolicyEventPhotoLibrary", "__Internal")]
		NSString PhotoLibrary { get; }

		[Field ("PSPDFPolicyEventPasteboard", "__Internal")]
		NSString Pasteboard { get; }

		[Field ("PSPDFPolicyEventSubmitForm", "__Internal")]
		NSString SubmitForm { get; }

		[Field ("PSPDFPolicyEventNetwork", "__Internal")]
		NSString Network { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationSummarizer {

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Retain)]
		PSPDFDocument Document { get; }

		[Export ("annotationSummaryForPages:")]
		NSAttributedString GetAnnotationSummary (NSIndexSet pages);

		[Export ("temporaryTextFileURLForPages:error:")]
		[return: NullAllowed]
		NSUrl GetTemporaryTextFileUrl (NSIndexSet pages, out NSError error);

		[Static]
		[Export ("temporaryTextFileURLForDocuments:error:")]
		[return: NullAllowed]
		NSUrl GetTemporaryTextFileUrl (PSPDFDocument [] documents, out NSError error);

		[Static]
		[Export ("annotationSummaryForDocuments:")]
		NSAttributedString GetAnnotationSummary (PSPDFDocument [] documents);
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFAnnotationSetsCell : IUICollectionViewDelegate, IUICollectionViewDataSource {

		[Export ("annotations", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] Annotations { get; set; }

		[Export ("collectionView", ArgumentSemantic.Retain)]
		UICollectionView CollectionView { get; }

		[Export ("border", ArgumentSemantic.Assign)]
		nfloat Border { get; set; }

		[Export ("collectionViewUpdateBlock", ArgumentSemantic.Copy)]
		Action<PSPDFAnnotationSetsCell> CollectionViewUpdateHandler { get; set; }
	}

	interface IPSPDFBackForwardActionListDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionListDelegate {

		[Export ("backForwardList:requestedBackActionExecution:")][Abstract]
		void RequestedBackActionExecution (PSPDFBackForwardActionList list, PSPDFAction [] actions);

		[Export ("backForwardList:requestedForwardActionExecution:")][Abstract]
		void RequestedForwardActionExecution (PSPDFBackForwardActionList list, PSPDFAction [] actions);

		[Export ("backForwardListDidUpdate:")]
		void DidUpdate (PSPDFBackForwardActionList list);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionList {

		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFBackForwardActionListDelegate del);

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFBackForwardActionListDelegate Delegate { get; set; }

		[Export ("registerAction:")]
		void RegisterAction (PSPDFAction action);

		[Export ("requestBack")]
		void RequestBack ();

		[Export ("requestBackToAction:")]
		void RequestBack (PSPDFAction action);

		[Export ("requestForward")]
		void RequestForward ();

		[Export ("requestForwardToAction:")]
		void RequestForward (PSPDFAction action);

		[Export ("resetBackList")]
		void ResetBackList ();

		[Export ("resetForwardList")]
		void ResetForwardList ();

		[Export ("reset")]
		void Reset ();

		[Export ("backListCap", ArgumentSemantic.Assign)]
		nuint BackListCap { get; set; }

		[Export ("backAction", ArgumentSemantic.Retain), NullAllowed]
		PSPDFAction BackAction { get; }

		[Export ("forwardAction", ArgumentSemantic.Retain), NullAllowed]
		PSPDFAction ForwardAction { get; }

		[Export ("backList", ArgumentSemantic.Copy)]
		PSPDFAction [] BackList { get; }

		[Export ("forwardList", ArgumentSemantic.Copy)]
		PSPDFAction [] ForwardList { get; }
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFButton {

		[Export ("touchAreaInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets TouchAreaInsets { get; set; }

		[Export ("positionImageOnRight", ArgumentSemantic.Assign)]
		bool PositionImageOnRight { get; set; }

		[Export ("actionBlock", ArgumentSemantic.Copy)]
		Action<PSPDFButton> ActionHandler { get; set; }

		[Export ("setActionBlock:forControlEvents:")]
		void SetActionHandler ([NullAllowed] Action<PSPDFButton> action, UIControlEvent controlEvents);
	}

	[BaseType (typeof (PSPDFButton))]
	interface PSPDFBackForwardButton {

		[Static]
		[Export ("backButton")]
		PSPDFBackForwardButton BackButton { get; }

		[Static]
		[Export ("forwardButton")]
		PSPDFBackForwardButton ForwardButton { get; }

		[Export ("buttonStyle", ArgumentSemantic.Assign)]
		PSPDFBackButtonStyle ButtonStyle { get; set; }

		[Export ("blurEffectStyle", ArgumentSemantic.Assign)]
		UIBlurEffectStyle BlurEffectStyle { get; set; }

		[Export ("longPressRecognizer", ArgumentSemantic.Retain)]
		UILongPressGestureRecognizer LongPressRecognizer { get; }
	}

	[BaseType (typeof (UICollectionReusableView))]
	interface PSPDFCollectionReusableFilterView {

		[Export ("filterElement", ArgumentSemantic.Retain), NullAllowed]
		UISegmentedControl FilterElement { get; set; }

		[Export ("filterElementOffset", ArgumentSemantic.Assign)]
		CGPoint FilterElementOffset { get; set; }

		[Export ("minimumFilterMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets MinimumFilterMargin { get; set; }

		[Export ("backgroundStyle")]
		PSPDFCollectionReusableFilterViewStyle BackgroundStyle { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureValidator {

		[Export ("initWithSignatureFormElement:")]
		IntPtr Constructor (PSPDFSignatureFormElement formElement);

		[Export ("signatureFormElement")]
		PSPDFSignatureFormElement SignatureFormElement { get; }

		[Export ("verifySignatureWithTrustedCertificates:error:")]
		NSObject VerifySignature ([NullAllowed] PSPDFX509 [] trustedCertificates, out NSError error);
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFPagingScrollView {

	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFPageScrollViewController : IUIScrollViewDelegate {

		[Export ("initWithPresentationContext:")]
		IntPtr Constructor (IPSPDFPresentationContext presentationContext);

		[Export ("presentationContext", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFPresentationContext PresentationContext { get; set; }

		[Export ("pagingScrollView")]
		UIScrollView PagingScrollView { get; }

		[Export ("visiblePageIndexes")]
		NSOrderedSet<NSNumber> VisiblePageIndexes { get; }

		[Export ("pageViewForPageAtIndex:")]
		PSPDFPageView GetPageView (nuint pageIndex);

		[Export ("reloadData")]
		void ReloadData ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFTabbedBar {

		[Export ("barHeight")]
		nfloat BarHeight { get; set; }

		[Export ("tabbedBarStyle", ArgumentSemantic.Assign)]
		PSPDFTabbedBarStyle TabbedBarStyle { get; set; }

		[Export ("minTabWidth")]
		nfloat MinTabWidth { get; set; }

		[Export ("tabTitleFont", ArgumentSemantic.Assign), NullAllowed]
		UIFont TabTitleFont { get; set; }

		[Export ("closeButtonImage", ArgumentSemantic.Assign), NullAllowed]
		UIImage CloseButtonImage { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Assign), NullAllowed]
		UIView BackgroundView { get; set; }

		// @property (readonly, nonatomic) UIButton * _Nonnull documentPickerButton;
		[Export ("documentPickerButton")]
		UIButton DocumentPickerButton { get; }

		[Export ("overviewButton")]
		UIButton OverviewButton { get; }

		[Export ("overviewThreshold")]
		nint OverviewThreshold { get; set; }

		[Export ("leftViews", ArgumentSemantic.Copy)]
		UIView [] LeftViews { get; set; }

		[Export ("rightViews", ArgumentSemantic.Copy)]
		UIView [] RightViews { get; set; }

		[Export ("interactiveReorderingGestureRecognizer"), NullAllowed]
		UILongPressGestureRecognizer InteractiveReorderingGestureRecognizer { get; }
	}

	[Category]
	[BaseType (typeof (UISearchController))]
	interface UISearchController_PSPDFKitAdditions {
		
		[Export ("pspdf_searchResultsTableView")]
		UITableView Pspdf_SearchResultsTableView ();

		[Export ("pspdf_install352525StatusBarWorkaroundOn:")]
		void Pspdf_install352525StatusBarWorkaroundOn (UIViewController controller);
	}

	interface IPSPDFAppearanceModeManagerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAppearanceModeManagerDelegate {
		
		[Export ("appearanceManager:renderOptionsForMode:")]
		NSDictionary<NSString, NSObject> RenderOptions (PSPDFAppearanceModeManager manager, PSPDFAppearanceMode mode);

		[Export ("appearanceManager:applyAppearanceSettingsForMode:")]
		void ApplyAppearanceSettings (PSPDFAppearanceModeManager manager, PSPDFAppearanceMode mode);

		[Export ("appearanceManager:updateConfiguration:forMode:")]
		void UpdateConfiguration (PSPDFAppearanceModeManager manager, PSPDFConfigurationBuilder builder, PSPDFAppearanceMode mode);
	}

	interface PSPDFAppearanceModeManagerEventArgs {
		
		[Export ("PSPDFAppearanceModeChangedAnimatedKey")]
		NSNumber ChangedAnimated { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAppearanceModeManager {

		[Field ("PSPDFAppearanceModeChangedNotification", "__Internal")]
		[Notification (typeof (PSPDFAppearanceModeManagerEventArgs))]
		NSString AppearanceModeChangedNotification { get; }

		[Export ("appearanceMode", ArgumentSemantic.Assign)]
		PSPDFAppearanceMode AppearanceMode { get; set; }

		[Export ("setAppearanceMode:animated:")]
		void SetAppearanceMode (PSPDFAppearanceMode appearanceMode, bool animated);

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFAppearanceModeManagerDelegate Delegate { get; set; }
	}

	[Category]
	[BaseType (typeof (NSValue))]
	interface NSValue_PSPDFModel {
		
		[Static]
		[Export ("pspdf_valueWithDrawingPoint:")]
		NSValue FromDrawingPoint (PSPDFDrawingPoint point);

		[Export ("pspdf_drawingPointValue")]
		PSPDFDrawingPoint GetDrawingPoint ();
	}

	delegate void PSPDFUsernameHelperCompletionHandler (string userName);

	[BaseType (typeof (NSObject))]
	interface PSPDFUsernameHelper {

		[Field ("PSPDFUsernameHelperWillDismissAlertNotification", "__Internal")]
		[Notification]
		NSString WillDismissAlertNotification { get; }

		[Static]
		[Export ("isDefaultAnnotationUserNameSet")]
		bool IsDefaultAnnotationUserNameSet { get; }

		[Static]
		[NullAllowed]
		[Export ("defaultAnnotationUsername")]
		string DefaultAnnotationUsername { get; set; }

		[Static]
		[Export ("askForDefaultAnnotationUsernameIfNeeded:completionBlock:")]
		void AskForDefaultAnnotationUsernameIfNeeded (PSPDFViewController pdfViewController, PSPDFUsernameHelperCompletionHandler completionHandler);

		[Export ("askForDefaultAnnotationUsername:suggestedName:completionBlock:")]
		void AskForDefaultAnnotationUsername (UIViewController viewController, [NullAllowed] string suggestedName, PSPDFUsernameHelperCompletionHandler completionHandler);
	}

	[BaseType (typeof (UIActivityViewController))]
	interface PSPDFActivityViewController {

		[Export ("initWithActivityItems:applicationActivities:")]
		IntPtr Constructor (NSObject [] activityItems, UIActivity [] applicationActivities);
	}

	[Static]
	interface PSPDFRenderOption {

		[Field ("PSPDFRenderOptionPreserveAspectRatioKey", "__Internal")]
		NSString PreserveAspectRatioKey { get; }

		[Field ("PSPDFRenderOptionIgnoreDisplaySettingsKey", "__Internal")]
		NSString IgnoreDisplaySettingsKey { get; }

		[Field ("PSPDFRenderOptionPageColorKey", "__Internal")]
		NSString PageColorKey { get; }

		[Field ("PSPDFRenderOptionInvertedKey", "__Internal")]
		NSString InvertedKey { get; }

		[Field ("PSPDFRenderOptionFiltersKey", "__Internal")]
		NSString FiltersKey { get; }

		[Field ("PSPDFRenderOptionInterpolationQualityKey", "__Internal")]
		NSString InterpolationQualityKey { get; }

		[Field ("PSPDFRenderOptionSkipPageContentKey", "__Internal")]
		NSString SkipPageContentKey { get; }

		[Field ("PSPDFRenderOptionOverlayAnnotationsKey", "__Internal")]
		NSString OverlayAnnotationsKey { get; }

		[Field ("PSPDFRenderOptionSkipAnnotationArrayKey", "__Internal")]
		NSString SkipAnnotationArrayKey { get; }

		[Field ("PSPDFRenderOptionIgnorePageClipKey", "__Internal")]
		NSString IgnorePageClipKey { get; }

		[Field ("PSPDFRenderOptionAllowAntiAliasingKey", "__Internal")]
		NSString AllowAntiAliasingKey { get; }

		[Field ("PSPDFRenderOptionBackgroundFillColorKey", "__Internal")]
		NSString BackgroundFillColorKey { get; }

		[Field ("PSPDFRenderOptionInteractiveFormFillColorKey", "__Internal")]
		NSString InteractiveFormFillColorKey { get; }

		[Field ("PSPDFRenderOptionDrawBlockKey", "__Internal")]
		NSString DrawBlockKey { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderRequest : INSMutableCopying {

		[DesignatedInitializer]
		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong)]
		PSPDFDocument Document { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pageIndex")]
		nuint PageIndex { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pdfRect")]
		CGSize PdfRect { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("annotations", ArgumentSemantic.Copy), NullAllowed]
		PSPDFAnnotation [] Annotations { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("cachePolicy")]
		PSPDFRenderRequestCachePolicy CachePolicy { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("isEqualRenderRequest:")]
		bool IsEqualRenderRequest (PSPDFRenderRequest renderRequest);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFRenderRequest))]
	interface PSPDFMutableRenderRequest {

		[DesignatedInitializer]
		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);
	}

	interface IPSPDFRenderTaskDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderTaskDelegate {

		[Export ("renderTaskDidFinish:")]
		void DisFinish (PSPDFRenderTask task);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderTask {

		[DesignatedInitializer]
		[Export ("initWithRequest:")]
		IntPtr Constructor (PSPDFRenderRequest request);

		[Export ("request", ArgumentSemantic.Strong)]
		PSPDFRenderRequest Request { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFRenderTaskDelegate Delegate { get; set; }

		[Export ("completionHandler", ArgumentSemantic.Copy)]
		Action<UIImage> CompletionHandler { get; set; }

		[Export ("priority")]
		PSPDFRenderQueuePriority Priority { get; set; }

		[Export ("image", ArgumentSemantic.Strong), NullAllowed]
		UIImage Image { get; }

		[Export ("cancelled")]
		bool Cancelled { [Bind ("isCancelled")] get; }

		[Export ("cancel")]
		void Cancel ();

		[Static]
		[Export ("groupTasks:completionHandler:")]
		void GroupTasks (PSPDFRenderTask [] tasks, Action completionHandler);
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFBookmarkIndicatorButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("imageType")]
		PSPDFBookmarkIndicatorImageType ImageType { get; set; }

		[Export ("normalTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor NormalTintColor { get; set; }

		[Export ("selectedTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectedTintColor { get; set; }
	}

	interface IPSPDFAnalyticsClient { }

	[Protocol]
	interface PSPDFAnalyticsClient {

		[Export ("logEvent:attributes:")]
		void LogEvent (NSString @event, [NullAllowed] NSDictionary<NSString,NSObject> attributes);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnalytics : PSPDFAnalyticsClient {

		[Field ("PSPDFAnalyticsEventPrefix", "__Internal")]
		NSString EventPrefix { get; }

		[Export ("enabled")]
		bool Enabled { get; set; }

		[Export ("addAnalyticsClient:")]
		void AddAnalyticsClient (IPSPDFAnalyticsClient analyticsClient);

		[Export ("removeAnalyticsClient:")]
		void RemoveAnalyticsClient (IPSPDFAnalyticsClient analyticsClient);

		[Export ("logEvent:")]
		void LogEvent (NSString eventName);
	}

	[Static]
	interface PSPDFAnalyticsEventName {
		[Field ("PSPDFAnalyticsEventNameDocumentLoad", "__Internal")]
		NSString DocumentLoad { get; }

		[Field ("PSPDFAnalyticsEventNamePageChange", "__Internal")]
		NSString PageChange { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreationModeEnter", "__Internal")]
		NSString AnnotationCreationModeEnter { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreationModeExit", "__Internal")]
		NSString AnnotationCreationModeExit { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorDialogShow", "__Internal")]
		NSString AnnotationCreatorDialogShow { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorDialogCancel", "__Internal")]
		NSString AnnotationCreatorDialogCancel { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreatorSet", "__Internal")]
		NSString AnnotationCreatorSet { get; }

		[Field ("PSPDFAnalyticsEventNameTextSelect", "__Internal")]
		NSString TextSelect { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationSelect", "__Internal")]
		NSString AnnotationSelect { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationCreate", "__Internal")]
		NSString AnnotationCreate { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationDelete", "__Internal")]
		NSString AnnotationDelete { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineOpen", "__Internal")]
		NSString OutlineOpen { get; }

		[Field ("PSPDFAnalyticsEventNameThumbnailGridOpen", "__Internal")]
		NSString ThumbnailGridOpen { get; }

		[Field ("PSPDFAnalyticsEventNameDocumentEditorOpen", "__Internal")]
		NSString DocumentEditorOpen { get; }

		[Field ("PSPDFAnalyticsEventNameDocumentEditorAction", "__Internal")]
		NSString DocumentEditorAction { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineAnnotationSelect", "__Internal")]
		NSString OutlineAnnotationSelect { get; }

		[Field ("PSPDFAnalyticsEventNameOutlineElementSelect", "__Internal")]
		NSString OutlineElementSelect { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkAdd", "__Internal")]
		NSString BookmarkAdd { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkEdit", "__Internal")]
		NSString BookmarkEdit { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkRemove", "__Internal")]
		NSString BookmarkRemove { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkSort", "__Internal")]
		NSString BookmarkSort { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkRename", "__Internal")]
		NSString BookmarkRename { get; }

		[Field ("PSPDFAnalyticsEventNameBookmarkSelect", "__Internal")]
		NSString BookmarkSelect { get; }

		[Field ("PSPDFAnalyticsEventNameSearchStart", "__Internal")]
		NSString SearchStart { get; }

		[Field ("PSPDFAnalyticsEventNameSearchResultSelect", "__Internal")]
		NSString SearchResultSelect { get; }

		[Field ("PSPDFAnalyticsEventNameShare", "__Internal")]
		NSString Share { get; }

		[Field ("PSPDFAnalyticsEventNameToolbarMove", "__Internal")]
		NSString ToolbarMove { get; }

		[Field ("PSPDFAnalyticsEventNameAnnotationInspectorShow", "__Internal")]
		NSString AnnotationInspectorShow { get; }
	}

	[Static]
	interface PSPDFAnalyticsEventAttributeName {
		[Field ("PSPDFAnalyticsEventAttributeNameAnnotationType", "__Internal")]
		NSString AnnotationType { get; }

		[Field ("PSPDFAnalyticsEventAttributeNameAction", "__Internal")]
		NSString Action { get; }

		[Field ("PSPDFAnalyticsEventAttributeNameActivityType", "__Internal")]
		NSString ActivityType { get; }
	}

	[Static]
	interface PSPDFAnalyticsEventAttributeValue {
		[Field ("PSPDFAnalyticsEventAttributeValueActionInsertNewPage", "__Internal")]
		NSString ActionInsertNewPage { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRemoveSelectedPages", "__Internal")]
		NSString ActionRemoveSelectedPages { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionDuplicateSelectedPages", "__Internal")]
		NSString ActionDuplicateSelectedPages { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRotateSelectedPages", "__Internal")]
		NSString ActionRotateSelectedPages { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionExportSelectedPages", "__Internal")]
		NSString ActionExportSelectedPages { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionSelectAllPages", "__Internal")]
		NSString ActionSelectAllPages { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionUndo", "__Internal")]
		NSString ActionUndo { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueActionRedo", "__Internal")]
		NSString ActionRedo { get; }

		[Field ("PSPDFAnalyticsEventAttributeValueToolbarPosition", "__Internal")]
		NSString ToolbarPosition { get; }
	}

	delegate bool PSPDFLibraryFileSystemDataSourceDocumentHandler (PSPDFDocument document, out bool stop);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLibraryFileSystemDataSource : PSPDFLibraryDataSource {

		[Export ("initWithLibrary:documentsDirectoryURL:documentHandler:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFLibrary library, NSUrl documentsDirectoryUrl, [NullAllowed] PSPDFLibraryFileSystemDataSourceDocumentHandler documentHandler);

		[Export ("library", ArgumentSemantic.Weak)]
		PSPDFLibrary Library { get; }

		[Export ("documentsDirectoryURL")]
		NSUrl DocumentsDirectoryUrl { get; }

		[Export ("documentHandler"), NullAllowed]
		PSPDFLibraryFileSystemDataSourceDocumentHandler DocumentHandler { get; set; }

		[Export ("directoryEnumerationOptions", ArgumentSemantic.Assign)]
		NSDirectoryEnumerationOptions DirectoryEnumerationOptions { get; }

		[Export ("indexItemDescriptorForDocumentWithUID:")]
		PSPDFFileIndexItemDescriptor GetIndexItemDescriptor (string uid);

		[Export ("documentProvider", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFLibraryFileSystemDataSourceDocumentProvider DocumentProvider { get; }

		[Export ("explicitModeEnabled")]
		bool ExplicitModeEnabled { [Bind ("isExplicitModeEnabled")] get; }

		[Export ("didAddOrModifyDocumentAtURL:")]
		void DidAddOrModifyDocument (NSUrl url);

		[Export ("didRemoveDocumentAtURL:")]
		void DidRemoveDocument (NSUrl url);
	}

	interface IPSPDFLibraryFileSystemDataSourceDocumentProvider { }

	[Protocol][Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryFileSystemDataSourceDocumentProvider {

		[return: NullAllowed]
		[Export ("dataSource:documentWithUID:atURL:")]
		PSPDFDocument GetDocument (PSPDFLibraryFileSystemDataSource dataSource, string uid, NSUrl fileUrl);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSelectionState : INSSecureCoding {

		[Static]
		[return: NullAllowed]
		[Export ("stateForSelectionView:")]
		void GetState (PSPDFTextSelectionView selectionView);

		[Export ("UID")]
		string Uid { get; }

		[Export ("selectionPageIndex")]
		nuint SelectionPageIndex { get; }

		[Export ("selectedGlyphs")]
		PSPDFGlyph [] SelectedGlyphs { get; }

		[Export ("selectedImage")]
		PSPDFImageInfo SelectedImage { get; }

		[Export ("isEqualToSelectionState:")]
		bool IsEqualTo ([NullAllowed] PSPDFSelectionState selectionState);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileIndexItemDescriptor {

		[Export ("documentPath")]
		string DocumentPath { get; }

		[Export ("documentUID")]
		string DocumentUid { get; }

		[Export ("lastModificationDate")]
		NSDate LastModificationDate { get; }

		[Export ("isEqualToFileIndexItemDescriptor:")]
		bool IsEqualTo (PSPDFFileIndexItemDescriptor other);
	}

	interface IPSPDFImagePickerControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFImagePickerControllerDelegate {

		[Export ("imagePickerController:didFinishWithImage:andInfo:")]
		void DidFinish (PSPDFImagePickerController picker, UIImage image, NSDictionary <NSString, NSObject> info);
	}

	[BaseType (typeof (UIImagePickerController))]
	interface PSPDFImagePickerController {

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed, New]
		IPSPDFImagePickerControllerDelegate Delegate { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFFormField : PSPDFUndoProtocol, PSPDFJSONSerializing {

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFFormFieldType Type { get; }

		[Export ("name"), NullAllowed]
		string Name { get; }

		[Export ("fullyQualifiedName"), NullAllowed]
		string FullyQualifiedName { get; }

		[Export ("mappingName"), NullAllowed]
		string MappingName { get; }

		[Export ("alternateFieldName"), NullAllowed]
		string AlternateFieldName { get; }

		[Export ("isReadOnly", ArgumentSemantic.Assign)]
		bool IsReadOnly { get; }

		[Export ("isRequired", ArgumentSemantic.Assign)]
		bool IsRequired { get; }

		[Export ("isNoExport", ArgumentSemantic.Assign)]
		bool IsNoExport { get; }

		[Export ("defaultValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject DefaultValue { get; }

		[Export ("exportValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject ExportValue { get; }

		[Export ("value", ArgumentSemantic.Assign), NullAllowed]
		NSObject Value { get; set; }

		[Export ("calculationOrderIndex", ArgumentSemantic.Assign)]
		nuint CalculationOrderIndex { get; }

		[Export ("dirty", ArgumentSemantic.Assign)]
		bool Dirty { get; }

		[Export ("annotations")]
		PSPDFFormElement [] Annotations { get; }

		[Export ("nameForAnnotation:")]
		[return: NullAllowed]
		string GetNameForAnnotation (PSPDFFormElement annotation);

		[Export ("fullyQualifiedNameForAnnotation:")]
		[return: NullAllowed]
		string GetFullyQualifiedNameForAnnotation (PSPDFFormElement annotation);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormOption {

		[Export ("initWithLabel:value:")]
		[DesignatedInitializer]
		IntPtr Constructor (string label, string value);

		[Export ("label")]
		string Label { get; }

		[Export ("value")]
		string Value { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFPrintConfigurationBuilder {

		[Export ("printMode", ArgumentSemantic.Assign)]
		PSPDFPrintMode PrintMode { get; set; }

		[NullAllowed, Export ("defaultPrinter", ArgumentSemantic.Assign)]
		UIPrinter DefaultPrinter { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFPrintConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFPrintConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFPrintConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFPrintConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFPrintConfigurationBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFPrintConfiguration ConfigurationUpdated ([NullAllowed] Action<PSPDFPrintConfigurationBuilder> builderHandler);

		[Export ("printMode")]
		PSPDFPrintMode PrintMode { get; }

		[NullAllowed, Export ("defaultPrinter")]
		UIPrinter DefaultPrinter { get; }
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFTextFormField {

		[Export ("isMultiLine", ArgumentSemantic.Assign)]
		bool IsMultiLine { get; }

		[Export ("isPassword", ArgumentSemantic.Assign)]
		bool IsPassword { get; }

		[Export ("isComb", ArgumentSemantic.Assign)]
		bool IsComb { get; }

		[Export ("doNotScroll", ArgumentSemantic.Assign)]
		bool DoNotScroll { get; }

		[Export ("isRichText", ArgumentSemantic.Assign)]
		bool IsRichText { get; }

		[Export ("doNotSpellCheck", ArgumentSemantic.Assign)]
		bool DoNotSpellCheck { get; }

		[Export ("fileSelect", ArgumentSemantic.Assign)]
		bool FileSelect { get; }

		[Export ("text"), NullAllowed]
		string Text { get; set; }

		[Export ("richText"), NullAllowed]
		string RichText { get; set; }

		[Export ("maxLength", ArgumentSemantic.Assign)]
		nuint MaxLength { get; }
	}
}

