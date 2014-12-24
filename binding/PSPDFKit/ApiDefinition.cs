using System;

#if __UNIFIED__
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
#else
using MonoTouch.ObjCRuntime;
using MonoTouch.Foundation;
using MonoTouch.UIKit;
using MonoTouch.CoreGraphics;
using MonoTouch.AVFoundation;
using MonoTouch.CoreFoundation;
using MonoTouch.QuickLook;
using MonoTouch.MessageUI;
using MonoTouch.CoreAnimation;
using MonoTouch.CoreMedia;
using MonoTouch.MediaPlayer;
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
	[DisableDefaultCtor]
	[BaseType (typeof(NSObject), Name = "PSPDFKit")]
	interface PSPDFKitGlobal : IPSPDFSettings
	{
		[Field ("PSPDFXCallbackURLString", "__Internal")]
		NSString XCallbackUrlString { get; }

		[Field ("PSPDFMultimediaIdentifierKey", "__Internal")]
		NSString MultimediaIdentifierKey { get; }

		[Field ("PSPDFWebKitLegacyModeKey", "__Internal")]
		NSString WebKitLegacyModeKey { get; }

		[Static, Export ("sharedInstance")]
		PSPDFKitGlobal SharedInstance { get; }

		[Export ("setObject:forKeyedSubscript:")]
		void SetObject (NSObject obj, NSString key);

		[Static, Export ("setLicenseKey:")]
		void SetLicenseKey (string licenseKey);

		[Export ("version")]
		string Version { get; }

		[Export ("compiledAt", ArgumentSemantic.Copy)]
		NSDate CompiledAt { get; }

		[Static, Export ("isFeatureEnabled:")]
		bool IsFeatureEnabled (PSPDFFeatureMask feature);

		[Export ("securityAuditor", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFSecurityAuditor SecurityAuditor { get; }

		[Export ("fileManager", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFFileManager FileManager { get; }

		[Export ("cache", ArgumentSemantic.Strong), NullAllowed]
		PSPDFCache Cache { get; }

		[Export ("renderManager", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFRenderManager RenderManager { get; }

		[Export ("styleManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStyleManager StyleManager { get; }

		[Export ("speechSynthesizer", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSpeechController SpeechSynthesizer { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFModel))]
	interface PSPDFConfiguration : IPSPDFOverridable
	{
		[Static, Export ("defaultConfiguration")]
		PSPDFConfiguration DefaultConfiguration { get; }

		[Static, Export ("configurationWithBuilder:")]
		PSPDFConfiguration FromConfigurationBuilder (Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFConfiguration ConfigurationUpdated (Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFPageMode PageMode { get; }

		[Export ("thumbnailGrouping", ArgumentSemantic.Assign)]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; }

		[Export ("pageTransition", ArgumentSemantic.Assign)]
		PSPDFPageTransition PageTransition { get; }

		[Export ("doublePageModeOnFirstPage", ArgumentSemantic.Assign)]
		bool DoublePageModeOnFirstPage { [Bind ("isDoublePageModeOnFirstPage")] get; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; }

		[Export ("pageCurlDirectionLeftToRight", ArgumentSemantic.Assign)]
		bool PageCurlDirectionLeftToRight { [Bind ("isPageCurlDirectionLeftToRight")] get; }

		[Export ("fitToWidthEnabled", ArgumentSemantic.Assign)]
		bool FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; }

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

		[Export ("scrollDirection", ArgumentSemantic.Assign)]
		PSPDFScrollDirection ScrollDirection { get; }

		[Export ("shouldAutomaticallyAdjustScrollViewInsets", ArgumentSemantic.Assign)]
		bool ShouldAutomaticallyAdjustScrollViewInsets { get; }

		[Export ("alwaysBouncePages", ArgumentSemantic.Assign)]
		bool AlwaysBouncePages { get; }

		[Export ("showsScrollIndicators", ArgumentSemantic.Assign)]
		bool ShowsScrollIndicators { get; }

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

		[Export ("renderingMode", ArgumentSemantic.Assign)]
		PSPDFPageRenderingMode RenderingMode { get; }

		[Export ("renderAnimationEnabled", ArgumentSemantic.Assign)]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; }

		[Export ("smartZoomEnabled", ArgumentSemantic.Assign)]
		bool SmartZoomEnabled { [Bind ("isSmartZoomEnabled")] get; }

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

		[Export ("HUDViewMode", ArgumentSemantic.Assign)]
		PSPDFHUDViewMode HUDViewMode { get; }

		[Export ("HUDViewAnimation", ArgumentSemantic.Assign)]
		PSPDFHUDViewAnimation HUDViewAnimation { get; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; }

		[Export ("documentLabelEnabled", ArgumentSemantic.Assign)]
		bool DocumentLabelEnabled { [Bind ("isDocumentLabelEnabled")] get; }

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

		[Export ("thumbnailBarMode", ArgumentSemantic.Assign)]
		PSPDFThumbnailBarMode ThumbnailBarMode { get; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; }

		[Export ("annotationAnimationDuration", ArgumentSemantic.Assign)]
		nfloat AnnotationAnimationDuration { get; }

		[Export ("annotationGroupingEnabled", ArgumentSemantic.Assign)]
		bool AnnotationGroupingEnabled { get; }

		[Export ("createAnnotationMenuEnabled", ArgumentSemantic.Assign)]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		NSObject [] CreateAnnotationMenuGroups { get; }

		[Export ("naturalDrawingAnnotationEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingAnnotationEnabled { get; }

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

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; }

		[Export ("signatureSavingEnabled", ArgumentSemantic.Assign)]
		bool SignatureSavingEnabled { get; }

		[Export ("customerSignatureFeatureEnabled", ArgumentSemantic.Assign)]
		bool CustomerSignatureFeatureEnabled { get; }

		[Export ("naturalSignatureDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalSignatureDrawingEnabled { get; }

		[Export ("signatureStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFSignatureStore SignatureStore { get; }

		[Export ("internalTapGesturesEnabled", ArgumentSemantic.Assign)]
		bool InternalTapGesturesEnabled { get; }

		[Export ("useParentNavigationBar", ArgumentSemantic.Assign)]
		bool UseParentNavigationBar { get; }

		[Export ("shouldCacheThumbnails", ArgumentSemantic.Assign)]
		bool ShouldCacheThumbnails { get; }

		[Export ("galleryConfiguration", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryConfiguration GalleryConfiguration { get; }
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFConfigurationBuilder
	{
		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }

		[Export ("padding", ArgumentSemantic.Assign)]
		UIEdgeInsets Padding { get; set; }

		[Export ("pagePadding", ArgumentSemantic.Assign)]
		nfloat PagePadding { get; set; }

		[Export ("renderingMode", ArgumentSemantic.Assign)]
		PSPDFPageRenderingMode RenderingMode { get; set; }

		[Export ("smartZoomEnabled", ArgumentSemantic.Assign)]
		bool SmartZoomEnabled { [Bind ("isSmartZoomEnabled")] get; set; }

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
		bool DocumentLabelEnabled { [Bind ("isDocumentLabelEnabled")] get; set; }

		[Export ("shouldHideHUDOnPageChange", ArgumentSemantic.Assign)]
		bool ShouldHideHUDOnPageChange { get; set; }

		[Export ("shouldShowHUDOnViewWillAppear", ArgumentSemantic.Assign)]
		bool ShouldShowHUDOnViewWillAppear { get; set; }

		[Export ("toolbarEnabled", ArgumentSemantic.Assign)]
		bool ToolbarEnabled { [Bind ("isToolbarEnabled")] get; set; }

		[Export ("allowToolbarTitleChange", ArgumentSemantic.Assign)]
		bool AllowToolbarTitleChange { get; set; }

		[Export ("renderAnimationEnabled", ArgumentSemantic.Assign)]
		bool RenderAnimationEnabled { [Bind ("isRenderAnimationEnabled")] get; set; }

		[Export ("pageMode", ArgumentSemantic.Assign)]
		PSPDFPageMode PageMode { get; set; }

		[Export ("thumbnailGrouping", ArgumentSemantic.Assign)]
		PSPDFThumbnailGrouping ThumbnailGrouping { get; set; }

		[Export ("pageTransition", ArgumentSemantic.Assign)]
		PSPDFPageTransition PageTransition { get; set; }

		[Export ("scrollDirection", ArgumentSemantic.Assign)]
		PSPDFScrollDirection ScrollDirection { get; set; }

		[Export ("shouldAutomaticallyAdjustScrollViewInsets", ArgumentSemantic.Assign)]
		bool ShouldAutomaticallyAdjustScrollViewInsets { get; set; }

		[Export ("doublePageModeOnFirstPage", ArgumentSemantic.Assign)]
		bool DoublePageModeOnFirstPage { [Bind ("isDoublePageModeOnFirstPage")] get; set; }

		[Export ("zoomingSmallDocumentsEnabled", ArgumentSemantic.Assign)]
		bool ZoomingSmallDocumentsEnabled { [Bind ("isZoomingSmallDocumentsEnabled")] get; set; }

		[Export ("pageCurlDirectionLeftToRight", ArgumentSemantic.Assign)]
		bool PageCurlDirectionLeftToRight { [Bind ("isPageCurlDirectionLeftToRight")] get; set; }

		[Export ("fitToWidthEnabled", ArgumentSemantic.Assign)]
		bool FitToWidthEnabled { [Bind ("isFitToWidthEnabled")] get; set; }

		[Export ("showsScrollIndicators", ArgumentSemantic.Assign)]
		bool ShowsScrollIndicators { get; set; }

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

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("annotationAnimationDuration", ArgumentSemantic.Assign)]
		nfloat AnnotationAnimationDuration { get; set; }

		[Export ("annotationGroupingEnabled", ArgumentSemantic.Assign)]
		bool AnnotationGroupingEnabled { get; set; }

		[Export ("createAnnotationMenuEnabled", ArgumentSemantic.Assign)]
		bool CreateAnnotationMenuEnabled { [Bind ("isCreateAnnotationMenuEnabled")] get; set; }

		[Export ("createAnnotationMenuGroups", ArgumentSemantic.Copy)]
		NSObject [] CreateAnnotationMenuGroups { get; set; }

		[Export ("naturalDrawingAnnotationEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingAnnotationEnabled { get; set; }

		[Export ("showAnnotationMenuAfterCreation", ArgumentSemantic.Assign)]
		bool ShowAnnotationMenuAfterCreation { get; set; }

		[Export ("shouldAskForAnnotationUsername", ArgumentSemantic.Assign)]
		bool ShouldAskForAnnotationUsername { get; set; }

		[Export ("annotationEntersEditModeAfterSecondTapEnabled", ArgumentSemantic.Assign)]
		bool AnnotationEntersEditModeAfterSecondTapEnabled { get; set; }

		[Export ("autosaveEnabled", ArgumentSemantic.Assign)]
		bool AutosaveEnabled { [Bind ("isAutosaveEnabled")] get; set; }

		[Export ("allowBackgroundSaving", ArgumentSemantic.Assign)]
		bool AllowBackgroundSaving { get; set; }

		[Export ("shouldCacheThumbnails", ArgumentSemantic.Assign)]
		bool ShouldCacheThumbnails { get; set; }

		[Export ("shouldScrollToChangedPage", ArgumentSemantic.Assign)]
		bool ShouldScrollToChangedPage { get; set; }

		[Export ("searchMode", ArgumentSemantic.Assign)]
		PSPDFSearchMode SearchMode { get; set; }

		[Export ("signatureSavingEnabled", ArgumentSemantic.Assign)]
		bool SignatureSavingEnabled { get; set; }

		[Export ("customerSignatureFeatureEnabled", ArgumentSemantic.Assign)]
		bool CustomerSignatureFeatureEnabled { get; set; }

		[Export ("naturalSignatureDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalSignatureDrawingEnabled { get; set; }

		[Export ("signatureStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFSignatureStore SignatureStore { get; set; }
	}

	interface IPSPDFPresentationControls { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPresentationControls
	{

		[Export ("presentViewController:options:animated:sender:completion:")]
		[Abstract]
		NSObject PresentViewController (UIViewController controller, NSDictionary options, bool animated, NSObject sender, Action completion);

		[Export ("dismissViewControllerAnimated:class:completion:")]
		[Abstract]
		void DismissViewController (bool animated, Class controllerClass, Action completion);

		[Export ("dismissPopoverAnimated:class:completion:")]
		[Abstract]
		bool DismissPopover (bool animated, Class popoverClass, Action completion);
	}

	interface IPSPDFPageControls
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPageControls
	{
		[Export ("setPage:animated:")]
		[Abstract]
		bool SetPage (nuint page, bool animated);

		[Export ("scrollToNextPageAnimated:")]
		[Abstract]
		bool ScrollToNextPage (bool animated);

		[Export ("scrollToPreviousPageAnimated:")]
		[Abstract]
		bool ScrollToPreviousPage (bool animated);

		[Export ("setViewMode:animated:")]
		[Abstract]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("executePDFAction:inTargetRect:forPage:animated:actionContainer:")]
		[Abstract]
		bool ExecutePDFAction (PSPDFAction action, CGRect targetRect, nuint page, bool animated, NSObject actionContainer);

		[Export ("searchForString:options:sender:animated:")]
		[Abstract]
		void SearchForString (string searchText, NSDictionary options, NSObject sender, bool animated);

		[Export ("reloadData")]
		[Abstract]
		void ReloadData ();
	}

	interface IPSPDFHUDControls
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFHUDControls
	{
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
		bool ShouldShowControls ();

		[Export ("showControlsAnimated:")]
		[Abstract]
		bool ShowControls (bool animated);

		[Export ("showMenuIfSelectedAnimated:allowPopovers:")]
		[Abstract]
		void ShowMenuIfSelected (bool animated, bool allowPopovers);
	}

	interface IPSPDFControlDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFControlDelegate : PSPDFPresentationControls, PSPDFPageControls, PSPDFHUDControls
	{

	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFViewController
	{
		[Notification]
		[Field ("PSPDFViewControllerDidShowPageViewNotification", "__Internal")]
		NSString DidShowPageViewNotification { get; }

		[Notification]
		[Field ("PSPDFViewControllerDidLoadPageViewNotification", "__Internal")]
		NSString DidLoadPageViewNotification { get; }

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("initWithDocument:configuration:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document, PSPDFConfiguration configuration);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFViewControllerDelegate Delegate { get; set; }

		[Export ("formSubmissionDelegate", ArgumentSemantic.Weak)][NullAllowed]
		IPSPDFFormSubmissionDelegate FormSubmissionDelegate { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("screenPage", ArgumentSemantic.Assign)]
		nuint ScreenPage { get; }

		[Export ("scrollingEnabled", ArgumentSemantic.Assign)]
		bool ScrollingEnabled { [Bind ("isScrollingEnabled")] get; set; }

		[Export ("viewLockEnabled", ArgumentSemantic.Assign)]
		bool ViewLockEnabled { [Bind ("isViewLockEnabled")] get; set; }

		[Export ("viewState", ArgumentSemantic.Strong), NullAllowed]
		PSPDFViewState ViewState { get; set; }

		[Export ("searchActive", ArgumentSemantic.Assign)]
		bool SearchActive { [Bind ("isSearchActive")] get; }

		[Export ("searchHighlightViewManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSearchHighlightViewManager SearchHighlightViewManager { get; }

		[Export ("textSearch", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSearch TextSearch { get; }

		[Export ("inlineSearchManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFInlineSearchManager InlineSearchManager { get; }

		[Export ("HUDView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFHUDView HUDView { get; }

		[Export ("HUDVisible", ArgumentSemantic.Assign)]
		bool HUDVisible { [Bind ("isHUDVisible")] get; set; }

		[Export ("shouldAlwaysShowHUD", ArgumentSemantic.Assign)]
		bool ShouldAlwaysShowHUD { get; set; }

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRelayTouchesView ContentView { get; }

		[Export ("navigationBarHidden", ArgumentSemantic.Assign)]
		bool NavigationBarHidden { [Bind ("isNavigationBarHidden")] get; }

		[Export ("rotationLockEnabled", ArgumentSemantic.Assign)]
		bool RotationLockEnabled { [Bind ("isRotationLockEnabled")] get; set; }

		[Export ("pagingScrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Export ("viewMode", ArgumentSemantic.Assign)]
		PSPDFViewMode ViewMode { get; set; }

		[Export ("thumbnailController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailViewController ThumbnailController { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("setPage:animated:")]
		bool SetPage (nuint page, bool animated);

		[Export ("scrollToNextPageAnimated:")]
		bool ScrollToNextPage (bool animated);

		[Export ("scrollToPreviousPageAnimated:")]
		bool ScrollToPreviousPage (bool animated);

		[Export ("scrollRectToVisible:animated:")]
		void ScrollRectToVisible (CGRect rect, bool animated);

		[Export ("zoomToRect:page:animated:")]
		void ZoomToRect (CGRect rect, nuint page, bool animated);

		[Export ("setZoomScale:animated:")]
		void SetZoomScale (nfloat scale, bool animated);

		[Export ("setViewState:animated:")]
		void SetViewState (PSPDFViewState viewState, bool animated);

		[Export ("searchForString:options:sender:animated:")]
		void SearchForString (string searchText, NSDictionary options, NSObject sender, bool animated);

		[Export ("cancelSearchAnimated:")]
		void CancelSearch (bool animated);

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

		[Export ("pageViewForPage:")]
		PSPDFPageView PageViewForPage (nuint page);

		[Export ("setViewMode:animated:")]
		void SetViewMode (PSPDFViewMode viewMode, bool animated);

		[Export ("visiblePageNumbers"), NullAllowed]
		NSOrderedSet VisiblePageNumbers { get; }

		[Export ("visiblePageViews")]
		PSPDFPageView [] VisiblePageViews { get; }

		[Export ("isDoublePageMode")]
		bool IsDoublePageMode { get; }

		[Export ("isLastPage")]
		bool IsLastPage { get; }

		[Export ("isFirstPage")]
		bool IsFirstPage { get; }

		// PSPDFViewController (Presentation) Category

		[Export ("updateConfigurationWithoutReloadingWithBuilder:")]
		void UpdateConfigurationWithoutReloading (Action<PSPDFConfigurationBuilder> builderHandler);

		[Export ("popoverController", ArgumentSemantic.Strong), NullAllowed]
		UIPopoverController PopoverController { get; set; }

		[Export ("halfModalController", ArgumentSemantic.Strong), NullAllowed]
		UIViewController HalfModalController { get; set; }

		[Export ("presentViewController:options:animated:sender:completion:")]
		NSObject PresentViewController (UIViewController controller, NSDictionary options, bool animated, NSObject sender, Action completion);

		[Export ("dismissPopoverAnimated:class:completion:")]
		bool DismissPopover (bool animated, Class popoverClass, Action completion);

		[Export ("dismissViewControllerAnimated:class:completion:")]
		void DismissViewController (bool animated, Class controllerClass, Action completion);

		[Export ("configuration", ArgumentSemantic.Copy)]
		PSPDFConfiguration Configuration { get; }

		[Export ("updateConfigurationWithBuilder:")]
		void UpdateConfiguration (Action<PSPDFConfigurationBuilder> builderHandler);

		// PSPDFViewController (Annotations) Category

		[Export ("annotationStateManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationStateManager AnnotationStateManager { get; }

		// PSPDFViewController (Toolbar) Category

		[Export ("closeButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFCloseBarButtonItem CloseButtonItem { get; }

		[Export ("outlineButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineBarButtonItem OutlineButtonItem { get; }

		[Export ("searchButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSearchBarButtonItem SearchButtonItem { get; }

		[Export ("viewModeButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFViewModeBarButtonItem ViewModeButtonItem { get; }

		[Export ("printButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPrintBarButtonItem PrintButtonItem { get; }

		[Export ("openInButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOpenInBarButtonItem OpenInButtonItem { get; }

		[Export ("emailButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFEmailBarButtonItem EmailButtonItem { get; }

		[Export ("messageButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMessageBarButtonItem MessageButtonItem { get; }

		[Export ("annotationButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationBarButtonItem AnnotationButtonItem { get; }

		[Export ("bookmarkButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBookmarkBarButtonItem BookmarkButtonItem { get; }

		[Export ("brightnessButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBrightnessBarButtonItem BrightnessButtonItem { get; }

		[Export ("activityButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFActivityBarButtonItem ActivityButtonItem { get; }

		[Export ("additionalActionsButtonItem", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMoreBarButtonItem AdditionalActionsButtonItem { get; }

		[Export ("leftBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] LeftBarButtonItems { get; set; }

		[Export ("rightBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] RightBarButtonItems { get; set; }

		[Export ("additionalBarButtonItems", ArgumentSemantic.Copy)]
		NSObject [] AdditionalBarButtonItems { get; set; }

		[Export ("barButtonItemsAlwaysEnabled", ArgumentSemantic.Copy)]
		NSObject [] BarButtonItemsAlwaysEnabled { get; set; }

		// PSPDFViewController (SubclassingHooks) Category

		[Export ("pageTransitionController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTransitionProtocol PageTransitionController { get; }

		[Export ("commonInitWithDocument:configuration:")]
		void CommonInitWithDocument (PSPDFDocument document, PSPDFConfiguration configuration);

		[Export ("executePDFAction:inTargetRect:forPage:animated:actionContainer:")]
		bool ExecutePDFAction (PSPDFAction action, CGRect targetRect, nuint page, bool animated, NSObject actionContainer);

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("updateBarButtonItem:animated:")]
		void UpdateBarButtonItem (UIBarButtonItem barButtonItem, bool animated);

		[Export ("setUpdateSettingsForBoundsChangeBlock:")]
		void SetUpdateSettingsForBoundsChange (Action<PSPDFViewController> handler);

		[Export ("contentRect")]
		CGRect ContentRect { get; }

		[Export ("visibleAnnotationToolbar")]
		PSPDFAnnotationToolbar VisibleAnnotationToolbar { get; }

		[Export ("createNewControllerForDocument:")]
		PSPDFViewController CreateNewControllerForDocument (PSPDFDocument document);

		[Export ("calculatedVisiblePageNumbers")]
		NSObject [] CalculatedVisiblePageNumbers ();

		[Export ("updatePage:animated:")]
		void UpdatePage (nuint page, bool animated);
	}

	[Static]
	interface PSPDFPresentationKeys
	{
		[Field ("PSPDFPresentationStyleKey", "__Internal")]
		NSString StyleKey { get; }

		[Field ("PSPDFPresentationModalStyleKey", "__Internal")]
		NSString ModalStyleKey { get; }

		[Field ("PSPDFPresentationWillDismissBlockKey", "__Internal")]
		NSString WillDismissBlockKey { get; }

		[Field ("PSPDFPresentationDidDismissBlockKey", "__Internal")]
		NSString DidDismissBlockKey { get; }

		[Field ("PSPDFPresentationRectKey", "__Internal")]
		NSString RectKey { get; }

		[Field ("PSPDFPresentationContentSizeKey", "__Internal")]
		NSString ContentSizeKey { get; }

		[Field ("PSPDFPresentationPopoverArrowDirectionsKey", "__Internal")]
		NSString PopoverArrowDirectionsKey { get; }

		[Field ("PSPDFPresentationPopoverPassthroughViewsKey", "__Internal")]
		NSString PopoverPassthroughViewsKey { get; }

		[Field ("PSPDFPresentationInNavigationControllerKey", "__Internal")]
		NSString InNavigationControllerKey { get; }

		[Field ("PSPDFPresentationCloseButtonKey", "__Internal")]
		NSString CloseButtonKey { get; }

		[Field ("PSPDFPresentationPersistentCloseButtonKey", "__Internal")]
		NSString PersistentCloseButtonKey { get; }
	}

	interface IPSPDFPresentableViewController
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPresentableViewController
	{

	}

	interface IPSPDFHostableViewController
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFHostableViewController
	{

	}

	interface IPSPDFViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFViewControllerDelegate
	{

		[Export ("pdfViewController:shouldChangeDocument:")]
		bool ShouldChangeDocument (PSPDFViewController pdfController, PSPDFDocument document);

		[Export ("pdfViewController:didChangeDocument:")]
		void DidChangeDocument (PSPDFViewController pdfController, PSPDFDocument document);

		[Export ("pdfViewController:shouldScrollToPage:")]
		bool ShouldScrollToPage (PSPDFViewController pdfController, nuint page);

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
		bool ShouldSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph[] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectText:withGlyphs:atRect:onPageView:")]
		void DidSelectText (PSPDFViewController pdfController, string text, PSPDFGlyph[] glyphs, CGRect rect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedText:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedText (PSPDFViewController pdfController, PSPDFMenuItem[] menuItems, CGRect rect, string selectedText, CGRect textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forSelectedImage:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForSelectedImage (PSPDFViewController pdfController, PSPDFMenuItem[] menuItems, CGRect rect, PSPDFImageInfo selectedImage, CGRect textRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowMenuItems:atSuggestedTargetRect:forAnnotations:inRect:onPageView:")]
		PSPDFMenuItem [] ShouldShowMenuItemsForAnnotations (PSPDFViewController pdfController, PSPDFMenuItem[] menuItems, CGRect rect, PSPDFAnnotation[] annotations, CGRect annotationRect, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldDisplayAnnotation:onPageView:")]
		bool ShouldDisplayAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:didTapOnAnnotation:annotationPoint:annotationView:pageView:viewPoint:")]
		bool DidTapOnAnnotation (PSPDFViewController pdfController, PSPDFAnnotation annotation, CGPoint annotationPoint, IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView, CGPoint viewPoint);

		[Export ("pdfViewController:shouldSelectAnnotations:onPageView:")]
		PSPDFAnnotation [] ShouldSelectAnnotations (PSPDFViewController pdfController, PSPDFAnnotation[] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:didSelectAnnotations:onPageView:")]
		void DidSelectAnnotations (PSPDFViewController pdfController, NSObject[] annotations, PSPDFPageView pageView);

		[Export ("pdfViewController:annotationView:forAnnotation:onPageView:")]
		IPSPDFAnnotationViewProtocol AnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationViewProtocol annotationView, PSPDFAnnotation annotation, PSPDFPageView pageView);

		[Export ("pdfViewController:willShowAnnotationView:onPageView:")]
		void WillShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:didShowAnnotationView:onPageView:")]
		void DidShowAnnotationView (PSPDFViewController pdfController, IPSPDFAnnotationViewProtocol annotationView, PSPDFPageView pageView);

		[Export ("pdfViewController:shouldShowController:embeddedInController:options:animated:")]
		bool ShouldShowController (PSPDFViewController pdfController, IPSPDFPresentableViewController controller, IPSPDFHostableViewController hostController, NSDictionary options, bool animated);

		[Export ("pdfViewController:didShowController:embeddedInController:options:animated:")]
		void DidShowController (PSPDFViewController pdfController, IPSPDFPresentableViewController controller, IPSPDFHostableViewController hostController, NSDictionary options, bool animated);

		[Export ("pdfViewController:requestsUpdateForBarButtonItem:animated:")]
		void RequestsUpdateForBarButtonItem (PSPDFViewController pdfController, UIBarButtonItem barButtonItem, bool animated);

		[Export ("pdfViewController:didChangeViewMode:")]
		void DidChangeViewMode (PSPDFViewController pdfController, PSPDFViewMode viewMode);

		[Export ("pdfViewControllerWillDismiss:")]
		void PdfViewControllerWillDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewControllerDidDismiss:")]
		void PdfViewControllerDidDismiss (PSPDFViewController pdfController);

		[Export ("pdfViewController:shouldShowHUD:")]
		bool ShouldShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didShowHUD:")]
		void DidShowHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:shouldHideHUD:")]
		bool ShouldHideHud (PSPDFViewController pdfController, bool animated);

		[Export ("pdfViewController:didHideHUD:")]
		void DidHideHud (PSPDFViewController pdfController, bool animated);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFDocument {

		[Static, Export ("document")]
		PSPDFDocument Document { get; }

		[Static, Export ("documentWithURL:")]
		PSPDFDocument FromUrl (NSUrl url);

		[Static, Export ("documentWithData:")]
		PSPDFDocument FromData (NSData data);

		[Static, Export ("documentWithDataArray:")]
		PSPDFDocument FromData (NSData[] data);

		[Static, Export ("documentWithDataProvider:"), Internal]
		PSPDFDocument FromDataProvider (IntPtr /* CGDataProvider */ dataProvider);

		[Static, Export ("documentWithDataProviderArray:")]
		PSPDFDocument FromDataProvider (CGDataProvider[] dataProviders);

		[Static, Export ("documentWithBaseURL:files:")]
		PSPDFDocument FromBaseUrl (NSUrl baseUrl, string[] files);

		[Static, Export ("documentWithBaseURL:fileTemplate:startPage:endPage:")]
		PSPDFDocument FromBaseUrl (NSUrl baseURL, string fileTemplate, nint startPage, nint endPage);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		[Export ("initWithDataArray:")]
		IntPtr Constructor (NSData[] data);

		[Export ("initWithDataProviderArray:")]
		IntPtr Constructor (CGDataProvider[] dataProviders);

		[Export ("initWithBaseURL:files:")]
		IntPtr Constructor (NSUrl baseURL, string[] files);

		[Export ("initWithBaseURL:fileTemplate:startPage:endPage:")]
		IntPtr Constructor (NSUrl baseURL, string fileTemplate, nint startPage, nint endPage);

		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		IPSPDFDocumentDelegate Delegate { get; set; }

		[Export ("baseURL", ArgumentSemantic.Copy)]
		NSUrl BaseUrl { get; }

		[Export ("files", ArgumentSemantic.Copy)]
		NSUrl [] Files { get; }

		[Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }

		[Export ("data", ArgumentSemantic.Copy)]
		NSData Data { get; }

		[Export ("dataArray", ArgumentSemantic.Copy)]
		NSData [] DataArray { get; }

		[Export ("dataProviderArray", ArgumentSemantic.Copy)]
		CGDataProvider [] DataProviderArray { get; }

		[Export ("UID")]
		string Uid { get; set; }

		[Export ("valid", ArgumentSemantic.Assign)]
		bool Valid { [Bind ("isValid")] get; }

		[Export ("pageCount", ArgumentSemantic.Assign)]
		nuint PageCount { get; }

		[Export ("pageRange", ArgumentSemantic.Copy)]
		NSIndexSet PageRange { get; set; }

		[Export ("outlineParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineParser OutlineParser { get; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; set; }

		[Export ("isEqualToDocument:")]
		bool IsEqualToDocument (PSPDFDocument otherDocument);

		[Export ("pathForPage:")]
		NSUrl PathForPage (nuint page);

		[Export ("fileIndexForPage:")]
		nint FileIndexForPage (nuint page);

		[Export ("URLForFileIndex:")]
		NSUrl UrlForFileIndex (nuint fileIndex);

		[Export ("filesWithBasePath")]
		NSUrl [] FilesWithBasePath { get; }

		[Export ("fileNamesWithDataDictionary")]
		NSDictionary FileNamesWithDataDictionary { get; }

		[Export ("fileNameForPage:")]
		string FileNameForPage (nuint pageIndex);

		[Export ("fileName")]
		string FileName { get; }

		[Export ("documentByAppendingObjects:")]
		PSPDFDocument DocumentByAppendingObjects (NSObject[] objects);

		[Export ("documentProviderForPage:")]
		PSPDFDocumentProvider DocumentProviderForPage (nuint page);

		[Export ("pageOffsetForDocumentProvider:")]
		nuint PageOffsetForDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("documentProviders")]
		PSPDFDocumentProvider [] DocumentProviders ();

		[Export ("pageWithPageRange:")]
		nuint PageWithPageRange (nuint page);

		[Export ("PDFPageNumberForPage:")]
		nuint PdfPageNumberForPage (nuint page);

		[Export ("pageInfoForPage:")]
		PSPDFPageInfo PageInfoForPage (nuint page);

		[Export ("setPageInfo:forPage:")]
		void SetPageInfo (PSPDFPageInfo pageInfo, nuint page);

		// PSPDFDocument (Caching) Category

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("fillCache")]
		void FillCache ();

		[Export ("dataDirectory", ArgumentSemantic.Copy)]
		string DataDirectory { get; set; }

		[Export ("ensureDataDirectoryExistsWithError:")] [Internal]
		bool _EnsureDataDirectoryExists (IntPtr error);

		[Export ("diskCacheStrategy", ArgumentSemantic.Assign)]
		PSPDFDiskCacheStrategy DiskCacheStrategy { get; set; }

		// PSPDFDocument (Security) Category

		[Export ("unlockWithPassword:")]
		bool UnlockWithPassword (string password);

		[Export ("lock")]
		void Lock ();

		[Export ("password")]
		string Password { get; set; }

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("encryptionFilter")]
		string EncryptionFilter { get; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("allowsPrinting", ArgumentSemantic.Assign)]
		bool AllowsPrinting { get; }

		[Export ("allowsCopying", ArgumentSemantic.Assign)]
		bool AllowsCopying { get; set; }

		[Export ("allowAnnotationChanges", ArgumentSemantic.Assign)]
		bool AllowAnnotationChanges { get; }

		// PSPDFDocument (Bookmarks) Category

		[Export ("bookmarksEnabled", ArgumentSemantic.Assign)]
		bool BookmarksEnabled { [Bind ("isBookmarksEnabled")] get; set; }

		[Export ("bookmarkParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBookmarkParser BookmarkParser { get; set; }

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		// PSPDFDocument (PageLabels) Category

		[Export ("pageLabelsEnabled", ArgumentSemantic.Assign)]
		bool PageLabelsEnabled { [Bind ("isPageLabelsEnabled")] get; set; }

		[Export ("pageLabelForPage:substituteWithPlainLabel:")]
		string PageLabelForPage (nuint page, bool substitute);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint PageForPageLabel (string pageLabel, bool partialMatching);

		// PSPDFDocument (Forms) Category

		[Export ("formsEnabled", ArgumentSemantic.Assign)]
		bool FormsEnabled { [Bind ("isFormsEnabled")] get; set; }

		[Export ("formParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFormParser FormParser { get; }

		// PSPDFDocument (EmbeddedFiles) Category

		[Export ("allEmbeddedFiles")]
		PSPDFEmbeddedFile [] AllEmbeddedFiles { get; }

		// PSPDFDocument (Annotations) Category

		[Export ("annotationsEnabled", ArgumentSemantic.Assign)]
		bool AnnotationsEnabled { [Bind ("isAnnotationsEnabled")] get; set; }

		[Export ("annotationsForPage:type:")]
		PSPDFAnnotation [] AnnotationsForPage (nuint page, PSPDFAnnotationType type);

		[Export ("addAnnotations:")]
		bool AddAnnotations (PSPDFAnnotation[] annotations);

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation[] annotations, NSDictionary options);

		[Export ("removeAnnotations:")]
		bool RemoveAnnotations (PSPDFAnnotation[] annotations);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation[] annotations, NSDictionary options);

		[Export ("allAnnotationsOfType:")]
		NSDictionary AllAnnotationsOfType (PSPDFAnnotationType annotationType);

		[Export ("annotationManagerForPage:")]
		PSPDFAnnotationManager AnnotationManagerForPage (nuint page);

		// PSPDFDocument (AnnotationSaving) Category

		[Notification]
		[Field ("PSPDFDocumentWillSaveAnnotationsNotification", "__Internal")]
		NSString DocumentWillSaveAnnotationsNotification { get; }

		// TODO: Change this to NSOrderedSet once the comparison bug is fixed
		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSObject EditableAnnotationTypes { get; set; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; }

		[Export ("annotationSaveMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationSaveMode AnnotationSaveMode { get; set; }

		[Export ("defaultAnnotationUsername")]
		string DefaultAnnotationUsername { get; set; }

		[Export ("annotationWritingOptions", ArgumentSemantic.Copy)]
		NSDictionary AnnotationWritingOptions { get; set; }

		[Export ("saveAnnotationsWithCompletionBlock:")]
		void SaveAnnotations (Action<PSPDFAnnotation[], NSError> completionBlock);

		[Export ("saveAnnotationsWithError:"), Internal]
		bool SaveAnnotations (IntPtr error);

		[Export ("hasDirtyAnnotations")]
		bool HasDirtyAnnotations { get; }

		// PSPDFDocument (Rendering) Category

		[Field ("PSPDFPreserveAspectRatioKey", "__Internal")]
		NSString PreserveAspectRatioKey { get; }

		[Field ("PSPDFIgnoreDisplaySettingsKey", "__Internal")]
		NSString IgnoreDisplaySettingsKey { get; }

		[Export ("renderOptions", ArgumentSemantic.Copy)]
		NSDictionary RenderOptions { get; set; }

		[Export ("renderAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType RenderAnnotationTypes { get; set; }

		[Export ("imageForPage:size:clippedToRect:annotations:options:receipt:error:")]
		UIImage ImageForPage (nuint page, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options, out PSPDFRenderReceipt receipt, out NSError error);

		[Export ("renderPage:context:size:clippedToRect:annotations:options:error:")]
		PSPDFRenderReceipt RenderPage (nuint page, CGContext context, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options, out NSError error);

		// PSPDFDocument (Metadata) Category

		[Export ("title")]
		string Title { get; set; }

		[Export ("titleLoaded", ArgumentSemantic.Assign)]
		bool TitleLoaded { [Bind ("isTitleLoaded")] get; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		// PSPDFDocument (SubclassingHooks) Category

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Export ("didCreateDocumentProvider:")]
		PSPDFDocumentProvider DidCreateDocumentProvider (PSPDFDocumentProvider documentProvider);

		[Export ("setDidCreateDocumentProviderBlock:")]
		void SetDidCreateDocumentProviderHandler (Action<PSPDFDocumentProvider> handler);

		[Export ("pageContentForPage:")]
		string PageContentForPage (nuint page);

		[Export ("backgroundColorForPage:")]
		UIColor BackgroundColorForPage (nuint page);

		[Export ("backgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BackgroundColor { get; set; }

		[Export ("pageInfoForPage:pageRef:"), Internal]
		PSPDFPageInfo PageInfoForPage (nuint page, IntPtr /* CGPDFPage */ pageRef);

		[Export ("fileNameForIndex:")]
		string FileNameForIndex (nuint fileIndex);

		// PSPDFDocument (Advanced) Category

		[Export ("PDFBox", ArgumentSemantic.Assign)]
		CGPDFBox PdfBox { get; set; }

		[Export ("boxRect:forPage:error:"), Internal]
		CGRect BoxRectForPage (CGPDFBox boxType, nuint page, IntPtr error);

		[Export ("undoEnabled", ArgumentSemantic.Assign)]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; set; }

		[Export ("undoController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFUndoController UndoController { get; set; }

		[Export ("documentProviderRelativePageForPage:")]
		nuint DocumentProviderRelativePageForPage (nuint page);

		[Export ("documentProviderRelativePageWithPageRangeCompensated:")]
		nuint DocumentProviderRelativePageWithPageRangeCompensated (nuint page);

		// PSPDFDocument (DataDetection) Category

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsFromDetectingLinkTypes:pagesInRange:options:progress:error:")]
		NSDictionary AnnotationsFromDetectingLinkTypes (PSPDFTextCheckingType textLinkTypes, NSIndexSet pageRange, [NullAllowed] NSDictionary options, Action<PSPDFAnnotation[], nuint, bool> progressBlock, out NSError error);

		// PSPDFDocument (ObjectFinder) Category

		[Export ("objectsAtPDFPoint:page:options:")]
		NSDictionary ObjectsAtPdfPoint (CGPoint pdfPoint, nuint page, NSDictionary options);

		[Export ("objectsAtPDFRect:page:options:")]
		NSDictionary ObjectsAtPdfRect (CGRect pdfRect, nuint page, NSDictionary options);
	}

	[Static]
	interface PSPDFObjects
	{
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

		// Output categories

		//		[Field ("PSPDFObjectsGlyphKey", "__Internal")]
		//		NSString GlyphKey { get; }
		//
		//		[Field ("PSPDFObjectsWordKey", "__Internal")]
		//		NSString WordKey { get; }
		//
		//		[Field ("PSPDFObjectsTextKey", "__Internal")]
		//		NSString TextKey { get; }
		//
		//		[Field ("PSPDFObjectsTextBlockKey", "__Internal")]
		//		NSString TextBlockKey { get; }
		//
		//		[Field ("PSPDFObjectsAnnotationKey", "__Internal")]
		//		NSString AnnotationKey { get; }
		//
		//		[Field ("PSPDFObjectsImageKey", "__Internal")]
		//		NSString ImageKey { get; }
	}

	[Static]
	interface PSPDFMetadataKey
	{
		[Field ("PSPDFMetadataKeyTitle", "__Internal")]
		NSString Title { get; }

		[Field ("PSPDFMetadataKeyAuthor", "__Internal")]
		NSString Author { get; }

		[Field ("PSPDFMetadataKeySubject", "__Internal")]
		NSString Subject { get; }

		[Field ("PSPDFMetadataKeyKeywords", "__Internal")]
		NSString Keywords { get; }

		[Field ("PSPDFMetadataKeyCreator", "__Internal")]
		NSString Creator { get; }

		[Field ("PSPDFMetadataKeyProducer", "__Internal")]
		NSString Producer { get; }

		[Field ("PSPDFMetadataKeyCreationDate", "__Internal")]
		NSString CreationDate { get; }

		[Field ("PSPDFMetadataKeyModDate", "__Internal")]
		NSString ModDate { get; }

		[Field ("PSPDFMetadataKeyTrapped", "__Internal")]
		NSString Trapped { get; }

		[Field ("PSPDFMetadataKeyPortfolio", "__Internal")]
		NSString Portfolio { get; }
	}

	interface IPSPDFDocumentDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFDocumentDelegate
	{

		[Export ("pdfDocument:didRenderPage:inContext:withSize:clippedToRect:annotations:options:")]
		void DidRenderPage (PSPDFDocument document, nuint page, CGContext context, CGSize fullSize, CGRect clipRect, PSPDFAnnotation[] annotations, NSDictionary options);

		[Export ("pdfDocument:resolveCustomAnnotationPathToken:")]
		string ResolveCustomAnnotationPathToken (PSPDFDocument document, string pathToken);

		[Export ("pdfDocument:provider:shouldSaveAnnotations:")]
		bool ShouldSaveAnnotations (PSPDFDocument document, PSPDFDocumentProvider documentProvider, PSPDFAnnotation[] annotations);

		[Export ("pdfDocument:didSaveAnnotations:")]
		void DidSaveAnnotations (PSPDFDocument document, PSPDFAnnotation[] annotations);

		[Export ("pdfDocument:failedToSaveAnnotations:error:")]
		void FailedToSaveAnnotations (PSPDFDocument document, PSPDFAnnotation[] annotations, NSError error);
	}

	interface IPSPDFPageViewDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPageViewDelegate
	{

	}

	[BaseType (typeof(PSPDFRelayTouchesView))]
	interface PSPDFAnnotationContainerView
	{

	}

	[BaseType (typeof(UIView))]
	interface PSPDFPageView
	{

		[Notification]
		[Field ("PSPDFPageViewSelectedAnnotationsDidChangeNotification", "__Internal")]
		NSString SelectedAnnotationsDidChangeNotification { get; }

		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (CGRect frame, IPSPDFPageViewDelegate aDelegate);

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; set; }

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView ContentView { get; }

		[Export ("renderView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView RenderView { get; }

		[Export ("annotationContainerView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationContainerView AnnotationContainerView { get; }

		[Export ("selectionView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSelectionView SelectionView { get; }

		[Export ("renderStatusView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRenderStatusView RenderStatusView { get; set; }

		[Export ("renderStatusViewOffset", ArgumentSemantic.Assign)]
		nfloat RenderStatusViewOffset { get; set; }

		[Export ("centerRenderStatusView", ArgumentSemantic.Assign)]
		bool CenterRenderStatusView { get; set; }

		[Export ("PDFScale", ArgumentSemantic.Assign)]
		nfloat PdfScale { get; }

		[Export ("visibleRect", ArgumentSemantic.Assign)]
		CGRect VisibleRect { get; }

		[Export ("highlightColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor HighlightColor { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("pageInfo", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageInfo PageInfo { get; }

		[Export ("rightPage", ArgumentSemantic.Assign)]
		bool RightPage { [Bind ("isRightPage")] get; }

		[Export ("displayPage:pageRect:scale:delayPageAnnotations:presentationContext:")]
		void DisplayPage (nuint page, CGRect pageRect, nfloat scale, bool delayPageAnnotations, PSPDFPresentationContext presentationContext);

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("updateRenderView")]
		void UpdateRenderView ();

		[Export ("updateView")]
		void UpdateView ();

		[Export ("annotationViewForAnnotation:")]
		IPSPDFAnnotationViewProtocol AnnotationViewForAnnotation (PSPDFAnnotation annotation);

		[Export ("convertViewPointToPDFPoint:")]
		CGPoint ConvertViewPointToPDFPoint (CGPoint viewPoint);

		[Export ("convertPDFPointToViewPoint:")]
		CGPoint ConvertPdfPointToViewPoint (CGPoint pdfPoint);

		[Export ("convertViewRectToPDFRect:")]
		CGRect ConvertViewRectToPdfRect (CGRect viewRect);

		[Export ("convertPDFRectToViewRect:")]
		CGRect ConvertPdfRectToViewRect (CGRect pdfRect);

		[Export ("convertGlyphRectToViewRect:")]
		CGRect ConvertGlyphRectToViewRect (CGRect glyphRect);

		[Export ("convertViewRectToGlyphRect:")]
		CGRect ConvertViewRectToGlyphRect (CGRect viewRect);

		[Export ("objectsAtPoint:options:")]
		NSDictionary ObjectsAtPoint (CGPoint viewPoint, [NullAllowed] NSDictionary options);

		[Export ("objectsAtRect:options:")]
		NSDictionary ObjectsAtRect (CGRect viewRect, [NullAllowed] NSDictionary options);

		[Export ("scrollView")]
		PSPDFScrollView ScrollView { get; }

		[Export ("visibleAnnotationViews")]
		IPSPDFAnnotationViewProtocol [] VisibleAnnotationViews { get; }

		[Export ("updateShadowAnimated:")]
		void UpdateShadow (bool animated);

		// PSPDFPageView (AnnotationViews) Category

		[Export ("setAnnotation:forAnnotationView:")]
		void SetAnnotation (PSPDFAnnotation annotation, IPSPDFAnnotationViewProtocol annotationView);

		[Export ("annotationForAnnotationView:")]
		PSPDFAnnotation AnnotationForAnnotationView (IPSPDFAnnotationViewProtocol annotationView);

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; set; }

		[Export ("singleTapped:")]
		bool SingleTapped (UITapGestureRecognizer recognizer);

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);

		[Export ("addAnnotation:options:animated:")]
		void AddAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary options, bool animated);

		[Export ("removeAnnotation:options:animated:")]
		bool RemoveAnnotation (PSPDFAnnotation annotation, [NullAllowed] NSDictionary options, bool animated);

		[Export ("selectAnnotation:animated:")]
		void SelectAnnotation (PSPDFAnnotation annotation, bool animated);

		// PSPDFPageView (SubclassingHooks) Category

		[Export ("insertAnnotations:forPage:inDocument:")]
		void InsertAnnotations (PSPDFAnnotation[] annotations, nuint page, PSPDFDocument document);

		[Export ("tappableAnnotationsAtPoint:")]
		PSPDFAnnotation [] TappableAnnotations (CGPoint viewPoint);

		[Export ("tappableAnnotationsForLongPressAtPoint:")]
		PSPDFAnnotation [] TappableAnnotationsForLongPress (CGPoint viewPoint);

		[Export ("hitTestRectForPoint:")]
		CGRect HitTestRectForPoint (CGPoint viewPoint);

		[Export ("singleTappedAtViewPoint:")]
		bool SingleTappedAtViewPoint (CGPoint viewPoint);

		[Export ("rectForAnnotations:")]
		CGRect RectForAnnotations (PSPDFAnnotation[] annotations);

		[Export ("renderOptionsDictWithZoomScale:animated:")]
		NSDictionary RenderOptionsDict (nfloat zoomScale, bool animated);

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

		// PSPDFPageView (AnnotationMenu) Category

		[Export ("menuItemsForAnnotations:")]
		PSPDFMenuItem [] MenuItemsForAnnotations (PSPDFAnnotation[] annotations);

		[Export ("menuItemsForNewAnnotationAtPoint:")]
		PSPDFMenuItem [] MenuItemsForNewAnnotationAtPoint (CGPoint point);

		[Export ("colorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] ColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("fillColorMenuItemsForAnnotation:")]
		PSPDFMenuItem [] FillColorMenuItemsForAnnotation (PSPDFAnnotation annotation);

		[Export ("opacityMenuItemForAnnotation:withColor:")]
		PSPDFMenuItem OpacityMenuItemForAnnotation (PSPDFAnnotation annotation, UIColor color);

		[Export ("showInspectorForAnnotations:options:animated:")]
		NSObject ShowInspectorForAnnotations (PSPDFAnnotation[] annotations, NSDictionary options, bool animated);

		[Export ("showMenuForAnnotations:edgeInsets:allowPopovers:animated:")]
		void ShowMenuForAnnotations (PSPDFAnnotation[] annotations, UIEdgeInsets edgeInsets, bool allowPopovers, bool animated);

		[Export ("showNoteControllerForAnnotation:showKeyboard:animated:")]
		PSPDFNoteAnnotationViewController ShowNoteControllerForAnnotation (PSPDFAnnotation annotation, bool showKeyboard, bool animated);

		[Export ("showFontPickerForAnnotation:animated:")]
		void ShowFontPickerForAnnotation (PSPDFFreeTextAnnotation annotation, bool animated);

		[Export ("showColorPickerForAnnotation:animated:")]
		void ShowColorPickerForAnnotation (PSPDFAnnotation annotation, bool animated);

		[Export ("showSignatureControllerAtRect:withTitle:shouldSaveSignature:animated:")]
		void ShowSignatureControllerAtRect (CGRect rect, string title, bool shouldSaveSignature, bool animated);

		[Export ("availableFontSizes")]
		NSNumber [] AvailableFontSizes { get; }

		[Export ("availableLineWidths")]
		NSNumber [] AvailableLineWidths { get; }

		[Export ("passthroughViewsForPopoverController")]
		NSNumber [] PassthroughViewsForPopoverController { get; }

		// PSPDFPageView (AnnotationMenuSubclassingHooks) Category

		[Export ("showNewSignatureMenuAtRect:animated:")]
		void ShowNewSignatureMenuAtRect (CGRect rect, bool animated);

		[Export ("showDigitalSignatureMenuForSignatureField:animated:")]
		bool ShowDigitalSignatureMenuForSignatureField (PSPDFSignatureFormElement signatureField, bool animated);

		[Export ("showNewImageMenuAtPoint:animated:")]
		void ShowNewImageMenuAtPoint (CGPoint point, bool animated);

		[Export ("defaultColorOptionsForAnnotationType:")]
		NSObject [] DefaultColorOptionsForAnnotationType (PSPDFAnnotationType annotationType);

		[Export ("useAnnotationInspectorForAnnotations:")]
		bool UseAnnotationInspectorForAnnotations (PSPDFAnnotation[] annotations);

		[Export ("selectColorForAnnotation:isFillColor:")]
		void SelectColorForAnnotation (PSPDFAnnotation annotation, bool isFillColor);

		[Export ("shouldMoveStyleMenuEntriesIntoSubmenu")]
		bool ShouldMoveStyleMenuEntriesIntoSubmenu ();

		[Export ("showLinkPreviewActionSheetForAnnotation:fromRect:animated:")]
		NSObject ShowLinkPreviewActionSheetForAnnotation (PSPDFLinkAnnotation annotation, CGRect viewRect, bool animated);

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
	interface PSPDFAnnotationMenuStrings
	{
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

		[Field ("PSPDFAnnotationMenuColorClear", "__Internal")]
		NSString ColorClear { get; }

		[Field ("PSPDFAnnotationMenuColorWhite", "__Internal")]
		NSString ColorWhite { get; }

		[Field ("PSPDFAnnotationMenuColorYellow", "__Internal")]
		NSString ColorYellow { get; }

		[Field ("PSPDFAnnotationMenuColorRed", "__Internal")]
		NSString ColorRed { get; }

		[Field ("PSPDFAnnotationMenuColorPink", "__Internal")]
		NSString ColorPink { get; }

		[Field ("PSPDFAnnotationMenuColorGreen", "__Internal")]
		NSString ColorGreen { get; }

		[Field ("PSPDFAnnotationMenuColorBlue", "__Internal")]
		NSString ColorBlue { get; }

		[Field ("PSPDFAnnotationMenuColorBlack", "__Internal")]
		NSString ColorBlack { get; }

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

	interface IPSPDFTextSelectionViewDataSource
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTextSelectionViewDataSource : PSPDFOverridable
	{

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

		[Export ("isTextSelectionEnabledForView:")]
		bool IsTextSelectionEnabledForView (PSPDFTextSelectionView textSelectionView);

		[Export ("isImageSelectionEnabledForView:")]
		bool IsImageSelectionEnabledForView (PSPDFTextSelectionView textSelectionView);
	}

	interface IPSPDFTextSelectionViewDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTextSelectionViewDelegate
	{

		[Export ("textSelectionView:updateMenuAnimated:")]
		[Abstract]
		bool UpdateMenuAnimated (PSPDFTextSelectionView textSelectionView, bool animated);

		[Export ("textSelectionView:shouldSelectText:withGlyphs:atRect:")]
		bool ShouldSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph[] glyphs, CGRect rect);

		[Export ("textSelectionView:didSelectText:withGlyphs:atRect:")]
		void DidSelectText (PSPDFTextSelectionView textSelectionView, string text, PSPDFGlyph[] glyphs, CGRect rect);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(UIView))]
	interface PSPDFTextSelectionView : IAVSpeechSynthesizerDelegate
	{

		[Export ("initWithFrame:dataSource:delegate:")]
		IntPtr Constructor (CGRect frame, IPSPDFTextSelectionViewDataSource dataSource, IPSPDFTextSelectionViewDelegate aDelegate);

		[Export ("dataSource", ArgumentSemantic.Weak)]
		[NullAllowed]
		IPSPDFTextSelectionViewDataSource DataSource { get; }

		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		IPSPDFTextSelectionViewDelegate Delegate { get; }

		[Export ("selectedGlyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] SelectedGlyphs { get; set; }

		[Export ("selectedText")]
		string SelectedText { get; }

		[Export ("selectedImage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFImageInfo SelectedImage { get; set; }

		[Export ("selectionColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("selectionAlpha", ArgumentSemantic.Assign)]
		nfloat SelectionAlpha { get; set; }

		[Export ("simpleSelectionModeEnabled", ArgumentSemantic.Assign)]
		bool SimpleSelectionModeEnabled { get; set; }

		[Export ("trimmedSelectedText")]
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

		[Export ("hasSelection")]
		bool HasSelection ();

		// PSPDFTextSelectionView (Advanced) Category

		[Export ("sortedGlyphs:")]
		PSPDFGlyph [] SortedGlyphs (PSPDFGlyph[] glyphs);

		[Export ("presentWikipediaBrowserForSelectedText")]
		UIViewController PresentWikipediaBrowserForSelectedText ();

		// PSPDFTextSelectionView (SubclassingHooks) Category

		[Export ("addHighlightAnnotationWithType:")]
		void AddHighlightAnnotation (PSPDFAnnotationType highlightType);

		[Export ("showTextFlowData:animated:")]
		void ShowTextFlowData (bool show, bool animated);
	}

	[BaseType (typeof (UIScrollView))]
	interface PSPDFKeyboardAvoidingScrollView {

		[Export ("keyboardVisible", ArgumentSemantic.Assign)]
		bool KeyboardVisible { [Bind ("isKeyboardVisible")] get; }

		[Export ("firstResponderIsTextInput", ArgumentSemantic.Assign)]
		bool FirstResponderIsTextInput { get; }

		[Export ("enableKeyboardAvoidance", ArgumentSemantic.Assign)]
		bool EnableKeyboardAvoidance { get; set; }
	}

	interface IPSPDFLongPressGestureRecognizerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFLongPressGestureRecognizerDelegate : IUIGestureRecognizerDelegate {

		[Export ("pressRecognizerShouldHandlePressImmediately:")]
		[Abstract]
		bool PressRecognizerShouldHandlePressImmediately (PSPDFLongPressGestureRecognizer recognizer);
	}

	[BaseType (typeof (UILongPressGestureRecognizer))]
	interface PSPDFLongPressGestureRecognizer {

	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFPresentationContext : PSPDFOverridable {

		[Export ("configuration", ArgumentSemantic.Copy)]
		PSPDFConfiguration Configuration { get; }

		[Export ("displayingViewController", ArgumentSemantic.Strong), NullAllowed]
		UIViewController DisplayingViewController { get; }

		[Export ("popoverController", ArgumentSemantic.Strong), NullAllowed]
		UIPopoverController PopoverController { get; }

		[Export ("halfModalController", ArgumentSemantic.Strong), NullAllowed]
		UIViewController HalfModalController { get; }

		[Export ("visibleViewControllerInPopoverController", ArgumentSemantic.Strong), NullAllowed]
		UIViewController VisibleViewControllerInPopoverController { get; }

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("viewMode", ArgumentSemantic.Assign)]
		PSPDFViewMode ViewMode { get; }

		[Export ("contentRect", ArgumentSemantic.Assign)]
		CGRect ContentRect { get; }

		[Export ("scrollViewInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets ScrollViewInsets { get; }

		[Export ("doublePageMode", ArgumentSemantic.Assign)]
		bool DoublePageMode { [Bind ("isDoublePageMode")] get; }

		[Export ("scrollingEnabled", ArgumentSemantic.Assign)]
		bool ScrollingEnabled { [Bind ("isScrollingEnabled")] get; }

		[Export ("viewLockEnabled", ArgumentSemantic.Assign)]
		bool ViewLockEnabled { [Bind ("isViewLockEnabled")] get; }

		[Export ("rotationActive", ArgumentSemantic.Assign)]
		bool RotationActive { [Bind ("isRotationActive")] get; }

		[Export ("HUDVisible", ArgumentSemantic.Assign)]
		bool HUDVisible { [Bind ("isHUDVisible")] get; }

		[Export ("viewWillAppearing", ArgumentSemantic.Assign)]
		bool ViewWillAppearing { [Bind ("isViewWillAppearing")] get; }

		[Export ("pagingScrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Export ("annotationStateManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationStateManager AnnotationStateManager { get; }

		[Export ("actionDelegate", ArgumentSemantic.Strong), NullAllowed]
		NSObject WeakActionDelegate { get; }

		[Wrap ("WeakActionDelegate")]
		PSPDFControlDelegate ActionDelegate { get; }

		[Export ("pdfController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFViewController PdfController { get; }

		[Export ("visiblePageViews")]
		[Abstract]
		NSObject [] VisiblePageViews ();

		[Export ("visiblePageViewsForcingLayout:")]
		[Abstract]
		NSObject [] VisiblePageViewsForcingLayout (bool forcingLayout);

		[Export ("pageViewForPage:")]
		[Abstract]
		PSPDFPageView PageViewForPage (nuint page);

		[Export ("annotationRectForAnnotation:")]
		[Abstract]
		CGRect AnnotationRectForAnnotation (PSPDFAnnotation annotation);

		[Export ("calculatedVisiblePageNumbers")]
		[Abstract]
		NSObject [] CalculatedVisiblePageNumbers ();

		[Export ("isRightPageInDoublePageMode:")]
		[Abstract]
		bool IsRightPageInDoublePageMode (nuint page);

		[Export ("isDoublePageModeForOrientation:")]
		[Abstract]
		bool IsDoublePageModeForOrientation (UIInterfaceOrientation interfaceOrientation);

		[Export ("isDoublePageModeForPage:")]
		[Abstract]
		bool IsDoublePageModeForPage (nuint page);

		[Export ("portraitPageForLandscapePage:")]
		[Abstract]
		nuint PortraitPageForLandscapePage (nuint page);

		[Export ("landscapePageForPage:")]
		[Abstract]
		nuint LandscapePageForPage (nuint aPage);
	}


	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFKeyboardAvoidingScrollView))]
	interface PSPDFScrollView : IUIScrollViewDelegate, IPSPDFLongPressGestureRecognizerDelegate
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; set; }

		[Export ("leftPage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageView LeftPage { get; }

		[Export ("rightPage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageView RightPage { get; }

		[Export ("zoomingEnabled", ArgumentSemantic.Assign)]
		bool ZoomingEnabled { [Bind ("isZoomingEnabled")] get; set; }

		[Export ("displayPage:")]
		void DisplayPage (nuint page);

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

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

		[Export ("pathShadowForView:")]
		NSObject PathShadowForView (UIView view);

		[Export ("ensureContentIsCentered")]
		void EnsureContentIsCentered ();

		[Export ("createDoubleTapGesture")]
		UITapGestureRecognizer CreateDoubleTapGesture ();

		[Export ("compoundView", ArgumentSemantic.Strong), NullAllowed]
		UIView CompoundView { get; }

		// PSPDFScrollView (PSPDFAdvanced) Category

		[Export ("selectedAnnotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] SelectedAnnotations { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFDocumentProvider
	{

		[Export ("initWithFileURL:document:")]
		IntPtr Constructor (NSUrl fileURL, PSPDFDocument document);

		[Export ("initWithData:document:")]
		IntPtr Constructor (NSData data, PSPDFDocument document);

		[Export ("initWithDataProvider:document:"), Internal]
		IntPtr Constructor (IntPtr /*CGDataProvider*/ dataProvider, PSPDFDocument document);

		[Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("data", ArgumentSemantic.Strong), NullAllowed]
		NSData Data { get; }

		[Export ("dataProvider"), Internal]
		IntPtr _DataProvider { get; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentProviderDelegate Delegate { get; set; }

		[Export ("pageCount", ArgumentSemantic.Assign)]
		nuint PageCount { get; set; }

		[Export ("password")]
		string Password { get; set; }

		[Export ("allowsPrinting", ArgumentSemantic.Assign)]
		bool AllowsPrinting { get; }

		[Export ("allowsCopying", ArgumentSemantic.Assign)]
		bool AllowsCopying { get; set; }

		[Export ("isEncrypted", ArgumentSemantic.Assign)]
		bool IsEncrypted { get; }

		[Export ("isLocked", ArgumentSemantic.Assign)]
		bool IsLocked { get; }

		[Export ("canEmbedAnnotations", ArgumentSemantic.Assign)]
		bool CanEmbedAnnotations { get; set; }

		[Export ("allowAnnotationChanges", ArgumentSemantic.Assign)]
		bool AllowAnnotationChanges { get; }

		[Export ("title")]
		string Title { get; }

		[Export ("outlineParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineParser OutlineParser { get; set; }

		[Export ("formParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFormParser FormParser { get; set; }

		[Export ("embeddedFilesParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFEmbeddedFilesParser EmbeddedFilesParser { get; set; }

		[Export ("annotationManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationManager AnnotationManager { get; set; }

		[Export ("labelParser", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLabelParser LabelParser { get; set; }

		[Export ("dataRepresentationWithError:"), Internal]
		NSData DataRepresentationWithError (IntPtr error);

		[Export ("fileSize")]
		ulong FileSize ();

		[Export ("pageInfoForPage:")]
		PSPDFPageInfo PageInfoForPage (nuint page);

		[Export ("unlockWithPassword:")]
		bool Unlock (string password);

		[Export ("textParserForPage:")]
		PSPDFTextParser TextParserForPage (nuint page);

		[Export ("XMPMetadata")]
		string XmpMetadata { get; }

		// PSPDFDocumentProvider (Advanced) Category

		[Export ("ignoreLocking", ArgumentSemantic.Assign)]
		bool IgnoreLocking { get; set; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary Metadata { get; }

		[Export ("metadataLoaded", ArgumentSemantic.Assign)]
		bool MetadataLoaded { [Bind ("isMetadataLoaded")] get; }

		[Export ("flushDocumentReference")]
		bool FlushDocumentReference ();

		// PSPDFDocumentProvider (SubclassingHooks) Category

		[Export ("pageInfoForPage:pageRef:"), Internal]
		PSPDFPageInfo PageInfoForPage (nuint page, IntPtr /*CGPDFPage*/ pageRef);

		[Export ("setPageInfo:forPage:")]
		void SetPageInfo (PSPDFPageInfo pageInfo, nuint page);

		[Advice ("You shouldn't call this method directly, use the high-level save method in PSPDFDocument instead")]
		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotationsWithOptions (NSDictionary options, IntPtr error);

		[Export ("resolveTokenizedPath:alwaysLocal:")]
		string ResolveTokenizedPath (string path, bool alwaysLocal);
	}

	interface IPSPDFDocumentProviderDelegate { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFDocumentProviderDelegate
	{

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

	[BaseType (typeof (NSObject))]
	interface PSPDFMemoryCache {

		[Export ("initWithSettings:")]
		IntPtr Constructor (IPSPDFSettings settings);

		[Export ("numberOfPixels", ArgumentSemantic.Assign)]
		nuint NumberOfPixels { get; }

		[Export ("maxNumberOfPixels", ArgumentSemantic.Assign)]
		nuint MaxNumberOfPixels { get; set; }

		[Export ("maxNumberOfPixelsUnderStress", ArgumentSemantic.Assign)]
		nuint MaxNumberOfPixelsUnderStress { get; set; }

		[Export ("cacheInfoForImageWithUID:page:size:infoSelector:")]
		PSPDFCacheInfo CacheInfoForImageWithUID (string uid, nuint page, CGSize size, Func<NSOrderedSet, PSPDFCacheInfo> infoSelector);

		[Export ("storeImage:UID:page:receipt:")]
		void StoreImage (UIImage image, string uid, nuint page, string renderReceipt);

		[Export ("invalidateAllImagesWithUID:")]
		bool InvalidateAllImages (string uid);

		[Export ("invalidateAllImagesWithUID:page:infoArraySelector:")]
		bool InvalidateAllImages (string uid, nuint page, Func<NSOrderedSet, PSPDFCacheInfo[]> infoSelector);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("count")]
		nuint Count ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDiskCache {

		[Export ("initWithCacheDirectory:fileFormat:settings:")]
		IntPtr Constructor (string cacheDirectory, string fileFormat, PSPDFKitGlobal settings);

		[Export ("allowedDiskSpace", ArgumentSemantic.Assign)]
		ulong AllowedDiskSpace { get; set; }

		[Export ("usedDiskSpace", ArgumentSemantic.Assign)]
		ulong UsedDiskSpace { get; }

		[Export ("freeDiskSpace", ArgumentSemantic.Assign)]
		ulong FreeDiskSpace { get; }

		[Export ("fileFormat")]
		string FileFormat { get; set; }

		[Export ("cacheInfoForImageWithUID:page:size:infoSelector:")]
		PSPDFCacheInfo CacheInfoForImage (string uid, nuint page, CGSize size, Func<NSOrderedSet, PSPDFCacheInfo> infoSelector);

		[Export ("imageWithUID:page:size:infoSelector:decryptionHelper:cacheInfo:")]
		UIImage ImageWithUid (string uid, nuint page, CGSize size, Func<NSOrderedSet, PSPDFCacheInfo> infoSelector, Func<NSString, UIImage> decryptionHelper, out PSPDFCacheInfo outCacheInfo);

		[Export ("scheduleLoadImageWithUID:page:size:infoSelector:decryptionHelper:completionBlock:")]
		PSPDFCacheInfo ScheduleLoadImage (string uid, nuint page, CGSize size, Func<NSOrderedSet, PSPDFCacheInfo> infoSelector, Func<NSString, UIImage> decryptionHelper, Action<UIImage, PSPDFCacheInfo> completionBlock);

		[Export ("storeImage:UID:page:encryptionHelper:receipt:")]
		void StoreImage (UIImage image, string uid, nuint page, Func<UIImage, NSData> encryptionHelper, string renderReceipt);

		[Export ("storeImage:UID:page:encryptionHelper:receipt:completionBlock:")]
		void StoreImage (UIImage image, string uid, nuint page, Func<UIImage, NSData> encryptionHelper, string renderReceipt, Action<PSPDFCacheInfo> completionBlock);

		[Export ("invalidateAllImagesWithUID:")]
		bool InvalidateAllImages (string uid);

		[Export ("invalidateAllImagesWithUID:page:infoArraySelector:")]
		bool InvalidateAllImages (string uid, nuint page, Func<NSOrderedSet, PSPDFCacheInfo[]> infoSelector);

		[Export ("cancelWriteRequestsWithUID:page:infoArraySelector:")]
		void CancelWriteRequests (string uid, nuint page, Func<NSOrderedSet, PSPDFCacheInfo[]> infoSelector);

		[Export ("clearCache")]
		void ClearCache ();
	}


	interface IPSPDFCacheDelegate { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFCacheDelegate
	{

		[Export ("didRenderImage:document:page:size:")]
		void DidRenderImage (UIImage image, PSPDFDocument document, nuint page, CGSize size);
	}

	delegate NSData PSPDFCacheDecryptFromPathHandler (PSPDFDocument document, string path);
	delegate void PSPDFCacheEncryptDataHandler (PSPDFDocument document, NSMutableData data);

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFCache : IPSPDFRenderDelegate
	{

		[Export ("initWithSettings:")]
		IntPtr Constructor (PSPDFKitGlobal pspdfkit);

		[Export ("memoryCache", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMemoryCache MemoryCache { get; }

		[Export ("diskCache", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDiskCache DiskCache { get; }

		[Export ("cacheDirectory")]
		string CacheDirectory { get; set; }

		[Export ("diskCacheStrategy", ArgumentSemantic.Assign)]
		PSPDFDiskCacheStrategy DiskCacheStrategy { get; set; }

		[Export ("useJPGFormat", ArgumentSemantic.Assign)]
		bool UseJpgFormat { get; set; }

		[Export ("JPGFormatCompression", ArgumentSemantic.Assign)]
		nfloat JpgFormatCompression { get; set; }

		[Export ("allowImageResize", ArgumentSemantic.Assign)]
		bool AllowImageResize { get; set; }

		[Export ("cacheStatusForImageFromDocument:page:size:options:")]
		PSPDFCacheStatus CacheStatusForImage (PSPDFDocument document, nuint page, CGSize size, PSPDFCacheOptions options);

		[Export ("imageFromDocument:page:size:options:")]
		UIImage ImageFromDocument (PSPDFDocument document, nuint page, CGSize size, PSPDFCacheOptions options);

		[Export ("imageFromDocument:page:size:options:completionBlock:")]
		UIImage ImageFromDocument (PSPDFDocument document, nuint page, CGSize size, PSPDFCacheOptions options, Action<UIImage, PSPDFDocument, nuint, CGSize> completionHandler);

		[Export ("saveImage:document:page:receipt:")]
		void SaveImage (UIImage image, PSPDFDocument document, nuint page, string renderReceipt);

		[Export ("cacheDocument:pageSizes:withDiskCacheStrategy:aroundPage:")]
		void CacheDocument (PSPDFDocument document, NSValue[] sizes, PSPDFDiskCacheStrategy strategy, nuint page);

		[Export ("cacheDocument:pageSizes:withDiskCacheStrategy:aroundPage:imageRenderingCompletionBlock:")]
		void CacheDocument (PSPDFDocument document, NSValue[] sizes, PSPDFDiskCacheStrategy strategy, nuint page, Action<UIImage, PSPDFDocument, nuint, CGSize> pageCompletionHandler);

		[Export ("stopCachingDocument:")]
		void StopCachingDocument (PSPDFDocument document);

		[Export ("cancelRequestForImageFromDocument:page:size:")]
		void CancelRequestForImageFromDocument (PSPDFDocument document, nuint page, CGSize size);

		[Export ("invalidateImageFromDocument:page:")]
		void InvalidateImageFromDocument (PSPDFDocument document, nuint page);

		[Export ("removeCacheForDocument:deleteDocument:error:")]
		bool RemoveCacheForDocument (PSPDFDocument document, bool deleteDocument, out NSError error);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("pauseCachingForService:")]
		bool PauseCachingForService (NSObject service);

		[Export ("resumeCachingForService:")]
		bool ResumeCachingForService (NSObject service);

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFCacheDelegate aDelegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFCacheDelegate aDelegate);

		[Export ("setDecryptFromPathBlock:")]
		void SetDecryptFromPathHandler (PSPDFCacheDecryptFromPathHandler handler);

		[Export ("setEncryptDataBlock:")]
		void SetEncryptDataHandler (PSPDFCacheEncryptDataHandler handler);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFTranslucentToolbar))]
	interface PSPDFTransparentToolbar
	{
	}

	[BaseType (typeof(UIToolbar))]
	interface PSPDFTranslucentToolbar
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	interface IPSPDFPageRenderer
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPageRenderer
	{

		[Export ("drawPage:inContext:withOptions:error:")]
		[Abstract]
		bool DrawPage (nuint page, CGContext context, NSDictionary options, out NSError error);

		[Export ("renderAppearanceStream:inContext:error:")]
		bool RenderAppearanceStream (PSPDFAnnotation annotation, CGContext context, out NSError error);
	}

	[Static]
	interface PSPDFRenderKeys
	{
		[Field ("PSPDFRenderPageColorKey", "__Internal")]
		NSString PageColorKey { get; }

		[Field ("PSPDFRenderContentOpacityKey", "__Internal")]
		NSString ContentOpacityKey { get; }

		[Field ("PSPDFRenderInvertedKey", "__Internal")]
		NSString InvertedKey { get; }

		[Field ("PSPDFRenderInterpolationQualityKey", "__Internal")]
		NSString InterpolationQualityKey { get; }

		[Field ("PSPDFRenderSkipPageContentKey", "__Internal")]
		NSString SkipPageContentKey { get; }

		[Field ("PSPDFRenderOverlayAnnotationsKey", "__Internal")]
		NSString OverlayAnnotationsKey { get; }

		[Field ("PSPDFRenderSkipAnnotationArrayKey", "__Internal")]
		NSString SkipAnnotationArrayKey { get; }

		[Field ("PSPDFRenderIgnorePageClipKey", "__Internal")]
		NSString IgnorePageClipKey { get; }

		[Field ("PSPDFRenderAllowAntiAliasingKey", "__Internal")]
		NSString AllowAntiAliasingKey { get; }

		[Field ("PSPDFRenderPDFBoxKey", "__Internal")]
		NSString PdfBoxKey { get; }

		[Field ("PSPDFRenderDrawBlockKey", "__Internal")]
		NSString DrawBlockKey { get; }
	}

	interface IPSPDFRenderManager { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderManager {

		[Field ("PSPDFPageRendererPageInfoKey", "__Internal")]
		NSString PageInfoKey { get; }

		[Export ("setupGraphicsContext:rectangle:pageInfo:")]
		[Abstract]
		void Rectangle (CGContext context, CGRect displayRectangle, PSPDFPageInfo pageInfo);

		[Export ("renderQueue")]
		PSPDFRenderQueue GetRenderQueue ();

		[Export ("rendererInfo")]
		NSDictionary GetRendererInfo ();

		[Export ("renderer")]
		IPSPDFPageRenderer GetRenderer ();

		[Export ("setRenderer:")]
		void SetRenderer (IPSPDFPageRenderer renderer);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFPageInfo
	{

		[Export ("initWithPage:rect:rotation:documentProvider:")]
		IntPtr Constructor (nuint page, CGRect pageRect, nint rotation, PSPDFDocumentProvider documentProvider);

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("rect", ArgumentSemantic.Assign)]
		CGRect Rect { get; }

		[Export ("rotation", ArgumentSemantic.Assign)]
		nuint Rotation { get; }

		[Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary AdditionalActions { get; }

		[Export ("rotatedRect", ArgumentSemantic.Assign)]
		CGRect RotatedRect { get; }

		[Export ("rotationTransform", ArgumentSemantic.Assign)]
		CGAffineTransform RotationTransform { get; }

		[Export ("isEqualToPageInfo:")]
		bool IsEqualToPageInfo (PSPDFPageInfo otherPageInfo);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFRelayTouchesView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);
	}

	[BaseType (typeof(PSPDFScrollView))]
	interface PSPDFContentScrollView
	{

		[Export ("initWithTransitionViewController:")]
		IntPtr Constructor (PSPDFTransitionProtocol viewController);

		[Export ("contentController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTransitionProtocol ContentController { get; }
	}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFTransitionProtocol {

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; }

		[Export ("scrollView", ArgumentSemantic.Weak)]
		PSPDFContentScrollView ScrollView { get; set; }

		[Export ("setPage:animated:")]
		[Abstract]
		void SetPage (nuint page, bool animated);

		[Export ("visiblePageNumbers")]
		[Abstract]
		NSOrderedSet VisiblePageNumbers ();

		[Export ("pageViewForPage:")]
		[Abstract]
		PSPDFPageView PageViewForPage (nuint page);

		[Export ("visiblePageViewsForcingLayout:")]
		NSObject [] VisiblePageViewsForcingLayout (bool forcingLayout);

		[Export ("compensatedContentOffset")]
		CGPoint CompensatedContentOffset ();
	}


	[BaseType (typeof(UIPageViewController))]
	interface PSPDFPageViewController
	{

		[Export ("initWithPresentationContext:")]
		IntPtr Constructor (PSPDFPresentationContext presentationContext);

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; }

		[Export ("useSolidBackground", ArgumentSemantic.Assign)]
		bool UseSolidBackground { get; set; }

		[Export ("clipToPageBoundaries", ArgumentSemantic.Assign)]
		bool ClipToPageBoundaries { get; set; }
	}

	[BaseType (typeof(PSPDFModel))]
	interface PSPDFViewState
	{

		[Export ("initWithPage:")]
		IntPtr Constructor (nuint page);

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }

		[Export ("contentOffset", ArgumentSemantic.Assign)]
		CGPoint ContentOffset { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }
	}

	[BaseType (typeof(PSPDFModel))]
	interface PSPDFBookmark
	{

		[Export ("initWithPage:")]
		IntPtr Constructor (nuint page);

		[Export ("initWithAction:")]
		IntPtr Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction Action { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("name")]
		string Name { get; set; }

		[Export ("pageOrNameString")]
		string PageOrNameString { get; }
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFBookmarkParser
	{

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("bookmarks", ArgumentSemantic.Copy)]
		PSPDFBookmark [] Bookmarks { get; set; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("addBookmarkForPage:")]
		bool AddBookmark (nuint page);

		[Export ("removeBookmarkForPage:")]
		bool RemoveBookmark (nuint page);

		[Export ("clearAllBookmarksWithError:"), Internal]
		bool ClearAllBookmarksWithError (IntPtr error);

		[Export ("bookmarkForPage:")]
		PSPDFBookmark BookmarkForPage (nuint page);

		// PSPDFBookmarkParser (SubclassingHooks) Category

		[Export ("bookmarkQueue")]
		DispatchQueue BookmarkQueue { get; }

		[Export ("bookmarkPath")]
		string BookmarkPath { get; }

		[Export ("loadBookmarksWithError:"), Internal]
		PSPDFBookmark [] LoadBookmarksWithError (IntPtr error);

		[Export ("saveBookmarksWithError:"), Internal]
		bool SaveBookmarksWithError (IntPtr error);
	}

	interface IPSPDFRenderDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFRenderDelegate
	{

		[Export ("renderQueue:jobDidFinish:")]
		[Abstract]
		void JobDidFinish (PSPDFRenderQueue renderQueue, PSPDFRenderJob job);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFRenderQueue
	{

		[Field ("PSPDFRenderQueueDidEnqueueJob", "__Internal")]
		[Notification]
		NSString DidEnqueueJobNotification { get; }

		[Field ("PSPDFRenderQueueDidFinishJob", "__Internal")]
		[Notification]
		NSString DidFinishJobNotification { get; }

		[Field ("PSPDFRenderQueueDidCancelJob", "__Internal")]
		[Notification]
		NSString DidCancelJobNotification { get; }

		[Export ("minimumProcessPriority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority MinimumProcessPriority { get; set; }

		[Export ("concurrentRunningRenderRequests", ArgumentSemantic.Assign)]
		nuint ConcurrentRunningRenderRequests { get; set; }

		[Export ("requestRenderedImageForDocument:page:size:clippedToRect:annotations:options:priority:queueAsNext:delegate:completionBlock:")]
		PSPDFRenderJob RequestRenderedImageForDocument (PSPDFDocument document, nuint page, CGSize size, CGRect clipRect, PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options, PSPDFRenderQueuePriority priority, bool queueAsNext, IPSPDFRenderDelegate aDelegate, Action<PSPDFRenderJob, PSPDFRenderQueue> completionHandler);

		[Export ("renderJobsForDocument:page:delegate:")]
		PSPDFRenderJob [] RenderJobsForDocument (PSPDFDocument document, nuint page, IPSPDFRenderDelegate aDelegate);

		[Export ("hasRenderJobsForDelegate:")]
		bool HasRenderJobsForDelegate (IPSPDFRenderDelegate aDelegate);

		[Export ("numberOfQueuedJobs")]
		nuint NumberOfQueuedJobs { get; }

		[Export ("cancelJob:onlyIfQueued:")]
		bool CancelJob (PSPDFRenderJob job, bool onlyIfQueued);

		[Export ("cancelAllJobs")]
		void CancelAllJobs ();

		[Export ("cancelJobsForDocument:page:delegate:includeRunning:")]
		void CancelJobsForDocument (PSPDFDocument document, nuint page, IPSPDFRenderDelegate aDelegate, bool includeRunning);

		[Export ("cancelJobsForDelegate:")]
		void CancelJobsForDelegate (IPSPDFRenderDelegate aDelegate);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFRenderJob
	{

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; }

		[Export ("clipRect", ArgumentSemantic.Assign)]
		CGRect ClipRect { get; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		float ZoomScale { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("priority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority Priority { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFRenderDelegate Delegate { get; }

		[Export ("setCompletionBlock:", ArgumentSemantic.Copy)]
		void SetCompletionHandler (Action<PSPDFRenderJob, PSPDFRenderQueue> handler);

		[Export ("renderedImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage RenderedImage { get; }

		[Export ("renderReceipt", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRenderReceipt RenderReceipt { get; }

		[Export ("renderTime", ArgumentSemantic.Assign)]
		ulong RenderTime { get; }
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFRenderReceipt {

		[Export ("initWithDocument:page:size:clipRect:annotations:options:")]
		IntPtr Constructor (PSPDFDocument document, nuint page, CGSize size, CGRect clipRect, PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options);

		[Export ("renderFingerprint")]
		string RenderFingerprint { get; set; }

		[Export ("timeInNanoseconds", ArgumentSemantic.Assign)]
		double TimeInNanoseconds { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationStyle {

		[Export ("initWithName:")]
		IntPtr Constructor (string styleName);

		[Export ("styleName")]
		string StyleName { get; set; }

		[Export ("styleDictionary", ArgumentSemantic.Copy)]
		NSDictionary StyleDictionary { get; set; }

		[Export ("setStyle:forKey:")]
		void SetStyle (NSObject style, string key);

		[Export ("applyStyleToAnnotation:")]
		void ApplyStyleToAnnotation (PSPDFAnnotation annotation);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFStyleManager
	{
		[Field ("PSPDFStyleManagerLastUsedStylesKey", "__Internal")]
		NSString LastUsedStylesKey { get; }

		[Field ("PSPDFStyleManagerGenericStylesKey", "__Internal")]
		NSString ManagerGenericStylesKey { get; }

		[Export ("styleKeys", ArgumentSemantic.Copy)]
		NSSet StyleKeys { get; set; }

		[Export ("shouldUpdateDefaultsForAnnotationChanges", ArgumentSemantic.Assign)]
		bool ShouldUpdateDefaultsForAnnotationChanges { get; set; }

		[Export ("setupDefaultStylesIfNeeded")]
		void SetupDefaultStylesIfNeeded ();

		[Export ("stylesForKey:")]
		PSPDFAnnotationStyle [] StylesForKey (string key);

		[Export ("addStyle:forKey:")]
		void AddStyle (PSPDFAnnotationStyle style, string key);

		[Export ("removeStyle:forKey:")]
		void RemoveStyle (PSPDFAnnotationStyle style, string key);

		[Export ("lastUsedStyleForKey:")]
		PSPDFAnnotationStyle LastUsedStyleForKey (string key);

		[Export ("lastUsedProperty:forKey:")]
		NSObject LastUsedProperty (string styleProperty, string key);

		[Export ("setLastUsedValue:forProperty:forKey:")]
		void SetLastUsedValue (NSObject value, string styleProperty, string key);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFUndoController
	{
		[Field ("PSPDFUndoControllerAddedUndoActionNotification", "__Internal")]
		[Notification]
		NSString AddedUndoActionNotification { get; }

		[Export ("initWithUndoEnabled:")]
		IntPtr Constructor (bool undoEnabled);

		[Export ("undoEnabled", ArgumentSemantic.Assign)]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; }

		[Export ("undoManager", ArgumentSemantic.Strong), NullAllowed]
		NSUndoManager UndoManager { get; }

		[Export ("timedCoalescingInterval", ArgumentSemantic.Assign)]
		double TimedCoalescingInterval { get; set; }

		[Export ("levelsOfUndo", ArgumentSemantic.Assign)]
		nuint LevelsOfUndo { get; set; }

		[Export ("isWorking")]
		bool IsWorking { get; }

		[Export ("isUndoing")]
		bool IsUndoing { get; }

		[Export ("isRedoing")]
		bool IsRedoing { get; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("undo")]
		void Undo ();

		[Export ("redo")]
		void Redo ();

		[Export ("beginUndoGrouping")]
		void BeginUndoGrouping ();

		[Export ("endUndoGroupingWithName:")]
		void EndUndoGrouping (string groupName);

		[Export ("endUndoGroupingWithProperty:ofObject:")]
		void EndUndoGrouping (string changedProperty, NSObject obj);

		[Export ("removeAllActions")]
		void RemoveAllActions ();

		[Export ("removeAllActionsWithTarget:")]
		void RemoveAllActions (NSObject target);

		[Export ("registerObjectForUndo:")]
		void RegisterObjectForUndo (IPSPDFUndoProtocol obj);

		[Export ("unregisterObjectForUndo:")]
		void UnregisterObjectForUndo (IPSPDFUndoProtocol obj);

		[Export ("isObjectRegisteredForUndo:")]
		bool IsObjectRegisteredForUndo (IPSPDFUndoProtocol obj);

		[Export ("performBlockAsGroup:name:")]
		void PerformActionAsGroup (Action handler, string groupName);

		[Export ("performBlockWithoutUndo:")]
		void PerformActionWithoutUndo (Action block);

		[Export ("prepareWithInvocationTarget:block:")]
		void PrepareWithInvocationTarget (NSObject target, Action<NSObject> handler);

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

	interface IPSPDFFileManager { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFFileManager {

		[Export ("usesEncryption")]
		[Abstract]
		bool UsesEncryption ();

		[Export ("allowsSecurityException:")]
		[Abstract]
		bool AllowsSecurityException (PSPDFSecurityEvent exception);

		[Export ("copyFileToUnencryptedLocationIfRequired:securityException:error:")]
		[Abstract]
		NSUrl CopyFileToUnencryptedLocationIfRequired (NSUrl fileURL, PSPDFSecurityEvent securityException, out NSError error);

		[Export ("cleanupIfTemporaryFile:")]
		[Abstract]
		bool CleanupIfTemporaryFile (NSUrl URL);

		[Export ("libraryDirectory")]
		[Abstract]
		string LibraryDirectory ();

		[Export ("cachesDirectory")]
		[Abstract]
		string CachesDirectory ();

		[Export ("documentDirectory")]
		[Abstract]
		string DocumentDirectory ();

		[Export ("temporaryDirectoryWithUID:")]
		[Abstract]
		string TemporaryDirectoryWithUID (string UID);

		[Export ("unencryptedTemporaryDirectoryWithUID:")]
		[Abstract]
		string UnencryptedTemporaryDirectoryWithUID (string UID);

		[Export ("isNativePath:")]
		[Abstract]
		bool IsNativePath (string path);

		[Export ("fileExistsAtPath:")]
		[Abstract]
		bool FileExistsAtPath (string path);

		[Export ("fileExistsAtPath:isDirectory:")]
		[Abstract]
		bool FileExistsAtPath (string path, sbyte isDirectory);

		[Export ("fileExistsAtURL:")]
		[Abstract]
		bool FileExistsAtURL (NSUrl url);

		[Export ("fileExistsAtURL:isDirectory:")]
		[Abstract]
		bool FileExistsAtURL (NSUrl url, sbyte isDirectory);

		[Export ("createFileAtPath:contents:attributes:")]
		[Abstract]
		bool CreateFileAtPath (string path, NSData data, NSDictionary attr);

		[Export ("createDirectoryAtPath:withIntermediateDirectories:attributes:error:")]
		[Abstract]
		bool CreateDirectoryAtPath (string path, bool createIntermediates, NSDictionary attributes, out NSError error);

		[Export ("writeData:toFile:options:error:")]
		[Abstract]
		bool WriteData (NSData data, string path, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("dataWithContentsOfFile:options:error:")]
		[Abstract]
		NSData DataWithContentsOfFile (string path, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("copyItemAtURL:toURL:error:")]
		[Abstract]
		bool CopyItemAtURL (NSUrl srcURL, NSUrl dstURL, out NSError error);

		[Export ("moveItemAtURL:toURL:error:")]
		[Abstract]
		bool MoveItemAtURL (NSUrl srcURL, NSUrl dstURL, out NSError error);

		[Export ("removeItemAtPath:error:")]
		[Abstract]
		bool RemoveItemAtPath (string path, out NSError error);

		[Export ("removeItemAtURL:error:")]
		[Abstract]
		bool RemoveItemAtURL (NSUrl URL, out NSError error);

		[Export ("attributesOfFileSystemForPath:error:")]
		[Abstract]
		NSDictionary AttributesOfFileSystemForPath (string path, out NSError error);

		[Export ("attributesOfItemAtPath:error:")]
		[Abstract]
		NSDictionary AttributesOfItemAtPath (string path, out NSError error);

		[Export ("isDeletableFileAtPath:")]
		[Abstract]
		bool IsDeletableFileAtPath (string path);

		[Export ("isWritableFileAtPath:")]
		[Abstract]
		bool IsWritableFileAtPath (string path);

		[Export ("contentsOfDirectoryAtPath:error:")]
		[Abstract]
		NSObject [] ContentsOfDirectoryAtPath (string path, out NSError error);

		[Export ("subpathsOfDirectoryAtPath:error:")]
		[Abstract]
		NSObject [] SubpathsOfDirectoryAtPath (string path, out NSError error);

		[Export ("enumeratorAtPath:")]
		[Abstract]
		NSDirectoryEnumerator EnumeratorAtPath (string path);

		[Export ("destinationOfSymbolicLinkAtPath:error:")]
		[Abstract]
		string DestinationOfSymbolicLinkAtPath (string path, out NSError error);

		[Export ("fileSystemRepresentationForPath:")]
		[Abstract]
		sbyte FileSystemRepresentationForPath (string path);

		[Export ("fileHandleForReadingFromURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForReadingFromURL (NSUrl url, out NSError error, Func<NSFileHandle, bool> reader);

		[Export ("fileHandleForWritingToURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForWritingToURL (NSUrl url, out NSError error, Func<NSFileHandle, bool> writer);

		[Export ("fileHandleForUpdatingURL:error:withBlock:")]
		[Abstract]
		bool FileHandleForUpdatingURL (NSUrl url, out NSError error, Func<NSFileHandle, bool> updater);
	}


	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileCache
	{
		[Export ("directoryPath", ArgumentSemantic.Copy)]
		string DirectoryPath { get; }

		[Export ("diskSizeLimit", ArgumentSemantic.Assign)]
		nuint DiskSizeLimit { get; set; }

		[Export ("usedDiskSpace", ArgumentSemantic.Copy)]
		nuint UsedDiskSpace { get; }

		[Export ("initWithDirectory:fileManager:")]
		IntPtr Constructor (string directoryPath, IPSPDFFileManager fileManager);

		[Export ("setFile:remoteURL:")]
		void SetFile (NSUrl location, NSUrl url);

		[Export ("setData:remoteURL:")]
		void SetFile (NSData location, NSUrl url);

		[Export ("fileForRemoteURL:")]
		NSUrl FileForRemoteUrl (NSUrl url);

		[Export ("dataForRemoteURL:")]
		NSData DataForRemoteUrl (NSUrl url);

		[Export ("containsFileForRemoteURL:")]
		bool ContainsFileForRemoteUrl (NSUrl url);

		[Export ("removeFileForURL:")]
		void RemoveFile (NSUrl url);

		[Export ("removeAllFiles")]
		void RemoveAllFiles ();
	}

	delegate bool PSPDFDownloadManagerPredicateHandler (NSObject obj, nuint index, bool stop);

	[BaseType (typeof(NSObject))]
	interface PSPDFDownloadManager
	{

		[Export ("numberOfConcurrentDownloads", ArgumentSemantic.Assign)]
		nuint NumberOfConcurrentDownloads { get; set; }

		[Export ("enableDynamicNumberOfConcurrentDownloads", ArgumentSemantic.Assign)]
		bool EnableDynamicNumberOfConcurrentDownloads { get; set; }

		[Export ("delegate", ArgumentSemantic.Assign)]
		[NullAllowed]
		IPSPDFDownloadManagerDelegate Delegate { get; set; }

		[Export ("shouldFinishLoadingObjectsInBackground", ArgumentSemantic.Assign)]
		bool ShouldFinishLoadingObjectsInBackground { get; set; }

		[Export ("cache", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFileCache Cache { get; set; }

		[Export ("reachability", ArgumentSemantic.Assign)]
		PSPDFReachability Reachability { get; }

		[Export ("waitingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] WaitingObjects { get; }

		[Export ("loadingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] LoadingObjects { get; }

		[Export ("failedObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] FailedObjects { get; }

		[Export ("enqueueObject:")]
		void EnqueueObject (IPSPDFRemoteContentObject obj);

		[Export ("enqueueObject:atFront:")]
		void EnqueueObject (IPSPDFRemoteContentObject obj, bool enqueueAtFront);

		[Export ("enqueueObjects:")]
		void EnqueueObjects (IPSPDFRemoteContentObject[] objects);

		[Export ("enqueueObjects:atFront:")]
		void EnqueueObjects (IPSPDFRemoteContentObject[] objects, bool enqueueAtFront);

		[Export ("cancelObject:")]
		void CancelObject (IPSPDFRemoteContentObject obj);

		[Export ("cancelAllObjects")]
		void CancelAllObjects ();

		[Export ("objectsPassingTest:")]
		IPSPDFRemoteContentObject [] ObjectsPassingTest (PSPDFDownloadManagerPredicateHandler predicateHandler);

		[Export ("handlesObject:")]
		bool HandlesObject (IPSPDFRemoteContentObject obj);

		[Export ("stateForObject:")]
		PSPDFDownloadManagerObjectState StateForObject (IPSPDFRemoteContentObject obj);
	}

	interface IPSPDFDownloadManagerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFDownloadManagerDelegate
	{

		[Export ("downloadManager:didChangeObject:")]
		void DidChangeObject (PSPDFDownloadManager downloadManager, IPSPDFRemoteContentObject obj);

		[Export ("downloadManager:reachabilityDidChange:")]
		void ReachabilityDidChange (PSPDFDownloadManager downloadManager, PSPDFReachability reachability);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFRemoteFileObject : IPSPDFRemoteContentObject
	{

		[Export ("initWithRemoteURL:targetURL:")]
		IntPtr Constructor (NSUrl remoteUrl, NSUrl targetFileUrl);

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

		[Export ("userInfo", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary UserInfo { get; set; }

		[Export ("setCompletionBlock:")]
		void SetCompletionHandler (Action<IPSPDFRemoteContentObject> completionHandler);
	}

	interface IPSPDFSearchHighlightViewManagerDataSource
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFSearchHighlightViewManagerDataSource : PSPDFOverridable
	{

		[Export ("shouldHighlightSearchResults")]
		[Abstract]
		bool ShouldHighlightSearchResults ();

		[Export ("visiblePageViews")]
		[Abstract]
		PSPDFPageView [] VisiblePageViews ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFSearchHighlightViewManager
	{

		[Export ("initWithDataSource:")]
		IntPtr Constructor (IPSPDFSearchHighlightViewManagerDataSource dataSource);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSearchHighlightViewManagerDataSource DataSource { get; }

		[Export ("hasVisibleSearchResults")]
		bool HasVisibleSearchResults { get; }

		[Export ("clearHighlightedSearchResultsAnimated:")]
		void ClearHighlightedSearchResults (bool animated);

		[Export ("addHighlightSearchResults:animated:")]
		void AddHighlightSearchResults (PSPDFSearchResult[] searchResults, bool animated);

		[Export ("animateSearchHighlight:")]
		void AnimateSearchHighlight (PSPDFSearchResult searchResult);
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFTableViewCell {

		[Export ("performBlockWithoutAnimationIfResizingPopover:")]
		void PerformActionWithoutAnimationIfResizingPopover (Action action);
	}


	[BaseType (typeof(PSPDFTableViewCell))]
	interface PSPDFSpinnerCell
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		// PSPDFSpinnerCell (SubclassingHooks) Category

		[Export ("spinner", ArgumentSemantic.Strong), NullAllowed]
		UIActivityIndicatorView Spinner { get; }

		[Export ("alignTextLabel")]
		void AlignTextLabel ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFAnnotationViewCache
	{

		[Export ("initWithPDFController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("prepareAnnotationViewForAnnotation:frame:pageView:")]
		IPSPDFAnnotationViewProtocol PrepareAnnotationView (PSPDFAnnotation annotation, CGRect annotationRect, PSPDFPageView pageView);

		[Export ("recycleAnnotationView:")]
		void RecycleAnnotationView (IPSPDFAnnotationViewProtocol annotationView);

		[Export ("dequeueViewFromCacheForAnnotation:class:")]
		IPSPDFAnnotationViewProtocol DequeueViewFromCache (PSPDFAnnotation annotation, Class annotationViewClass);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; set; }

		// PSPDFAnnotationViewCache (SubclassingHooks) Category

		[Export ("createAnnotationViewForAnnotation:frame:")]
		IPSPDFAnnotationViewProtocol CreateAnnotationView (PSPDFAnnotation annotation, CGRect annotationRect);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFPopOutMenu
	{

		[Export ("anchorFrame", ArgumentSemantic.Assign)]
		CGRect AnchorFrame { get; set; }

		[Export ("items", ArgumentSemantic.Copy)]
		UIView [] Items { get; set; }

		[Export ("selectedItem", ArgumentSemantic.Strong), NullAllowed]
		UIView SelectedItem { get; set; }

		[Export ("selectedItemIndex", ArgumentSemantic.Assign)]
		nuint SelectedItemIndex { get; set; }

		[Export ("expanded", ArgumentSemantic.Assign)]
		bool Expanded { [Bind ("isExpanded")] get; set; }

		[Export ("expansionDirection", ArgumentSemantic.Assign)]
		PSPDFPopOutMenuExpansionDirection ExpansionDirection { get; set; }

		[Export ("itemPadding", ArgumentSemantic.Assign)]
		nfloat ItemPadding { get; set; }

		[Export ("setExpanded:animated:")]
		void SetExpanded (bool expanded, bool animated);

		[Export ("toggleAnimated:")]
		void Toggle (bool animated);
	}

	interface IPSPDFSinglePageViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFSinglePageViewControllerDelegate
	{

		[Export ("singlePageViewControllerReadyForReuse:")]
		[Abstract]
		void ReadyForReuse (PSPDFSinglePageViewController singlePageViewController);

		[Export ("singlePageViewControllerWillDealloc:")]
		[Abstract]
		void WillDealloc (PSPDFSinglePageViewController singlePageViewController);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFSinglePageViewController
	{

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSinglePageViewControllerDelegate Delegate { get; set; }

		[Export ("prepareForReuse")]
		void PrepareForReuse ();

		[Export ("useSolidBackground", ArgumentSemantic.Assign)]
		bool UseSolidBackground { get; set; }

		// PSPDFSinglePageViewController (SubclassingHooks) Category

		[Export ("pageView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageView PageView { get; }

		[Export ("layoutPage")]
		void LayoutPage ();
	}

	[BaseType (typeof(UIScrollView))]
	interface PSPDFPagingScrollView
	{

	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFPageScrollViewController
	{

		[Export ("initWithPresentationContext:")]
		IntPtr Constructor (PSPDFPresentationContext presentationContext);

		[Export ("presentationContext", ArgumentSemantic.Weak), NullAllowed]
		PSPDFPresentationContext PresentationContext { get; }

		[Export ("pagingScrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView PagingScrollView { get; }

		[Export ("visiblePageNumbers")]
		NSOrderedSet VisiblePageNumbers ();

		[Export ("pageViewForPage:")]
		PSPDFPageView PageViewForPage (nuint page);

		[Export ("setPage:animated:")]
		void SetPage (nuint page, bool animated);

		[Export ("reloadData")]
		void ReloadData ();
	}

	interface IPSPDFMultiDocumentViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFMultiDocumentViewControllerDelegate
	{

		[Export ("multiPDFController:shouldChangeDocuments:")]
		bool ShouldChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument[] newDocuments);

		[Export ("multiPDFController:didChangeDocuments:")]
		void DidChangeDocuments (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument[] oldDocuments);

		[Export ("multiPDFController:shouldChangeVisibleDocument:")]
		bool ShouldChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument newDocument);

		[Export ("multiPDFController:didChangeVisibleDocument:")]
		void DidChangeVisibleDocument (PSPDFMultiDocumentViewController multiPDFController, PSPDFDocument oldDocument);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFMultiDocumentViewController
	{

		[Export ("initWithPDFViewController:")]
		IntPtr Constructor ([NullAllowed] PSPDFViewController pdfController);

		[Export ("visibleDocument", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument VisibleDocument { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFMultiDocumentViewControllerDelegate Delegate { get; set; }

		[Export ("enableAutomaticStatePersistence", ArgumentSemantic.Assign)]
		bool EnableAutomaticStatePersistence { get; set; }

		[Export ("statePersistenceKey")]
		string StatePersistenceKey { get; set; }

		[Export ("pdfController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFViewController PdfController { get; }

		[Export ("changeDocumentOnTapPageEndMargin", ArgumentSemantic.Assign)]
		bool ChangeDocumentOnTapPageEndMargin { get; set; }

		[Export ("multiDocumentThumbnails", ArgumentSemantic.Assign)]
		bool MultiDocumentThumbnails { get; set; }

		[Export ("showTitle", ArgumentSemantic.Assign)]
		bool ShowTitle { get; set; }

		[Export ("addDocuments:atIndex:")]
		void AddDocuments (PSPDFDocument[] documents, nuint index);

		[Export ("removeDocuments:")]
		void RemoveDocuments (PSPDFDocument[] documents);

		[Export ("persistState")]
		void PersistState ();

		[Export ("restoreState")]
		bool RestoreState ();

		[Export ("restoreStateAndMergeWithDocuments:")]
		bool RestoreState (PSPDFDocument[] documents);

		// PSPDFMultiDocumentViewController (SubclassingHooks) Category

		[Advice ("Requires base call if overridden")]
		[Export ("commonInitWithPDFController:")]
		void CommonInitWithPdfController (PSPDFViewController pdfController);

		[Export ("swizzlePDFController:")]
		void SwizzlePdfController (PSPDFViewController pdfController);
	}

	[BaseType (typeof(QLPreviewController))]
	interface PSPDFFilePreviewController
	{

		[Export ("initWithPreviewURL:")]
		IntPtr Constructor (NSUrl previewUrl);

		[Export ("previewURL", ArgumentSemantic.Copy)]
		NSUrl PreviewUrl { get; set; }

		[Export ("sourceRect", ArgumentSemantic.Assign)]
		CGRect SourceRect { get; set; }
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFBrightnessViewController
	{

		[Export ("wantsSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsSoftwareDimming { get; set; }

		[Export ("wantsAdditionalSoftwareDimming", ArgumentSemantic.Assign)]
		bool WantsAdditionalSoftwareDimming { get; set; }

		[Export ("additionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		nfloat AdditionalBrightnessDimmingFactor { get; set; }

		[Export ("maximumAdditionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		nfloat MaximumAdditionalBrightnessDimmingFactor { get; set; }

		// PSPDFBrightnessViewController (SubclassingHooks) Category

		[Export ("slider", ArgumentSemantic.Strong), NullAllowed]
		UISlider Slider { get; set; }

		[Export ("gradient", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGradientView Gradient { get; set; }

		[Export ("dimmingView")]
		PSPDFDimmingView DimmingView { get; }

		[Export ("addDimmingView")]
		PSPDFDimmingView AddDimmingView { get; }

		[Export ("removeDimmingView")]
		void RemoveDimmingView ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGradientView {

		[Export ("direction", ArgumentSemantic.Assign)]
		PSPDFGradientViewDirection Direction { get; set; }

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; set; }

		[Export ("locations", ArgumentSemantic.Copy)]
		NSValue [] Locations { get; set; }
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFAnnotationGridViewController : IPSPDFStyleable {

	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFBaseViewController {

	}


	[BaseType (typeof(UIView))]
	interface PSPDFDimmingView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("additionalBrightnessDimmingFactor", ArgumentSemantic.Assign)]
		nfloat AdditionalBrightnessDimmingFactor { get; set; }
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


	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFTextStampViewController : PSPDFColorSelectionViewControllerDelegate, IUITextFieldDelegate {

		[Export ("initWithStampAnnotation:")]
		IntPtr Constructor (PSPDFStampAnnotation stampAnnotation);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFTextStampViewControllerDelegate Delegate { get; set; }

		[Export ("stampAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStampAnnotation StampAnnotation { get; }

		[Export ("defaultStampText")]
		string DefaultStampText { get; set; }
	}


	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFAnnotationGridViewController))]
	interface PSPDFStampViewController : IPSPDFAnnotationGridViewControllerDataSource, IPSPDFTextStampViewControllerDelegate
	{

		[Static, Export ("defaultStampAnnotations")]
		PSPDFStampAnnotation [] DefaultStampAnnotations { get; set; }

		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFAnnotationGridViewControllerDelegate aDelegate);

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

		[Export ("isInPopover")]
		bool IsInPopover ();

		[Export ("setIsInPopover:")]
		void IsInPopover (bool isInPopover);

		[Export ("parentPopoverController")]
		UIPopoverController GetParentPopoverController ();

		[Export ("setParentPopoverController:")]
		void SetParentPopoverController (UIPopoverController controller);

		[Export ("prefersStatusBarHidden")]
		bool GetPrefersStatusBarHidden ();

		[Export ("setPrefersStatusBarHidden:")]
		void SetPrefersStatusBarHidden (bool val);
	}


	interface IPSPDFSignatureViewControllerDelegate	{}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFSignatureViewControllerDelegate : PSPDFOverridable
	{

		[Export ("signatureViewControllerDidCancel:")]
		void SignatureViewControllerDidCancel (PSPDFSignatureViewController signatureController);

		[Export ("signatureViewControllerDidSave:")]
		void SignatureViewControllerDidSave (PSPDFSignatureViewController signatureController);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFSignatureViewController : IPSPDFStyleable
	{

		[Field ("PSPDFSignatureControllerShouldSaveKey", "__Internal")]
		NSString ShouldSaveKey { get; }

		[Field ("PSPDFSignatureControllerTargetRectKey", "__Internal")]
		NSString TargetRectKey { get; }

		[Export ("lines", ArgumentSemantic.Strong), NullAllowed]
		NSValue [] Lines { get; }

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("menuColors", ArgumentSemantic.Copy)]
		UIColor [] MenuColors { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSignatureViewControllerDelegate Delegate { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary UserInfo { get; set; }

		[Export ("drawView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDrawView DrawView { get; }

		[Export ("clearButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton ClearButton { get; set; }

		[Export ("colorMenu", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPopOutMenu ColorMenu { get; set; }

		[Export ("backgroundView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSignatureBackgroundView BackgroundView { get; set; }

		[Export ("keepLandscapeAspectRatio", ArgumentSemantic.Assign)]
		bool KeepLandscapeAspectRatio { get; set; }

		// PSPDFSignatureViewController (SubclassingHooks) Category

		[Export ("cancel:")]
		void Cancel (NSObject sender);

		[Export ("done:")]
		void Done (NSObject sender);

		[Export ("clear:")]
		void Clear (NSObject sender);

		[Export ("color:")]
		void Color (PSPDFColorButton sender);

		[Export ("colorButtonForColor:")]
		PSPDFColorButton ColorButtonForColor (UIColor color);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFSignatureBackgroundView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("signatureLine", ArgumentSemantic.Strong), NullAllowed]
		UIView SignatureLine { get; }

		[Export ("signatureLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel SignatureLabel { get; }

		[Export ("topSeparator", ArgumentSemantic.Strong), NullAllowed]
		UIView TopSeparator { get; }

		[Export ("bottomSeparator", ArgumentSemantic.Strong), NullAllowed]
		UIView BottomSeparator { get; }
	}

	interface IPSPDFFontPickerViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFFontPickerViewControllerDelegate : PSPDFOverridable
	{

		[Export ("fontPickerViewController:didSelectFont:")]
		[Abstract]
		void DidSelectFont (PSPDFFontPickerViewController fontPickerViewController, UIFont selectedFont);
	}

	[BaseType (typeof(PSPDFBaseTableViewController))]
	interface PSPDFFontPickerViewController
	{

		[Export ("initWithFontFamilyDescriptors:")]
		IntPtr Constructor ([NullAllowed] UIFontDescriptor[] fontFamilyDescriptors);

		[Export ("fontFamilyDescriptors", ArgumentSemantic.Copy), NullAllowed]
		UIFontDescriptor [] FontFamilyDescriptors { get; }

		[Export ("selectedFont", ArgumentSemantic.Strong), NullAllowed]
		UIFont SelectedFont { get; set; }

		[Export ("searchEnabled", ArgumentSemantic.Assign)]
		bool SearchEnabled { get; set; }

		[Export ("showDownloadableFonts", ArgumentSemantic.Assign)]
		bool ShowDownloadableFonts { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFontPickerViewControllerDelegate Delegate { get; set; }

		// UIFontDescriptor (Blacklisting) Category

		[Static, Export ("setPSPDFDefaultBlacklist:")]
		void SetDefaultBlacklist (string[] defaultBlacklist);

		[Static, Export ("pspdf_defaultBlacklist")]
		string [] DefaultBlacklist { get; }
	}

	interface IPSPDFColorSelectionViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFColorSelectionViewControllerDelegate
	{

		[Export ("colorSelectionControllerSelectedColor:context:")]
		[Abstract]
		UIColor SelectedColor (UIViewController controller, NSObject context);

		[Export ("colorSelectionController:didSelectColor:finishedSelection:context:")]
		[Abstract]
		void DidSelectColor (UIViewController controller, UIColor color, bool finished, CGContext context);
	}

	[BaseType (typeof (PSPDFBaseViewController))]
	interface PSPDFSimplePageViewController {

		[Export ("initWithViewControllers:")]
		IntPtr Constructor (UIViewController [] viewControllers);

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("setPage:animated:")]
		void SetPage (nuint page, bool animated);

		[Export ("scrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView ScrollView { get; }

		[Export ("pageControl", ArgumentSemantic.Strong), NullAllowed]
		UIPageControl PageControl { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof(UIViewController))]
	interface PSPDFColorSelectionViewController
	{
		[Static, Export ("defaultColorPickerWithTitle:wantTransparency:delegate:context:")]
		PSPDFSimplePageViewController DefaultColorPicker (string title, bool wantTransparency, IPSPDFColorSelectionViewControllerDelegate aDelegate, NSObject context);

		[Static, Export ("setDefaultColorPickerStyles:")]
		void SetDefaultColorPickerStyles (NSNumber[] colorPickerStyles);

		[Static, Export ("monoChromeSelectionViewController")]
		PSPDFColorSelectionViewController MonoChromeSelectionViewController  { get; }

		[Static, Export ("modernColorsSelectionViewController")]
		PSPDFColorSelectionViewController ModernColorsSelectionViewController { get; }

		[Static, Export ("vintageColorsSelectionViewController")]
		PSPDFColorSelectionViewController VintageColorsSelectionViewController { get; }

		[Static, Export ("rainbowSelectionViewController")]
		PSPDFColorSelectionViewController RainbowSelectionViewController { get; }

		[Static, Export ("colorSelectionViewControllerFromColors:addDarkenedVariants:")]
		PSPDFColorSelectionViewController FromColors (UIColor[] colors, bool darkenedVariants);

		[Static, Export ("colorsFromPaletteURL:addDarkenedVariants:")]
		UIColor [] ColorsFromPaletteUrl (NSUrl paletteUrl, bool darkenedVariants);

		[Export ("initWithColors:")]
		IntPtr Constructor (UIColor[] colors);

		[Export ("colors", ArgumentSemantic.Copy)]
		UIColor [] Colors { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFColorSelectionViewControllerDelegate Delegate { get; set; }

		// PSPDFColorSelectionViewController (SubclassingHooks) Category

		[Static, Export ("colorPickerForType:")]
		UIViewController ColorPickerForType (PSPDFColorPickerStyle type);
	}

	interface IPSPDFNoteAnnotationViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFNoteAnnotationViewControllerDelegate : PSPDFOverridable
	{

		[Export ("noteAnnotationController:didDeleteAnnotation:")]
		void DidDeleteAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didClearContentsForAnnotation:")]
		void DidClearContentsForAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:didChangeAnnotation:")]
		void DidChangeAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);

		[Export ("noteAnnotationController:willDismissWithAnnotation:")]
		void WillDismissWithAnnotation (PSPDFNoteAnnotationViewController noteController, PSPDFAnnotation annotation);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFNoteAnnotationViewController : IPSPDFStyleable
	{

		[Export ("initWithAnnotation:editable:")]
		IntPtr Constructor (PSPDFAnnotation annotation, bool allowEditing);

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
		string DeleteAnnotationActionTitle ();

		[Export ("beginEditing")]
		bool BeginEditing ();

		[Advice ("Requires base call if overridden")]
		[Export ("updateTextView")]
		void UpdateTextView ();

		[Export ("backgroundView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGradientView BackgroundView { get; set; }

		[Export ("optionsView", ArgumentSemantic.Strong), NullAllowed]
		UIView OptionsView { get; set; }

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BorderColor { get; set; }

		[Export ("tapGesture", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer TapGesture { get; set; }
	}

	interface IPSPDFAnnotationTableViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFAnnotationTableViewControllerDelegate : PSPDFOverridable
	{

		[Export ("annotationTableViewController:didSelectAnnotation:")]
		[Abstract]
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


	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFStatefulTableViewController))]
	interface PSPDFAnnotationTableViewController : IPSPDFStyleable
	{

		[Export ("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, IPSPDFAnnotationTableViewControllerDelegate aDelegate);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("visibleAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSOrderedSet VisibleAnnotationTypes { get; set; }

		[Export ("allowCopy", ArgumentSemantic.Assign)]
		bool AllowCopy { get; set; }

		[Export ("showDeleteAllOption", ArgumentSemantic.Assign)]
		bool ShowDeleteAllOption { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFAnnotationTableViewControllerDelegate Delegate { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFAnnotationTableViewController (SubclassingHooks) Category

		[Export ("annotationsForPage:")]
		PSPDFAnnotation [] AnnotationsForPage (nuint page);

		[Export ("annotationForIndexPath:")]
		PSPDFAnnotation AnnotationForIndexPath (NSIndexPath indexPath);

		[Export ("targetTableViewStyle")]
		UITableViewStyle TargetTableViewStyle ();

		[Export ("deleteAllAction:")]
		void DeleteAllAction (NSObject sender);
	}

	interface IPSPDFAnnotationSetStore
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFAnnotationSetStore
	{

		[Export ("annotationSets")]
		PSPDFAnnotationSet [] GetAnnotationSets ();

		[Export ("setAnnotationSets:")]
		void SetAnnotationSets (PSPDFAnnotationSet[] annotationSets);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFKeychainAnnotationSetsStore : IPSPDFAnnotationSetStore
	{

	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFAnnotationGridViewController))]
	interface PSPDFSavedAnnotationsViewController : IPSPDFAnnotationGridViewControllerDataSource, IPSPDFStyleable
	{

		[Static, Export ("sharedAnntationStore")]
		IPSPDFAnnotationSetStore SharedAnnotationStore ();

		[Export ("initWithDelegate:")]
		IntPtr Constructor (IPSPDFAnnotationGridViewControllerDelegate aDelegate);

		[Export ("annotationStore", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFAnnotationSetStore AnnotationStore { get; set; }

		// PSPDFSavedAnnotationsViewController (SubclassingHooks) Category

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbarAnimated (bool animated);
	}

	interface IPSPDFContainerViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFContainerViewControllerDelegate
	{

		[Export ("containerViewController:didUpdateSelectedIndex:")]
		void DidUpdateSelectedIndex (PSPDFContainerViewController controller, nuint selectedIndex);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFContainerViewController : IPSPDFStyleable
	{

		[Export ("initWithControllers:titles:delegate:")]
		IntPtr Constructor (UIViewController[] controllers, string[] titles, IPSPDFContainerViewControllerDelegate aDelegate);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFContainerViewControllerDelegate Delegate { get; set; }

		[Export ("viewControllers", ArgumentSemantic.Copy)]
		UIViewController [] ViewControllers { get; }

		[Export ("visibleViewControllerIndex", ArgumentSemantic.Assign)]
		nuint VisibleViewControllerIndex { get; set; }

		[Export ("shouldAnimateChanges", ArgumentSemantic.Assign)]
		bool ShouldAnimateChanges { get; set; }

		[Export ("addViewController:withTitle:")]
		void AddViewController (UIViewController controller, string title);

		[Export ("addViewController:")]
		void AddViewController (UIViewController controller);

		[Export ("removeViewController:")]
		void RemoveViewController (UIViewController controller);

		[Export ("setVisibleViewControllerIndex:animated:")]
		void SetVisibleViewControllerIndex (nuint visibleViewControllerIndex, bool animated);

		// PSPDFSavedAnnotationsViewController (SubclassingHooks) Category

		[Export ("filterSegment", ArgumentSemantic.Strong), NullAllowed]
		UISegmentedControl FilterSegment { get; }
	}

	interface IPSPDFWebViewControllerDelegate
	{

	}

	delegate void PSPDFWebViewControllerDelegateHandleExternalUrlHandler (PSPDFAlertView alert, nuint buttonIndex);

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFWebViewControllerDelegate
	{

		[Export ("handleExternalURL:buttonCompletionBlock:")]
		bool ButtonCompletionHandler (NSUrl url, PSPDFWebViewControllerDelegateHandleExternalUrlHandler completionHandler);
	}

	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFWebViewController : IPSPDFStyleable
	{

		[Static, Export ("modalWebViewWithURL:")]
		UINavigationController GetModalWebView (NSUrl url);

		[Export ("initWithURLRequest:")]
		IntPtr Constructor (NSUrlRequest request);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("availableActions", ArgumentSemantic.Assign)]
		PSPDFWebViewControllerAvailableActions AvailableActions { get; set; }

		[Export ("popoverController", ArgumentSemantic.Strong), NullAllowed]
		UIPopoverController PopoverController { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFWebViewControllerDelegate Delegate { get; set; }

		[Export ("updateGlobalActivityIndicator", ArgumentSemantic.Assign)]
		bool UpdateGlobalActivityIndicator { get; set; }

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

		// PSPDFWebViewController (SubclassingHooks) Category

		[Export ("webView", ArgumentSemantic.Strong), NullAllowed]
		UIView WebView { get; }

		[Export ("setActivityIndicatorEnabled:")]
		void SetActivityIndicatorEnabled (bool enabled);

		[Export ("showHTMLWithError:")]
		void ShowHtml (NSError error);

		[Export ("createDefaultActivityViewController")]
		UIActivityViewController CreateDefaultActivityViewController ();

		[Export ("goBack:")]
		void GoBack (NSObject sender);

		[Export ("goForward:")]
		void GoForward (NSObject sender);

		[Export ("reload:")]
		void Reload (NSObject sender);

		[Export ("stop:")]
		void Stop (NSObject sender);

		[Export ("action:")]
		void Action (NSObject sender);

		[Export ("done:")]
		void Done (NSObject sender);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFSoundAnnotationController
	{

		[Field ("PSPDFSoundAnnotationChangedStateNotification", "__Internal")]
		[Notification]
		NSString ChangedStateNotification { get; }

		[Field ("PSPDFSoundAnnotationStopAll", "__Internal")]
		[Notification]
		NSString StopAllNotification { get; }

		[Static, Export ("stopRecordingOrPlaybackForAllExcept:")]
		void StopRecordingOrPlaybackForAllExcept (NSObject sender);

		[Static, Export ("requestRecordPermission:")]
		void RequestRecordPermission (Action<bool> hanlder);

		[Export ("initWithSoundAnnotation:")]
		IntPtr Constructor (PSPDFSoundAnnotation annot);

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFSoundAnnotation Annotation { get; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFSoundAnnotationState State { get; }

		[Export ("audioPlayer", ArgumentSemantic.Strong), NullAllowed]
		AVAudioPlayer AudioPlayer { get; }

		[Export ("startPlayback:")]
		bool StartPlayback (out NSError error);

		[Export ("startRecording:")]
		bool StartRecording (out NSError error);

		[Export ("pause")]
		void Pause ();

		[Export ("discardRecording")]
		void DiscardRecording ();

		[Export ("stop:")]
		bool Stop (out NSError error);
	}

	[BaseType (typeof(UINavigationController))]
	interface PSPDFNavigationController
	{

		[Export ("rotationForwardingEnabled", ArgumentSemantic.Assign)]
		bool RotationForwardingEnabled { [Bind ("isRotationForwardingEnabled")] get; set; }

		[Export ("persistentCloseButtonMode", ArgumentSemantic.Assign)]
		PSPDFPersistentCloseButtonMode PersistentCloseButtonMode { get; set; }

		[Export ("persistentCloseButton", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem PersistentCloseButton { get; set; }
	}

	interface IPSPDFColorPickerViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFColorPickerViewDelegate {

		[Export ("colorPickerDidChangeSelection:finishedSelection:")]
		void FinishedSelection (PSPDFColorPickerView colorPicker, bool finished);
	}

	[BaseType (typeof (UISlider))]
	interface PSPDFBrightnessSlider {

		[Export ("backgroundStyle", ArgumentSemantic.Assign)]
		PSPDFSliderBackgroundStyle BackgroundStyle { get; set; }

		[Export ("colorPicker", ArgumentSemantic.Weak)]
		PSPDFColorPickerView ColorPicker { get; set; }

		[Export ("updateBackground")]
		void UpdateBackground ();
	}


	[BaseType (typeof (UIControl))]
	interface PSPDFColorPickerView {

		[Export ("selection", ArgumentSemantic.Assign)]
		CGPoint Selection { get; }

		[Export ("cropToCircle", ArgumentSemantic.Assign)]
		bool CropToCircle { get; set; }

		[Export ("isOrthogonal", ArgumentSemantic.Assign)]
		bool IsOrthogonal { get; set; }

		[Export ("loupeEnabled", ArgumentSemantic.Assign)]
		bool LoupeEnabled { [Bind ("isLoupeEnabled")] get; set; }

		[Export ("selectionColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("brightness", ArgumentSemantic.Assign)]
		nfloat Brightness { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFColorPickerViewDelegate Delegate { get; set; }

		[Export ("brightnessSlider", ArgumentSemantic.Weak)]
		PSPDFBrightnessSlider BrightnessSlider { get; set; }

		[Export ("selectionToHue:saturation:brightness:")]
		void SelectionToHue (float pH, float pS, float pV);

		[Export ("colorAtPoint:")]
		UIColor ColorAtPoint (CGPoint point);
	}


	[BaseType (typeof(PSPDFBaseViewController))]
	interface PSPDFHSVColorPickerController
	{

		[Export ("selectionColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("margin", ArgumentSemantic.Assign)]
		UIEdgeInsets Margin { get; set; }

		[Export ("colorPicker", ArgumentSemantic.Strong), NullAllowed]
		PSPDFColorPickerView ColorPicker { get; }

		[Export ("brightnessSlider", ArgumentSemantic.Strong), NullAllowed]
		PSPDFBrightnessSlider BrightnessSlider { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFColorSelectionViewControllerDelegate Delegate { get; set; }
	}

	[BaseType (typeof(UIImagePickerController))]
	interface PSPDFImagePickerController
	{

	}

	interface IPSPDFJSONSerializing { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFJSONSerializing {

		[Export ("JSONKeyPathsByPropertyKey")]
		[Abstract]
		NSDictionary JSONKeyPathsByPropertyKey ();

		[Export ("JSONTransformerForKey:")]
		NSObject JSONTransformerForKey (string key);

		[Export ("classForParsingJSONDictionary:")]
		Class ClassForParsingJSONDictionary (NSDictionary JSONDictionary);
	}


	[Static]
	interface PSPDFActionOption
	{
		[Field ("PSPDFActionOptionModal", "__Internal")]
		NSString Modal { get; }

		[Field ("PSPDFActionOptionAutoplay", "__Internal")]
		NSString Autoplay { get; }

		[Field ("PSPDFActionOptionControls", "__Internal")]
		NSString Controls { get; }

		[Field ("PSPDFActionOptionLoop", "__Internal")]
		NSString Loop { get; }

		[Field ("PSPDFActionOptionOffset", "__Internal")]
		NSString Offset { get; }

		[Field ("PSPDFActionOptionSize", "__Internal")]
		NSString Size { get; }

		[Field ("PSPDFActionOptionPopover", "__Internal")]
		NSString Popover { get; }

		[Field ("PSPDFActionOptionCover", "__Internal")]
		NSString Cover { get; }

		[Field ("PSPDFActionOptionPage", "__Internal")]
		NSString Page { get; }

		[Field ("PSPDFActionOptionButton", "__Internal")]
		NSString Button { get; }

		[Field ("PSPDFActionOptionCloseButton", "__Internal")]
		NSString CloseButton { get; }
	}

	[BaseType (typeof(PSPDFModel))]
	interface PSPDFAction : IPSPDFJSONSerializing {

		[Field ("PSPDFActionTypeTransformerName", "__Internal")]
		NSString ActionTypeTransformerName { get; }

		[Static, Export ("actionClassForType:")]
		Class ActionClassForType (PSPDFActionType actionType);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFActionType Type { get; }

		[Export ("nextAction", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction NextAction { get; set; }

		[Export ("options", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary Options { get; }

		[Export ("localizedDescriptionWithDocumentProvider:")]
		string LocalizedDescription (PSPDFDocumentProvider documentProvider);
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFGoToAction
	{

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (nuint pageIndex);

		[Export ("initWithNamedDestination:")]
		IntPtr Constructor (string namedDestination);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		nuint PageIndex { get; set; }

		[Export ("namedDestination")]
		string NamedDestination { get; }

		[Export ("resolveNamedDestionationWithDocumentProvider:")]
		bool ResolveNamedDestination (PSPDFDocumentProvider documentProvider);

		[Static, Export ("resolveActionsWithNamedDestinations:documentRef:"), Internal]
		nuint ResolveActionsWithNamedDestinations (PSPDFGoToAction[] actions, IntPtr documentRef);
	}

	[BaseType (typeof(PSPDFGoToAction))]
	interface PSPDFRemoteGoToAction
	{

		[Export ("initWithRemotePath:pageIndex:")]
		IntPtr Constructor (string remotePath, nuint pageIndex);

		[Export ("relativePath")]
		string RelativePath { get; }
	}

	[BaseType (typeof(PSPDFGoToAction))]
	interface PSPDFEmbeddedGoToAction
	{

		[Export ("initWithRemotePath:targetRelationship:openInNewWindow:pageIndex:")]
		IntPtr Constructor (string remotePath, PSPDFEmbeddedGoToActionTarget targetRelationship, bool openInNewWindow, nuint pageIndex);

		[Export ("targetRelationship", ArgumentSemantic.Assign)]
		PSPDFEmbeddedGoToActionTarget TargetRelationship { get; }

		[Export ("relativePath")]
		string RelativePath { get; }

		[Export ("openInNewWindow", ArgumentSemantic.Assign)]
		bool OpenInNewWindow { get; }
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFURLAction
	{

		[Export ("initWithURLString:")]
		IntPtr Constructor (string urlString);

		[Export ("initWithURL:options:")]
		IntPtr Constructor (NSUrl url, NSDictionary options);

		[Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("unmodifiedURL", ArgumentSemantic.Copy)]
		NSUrl UnmodifiedUrk { get; }

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

		[Export ("emailURL", ArgumentSemantic.Assign)]
		bool EmailUrl { [Bind ("isEmailURL")] get; }

		[Export ("updateURLWithAnnotationManager:")]
		bool UpdateUrl (PSPDFAnnotationManager annotationManager);

		[Export ("prefixedURLStringWithAnnotationManager:")]
		string PrefixedUrlString (PSPDFAnnotationManager annotationManager);

		[Export ("configureMailComposeViewController:")]
		bool ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController);
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFNamedAction
	{

		[Field ("PSPDFNamedActionTypeTransformerName", "__Internal")]
		NSString TypeTransformerName { get; }

		[Export ("initWithActionNamedString:")]
		IntPtr Constructor (string actionNameString);

		[Export ("namedActionType", ArgumentSemantic.Assign)]
		PSPDFNamedActionType NamedActionType { get; }

		[Export ("namedAction")]
		string NamedAction { get; }

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		nuint PageIndexWithCurrentPage (nuint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFJavaScriptAction
	{

		[Export ("initWithScript:")]
		IntPtr Constructor (string script);

		[Export ("script")]
		string Script { get; }

		[Export ("executeScriptAppliedToDocumentProvider:application:eventDictionary:sender:error:")]
		NSDictionary ExecuteScript (PSPDFDocumentProvider documentProvider, NSObject application, [NullAllowed] NSDictionary eventDictionary, NSObject sender, out NSError error);
	}

	[Static]
	interface PSPDFActionEvent
	{
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

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFRenditionAction
	{

		[Field ("PSPDFRenditionActionTypeTransformerName", "__Internal")]
		NSString TypeTransformerName { get; }

		[Export ("initWithActionType:annotation:")]
		IntPtr Constructor (PSPDFRenditionActionType actionType, PSPDFScreenAnnotation annotation);

		[Export ("actionType", ArgumentSemantic.Assign)]
		PSPDFRenditionActionType ActionType { get; }

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFScreenAnnotation Annotation { get; }
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFRichMediaExecuteAction
	{

		[Export ("initWithCommand:argument:annotation:")]
		IntPtr Constructor (string command, string argument, PSPDFRichMediaAnnotation annotation);

		[Export ("command")]
		string Command { get; }

		[Export ("argument")]
		string Argument { get; }

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFRichMediaAnnotation Annotation { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFAbstractFormAction {

		[Export ("fields", ArgumentSemantic.Copy)]
		NSObject [] Fields { get; set; }
	}


	[BaseType (typeof(PSPDFAbstractFormAction))]
	interface PSPDFSubmitFormAction
	{

		[Export ("initWithURL:flags:")]
		IntPtr Constructor (NSUrl URL, PSPDFSubmitFormActionFlag flags);

		[Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFSubmitFormActionFlag Flags { get; }
	}

	[BaseType (typeof(PSPDFAbstractFormAction))]
	interface PSPDFResetFormAction
	{

		[Export ("initWithFlags:")]
		IntPtr Constructor (PSPDFResetFormActionFlag flags);

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFResetFormActionFlag Flags { get; }
	}

	[BaseType (typeof(PSPDFAction))]
	interface PSPDFHideAction
	{

		[Export ("initWithAssociatedAnnotations:shouldHide:")]
		IntPtr Constructor (PSPDFAnnotation[] annotations, bool shouldHide);

		[Export ("shouldHide", ArgumentSemantic.Assign)]
		bool ShouldHide { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }
	}

	interface IPSPDFEraseOverlayDataSource
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFEraseOverlayDataSource
	{

		[Export ("zoomScaleForEraseOverlay:")]
		[Abstract]
		nfloat ZoomScaleForEraseOverlay (PSPDFEraseOverlay overlay);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFEraseOverlay
	{

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

	interface IPSPDFPageLabelViewDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPageLabelViewDelegate
	{

		[Export ("pageLabelView:didPressThumbnailGridButton:")]
		[Abstract]
		void DidPressThumbnailGridButton (PSPDFPageLabelView pageLabelView, UIButton sender);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLabelView {

		[Export ("label", ArgumentSemantic.Strong), NullAllowed]
		UILabel Label { get; }

		[Export ("labelMargin", ArgumentSemantic.Assign)]
		nfloat LabelMargin { get; set; }

		[Export ("labelStyle", ArgumentSemantic.Assign)]
		PSPDFLabelStyle LabelStyle { get; set; }
	}


	[BaseType (typeof(PSPDFLabelView))]
	interface PSPDFPageLabelView
	{

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPageLabelViewDelegate Delegate { get; set; }

		[Export ("showThumbnailGridButton", ArgumentSemantic.Assign)]
		bool ShowThumbnailGridButton { get; set; }

		[Export ("thumbnailGridButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton ThumbnailGridButton { get; set; }

		[Export ("updateLabelWithDocument:page:visiblePageNumbers:")]
		bool UpdateLabel (PSPDFDocument document, nuint page, NSObject[] visiblePageNumbers);

		// Category

		[Export ("pageLabelWithDocument:page:visiblePageNumbers:")]
		string PageLabel (PSPDFDocument document, nuint page, NSNumber[] visiblePageNumbers);

		[Export ("updateFrame")]
		void UpdateFrame ();
	}

	interface IPSPDFPasswordViewDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFPasswordViewDelegate
	{

		[Export ("passwordView:didUnlockWithPassword:")]
		[Abstract]
		void DidUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:didFailToUnlockWithPassword:")]
		void DidFailToUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:shouldUnlockWithPassword:")]
		bool ShouldUnlock (PSPDFPasswordView passwordView, string password);

		[Export ("passwordView:willUnlockWithPassword:")]
		void WillUnlock (PSPDFPasswordView passwordView, string password);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFPasswordView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFPasswordViewDelegate Delegate { get; set; }

		[Export ("shakeOnError", ArgumentSemantic.Assign)]
		bool ShakeOnError { get; set; }

		[Export ("becomeFirstResponder")][New]
		bool BecomeFirstResponder ();

		// PSPDFPasswordView (SubclassingHooks) Category

		[Export ("passwordField", ArgumentSemantic.Strong), NullAllowed]
		UITextField PasswordField { get; }

		[Export ("statusLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel StatusLabel { get; }

		[Export ("unlockButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton UnlockButton { get; }
	}

	[BaseType (typeof(UILabel))]
	interface PSPDFActivityLabel
	{

		[Export ("showActivity", ArgumentSemantic.Assign)]
		bool ShowActivity { get; set; }

		[Static, Export ("labelWithText:showActivity:")]
		PSPDFActivityLabel FromText (string text, bool showActivity);
	}

	[BaseType (typeof(PSPDFLabelView))]
	interface PSPDFDocumentLabelView
	{

	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFRelayTouchesView))]
	interface PSPDFHUDView
	{

		[Export ("initWithFrame:dataSource:")]
		IntPtr Constructor (CGRect frame, PSPDFPresentationContext dataSource);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		PSPDFPresentationContext DataSource { get; }

		[Export ("pageLabelDistance", ArgumentSemantic.Assign)]
		nfloat PageLabelDistance { get; set; }

		[Export ("documentLabelDistance", ArgumentSemantic.Assign)]
		nfloat DocumentLabelDistance { get; set; }

		[Export ("layoutSubviewsAnimated:")]
		void LayoutSubviews (bool animated);

		[Export ("reloadData")]
		void ReloadData ();

		// PSPDFHUDView (Subviews) Category

		[Export ("documentLabel", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocumentLabelView DocumentLabel { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Strong), NullAllowed]
		PSPDFPageLabelView PageLabel { get; set; }

		[Export ("scrobbleBar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFScrobbleBar ScrobbleBar { get; }

		[Export ("thumbnailBar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailBar ThumbnailBar { get; }

		// PSPDFHUDView (SubclassingHooks) Category

		[Export ("updateDocumentLabelFrameAnimated:")]
		void UpdateDocumentLabelFrame (bool animated);

		[Export ("updatePageLabelFrameAnimated:")]
		void UpdatePageLabelFrame (bool animated);

		[Export ("updateThumbnailBarFrameAnimated:")]
		void UpdateThumbnailBarFrame (bool animated);

		[Export ("updateScrobbleBarFrameAnimated:")]
		void UpdateScrobbleBarFrame (bool animated);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFRenderStatusView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("activityIndicator", ArgumentSemantic.Strong), NullAllowed]
		UIActivityIndicatorView ActivityIndicator { get; set; }

		// PSPDFRenderStatusView (SubclassingHooks) Category

		[Export ("loadActivityIndicator")]
		void LoadActivityIndicator ();
	}

	[BaseType (typeof(UIView))]
	interface PSPDFBaseView
	{

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Advice ("Needs base call if overridden")]
		[Export ("contentSizeDidChange")]
		void ContentSizeDidChange ();
	}

	interface IPSPDFSearchOperationDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSearchOperationDelegate {

		[Export ("didUpdateSearchOperation:forString:newSearchResults:forPage:")]
		[Abstract]
		void DidUpdateSearchOperation (PSPDFSearchOperation operation, string searchTerm, PSPDFSearchResult [] searchResults, nuint page);

		[Export ("willStartSearchOperation:forString:isFullSearch:")]
		void WillStartSearchOperation (PSPDFSearchOperation operation, string searchTerm, bool isFullSearch);
	}

	[BaseType (typeof (NSOperation))]
	interface PSPDFSearchOperation {

		[Export ("initWithDocument:searchTerm:")]
		IntPtr Constructor (PSPDFDocument document, string searchTerm);

		[Export ("pageRanges", ArgumentSemantic.Copy)]
		NSIndexSet PageRanges { get; set; }

		[Export ("shouldSearchAllPages", ArgumentSemantic.Assign)]
		bool ShouldSearchAllPages { get; set; }

		[Export ("maximumNumberOfSearchResults", ArgumentSemantic.Assign)]
		nuint MaximumNumberOfSearchResults { get; set; }

		[Export ("compareOptions", ArgumentSemantic.Assign)]
		NSStringCompareOptions CompareOptions { get; set; }

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSearchOperationDelegate Delegate { get; set; }

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("searchTerm")]
		string SearchTerm { get; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("pageTextFound")]
		bool PageTextFound ();
	}

	interface IPSPDFTextSearchDelegate {}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTextSearchDelegate
	{

		[Export ("willStartSearch:term:isFullSearch:")]
		void WillStartSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);

		[Export ("didUpdateSearch:term:newSearchResults:page:")]
		void DidUpdateSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult[] searchResults, nuint page);

		[Export ("didFinishSearch:term:searchResults:isFullSearch:pageTextFound:")]
		void DidFinishSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult[] searchResults, bool isFullSearch, bool pageTextFound);

		[Export ("didCancelSearch:term:isFullSearch:")]
		void DidCancelSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFTextSearch : IPSPDFSearchOperationDelegate
	{

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("searchForString:")]
		void SearchForString (string searchTerm);

		[Export ("searchForString:inRanges:rangesOnly:")]
		void SearchForString (string searchTerm, NSIndexSet ranges, bool rangesOnly);

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
	[BaseType (typeof(NSObject))]
	interface PSPDFTextParser {
		[Export ("initWithPDFPage:page:documentProvider:fontCache:hideGlyphsOutsidePageRect:PDFBox:"), Internal]
		IntPtr Constructor (IntPtr pageRef, nuint page, PSPDFDocumentProvider documentProvider, NSMutableDictionary fontCache, bool hideGlyphsOutsidePageRect, CGPDFBox PDFBox);

		[Static, Export ("initWithStream:"), Internal]
		IntPtr FromStream (IntPtr stream);

		[Export ("text")]
		string Text { get; }

		[Export ("glyphs", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Strong), NullAllowed]
		PSPDFWord [] Words { get; }

		[Export ("lines", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextLine [] Lines { get; }

		[Export ("images", ArgumentSemantic.Strong), NullAllowed]
		PSPDFImageInfo [] Images { get; }

		[Export ("textBlocks", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextBlock [] TextBlocks { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("textWithGlyphs:")]
		string TextWithGlyphs (PSPDFGlyph[] glyphs);

		// PSPDFTextParser (SubclassingHooks) Category

		[Export ("shouldParseCharacter:")]
		bool ShouldParseCharacter (ushort character);

		[Export ("markedContentStack", ArgumentSemantic.Strong), NullAllowed]
		NSObject[] MarkedContentStack { get; }

		[Export ("parsingQueue", ArgumentSemantic.Assign)]
		DispatchQueue ParsingQueue { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFGlyph {

		[Export ("initWithFrame:content:font:")]
		IntPtr Constructor (CGRect frame, string content, PSPDFFontInfo font);

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("font", ArgumentSemantic.Assign)]
		PSPDFFontInfo Font { get; set; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; }

		[Export ("isWordBreaker", ArgumentSemantic.Assign)]
		bool IsWordBreaker { get; }

		[Export ("isWordOrLineBreaker", ArgumentSemantic.Assign)]
		bool IsWordOrLineBreaker { get; }

		[Export ("indexOnPage", ArgumentSemantic.Assign)]
		nint IndexOnPage { get; }

		[Export ("compareByXPosition:")]
		NSComparisonResult CompareByXPosition (PSPDFGlyph glyph);

		[Export ("isOnSameLineAs:")]
		bool IsOnSameLineAs (PSPDFGlyph glyph);

		[Export ("isEqualToGlyph:")]
		bool IsEqualToGlyph (PSPDFGlyph otherGlyph);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFWord {

		[Export ("initWithGlyphs:")]
		IntPtr Constructor (PSPDFGlyph[] wordGlyphs);

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect wordFrame);

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; set; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; set; }

		[Export ("lineBreaker", ArgumentSemantic.Assign)]
		bool LineBreaker { get; set; }

		[Export ("stringValue")]
		string StringValue ();

		[Export ("isOnSameLineAs:")]
		bool IsOnSameLineAs (PSPDFWord word);

		[Export ("compareByLayout:")]
		NSComparisonResult CompareByLayout (PSPDFWord word);

		[Export ("isEqualToWord:")]
		bool IsEqualToWord (PSPDFWord otherWord);
	}

	[BaseType (typeof(PSPDFWord))]
	interface PSPDFTextLine
	{

		[Export ("prevLine", ArgumentSemantic.Assign)]
		PSPDFTextLine PrevLine { get; }

		[Export ("nextLine", ArgumentSemantic.Assign)]
		PSPDFTextLine NextLine { get; }

		[Export ("borderType", ArgumentSemantic.Assign)]
		PSPDFTextLineBorder BorderType { get; }

		[Export ("blockID", ArgumentSemantic.Assign)]
		nint BlockId { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFTextBlock {

		[Export ("initWithGlyphs:")]
		IntPtr Constructor (PSPDFGlyph[] glyphs);

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; }

		[Export ("glyphs", ArgumentSemantic.Copy)]
		PSPDFGlyph [] Glyphs { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("isEqualToTextBlock:")]
		bool IsEqualToTextBlock (PSPDFTextBlock otherBlock);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFImageInfo {

		[Export ("initWithImageID:pixelSize:bitsPerComponent:transform:vertices:page:documentProvider:")]
		IntPtr Constructor (string imageId, CGSize pixelSize, nuint bitsPerComponent, CGAffineTransform transform, IntPtr vertices, nuint page, PSPDFDocumentProvider documentProvider);

		[Export ("imageID")]
		string ImageId { get; }

		[Export ("pixelSize", ArgumentSemantic.Assign)]
		int PixelSize { get; }

		[Export ("bitsPerComponent", ArgumentSemantic.Assign)]
		nuint BitsPerComponent { get; }

		[Export ("transform", ArgumentSemantic.Assign)]
		int Transform { get; }

		[Export ("vertices", ArgumentSemantic.Assign)]
		int Vertices { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("displaySize", ArgumentSemantic.Assign)]
		int DisplaySize { get; }

		[Export ("horizontalResolution", ArgumentSemantic.Assign)]
		int HorizontalResolution { get; }

		[Export ("verticalResolution", ArgumentSemantic.Assign)]
		int VerticalResolution { get; }

		[Export ("hitTest:")]
		bool HitTest (CGPoint point);

		[Export ("isEqualToImageInfo:")]
		bool IsEqualToImageInfo (PSPDFImageInfo otherImageInfo);

		[Export ("boundingBox")]
		CGRect BoundingBox ();

		[Export ("imageWithError:"), Internal]
		UIImage GetImage (IntPtr error);

		[Export ("imageInRGBColorSpaceWithError:"), Internal]
		UIImage GetImageInRgbColorSpace (IntPtr error);
	}

	[BaseType (typeof(NSObject))]
	interface PSPDFFontInfo {
		[Export ("initWithFontDictionary:fontKey:"), Internal]
		IntPtr Constructor (IntPtr font, string fontKey);

		[Export ("name")]
		string Name { get; }

		[Export ("fontKey")]
		string FontKey { get; }

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFFontInfoType Type { get; }

		[Export ("ascent", ArgumentSemantic.Assign)]
		int Ascent { get; }

		[Export ("descent", ArgumentSemantic.Assign)]
		int Descent { get; }

		[Export ("encodingArray", ArgumentSemantic.Copy)]
		string [] EncodingArray { get; }

		[Export ("toUnicodeMap", ArgumentSemantic.Strong), NullAllowed]
		NSObject ToUnicodeMap { get; }

		[Export ("fontFileDescriptor", ArgumentSemantic.Strong), NullAllowed]
		NSObject FontFileDescriptor { get; }

		[Export ("fontCMap", ArgumentSemantic.Strong), NullAllowed]
		NSObject FontCMap { get; }

		[Export ("ucsCMap", ArgumentSemantic.Strong), NullAllowed]
		NSObject UcsCMap { get; }

		[Export ("isMultiByteFont", ArgumentSemantic.Assign)]
		bool IsMultiByteFont { get; }

		[Export ("widthForCharacter:")]
		nfloat WidthForCharacter (ushort character);

		[Static, Export ("glyphNames")]
		NSDictionary GlyphNames ();

		[Static, Export ("standardFontWidths")]
		NSDictionary StandardFontWidths ();

		[Export ("isEqualToFontInfo:")]
		bool IsEqualToFontInfo (PSPDFFontInfo otherFontInfo);

		[Export ("unicodeStringForCharacter:")]
		string UnicodeStringForCharacter (ushort character);
	}

	interface IPSPDFSearchViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFSearchViewControllerDelegate : PSPDFTextSearchDelegate, PSPDFOverridable
	{

		[Export ("searchViewController:didTapSearchResult:")]
		void DidTapSearchResult (PSPDFSearchViewController searchController, PSPDFSearchResult searchResult);

		[Export ("searchViewControllerDidClearAllSearchResults:")]
		void DidClearAllSearchResults (PSPDFSearchViewController searchController);

		[Export ("searchViewControllerGetVisiblePages:")]
		NSObject [] GetVisiblePages (PSPDFSearchViewController searchController);

		[Export ("searchViewController:searchRangeForScope:")]
		NSIndexSet SearchRangeForScope (PSPDFSearchViewController searchController, string scope);

		[Export ("searchViewControllerTextSearchObject:")]
		PSPDFTextSearch TextSearchObject (PSPDFSearchViewController searchController);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFBaseTableViewController))]
	interface PSPDFSearchViewController
	{

		[Export ("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] IPSPDFSearchViewControllerDelegate aDelegate);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("searchText")]
		string SearchText { get; set; }

		[Export ("showsCancelButton", ArgumentSemantic.Assign)]
		bool ShowsCancelButton { get; set; }

		[Export ("searchBar", ArgumentSemantic.Strong), NullAllowed]
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

		[Export ("searchableAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SearchableAnnotationTypes { get; set; }

		[Export ("pinSearchBarToHeader", ArgumentSemantic.Assign)]
		bool PinSearchBarToHeader { get; set; }

		[Export ("textSearch", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSearch TextSearch { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSearchViewControllerDelegate Delegate { get; set; }

		[Export ("restartSearch")]
		void RestartSearch ();

		// PSPDFSearchViewController (SubclassingHooks) Category

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("filterContentForSearchText:scope:")]
		void FilterContentForSearchText (string searchText, string scope);

		[Export ("setSearchStatus:updateTable:")]
		void SetSearchStatus (PSPDFSearchStatus searchStatus, bool updateTable);

		[Export ("searchResultForIndexPath:")]
		PSPDFSearchResult SearchResultForIndexPath (NSIndexPath indexPath);

		[Export ("createSearchBar")]
		UISearchBar CreateSearchBar ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFModel))]
	interface PSPDFSearchResult
	{

		[Export ("initWithDocumentUID:page:range:previewText:rangeInPreviewText:selection:annotation:")]
		IntPtr Constructor (string documentUid, nuint page, NSRange range, string previewText, NSRange rangeInPreviewText, PSPDFTextBlock selection, PSPDFAnnotation annotation);

		[Export ("initWithDocument:page:range:previewText:rangeInPreviewText:selection:annotation:")]
		IntPtr Constructor (PSPDFDocument document, nuint page, NSRange range, string previewText, NSRange rangeInPreviewText, PSPDFTextBlock selection, PSPDFAnnotation annotation);

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

		[Export ("selection", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextBlock Selection { get; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFAnnotation Annotation { get; }

		[Export ("isEqualToSearchResult:")]
		bool IsEqualToSearchResult (PSPDFSearchResult otherSearchResult);
	}

	[BaseType (typeof(PSPDFTableViewCell))]
	interface PSPDFSearchResultCell : IPSPDFCacheDelegate
	{

		[Export ("configureWithSearchResult:")]
		void Configure (PSPDFSearchResult searchResult);

		[Export ("configureWithDocument:page:text:detailText:")]
		void Configure (PSPDFDocument document, nuint page, string text, NSAttributedString detailText);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("useOutlineForPageNames", ArgumentSemantic.Assign)]
		bool UseOutlineForPageNames { get; set; }

		[Static, Export ("heightForSearchResult:numberOfPreviewLines:")]
		nfloat HeightForSearchResult (PSPDFSearchResult searchResult, nuint numberOfPreviewLines);

		// PSPDFSearchResult (SubclassingHooks) Category

		[Export ("pagePreviewImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage PagePreviewImage { get; set; }

		[Export ("placeholderImage")]
		UIImage PlaceholderImage ();

		[Static, Export ("textLabelFont")]
		UIFont TextLabelFont ();

		[Static, Export ("detailLabelFont")]
		UIFont DetailLabelFont ();
	}

	[BaseType (typeof(PSPDFSpinnerCell))]
	interface PSPDFSearchStatusCell
	{

		[Static, Export ("cellHeight")]
		nfloat CellHeight { get; }

		[Export ("updateCellWithSearchStatus:results:page:pageCount:")]
		void UpdateCell (PSPDFSearchStatus searchStatus, nuint results, nuint page, nuint pageCount);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(UIView))]
	interface PSPDFSearchHighlightView : IPSPDFAnnotationViewProtocol
	{

		[Export ("initWithSearchResult:")]
		IntPtr Constructor (PSPDFSearchResult searchResult);

		[Export ("popupAnimation")]
		void PopupAnimation ();

		[Export ("removeFromSuperviewAnimated")]
		void RemoveFromSuperviewAnimated ();

		[Export ("searchResult", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSearchResult SearchResult { get; set; }

		[Export ("selectionBackgroundColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionBackgroundColor { get; set; }

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nuint CornerRadius { get; set; }
	}

	interface IPSPDFInlineSearchManagerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFInlineSearchManagerDelegate : PSPDFTextSearchDelegate, PSPDFOverridable
	{

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
	[BaseType (typeof(NSObject))]
	interface PSPDFInlineSearchManager
	{
		[Export ("initWithPresentationContext:delegate:")]
		IntPtr Constructor ([NullAllowed] PSPDFPresentationContext presentationContext, [NullAllowed] IPSPDFInlineSearchManagerDelegate aDelegate);

		[Export ("presentationContext", ArgumentSemantic.Weak)]
		PSPDFPresentationContext PresentationContext { get; }

		[Export ("textSearch", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTextSearch TextSearch { get; }

		[Export ("searchText")]
		string SearchText { get; }

		[Export ("searchResults", ArgumentSemantic.Copy)]
		PSPDFSearchResult [] SearchResults { get; }

		[Export ("searchStatus", ArgumentSemantic.Assign)]
		PSPDFSearchStatus SearchStatus { get; }

		[Export ("delegate", ArgumentSemantic.Weak)]
		[NullAllowed]
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

		[Export ("presentInlineSearchWithSearchText:animated:")]
		void PresentInlineSearch (string text, bool animated);

		[Export ("hideInlineSearchAnimated:")]
		bool HideInlineSearch (bool animated);

		[Export ("hideKeyboard")]
		void HideKeyboard ();

		[Export ("isSearchVisible")]
		bool IsSearchVisible ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFLibrary
	{

		[Notification]
		[Field ("PSPDFLibraryWillStartIndexingDocumentNotification", "__Internal")]
		NSString WillStartIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidFinishIndexingDocumentNotification", "__Internal")]
		NSString DidFinishIndexingDocumentNotification { get; }

		[Notification]
		[Field ("PSPDFLibraryDidFailIndexingDocumentNotification", "__Internal")]
		NSString DidFailIndexingDocumentNotification { get; }

		[Static, Export ("defaultLibrary")]
		PSPDFLibrary DefaultLibrary { get; }

		[Static, Export ("libraryWithPath:")]
		PSPDFLibrary FromPath (string path);

		[Export ("path")]
		string Path { get; }

		[Export ("tokenizer")]
		string Tokenizer { get; set; }

		[Export ("saveGlyphPositions", ArgumentSemantic.Assign)]
		bool SaveGlyphPositions { get; set; }

		[Export ("saveReversedPageText", ArgumentSemantic.Assign)]
		bool SaveReversedPageText { get; set; }

		[Field ("PSPDFLibraryMaximumSearchResultsTotalKey", "__Internal")]
		NSString MaximumSearchResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumSearchResultsPerDocumentKey", "__Internal")]
		NSString MaximumSearchResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryMatchExactWordsOnlyKey", "__Internal")]
		NSString MatchExactWordsOnlyKey { get; }

		[Field ("PSPDFLibraryPreviewRangeKey", "__Internal")]
		NSString PreviewRangeKey { get; }

		[Export ("documentUIDsMatchingString:options:completionHandler:")]
		void DocumentUidsMatchingString (string searchString, [NullAllowed] NSDictionary options, [NullAllowed] Action<string, NSDictionary> completionHandler);

		[Export ("documentUIDsMatchingString:options:completionHandler:previewTextHandler:")]
		void DocumentUidsMatchingString (string searchString, [NullAllowed] NSDictionary options, [NullAllowed] Action<string, NSDictionary> completionHandler, [NullAllowed] Action<NSString, NSDictionary> previewTextHandler);

		[Export ("indexStatusForUID:withProgress:")]
		PSPDFLibraryIndexStatus IndexStatus (string uid, nfloat outProgress);

		[Export ("isIndexing")]
		bool IsIndexing ();

		[Export ("queuedUIDs")]
		NSOrderedSet QueuedUids ();

		[Export ("enqueueDocuments:")]
		void EnqueueDocuments (PSPDFDocument[] documents);

		[Export ("removeIndexForUID:")]
		void RemoveIndex (string uid);

		[Export ("clearAllIndexes")]
		void ClearAllIndexes ();

		[Export ("cancelAllPreviewTextOperations")]
		void CancelAllPreviewTextOperations ();
	}

	interface IPSPDFDocumentPickerControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFDocumentPickerControllerDelegate : PSPDFOverridable
	{

		[Export ("documentPickerController:didSelectDocument:page:searchString:")]
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
	[BaseType (typeof(PSPDFStatefulTableViewController))]
	interface PSPDFDocumentPickerController
	{

		[Static, Export ("documentsFromDirectory:includeSubdirectories:")]
		PSPDFDocument [] DocumentsFromDirectory (string directoryName, bool includeSubdirectories);

		[Export ("initWithDirectory:includeSubdirectories:library:delegate:")]
		IntPtr Constructor (string directory, bool includeSubdirectories, PSPDFLibrary library, [NullAllowed] IPSPDFDocumentPickerControllerDelegate aDelegate);

		[Export ("initWithDocuments:library:delegate:")]
		IntPtr Constructor (PSPDFDocument[] documents, PSPDFLibrary library, [NullAllowed] IPSPDFDocumentPickerControllerDelegate aDelegate);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentPickerControllerDelegate Delegate { get; set; }

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; }

		[Export ("directory")]
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

	[BaseType (typeof(UITableViewCell))]
	interface PSPDFDocumentPickerCell
	{

		[Export ("rotatedPageRect", ArgumentSemantic.Assign)]
		CGRect RotatedPageRect { get; set; }

		[Export ("pagePreviewImage", ArgumentSemantic.Strong), NullAllowed]
		UIImage PagePreviewImage { get; set; }

		[Export ("setPagePreviewImage:animated:")]
		void SetPagePreviewImage (UIImage pagePreviewImage, bool animated);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }
	}

	[BaseType (typeof(PSPDFSpinnerCell))]
	interface PSPDFDocumentPickerIndexStatusCell
	{

	}

	[BaseType (typeof (UISegmentedControl))]
	interface PSPDFSegmentedControl {

		[Export ("hitTestEdgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets HitTestEdgeInsets { get; set; }
	}


	[BaseType (typeof(PSPDFSegmentedControl))]
	interface PSPDFThumbnailFilterSegmentedControl
	{

	}

	interface IPSPDFThumbnailViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFThumbnailViewControllerDelegate
	{

		[Export ("thumbnailViewController:didSelectPage:inDocument:")]
		void DidSelectPage (PSPDFThumbnailViewController thumbnailViewController, nuint page, PSPDFDocument document);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(UICollectionViewController))]
	interface PSPDFThumbnailViewController
	{

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("collectionView", ArgumentSemantic.Strong), NullAllowed][New]
		UICollectionView CollectionView { get; set; }

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		PSPDFPresentationContext DataSource { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFThumbnailViewControllerDelegate Delegate { get; set; }

		[Export ("cellForPage:document:")]
		UICollectionViewCell CellForPage (nuint page, PSPDFDocument document);

		[Export ("scrollToPage:document:animated:")]
		void ScrollToPage (nuint page, PSPDFDocument document, bool animated);

		[Export ("stopScrolling")]
		void StopScrolling ();

		[Export ("updateFilterAndVisibleCellsAnimated:")]
		void UpdateFilterAndVisibleCells (bool animated);

		[Export ("setActiveFilter:animated:")]
		void SetActiveFilter (PSPDFThumbnailViewFilter activeFilter, bool animated);

		[Export ("fixedItemSizeEnabled", ArgumentSemantic.Assign)]
		bool FixedItemSizeEnabled { get; set; }

		[Export ("stickyHeaderEnabled", ArgumentSemantic.Assign)]
		bool StickyHeaderEnabled { get; set; }

		[Export ("filterOptions", ArgumentSemantic.Copy), NullAllowed]
		NSOrderedSet FilterOptions { get; set; }

		[Export ("activeFilter", ArgumentSemantic.Assign)]
		PSPDFThumbnailViewFilter ActiveFilter { get; set; }

		[Export ("thumbnailSize", ArgumentSemantic.Assign)]
		CGSize ThumbnailSize { get; set; }

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		UIEdgeInsets ThumbnailMargin { get; set; }

		[Export ("thumbnailRowMargin", ArgumentSemantic.Assign)]
		nfloat ThumbnailRowMargin { get; set; }

		[Export ("thumbnailCellClass", ArgumentSemantic.Strong), NullAllowed]
		Class ThumbnailCellClass { get; set; }

		// PSPDFThumbnailViewController (SubclassingHooks) Category

		[Export ("configureCell:forIndexPath:")]
		void ConfigureCell (PSPDFThumbnailGridViewCell cell, NSIndexPath indexPath);

		[Export ("layout", ArgumentSemantic.Strong), NullAllowed][New]
		UICollectionViewLayout Layout { get; set; }

		[Export ("filterSegment", ArgumentSemantic.Strong), NullAllowed]
		PSPDFThumbnailFilterSegmentedControl FilterSegment { get; }

		[Export ("updateFilterSegment")]
		void UpdateFilterSegment ();

		[Export ("filteredPagesForType:")]
		NSObject [] FilteredPages (PSPDFThumbnailViewFilter filter);

		[Export ("updateEmptyView")]
		void UpdateEmptyView ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFThumbnailViewController))]
	interface PSPDFMultiDocumentThumbnailViewController
	{

		[Export ("initWithDocuments:")]
		IntPtr Constructor (PSPDFDocument[] documents);

		[Export ("documents", ArgumentSemantic.Copy)]
		PSPDFDocument [] Documents { get; set; }

		[Export ("firstPageOnly", ArgumentSemantic.Assign)]
		bool FirstPageOnly { get; set; }
	}

	[BaseType (typeof (UILabel))]
	interface PSPDFRoundedLabel {

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nfloat CornerRadius { get; set; }

		[Export ("rectColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor RectColor { get; set; }
	}


	[BaseType (typeof(UICollectionViewCell))]
	interface PSPDFThumbnailGridViewCell : IPSPDFCacheDelegate
	{

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; set; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("edgeInsets", ArgumentSemantic.Assign)]
		UIEdgeInsets EdgeInsets { get; set; }

		[Export ("shadowEnabled", ArgumentSemantic.Assign)]
		bool ShadowEnabled { [Bind ("isShadowEnabled")] get; set; }

		[Export ("pageLabelEnabled", ArgumentSemantic.Assign)]
		bool PageLabelEnabled { [Bind ("isPageLabelEnabled")] get; set; }

		[Export ("selectedBackgroundView", ArgumentSemantic.Strong), NullAllowed][New]
		UIView SelectedBackgroundView { get; set; }

		[Export ("bookmarkImageColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BookmarkImageColor { get; set; }

		[Export ("updateCell")]
		void UpdateCell ();

		// PSPDFThumbnailGridViewCell (SubclassingHooks) Category

		[Export ("imageView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView ImageView { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRoundedLabel PageLabel { get; set; }

		[Export ("bookmarkImageView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView BookmarkImageView { get; }

		[Export ("pathShadowForView:")]
		CGPath PathShadow (UIView imgView);

		[Export ("setImage:animated:")]
		void SetImage (UIImage image, bool animated);

		[Export ("setImageSize:")]
		void SetImageSize (CGSize imageSize);

		[Export ("updateImageSize")]
		void UpdateImageSize ();

		[Export ("updatePageLabel")]
		void UpdatePageLabel ();

		[Export ("updateBookmarkImage")]
		void UpdateBookmarkImage ();
	}

	interface IPSPDFScrobbleBarDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFScrobbleBarDelegate
	{

		[Export ("scrobbleBar:didSelectPage:")]
		[Abstract]
		void DidSelectPage (PSPDFScrobbleBar scrobbleBar, nuint page);
	}

	[BaseType (typeof(UIView))]
	interface PSPDFScrobbleBar : IPSPDFCacheDelegate
	{

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFScrobbleBarDelegate Delegate { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		PSPDFPresentationContext DataSource { get; set; }

		[Export ("updateToolbarAnimated:")]
		void UpdateToolbar (bool animated);

		[Export ("updateToolbarForced")]
		void UpdateToolbarForced ();

		[Export ("updatePageMarker")]
		void UpdatePageMarker ();

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

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

		// PSPDFScrobbleBar (SubclassingHooks) Category

		[Export ("isSmallToolbar")]
		bool IsSmallToolbar ();

		[Export ("scrobbleBarHeight")]
		nfloat ScrobbleBarHeight ();

		[Export ("scrobbleBarThumbSize")]
		CGSize ScrobbleBarThumbSize ();

		[Export ("emptyThumbnailImageView")]
		UIImageView EmptyThumbnailImageView ();

		[Export ("processTouch:")]
		bool ProcessTouch (UITouch touch);

		[Export ("thumbnailMargin", ArgumentSemantic.Assign)]
		nfloat ThumbnailMargin { get; set; }

		[Export ("pageMarkerSizeMultiplier", ArgumentSemantic.Assign)]
		nfloat PageMarkerSizeMultiplier { get; set; }
	}

	interface IPSPDFThumbnailBarDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFThumbnailBarDelegate
	{

		[Export ("thumbnailBar:didSelectPage:")]
		void DidSelectPage (PSPDFThumbnailBar thumbnailBar, nuint page);
	}

	[BaseType (typeof(UICollectionView))]
	interface PSPDFThumbnailBar
	{

		[Export ("thumbnailBarDelegate", ArgumentSemantic.Weak)]
		[NullAllowed]
		IPSPDFThumbnailBarDelegate ThumbnailBarDelegate { get; set; }

		[Export ("thumbnailBarDataSource", ArgumentSemantic.Weak)]
		PSPDFPresentationContext ThumbnailBarDataSource { get; set; }

		[Export ("scrollToPage:animated:")]
		void ScrollToPage (nuint page, bool animated);

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
	[BaseType (typeof(NSObject))]
	interface PSPDFOutlineParser
	{

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("outlineElementForPage:exactPageOnly:")]
		PSPDFOutlineElement OutlineElement (nuint page, bool exactPageOnly);

		[Export ("outline", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineElement Outline { get; set; }

		[Export ("outlineParsed", ArgumentSemantic.Assign)]
		bool OutlineParsed { [Bind ("isOutlineParsed")] get; }

		[Export ("outlineAvailable", ArgumentSemantic.Assign)]
		bool OutlineAvailable { [Bind ("isOutlineAvailable")] get; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("firstVisibleElement", ArgumentSemantic.Assign)]
		nuint FirstVisibleElement { get; set; }

		[Export ("namedDestinationResolveThreshold", ArgumentSemantic.Assign)]
		nuint NamedDestinationResolveThreshold { get; set; }
	}

	[BaseType (typeof(PSPDFBookmark))]
	interface PSPDFOutlineElement
	{

		[Export ("initWithTitle:color:fontTraits:action:children:level:")]
		IntPtr Constructor (string title, UIColor color, UIFontDescriptorSymbolicTraits fontTraits, PSPDFAction action, PSPDFOutlineElement[] children, nuint level);

		[Export ("flattenedChildren")]
		PSPDFOutlineElement [] FlattenedChildren { get; }

		[Export ("allFlattenedChildren")]
		PSPDFOutlineElement [] AllFlattenedChildren { get; }

		[Export ("title")]
		string Title { get; }

		[Export ("color", ArgumentSemantic.Strong), NullAllowed]
		UIColor Color { get; }

		[Export ("fontTraits", ArgumentSemantic.Assign)]
		UIFontDescriptorSymbolicTraits FontTraits { get; }

		[Export ("children", ArgumentSemantic.Copy)]
		PSPDFOutlineElement [] Children { get; }

		[Export ("level", ArgumentSemantic.Assign)]
		nuint Level { get; }

		[Export ("expanded", ArgumentSemantic.Assign)]
		bool Expanded { [Bind ("isExpanded")] get; set; }
	}

	interface IPSPDFOutlineViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFOutlineViewControllerDelegate : PSPDFOverridable
	{

		[Export ("outlineController:didTapAtElement:")]
		[Abstract]
		bool DidTapAtElement (PSPDFOutlineViewController outlineController, PSPDFOutlineElement outlineElement);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFStatefulTableViewController))]
	interface PSPDFOutlineViewController : IPSPDFStyleable
	{

		[Export ("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, IPSPDFOutlineViewControllerDelegate aDelegate);

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

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOutlineViewControllerDelegate Delegate { get; set; }

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		// PSPDFOutlineViewController (SubclassingHooks) Category

		[Export ("searchController", ArgumentSemantic.Strong), NullAllowed]
		UISearchController SearchController { get; }

		[Export ("searchBar", ArgumentSemantic.Strong), NullAllowed]
		UISearchBar SearchBar { get; }

		[Export ("outlineCellDidTapDisclosureButton:")]
		void OutlineCellDidTapDisclosureButton (PSPDFOutlineCell cell);
	}

	interface IPSPDFOutlineCellDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFOutlineCellDelegate
	{

		[Export ("outlineCellDidTapDisclosureButton:")]
		[Abstract]
		void DidTapDisclosureButton (PSPDFOutlineCell outlineCell);
	}

	[BaseType (typeof(PSPDFTableViewCell))]
	interface PSPDFOutlineCell
	{

		[Static, Export ("heightForCellWithOutlineElement:constrainedToSize:outlineIntentLeftOffset:outlineIntentMultiplier:showPageLabel:")]
		nfloat HeightForCell (PSPDFOutlineElement outlineElement, CGSize constraintSize, nfloat leftOffset, nfloat multiplier, bool showPageLabel);

		[Export ("configureWithOutlineElement:documentProvider:")]
		void Configure (PSPDFOutlineElement outlineElement, PSPDFDocumentProvider documentProvider);

		[Export ("outlineElement", ArgumentSemantic.Strong), NullAllowed]
		PSPDFOutlineElement OutlineElement { get; }

		[Export ("pageLabelString")]
		string PageLabelString { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOutlineCellDelegate Delegate { get; set; }

		[Export ("showExpandCollapseButton", ArgumentSemantic.Assign)]
		bool ShowExpandCollapseButton { get; set; }

		[Export ("showPageLabel", ArgumentSemantic.Assign)]
		bool ShowPageLabel { get; set; }

		// PSPDFOutlineCell (SubclassingHooks) Category

		[Export ("disclosureButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton DisclosureButton { get; set; }

		[Export ("pageLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel PageLabel { get; set; }

		[Static, Export ("fontForOutlineElement:")]
		UIFont FontForOutlineElement (PSPDFOutlineElement outlineElement);

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
	[BaseType (typeof(NSObject))]
	interface PSPDFLabelParser
	{

		[Export ("initWithDocumentProvider:labels:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, [NullAllowed] NSDictionary labels);

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageLabelForPage:")]
		string GetPageLabel (nuint page);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		[Export ("labels", ArgumentSemantic.Copy)]
		NSDictionary Labels { get; }
	}

	interface IPSPDFTabbedViewControllerDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTabbedViewControllerDelegate : PSPDFMultiDocumentViewControllerDelegate
	{

		[Export ("tabbedPDFController:shouldChangeDocuments:")]
		bool TabbedPdfShouldChangeDocuments (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument[] newDocuments);

		[Export ("tabbedPDFController:didChangeDocuments:")]
		void TabbedPdfDidChangeDocuments (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument[] oldDocuments);

		[Export ("tabbedPDFController:shouldChangeVisibleDocument:")]
		bool TabbedPdfShouldChangeVisibleDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument newDocument);

		[Export ("tabbedPDFController:didChangeVisibleDocument:")]
		void TabbedPdfDidChangeVisibleDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument oldDocument);

		[Export ("tabbedPDFController:shouldCloseDocument:")]
		bool TabbedPdfShouldCloseDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument closedDocument);

		[Export ("tabbedPDFController:didCloseDocument:")]
		void TabbedPdfDidCloseDocument (PSPDFTabbedViewController tabbedPDFController, PSPDFDocument closedDocument);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(PSPDFMultiDocumentViewController))]
	interface PSPDFTabbedViewController
	{

		[Export ("initWithPDFViewController:")]
		IntPtr Constructor (PSPDFViewController pdfController);

		[Export ("addDocuments:atIndex:animated:")]
		void AddDocuments (PSPDFDocument[] documents, nuint index, bool animated);

		[Export ("removeDocuments:animated:")]
		void RemoveDocuments (PSPDFDocument[] documents, bool animated);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed][New]
		IPSPDFTabbedViewControllerDelegate Delegate { get; set; }

		[Export ("statePersistenceKey")][New]
		string StatePersistenceKey { get; set; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		nfloat MinTabWidth { get; set; }

		[Export ("openDocumentActionInNewTab", ArgumentSemantic.Assign)]
		bool OpenDocumentActionInNewTab { get; set; }

		[Export ("tabBar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTabBarView TabBar { get; }
	}

	[BaseType (typeof(UIView))]
	interface PSPDFTabBarView
	{

		[Export ("selectedTabIndex")]
		nuint SelectedTabIndex { get; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		nfloat MinTabWidth { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFTabBarViewDelegate Delegate { get; set; }

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFTabBarViewDataSource DataSource { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("selectTabAtIndex:animated:")]
		void SelectTab (nuint index, bool animated);

		[Export ("scrollToTabAtIndex:animated:")]
		void ScrollToTab (nuint index, bool animated);

		// PSPDFTabBarView (SubclassingHooks) Category

		[Export ("scrollView", ArgumentSemantic.Strong), NullAllowed]
		UIScrollView ScrollView { get; }

		[Export ("selectTabAtIndex:animated:callDelegate:")]
		void SelectTab (nuint index, bool animated, bool callDelegate);
	}

	interface IPSPDFTabBarViewDelegate
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTabBarViewDelegate
	{

		[Export ("tabBarView:didSelectTabAtIndex:"), Abstract]
		void DidSelectTabAtIndex (PSPDFTabBarView tabBarView, nuint index);

		[Export ("tabBarView:didSelectCloseButtonOfTabAtIndex:"), Abstract]
		void DidSelectCloseButtonOfTabAtIndex (PSPDFTabBarView tabBarView, nuint index);
	}

	interface IPSPDFTabBarViewDataSource
	{

	}

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFTabBarViewDataSource
	{

		[Export ("numberOfTabsInTabBarView:")]
		[Abstract]
		nint NumberOfTabsInTabBarView (PSPDFTabBarView tabBarView);

		[Export ("tabBarView:titleForTabAtIndex:")]
		[Abstract]
		string TitleForTabAtIndex (PSPDFTabBarView tabBarView, nuint index);
	}

	[BaseType (typeof(UIButton))]
	interface PSPDFTabBarCloseButton
	{

	}

	[BaseType (typeof(UIButton))]
	interface PSPDFTabBarButton
	{

		[Export ("selected", ArgumentSemantic.Assign)][New]
		bool Selected { [Bind ("isSelected")] get; set; }

		[Export ("closeButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFTabBarCloseButton CloseButton { get; set; }

		[Export ("showCloseButton", ArgumentSemantic.Assign)]
		bool ShowCloseButton { get; set; }

		[Export ("minTabWidth", ArgumentSemantic.Assign)]
		nfloat MinTabWidth { get; set; }

		[Export ("maxTabWidth", ArgumentSemantic.Assign)]
		nfloat MaxTabWidth { get; set; }

		[Export ("tabColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor TabColor { get; set; }

		[Export ("selectedTabColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectedTabColor { get; set; }

		[Export ("highlightedTabColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor HighlightedTabColor { get; set; }

		[Export ("setSelected:animated:")]
		void SetSelected (bool selected, bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof(NSObject))]
	interface PSPDFAnnotationManager : IPSPDFAnnotationProviderChangeNotifier
	{

		[Notification]
		[Field ("PSPDFAnnotationsAddedNotification", "__Internal")]
		NSString AnnotationsAddedNotification { get; }

		[Notification]
		[Field ("PSPDFAnnotationsRemovedNotification", "__Internal")]
		NSString AnnotationsRemovedNotification { get; }

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("annotationProviders", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] AnnotationProviders { get; set; }

		[Export ("fileAnnotationProvider", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFileAnnotationProvider FileAnnotationProvider { get; }

		[Export ("allAnnotationsOfType:")]
		NSDictionary GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("hasLoadedAnnotationsForPage:")]
		bool HasLoadedAnnotations (nuint page);

		[Export ("annotationViewClassForAnnotation:")]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary options);

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, NSObject[] keyPaths, [NullAllowed] NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotations (PSPDFAnnotation options, IntPtr error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations { get; }

		[Export ("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation[] annotations, bool animated);

		[Export ("annotationsIncludingGroupsFromAnnotations:")]
		PSPDFAnnotation [] AnnotationsIncludingGroups (PSPDFAnnotation[] annotations);

		[Export ("annotationsForPage:type:")]
		PSPDFAnnotation [] AnnotationsForPage (nuint page, PSPDFAnnotationType type);

		[Export ("protocolStrings", ArgumentSemantic.Copy)]
		string [] ProtocolStrings { get; set; }

		[Static, Export ("fileTypeTranslationTable")]
		NSDictionary FileTypeTranslationTable ();

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFAnnotationManager (SubclassingHooks) Category

		[Export ("annotationsForPage:type:pageRef:"), Internal]
		PSPDFAnnotation [] AnnotationsForPage (nuint page, PSPDFAnnotationType type, IntPtr pageRef);

		[Export ("dirtyAnnotations")]
		NSDictionary DirtyAnnotations ();

		[Static, Export ("mediaFileTypes")]
		NSObject [] MediaFileTypes ();

		[Export ("defaultAnnotationViewClassForAnnotation:")]
		Class DefaultAnnotationViewClass (PSPDFAnnotation annotation);
	}

	[Static]
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

		[Field ("PSPDFAnnotationStringEraser", "__Internal")]
		NSString Eraser { get; }

		[Field ("PSPDFAnnotationStringSelectionTool", "__Internal")]
		NSString SelectionTool { get; }

		[Field ("PSPDFAnnotationStringSavedAnnotations", "__Internal")]
		NSString SavedAnnotations { get; }

		// PSPDFFlexibleAnnotationToolbar Strings

		[Field ("PSPDFAnnotationStringInkVariantPen", "__Internal")]
		NSString InkVariantPen { get; }

		[Field ("PSPDFAnnotationStringInkVariantHighlighter", "__Internal")]
		NSString InkVariantHighlighter { get; }

		[Field ("PSPDFAnnotationStringFreeTextVariantCallout", "__Internal")]
		NSString FreeTextVariantCallout { get; }
	}

	interface IPSPDFUndoProtocol { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFUndoProtocol {

		[Export ("keysForValuesToObserveForUndo")]
		[Abstract]
		NSSet KeysForValuesToObserveForUndo ();

		[Static, Export ("localizedUndoActionNameForKey:")]
		string LocalizedUndoActionNameForKey (string key);

		[Static, Export ("undoCoalescingForKey:")]
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
		
	[BaseType (typeof(PSPDFModel))]
	interface PSPDFAnnotation : IPSPDFUndoProtocol, IPSPDFJSONSerializing
	{
		[Static, Export ("annotationFromJSONDictionary:document:error:")]
		PSPDFAnnotation FromJsonDictionary (NSDictionary jsonDictionary, [NullAllowed] PSPDFDocument document, out NSError error);

		[Export ("initWithType:")]
		IntPtr Constructor (PSPDFAnnotationType annotationType);

		[Static, Export ("isWriteable")]
		bool IsWriteable { get; }

		[Static, Export ("isDeletable")]
		bool IsDeletable { get; }

		[Static, Export ("isFixedSize")]
		bool IsFixedSize { get; }

		[Export ("wantsSelectionBorder")]
		bool WantsSelectionBorder { get; }

		[Export ("requriesPopupAnnotation")]
		bool RequriesPopupAnnotation { get; }

		[Export ("isMovable")]
		bool IsMovable { get; }

		[Export ("isResizable")]
		bool IsResizable { get; }

		[Export ("shouldMaintainAspectRatio")]
		bool ShouldMaintainAspectRatio { get; }

		[Export ("minimumSize")]
		CGSize MinimumSize ();

		[Export ("hitTest:minDiameter:")]
		bool HitTest (CGPoint point, nfloat minDiameter);

		[Export ("boundingBoxForPageRect:")]
		CGRect BoundingBoxForPageRect (CGRect pageRect);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFAnnotationType Type { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; set; }

		[Export ("absolutePage", ArgumentSemantic.Assign)]
		nuint AbsolutePage { get; set; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("document", ArgumentSemantic.Assign)]
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

		[Export ("contents")]
		string Contents { get; set; }

		[Export ("subject")]
		string Subject { get; set; }

		[Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary AdditionalActions { get; set; }

		[Export ("value", ArgumentSemantic.Copy)]
		NSObject Value { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFAnnotationFlags Flags { get; set; }

		[Export ("hidden", ArgumentSemantic.Assign)]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[Export ("name")]
		string Name { get; set; }

		[Export ("user")]
		string User { get; set; }

		[Export ("group")]
		string Group { get; set; }

		[Export ("creationDate", ArgumentSemantic.Strong), NullAllowed]
		NSDate CreationDate { get; set; }

		[Export ("lastModified", ArgumentSemantic.Strong), NullAllowed]
		NSDate LastModified { get; set; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		nfloat LineWidth { get; set; }

		[Export ("borderStyle", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderStyle BorderStyle { get; set; }

		[Export ("dashArray", ArgumentSemantic.Copy)]
		NSValue [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity", ArgumentSemantic.Assign)]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("rotation", ArgumentSemantic.Assign)]
		nuint Rotation { get; set; }

		[Export ("rects", ArgumentSemantic.Copy)]
		NSValue [] Rects { get; set; }

		[Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; set; }

		[Export ("indexOnPage")]
		nint IndexOnPage { get; }

		[Export ("userInfo", ArgumentSemantic.Copy), NullAllowed]
		NSDictionary UserInfo { get; set; }

		[Export ("hasAppearanceStream", ArgumentSemantic.Assign)]
		bool HasAppearanceStream { get; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[Export ("annotationIcon")]
		UIImage AnnotationIcon { get; }

		[Export ("isEqualToAnnotation:")]
		bool IsEqualToAnnotation (PSPDFAnnotation otherAnnotation);

		// PSPDFAnnotation (Drawing) Category

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary options);

		[Export ("imageWithSize:withOptions:")]
		UIImage ImageWithSize (CGSize size, [NullAllowed] NSDictionary options);

		[Export ("noteIconPoint")]
		CGPoint NoteIconPoint ();

		// PSPDFAnnotation (Advanced) Category

		[Export ("shouldUpdatePropertiesOnBoundsChange")]
		bool ShouldUpdatePropertiesOnBoundsChange ();

		[Export ("shouldUpdateOptionalPropertiesOnBoundsChange")]
		bool ShouldUpdateOptionalPropertiesOnBoundsChange ();

		[Export ("updatePropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateProperties (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("updateOptionalPropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateOptionalPropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("setBoundingBox:transform:includeOptional:")]
		void SetBoundingBox (CGRect boundingBox, bool transform, bool optionalProperties);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Export ("shouldDeleteAnnotation")]
		bool ShouldDeleteAnnotation ();

		// PSPDFAnnotation (Fonts) Category

		[Export ("fontAttributes", ArgumentSemantic.Copy)]
		NSDictionary FontAttributes { get; set; }

		[Export ("fontName")]
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
	}

	[BaseType (typeof(PSPDFModel))]
	interface PSPDFAnnotationSet
	{

		[Static, Export ("unarchiveFromClipboard")]
		PSPDFAnnotationSet FromClipboard ();

		[Export ("initWithAnnotations:")]
		IntPtr Constructor (PSPDFAnnotation[] annotations);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, NSDictionary options);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();
	}

	interface IPSPDFAnnotationProvider { }

	[Protocol, Model]
	[BaseType (typeof(NSObject))]
	interface PSPDFAnnotationProvider
	{

		[Export ("annotationsForPage:")]
		[Abstract]
		PSPDFAnnotation [] AnnotationsForPage (nuint page);

		[Export ("hasLoadedAnnotationsForPage:")]
		bool HasLoadedAnnotations (nuint page);

		[Export ("annotationViewClassForAnnotation:")]
		Class AnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("removeAnnotations:options:")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations (NSDictionary options, out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations ();

		[Export ("dirtyAnnotations")]
		NSDictionary DirtyAnnotations ();

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, NSObject[] keyPaths, NSDictionary options);

		[Export ("providerDelegate")]
		IPSPDFAnnotationProviderChangeNotifier GetProviderDelegate ();

		[Export ("setProviderDelegate:")]
		void SetProviderDelegate (IPSPDFAnnotationProviderChangeNotifier provider);
	}

	interface IPSPDFAnnotationProviderChangeNotifier { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationProviderChangeNotifier {

		[Export ("updateAnnotations:animated:")]
		[Abstract]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Export ("parentDocumentProvider")]
		[Abstract]
		PSPDFDocumentProvider ParentDocumentProvider ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFContainerAnnotationProvider : IPSPDFAnnotationProvider, IPSPDFUndoProtocol {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFContainerAnnotationProvider (SubclassingHooks) Category

		[Export ("performBlockForReading:")]
		void PerformAnctionForReading (Action action);

		[Export ("performBlockForWriting:")]
		void PerformActionForWriting (Action action);

		[Export ("performBlockForWritingAndWait:")]
		void PerformActionForWritingAndWait (Action action);

		[Export ("setAnnotations:forPage:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, nuint page, bool append);

		[Export ("setAnnotations:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, bool append);

		[Export ("removeAllAnnotationsWithOptions:")]
		void RemoveAllAnnotations ([NullAllowed] NSDictionary options);

		[Export ("allAnnotations")]
		PSPDFAnnotation [] AllAnnotations { get; }

		[Export ("annotations")]
		NSDictionary Annotations { get; }

		[Export ("clearNeedsSaveFlag")]
		void ClearNeedsSaveFlag ();

		[Export ("setAnnotationCacheDirect:")]
		void SetAnnotationCacheDirect (NSDictionary annotationCache);

		[Export ("registerAnnotationsForUndo:")]
		void RegisterAnnotationsForUndo (PSPDFAnnotation [] annotations);

		[Export ("annotationCache", ArgumentSemantic.Strong), NullAllowed]
		NSMutableDictionary AnnotationCache { get; }

		[Export ("willInsertAnnotations:")]
		void WillInsertAnnotations (PSPDFAnnotation [] annotations);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFFileAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsForPage:pageRef:"), Internal]
		PSPDFAnnotation [] AnnotationsForPage (nuint page, IntPtr pageRef);

		[Export ("addAnnotations:options:")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("removeAnnotations:options:")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("tryLoadAnnotationsFromFileWithError:"), Internal]
		bool TryLoadAnnotationsFromFile (IntPtr error);

		// PSPDFFileAnnotationProvider (Advanced) Category

		[Export ("saveableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SaveableTypes { get; set; }

		[Export ("parsableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType ParsableTypes { get; set; }

		[Export ("annotationsPath")]
		string AnnotationsPath { get; set; }

		// PSPDFFileAnnotationProvider (SubclassingHooks) Category

		[Export ("parseAnnotationsForPage:pageRef:"), Internal]
		PSPDFAnnotation [] ParseAnnotations (nuint page, IntPtr pageRef);

		[Export ("saveAnnotationsWithOptions:error:"), Internal]
		bool SaveAnnotations (NSDictionary options, IntPtr error);

		[Export ("loadAnnotationsWithError:"), Internal]
		NSDictionary LoadAnnotations (IntPtr error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractTextOverlayAnnotation {

		[Static, Export ("textOverlayAnnotationWithGlyphs:pageRotationTransform:")]
		PSPDFAbstractTextOverlayAnnotation TextOverlayAnnotationWithGlyphs (PSPDFGlyph [] glyphs, CGAffineTransform pageRotationTransform);

		[Export ("highlightedString")]
		string HighlightedString ();
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
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFNoteAnnotation {

		[Export ("initWithContents:")]
		IntPtr Constructor (string contents);

		[Export ("iconName")]
		string IconName { get; set; }

		// PSPDFNoteAnnotation (SubclassingHooks) Category

		[Export ("renderAnnnotationIcon")]
		UIImage RenderAnnnotationIcon ();

		[Export ("drawImageInContext:boundingBox:")]
		void DrawImageInContext (CGContext context, CGRect boundingBox);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFInkAnnotation {

		[Export ("initWithLines:")][Internal]
		IntPtr InitWithLines (NSArray lines);

		[Export ("lines", ArgumentSemantic.Copy)][Internal]
		NSArray _Lines { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Copy)]
		UIBezierPath BezierPath { get; }

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("isSignature", ArgumentSemantic.Assign)]
		bool IsSignature { get; set; }

		[Export ("setBoundingBox:transformLines:")]
		void SetBoundingBox (CGRect boundingBox, bool transformLines);

		[Export ("copyLinesByApplyingTransform:")]
		NSValue [] CopyLinesByApplyingTransform (CGAffineTransform transform);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractLineAnnotation {

		[Export ("initWithPoints:")]
		IntPtr Constructor (NSValue [] points);

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; set; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; set; }

		[Export ("bezierPath", ArgumentSemantic.Copy)]
		UIBezierPath BezierPath { get; }

		[Export ("setBoundingBox:transformPoints:")]
		void SetBoundingBox (CGRect boundingBox, bool transformPoints);
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

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFLinkAnnotation {

		[Export ("initWithLinkAnnotationType:")]
		IntPtr Constructor (PSPDFLinkAnnotationType linkAnotationType);

		[Export ("initWithAction:")]
		IntPtr Constructor (PSPDFAction action);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("linkType", ArgumentSemantic.Assign)]
		PSPDFLinkAnnotationType LinkType { get; set; }

		[Export ("action", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAction Action { get; set; }

		[Export ("URL", ArgumentSemantic.Copy)]
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

		[Export ("URLAction")]
		PSPDFURLAction UrlAction ();

		[Export ("targetString")]
		string TargetString ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSquareAnnotation {

		[Export ("bezierPath", ArgumentSemantic.Strong), NullAllowed]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCircleAnnotation {

		[Export ("bezierPath", ArgumentSemantic.Strong), NullAllowed]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFStampAnnotation {

		[Static, Export ("stampColorForSubject:")]
		UIColor StampColorForSubject (string subject);

		[Export ("initWithSubject:")]
		IntPtr Constructor (string subject);

		[Export ("initWithImage:")]
		IntPtr Constructor (UIImage image);

		[Export ("subtext")]
		string Subtext { get; set; }

		[Export ("localizedSubject")]
		string LocalizedSubject { get; set; }

		[Export ("image", ArgumentSemantic.Strong), NullAllowed]
		UIImage Image { get; set; }

		[Export ("imageTransform", ArgumentSemantic.Assign)]
		CGAffineTransform ImageTransform { get; set; }

		[Export ("loadImageWithTransform:error:"), Internal]
		UIImage LoadImage (CGAffineTransform transform, IntPtr error);

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

		[Export ("shouldRenderAppearanceStream", ArgumentSemantic.Assign)]
		bool ShouldRenderAppearanceStream { get; set; }

		[Export ("borderColor", ArgumentSemantic.Strong), NullAllowed][New]
		UIColor BorderColor { get; set; }

		[Export ("widgetRotation", ArgumentSemantic.Assign)]
		nint WidgetRotation { get; set; }

		[Export ("appearanceCharacteristics", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAppearanceCharacteristics AppearanceCharacteristics { get; set; }
	}

	interface IPSPDFStreamProvider { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFStreamProvider {

		[Export ("documentProvider")]
		PSPDFDocumentProvider GetDocumentProvider ();

		[Export ("streamPath")]
		string StreamPath ();

		[Export ("fileURLWithError:")]
		[Abstract]
		NSUrl FileUrlWithError (out NSError error);
	}


	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFScreenAnnotation : IPSPDFStreamProvider {

		[Export ("assetName")]
		string AssetName { get; }

		[Export ("mediaScreenWindowType", ArgumentSemantic.Assign)]
		PSPDFMediaScreenWindowType MediaScreenWindowType { get; }
	}

	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFRichMediaAnnotation : IPSPDFStreamProvider {

		[Export ("assetName")]
		string AssetName { get; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFileAnnotation {

		[Export ("appearanceName")]
		string AppearanceName { get; set; }

		[Export ("embeddedFile", ArgumentSemantic.Strong), NullAllowed]
		PSPDFEmbeddedFile EmbeddedFile { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSoundAnnotation {

		[Export ("initWithRate:channels:bits:encoding:")]
		IntPtr Constructor (nuint rate, nuint channels, nuint bits, NSString /*PSPDFSoundAnnotationEncoding*/ encoding);

		[Export ("initWithURL:error:")]
		IntPtr Constructor (NSUrl soundUrl, out NSError error);

		[Export ("controller", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSoundAnnotationController Controller { get; set; }

		[Export ("iconName")]
		string IconName { get; set; }

		[Export ("canRecord", ArgumentSemantic.Assign)]
		bool CanRecord { get; }

		[Export ("soundURL", ArgumentSemantic.Copy)]
		NSUrl SoundUrl { get; }

		[Export ("bits", ArgumentSemantic.Assign)]
		nuint Bits { get; }

		[Export ("rate", ArgumentSemantic.Assign)]
		nuint Rate { get; }

		[Export ("channels", ArgumentSemantic.Assign)]
		nuint Channels { get; }

		[Export ("encoding")]
		NSString /*PSPDFSoundAnnotationEncoding*/ Encoding { get; }

		[Export ("loadAttributesFromAudioFile:"), Internal]
		bool LoadAttributesFromAudioFile (IntPtr error);

		[Export ("soundData")]
		NSData SoundData ();
	}

	[Static]
	interface PSPDFSoundAnnotationEncoding
	{
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

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAppearanceCharacteristics {

		[Static, Export ("appearanceCharacteristicsFromPDFDictionary:"), Internal]
		PSPDFAppearanceCharacteristics FromPdfDictionary (IntPtr apCharacteristicsDict);

		[Export ("normalCaption")]
		string NormalCaption { get; set; }

		[Export ("rolloverCaption")]
		string RolloverCaption { get; set; }

		[Export ("alternateCaption")]
		string AlternateCaption { get; set; }

		[Export ("textPosition", ArgumentSemantic.Assign)]
		PSPDFAppearanceCharacteristicsTextPosition TextPosition { get; set; }

		[Export ("normalIcon", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStream NormalIcon { get; set; }

		[Export ("rolloverIcon", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStream RolloverIcon { get; set; }

		[Export ("alternateIcon", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStream AlternateIcon { get; set; }

		[Export ("iconFit", ArgumentSemantic.Strong), NullAllowed]
		PSPDFIconFit IconFit { get; set; }

		// PSPDFAppearanceCharacteristics (PDFRepresentation) Category

		[Export ("partialPDFString")]
		string PartialPdfString ();
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFIconFit {

		[Static, Export ("iconFitFromPDFDictionary:"), Internal]
		PSPDFIconFit IconFitFromPdfDictionary (IntPtr iconFitDict);

		[Export ("scaleMode", ArgumentSemantic.Assign)]
		PSPDFIconFitScaleMode ScaleMode { get; set; }

		[Export ("scaleType", ArgumentSemantic.Assign)]
		PSPDFIconFitScaleType ScaleType { get; set; }

		[Export ("leftoverSpace", ArgumentSemantic.Copy)]
		NSNumber [] LeftoverSpace { get; set; }

		[Export ("scaleButtonAppearanceWithoutConsideringBorder", ArgumentSemantic.Assign)]
		bool ScaleButtonAppearanceWithoutConsideringBorder { get; set; }

		// PSPDFIconFit (PDFRepresentation) Category

		[Export ("PDFString")]
		string PdfString ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFImageFile {

		[Static, Export ("imageFileWithPath:")]
		PSPDFImageFile FromPath (string path);

		[Export ("initWithPath:")]
		IntPtr Constructor (string path);

		[Export ("image", ArgumentSemantic.Strong), NullAllowed]
		UIImage Image { get; }

		[Export ("imageSize", ArgumentSemantic.Assign)]
		CGSize ImageSize { get; }

		[Export ("path")]
		string Path { get; }
	}


	[BaseType (typeof (NSObject))]
	interface PSPDFStream {

	}

	interface IPSPDFAnnotationStateManagerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManagerDelegate {

		[Export ("annotationStateManager:didChangeState:to:variant:to:")]
		void DidChangeState (PSPDFAnnotationStateManager manager, NSString state, NSString newState, NSString variant, NSString newVariant);

		[Export ("annotationStateManager:didChangeUndoState:redoState:")]
		void DidChangeUndoState (PSPDFAnnotationStateManager manager, bool undoEnabled, bool redoEnabled);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManager : IPSPDFOverridable {

		[Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; }

		[Export ("stateDelegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFAnnotationStateManagerDelegate StateDelegate { get; set; }

		[Export ("state")]
		NSString State { get; set; }

		[Export ("toggleState:")]
		void ToggleState (NSString state);

		[Export ("variant")]
		NSString Variant { get; set; }

		[Export ("setState:variant:")]
		void SetState (NSString state, NSString variant);

		[Export ("toggleState:variant:")]
		void ToggleState ([NullAllowed] NSString state, [NullAllowed] NSString variant);

		[Export ("stateVariantIdentifier")]
		NSString StateVariantIdentifier { get; }

		[Export ("drawingInputMode", ArgumentSemantic.Assign)]
		PSPDFDrawViewInputMode DrawingInputMode { get; set; }

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

		[Export ("fontName")]
		string FontName { get; set; }

		[Export ("fontSize", ArgumentSemantic.Assign)]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("allowedImageQualities", ArgumentSemantic.Assign)]
		PSPDFImageQuality AllowedImageQualities { get; set; }
		[Export ("undo")]
		void Undo ();

		[Export ("redo")]
		void Redo ();

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("showStylePickerFrom:presentationOptions:")]
		void ShowStylePickerFrom (NSObject sender, [NullAllowed] NSDictionary options);

		[Export ("toggleSignatureControllerFrom:presentationOptions:")]
		void ToggleSignatureControllerFrom (NSObject sender, [NullAllowed] NSDictionary options);

		[Export ("toggleStampControllerFrom:includeSavedAnnotations:")]
		void ToggleStampControllerFrom (NSObject sender, bool includeSavedAnnotations);

		[Export ("toggleStampControllerFrom:includeSavedAnnotations:presentationOptions:")]
		void ToggleStampControllerFrom (NSObject sender, bool includeSavedAnnotations, [NullAllowed] NSDictionary options);

		[Export ("toggleImagePickerControllerFrom:presentationOptions:")]
		void ToggleImagePickerControllerFrom (NSObject sender, [NullAllowed] NSDictionary options);

		[Export ("performSelectionOnPageView:at:")]
		void PerformSelectionOnPageView (PSPDFPageView pageView, CGPoint point);

		// PSPDFAnnotationStateManager (StateHelper) Category

		[Export ("isDrawingState:")]
		bool IsDrawingState (NSString state);

		[Export ("isHighlightAnnotationState:")]
		bool IsHighlightAnnotationState (NSString state);

		// PSPDFAnnotationStateManager (SubclassingHooks) Category

		[Export ("drawViews", ArgumentSemantic.Strong), NullAllowed]
		NSDictionary DrawViews { get; }

		[Export ("cancelDrawingAnimated:")]
		void CancelDrawing (bool animated);

		[Export ("doneDrawingAnimated:")]
		void DoneDrawing (bool animated);

		[Export ("setLastUsedColor:annotationString:")]
		void SetLastUsedColor (UIColor lastUsedDrawColor, NSString annotationString);

		[Export ("lastUsedColorForAnnotationString:")]
		UIColor LastUsedColor (NSString annotationString);

		[Export ("finishDrawingAnimated:saveAnnotation:")]
		void FinishDrawing (bool animated, bool saveAnnotation);

		[Export ("stateShowsStylePicker:")]
		bool StateShowsStylePicker (NSString state);

		// PSPDFAnnotationStateManager (StylusSupport) Category

		[Export ("stylusStatusButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFlexibleToolbarButton StylusStatusButton { get; }

		[Export ("stylusSupport")]
		PSPDFAnnotationStateManagerStylusSupport StylusSupport ();
	}

	[BaseType (typeof (PSPDFTableViewCell))]
	interface PSPDFNonAnimatingTableViewCell {

	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFAnnotationCell {

		[Static, Export ("heightForAnnotation:constrainedToSize:")]
		nfloat HeightForAnnotation (PSPDFAnnotation annotation, CGSize constrainedToSize);

		[Export ("annotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		// PSPDFAnnotationCell (SubclassingHooks) Category

		[Static, Export ("dateAndUserStringForAnnotation:")]
		string DateAndUserString (PSPDFAnnotation annotation);
	}

	interface IPSPDFSignatureStore {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureStore {

		[Export ("initWithStoreName:")]
		IntPtr Constructor (string storeName);

		[Export ("addSignature:")]
		[Abstract]
		void AddSignature (PSPDFInkAnnotation signature);

		[Export ("removeSignature:")]
		[Abstract]
		bool RemoveSignature (PSPDFInkAnnotation signature);

		[Export ("signatures")]
		PSPDFInkAnnotation [] GetSignatures ();

		[Export ("setSignatures:")]
		void SetSignatures (PSPDFInkAnnotation [] signatures);

		[Export ("storeName")]
		string GetStoreName ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFKeychainSignatureStore : IPSPDFSignatureStore {

		[Field ("PSPDFKeychainSignatureStoreDefaultStoreName", "__Internal")]
		NSString DefaultStoreName { get; }
	}

	[BaseType (typeof (UICollectionViewCell))]
	interface PSPDFSelectableCollectionViewCell {

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
		void SetAnnotation (PSPDFAnnotation annotation);

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
		void SetPageView (PSPDFPageView PageView);

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
		bool ShouldSyncRemovalFromSuperview ();

		[Export ("willRemoveFromSuperview")]
		void WillRemoveFromSuperview ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFLinkAnnotationBaseView : IPSPDFAnnotationViewProtocol {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("linkAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("zIndex", ArgumentSemantic.Assign)]
		nuint ZIndex { get; set; }

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		UIView ContentView { get; }
	}

	[BaseType (typeof (PSPDFLinkAnnotationBaseView))]
	interface PSPDFWebAnnotationView {

		[Export ("webView", ArgumentSemantic.Strong), NullAllowed]
		UIWebView WebView { get; }
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

		[Export ("hideSmallLinks", ArgumentSemantic.Assign)]
		bool HideSmallLinks { [Bind ("shouldHideSmallLinks")] get; set; }

		[Export ("overspan", ArgumentSemantic.Assign)]
		CGSize Overspan { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFNoteAnnotationView {

		[Export ("initWithAnnotation:")]
		IntPtr Constructor (PSPDFAnnotation noteAnnotation);

		[Export ("annotationImageView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView AnnotationImageView { get; set; }

		// PSPDFNoteAnnotationView (SubclassingHooks) Category

		[Export ("renderNoteImage")]
		UIImage RenderNoteImage ();

		[Export ("updateImageAnimated:")]
		void UpdateImage (bool animated);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFSoundAnnotationView : IPSPDFAnnotationViewProtocol {

		[Export ("soundAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSoundAnnotation SoundAnnotation { get; }

		[Export ("editViewController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSoundAnnotationEditViewController EditViewController { get; }

		[Export ("showEditorAnimated:")]
		void ShowEditor (bool animated);

		[Export ("hideEditorAnimated:")]
		void HideEditor (bool animated);

		[Export ("editorVisible")]
		bool EditorVisible { get; }

		[Export ("shouldShowEditMenu")]
		bool ShouldShowEditMenu { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMicrophonePlotView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("waveformLineWidth", ArgumentSemantic.Assign)]
		nfloat WaveformLineWidth { get; set; }

		[Export ("waveformColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor WaveformColor { get; set; }

		[Export ("lineSmoothEnabled", ArgumentSemantic.Assign)]
		bool LineSmoothEnabled { get; set; }

		[Export ("isRendering", ArgumentSemantic.Assign)]
		bool IsRendering { get; }

		[Export ("start")]
		void Start ();

		[Export ("stop")]
		void Stop ();
	}

	[BaseType (typeof (UIView))]
	interface PSPDFAudioPlotView
	{
		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("waveformColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor WaveformColor { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFAudioPlotViewDelegate Delegate { get; set; }
	}

	interface IPSPDFAudioPlotViewDelegate { }

	[Model, Protocol]
	[BaseType (typeof (NSObject))]
	interface PSPDFAudioPlotViewDelegate
	{
		[Export ("URLForAudioPlotView:")]
		NSUrl UrlForAudioPlotView (PSPDFAudioPlotView view);
	}

	[BaseType (typeof (UIViewController))]
	interface PSPDFSoundAnnotationEditViewController {

		[Export ("microphonePlotView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMicrophonePlotView MicrophonePlotView { get; }

		[Export ("plotView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAudioPlotView PlotView { get; }

		[Export ("recordingButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton RecordingButton { get; }

		[Export ("playbackButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton PlaybackButton { get; }

		[Export ("doneButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton DoneButton { get; }

		[Export ("playbackTimeLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel PlaybackTimeLabel { get; }

		[Export ("totalTimeLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel TotalTimeLabel { get; }

		[Export ("statusLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel StatusLabel { get; }

		[Export ("progressSlider", ArgumentSemantic.Strong), NullAllowed]
		UISlider ProgressSlider { get; }
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
	interface PSPDFResizableView : IPSPDFLongPressGestureRecognizerDelegate {

		[Export ("initWithTrackedView:")]
		IntPtr Constructor (UIView trackedView);

		[Export ("trackedViews", ArgumentSemantic.Copy)]
		NSSet TrackedViews { get; set; }

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

		[Export ("limitMode", ArgumentSemantic.Assign)]
		PSPDFResizableViewLimitMode LimitMode { get; set; }

		[Export ("selectionBorderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionBorderColor { get; set; }

		[Export ("selectionBorderWidth", ArgumentSemantic.Assign)]
		nfloat SelectionBorderWidth { get; set; }

		[Export ("guideBorderColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor GuideBorderColor { get; set; }

		[Export ("cornerRadius", ArgumentSemantic.Assign)]
		nuint CornerRadius { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFResizableViewDelegate Delegate { get; set; }

		[Export ("contentFrame", ArgumentSemantic.Assign)]
		CGRect ContentFrame { get; set; }

		[Export ("mode", ArgumentSemantic.Assign)]
		PSPDFResizableViewMode Mode { get; set; }

		[Export ("pageView", ArgumentSemantic.Weak)]
		PSPDFPageView PageView { get; set; }

		[Export ("effectiveInnerEdgeInsets")]
		UIEdgeInsets EffectiveInnerEdgeInsets ();

		[Export ("effectiveOuterEdgeInsets")]
		UIEdgeInsets EffectiveOuterEdgeInsets ();

		[Export ("longPress:")]
		bool LongPress (UILongPressGestureRecognizer recognizer);
	}

	interface IPSPDFResizableViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFResizableViewDelegate {

		[Export ("resizableViewDidBeginEditing:")]
		void ResizableViewDidBeginEditing (PSPDFResizableView resizableView);

		[Export ("resizableViewChangedFrame:outerKnobType:")]
		void OuterKnobType (PSPDFResizableView resizableView, PSPDFResizableViewOuterKnob outerKnobType);

		[Export ("resizableViewDidEndEditing:")]
		void ResizableViewDidEndEditing (PSPDFResizableView resizableView);
	}

	[BaseType (typeof (PSPDFAnnotationView))]
	interface PSPDFHostingAnnotationView : PSPDFRenderDelegate {

		[Export ("annotationImageView", ArgumentSemantic.Strong), NullAllowed]
		UIImageView AnnotationImageView { get; }

		[Export ("shouldSyncRemovalFromSuperview", ArgumentSemantic.Assign)]
		bool ShouldSyncRemovalFromSuperview { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFAnnotationView : IPSPDFAnnotationViewProtocol {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("annotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotation Annotation { get; set; }

		[Export ("pageView", ArgumentSemantic.Weak)]
		PSPDFPageView PageView { get; set; }
	}

	[BaseType (typeof (PSPDFHostingAnnotationView))]
	interface PSPDFFreeTextAnnotationView : IPSPDFResizableTrackedViewDelegate {

		[Export ("textView", ArgumentSemantic.Strong), NullAllowed]
		UITextView TextView { get; }

		[Export ("resizableView", ArgumentSemantic.Weak)]
		PSPDFResizableView ResizableView { get; set; }

		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		// PSPDFFreeTextAnnotationView (SubclassingHooks) Category

		[Export ("textViewForEditing")]
		UITextView TextViewForEditing ();
	}

	interface IPSPDFFreeTextAccessoryViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFreeTextAccessoryViewDelegate {

		[Export ("doneButtonPressedOnFreeTextAccessoryView:")]
		[Abstract]
		void DoneButtonPressedOnFreeTextAccessoryView (PSPDFFreeTextAccessoryView inputView);

		[Export ("inspectorButtonPressedOnFreeTextAccessoryView:")]
		[Abstract]
		void InspectorButtonPressedOnFreeTextAccessoryView (PSPDFFreeTextAccessoryView inputView);

		[Export ("clearButtonPressedOnFreeTextAccessoryView:")]
		[Abstract]
		void ClearButtonPressedOnFreeTextAccessoryView (PSPDFFreeTextAccessoryView inputView);

		[Export ("freeTextAccessoryViewShouldEnableClearButton:")]
		[Abstract]
		bool FreeTextAccessoryViewShouldEnableClearButton (PSPDFFreeTextAccessoryView inputView);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIView))]
	interface PSPDFFreeTextAccessoryView {

		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (CGRect frame, [NullAllowed] IPSPDFFreeTextAccessoryViewDelegate aDelegate);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFreeTextAccessoryViewDelegate Delegate { get; set; }

		[Export ("updateToolbar")]
		void UpdateToolbar ();

		// PSPDFFreeTextAnnotationView (SubclassingHooks) Category

		[Export ("doneButton", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem DoneButton { get; }

		[Export ("clearButton", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem ClearButton { get; }

		[Export ("inspectorButton", ArgumentSemantic.Strong), NullAllowed]
		UIBarButtonItem InspectorButton { get; }

		[Export ("doneButtonPressed:")]
		void DoneButtonPressed (NSObject sender);

		[Export ("clearButtonPressed:")]
		void ClearButtonPressed (NSObject sender);

		[Export ("inspectorButtonPressed:")]
		void InspectorButtonPressed (NSObject sender);
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

		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (CGRect frame, [NullAllowed] IPSPDFSelectionViewDelegate aDelegate);

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSelectionViewDelegate Delegate { get; set; }

		[Export ("selectionColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor SelectionColor { get; set; }

		[Export ("wordSelectionColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor WordSelectionColor { get; set; }

		[Export ("rects", ArgumentSemantic.Copy), NullAllowed]
		NSValue [] Rects { get; set; }

		[Export ("setRawRects:count:")]
		void SetRawRects (CGRect rawRects, nuint count);

		[Export ("tapGestureRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer TapGestureRecognizer { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDrawAction
	{
		[Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; }

		[Export ("strokeColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor StrokeColor { get; }

		[Export ("fillColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor FillColor { get; }

		[Export ("lineWidth", ArgumentSemantic.Assign)]
		nfloat LineWidth { get; }

		[Export ("lineEnd1", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd1 { get; }

		[Export ("lineEnd2", ArgumentSemantic.Assign)]
		PSPDFLineEndType LineEnd2 { get; }
	}

	interface IPSPDFAnimatableShapeUpdates { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnimatableShapeUpdates {

		[Export ("animateShapeBoundsChanges", ArgumentSemantic.Assign)]
		bool AnimateShapeBoundsChanges { get; set; }

		[Export ("shapeBoundsChangeAnimationDuration", ArgumentSemantic.Assign)]
		double ShapeBoundsChangeAnimationDuration { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFDrawView : IPSPDFAnnotationViewProtocol, IPSPDFUndoProtocol, IPSPDFAnimatableShapeUpdates {

		[Export ("annotationType", ArgumentSemantic.Assign)]
		PSPDFAnnotationType AnnotationType { get; set; }

		[Export ("inputMode", ArgumentSemantic.Assign)]
		PSPDFDrawViewInputMode InputMode { get; set; }

		[Export ("currentAction", ArgumentSemantic.Strong), NullAllowed]
		NSObject CurrentAction { get; }

		[Export ("actionList", ArgumentSemantic.Strong), NullAllowed]
		NSObject [] ActionList { get; }

		[Export ("combineInk", ArgumentSemantic.Assign)]
		bool CombineInk { get; set; }

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

		[Export ("naturalDrawingEnabled", ArgumentSemantic.Assign)]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("undoController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFUndoController UndoController { get; set; }

		[Export ("scale", ArgumentSemantic.Assign)]
		nfloat Scale { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }

		[Export ("clearAllActions")]
		void ClearAllActions ();

		[Export ("removeUndoHistory")]
		void RemoveUndoHistory ();

		[Export ("updateActionsForAnnotations:")]
		void UpdateActionsForAnnotations (NSObject [] annotations);

		[Export ("newAnnotationsFromActionsList")]
		PSPDFAnnotation [] NewAnnotationsFromActionsList ();

		[Export ("writeChangesToAnnotations")]
		void WriteChangesToAnnotations ();

		// PSPDFDrawView (SubclassingHooks) Category

		[Export ("shouldProcessTouches:withEvent:")]
		bool ShouldProcessTouches (NSSet touches, UIEvent aEvent);

		[Export ("startDrawingAtPoint:")]
		void StartDrawingAtPoint (CGPoint location);

		[Export ("continueDrawingAtPoint:")]
		void ContinueDrawingAtPoint (CGPoint location);

		[Export ("endDrawing")]
		void EndDrawing ();

		[Export ("cancelDrawing")]
		void CancelDrawing ();
	}

	[BaseType (typeof (UITableViewCell))]
	interface PSPDFSignatureCell {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFModel))]
	interface PSPDFEmbeddedFile : IPSPDFStreamProvider {

		[Export ("initWithFileName:size:description:modificationDate:")]
		IntPtr Constructor (string fileName, nuint fileSize, string description, NSDate modificationDate);

		[Export ("fileName")]
		string FileName { get; }

		[Export ("fileSize", ArgumentSemantic.Assign)]
		nuint FileSize { get; }

		[Export ("fileDescription")]
		string FileDescription { get; }

		[Export ("modificationDate", ArgumentSemantic.Strong), NullAllowed]
		NSDate ModificationDate { get; }

		[Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFEmbeddedFilesParser {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("embeddedFiles", ArgumentSemantic.Copy)]
		PSPDFEmbeddedFile [] EmbeddedFiles { get; }
	}

	interface IPSPDFEmbeddedFilesViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFEmbeddedFilesViewControllerDelegate {

		[Export ("embeddedFilesController:didSelectFile:sender:")]
		[Abstract]
		void DidSelectFile (PSPDFEmbeddedFilesViewController embeddedFilesController, PSPDFEmbeddedFile embeddedFile, NSObject sender);
	}

	[BaseType (typeof (PSPDFStatefulTableViewController))]
	interface PSPDFEmbeddedFilesViewController {

		[Export ("initWithDocument:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] IPSPDFEmbeddedFilesViewControllerDelegate aDelegate);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFEmbeddedFilesViewControllerDelegate Delegate { get; set; }
	}

	[BaseType (typeof (PSPDFNonAnimatingTableViewCell))]
	interface PSPDFEmbeddedFileCell {

	}

	[DisableDefaultCtor]
	[BaseType (typeof (UIBarButtonItem))]
	interface PSPDFBarButtonItem {

		[Export ("initWithPDFViewController:")]
		IntPtr Constructor (PSPDFViewController pdfViewController);

		[Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; set; }

		[Static, Export ("dismissPopoverAnimated:completion:")]
		bool DismissPopover (bool animated, [NullAllowed] Action completion);

		[Static, Export ("isPopoverVisible")]
		bool IsPopoverVisible ();

		[Export ("customView")][New]
		UIView CustomView { get; }

		[Export ("image")][New]
		UIImage Image { get; }

		[Export ("systemItem")]
		UIBarButtonSystemItem SystemItem ();

		[Export ("landscapeImagePhone")][New]
		UIImage LandscapeImagePhone { get; }

		[Export ("actionName")]
		string ActionName ();

		[Export ("isAvailable")]
		bool IsAvailable ();

		[Export ("updateBarButtonItem")]
		void UpdateBarButtonItem ();

		[Export ("presentAnimated:sender:")]
		NSObject PresentAnimated (bool animated, NSObject sender);

		[Export ("dismissAnimated:completion:")]
		bool DismissAnimated (bool animated, [NullAllowed] Action completion);

		[Export ("didDismiss")]
		void DidDismiss ();

		[Export ("setPresentedObject:sender:")]
		void SetPresentedObject (NSObject presentedObject, NSObject sender);

		[Export ("presentViewController:sender:")]
		NSObject PresentViewController (UIViewController viewController, NSObject sender);

		[Export ("dismissModalOrPopoverAnimated:completion:")]
		bool DismissModalOrPopoverAnimated (bool animated, [NullAllowed] Action completion);

		[Static, Export ("popoverControllerForObject:")]
		UIPopoverController PopoverControllerForObject (NSObject obj);

		[Export ("action:")]
		void Actionn (NSObject sender);

		// PSPDFBarButtonItem (Advanced) Category

		[Export ("actionSheet", ArgumentSemantic.Strong), NullAllowed]
		UIActionSheet ActionSheet { get; set; }

		[Export ("dismissingSheet", ArgumentSemantic.Assign)]
		bool DismissingSheet { [Bind ("isDismissingSheet")] get; set; }

		[Export ("activeTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor ActiveTintColor { get; set; }

		[Export ("itemStyle")]
		UIBarButtonItemStyle ItemStyle ();

		[Export ("imageWithActiveState:")]
		UIImage ImageWithActiveState (UIImage image);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFCloseBarButtonItem {

	}

	interface IPSPDFDocumentSharingViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSharingViewControllerDelegate : PSPDFOverridable {

		[Export ("documentSharingViewController:didFinishWithSelectedOptions:resultingObjects:fileNames:annotationSummary:error:")]
		[Abstract]
		void DidFinishWithSelectedOptions (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, NSObject [] resultingObjects, string [] fileNames, NSAttributedString annotationSummary, NSError error);

		[Export ("documentSharingViewControllerDidCancel:")]
		void DocumentSharingViewControllerDidCancel (PSPDFDocumentSharingViewController shareController);

		[Export ("documentSharingViewController:shouldPrepareWithSelectedOptions:selectedPages:")]
		bool ShouldPrepareWithSelectedOptions (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions selectedSharingOption, NSIndexSet selectedPages);

		[Export ("documentSharingViewController:titleForOption:")]
		string TitleForOption (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions option);

		[Export ("documentSharingViewController:subtitleForOption:")]
		string SubtitleForOption (PSPDFDocumentSharingViewController shareController, PSPDFDocumentSharingOptions option);

		[Export ("processorOptionsForDocumentSharingViewController:")]
		NSDictionary ProcessorOptionsForDocumentSharingViewController (PSPDFDocumentSharingViewController shareController);

		[Export ("temporaryDirectoryForDocumentSharingViewController:")]
		string TemporaryDirectoryForDocumentSharingViewController (PSPDFDocumentSharingViewController shareController);
	}

	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFDocumentSharingViewController : PSPDFStyleable {

		[Export ("initWithDocument:visiblePages:allowedSharingOptions:delegate:")]
		IntPtr Constructor (PSPDFDocument document, [NullAllowed] NSOrderedSet visiblePages, PSPDFDocumentSharingOptions sharingOptions, PSPDFDocumentSharingViewControllerDelegate aDelegate);

		[Export ("document", ArgumentSemantic.Strong), NullAllowed]
		PSPDFDocument Document { get; }

		[Export ("visiblePages", ArgumentSemantic.Copy), NullAllowed]
		NSOrderedSet VisiblePages { get; set; }

		[Export ("sharingOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SharingOptions { get; set; }

		[Export ("selectedOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SelectedOptions { get; set; }

		[Export ("commitButtonTitle")]
		string CommitButtonTitle { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFDocumentSharingViewControllerDelegate Delegate { get; set; }

		[Export ("checkIfControllerHasOptionsAvailableAndCallDelegateIfNot")]
		bool CheckIfControllerHasOptionsAvailableAndCallDelegateIfNot ();

		[Export ("commitWithCurrentSettings")]
		bool CommitWithCurrentSettings ();
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFEmailBarButtonItem : IPSPDFDocumentSharingViewControllerDelegate {

		[Export ("sendOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SendOptions { get; set; }

		// PSPDFEmailBarButtonItem (SubclassingHooks) Category

		[Export ("mailComposeViewController", ArgumentSemantic.Weak)]
		MFMailComposeViewController MailComposeViewController { get; }

		[Export ("showEmailControllerWithSendOptions:dataArray:fileNames:sender:annotationSummary:animated:")]
		void ShowEmailControllerWithSendOptions (PSPDFDocumentSharingOptions sendOptions, NSObject [] dataArray, string [] fileNames, NSObject sender, NSAttributedString annotationSummary, bool animated);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFMessageBarButtonItem {

		[Export ("sendOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions SendOptions { get; set; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFOpenInBarButtonItem : IPSPDFDocumentSharingViewControllerDelegate, IUIDocumentInteractionControllerDelegate {

		[Notification]
		[Field ("PSPDFDocumentInteractionControllerWillBeginSendingToApplicationNotification", "__Internal")]
		NSString WillBeginSendingToApplicationNotification { get; }

		[Notification]
		[Field ("PSPDFDocumentInteractionControllerDidEndSendingToApplicationNotification", "__Internal")]
		NSString DidEndSendingToApplicationNotification { get; }

		[Export ("openOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions OpenOptions { get; set; }

		[Export ("documentInteractionController", ArgumentSemantic.Strong), NullAllowed]
		UIDocumentInteractionController DocumentInteractionController { get; }

		// PSPDFOpenInBarButtonItem (SubclassingHooks) Category

		[Export ("interactionControllerWithURL:")]
		UIDocumentInteractionController InteractionController (NSUrl fileURL);

		[Export ("showOpenInControllerWithOptions:fileURL:animated:sender:")]
		void ShowOpenInController (PSPDFDocumentSharingOptions options, NSUrl fileURL, bool animated, NSObject sender);

		[Export ("presentOpenInMenuFromBarButtonItem:animated:")]
		bool PresentOpenInMenuFromBarButtonItem (NSObject sender, bool animated);

		[Export ("presentOpenInMenuFromRect:inView:animated:")]
		bool PresentOpenInMenuFromRect (CGRect senderRect, NSObject sender, bool animated);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFPrintBarButtonItem : IPSPDFDocumentSharingViewControllerDelegate, IUIPrintInteractionControllerDelegate {

		[Export ("printOptions", ArgumentSemantic.Assign)]
		PSPDFDocumentSharingOptions PrintOptions { get; set; }

		// PSPDFPrintBarButtonItem (SubclassingHooks) Category

		[Export ("printInfo")]
		UIPrintInfo PrintInfo ();
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFSearchBarButtonItem {

	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFMoreBarButtonItem {

		[Export ("shouldConsolidateIfOnlyOneOptionAvailable", ArgumentSemantic.Assign)]
		bool ShouldConsolidateIfOnlyOneOptionAvailable { get; set; }
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFViewModeBarButtonItem {

		[Export ("viewModeStyle", ArgumentSemantic.Assign)]
		PSPDFViewModeBarButtonStyle ViewModeStyle { get; set; }

		// PSPDFViewModeBarButtonItem (SubclassingHooks) Category

		[Export ("viewModeSegment", ArgumentSemantic.Strong), NullAllowed]
		PSPDFCustomImagesSegmentedControl ViewModeSegment { get; }
	}

	[BaseType (typeof (PSPDFSegmentedControl))]
	interface PSPDFCustomImagesSegmentedControl {

		[Export ("setSelectedImage:forSegmentAtIndex:")]
		void SetSelectedImage (UIImage image, nuint segment);

		[Export ("selectedImageForSegmentAtIndex:")]
		UIImage SelectedImageForSegmentAtIndex (nuint segment);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFSelectableBarButtonItem {

		[Export ("selected", ArgumentSemantic.UnsafeUnretained)]
		bool Selected { [Bind ("isSelected")] get; set; }

		[Export ("setSelected:animated:")]
		void SetSelected (bool selected, bool animated);
	}

	[BaseType (typeof (PSPDFSelectableBarButtonItem))]
	interface PSPDFAnnotationBarButtonItem {

		[Export ("annotationToolbar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationToolbar AnnotationToolbar { get; }

		[Export ("hostView", ArgumentSemantic.Strong), NullAllowed]
		UIView HostView { get; set; }

		[Export ("toggleAnnotationToolbar")]
		void ToggleAnnotationToolbar ();

		[Export ("showAnnotationToolbarAnimated:")]
		bool ShowAnnotationToolbarAnimated (bool animated);

		[Export ("hideDisplayedToolbarAnimated:")]
		bool HideDisplayedToolbarAnimated (bool animated);

		[Export ("isAvailableBlocking")]
		bool IsAvailableBlocking ();
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFBookmarkBarButtonItem {

		[Export ("tapChangesBookmarkStatus", ArgumentSemantic.Assign)]
		bool TapChangesBookmarkStatus { get; set; }

		// PSPDFBookmarkBarButtonItem (SubclassingHooks) Category

		[Export ("bookmarkNumberForVisiblePages")]
		NSNumber BookmarkNumberForVisiblePages ();
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFBrightnessBarButtonItem {

		[Export ("setBrightnessControllerCustomizationBlock:")]
		void SetBrightnessControllerCustomizationHandler (Action<PSPDFBrightnessViewController> handler);
	}

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFOutlineBarButtonItem {

		[Export ("availableControllerOptions", ArgumentSemantic.Copy), NullAllowed]
		NSOrderedSet AvailableControllerOptions { get; set; }

		[Export ("didCreateControllerBlock:")]
		void SetDidCreateControllerHandler (Action<UIViewController, PSPDFOutlineBarButtonItemOption> handler);

		[Export ("isAvailableBlocking")]
		bool IsAvailableBlocking ();

		// PSPDFOutlineBarButtonItem (SubclassingHooks) Category

		[Export ("titleForOption:")]
		string TitleForOption (PSPDFOutlineBarButtonItemOption option);

		[Export ("controllerForOption:")]
		UIViewController ControllerForOption (PSPDFOutlineBarButtonItemOption option);

		[Export ("isOptionAvailable:")]
		bool IsOptionAvailable (PSPDFOutlineBarButtonItemOption option);
	}

	[Static]
	interface PSPDFActivityType
	{
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

	delegate void PSPDFActivityBarButtonItemHandler (NSString activityType, bool completed, NSObject[] returnedItems, NSError activityError);

	[BaseType (typeof (PSPDFBarButtonItem))]
	interface PSPDFActivityBarButtonItem {

		[Export ("applicationActivities", ArgumentSemantic.Copy)]
		NSObject [] ApplicationActivities { get; set; }

		[Export ("excludedActivityTypes", ArgumentSemantic.Copy)]
		NSString [] ExcludedActivityTypes { get; set; }

		[Export ("activityController", ArgumentSemantic.Strong), NullAllowed]
		UIActivityViewController ActivityController { get; }

		[Export ("setCompletionHandler:")]
		void CompletionHandler (PSPDFActivityBarButtonItemHandler completionHandler);
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

	[BaseType (typeof (UIView))]
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

		[Export ("setButtons:animated:")]
		void SetButtons (UIButton [] buttons, bool animated);

		[Export ("buttons", ArgumentSemantic.Copy)]
		UIButton [] Buttons { get; set; }

		[Export ("selectedButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton SelectedButton { get; set; }

		[Export ("setSelectedButton:animated:")]
		void SetSelectedButton (UIButton button, bool animated);

		[Export ("showToolbarAnimated:completion:")]
		void ShowToolbarAnimated (bool animated, Action<bool> completionHandler);

		[Export ("hideToolbarAnimated:completion:")]
		void HideToolbarAnimated (bool animated, Action<bool> completionHandler);

		[Export ("backgroundView", ArgumentSemantic.Strong), NullAllowed]
		UIView BackgroundView { get; set; }

		[Export ("dragView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFlexibleToolbarDragView DragView { get; }

		[Export ("barTintColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor BarTintColor { get; set; }

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

		[Export ("collapsedButtons", ArgumentSemantic.Copy)]
		PSPDFFlexibleToolbarButton [] CollapsedButtons { get; }

		[Export ("collapsedButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFFlexibleToolbarCollapsedButton CollapsedButton { get; }

		[Export ("showMenuWithItems:target:animated:")]
		void ShowMenuWithItems (PSPDFMenuItem [] menuItems, UIView target, bool animated);

		[Export ("showMenuForCollapsedButtons:fromButton:animated:")]
		void ShowMenuForCollapsedButtons (PSPDFFlexibleToolbarButton [] buttons, UIButton sourceButton, bool animated);

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

	[BaseType (typeof (UIButton))]
	interface PSPDFFlexibleToolbarButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("collapsible", ArgumentSemantic.Assign)]
		bool Collapsible { [Bind ("isCollapsible")] get; set; }

		[Export ("userInfo", ArgumentSemantic.Strong), NullAllowed]
		NSObject UserInfo { get; set; }

		[Export ("setImage:")]
		void SetImage (UIImage image);
	}

	[BaseType (typeof (PSPDFFlexibleToolbarButton))]
	interface PSPDFFlexibleToolbarGroupButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("longPressRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UILongPressGestureRecognizer LongPressRecognizer { get; }

		[Export ("groupIndicatorPosition", ArgumentSemantic.Assign)]
		PSPDFFlexibleToolbarGroupButtonIndicatorPosition GroupIndicatorPosition { get; set; }
	}

	[BaseType (typeof (PSPDFFlexibleToolbarGroupButton))]
	interface PSPDFFlexibleToolbarCollapsedButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("mimickedButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton MimickedButton { get; }
	}

	[BaseType (typeof (UIButton))]
	interface PSPDFFlexibleToolbarSpacerButton {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("flexible", ArgumentSemantic.Assign)]
		bool Flexible { [Bind ("isFlexible")] get; set; }

		[Export ("width", ArgumentSemantic.Assign)]
		nfloat Width { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFFlexibleToolbar))]
	interface PSPDFAnnotationToolbar : PSPDFAnnotationStateManagerDelegate {

		[Export ("initWithAnnotationStateManager:")]
		IntPtr Constructor (PSPDFAnnotationStateManager annotationStateManager);

		[Export ("annotationStateManager", ArgumentSemantic.Strong), NullAllowed]
		PSPDFAnnotationStateManager AnnotationStateManager { get; set; }

		[Export ("pdfController", ArgumentSemantic.Weak)]
		PSPDFViewController PdfController { get; }

		// TODO: Change this to NSOrderedSet once the comparison bug is fixed
		[Export ("editableAnnotationTypes", ArgumentSemantic.Copy), NullAllowed]
		NSObject EditableAnnotationTypes { get; set; }

		[Export ("annotationGroups", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroup [] AnnotationGroups { get; set; }

		[Export ("annotationGroupsOrGroupsFromEditableAnnotationTypes")]
		PSPDFAnnotationGroup [] AnnotationGroupsOrGroupsFromEditableAnnotationTypes ();

		[Export ("additionalButtons", ArgumentSemantic.Copy), NullAllowed]
		UIButton [] AdditionalButtons { get; set; }

		[Export ("saveAfterToolbarHiding", ArgumentSemantic.Assign)]
		bool SaveAfterToolbarHiding { get; set; }

		// PSPDFAnnotationToolbar (SubclassingHooks) Category

		[Export ("doneButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton DoneButton { get; }

		[Export ("undoButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton UndoButton { get; }

		[Export ("redoButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton RedoButton { get; }

		[Export ("strokeColorButton", ArgumentSemantic.Strong), NullAllowed]
		PSPDFColorButton StrokeColorButton { get; }

		[Export ("done:")]
		void Done (NSObject sender);
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

		[Export ("flexibleToolbarContainerContentRect:")]
		CGRect FlexibleToolbarContainerContentRect (PSPDFFlexibleToolbarContainer container);

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
		UIView OverlaidBar { get; set; }

		[Export ("dragging", ArgumentSemantic.Assign)]
		bool Dragging { get; }

		[Export ("flickToCloseEnabled", ArgumentSemantic.Assign)]
		bool FlickToCloseEnabled { [Bind ("isFlickToCloseEnabled")] get; set; }

		[Export ("containerDelegate", ArgumentSemantic.Weak)]
		[NullAllowed]
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
	interface PSPDFAnnotationGroup {

		[Static, Export ("groupWithItems:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items);

		[Static, Export ("groupWithItems:choice:")]
		PSPDFAnnotationGroup FromItems (PSPDFAnnotationGroupItem [] items, nuint choice);

		[Export ("choice", ArgumentSemantic.Assign)]
		nuint Choice { get; }

		[Export ("items", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItem [] Items { get; }
	}

	delegate UIImage PSPDFAnnotationGroupItemConfigurationHandler (PSPDFAnnotationGroupItem item, NSObject container, UIColor tintColor);

	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationGroupItem {

		[Static, Export ("itemWithType:")]
		PSPDFAnnotationGroupItem FromType (string type);

		[Static, Export ("itemWithType:variant:")]
		PSPDFAnnotationGroupItem FromType (string type, string variant);

		[Static, Export ("itemWithType:variant:configurationBlock:")]
		PSPDFAnnotationGroupItem FromType (string type, string variant, [NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Static, Export ("setDefaultConfigurationBlock:")]
		void SetDefaultConfigurationHanlder ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Static, Export ("setInkConfigurationBlock:")]
		void SetInkConfigurationHandler ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Static, Export ("setFreeTextConfigurationBlock:")]
		void SetFreeTextConfigurationHandler ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Export ("type")]
		string Type { get; }

		[Export ("variant")]
		string Variant { get; }

		[Export ("setConfigurationBlock:", ArgumentSemantic.Copy)]
		void SetConfigurationHandler ([NullAllowed] PSPDFAnnotationGroupItemConfigurationHandler handler);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFModel {

		[Static, Export ("modelWithDictionary:error:")]
		PSPDFModel FromDictionary (NSDictionary dictionaryValue, out NSError error);

		[Export ("initWithDictionary:error:")]
		IntPtr Constructor (NSDictionary dictionaryValue, out NSError error);

		[Static, Export ("propertyKeys")]
		NSOrderedSet PropertyKeys ();

		[Static, Export ("cachedPropertyKeys")]
		NSObject [] CachedPropertyKeys ();

		[Static, Export ("cachedPropertyKeySet")]
		NSObject CachedPropertyKeySet ();

		[Static, Export ("propertyKeysWithReferentialEquality")]
		NSOrderedSet PropertyKeysWithReferentialEquality ();

		[Export ("dictionaryValue", ArgumentSemantic.Copy)]
		NSDictionary DictionaryValue { get; }

		[Export ("mergeValueForKey:fromModel:")]
		void MergeValueForKey (string key, PSPDFModel model);

		[Export ("mergeValuesForKeysFromModel:")]
		void MergeValuesForKeysFromModel (PSPDFModel model);

		[Export ("validate:")]
		bool Validate (out NSError error);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFCacheInfo {

		[Export ("initWithUID:page:size:receipt:")]
		IntPtr Constructor (string uid, nuint page, CGSize size, string renderReceipt);

		[Export ("UID")]
		string Uid { get; }

		[Export ("page", ArgumentSemantic.Assign)]
		nuint Page { get; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; }

		[Export ("renderFingerprint")]
		string RenderFingerprint { get; set; }

		[Export ("lastAccessTime", ArgumentSemantic.Assign)]
		double LastAccessTime { get; set; }

		[Export ("diskSize", ArgumentSemantic.Assign)]
		ulong DiskSize { get; set; }

		[Export ("image", ArgumentSemantic.Strong), NullAllowed]
		UIImage Image { get; set; }

		[Export ("canBeScaledDownToSize:")]
		bool CanBeScaledDown (NSObject size);
	}

	[BaseType (typeof (UIAlertView))]
	interface PSPDFAlertView {

		[Export ("initWithTitle:")]
		IntPtr Constructor (string title);

		[Export ("initWithTitle:message:")]
		IntPtr Constructor (string title, string message);

		[Export ("setCancelButtonWithTitle:block:")]
		nint SetCancelButton (string title, [NullAllowed] Action<nint> handler);

		[Export ("addButtonWithTitle:block:")]
		nint AddButton (string title, [NullAllowed] Action<nint> handler);

		[Export ("addWillDismissBlock:")]
		void AddWillDismissHandler ([NullAllowed]Action<nint> willDismissHandler);

		[Export ("addDidDismissBlock:")]
		void AddDidDismissHandler ([NullAllowed]Action<nint> didDismissHandler);

		[Static, Export ("areAnyPSPDFAlertsVisible")]
		bool AreAnyPsPdfAlertsVisible ();
	}

	[BaseType (typeof (UIActionSheet))]
	interface PSPDFActionSheet {

		[Export ("initWithTitle:")]
		IntPtr Constructor (string title);

		[Export ("allowsTapToDismiss", ArgumentSemantic.Assign)]
		bool AllowsTapToDismiss { get; set; }

		[Export ("setCancelButtonWithTitle:block:")]
		void SetCancelButton (string title, [NullAllowed] Action<int> handler);

		[Export ("setDestructiveButtonWithTitle:block:")]
		void SetDestructiveButton (string title, [NullAllowed] Action<int> handler);

		[Export ("addButtonWithTitle:block:")]
		void AddButton (string title, [NullAllowed] Action<int> handler);

		[Export ("buttonCount")][New]
		nuint ButtonCount { get; }

		[Export ("showWithSender:fallbackView:animated:")]
		void Show (NSObject sender, UIView view, bool animated);

		[Export ("addCancelBlock:")]
		void AddCancelHandler ([NullAllowed] Action<int> cancelHandler);

		[Export ("addWillDismissBlock:")]
		void AddWillDismissHandler ([NullAllowed] Action<int> willDismissHandler);

		[Export ("addDidDismissBlock:")]
		void AddDidDismissHandler ([NullAllowed] Action<int> didDismissHandler);
	}

	[BaseType (typeof (UIMenuItem))]
	interface PSPDFMenuItem {

		[Export ("initWithTitle:block:")]
		IntPtr Constructor (string title, [NullAllowed] Action handler);

		[Export ("initWithTitle:block:identifier:")]
		IntPtr Constructor (string title, [NullAllowed] Action handler, string identifier);

		[Export ("initWithTitle:image:block:identifier:")]
		IntPtr Constructor (string title, UIImage image, [NullAllowed] Action handler, string identifier);

		[Export ("initWithTitle:image:block:identifier:allowImageColors:")]
		IntPtr Constructor (string title, UIImage image, Action handler, string identifier, bool allowImageColors);

		[Export ("enabled", ArgumentSemantic.Assign)]
		bool Enabled { [Bind ("isEnabled")] get; set; }

		[Export ("shouldInvokeAutomatically", ArgumentSemantic.Assign)]
		bool ShouldInvokeAutomatically { get; set; }

		[Export ("identifier")]
		string Identifier { get; set; }

		[Export ("title")][New]
		string Title { get; set; }

		[Export ("ps_image", ArgumentSemantic.Strong), NullAllowed]
		UIImage PsImage { get; set; }

		[Export ("setActionBlock:")]
		void SetAction (Action handler);

		[Static, Export ("installMenuHandlerForObject:")]
		void InstallMenuHandler (NSObject obj);

		// PSPDFMenuItem (Analytics) Category

		[Export ("performBlock")]
		void PerformHandler ();
	}

	[Static]
	interface PSPDFProcessorOptionKeys
	{
		[Field ("PSPDFProcessorAnnotationTypes", "__Internal")]
		NSString AnnotationTypes { get; }

		[Field ("PSPDFProcessorAnnotationDict", "__Internal")]
		NSString AnnotationDict { get; }

		[Field ("PSPDFProcessorAnnotationAsDictionary", "__Internal")]
		NSString AnnotationAsDictionary { get; }

		[Field ("PSPDFProcessorPageRect", "__Internal")]
		NSString PageRect { get; }

		[Field ("PSPDFProcessorNumberOfPages", "__Internal")]
		NSString NumberOfPages { get; }

		[Field ("PSPDFProcessorPageBorderMargin", "__Internal")]
		NSString PageBorderMargin { get; }

		[Field ("PSPDFProcessorAdditionalDelay", "__Internal")]
		NSString AdditionalDelay { get; }

		[Field ("PSPDFProcessorStripEmptyPages", "__Internal")]
		NSString StripEmptyPages { get; }

		[Field ("PSPDFProcessorSkipPDFCreation", "__Internal")]
		NSString SkipPdfCreation { get; }

		[Field ("PSPDFProcessorDrawRectBlock", "__Internal")]
		NSString DrawRectBlock { get; }

		[Field ("PSPDFProcessorDocumentTitle", "__Internal")]
		NSString DocumentTitle { get; }
	}

	delegate void PSPDFProcessorProgressHandler (nuint currentPage, nuint numberOfProcessedPages, nuint totalPages);
	delegate void PSPDFProcessorPdfFromUrlHandler (NSUrl fileUrl, NSError error);
	delegate void PSPDFProcessorPdfFromUrlNsdataHandler (NSData fileData, NSError error);

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFProcessor {

		[Static, Export ("defaultProcessor")]
		PSPDFProcessor DefaultProcessor { get; }

		[Export ("generatePDFFromDocument:pageRanges:outputFileURL:options:progressBlock:error:")]
		bool GeneratePdf (PSPDFDocument document, NSIndexSet [] pageRange, NSUrl fileURL, [NullAllowed] NSDictionary options, PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Export ("generatePDFFromDocument:pageRanges:options:progressBlock:error:")]
		NSData GeneratePdf (PSPDFDocument document, NSIndexSet [] pageRange, [NullAllowed] NSDictionary options, PSPDFProcessorProgressHandler progressHandler, out NSError error);

		[Export ("generatePDFFromHTMLString:outputFileURL:options:error:")]
		bool GeneratePdf (string html, NSUrl filerUrl, [NullAllowed] NSDictionary options, out NSError error);

		[Export ("generatePDFFromHTMLString:options:error:")]
		NSData GeneratePdf (string html, [NullAllowed] NSDictionary options, out NSError error);

		[Export ("generatePDFFromURL:outputFileURL:options:completionBlock:")]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl outputUrl, [NullAllowed] NSDictionary options, PSPDFProcessorPdfFromUrlHandler completionHandler);

		[Export ("generatePDFFromURL:options:completionBlock:")]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl, [NullAllowed] NSDictionary options, [NullAllowed] PSPDFProcessorPdfFromUrlNsdataHandler completionHandler);

		[Static, Export ("conversionOperationQueue")]
		NSOperationQueue ConversionOperationQueue { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSOperation))]
	interface PSPDFConversionOperation {

		[Export ("initWithURL:outputFileURL:options:completionBlock:")]
		IntPtr Constructor (NSUrl inputUrl, NSUrl outputFileUrl, [NullAllowed] NSDictionary options, [NullAllowed] Action<NSUrl, NSError> completionHandler);

		[Export ("initWithURL:options:completionBlock:")]
		IntPtr Constructor (NSUrl inputUrl, [NullAllowed] NSDictionary options, [NullAllowed] Action<NSData, NSError> completionHandler);

		[Export ("inputURL", ArgumentSemantic.Copy)]
		NSUrl InputUrl { get; }

		[Export ("outputFileURL", ArgumentSemantic.Copy)]
		NSUrl OutputFileUrl { get; }

		[Export ("outputData", ArgumentSemantic.Strong), NullAllowed]
		NSData OutputData { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; }

		[Export ("error", ArgumentSemantic.Strong), NullAllowed]
		NSError Error { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUD {

		[Static, Export ("items")]
		PSPDFStatusHUDItem [] Items { get; }

		[Static, Export ("popAllItemsAnimated:")]
		void PopAllItems (bool animated);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStatusHUDItem {

		[Export ("title")]
		string Title { get; set; }

		[Export ("subtitle")]
		string Subtitle { get; set; }

		[Export ("text")]
		string Text { get; set; }

		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; set; }

		[Export ("view", ArgumentSemantic.Strong), NullAllowed]
		UIView View { get; set; }

		[Static, Export ("progressWithText:")]
		PSPDFStatusHUDItem GetProgressHud (string text);

		[Static, Export ("indeterminateProgressWithText:")]
		PSPDFStatusHUDItem GetIndeterminateProgressHud (string text);

		[Static, Export ("successWithText:")]
		PSPDFStatusHUDItem GetSuccessHud (string text);

		[Static, Export ("errorWithText:")]
		PSPDFStatusHUDItem GetErrorHud (string text);

		[Static, Export ("itemWithText:image:")]
		PSPDFStatusHUDItem GetItemHud (string text, UIImage image);

		[Export ("setHUDStyle:")]
		void SetHudStyle (PSPDFStatusHUDStyle style);

		[Export ("pushAnimated:")]
		void Push (bool animated);

		[Export ("pushAndPopWithDelay:animated:")]
		void PushAndPop (double interval, bool animated);

		[Export ("popAnimated:")]
		void Pop (bool animated);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFStatusHUDView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("item", ArgumentSemantic.Strong), NullAllowed]
		PSPDFStatusHUDItem Item { get; set; }
	}

	[BaseType (typeof (UIButton))]
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
		void Speak (string speechString, [NullAllowed]  NSDictionary options, [NullAllowed] IAVSpeechSynthesizerDelegate aDelegate);

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

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFParser {

		[Export ("initWithInputStream:documentProvider:")]
		IntPtr Constructor (NSInputStream inputStream, PSPDFDocumentProvider documentProvider);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("parsing", ArgumentSemantic.Assign)]
		bool Parsing { [Bind ("isParsing")] get; }

		[Export ("parsingEnded", ArgumentSemantic.Assign)]
		bool ParsingEnded { get; }

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("inputStream", ArgumentSemantic.Strong), NullAllowed]
		NSInputStream InputStream { get; }

		[Export ("parseWithError:")]
		PSPDFAnnotation [] Parse (out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFWriter {

		[Export ("writeAnnotations:toOutputStream:documentProvider:error:")]
		bool WriteAnnotations (PSPDFAnnotation [] annotations, NSOutputStream outputStream, PSPDFDocumentProvider documentProvider, out NSError error);
	}

	delegate NSInputStream PSPDFXFDFAnnotationProviderStreamHandler (PSPDFXFDFAnnotationProvider provider);

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFXFDFAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, NSUrl XfdfFileUrl);

		[Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }

		[Export ("inputStream", ArgumentSemantic.Strong), NullAllowed]
		NSInputStream InputStream { get; }

		[Export ("outputStream", ArgumentSemantic.Strong), NullAllowed]
		NSOutputStream OutputStream { get; }

		[Export ("loadAllAnnotations")]
		void LoadAllAnnotations ();

		[Export ("setCreateInputStreamBlock:")]
		void SetCreateInputStreamHandler (PSPDFXFDFAnnotationProvider handler);

		[Export ("setCreateOutputStreamBlock:")]
		void SetCreateOutputStreamHandler (PSPDFXFDFAnnotationProvider handler);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCryptor {

		[Export ("keyFromPassphrase:salt:")]
		NSData GetKey (string passphrase, string salt);

		[Export ("legacyKeyFromPassphrase:salt:")]
		NSData GetLegacyKey (string passphrase, string salt);

		[Export ("encryptFromURL:toURL:key:error:")]
		bool Encrypt (NSUrl sourceURL, NSUrl targetURL, NSData key, out NSError error);

		[Export ("decryptFromURL:toURL:key:error:")]
		bool Decrypt (NSUrl sourceURL, NSUrl targetURL, NSData key, out NSError error);

		[Export ("encryptFromURL:toURL:passphrase:error:")]
		bool Encrypt (NSUrl sourceURL, NSUrl targetURL, string passphrase, out NSError error);

		[Export ("decryptFromURL:toURL:passphrase:error:")]
		bool Decrypt (NSUrl sourceURL, NSUrl targetURL, string passphrase, out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAESCryptoDataProvider {

		[Export ("initWithURL:passphrase:salt:rounds:")]
		IntPtr Constructor (NSUrl url, string passphrase, string salt, uint rounds);

		[Export ("initWithURL:passphraseData:salt:rounds:")]
		IntPtr Constructor (NSUrl url, NSData passphraseData, NSData saltData, uint rounds);

		[Export ("initWithURL:passphrase:")]
		IntPtr Constructor (NSUrl url, string passphrase);

		//[Export ("initWithLegacyFileFormatURL:passphrase:")]
		//IntPtr Constructor (NSUrl url, string passphrase);

		[Export ("initWithURL:binaryKey:")]
		IntPtr Constructor (NSUrl url, NSData key);

		[Export ("dataProvider"), Internal]
		IntPtr _DataProvider { get; }

		[Static, Export ("isAESCryptoDataProvider:")]
		bool IsAESCryptoDataProvider (IntPtr dataProvider);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormParser {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[Export ("forms", ArgumentSemantic.Copy)]
		PSPDFFormElement [] Forms { get; set; }

		[Export ("findAnnotationWithFieldName:")]
		PSPDFFormElement FindAnnotation (string fieldName);

		[Export ("findAnnotationWithFullFieldName:descendingFromForm:")]
		PSPDFFormElement FindAnnotation (string fullFieldName, [NullAllowed] PSPDFFormElement parent);
	}

	[BaseType (typeof (PSPDFWidgetAnnotation))]
	interface PSPDFFormElement {

		[Export ("parent", ArgumentSemantic.Weak)]
		PSPDFFormElement Parent { get; }

		[Export ("kids", ArgumentSemantic.Copy)]
		NSObject [] Kids { get; }

		[Export ("fieldType")]
		string FieldType { get; set; }

		[Export ("fieldName")]
		string FieldName { get; set; }

		[Export ("mappingName")]
		string MappingName { get; set; }

		[Export ("alternateFieldName")]
		string AlternateFieldName { get; set; }

		[Export ("fieldFlags", ArgumentSemantic.Assign)]
		nuint FieldFlags { get; set; }

		[Export ("value", ArgumentSemantic.Strong), NullAllowed][New]
		NSObject Value { get; set; }

		[Export ("defaultValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject DefaultValue { get; set; }

		[Export ("appearanceState")]
		string AppearanceState { get; set; }

		[Export ("exportValue", ArgumentSemantic.Strong), NullAllowed]
		NSObject ExportValue { get; }

		[Export ("highlightColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor HighlightColor { get; set; }

		[Export ("next", ArgumentSemantic.Weak)]
		PSPDFFormElement Next { get; set; }

		[Export ("previous", ArgumentSemantic.Weak)]
		PSPDFFormElement Previous { get; set; }

		[Export ("tabbingPage", ArgumentSemantic.Assign)]
		nuint TabbingPage { get; set; }

		[Export ("tabbingStructureIndex", ArgumentSemantic.Assign)]
		nuint TabbingStructureIndex { get; set; }

		[Export ("tabbingManualIndex", ArgumentSemantic.Assign)]
		nuint TabbingManualIndex { get; set; }

		[Export ("structParent", ArgumentSemantic.Assign)]
		nuint StructParent { get; set; }

		[Export ("tabOrder")]
		string TabOrder { get; set; }

		[Export ("formIndex", ArgumentSemantic.Assign)]
		nint FormIndex { get; set; }

		[Export ("calculationOrderIndex", ArgumentSemantic.Assign)]
		nuint CalculationOrderIndex { get; set; }

		[Export ("isResetable")]
		bool IsResetable { get; }

		[Export ("isReadOnly")]
		bool IsReadOnly { get; }

		[Export ("isRequired")]
		bool IsRequired { get; }

		[Export ("isNoExport")]
		bool IsNoExport { get; }

		[Export ("fullyQualifiedFieldName")]
		string FullyQualifiedFieldName ();

		[Export ("formTypeName")]
		string FormTypeName ();

		// PSPDFFormElement (Fonts) Category

		[Export ("maxLength", ArgumentSemantic.Assign)]
		nuint MaxLength { get; set; }

		[Export ("isMultiline", ArgumentSemantic.Assign)]
		bool IsMultiline { get; set; }

		// PSPDFFormElement (Drawing) Category

		[Export ("drawHighlightInContext:multiply:")]
		void DrawHighlightInContext (CGContext context, bool shouldMultiply);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormRequest {

		[Export ("initWithFormat:values:request:")]
		IntPtr Constructor (NSObject format, NSDictionary values, NSUrlRequest request);

		[Export ("submissionFormat")]
		PSPDFSubmitFormActionFormat SubmissionFormat { get; }

		[Export ("formValues")]
		NSDictionary FormValues { get; }

		[Export ("request")]
		NSUrlRequest Request { get; }
	}

	interface IPSPDFFormSubmissionDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormSubmissionDelegate {

		[Export ("pdfViewController:shouldSubmitFormRequest:")]
		bool ShouldSubmitFormRequest (PSPDFViewController viewController, PSPDFFormRequest formRequest);

		[Export ("pdfViewController:willSubmitFormValues:")]
		void WillSubmitFormValues (PSPDFViewController viewController, PSPDFFormRequest formRequest);

		[Export ("pdfViewController:didReceiveResponseData:")]
		void DidReceiveResponseData (PSPDFViewController viewController, NSData responseData);

		[Export ("pdfViewController:didFailWithError:")]
		void DidFail (PSPDFViewController viewController, NSError error);

		[Export ("pdfViewControllerShouldPresentResponseInWebView:")]
		bool PdfViewControllerShouldPresentResponseInWebView (PSPDFViewController viewController);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFButtonFormElement {

		[Export ("isPushButton")]
		bool IsPushButton { get; }

		[Export ("isCheckBox")]
		bool IsCheckBox { get; }

		[Export ("isRadioButton")]
		bool IsRadioButton { get; }

		[Export ("isSelected")]
		bool IsSelected { get; }

		[Export ("select")]
		void Select ();

		[Export ("deselect")]
		void Deselect ();

		[Export ("toggleButtonSelectionStateAndSendNotification:")]
		bool ToggleButtonSelectionStateAndSendNotification (bool sendNotification);

		[Export ("opt", ArgumentSemantic.Copy)]
		NSObject [] Opt { get; set; }

		[Export ("onState")]
		string OnState { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFChoiceFormElement {

		[Export ("isCombo")]
		bool IsCombo { get; }

		[Export ("isEdit")]
		bool IsEdit { get; }

		[Export ("isMultiSelect")]
		bool IsMultiSelect { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSObject [] Options { get; set; }

		[Export ("selectedIndices", ArgumentSemantic.Copy)]
		NSIndexSet SelectedIndices { get; set; }

		[Export ("customSelection", ArgumentSemantic.Assign)]
		bool CustomSelection { get; set; }

		[Export ("topIndex", ArgumentSemantic.Assign)]
		nuint TopIndex { get; set; }

		[Export ("customText")]
		string CustomText { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFSignatureFormElement {

		[Export ("viewController", ArgumentSemantic.Weak)]
		UIViewController ViewController { get; }

		[Export ("signedViewController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSignedFormElementViewController SignedViewController { get; }

		[Export ("unsignedViewController", ArgumentSemantic.Strong), NullAllowed]
		PSPDFUnsignedFormElementViewController UnsignedViewController { get; }

		[Export ("isSigned")]
		bool IsSigned { get; }

		[Export ("signatureInfo", ArgumentSemantic.Strong), NullAllowed]
		PSPDFSignatureInfo SignatureInfo { get; set; }

		[Export ("overlappingInkSignature")]
		PSPDFInkAnnotation OverlappingInkSignature ();

		// PSPDFSignatureFormElement (SubclassingHooks) Category

		[Export ("drawArrowWithText:andColor:inContext:")]
		void DrawArrowWithText (string text, UIColor color, CGContext context);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignaturePropBuildEntry {

		[Export ("name")]
		string Name { get; }

		[Export ("date")]
		string Date { get; }

		[Export ("R", ArgumentSemantic.Assign)]
		nint R { get; }

		[Export ("OS")]
		string OS { get; }

		[Export ("preRelease", ArgumentSemantic.Strong), NullAllowed]
		NSNumber PreRelease { get; }

		[Export ("nonEFontNoWarn", ArgumentSemantic.Strong), NullAllowed]
		NSNumber NonEFontNoWarn { get; }

		[Export ("trustedMode", ArgumentSemantic.Strong), NullAllowed]
		NSNumber TrustedMode { get; }

		[Export ("V", ArgumentSemantic.Assign)]
		nint V { get; }

		[Export ("REx")]
		string REx { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignaturePropBuild {

		[Export ("filter", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry Filter { get; }

		[Export ("pubSec", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry PubSec { get; }

		[Export ("app", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry App { get; }

		[Export ("sigQ", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry SigQ { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSignatureInfo {

		[Export ("placeholderBytes", ArgumentSemantic.Assign)]
		nuint PlaceholderBytes { get; }

		[Export ("contents", ArgumentSemantic.Copy)]
		NSData Contents { get; }

		[Export ("byteRange", ArgumentSemantic.Copy)]
		NSObject [] ByteRange { get; }

		[Export ("filter")]
		string Filter { get; }

		[Export ("subFilter")]
		string SubFilter { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("creationDate", ArgumentSemantic.Copy)]
		NSDate CreationDate { get; }

		[Export ("reason")]
		string Reason { get; }

		[Export ("propBuild", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuild PropBuild { get; }

		[Export ("references", ArgumentSemantic.Copy)]
		NSObject [] References { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFTextFieldFormElement {

		[Export ("inputFormat", ArgumentSemantic.Assign)]
		PSPDFTextInputFormat InputFormat { get; }

		[Export ("isMultiline")][New]
		bool IsMultiline { get; }

		[Export ("isPassword")]
		bool IsPassword { get; }

		[Export ("textFieldChangedWithContents:change:range:isFinal:error:")]
		string TextFieldChanged (string contents, string change, NSRange range, bool isFinal, out NSError validationError);

		[Export ("formattedContents")]
		string FormattedContents ();
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFButtonFormElementView {

	}

	interface IPSPDFChoiceEditorViewControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFChoiceEditorViewControllerDelegate {

		[Export ("choiceEditorController:didSelectIndex:shouldDismissController:")]
		[Abstract]
		void DidSelectIndex (PSPDFChoiceEditorViewController choiceEditorController, nuint index, bool shouldDismissController);

		[Export ("choiceEditorController:didUpdateCustomText:")]
		[Abstract]
		void DidUpdateCustomText (PSPDFChoiceEditorViewController choiceEditorController, string text);

		[Export ("choiceEditorControllerDidSelectCustomText:")]
		[Abstract]
		void ChoiceEditorControllerDidSelectCustomText (PSPDFChoiceEditorViewController choiceEditorController);

		[Export ("choiceEditorControllerDidEndEditingCustomText:")]
		[Abstract]
		void ChoiceEditorControllerDidEndEditingCustomText (PSPDFChoiceEditorViewController choiceEditorController);
	}

	interface IPSPDFChoiceEditorViewControllerDataSource { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFChoiceEditorViewControllerDataSource {

		[Export ("numberOfOptions")]
		[Abstract]
		nuint NumberOfOptions ();

		[Export ("numberOfOptionsSelected")]
		[Abstract]
		nuint NumberOfOptionsSelected ();

		[Export ("isMultiSelect")]
		[Abstract]
		bool IsMultiSelect ();

		[Export ("optionTextAtIndex:")]
		[Abstract]
		string OptionTextAtIndex (nuint index);

		[Export ("isSelectedAtIndex:")]
		[Abstract]
		bool IsSelectedAtIndex (nuint index);

		[Export ("selectedIndices")]
		[Abstract]
		NSIndexSet SelectedIndices ();

		[Export ("isEdit")]
		[Abstract]
		bool IsEdit ();

		[Export ("customText")]
		[Abstract]
		string CustomText ();
	}


	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFChoiceEditorViewController {

		[Export ("initWithDataSource:delegate:")]
		IntPtr Constructor ([NullAllowed] IPSPDFChoiceEditorViewControllerDataSource datasource, PSPDFChoiceEditorViewControllerDelegate aDelegate);

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFChoiceEditorViewControllerDataSource DataSource { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFChoiceEditorViewControllerDelegate Delegate { get; set; }

		[Export ("formElementView", ArgumentSemantic.Weak)]
		PSPDFChoiceFormElementView FormElementView { get; set; }

		[Export ("reloadData")]
		void ReloadData ();

		[Export ("selectNextValueAnimated:")]
		void SelectNextValue (bool animated);

		[Export ("selectPreviousValueAnimated:")]
		void SelectPreviousValue (bool animated);

		[Export ("enableEditModeAnimated:")]
		bool EnableEditMode (bool animated);
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFChoiceFormElementView {

		[Export ("prepareChoiceEditorController")]
		PSPDFChoiceEditorViewController PrepareChoiceEditorController ();
	}

	[BaseType (typeof (PSPDFHostingAnnotationView))]
	interface PSPDFFormElementView : IPSPDFFormInputAccessoryViewDelegate {

	}

	[BaseType (typeof (UIView))]
	interface PSPDFFormInputAccessoryView {

		[Export ("initWithFrame:delegate:")]
		IntPtr Constructor (CGRect frame, [NullAllowed] IPSPDFFormInputAccessoryViewDelegate aDelegate);

		[Export ("displayDoneButton")]
		bool DisplayDoneButton { get; set; }

		[Export ("displayClearButton")]
		bool DisplayClearButton { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFFormInputAccessoryViewDelegate Delegate { get; set; }

		[Export ("updateToolbar")]
		void UpdateToolbar ();
	}

	interface IPSPDFFormInputAccessoryViewDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFFormInputAccessoryViewDelegate {

		[Export ("doneButtonPressedOnFormInputView:")]
		[Abstract]
		void DoneButtonPressedOnFormInputView (PSPDFFormInputAccessoryView inputView);

		[Export ("previousButtonPressedOnFormInputView:")]
		[Abstract]
		void PreviousButtonPressedOnFormInputView (PSPDFFormInputAccessoryView inputView);

		[Export ("nextButtonPressedOnFormInputView:")]
		[Abstract]
		void NextButtonPressedOnFormInputView (PSPDFFormInputAccessoryView inputView);

		[Export ("clearButtonPressedOnFormInputView:")]
		[Abstract]
		void ClearButtonPressedOnFormInputView (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnablePreviousButton:")]
		[Abstract]
		bool FormInputViewShouldEnablePreviousButton (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnableNextButton:")]
		[Abstract]
		bool FormInputViewShouldEnableNextButton (PSPDFFormInputAccessoryView inputView);

		[Export ("formInputViewShouldEnableClearButton:")]
		[Abstract]
		bool FormInputViewShouldEnableClearButton (PSPDFFormInputAccessoryView inputView);
	}


	[BaseType (typeof (UITableViewCell))]
	interface PSPDFChoiceEditorCell {

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		PSPDFChoiceEditorViewControllerDelegate Delegate { get; set; }

		[Export ("textField", ArgumentSemantic.Strong), NullAllowed]
		UITextField TextField { get; set; }

		[Export ("editButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton EditButton { get; set; }

		[Export ("parent", ArgumentSemantic.Weak)]
		PSPDFChoiceEditorViewController Parent { get; set; }

		[Export ("chooseEditable")]
		void ChooseEditable ();

		[Export ("enterEditMode")]
		void EnterEditMode ();
	}

	interface IPSPDFChoiceFormElementControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFChoiceFormElementControllerDelegate {

		[Export ("choiceFormElementController:dismissPopoverAnimated:")]
		[Abstract]
		void DismissPopoverAnimated (PSPDFChoiceFormElementController choiceFormElementController, bool animated);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFChoiceFormElementController {

		[Export ("initWithChoiceFormElement:delegate:")]
		IntPtr Constructor (PSPDFChoiceFormElement choiceFormElement, [NullAllowed] IPSPDFChoiceFormElementControllerDelegate aDelegate);

		[Export ("choiceFormElement", ArgumentSemantic.Strong), NullAllowed]
		PSPDFChoiceFormElement ChoiceFormElement { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFChoiceFormElementControllerDelegate Delegate { get; }
	}

	[BaseType (typeof (PSPDFFormElementView))]
	interface PSPDFTextFieldFormElementView : IPSPDFResizableTrackedViewDelegate {

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

		[Export ("resizableView", ArgumentSemantic.Weak)]
		PSPDFResizableView ResizableView { get; set; }

		[Export ("beginEditing")]
		void BeginEditing ();

		[Export ("endEditing")]
		void EndEditing ();

		// PSPDFTextFieldFormElementView (SubclassingHooks) Category

		[Export ("setTextViewForEditing")]
		void SetTextViewForEditing ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFPKCS12 {

		[Export ("initWithData:")]
		IntPtr Constructor (NSData data);

		[Export ("unlockWithPassword:done:")]
		void Unlock (string password, Action<PSPDFX509, PSPDFRSAKey, NSError> done);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFSigner))]
	interface PSPDFPKCS12Signer {

		[Export ("initWithDisplayName:PKCS12:")]
		IntPtr Constructor (string name, PSPDFPKCS12 p12);

		[Export ("signFormElement:usingPassword:writeTo:completion:")]
		void SignFormElement (string password, PSPDFSignatureFormElement element, string path, Action<bool, PSPDFDocument, NSError> completion);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFRSAKey {

		[Export ("initWithKey:")]
		IntPtr Constructor (NSObject key);

		[Export ("key")]
		NSObject Key { get; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureDigest {

		[Export ("initWithBIO:")]
		IntPtr Constructor (NSObject bio);

		[Export ("bio")]
		NSObject Bio { get; }

		[Export ("digestRange:fileHandle:")]
		void DigestRange (NSRange range, NSFileHandle fh);

		[Export ("digestData:")]
		void DigestData (NSData data);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureManager {

		[Static, Export ("sharedManager")]
		PSPDFSignatureManager SharedManager { get; }

		[Export ("registeredSigners")]
		PSPDFSigner [] RegisteredSigners ();

		[Export ("registerSigner:")]
		void RegisterSigner (PSPDFSigner signer);

		[Export ("trustedCertificates")]
		NSObject [] TrustedCertificates ();

		[Export ("addTrustedCertificate:")]
		void AddTrustedCertificate (PSPDFX509 x509);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFSignedFormElementViewController {

		[Export ("initWithSignatureFormElement:")]
		IntPtr Constructor (PSPDFSignatureFormElement element);

		[Export ("formElement", ArgumentSemantic.Weak)]
		PSPDFSignatureFormElement FormElement { get; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFSignedFormElementViewControllerDelegate Delegate { get; set; }

		[Export ("verifySignature:")]
		PSPDFSignatureStatus VerifySignature (out NSError error);
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

		[Export ("requestSigningCertificate:done:")]
		void RequestSigningCertificate (UINavigationController controller, [NullAllowed] Action<PSPDFX509, NSError> done);

		[Advice ("Requires call to base if overridden")]
		[Export ("signFormElement:withCertificate:writeTo:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 x509, string path, Action<bool, PSPDFDocument, NSError> done);

		[Export ("signHash:algorithm:error:"), Internal]
		NSData SignHash (NSData hash, PSPDFSigningAlgorithm algorithm, IntPtr error);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFBaseTableViewController))]
	interface PSPDFUnsignedFormElementViewController {

		[Export ("initWithSignatureFormElement:")]
		IntPtr Constructor (PSPDFSignatureFormElement element);

		[Export ("formElement", ArgumentSemantic.Weak)]
		PSPDFSignatureFormElement FormElement { get; }

		[Export ("allowInkSignature", ArgumentSemantic.Assign)]
		bool AllowInkSignature { get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFUnsignedFormElementViewControllerDelegate Delegate { get; set; }
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

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFX509 {

		[Export ("initWithX509:")]
		IntPtr Constructor (NSObject x509);

		[Export ("cert")]
		NSObject Cert { get; }

		[Export ("publicKey")]
		PSPDFRSAKey PublicKey { get; }

		[Export ("commonName")]
		string CommonName { get; }

		[Static, Export ("adobeCA")]
		PSPDFX509 AdobeCA { get; }

		[Static, Export ("certificatesFromPKCS7Data:error:")]
		NSObject [] CertificatesFromPKCS7Data (NSData data, out NSError error);
	}

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

		[Static, Export ("initWithDictionary:"), Internal]
		PSPDFDigitalSignatureReference FromDictionary (IntPtr dict);

		[Export ("transformMethod", ArgumentSemantic.Assign)]
		PSPDFDigitalSignatureReferenceTransformMethod TransformMethod { get; set; }

		[Export ("transformParams", ArgumentSemantic.Copy)]
		NSDictionary TransformParams { get; set; }

		[Export ("data", ArgumentSemantic.Assign)]
		IntPtr Data { get; set; }

		[Export ("digestMethod")]
		string DigestMethod { get; set; }

		[Export ("digestValue")]
		string DigestValue { get; set; }

		[Export ("digestLocation", ArgumentSemantic.Assign)]
		NSRange DigestLocation { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryConfiguration {

		[Static, Export ("defaultConfiguration")]
		PSPDFGalleryConfiguration DefaultConfiguration { get; }

		[Static, Export ("configurationWithBuilder:")]
		PSPDFGalleryConfiguration FromBuilder (Action<PSPDFGalleryConfigurationBuilder> builderHandler);

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
	}

	[BaseType (typeof (NSObject))]
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
	}

	interface IPSPDFGalleryContentViewLoading {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryContentViewLoading {

		[Export ("setProgress:")]
		[Abstract]
		void SetProgress (nfloat progress);

		[Export ("progress")]
		[Abstract]
		nfloat Progress ();
	}

	interface IPSPDFGalleryContentViewError {}

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryContentViewError {

		[Export ("setError:")]
		[Abstract]
		void SetError (NSError error);

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
		void SetCaption (string caption);

		[Export ("caption")]
		[Abstract]
		string Caption ();
	}


	[BaseType (typeof (UIScrollView))]
	interface PSPDFGalleryView {

		[Export ("dataSource", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFGalleryViewDataSource DataSource { get; set; }

		[Export ("currentItemIndex", ArgumentSemantic.Assign)]
		nuint CurrentItemIndex { get; set; }

		[Export ("contentPadding", ArgumentSemantic.Assign)]
		nfloat ContentPadding { get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }

		[Export ("delegate", ArgumentSemantic.Weak), NullAllowed, New]
		IPSPDFGalleryViewDelegate Delegate { get; set; }

		[Export ("reload")]
		void Reload ();

		[Export ("contentViewForItemAtIndex:")]
		PSPDFGalleryContentView ContentViewForItem (nuint index);

		[Export ("contentViewsForItemAtIndex:")]
		NSSet ContentViewsForItem (nuint index);

		[Export ("itemIndexForContentView:")]
		nuint ItemIndexForContentView (PSPDFGalleryContentView contentView);

		[Export ("dequeueReusableContentViewWithIdentifier:")]
		PSPDFGalleryContentView DequeueReusableContentView (string identifier);

		[Export ("setCurrentItemIndex:animated:")]
		void SetCurrentItemIndex (nuint currentItemIndex, bool animated);

		[Export ("activeContentViews")]
		NSSet ActiveContentViews ();
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
	interface PSPDFGalleryViewController : IPSPDFOverridable, IPSPDFMultimediaViewController {

		[Export ("initWithPluginRegistry:options:")]
		IntPtr Constructor ([NullAllowed] NSObject pluginRegistry, [NullAllowed] NSDictionary options);

		[Export ("initWithLinkAnnotation:")]
		IntPtr Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("configuration", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryConfiguration Configuration { get; set; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFGalleryViewControllerState State { get; }

		[Export ("items", ArgumentSemantic.Copy), NullAllowed]
		PSPDFGalleryItem [] Items { get; }

		[Export ("linkAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("fullscreen", ArgumentSemantic.Assign)]
		bool Fullscreen { [Bind ("isFullscreen")] get; set; }

		[Export ("transitioning", ArgumentSemantic.Assign)]
		bool Transitioning { [Bind ("isTransitioning")] get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }

		[Export ("singleTapGestureRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer SingleTapGestureRecognizer { get; }

		[Export ("doubleTapGestureRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UITapGestureRecognizer DoubleTapGestureRecognizer { get; }

		[Export ("panGestureRecognizer", ArgumentSemantic.Strong), NullAllowed]
		UIPanGestureRecognizer PanGestureRecognizer { get; }

		[Export ("setFullscreen:animated:")]
		void SetFullscreen (bool fullscreen, bool animated);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor (string reuseIdentifier);

		[Export ("contentView", ArgumentSemantic.Strong), NullAllowed]
		UIView ContentView { get; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewLoading LoadingView { get; }

		[Export ("captionView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewCaption CaptionView { get; }

		[Export ("errorView", ArgumentSemantic.Strong), NullAllowed]
		IPSPDFGalleryContentViewError ErrorView { get; }

		[Export ("reuseIdentifier", ArgumentSemantic.Strong), NullAllowed]
		string ReuseIdentifier { get; }

		[Export ("content", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryItem Content { get; set; }

		[Export ("shouldHideCaption", ArgumentSemantic.Assign)]
		bool ShouldHideCaption { get; set; }

		// PSPDFGalleryContentView (SubclassingHooks) Category

		[Static, Export ("contentViewClass")]
		Class ContentViewClass { get; }

		[Static, Export ("loadingViewClass")]
		Class LoadingViewClass { get; }

		[Static, Export ("captionViewClass")]
		Class CaptionViewClass { get; }

		[Static, Export ("errorViewClass")]
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
	interface PSPDFGalleryContentCaptionView : IPSPDFGalleryContentViewCaption {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("initWithReuseIdentifier:")]
		IntPtr Constructor (string reuseIdentifier);

		[Export ("caption")]
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

		[Export ("pageView", ArgumentSemantic.Weak)]
		PSPDFPageView PageView { get; set; }
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

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("overrideDelegate", ArgumentSemantic.Weak), NullAllowed]
		IPSPDFOverridable OverrideDelegate { get; }

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewContentState ContentState { get; set; }

		[Export ("presentationMode", ArgumentSemantic.Assign)]
		PSPDFGalleryContainerViewPresentationMode PresentationMode { get; set; }

		[Export ("galleryView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryView GalleryView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFGalleryLoadingView LoadingView { get; set; }

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

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryContentLoadingView : IPSPDFGalleryContentViewLoading {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; set; }

		[Export ("color", ArgumentSemantic.Strong), NullAllowed]
		UIColor Color { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFGalleryLoadingView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("activityIndicatorView", ArgumentSemantic.Strong), NullAllowed]
		UIActivityIndicatorView ActivityIndicatorView { get; set; }
	}

	[DisableDefaultCtor]
	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryManifest {

		[Export ("initWithLinkAnnotation:")]
		IntPtr Constructor (PSPDFLinkAnnotation linkAnnotation);

		[Export ("linkAnnotation", ArgumentSemantic.Strong), NullAllowed]
		PSPDFLinkAnnotation LinkAnnotation { get; }

		[Export ("loading", ArgumentSemantic.Assign)]
		bool Loading { [Bind ("isLoading")] get; }

		[Export ("loadItemsWithCompletionBlock:")]
		void LoadItems (Action<PSPDFGalleryItem [], NSError> completionHandler);

		[Export ("cancel")]
		void Cancel ();
	}

	[BaseType (typeof (PSPDFGalleryItem))]
	interface PSPDFGalleryVideoItem {

		[Export ("autoplayEnabled", ArgumentSemantic.Assign)]
		bool AutoplayEnabled { get; set; }

		[Export ("controlsEnabled", ArgumentSemantic.Assign)]
		bool ControlsEnabled { get; set; }

		[Export ("loopEnabled", ArgumentSemantic.Assign)]
		bool LoopEnabled { get; set; }

		[Export ("preferredVideoQualities", ArgumentSemantic.Copy)]
		string [] PreferredVideoQualities { get; set; }

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

		[Export ("playableRange")]
		CMTimeRange PlayableRange ();

		[Export ("content")][New]
		NSUrl Content ();
	}

	[Static]
	interface PSPDFGalleryOption
	{
		[Field ("PSPDFGalleryOptionAutoplay", "__Internal")]
		NSString Autoplay { get; }

		[Field ("PSPDFGalleryOptionControls", "__Internal")]
		NSString Controls { get; }

		[Field ("PSPDFGalleryOptionLoop", "__Internal")]
		NSString Loop { get; }

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

		[Export ("content")][New]
		UIImage Content ();
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFGalleryItem {

		[Field ("PSPDFGalleryItemContentStateDidChangeNotification", "__Internal")]
		[Notification]
		NSString ContentStateDidChangeNotification { get; }

		[Export ("caption")]
		string Caption { get; }

		[Export ("contentURL", ArgumentSemantic.Copy)]
		NSUrl ContentUrl { get; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary Options { get; }

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

		[Static, Export ("itemsFromJSONData:error:")]
		PSPDFGalleryItem [] FromJsonData (NSData data, out NSError error);

		[Static, Export ("itemFromLinkAnnotation:error:")]
		PSPDFGalleryItem FromLinkAnnotation (PSPDFLinkAnnotation annotation, out NSError error);

		[Export ("initWithDictionary:")]
		IntPtr Constructor ([NullAllowed] NSDictionary dictionary);

		[Export ("initWithContentURL:caption:options:")]
		IntPtr Constructor (NSUrl contentUrl, string caption, [NullAllowed] NSDictionary options);
	}

	[Static]
	interface PSPDFGalleryItemType
	{
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

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerView {

		[Export ("initWithFrame:")]
		IntPtr Constructor (CGRect frame);

		[Export ("videoView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerVideoView VideoView { get; set; }

		[Export ("coverView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerCoverView CoverView { get; set; }

		[Export ("toolbar", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerToolbar Toolbar { get; set; }

		[Export ("placeholderView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerPlaceholderView PlaceholderView { get; set; }

		[Export ("loadingView", ArgumentSemantic.Strong), NullAllowed]
		UIActivityIndicatorView LoadingView { get; set; }

		[Export ("errorView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFErrorView ErrorView { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }
	}

	delegate void PSPDFErrorViewFormatterHandler (NSError error, string outTitle, string outSubtitle);

	[BaseType (typeof (UIView))]
	interface PSPDFErrorView
	{
		[Export ("error", ArgumentSemantic.Strong), NullAllowed]
		NSError Error { get; set; }

		[Export ("setFormatterBlock:")]
		void SetFormatterHandler (PSPDFErrorViewFormatterHandler handler);

		[Export ("titleLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel TitleLabel { get; }

		[Export ("subtitleLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel SubtitleLabel { get; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerPlaceholderView
	{
		[Export ("placeholderImageTemplate", ArgumentSemantic.Strong), NullAllowed]
		UIImage PlaceholderImageTemplate { get; set; }

		[Export ("gradientStartColor", ArgumentSemantic.Copy)]
		UIColor GradientStartColor { get; set; }

		[Export ("gradientEndColor", ArgumentSemantic.Copy)]
		UIColor GradientEndColor { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerScrubberView
	{
		[Export ("currentTime", ArgumentSemantic.Assign)]
		CMTime CurrentTime { get; set; }

		[Export ("duration", ArgumentSemantic.Assign)]
		CMTime Duration { get; set; }

		[Export ("currentTimeLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel CurrentTimeLabel { get; }

		[Export ("remainingTimeLabel", ArgumentSemantic.Strong), NullAllowed]
		UILabel RemainingTimeLabel { get; }

		[Export ("slider", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerSlider Slider { get; }
	}

	[BaseType (typeof (UISlider))]
	interface PSPDFMediaPlayerSlider
	{
		[Export ("currentTime", ArgumentSemantic.Assign)]
		double CurrentTime { get; set; }

		[Export ("duration", ArgumentSemantic.Assign)]
		double Duration { get; set; }

		[Export ("availableDuration", ArgumentSemantic.Assign)]
		double AvailableDuration { get; set; }

		[Export ("availableDurationTrackImage", ArgumentSemantic.Strong), NullAllowed]
		UILabel AvailableDurationTrackImage { get; set; }
	}

	[BaseType (typeof (PSPDFTranslucentToolbar))]
	interface PSPDFMediaPlayerToolbar
	{
		[Export ("playing", ArgumentSemantic.Assign)]
		bool Playing { [Bind ("isPlaying")] get; set; }

		[Export ("playButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton PlayButton { get; set; }

		[Export ("pauseButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton PauseButton { get; set; }

		[Export ("scrubberView", ArgumentSemantic.Strong), NullAllowed]
		PSPDFMediaPlayerScrubberView ScrubberView { get; set; }

		[Export ("externalPlaybackControl", ArgumentSemantic.Strong), NullAllowed]
		MPVolumeView ExternalPlaybackControl { get; set; }

		[Export ("externalPlaybackControlHidden", ArgumentSemantic.Assign)]
		bool ExternalPlaybackControlHidden { [Bind ("isExternalPlaybackControlHidden")] get; set; }

		[Export ("setExternalPlaybackControlHidden:animated:")]
		void SetExternalPlaybackControlHidden (bool externalPlaybackControlHidden, bool animated);
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerVideoView
	{
		[Export ("playerLayer", ArgumentSemantic.Strong), NullAllowed]
		AVPlayerLayer PlayerLayer { get; }

		[Export ("overlayView", ArgumentSemantic.Strong), NullAllowed]
		UIView OverlayView { get; set; }
	}

	[BaseType (typeof (UIView))]
	interface PSPDFMediaPlayerCoverView
	{
		[Export ("coverImage", ArgumentSemantic.Strong), NullAllowed]
		PSPDFRemoteImageView CoverImage { get; set; }

		[Export ("playButton", ArgumentSemantic.Strong), NullAllowed]
		UIButton PlayButton { get; set; }

		[Export ("playButtonColor", ArgumentSemantic.Strong), NullAllowed]
		UIColor PlayButtonColor { get; set; }

		[Export ("zoomScale", ArgumentSemantic.Assign)]
		nfloat ZoomScale { get; set; }
	}

	[BaseType (typeof (UIImageView))]
	interface PSPDFRemoteImageView
	{
		[Export ("URL", ArgumentSemantic.Copy), NullAllowed]
		NSUrl Url { get; set; }

		[Export ("loading", ArgumentSemantic.Assign)]
		bool Loading { [Bind ("isLoading")] get; }

		[Export ("progress", ArgumentSemantic.Assign)]
		nfloat Progress { get; }

		[Export ("error", ArgumentSemantic.Strong), NullAllowed]
		NSError Error { get; }
	}

	interface IPSPDFMultimediaViewController { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMultimediaViewController {

		[Export ("isFullscreen")]
		bool GetFullscreen ();

		[Export ("isTransitioning")]
		bool GetTransitioning ();

		[Export ("zoomScale")]
		nfloat GetZoomScale ();

		[Export ("overrideDelegate")]
		IPSPDFOverridable GetOverrideDelegate ();

		[Export ("setOverrideDelegate:")]
		void SetOverrideDelegate ([NullAllowed] IPSPDFOverridable overrideDelegate);

		[Export ("setFullscreen:animated:")]
		[Abstract]
		void SetFullscreen (bool fullscreen, bool animated);

		[Export ("performAction:")]
		[Abstract]
		void PerformAction (PSPDFAction action);

		[Export ("configure:")]
		void Configure (PSPDFConfiguration configuration);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFStylusManager {

		[Field ("PSPDFStylusManagerConnectionStatusChangedNotification", "__Internal")]
		[Notification]
		NSString ConnectionStatusChangedNotification { get; }

		[Export ("currentDriverClass", ArgumentSemantic.Assign)]
		Class CurrentDriverClass { get; set; }

		[Export ("connectionStatus", ArgumentSemantic.Assign)]
		PSPDFStylusConnectionStatus ConnectionStatus { get; }

		[Export ("stylusName")]
		string StylusName { get; }

		[Export ("enableLastDriver")]
		bool EnableLastDriver ();

		[Export ("driversAreAvailable")]
		bool DriversAreAvailable ();

		[Export ("stylusController")]
		PSPDFStylusViewController StylusController ();

		[Export ("settingsControllerForCurrentDriver")]
		UIViewController SettingsControllerForCurrentDriver ();

		[Export ("embeddedSizeForSettingsController")]
		CGSize EmbeddedSizeForSettingsController ();

		[Export ("hasSettingsControllerForDriverClass:")]
		bool HasSettingsControllerForDriverClass (Class driver);

		[Export ("registerView:")]
		void RegisterView (UIView view);

		[Export ("unregisterView:")]
		void UnregisterView (UIView view);

		[Export ("driverAllowsClassification")]
		bool DriverAllowsClassification ();

		[Export ("touchInfoForTouch:")]
		IPSPDFStylusTouch TouchInfoForTouch (UITouch touch);

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFStylusDriverDelegate aDelegate);

		[Export ("removeDelegate:")]
		void RemoveDelegate (IPSPDFStylusDriverDelegate aDelegate);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationStateManagerStylusSupport : IPSPDFStylusDriverDelegate {

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
		PSPDFStylusTouchClassification Classification ();

		[Export ("pressure")]
		nfloat Pressure ();
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFStaticTableViewController))]
	interface PSPDFStylusViewController {

		[Export ("initWithDriverClasses:")]
		IntPtr Constructor (NSObject [] driverClasses);

		[Export ("selectedDriverClass", ArgumentSemantic.Strong), NullAllowed]
		Class SelectedDriverClass { get; set; }

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

		[Export ("sections", ArgumentSemantic.Copy)]
		NSObject [] Sections { get; set; }

		[Export ("disableScrollingIfContentFits", ArgumentSemantic.Assign)]
		bool DisableScrollingIfContentFits { get; set; }

		[Export ("updateVisibleCells")]
		void UpdateVisibleCells ();
	}

	[BaseType (typeof (UITableViewController))]
	interface PSPDFBaseTableViewController {

		[Export ("automaticallyAdjustsTableViewInsets", ArgumentSemantic.Assign)]
		bool AutomaticallyAdjustsTableViewInsets { get; set; }

		[Export ("automaticallyResizesPopover", ArgumentSemantic.Assign)]
		bool AutomaticallyResizesPopover { get; set; }

		[Export ("minimumHeightForAutomaticallyResizingPopover", ArgumentSemantic.Assign)]
		nfloat MinimumHeightForAutomaticallyResizingPopover { get; set; }

		[Export ("hideSeparatorLinesAfterLastCell", ArgumentSemantic.Assign)]
		bool HideSeparatorLinesAfterLastCell { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; set; }

		// PSPDFBaseTableViewController (SubclassingHooks) Category

		[Export ("updatePopoverSize")]
		void UpdatePopoverSize ();

		[Export ("requiredPopoverSize")]
		CGSize RequiredPopoverSize ();

		[Advice ("Requires base call if overridden")]
		[Export ("contentSizeDidChange")]
		void ContentSizeDidChange ();
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

		[Export ("contentState", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerControllerContentState ContentState { get; }

		[Export ("state", ArgumentSemantic.Assign)]
		PSPDFMediaPlayerControllerState State { get; }

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

		[Export ("playAndPauseOtherInstances:")]
		void PlayAndPauseOtherInstances (bool pauseOtherInstances);

		[Export ("pause")]
		void Pause ();

		[Static, Export ("pauseAllInstances")]
		void PauseAllInstances ();

		[Export ("seekToTime:")]
		void SeekToTime (CMTime time);

		[Export ("setShouldHideToolbar:animated:")]
		void SetShouldHideToolbar (bool shouldHideToolbar, bool animated);

		// PSPDFMediaPlayerController (Advanced) Category

		[Export ("player", ArgumentSemantic.Strong), NullAllowed]
		AVPlayer Player { get; }
	}

	interface IPSPDFMediaPlayerControllerDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFMediaPlayerControllerDelegate {

		[Export ("mediaPlayerControllerDidStartPlaying:")]
		void MediaPlayerControllerDidStartPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidPause:")]
		void MediaPlayerControllerDidPause (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerControllerDidFinishPlaying:")]
		void MediaPlayerControllerDidFinishPlaying (PSPDFMediaPlayerController controller);

		[Export ("mediaPlayerController:didHideToolbar:")]
		void DidHideToolbar (PSPDFMediaPlayerController controller, bool hidden);

		[Export ("mediaPlayerController:contentStateDidChange:")]
		void ContentStateDidChange (PSPDFMediaPlayerController controller, PSPDFMediaPlayerControllerContentState contentState);

		[Export ("mediaPlayerController:stateDidChange:")]
		void StateDidChange (PSPDFMediaPlayerController controller, PSPDFMediaPlayerControllerState state);
	}

	interface IPSPDFRemoteContentObject { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFRemoteContentObject {

		[Export ("remoteContent")]
		NSObject GetRemoteContent ();

		[Export ("setRemoteContent:")]
		void SetRemoteContent (NSObject remoteContent);

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
		void SetRemoteContentError (NSError remoteContentError);

		[Export ("setCompletionBlock:")]
		void SetCompletionHandler (Action<PSPDFRemoteContentObject> handler);

		[Export ("URLRequestForRemoteContent")]
		[Abstract]
		NSUrlRequest UrlRequestForRemoteContent ();

		[Export ("shouldCacheRemoteContent")]
		bool ShouldCacheRemoteContent ();

		[Export ("shouldRetryLoadingRemoteContentOnConnectionFailure")]
		bool ShouldRetryLoadingRemoteContentOnConnectionFailure ();

		[Export ("hasRemoteContent")]
		bool HasRemoteContent ();
	}

	interface IPSPDFSecurityAuditor { }

	[Protocol, Model]
	interface PSPDFSecurityAuditor {

		[Export ("hasPermissionForEvent:isUserAction:")]
		[Abstract]
		bool IsUserAction (PSPDFSecurityEvent aEvent, bool isUserAction);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultSecurityAuditor : PSPDFSecurityAuditor {

	}
}