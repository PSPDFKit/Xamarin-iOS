using System;

using Foundation;
using ObjCRuntime;
using CoreGraphics;
using CoreFoundation;

#if __IOS__
using UIKit;
using MessageUI;
using CoreSpotlight;
#elif __MAC__
using AppKit;
using UIBezierPath = AppKit.NSBezierPath;
using UIColor = AppKit.NSColor;
using UIImage = AppKit.NSImage;
using UITextAlignment = AppKit.NSTextAlignment;
using UIFont = AppKit.NSFont;
using UIEdgeInsets = AppKit.NSEdgeInsets;
using UIFontDescriptorSymbolicTraits = AppKit.NSFontSymbolicTraits;
#endif

#if !__WATCH__
using AVFoundation;
#endif

namespace PSPDFKit.Core {

	[Abstract]
	[BaseType (typeof (PSPDFAction))]
	interface PSPDFAbstractFormAction {

		[Export ("fields", ArgumentSemantic.Copy), NullAllowed]
		NSObject [] Fields { get; set; }
	}

	[Abstract]
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

	[Abstract]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFAbstractShapeAnnotation {

		[Export ("pointSequences", ArgumentSemantic.Strong)]
		NSArray<NSValue> [] PointSequences { get; set; }
	}

	[Static]
	interface PSPDFActionOptionsKeys {

		[Field ("PSPDFActionOptionModalKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ModalKey { get; }

		[Field ("PSPDFActionOptionAutoplayKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AutoplayKey { get; }

		[Field ("PSPDFActionOptionControlsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ControlsKey { get; }

		[Field ("PSPDFActionOptionLoopKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString LoopKey { get; }

		[Field ("PSPDFActionOptionFullscreenKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FullscreenKey { get; }

		[Field ("PSPDFActionOptionOffsetKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString OffsetKey { get; }

		[Field ("PSPDFActionOptionSizeKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SizeKey { get; }

		[Field ("PSPDFActionOptionPopoverKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PopoverKey { get; }

		[Field ("PSPDFActionOptionCoverKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoverKey { get; }

		[Field ("PSPDFActionOptionPageKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageKey { get; }

		[Field ("PSPDFActionOptionButtonKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ButtonKey { get; }

		[Field ("PSPDFActionOptionCloseButtonKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CloseButtonKey { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAction : INSSecureCoding, INativeObject {

		[Field ("PSPDFActionTypeTransformerName", PSPDFKitLibraryPath.LibraryPath)]
		NSString TypeTransformerName { get; }

		[Static]
		[Export ("actionClassForType:")]
		Class GetActionClass (PSPDFActionType actionType);

		[Export ("type", ArgumentSemantic.Assign)]
		PSPDFActionType Type { get; }

		[Export ("parentAction", ArgumentSemantic.Weak)]
		PSPDFAction ParentAction { get; }

		[Export ("subActions", ArgumentSemantic.Strong)]
		PSPDFAction [] SubActions { get; set; }

		[Export ("options"), NullAllowed]
		NSDictionary<NSString, NSObject> Options { get; }

		[Export ("localizedDescriptionWithDocumentProvider:")]
		string GetLocalizedDescription ([NullAllowed] PSPDFDocumentProvider documentProvider);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFAESCryptoDataProvider : PSPDFFileDataProviding {

		[Export ("initWithURL:passphraseProvider:salt:rounds:")]
		IntPtr Constructor (NSUrl url, Func<NSString> passphraseProvider, string salt, nuint rounds);

		[Export ("initWithURL:passphraseDataProvider:salt:rounds:")]
		IntPtr Constructor (NSUrl url, Func<NSData> passphraseDataProvider, NSData saltData, nuint rounds);

		[Export ("initWithURL:passphraseProvider:")]
		[Internal]
		IntPtr InitWithURL (NSUrl url, Func<NSString> passphraseProvider);

		[Export ("initWithLegacyFileFormatURL:passphraseProvider:")]
		[Internal]
		IntPtr InitWithLegacyFileFormatURL (NSUrl url, Func<NSString> passphraseProvider);

		[Export ("initWithURL:binaryKeyProvider:")]
		IntPtr Constructor (NSUrl url, Func<NSData> binaryKeyProvider);

		[Export ("fileURL")]
		new NSUrl FileUrl { get; }
	}

	[BaseType (typeof (PSPDFCryptoInputStream))]
	interface PSPDFAESCryptoInputStream {

		[Export ("initWithInputStream:passphrase:")]
		IntPtr Constructor (NSInputStream stream, string passphrase);

		[Internal]
		[Export ("read:maxLength:")]
		nint _Read (IntPtr buffer, nuint len);

		[NullAllowed, Export ("closeWithData"), New]
		NSData Close ();
	}

	[BaseType (typeof (PSPDFCryptoOutputStream))]
	interface PSPDFAESCryptoOutputStream {

		[Export ("initWithOutputStream:passphrase:")]
		IntPtr Constructor (NSOutputStream stream, string passphrase);

		[Export ("close"), New]
		void Close ();
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotation : PSPDFUndoSupport, INativeObject {

		[Static]
		[Export ("isWriteable")]
		bool IsWriteable { get; }

		[Static]
		[Export ("isDeletable")]
		bool IsDeletable { get; }

		[Export ("fixedSize")]
		CGSize FixedSize { get; }

		[Export ("wantsSelectionBorder")]
		bool WantsSelectionBorder { get; }

		[Export ("requiresPopupAnnotation")]
		bool RequiresPopupAnnotation { get; }

		[Export ("readOnly")]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("locked")]
		bool Locked { [Bind ("isLocked")] get; }

		[Export ("contentsLocked")]
		bool ContentsLocked { [Bind ("isContentsLocked")] get; }

		[Export ("movable")]
		bool Movable { [Bind ("isMovable")] get; }

		[Export ("resizable")]
		bool Resizable { [Bind ("isResizable")] get; }

		[Export ("shouldMaintainAspectRatio")]
		bool ShouldMaintainAspectRatio { get; }

		[Export ("minimumSize")]
		CGSize MinimumSize { get; }

		[Export ("hitTest:minDiameter:")]
		bool HitTest (CGPoint point, nfloat minDiameter);

		[Export ("boundingBoxForPageRect:")]
		CGRect GetBoundingBox (CGRect pageRect);

		[Export ("type")]
		PSPDFAnnotationType Type { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("absolutePageIndex")]
		nuint AbsolutePageIndex { get; set; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("dirty")]
		bool Dirty { [Bind ("isDirty")] get; set; }

		[Export ("overlay")]
		bool Overlay { [Bind ("isOverlay")] get; set; }

		[Export ("editable")]
		bool Editable { [Bind ("isEditable")] get; set; }

		[Export ("deleted")]
		bool Deleted { [Bind ("isDeleted")] get; set; }

		[Export ("typeString")]
		string TypeString { get; set; }

		[Export ("alpha")]
		nfloat Alpha { get; set; }

		[NullAllowed, Export ("color", ArgumentSemantic.Strong)]
		UIColor Color { get; set; }

		[NullAllowed, Export ("borderColor", ArgumentSemantic.Strong)]
		UIColor BorderColor { get; set; }

		[NullAllowed, Export ("fillColor", ArgumentSemantic.Strong)]
		UIColor FillColor { get; set; }

		[NullAllowed, Export ("contents")]
		string Contents { get; set; }

		[NullAllowed, Export ("subject")]
		string Subject { get; set; }

		[NullAllowed, Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, PSPDFAction> AdditionalActions { get; set; }

		[NullAllowed, Export ("value", ArgumentSemantic.Copy)]
		NSObject Value { get; set; }

		[Export ("flags", ArgumentSemantic.Assign)]
		PSPDFAnnotationFlags Flags { get; set; }

		[Export ("hidden")]
		bool Hidden { [Bind ("isHidden")] get; set; }

		[NullAllowed, Export ("name")]
		string Name { get; set; }

		[NullAllowed, Export ("user")]
		string User { get; set; }

		[NullAllowed, Export ("group")]
		string Group { get; set; }

		[NullAllowed, Export ("creationDate", ArgumentSemantic.Strong)]
		NSDate CreationDate { get; set; }

		[NullAllowed, Export ("lastModified", ArgumentSemantic.Strong)]
		NSDate LastModified { get; set; }

		[Export ("lineWidth")]
		nfloat LineWidth { get; set; }

		[Export ("borderStyle", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderStyle BorderStyle { get; set; }

		[NullAllowed, Export ("dashArray", ArgumentSemantic.Copy)]
		NSNumber [] DashArray { get; set; }

		[Export ("borderEffect", ArgumentSemantic.Assign)]
		PSPDFAnnotationBorderEffect BorderEffect { get; set; }

		[Export ("borderEffectIntensity")]
		nfloat BorderEffectIntensity { get; set; }

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[NullAllowed, Export ("rects", ArgumentSemantic.Copy)]
		NSValue [] Rects { get; set; }

		[NullAllowed, Export ("points", ArgumentSemantic.Copy)]
		NSValue [] Points { get; set; }

		[Export ("objectNumber")]
		nint ObjectNumber { get; }

		[Export ("localizedDescription")]
		string LocalizedDescription { get; }

		[NullAllowed, Export ("annotationIcon")]
		UIImage AnnotationIcon { get; }

		[Export ("reply")]
		bool Reply { [Bind ("isReply")] get; }

		[NullAllowed, Export ("inReplyToAnnotation", ArgumentSemantic.Strong)]
		PSPDFAnnotation InReplyToAnnotation { get; set; }

		[Export ("isEqualToAnnotation:")]
		bool IsEqualTo (PSPDFAnnotation otherAnnotation);

		// PSPDFAnnotation (AppearanceStream) Category

		[Export ("hasAppearanceStream")]
		bool HasAppearanceStream { get; }

		[NullAllowed, Export ("appearanceStreamGenerator", ArgumentSemantic.Strong)]
		IPSPDFAppearanceStreamGenerating AppearanceStreamGenerator { get; set; }

		[Export ("maybeRenderCustomAppearanceStreamWithContext:withOptions:")]
		bool MaybeRenderCustomAppearanceStream (CGContext context, [NullAllowed] NSDictionary<NSString, NSObject> options);

		// PSPDFAnnotation (Drawing) Category

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary options);

		[Wrap ("DrawInContext (context, drawOptions?.Dictionary)")]
		void DrawInContext (CGContext context, PSPDFAnnotationDrawOptions drawOptions);

		[Export ("imageWithSize:withOptions:")]
		[return: NullAllowed]
		UIImage GetImage (CGSize size, [NullAllowed] NSDictionary options);

		[Export ("noteIconPoint")]
		CGPoint NoteIconPoint { get; }

		[Export ("shouldDrawNoteIconIfNeeded")]
		bool ShouldDrawNoteIconIfNeeded { get; }

		// PSPDFAnnotation (Advanced) Category

		[Export ("shouldUpdatePropertiesOnBoundsChange")]
		bool ShouldUpdatePropertiesOnBoundsChange { get; }

		[Export ("shouldUpdateOptionalPropertiesOnBoundsChange")]
		bool ShouldUpdateOptionalPropertiesOnBoundsChange { get; }

		[Export ("updatePropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdatePropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("updateOptionalPropertiesWithTransform:isSizeChange:meanScale:")]
		void UpdateOptionalPropertiesWithTransform (CGAffineTransform transform, bool isSizeChange, nfloat meanScale);

		[Export ("setBoundingBox:transform:includeOptional:")]
		void SetBoundingBox (CGRect boundingBox, bool transform, bool optionalProperties);

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Export ("shouldDeleteAnnotation")]
		bool ShouldDeleteAnnotation { get; }

		// PSPDFAnnotation (Fonts) Category

		[Field ("PSPDFFontSizeName", PSPDFKitLibraryPath.LibraryPath)]
		NSString FontSizeNameKey { get; }

		[Field ("PSPDFVerticalAlignmentName", PSPDFKitLibraryPath.LibraryPath)]
		NSString VerticalAlignmentNameKey { get; }

		[NullAllowed, Export ("fontAttributes", ArgumentSemantic.Copy)]
		NSDictionary FontAttributes { get; set; }

		[NullAllowed, Export ("fontName")]
		string FontName { get; set; }

		[Export ("fontSize")]
		nfloat FontSize { get; set; }

		[Export ("textAlignment", ArgumentSemantic.Assign)]
		UITextAlignment TextAlignment { get; set; }

		[Export ("verticalTextAlignment", ArgumentSemantic.Assign)]
		PSPDFVerticalAlignment VerticalTextAlignment { get; set; }

		[Export ("defaultFontSize")]
		nfloat DefaultFontSize { get; }

		[Export ("defaultFontName")]
		string DefaultFontName { get; }

		[Export ("defaultFont")]
		UIFont DefaultFont { get; }

		[NullAllowed, Export ("attributedString")]
		NSAttributedString AttributedString { get; }

		[Export ("attributedStringWithContents:")]
		[return: NullAllowed]
		NSAttributedString GetAttributedString ([NullAllowed] string contents);

		// PSPDFUndoSupport protocol support

		[Static]
		[Export ("keysForValuesToObserveForUndo")]
		NSSet<NSString> GetKeysForValuesToObserveForUndo ();

		[Static]
		[Export ("localizedUndoActionNameForKey:")]
		[return: NullAllowed]
		string LocalizedUndoActionName (string key);

		[Static]
		[Export ("undoCoalescingForKey:")]
		PSPDFUndoCoalescing GetUndoCoalescing (string key);

		// PSPDFAnnotation (InstantJSON) Category

		[Static]
		[Export ("annotationFromInstantJSON:documentProvider:error:")]
		[return: NullAllowed]
		PSPDFAnnotation FromInstantJson (NSData instantJson, PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		[Export ("generateInstantJSONWithError:")]
		[return: NullAllowed]
		NSData GenerateInstantJson ([NullAllowed] out NSError error);

		[Export ("hasBinaryInstantJSONAttachment")]
		bool HasBinaryInstantJsonAttachment { get; }

		[Export ("writeBinaryInstantJSONAttachmentToDataSink:error:")]
		bool WriteBinaryInstantJsonAttachment (IPSPDFDataSink dataSink, [NullAllowed] out NSError error);

		[Export ("attachBinaryInstantJSONAttachmentFromDataProvider:error:")]
		bool AttachBinaryInstantJsonAttachment (IPSPDFDataProviding dataProvider, [NullAllowed] out NSError error);
	}

	[Static]
	interface PSPDFAnnotationDrawOptionsKeys {

		[Field ("PSPDFAnnotationDrawFlattenedKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DrawFlattenedKey { get; }

		[Field ("PSPDFAnnotationDrawForPrintingKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DrawForPrintingKey { get; }

		[Field ("PSPDFAnnotationDrawCenteredKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DrawCenteredKey { get; }

		[Field ("PSPDFAnnotationMarginKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MarginKey { get; }
	}

	[StrongDictionary ("PSPDFAnnotationDrawOptionsKeys")]
	interface PSPDFAnnotationDrawOptions {
		bool DrawFlattened { get; set; }
		bool DrawForPrinting { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationAppearanceStream {

		[Static]
		[Export ("appearanceStreamWithImage:")]
		PSPDFAnnotationAppearanceStream FromImage (UIImage image);

		[Static]
		[Export ("appearanceStreamWithFileURL:")]
		PSPDFAnnotationAppearanceStream FromImage (NSUrl imageFileUrl);

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[NullAllowed, Export ("imageFileURL")]
		NSUrl ImageFileUrl { get; }
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

		[Export ("editableItems")]
		PSPDFAnnotationGroupItem [] EditableItems { get; }

		[Export ("choice")]
		nuint Choice { get; set; }

		[Export ("updateChoiceToItemWithType:variant:")]
		bool UpdateChoiceToItem ([BindAs (typeof (PSPDFAnnotationString))] NSString type, [BindAs (typeof (PSPDFAnnotationVariantString))] NSString variant);
	}

	delegate UIImage PSPDFAnnotationGroupItemConfigurationHandler (PSPDFAnnotationGroupItem item, [NullAllowed] NSObject container, UIColor tintColor);

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationGroupItem {

		[Static]
		[Export ("itemWithType:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType);

		[Static]
		[Export ("itemWithType:variant:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString annotationVariant);

		[Static]
		[Export ("itemWithType:variant:configurationBlock:")]
		PSPDFAnnotationGroupItem FromType ([BindAs (typeof (PSPDFAnnotationString))] NSString annotationType, [NullAllowed] [BindAs (typeof (PSPDFAnnotationVariantString))] NSString annotationVariant, PSPDFAnnotationGroupItemConfigurationHandler handler);

		[Static]
		[Export ("defaultConfigurationBlock")]
		PSPDFAnnotationGroupItemConfigurationHandler DefaultConfigurationHandler { get; }

		[BindAs (typeof (PSPDFAnnotationString))]
		[Export ("type")]
		NSString Type { get; }

		[BindAs (typeof (PSPDFAnnotationVariantString))]
		[NullAllowed, Export ("variant")]
		NSString Variant { get; }

		[Export ("configurationBlock", ArgumentSemantic.Copy)]
		PSPDFAnnotationGroupItemConfigurationHandler ConfigurationHandler { get; }
	}

	interface PSPDFAnnotationChangedNotificationEventArgs {

		[Export ("PSPDFAnnotationChangedNotificationIgnoreUpdateKey")]
		bool IgnoreUpdate { get; set; }

		[Export ("PSPDFAnnotationChangedNotificationKeyPathKey")]
		string [] KeyPaths { get; set; }
	}

	[Static]
	interface PSPDFAnnotationOptionsKeys {

		[Field ("PSPDFAnnotationOptionUserCreatedKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString UserCreatedKey { get; }

		[Field ("PSPDFAnnotationOptionSuppressNotificationsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SuppressNotificationsKey { get; }

		[Field ("PSPDFAnnotationOptionAnimateViewKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnimateViewKey { get; }
	}

	[StrongDictionary ("PSPDFAnnotationOptionsKeys")]
	interface PSPDFAnnotationOptions {
		bool UserCreated { get; set; }
		bool SuppressNotifications { get; set; }
		bool AnimateView { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationManager : PSPDFAnnotationProviderChangeNotifier {

		[Field ("PSPDFAnnotationsAddedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AnnotationsAddedNotification { get; }

		[Field ("PSPDFAnnotationsRemovedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AnnotationsRemovedNotification { get; }

		[Field ("PSPDFAnnotationChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFAnnotationChangedNotificationEventArgs))]
		NSString AnnotationChangedNotification { get; }

		[Field ("PSPDFAnnotationOptionSuppressNotificationsKey", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AnnotationOptionSuppressNotification { get; }

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("annotationProviders", ArgumentSemantic.Copy)]
		IPSPDFAnnotationProvider [] AnnotationProviders { get; set; }

		[Export ("fileAnnotationProvider"), NullAllowed]
		PSPDFFileAnnotationProvider FileAnnotationProvider { get; }

		[Export ("annotationsForPageAtIndex:type:")]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotationsForPage (nuint pageIndex);

		[Export ("annotationViewClassForAnnotation:")]
		[return: NullAllowed]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Wrap ("Class.Lookup (GetAnnotationViewClass (annotation))")]
		Type GetAnnotationViewType (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, string [] keyPaths, [NullAllowed] NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations { get; }

		[Export ("updateAnnotations:animated:")]
		new void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Export ("annotationsIncludingGroupsFromAnnotations:")]
		PSPDFAnnotation [] GetAnnotationsIncludingGroups (PSPDFAnnotation [] annotations);

		[Export ("protocolStrings", ArgumentSemantic.Copy)]
		string [] GetProtocolStrings { get; set; }

		[Static]
		[Export ("fileTypeTranslationTable")]
		NSDictionary<NSString, NSNumber> FileTypeTranslationTable { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		// PSPDFAnnotationManager (SubclassingHooks) Category

		[NullAllowed, Export ("dirtyAnnotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> DirtyAnnotations { get; }

		[Static]
		[Export ("mediaFileTypes")]
		string [] GetMediaFileTypes ();

		[Export ("defaultAnnotationViewClassForAnnotation:")]
		[return: NullAllowed]
		Class GetDefaultAnnotationViewClass (PSPDFAnnotation annotation);

		[Wrap ("Class.Lookup (GetDefaultAnnotationViewClass (annotation))")]
		Type GetDefaultAnnotationViewType (PSPDFAnnotation annotation);
	}

	interface IPSPDFAnnotationProvider {}

	[Protocol]
	interface PSPDFAnnotationProvider {

		[Abstract]
		[Export ("annotationsForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Export ("hasLoadedAnnotationsForPageAtIndex:")]
		bool HasLoadedAnnotationsForPage (nuint pageIndex);

		[Export ("annotationViewClassForAnnotation:")]
		Class GetAnnotationViewClass (PSPDFAnnotation annotation);

		[Export ("addAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("removeAnnotations:options:")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("shouldSaveAnnotations")]
		bool ShouldSaveAnnotations ();

		[NullAllowed, Export ("dirtyAnnotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetDirtyAnnotations ();

		[Export ("didChangeAnnotation:keyPaths:options:")]
		void DidChangeAnnotation (PSPDFAnnotation annotation, string [] keyPaths, [NullAllowed] NSDictionary options);

		[Export ("providerDelegate")]
		IPSPDFAnnotationProviderChangeNotifier GetProviderDelegate ();

		[Export ("setProviderDelegate:")]
		void SetProviderDelegate ([NullAllowed] IPSPDFAnnotationProviderChangeNotifier providerDelegate);

		[Export ("delegate")]
		IPSPDFAnnotationProviderDelegate GetDelegate ();

		[Export ("setDelegate:")]
		void SetDelegate ([NullAllowed] IPSPDFAnnotationProviderDelegate provider);
	}

	interface IPSPDFAnnotationProviderDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFAnnotationProviderDelegate {

		[Export ("annotationProvider:didSaveAnnotations:")]
		void DidSaveAnnotations (IPSPDFAnnotationProvider annotationProvider, PSPDFAnnotation [] annotations);

		[Export ("annotationProvider:failedToSaveAnnotations:error:")]
		void FailedToSaveAnnotations (IPSPDFAnnotationProvider annotationProvider, PSPDFAnnotation [] annotations, NSError error);
	}

	interface IPSPDFAnnotationProviderChangeNotifier { }

	[Protocol]
	interface PSPDFAnnotationProviderChangeNotifier {

		[Abstract]
		[Export ("updateAnnotations:animated:")]
		void UpdateAnnotations (PSPDFAnnotation [] annotations, bool animated);

		[Abstract]
		[NullAllowed, Export ("parentDocumentProvider")]
		PSPDFDocumentProvider ParentDocumentProvider { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationSet {

		[Export ("initWithAnnotations:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAnnotation [] annotations);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("drawInContext:withOptions:")]
		void DrawInContext (CGContext context, [NullAllowed] NSDictionary options);

		[Wrap ("DrawInContext (context, drawOptions?.Dictionary)")]
		void DrawInContext (CGContext context, PSPDFAnnotationDrawOptions drawOptions);

		[Export ("boundingBox", ArgumentSemantic.Assign)]
		CGRect BoundingBox { get; set; }

		[Export ("copyToClipboard")]
		void CopyToClipboard ();

		[Static]
		[Export ("unarchiveFromClipboard")]
		[return: NullAllowed]
		PSPDFAnnotationSet UnarchiveFromClipboard ();
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationStyle {

		[Export ("initWithName:")]
		[DesignatedInitializer]
		IntPtr Constructor (string styleName);

		[Export ("styleName")]
		string StyleName { get; set; }

		[NullAllowed, Export ("styleDictionary", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> StyleDictionary { get; set; }

		[Export ("setStyle:forKey:")]
		void SetStyle ([NullAllowed] NSObject style, string key);

		[Export ("applyStyleToAnnotation:")]
		void ApplyStyleToAnnotation (PSPDFAnnotation annotation);
	}

	interface IPSPDFAnnotationStyleManager {}

	[Protocol]
	interface PSPDFAnnotationStyleManager {

		[Abstract]
		[Export ("shouldUpdateDefaultsForAnnotationChanges")]
		bool ShouldUpdateDefaultsForAnnotationChanges { get; set; }

		[Abstract]
		[Export ("setupDefaultStylesIfNeeded")]
		void SetupDefaultStylesIfNeeded ();

		[Abstract]
		[NullAllowed, Export ("styleKeys", ArgumentSemantic.Copy)]
		NSSet<NSString> StyleKeys { get; set; }

		[Abstract]
		[Export ("stylesForKey:")]
		[return: NullAllowed]
		PSPDFAnnotationStyle [] GetStyles (NSString key);

		[Abstract]
		[Export ("addStyle:forKey:")]
		void AddStyle (PSPDFAnnotationStyle style, NSString key);

		[Abstract]
		[Export ("removeStyle:forKey:")]
		void RemoveStyle (PSPDFAnnotationStyle style, NSString key);

		[Abstract]
		[Export ("lastUsedStyleForKey:")]
		[return: NullAllowed]
		PSPDFAnnotationStyle GetLastUsedStyle (NSString key);

		[Abstract]
		[Export ("lastUsedProperty:forKey:")]
		[return: NullAllowed]
		NSObject GetLastUsedProperty (NSString styleProperty, NSString key);

		[Abstract]
		[Export ("setLastUsedValue:forProperty:forKey:")]
		void SetLastUsedValue ([NullAllowed] NSObject value, NSString styleProperty, NSString key);

		[Abstract]
		[Export ("defaultPresetsForKey:type:")]
		[return: NullAllowed]
		PSPDFModel [] GetDefaultPresets (NSString key, NSString type);

		[Abstract]
		[Export ("setDefaultPresets:forKey:type:")]
		void SetDefaultPresets ([NullAllowed] PSPDFModel [] presets, NSString key, NSString type);

		[Abstract]
		[Export ("presetsForKey:type:")]
		[return: NullAllowed]
		PSPDFModel [] GetPresets (NSString key, NSString type);

		[Abstract]
		[Export ("setPresets:forKey:type:")]
		void SetPresets ([NullAllowed] PSPDFModel [] presets, NSString key, NSString type);

		[Abstract]
		[Export ("isPresetModifiedAtIndex:forKey:type:")]
		bool IsPresetModified (nuint index, NSString key, NSString type);

		[Abstract]
		[Export ("resetPresetAtIndex:forKey:type:")]
		bool ResetPreset (nuint idx, NSString key, NSString type);
	}

	[Static]
	interface PSPDFStyleManagerKeys {

		[Field ("PSPDFAnnotationStyleTypeLastUsed", PSPDFKitLibraryPath.LibraryPath)]
		NSString LastUsedStylesKey { get; }

		[Field ("PSPDFAnnotationStyleTypeGeneric", PSPDFKitLibraryPath.LibraryPath)]
		NSString GenericStylesKey { get; }

		[Field ("PSPDFAnnotationStyleTypeColorPreset", PSPDFKitLibraryPath.LibraryPath)]
		NSString ColorPresetKey { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultAnnotationStyleManager : PSPDFAnnotationStyleManager {

		// PSPDFDefaultAnnotationStyleManager (Defaults) Category

		[Export ("setupDefaultStyles")]
		void SetupDefaultStyles ();

		[Export ("defaultColorPresetsForKey:")]
		PSPDFColorPreset [] GetDefaultColorPresets (string key);

		[Export ("defaultBorderPresetsForKey:")]
		NSObject [] GetDefaultBorderPresets (string key);

		[Static]
		[Export ("highlightYellow")]
		UIColor HighlightYellow { get; }

		[Static]
		[Export ("drawingBlue")]
		UIColor DrawingBlue { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAnnotationSummarizer {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("annotationSummaryForPages:")]
		NSAttributedString GetAnnotationSummary (NSIndexSet pages);

		[Export ("temporaryTextFileURLForPages:error:")]
		[return: NullAllowed]
		NSUrl GetTemporaryTextFileUrl (NSIndexSet pages, [NullAllowed] out NSError error);

		[Static]
		[Export ("temporaryTextFileURLForDocuments:error:")]
		[return: NullAllowed]
		NSUrl GetTemporaryTextFileUrl (PSPDFDocument [] documents, [NullAllowed] out NSError error);

		[Static]
		[Export ("annotationSummaryForDocuments:")]
		NSAttributedString GetAnnotationSummary (PSPDFDocument [] documents);
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAnnotationToolbarConfiguration {

		[Export ("initWithAnnotationGroups:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAnnotationGroup [] annotationGroups);

		[Export ("annotationGroups")]
		PSPDFAnnotationGroup [] AnnotationGroups { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFAppearanceCharacteristics {

		[NullAllowed, Export ("normalIcon", ArgumentSemantic.Strong)]
		UIImage NormalIcon { get; set; }
	}

	interface IPSPDFAppearanceStreamGenerating {}

	[Protocol]
	interface PSPDFAppearanceStreamGenerating {

		[Abstract]
		[Export ("dataProviderForAnnotation:options:")]
		[return: NullAllowed]
		IPSPDFDataProviding GetDataProvider (PSPDFAnnotation annotation, [NullAllowed] NSDictionary<NSString, NSObject> options);
	}

	[Static]
	interface PSPDFAppearanceStreamGenerationOptionsKeys {

		[Field ("PSPDFAppearanceGenerationFlatten", PSPDFKitLibraryPath.LibraryPath)]
		NSString FlattenKey { get; }

		[Field ("PSPDFAppearanceGenerationPrint", PSPDFKitLibraryPath.LibraryPath)]
		NSString PrintKey { get; }
	}

	interface IPSPDFApplicationPolicy {}

	[Protocol]
	interface PSPDFApplicationPolicy {

		[Abstract]
		[Export ("hasPermissionForEvent:isUserAction:")]
		bool HasPermissionForEvent (NSString @event, bool isUserAction);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultApplicationPolicy : PSPDFApplicationPolicy {

	}

	[Static]
	interface PSPDFPolicyEvents {

		[Field ("PSPDFPolicyEventOpenIn", PSPDFKitLibraryPath.LibraryPath)]
		NSString OpenIn { get; }

		[Field ("PSPDFPolicyEventPrint", PSPDFKitLibraryPath.LibraryPath)]
		NSString Print { get; }

		[Field ("PSPDFPolicyEventEmail", PSPDFKitLibraryPath.LibraryPath)]
		NSString Email { get; }

		[Field ("PSPDFPolicyEventMessage", PSPDFKitLibraryPath.LibraryPath)]
		NSString Message { get; }

		[Field ("PSPDFPolicyEventQuickLook", PSPDFKitLibraryPath.LibraryPath)]
		NSString QuickLook { get; }

		[Field ("PSPDFPolicyEventAudioRecording", PSPDFKitLibraryPath.LibraryPath)]
		NSString AudioRecording { get; }

		[Field ("PSPDFPolicyEventCamera", PSPDFKitLibraryPath.LibraryPath)]
		NSString EventCamera { get; }

		[Field ("PSPDFPolicyEventPhotoLibrary", PSPDFKitLibraryPath.LibraryPath)]
		NSString PhotoLibrary { get; }

		[Field ("PSPDFPolicyEventPasteboard", PSPDFKitLibraryPath.LibraryPath)]
		NSString Pasteboard { get; }

		[Field ("PSPDFPolicyEventSubmitForm", PSPDFKitLibraryPath.LibraryPath)]
		NSString SubmitForm { get; }

		[Field ("PSPDFPolicyEventNetwork", PSPDFKitLibraryPath.LibraryPath)]
		NSString Network { get; }
	}

	[Abstract]
	[BaseType (typeof (PSPDFLinkAnnotation))]
	interface PSPDFAssetAnnotation {

		[NullAllowed, Export ("assetName")]
		string AssetName { get; }

		[Export ("fileURLWithError:")]
		[return: NullAllowed]
		NSUrl GetFileUrl ([NullAllowed] out NSError error);
	}

	interface IPSPDFBackForwardActionListDelegate {}

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionListDelegate {

		[Abstract]
		[Export ("backForwardList:requestedBackActionExecution:")]
		void RequestedBackActionExecution (PSPDFBackForwardActionList list, PSPDFAction [] actions);

		[Abstract]
		[Export ("backForwardList:requestedForwardActionExecution:")]
		void RequestedForwardActionExecution (PSPDFBackForwardActionList list, PSPDFAction [] actions);

		[Export ("backForwardListDidUpdate:")]
		void BackForwardListDidUpdate (PSPDFBackForwardActionList list);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFBackForwardActionList {

		[Export ("initWithDelegate:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] IPSPDFBackForwardActionListDelegate @delegate);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
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

		[Export ("backListCap")]
		nuint BackListCap { get; set; }

		[NullAllowed, Export ("backAction")]
		PSPDFAction BackAction { get; }

		[NullAllowed, Export ("forwardAction")]
		PSPDFAction ForwardAction { get; }

		[Export ("backList", ArgumentSemantic.Copy)]
		PSPDFAction [] BackList { get; }

		[Export ("forwardList", ArgumentSemantic.Copy)]
		PSPDFAction [] ForwardList { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFBaseConfiguration {

		[Static]
		[Export ("defaultConfiguration")]
		PSPDFBaseConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFBaseConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFBaseConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFBaseConfigurationBuilder> builderHandler);

		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFBaseConfiguration GetUpdatedConfiguration (Action<PSPDFBaseConfigurationBuilder> builderHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFBaseConfigurationBuilder {

		[Static]
		[Export ("configurationTargetClass")]
		Class ConfigurationTargetClass { get; }

		[Static]
		[Wrap ("Class.Lookup (ConfigurationTargetClass)")]
		Type ConfigurationTargetType { get; }

		[Export ("build")]
		PSPDFBaseConfiguration Build { get; }

		[Export ("reset")]
		void Reset ();
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFBookmark : INSCopying, INSMutableCopying {

		[Export ("initWithAction:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Copy)]
		PSPDFAction Action { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[Export ("displayName")]
		string DisplayName { get; }

		// PSPDFBookmark (GoToAction) Category

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (uint pageIndex);

		[Export ("pageIndex", ArgumentSemantic.Assign)]
		uint PageIndex { get; }

		// ProviderSupport (PSPDFBookmark) Category

		[Export ("identifier")]
		string Identifier { get; }

		[Export ("sortKey"), NullAllowed]
		NSNumber SortKey { get; }

		[Export ("initWithIdentifier:action:name:sortKey:")]
		IntPtr Constructor (string identifier, PSPDFAction action, [NullAllowed] string name, [NullAllowed] NSNumber sortKey);
	}

	[BaseType (typeof (PSPDFBookmark))]
	interface PSPDFMutableBookmark {

		[Export ("initWithAction:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFAction action);

		[Export ("action", ArgumentSemantic.Copy), New]
		PSPDFAction Action { get; set; }

		[Export ("name"), NullAllowed, New]
		string Name { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFBookmarkManager {

		[Notification]
		[Field ("PSPDFBookmarksChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		NSString BookmarksChangedNotification { get; }

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("bookmarks", ArgumentSemantic.Copy)]
		PSPDFBookmark [] Bookmarks { get; }

		[Export ("bookmarksWithSortOrder:")]
		PSPDFBookmark [] GetBookmarks (PSPDFBookmarkManagerSortOrder sortOrder);

		[Export ("addBookmark:")]
		void AddBookmark (PSPDFBookmark bookmark);

		[Export ("removeBookmark:")]
		void RemoveBookmark (PSPDFBookmark bookmark);

		[Export ("moveBookmarkAtIndex:toIndex:")]
		void MoveBookmark (nuint sourceIndex, nuint destinationIndex);

		[Export ("performBlock:")]
		void PerformAction (Action action);

		[Export ("performBlockAndWait:")]
		void PerformActionAndWait (Action action);

		[Export ("provider", ArgumentSemantic.Copy)]
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
		bool AddBookmark (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("removeBookmark:")]
		bool RemoveBookmark (PSPDFBookmark bookmark);

		[Abstract]
		[Export ("save")]
		void Save ();
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFButtonFormElement {

		[Export ("selected")]
		bool Selected { [Bind ("isSelected")] get; }

		[NullAllowed, Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; }

		[NullAllowed, Export ("onState")]
		string OnState { get; }

		[NullAllowed, Export ("buttonFormField")]
		PSPDFButtonFormField ButtonFormField { get; }

		[Export ("select")]
		void Select ();

		[Export ("deselect")]
		void Deselect ();

		[Export ("toggleButtonSelectionState")]
		bool ToggleButtonSelectionState ();

	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFButtonFormField {

		[Static]
		[Export ("insertedButtonFieldWithType:fullyQualifiedName:documentProvider:formElements:buttonValues:error:")]
		[return: NullAllowed]
		PSPDFButtonFormField CreateInsertedButtonField (PSPDFFormFieldType type, string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement [] formElements, string [] buttonValues, [NullAllowed] out NSError error);

		[Export ("isPushButton")]
		bool IsPushButton { get; }

		[Export ("isCheckBox")]
		bool IsCheckBox { get; }

		[Export ("isRadioButton")]
		bool IsRadioButton { get; }

		[Export ("selectedAnnotationObjectNumbers", ArgumentSemantic.Strong)]
		NSNumber [] SelectedAnnotationObjectNumbers { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; set; }

		[Export ("onStateForButton:")]
		[return: NullAllowed]
		string OnStateForButton (PSPDFWidgetAnnotation annotation);

		[Export ("toggleButton:")]
		void ToggleButton (PSPDFWidgetAnnotation annotation);

		[Export ("isSelected:")]
		bool IsSelected (PSPDFWidgetAnnotation annotation);

		[Export ("selectButton:")]
		void SelectButton (PSPDFWidgetAnnotation annotation);

		[Export ("deselectButton:")]
		void DeselectButton (PSPDFWidgetAnnotation annotation);

		[Export ("valueForButton:")]
		[return: NullAllowed]
		string ValueForButton (PSPDFWidgetAnnotation annotation);
	}

	delegate void PSPDFCacheDocumentImageRenderingCompletionHandler (UIImage image, PSPDFDocument document, nuint page, CGSize size);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFCache {

		[Export ("initWithSettings:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFKitGlobal pspdfkit);

		[Export ("memoryCache")]
		PSPDFMemoryCache MemoryCache { get; }

		[Export ("diskCache")]
		PSPDFDiskCache DiskCache { get; }

		[Export ("cacheStatusForRequest:imageSizeMatching:")]
		PSPDFCacheStatus GetCacheStatus (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching);

		[Export ("imageForRequest:imageSizeMatching:")]
		[return: NullAllowed]
		UIImage GetImage (PSPDFRenderRequest request, PSPDFCacheImageSizeMatching imageSizeMatching);

		[Export ("saveImage:forRequest:")]
		void SaveImage (UIImage image, PSPDFRenderRequest request);

		[Export ("cacheDocument:withPageSizes:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes);

		[Export ("cacheDocument:withPageSizes:imageRenderingCompletionBlock:")]
		void CacheDocument ([NullAllowed] PSPDFDocument document, NSValue [] sizes, [NullAllowed] PSPDFCacheDocumentImageRenderingCompletionHandler pageCompletionHandler);

		[Export ("stopCachingDocument:")]
		void StopCachingDocument ([NullAllowed] PSPDFDocument document);

		[Export ("invalidateImageFromDocument:pageIndex:")]
		void InvalidateImage ([NullAllowed] PSPDFDocument document, nuint pageIndex);

		[Export ("removeCacheForDocument:")]
		void RemoveCache ([NullAllowed] PSPDFDocument document);

		[Export ("clearCache")]
		void ClearCache ();
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFCaretAnnotation {

	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFChoiceFormElement {

		[NullAllowed, Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; }

		[NullAllowed, Export ("selectedIndices", ArgumentSemantic.Copy)]
		NSIndexSet SelectedIndices { get; set; }

		[NullAllowed, Export ("selectedOptions", ArgumentSemantic.Copy)]
		PSPDFFormOption [] SelectedOptions { get; }

		[Export ("customSelection")]
		bool CustomSelection { get; }

		[Export ("topIndex")]
		nuint TopIndex { get; }

		[NullAllowed, Export ("customText")]
		string CustomText { get; set; }

		[NullAllowed, Export ("choiceFormField")]
		PSPDFChoiceFormField ChoiceFormField { get; }
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFChoiceFormField {

		[Static]
		[Export ("insertedChoiceFieldWithType:fullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFChoiceFormField CreateInsertedChoiceField (PSPDFFormFieldType type, string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);

		[Export ("isCombo")]
		bool IsCombo { get; }

		[Export ("isEdit")]
		bool IsEdit { get; set; }

		[Export ("isMultiSelect")]
		bool IsMultiSelect { get; set; }

		[Export ("commitOnSelChange")]
		bool CommitOnSelChange { get; set; }

		[Export ("doNotSpellCheck")]
		bool DoNotSpellCheck { get; set; }

		[Export ("options", ArgumentSemantic.Copy)]
		PSPDFFormOption [] Options { get; set; }

		[NullAllowed, Export ("selectedIndices", ArgumentSemantic.Copy)]
		NSIndexSet SelectedIndices { get; set; }

		[Export ("customSelection")]
		bool CustomSelection { get; }

		[NullAllowed, Export ("customText")]
		string CustomText { get; set; }

		[Export ("clearOptionsCache")]
		void ClearOptionsCache ();
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFCircleAnnotation {

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFColorPreset {

		[Static]
		[Export ("presetWithColor:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color);

		[Static]
		[Export ("presetWithColor:fillColor:alpha:")]
		PSPDFColorPreset FromColor ([NullAllowed] UIColor color, [NullAllowed] UIColor fillColor, nfloat alpha);

		[NullAllowed, Export ("color")]
		UIColor Color { get; }

		[NullAllowed, Export ("colorWithAlpha")]
		UIColor ColorWithAlpha { get; }

		[NullAllowed, Export ("fillColor")]
		UIColor FillColor { get; }

		[NullAllowed, Export ("fillColorWithAlpha")]
		UIColor FillColorWithAlpha { get; }

		[Export ("alpha")]
		nfloat Alpha { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFContainerAnnotationProvider : PSPDFAnnotationProvider, PSPDFUndoSupport {

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[NullAllowed, Export ("undoController", ArgumentSemantic.Weak)]
		PSPDFUndoController UndoController { get; }

		// PSPDFContainerAnnotationProvider (SubclassingHooks) Category

		[Static]
		[Export ("shouldTrackDeletions")]
		bool ShouldTrackDeletions { get; }

		[Export ("clearNeedsSaveFlag")]
		void ClearNeedsSaveFlag ();

		[Export ("registerAnnotationsForUndo:")]
		void RegisterAnnotationsForUndo (PSPDFAnnotation [] annotations);

		[Export ("annotationCache")]
		NSMutableDictionary<NSNumber, NSArray<PSPDFAnnotation>> AnnotationCache { get; }

		[Export ("willInsertAnnotations:")]
		void WillInsertAnnotations (PSPDFAnnotation [] annotations);

		[Export ("performBlockForReading:")]
		void PerformActionForReading (Action action);

		[Export ("performBlockForWriting:")]
		void PerformActionForWriting (Action action);

		[Export ("performBlockForWritingAndWait:")]
		void PerformActionForWritingAndWait (Action action);

		[Export ("setAnnotations:forPageAtIndex:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, nuint pageIndex, bool append);

		[Export ("setAnnotations:append:")]
		void SetAnnotations (PSPDFAnnotation [] annotations, bool append);

		[Export ("addAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation[] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation[] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[Export ("removeAllAnnotationsWithOptions:")]
		void RemoveAllAnnotations (NSDictionary<NSString, NSObject> options);

		[Export ("allAnnotations")]
		PSPDFAnnotation [] AllAnnotations { get; }

		[Export ("annotations")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> Annotations { get; }

		[Export ("setAnnotationCacheDirect:")]
		void SetAnnotationCacheDirect (NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> annotationCache);

		// PSPDFUndoSupport protocol support

		[Static]
		[Export ("keysForValuesToObserveForUndo")]
		NSSet<NSString> GetKeysForValuesToObserveForUndo ();

		[Static]
		[Export ("localizedUndoActionNameForKey:")]
		[return: NullAllowed]
		string LocalizedUndoActionName (string key);

		[Static]
		[Export ("undoCoalescingForKey:")]
		PSPDFUndoCoalescing GetUndoCoalescing (string key);
	}

	[BaseType (typeof (PSPDFFileDataProvider))]
	interface PSPDFCoordinatedFileDataProvider : PSPDFCoordinatedFileDataProviding, INSFilePresenter {

		// Inine to avoid compiler warnings

		[Export ("additionalOperationsSupported"), New]
		new PSPDFDataProvidingAdditionalOperations AdditionalOperationsSupported { get; }

		[NullAllowed, Export ("data"), New]
		new NSData Data { get; }

		[NullAllowed, Export ("fileURL"), New]
		new NSUrl FileUrl { get; }

		[Export ("size"), New]
		new ulong Size { get; }

		[Export ("UID"), New]
		new string Uid { get; }

		[NullAllowed, Export ("signature", ArgumentSemantic.Strong), New]
		new NSData Signature { get; set; }
	}

	interface IPSPDFCoordinatedFileDataProviding { }

	[Protocol]
	interface PSPDFCoordinatedFileDataProviding : PSPDFFileDataProviding {

		[Abstract]
		[Export ("filePresenter")]
		NSFilePresenter FilePresenter { get; }

		[Abstract]
		[NullAllowed, Export ("coordinationDelegate", ArgumentSemantic.Weak)]
		IPSPDFFileCoordinationDelegate CoordinationDelegate { get; set; }
	}

	delegate nint PSPDFCryptoInputStreamDecryptionHandler (PSPDFCryptoInputStream superStream, IntPtr buffer, nint len);

	[BaseType (typeof (NSInputStream))]
	interface PSPDFCryptoInputStream {

		[Export ("initWithInputStream:decryptionBlock:")]
		IntPtr Constructor (NSInputStream stream, [NullAllowed] PSPDFCryptoInputStreamDecryptionHandler decryptionHandler);

		[Export ("decryptionBlock", ArgumentSemantic.Copy)]
		PSPDFCryptoInputStreamDecryptionHandler DecryptionHandler { get; set; }
	}

	delegate NSData PSPDFCryptoOutputStreamEncryptionHandler (PSPDFCryptoOutputStream stream, IntPtr buffer, nuint len);

	[BaseType (typeof (NSOutputStream))]
	interface PSPDFCryptoOutputStream {

		[Export ("initWithOutputStream:encryptionBlock:")]
		IntPtr Constructor (NSOutputStream stream, PSPDFCryptoOutputStreamEncryptionHandler encryptionHandler);

		[Export ("encryptionBlock", ArgumentSemantic.Copy)]
		PSPDFCryptoOutputStreamEncryptionHandler EncryptionHandler { get; set; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFCryptor {

		[Export ("keyFromPassphrase:salt:")]
		NSData GetKey (string passphrase, string salt);

		[Export ("legacyKeyFromPassphrase:salt:")]
		NSData GetLegacyKey (string passphrase, string salt);

		[Export ("encryptFromURL:toURL:key:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, [NullAllowed] out NSError error);

		[Export ("decryptFromURL:toURL:key:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, NSData key, [NullAllowed] out NSError error);

		[Export ("encryptFromURL:toURL:passphrase:error:")]
		bool Encrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, [NullAllowed] out NSError error);

		[Export ("decryptFromURL:toURL:passphrase:error:")]
		bool Decrypt (NSUrl sourceUrl, NSUrl targetUrl, string passphrase, [NullAllowed] out NSError error);

		[NullAllowed, Export ("fileManager", ArgumentSemantic.Strong)]
		IPSPDFFileManager FileManager { get; set; }
	}

	interface IPSPDFDatabaseEncryptionProvider { }

	[Protocol]
	interface PSPDFDatabaseEncryptionProvider {

		[Abstract]
		[Export ("encryptDatabase:withKey:")]
		bool EncryptDatabase (IntPtr db, NSData keyData);

		[Abstract]
		[Export ("reEncryptDatabase:withKey:")]
		unsafe bool ReEncryptDatabase (IntPtr db, NSData keyData);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDataContainerProvider : PSPDFDataProviding {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSData data);
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

	interface IPSPDFDataProviding { }

	[Protocol]
	interface PSPDFDataProviding : INSSecureCoding {

		[Abstract]
		[NullAllowed, Export ("data")]
		NSData Data { get; }

		[Abstract]
		[Export ("size")]
		ulong Size { get; }

		[Abstract]
		[Export ("UID")]
		string Uid { get; }

		[Abstract]
		[Export ("additionalOperationsSupported")]
		PSPDFDataProvidingAdditionalOperations AdditionalOperationsSupported { get; }

		[Abstract]
		[Export ("readDataWithSize:atOffset:")]
		[return: NullAllowed]
		NSData ReadData (ulong size, ulong offset);

		[Abstract]
		[NullAllowed, Export ("signature", ArgumentSemantic.Strong)]
		NSData Signature { get; set; }

		[NullAllowed, Export ("progress")]
		NSProgress GetProgress ();

		[Export ("createDataSinkWithOptions:error:")]
		[return: NullAllowed]
		IPSPDFDataSink CreateDataSink (PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("replaceWithDataSink:")]
		bool Replace (IPSPDFDataSink replacementDataSink);

		[Export ("canWrite")]
		bool GetCanWrite ();

		[Export ("deleteDataWithError:")]
		bool DeleteData ([NullAllowed] out NSError error);

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("useDiskCache")]
		bool UseDiskCache ();
	}

	interface IPSPDFDataSink { }

	[Protocol]
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

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDigitalSignatureReference {

		[Export ("transformMethod")]
		PSPDFDigitalSignatureReferenceTransformMethod TransformMethod { get; }

		[NullAllowed, Export ("transformParams", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> TransformParams { get; }

		[NullAllowed, Export ("digestMethod")]
		string DigestMethod { get; }

		[NullAllowed, Export ("digestValue")]
		string DigestValue { get; }

		[Export ("digestLocation")]
		NSRange DigestLocation { get; }
	}

	delegate NSData PSPDFDiskCacheEncryptionHelper (PSPDFRenderRequest request, NSData data);
	delegate NSData PSPDFDiskCacheDecryptionHelper (PSPDFRenderRequest request, NSData encryptedData);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDiskCache {

		[Export ("initWithCacheDirectory:fileFormat:settings:")]
		[DesignatedInitializer]
		IntPtr Constructor (string cacheDirectory, PSPDFDiskCacheFileFormat fileFormat, PSPDFKitGlobal settings);

		[Export ("allowedDiskSpace")]
		long AllowedDiskSpace { get; set; }

		[Export ("usedDiskSpace")]
		long UsedDiskSpace { get; }

		[Export ("cacheDirectory")]
		string CacheDirectory { get; set; }

		[Export ("fileFormat", ArgumentSemantic.Assign)]
		PSPDFDiskCacheFileFormat FileFormat { get; set; }

		[Export ("compression")]
		nfloat Compression { get; set; }

		[NullAllowed, Export ("encryptionHelper", ArgumentSemantic.Copy)]
		PSPDFDiskCacheEncryptionHelper EncryptionHelper { get; set; }

		[NullAllowed, Export ("decryptionHelper", ArgumentSemantic.Copy)]
		PSPDFDiskCacheDecryptionHelper DecryptionHelper { get; set; }
	}

	[Static]
	interface PSPDFDocumentSaveOptionsKeys {

		[Field ("PSPDFDocumentSaveOptionForceRewrite", PSPDFKitLibraryPath.LibraryPath)]
		NSString ForceRewriteKey { get; }

		[Field ("PSPDFDocumentSaveOptionSecurityOptions", PSPDFKitLibraryPath.LibraryPath)]
		NSString SecurityOptionsKey { get; }
	}

	[StrongDictionary ("PSPDFDocumentSaveOptionsKeys")]
	interface PSPDFDocumentSaveOptions {

		bool ForceRewrite { get; set; }
		PSPDFDocumentSecurityOptions SecurityOptions { get; set; }
	}

	[StrongDictionary ("PSPDFDocument")]
	interface PSPDFDocumentAnnotationWriteOptions {

	}

	interface PSPDFDocumentUnderlyingFileChangedNotificationEventArgs {

		[Export ("PSPDFDocumentUnderlyingFileURLKey")]
		NSUrl FileUrl { get; }

		[Export ("PSPDFDocumentUnderlyingFileWillBeDeletedKey")]
		bool FileWillBeDeleted { get; }
	}

	delegate void PSPDFDocumentSaveHandler (NSError error, PSPDFAnnotation [] savedAnnotations);
	delegate void PSPDFDocumentGetAnnotationsByDetectingLinkTypesProgressHandler (PSPDFAnnotation [] annotations, nuint page, ref bool stop);

	[BaseType (typeof (NSObject))]
	interface PSPDFDocument : PSPDFDocumentProviderDelegate, PSPDFOverridable, INSCopying, INSSecureCoding, PSPDFFileCoordinationDelegate {

		[Field ("PSPDFDocumentUnderlyingFileChangedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFDocumentUnderlyingFileChangedNotificationEventArgs))]
		NSString UnderlyingFileChangedNotification { get; }

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("initWithDataProviders:")]
		IntPtr Constructor (IPSPDFDataProviding [] dataProviders);

		[Export ("initWithDataProviders:loadCheckpointIfAvailable:")]
		[DesignatedInitializer]
		IntPtr Constructor (IPSPDFDataProviding [] dataProviders, bool loadCheckpoint);

		[Export ("documentByAppendingDataProviders:")]
		PSPDFDocument GetDocumentByAppendingDataProviders (IPSPDFDataProviding [] dataProviders);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentDelegate Delegate { get; set; }

		[Export ("valid")]
		bool Valid { [Bind ("isValid")] get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }

		[Export ("isEqualToDocument:")]
		bool IsEqualTo (PSPDFDocument otherDocument);

		[Export ("features")]
		PSPDFDocumentFeatures Features { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("fileURLs")]
		NSUrl [] FileUrls { get; }

		[Export ("filePresenters")]
		NSFilePresenter [] FilePresenters { get; }

		[Export ("progress")]
		NSProgress Progress { get; }

		[NullAllowed, Export ("originalFile", ArgumentSemantic.Strong)]
		PSPDFFile OriginalFile { get; set; }

		[Export ("pathForPageAtIndex:")]
		[return: NullAllowed]
		NSUrl GetPathForPage (nuint pageIndex);

		[Export ("fileIndexForPageAtIndex:")]
		nint GetFileIndexForPage (nuint pageIndex);

		[Export ("URLForFileIndex:")]
		[return: NullAllowed]
		NSUrl GetUrlForFile (nuint fileIndex);

		[NullAllowed, Export ("fileName")]
		string FileName { get; }

		[Export ("fileNameForPageAtIndex:")]
		string GetFileNameForPage (nuint pageIndex);

		[Export ("deleteFiles:")]
		bool DeleteFiles ([NullAllowed] out NSError error);

		[NullAllowed, Export ("data")]
		NSData Data { get; }

		[Export ("dataArray")]
		NSData [] DataArray { get; }

		[Export ("fileNamesWithDataDictionary")]
		NSDictionary<NSString, NSData> FileNamesWithDataDictionary { get; }

		[Export ("dataProviders")]
		IPSPDFDataProviding [] DataProviders { get; }

		[Export ("documentProviders")]
		PSPDFDocumentProvider [] DocumentProviders { get; }

		[Export ("documentProviderForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFDocumentProvider GetDocumentProviderForPage (nuint pageIndex);

		[Export ("pageOffsetForDocumentProvider:")]
		nuint GetPageOffset (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("documentId", ArgumentSemantic.Copy)]
		NSData DocumentId { get; }

		[NullAllowed, Export ("documentIdString")]
		string DocumentIdString { get; }

		[Export ("UID"), NullAllowed]
		string Uid { get; set; }

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageInfoForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageInfo GetPageInfo (nuint pageIndex);

		[Export ("saveWithOptions:error:")]
		bool Save ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Wrap ("Save (saveOptions?.Dictionary, out error)")]
		bool Save ([NullAllowed] PSPDFDocumentSaveOptions saveOptions, [NullAllowed] out NSError error);

		[Async (ResultTypeName = "PSPDFDocumentSaveHandlerResult")]
		[Export ("saveWithOptions:completionHandler:")]
		void Save ([NullAllowed] NSDictionary options, [NullAllowed] PSPDFDocumentSaveHandler completionHandler);

		[Async (ResultTypeName = "PSPDFDocumentSaveHandlerResult")]
		[Wrap ("Save (saveOptions?.Dictionary, completionHandler)")]
		void Save ([NullAllowed] PSPDFDocumentSaveOptions saveOptions, [NullAllowed] PSPDFDocumentSaveHandler completionHandler);

		[Export ("checkpointer")]
		PSPDFDocumentCheckpointer Checkpointer { get; }

		[NullAllowed, Export ("outline")]
		PSPDFOutlineElement Outline { get; }

		// PSPDFDocument (Caching) Category

		[Export ("clearCache")]
		void ClearCache ();

		[Export ("fillCache")]
		void FillCache ();

		[Export ("dataDirectory")]
		string DataDirectory { get; set; }

		[Export ("ensureDataDirectoryExistsWithError:")]
		bool EnsureDataDirectoryExists ([NullAllowed] out NSError error);

		[Export ("useDiskCache")]
		bool UseDiskCache { get; set; }

		// PSPDFDocument (Security) Category

		[Export ("unlockWithPassword:")]
		bool Unlock (string password);

		[Export ("lock")]
		void Lock ();

		[Export ("isEncrypted")]
		bool IsEncrypted { get; }

		[Export ("isLocked")]
		bool IsLocked { get; }

		[Export ("isUnlockedWithFullAccess")]
		bool IsUnlockedWithFullAccess { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("allowAnnotationChanges")]
		bool AllowAnnotationChanges { get; }

		// PSPDFDocument (Bookmarks) Category

		[Export ("bookmarksEnabled")]
		bool BookmarksEnabled { [Bind ("areBookmarksEnabled")] get; set; }

		[NullAllowed, Export ("bookmarkManager")]
		PSPDFBookmarkManager BookmarkManager { get; }

		[Export ("bookmarks")]
		PSPDFBookmark [] Bookmarks { get; }

		// PSPDFDocument (PageLabels) Category

		[Export ("pageLabelsEnabled")]
		bool PageLabelsEnabled { [Bind ("arePageLabelsEnabled")] get; set; }

		[Export ("pageLabelForPageAtIndex:substituteWithPlainLabel:")]
		[return: NullAllowed]
		string GetPageLabel (nuint pageIndex, bool substitute);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		// PSPDFDocument (Forms) Category

		[Export ("formsEnabled")]
		bool FormsEnabled { [Bind ("areFormsEnabled")] get; set; }

		[Export ("javaScriptStatus", ArgumentSemantic.Assign)]
		PSPDFJavaScriptStatus JavaScriptStatus { get; set; }

		[Export ("isJavaScriptStatusEnabled")]
		bool IsJavaScriptStatusEnabled { get; }

		[NullAllowed, Export ("formParser")]
		PSPDFFormParser FormParser { get; }

		// PSPDFDocument (EmbeddedFiles) Category

		[Export ("allEmbeddedFiles")]
		PSPDFEmbeddedFile [] AllEmbeddedFiles { get; }

		[Export ("annotationsEnabled")]
		bool AnnotationsEnabled { [Bind ("areAnnotationsEnabled")] get; set; }

		[Export ("addAnnotations:options:")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("removeAnnotations:options:")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		bool RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("annotationsForPageAtIndex:type:")]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex, PSPDFAnnotationType type);

		[Export ("allAnnotationsOfType:")]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAllAnnotations (PSPDFAnnotationType annotationType);

		[Export ("containsAnnotations")]
		bool ContainsAnnotations { get; }

		// PSPDFDocument (AnnotationSaving) Category

		[Field ("PSPDFDocumentWillSaveAnnotationsNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString WillSaveAnnotationsNotification { get; }

		[Export ("canEmbedAnnotations")]
		bool CanEmbedAnnotations { get; }

		[Export ("canSaveAnnotations")]
		bool CanSaveAnnotations { get; }

		[Export ("annotationSaveMode", ArgumentSemantic.Assign)]
		PSPDFAnnotationSaveMode AnnotationSaveMode { get; set; }

		[Field ("PSPDFDocumentDefaultAnnotationUsernameKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DefaultAnnotationUsernameKey { get; }

		[NullAllowed, Export ("defaultAnnotationUsername")]
		string DefaultAnnotationUsername { get; set; }

		[Field ("PSPDFAnnotationWriteOptionsGenerateAppearanceStreamForTypeKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString WriteOptionsGenerateAppearanceStreamForTypeKey { get; }

		[NullAllowed, Export ("annotationWritingOptions", ArgumentSemantic.Copy)]
		NSDictionary WeakAnnotationWritingOptions { get; set; }

		[Wrap ("WeakAnnotationWritingOptions")]
		PSPDFDocumentAnnotationWriteOptions AnnotationWritingOptions { get; set; }

		[Export ("hasDirtyAnnotations")]
		bool HasDirtyAnnotations { get; }

		// PSPDFDocument (Rendering) Category

		[Export ("imageForPageAtIndex:size:clippedToRect:annotations:options:error:")]
		[return: NullAllowed]
		UIImage GetImage (nuint pageIndex, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] out NSError error);

		[Export ("renderPageAtIndex:context:size:clippedToRect:annotations:options:error:")]
		bool RenderPage (nuint pageIndex, CGContext context, CGSize size, CGRect clipRect, [NullAllowed] PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] out NSError error);

		[Export ("setRenderOptions:type:")]
		void SetRenderOptions ([NullAllowed] NSDictionary<NSString, NSObject> options, PSPDFRenderType type);

		[Export ("updateRenderOptions:type:")]
		void UpdateRenderOptions (NSDictionary<NSString, NSObject> options, PSPDFRenderType type);

		[Export ("renderOptionsForType:context:")]
		NSDictionary<NSString, NSObject> GetRenderOptions (PSPDFRenderType type, [NullAllowed] NSObject context);

		[Export ("renderAnnotationTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType RenderAnnotationTypes { get; set; }

		// PSPDFDocument (Metadata) Category

		[NullAllowed, Export ("title")]
		string Title { get; set; }

		[Export ("titleLoaded")]
		bool TitleLoaded { [Bind ("isTitleLoaded")] get; }

#if __IOS__
		[Export ("searchableItemAttributeSetWithThumbnail:")]
		[return: NullAllowed]
		CSSearchableItemAttributeSet GetSearchableItemAttributeSet (bool renderThumbnail);
#endif
		// PSPDFDocument (SubclassingHooks) Category

		[Export ("overrideClass:withClass:")]
		void OverrideClass (Class builtinClass, Class subclass);

		[Wrap ("OverrideClass (builtinType == null ? null : new Class (builtinType), subType == null ? null : new Class (subType))")]
		void OverrideClass (Type builtinType, Type subType);

		[Export ("didCreateDocumentProvider:")]
		PSPDFDocumentProvider DidCreateDocumentProvider (PSPDFDocumentProvider documentProvider);

		[NullAllowed, Export ("didCreateDocumentProviderBlock", ArgumentSemantic.Copy)]
		Action<PSPDFDocumentProvider> DidCreateDocumentProviderHandler { get; set; }

		[return: NullAllowed]
		[Export ("fileNameForIndex:")]
		string GetFileName (nuint fileIndex);

		// PSPDFDocument (Advanced) Category

		[Export ("undoEnabled")]
		bool UndoEnabled { [Bind ("isUndoEnabled")] get; set; }

		[NullAllowed, Export ("undoController")]
		PSPDFUndoController UndoController { get; }

		[Export ("relativePageIndexForPageAtIndex:")]
		nuint GetRelativePageIndex (nuint pageIndex);

		[Export ("pageBinding", ArgumentSemantic.Assign)]
		PSPDFPageBinding PageBinding { get; set; }

		[Export ("pspdfkit")]
		PSPDFKitGlobal PSPdfKit { get; }

		// PSPDFDocument (DataDetection) Category

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsByDetectingLinkTypes:forPagesAtIndexes:options:progress:error:")]
		[return: NullAllowed]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> GetAnnotationsByDetectingLinkTypes (PSPDFTextCheckingType linkTypes, NSIndexSet indexes, [NullAllowed] NSDictionary<NSString, NSDictionary<NSNumber, NSArray<PSPDFAnnotation>>> options, [NullAllowed] PSPDFDocumentGetAnnotationsByDetectingLinkTypesProgressHandler progressHandler, [NullAllowed] out NSError error);

		// PSPDFDocument (TextParser) Category

		[Export ("watermarkFilterEnabled")]
		bool WatermarkFilterEnabled { [Bind ("isWatermarkFilterEnabled")] get; set; }

		[Export ("textParserForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFTextParser GetTextParser (nuint pageIndex);

		// PSPDFDocument (ObjectFinder) Category

		[Advice ("Options parameter comes from 'PSPDFObjectsKeys'.")]
		[Export ("objectsAtPDFPoint:pageIndex:options:")]
		NSDictionary<NSString, NSObject> GetObjectsAtPdfPoint (CGPoint pdfPoint, nuint pageIndex, [NullAllowed] NSDictionary<NSString, NSNumber> options);

		[Advice ("Options parameter comes from 'PSPDFObjectsKeys'.")]
		[Export ("objectsAtPDFRect:pageIndex:options:")]
		NSDictionary<NSString, NSObject> GetObjectsAtPdfRect (CGRect pdfRect, nuint pageIndex, [NullAllowed] NSDictionary<NSString, NSNumber> options);

		// PSPDFAnnotation (InstantJSON) Category

		[Export ("applyInstantJSONFromDataProvider:toDocumentProvider:error:")]
		bool ApplyInstantJson (IPSPDFDataProviding dataProvider, PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		[Export ("generateInstantJSONFromDocumentProvider:error:")]
		[return: NullAllowed]
		NSData GenerateInstantJson (PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);

		// PSPDFDocument (JavaScript)

		[Export ("loadDocumentLevelJavaScriptActionsWithError:")]
		bool LoadDocumentLevelJavaScriptActions ([NullAllowed] out NSError error);
	}

	[Static]
	interface PSPDFObjectsKeys {

		[Field ("PSPDFObjectsGlyphsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString GlyphsKey { get; }

		[Field ("PSPDFObjectsWordsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString WordsKey { get; }

		[Field ("PSPDFObjectsTextKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextKey { get; }

		[Field ("PSPDFObjectsTextBlocksKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextBlocksKey { get; }

		[Field ("PSPDFObjectsImagesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ImagesKey { get; }

		[Field ("PSPDFObjectsAnnotationsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationsKey { get; }

		[Field ("PSPDFObjectsIgnoreLargeTextBlocksKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString IgnoreLargeTextBlocksKey { get; }

		[Field ("PSPDFObjectsAnnotationTypesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFObjectsAnnotationPageBoundsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationPageBoundsKey { get; }

		[Field ("PSPDFObjectsPageZoomLevelKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageZoomLevelKey { get; }

		[Field ("PSPDFObjectsAnnotationIncludedGroupedKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationIncludedGroupedKey { get; }

		[Field ("PSPDFObjectsSmartSortKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SmartSortKey { get; }

		[Field ("PSPDFObjectMinDiameterKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MinDiameterKey { get; }

		[Field ("PSPDFObjectsTextFlowKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextFlowKey { get; }

		[Field ("PSPDFObjectsFindFirstOnlyKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FindFirstOnlyKey { get; }

		[Field ("PSPDFObjectsTestIntersectionKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TestIntersectionKey { get; }
	}

	interface PSPDFDocumentCheckpointSavedNotificationEventArgs {

		[Export ("PSPDFDocumentCheckpointSavedNotificationSucessKey")]
		bool Success { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentCheckpointer {

		[Field ("PSPDFDocumentCheckpointSavedNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFDocumentCheckpointSavedNotificationEventArgs))]
		NSString SavedNotification { get; }

		[Export ("strategy", ArgumentSemantic.Assign)]
		PSPDFDocumentCheckpointingStrategy Strategy { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("checkpointExists")]
		bool CheckpointExists { get; }

		[Async]
		[Export ("saveCheckpointWithCompletionHandler:")]
		void SaveCheckpoint ([NullAllowed] Action<bool, NSError> completionHandler);

		[Async]
		[Export ("deleteCheckpointWithCompletionHandler:")]
		void DeleteCheckpoint ([NullAllowed] Action<bool, NSError> completionHandler);
	}

	interface IPSPDFDocumentDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentDelegate {

		[Export ("pdfDocument:resolveCustomAnnotationPathToken:")]
		string ResolveCustomAnnotationPathToken (PSPDFDocument document, string pathToken);

		[Export ("pdfDocumentDidSave:")]
		void DidSave (PSPDFDocument document);

		[Export ("pdfDocument:saveDidFailWithError:")]
		void SaveDidFail (PSPDFDocument document, NSError error);
	}

	interface IPSPDFDocumentEditorDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditorDelegate {

		[Abstract]
		[Export ("documentEditorRequestsFullReload:")]
		void RequestsFullReload (PSPDFDocumentEditor editor);

		[Export ("documentEditor:didPerformChanges:")]
		void DidPerformChanges (PSPDFDocumentEditor editor, PSPDFEditingChange [] changes);
	}

	delegate void PSPDFDocumentEditorSaveHandler ([NullAllowed] PSPDFDocument document, [NullAllowed] NSError error);
	delegate void PSPDFDocumentEditorImportHandler ([NullAllowed] PSPDFEditingChange [] changes, [NullAllowed] NSError error);
	delegate void PSPDFDocumentEditorGroupDatesHandler ([BlockCallback] PSPDFDocumentEditorGroupDatesHandlerCompletion response);
	delegate void PSPDFDocumentEditorGroupDatesHandlerCompletion (bool completed);

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentEditor {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("securityOptions", ArgumentSemantic.Strong)]
		PSPDFDocumentSecurityOptions SecurityOptions { get; set; }

		[Export ("addDelegate:")]
		void AddDelegate (IPSPDFDocumentEditorDelegate @delegate);

		[Export ("removeDelegate:")]
		bool RemoveDelegate (IPSPDFDocumentEditorDelegate @delegate);

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageSizeForPageAtIndex:")]
		CGSize GetPageSize (nuint pageIndex);

		[Export ("addPagesInRange:withConfiguration:")]
		PSPDFEditingChange [] AddPages (NSRange range, PSPDFNewPageConfiguration configuration);

		[Export ("groupUpdates:")]
		void GroupUpdates (PSPDFDocumentEditorGroupDatesHandler updateHandler);

		[Export ("groupUpdates:completion:")]
		void GroupUpdates (PSPDFDocumentEditorGroupDatesHandler updateHandler, [NullAllowed] Action<PSPDFEditingChange []> completionHandler);

		[Export ("movePages:to:")]
		PSPDFEditingChange [] MovePages (NSIndexSet pageIndexes, nuint destination);

		[Export ("removePages:")]
		PSPDFEditingChange [] RemovePages (NSIndexSet pageIndexes);

		[Export ("duplicatePages:")]
		PSPDFEditingChange [] DuplicatePages (NSIndexSet pageIndexes);

		[Export ("rotatePages:rotation:")]
		PSPDFEditingChange [] RotatePages (NSIndexSet pageIndexes, nint rotation);

		[NullAllowed, Export ("undo")]
		PSPDFEditingChange [] Undo ();

		[NullAllowed, Export ("redo")]
		PSPDFEditingChange [] Redo ();

		[Export ("canRedo")]
		bool CanRedo { get; }

		[Export ("canUndo")]
		bool CanUndo { get; }

		[Export ("reset")]
		void Reset ();

		[Export ("canSave")]
		bool CanSave { get; }

		[Async]
		[Export ("saveWithCompletionBlock:")]
		void Save ([NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Async]
		[Export ("saveToPath:withCompletionBlock:")]
		void Save (string path, [NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Async]
		[Export ("exportPages:toPath:withCompletionBlock:")]
		void ExportPages (NSIndexSet pageIndexes, string path, [NullAllowed] PSPDFDocumentEditorSaveHandler handler);

		[Export ("importPagesTo:fromDocument:withCompletionBlock:queue:")]
		NSProgress ImportPages (nuint index, PSPDFDocument sourceDocument, [NullAllowed] PSPDFDocumentEditorImportHandler handler, [NullAllowed] DispatchQueue queue);

		[Export ("imageForPageAtIndex:size:scale:")]
		[return: NullAllowed]
		UIImage GetImage (nuint pageIndex, CGSize size, nfloat scale);
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFDocumentEditorConfiguration {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFDocumentEditorConfiguration DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFDocumentEditorConfigurationBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFDocumentEditorConfiguration FromConfigurationBuilder ([NullAllowed] Action<PSPDFDocumentEditorConfigurationBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFDocumentEditorConfiguration ConfigurationUpdated ([NullAllowed] Action<PSPDFDocumentEditorConfigurationBuilder> builderHandler);


		[Export ("pageTemplates")]
		PSPDFPageTemplate [] PageTemplates { get; }

		[Export ("currentDocumentPageSize")]
		PSPDFPageSize CurrentDocumentPageSize { get; }

		[Export ("pageSizes")]
		PSPDFPageSize [] PageSizes { get; }

		[Export ("currentDocumentDirectory")]
		PSPDFDirectory CurrentDocumentDirectory { get; }

		[Export ("saveDirectories")]
		PSPDFDirectory [] SaveDirectories { get; }

		[Export ("compressions")]
		PSPDFCompression [] Compressions { get; }

		[Export ("selectedTemplate")]
		PSPDFPageTemplate SelectedTemplate { get; }

		[Export ("selectedPageSize")]
		PSPDFPageSize SelectedPageSize { get; }

		[Export ("selectedOrientation")]
		PSPDFDocumentOrientation SelectedOrientation { get; }

		[Export ("selectedColor")]
		UIColor SelectedColor { get; }

		[Export ("selectedImage")]
		UIImage SelectedImage { get; }

		[Export ("selectedImagePageSize")]
		PSPDFPageSize SelectedImagePageSize { get; }

		[Export ("selectedCompression")]
		PSPDFCompression SelectedCompression { get; }

		[Export ("selectedSaveDirectory")]
		PSPDFDirectory SelectedSaveDirectory { get; }

		[Export ("userFacingCompressionEnabled")]
		bool UserFacingCompressionEnabled { get; }

		[Export ("allowExternalFileSource")]
		bool AllowExternalFileSource { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFPageSize {

		[Static]
		[Export ("size:name:")]
		PSPDFPageSize Create (CGSize size, string name);

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
		CGSize GetSize (PSPDFDocumentOrientation orientation);
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

		[NullAllowed, Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }
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

		[Export ("compression")]
		nfloat Compression { get; }

		[Export ("name")]
		string Name { get; }

		[Export ("localizedName")]
		string LocalizedName { get; }
	}

	interface IPSPDFDocumentEditorConfigurationConfigurable { }

	[Protocol]
	interface PSPDFDocumentEditorConfigurationConfigurable {

		// This ctor needs to be manually added to any class implementing this protocol
		//[Abstract] // TODO: add this to classes that implement this
		//[Export ("initWithDocumentEditorConfiguration:")]
		//IntPtr Constructor (PSPDFDocumentEditorConfiguration configuration);

		[Abstract]
		[Export ("documentEditorConfiguration")]
		PSPDFDocumentEditorConfiguration DocumentEditorConfiguration { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentPDFMetadata {

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("allInfoKeys")]
		string [] AllInfoKeys { get; }

		[Export ("objectForInfoDictionaryKey:")]
		[return: NullAllowed]
		NSObject GetObject (string key);

		[Export ("setObject:forInfoDictionaryKey:")]
		void SetObject ([NullAllowed] NSObject @object, string key);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProvider {

		[NullAllowed, Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("dataRepresentationWithError:")]
		[return: NullAllowed]
		NSData GetDataRepresentation ([NullAllowed] out NSError error);

		[Export ("fileSize")]
		ulong FileSize { get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDocumentProviderDelegate Delegate { get; set; }

		[Export ("pageInfoForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFPageInfo GetPageInfo (nuint pageIndex);

		[Export ("pageCount")]
		nuint PageCount { get; }

		[Export ("pageOffsetForDocument")]
		nuint PageOffsetForDocument { get; }

		[NullAllowed, Export ("password")]
		string Password { get; }

		[NullAllowed, Export ("contentSignature", ArgumentSemantic.Copy)]
		NSData ContentSignature { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("permissionsLevel")]
		PSPDFDocumentPermissionsLevel PermissionsLevel { get; }

		[Export ("isEncrypted")]
		bool IsEncrypted { get; }

		[Export ("isEncryptedWithUserPassword")]
		bool IsEncryptedWithUserPassword { get; }

		[Export ("isUnlockedWithFullAccess")]
		bool IsUnlockedWithFullAccess { get; }

		[Export ("isUnlockedWithUserPassword")]
		bool IsUnlockedWithUserPassword { get; }

		[Export ("isLocked")]
		bool IsLocked { get; }

		[Export ("canEmbedAnnotations")]
		bool CanEmbedAnnotations { get; }

		[Export ("allowAnnotationChanges")]
		bool AllowAnnotationChanges { get; }

		[NullAllowed, Export ("fileId", ArgumentSemantic.Copy)]
		NSData FileId { get; }

		[Export ("title"), NullAllowed]
		string Title { get; }

		[Export ("textParserForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFTextParser GetTextParser (nuint pageIndex);

		[Export ("outlineParser")]
		PSPDFOutlineParser OutlineParser { get; }

		[NullAllowed, Export ("formParser")]
		PSPDFFormParser FormParser { get; }

		[Export ("annotationManager")]
		PSPDFAnnotationManager AnnotationManager { get; }

		[Export ("labelParser")]
		PSPDFLabelParser LabelParser { get; }

		[Export ("metadata", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSObject> Metadata { get; }

		[NullAllowed, Export ("XMPMetadata")]
		string XmpMetadata { get; }

		[Export ("setRotation:forPageAtIndex:")]
		void SetRotation (nuint rotation, nuint pageIndex);

		[Export ("resolveNamedDestination:")]
		nuint ResolveNamedDestination (string namedDestination);

		// PSPDFDocumentProvider (SubclassingHooks) Category

		[Advice ("You shouldn't call this method directly, use the high-level save method in 'PSPDFDocument' instead.")]
		[Export ("saveAnnotationsWithOptions:error:")]
		bool SaveAnnotations ([NullAllowed] NSDictionary<NSString, NSObject> options, [NullAllowed] out NSError error);

		[Export ("resolveTokenizedPath:alwaysLocal:")]
		string ResolveTokenizedPath (string path, bool alwaysLocal);
	}

	interface IPSPDFDocumentProviderDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentProviderDelegate {

		[Export ("documentProvider:shouldAppendData:")]
		bool ShouldAppendData (PSPDFDocumentProvider documentProvider, NSData data);

		[Export ("documentProvider:didAppendData:")]
		void DidAppendData (PSPDFDocumentProvider documentProvider, NSData data);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentSecurityOptions {

		[Field ("PSPDFDocumentSecurityOptionsKeyLengthAutomatic", PSPDFKitLibraryPath.LibraryPath)]
		nuint KeyLengthAutomatic { get; }

		[Export ("initWithOwnerPassword:userPassword:error:")]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:error:")]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:permissions:error:")]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, PSPDFDocumentPermissions documentPermissions, [NullAllowed] out NSError error);

		[Export ("initWithOwnerPassword:userPassword:keyLength:permissions:encryptionAlgorithm:error:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string ownerPassword, [NullAllowed] string userPassword, nuint keyLength, PSPDFDocumentPermissions documentPermissions, PSPDFDocumentEncryptionAlgorithm encryptionAlgorithm, [NullAllowed] out NSError error);

		[NullAllowed, Export ("ownerPassword")]
		string OwnerPassword { get; }

		[NullAllowed, Export ("userPassword")]
		string UserPassword { get; }

		[Export ("keyLength")]
		nuint KeyLength { get; }

		[Export ("permissions")]
		PSPDFDocumentPermissions Permissions { get; }

		[Export ("encryptionAlgorithm")]
		PSPDFDocumentEncryptionAlgorithm EncryptionAlgorithm { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentXMPMetadata {

		[Export ("initWithDocument:")]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Advice ("Use 'PSPDFXMPKeys'.")]
		[Export ("stringForXMPKey:namespace:")]
		[return: NullAllowed]
		string GetString (NSString xmpKey, string ns);

		[Advice ("Use 'PSPDFXMPKeys'.")]
		[Export ("setString:forXMPKey:namespace:suggestedNamespacePrefix:")]
		void SetString ([NullAllowed] string @string, NSString xmpKey, string ns, string nsPrefix);
	}

	[Static]
	interface PSPDFXMPKeys {

		[Field ("PSPDFXMPPDFNamespace", PSPDFKitLibraryPath.LibraryPath)]
		NSString PdfNamespaceKey { get; }

		[Field ("PSPDFXMPPDFNamespacePrefix", PSPDFKitLibraryPath.LibraryPath)]
		NSString PdfNamespacePrefixKey { get; }

		[Field ("PSPDFXMPDCNamespace", PSPDFKitLibraryPath.LibraryPath)]
		NSString DCNamespaceKey { get; }

		[Field ("PSPDFXMPDCNamespacePrefix", PSPDFKitLibraryPath.LibraryPath)]
		NSString DCNamespacePrefixKey { get; }
	}

	delegate bool PSPDFDownloadManagerObjectsPassingTestPredicate (IPSPDFRemoteContentObject obj, nuint index, ref bool stop);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDownloadManager {

		[Field ("PSPDFDownloadManagerDidStartLoadingTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidStartLoadingTaskNotification { get; }

		[Field ("PSPDFDownloadManagerDidFinishLoadingTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidFinishLoadingTaskNotification { get; }

		[Field ("PSPDFDownloadManagerDidFailToLoadTaskNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidFailToLoadTaskNotification { get; }

		[Export ("numberOfConcurrentDownloads")]
		nuint NumberOfConcurrentDownloads { get; set; }

		[Export ("enableDynamicNumberOfConcurrentDownloads")]
		bool EnableDynamicNumberOfConcurrentDownloads { get; set; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFDownloadManagerDelegate Delegate { get; set; }

		[Export ("shouldFinishLoadingObjectsInBackground")]
		bool ShouldFinishLoadingObjectsInBackground { get; set; }

		[Export ("enqueueObject:")]
		void EnqueueObject (IPSPDFRemoteContentObject @object);

		[Export ("enqueueObject:atFront:")]
		void EnqueueObject (IPSPDFRemoteContentObject @object, bool enqueueAtFront);

		[Export ("enqueueObjects:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects);

		[Export ("enqueueObjects:atFront:")]
		void EnqueueObjects (IPSPDFRemoteContentObject [] objects, bool enqueueAtFront);

		[Export ("cancelObject:")]
		void CancelObject (IPSPDFRemoteContentObject @object);

		[Export ("cancelAllObjects")]
		void CancelAllObjects ();

		[Export ("reachability")]
		PSPDFReachability Reachability { get; }

		[Export ("waitingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] WaitingObjects { get; }

		[Export ("loadingObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] LoadingObjects { get; }

		[Export ("failedObjects", ArgumentSemantic.Copy)]
		IPSPDFRemoteContentObject [] FailedObjects { get; }

		[Export ("objectsPassingTest:")]
		IPSPDFRemoteContentObject [] GetObjectsPassingTest (PSPDFDownloadManagerObjectsPassingTestPredicate predicate);

		[Export ("handlesObject:")]
		bool HandlesObject (IPSPDFRemoteContentObject @object);

		[Export ("stateForObject:")]
		PSPDFDownloadManagerObjectState GetState (IPSPDFRemoteContentObject @object);
	}

	interface IPSPDFDownloadManagerDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFDownloadManagerDelegate {

		[Export ("downloadManager:authenticationChallenge:completionHandler:")]
		void AuthenticationChallenge (PSPDFDownloadManager downloadManager, NSUrlAuthenticationChallenge authenticationChallenge, Action<NSUrlSessionAuthChallengeDisposition, NSUrlCredential> completionHandler);

		[Export ("downloadManager:didChangeObject:")]
		void DidChangeObject (PSPDFDownloadManager downloadManager, IPSPDFRemoteContentObject @object);

		[Export ("downloadManager:reachabilityDidChange:")]
		void ReachabilityDidChange (PSPDFDownloadManager downloadManager, PSPDFReachability reachability);
	}

	interface IPSPDFDownloadManagerPolicy { }

	[Protocol]
	interface PSPDFDownloadManagerPolicy {

		[Abstract]
		[Export ("hasPermissionForNetworkEvent")]
		bool HasPermissionForNetworkEvent { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDrawingContextAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

		[Static]
		[Export ("defaultGenerator", ArgumentSemantic.Strong)]
		PSPDFDrawingContextAppearanceStreamGenerator DefaultGenerator { get; }
	}

	[Category (allowStaticMembers: true)]
	[BaseType (typeof (NSValue))]
	interface NSValue_PSPDFModel {

		[Static]
		[Export ("pspdf_valueWithDrawingPoint:")]
		NSValue FromPSPDFDrawingPoint (PSPDFDrawingPoint point);

		[Export ("pspdf_drawingPointValue")]
		PSPDFDrawingPoint GetPSPDFDrawingPoint ();
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

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFEmbeddedFile {
		
		[Export ("initWithFileURL:fileDescription:")]
		IntPtr Constructor (NSUrl fileUrl, [NullAllowed] string fileDescription);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("fileName")]
		string FileName { get; }

		[Export ("fileSize")]
		ulong FileSize { get; }

		[NullAllowed, Export ("fileDescription")]
		string FileDescription { get; }

		[NullAllowed, Export ("modificationDate")]
		NSDate ModificationDate { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[Export ("fileURLWithError:")]
		[return: NullAllowed]
		NSUrl GetFileUrl ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFEmbeddedGoToAction {

		[Export ("initWithRelativePath:targetRelationship:openInNewWindow:pageIndex:")]
		IntPtr Constructor (string remotePath, PSPDFEmbeddedGoToActionTarget targetRelationship, bool openInNewWindow, nuint pageIndex);

		[Export ("targetRelationship")]
		PSPDFEmbeddedGoToActionTarget TargetRelationship { get; }

		[NullAllowed, Export ("relativePath")]
		string RelativePath { get; }

		[Export ("openInNewWindow")]
		bool OpenInNewWindow { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFFile {

		[Export ("initWithName:URL:data:")]
		[DesignatedInitializer]
		IntPtr Constructor (string fileName, [NullAllowed] NSUrl fileUrl, [NullAllowed] NSData fileData);

		[Export ("fileName")]
		string FileName { get; }

		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }

		[NullAllowed, Export ("fileData")]
		NSData FileData { get; }

		[Export ("mimeType")]
		string MimeType { get; }

		[Export ("fileDataMappedWithError:")]
		[return: NullAllowed]
		NSData GetFileDataMapped ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFileAnnotation {

		[BindAs (typeof (PSPDFFileIconName))]
		[Export ("appearanceName")]
		NSString AppearanceName { get; set; }

		[NullAllowed, Export ("embeddedFile", ArgumentSemantic.Strong)]
		PSPDFEmbeddedFile EmbeddedFile { get; set; }
	}

	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	[DisableDefaultCtor]
	interface PSPDFFileAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, [NullAllowed] NSUrl annotationFileUrl);

		[Export ("autodetectTextLinkTypes", ArgumentSemantic.Assign)]
		PSPDFTextCheckingType AutodetectTextLinkTypes { get; set; }

		[Export ("annotationsForPageAtIndex:"), New]
		[return: NullAllowed]
		PSPDFAnnotation [] GetAnnotations (nuint pageIndex);

		[Export ("addAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("AddAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		PSPDFAnnotation [] AddAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("removeAnnotations:options:")]
		[Advice ("Requires base call if override.")]
		[return: NullAllowed]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, [NullAllowed] NSDictionary options);

		[Wrap ("RemoveAnnotations (annotations, options: annotationOptions?.Dictionary)")]
		PSPDFAnnotation [] RemoveAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationOptions annotationOptions);

		[Export ("clearCache")]
		void ClearCache ();

		// PSPDFFileAnnotationProvider (Advanced) Category

		[Export ("saveableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType SaveableTypes { get; set; }

		[Export ("parsableTypes", ArgumentSemantic.Assign)]
		PSPDFAnnotationType ParsableTypes { get; set; }

		[Export ("annotationsPath")]
		string AnnotationsPath { get; }

		// PSPDFFileAnnotationProvider (SubclassingHooks) Category

		[Export ("parseAnnotationsForPageAtIndex:")]
		[return: NullAllowed]
		PSPDFAnnotation [] ParseAnnotations (nuint pageIndex);

		[Export ("saveAnnotationsWithOptions:error:"), New]
		bool SaveAnnotations ([NullAllowed] NSDictionary options, [NullAllowed] out NSError error);

		[Export ("loadAnnotationsWithError:")]
		[return: NullAllowed]
		NSDictionary<NSNumber, NSArray<PSPDFAnnotation>> LoadAnnotations ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

		[Export ("initWithFileURL:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl fileUrl);

		[Export ("fileURL")]
		NSUrl FileUrl { get; }
	}

	interface IPSPDFFileCoordinationDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFFileCoordinationDelegate {

		[Abstract]
		[Export ("presentedItemDidChangeForDataProvider:")]
		void PresentedItemDidChange (IPSPDFCoordinatedFileDataProviding dataProvider);

		[Abstract]
		[Export ("accommodatePresentedItemDeletionForDataProvider:completionHandler:")]
		void AccommodatePresentedItemDeletion (IPSPDFCoordinatedFileDataProviding dataProvider, Action<NSError> completionHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileDataProvider : PSPDFFileDataProviding {

		[Export ("initWithFileURL:progress:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl fileUrl, [NullAllowed] NSProgress progress);

		[Export ("initWithFileURL:")]
		IntPtr Constructor (NSUrl fileUrl);
	}

	interface IPSPDFFileDataProviding { }

	[Protocol]
	interface PSPDFFileDataProviding : PSPDFDataProviding {

		[Abstract]
		[NullAllowed, Export ("fileURL")]
		NSUrl FileUrl { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFileDataSink : PSPDFDataSink {

		[Export ("initWithFileURL:options:error:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl fileURL, PSPDFDataSinkOptions options, [NullAllowed] out NSError error);

		[Export ("options")]
		PSPDFDataSinkOptions Options { get; }

		[Export ("fileURL")]
		NSUrl FileUrl { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
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

	delegate bool PSPDFFileManagerErrorHandler (NSUrl url, NSError error);

	interface IPSPDFFileManager { }

	[Protocol]
	interface PSPDFFileManager {

		[Export ("usesEncryption")]
		[Abstract]
		bool UsesEncryption { get; }

		[Export ("allowsPolicyEvent:")]
		[Abstract]
		bool AllowsPolicyEvent (string policyEvent);

		[Export ("copyFileToUnencryptedLocationIfRequired:policyEvent:error:")]
		[Abstract]
		[return: NullAllowed]
		NSUrl CopyFileToUnencryptedLocationIfRequired ([NullAllowed] NSUrl fileUrl, string policyEvent, out NSError error);

		[Export ("cleanupIfTemporaryFile:")]
		[Abstract]
		bool CleanupIfTemporaryFile (NSUrl url);

		[Export ("createTemporaryWritableDataProviderWithPrefix:")]
		[return: NullAllowed]
		[Abstract]
		IPSPDFDataProviding CreateTemporaryWritableDataProvider ([NullAllowed] string prefix);

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
		[Abstract]
		[return: NullAllowed]
		string UnencryptedTemporaryDirectory (string uid);

		[Export ("isNativePath:")]
		[Abstract]
		bool IsNativePath ([NullAllowed] string path);

		[Export ("fileExistsAtPath:")]
		[Abstract]
		bool FileExists ([NullAllowed] string path);

		[Export ("fileExistsAtPath:isDirectory:")]
		[Abstract]
		bool FileExists ([NullAllowed] string path, ref bool isDirectory);

		[Export ("fileExistsAtURL:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url);

		[Export ("fileExistsAtURL:isDirectory:")]
		[Abstract]
		bool FileExists ([NullAllowed] NSUrl url, ref bool isDirectory);

		[Export ("createFileAtPath:contents:attributes:")]
		[Abstract]
		bool CreateFile (string path, [NullAllowed] NSData data, [NullAllowed] NSDictionary<NSString, NSObject> attributes);

		[Export ("createDirectoryAtPath:withIntermediateDirectories:attributes:error:")]
		[Abstract]
		bool CreateDirectory (string path, bool createIntermediates, [NullAllowed] NSDictionary<NSString, NSObject> attributes, out NSError error);

		[Abstract]
		[Export ("createDirectoryAtURL:withIntermediateDirectories:attributes:error:")]
		bool CreateDirectory (NSUrl url, bool createIntermediates, [NullAllowed] NSDictionary<NSString, NSObject> attributes, out NSError error);

		[Export ("writeData:toFile:options:error:")]
		[Abstract]
		bool WriteData (NSData data, string path, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("writeData:toURL:options:error:")]
		[Abstract]
		bool WriteData (NSData data, NSUrl fileUrl, NSDataWritingOptions writeOptionsMask, out NSError error);

		[Export ("dataWithContentsOfFile:options:error:")]
		[Abstract]
		NSData GetData (string path, NSDataReadingOptions readOptionsMask, out NSError error);

		[Export ("dataWithContentsOfURL:options:error:")]
		[Abstract]
		NSData GetData (NSUrl fileUrl, NSDataReadingOptions readOptionsMask, out NSError error);

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
		[Abstract]
		[return: NullAllowed]
		NSDictionary<NSString, NSObject> GetAttributesOfFileSystem (string path, out NSError error);

		[Export ("attributesOfItemAtPath:error:")]
		[Abstract]
		[return: NullAllowed]
		NSDictionary<NSString, NSObject> GetAttributesOfItem ([NullAllowed] string path, out NSError error);

		[Export ("isDeletableFileAtPath:")]
		[Abstract]
		bool IsDeletableFile (string path);

		[Export ("isWritableFileAtPath:")]
		[Abstract]
		bool IsWritableFile (string path);

		[Export ("contentsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] GetContentsOfDirectory (string path, out NSError error);

		[Export ("subpathsOfDirectoryAtPath:error:")]
		[Abstract]
		string [] GetSubpathsOfDirectory (string path, out NSError error);

		[Export ("enumeratorAtPath:")]
		[Abstract]
		NSDirectoryEnumerator GetEnumerator (string path);

		[Export ("enumeratorAtURL:includingPropertiesForKeys:options:errorHandler:")]
		[Abstract]
		NSDirectoryEnumerator GetEnumerator (NSUrl url, string [] keys, NSDirectoryEnumerationOptions options, [NullAllowed] PSPDFFileManagerErrorHandler handler);

		[Export ("destinationOfSymbolicLinkAtPath:error:")]
		[Abstract]
		string GetDestinationOfSymbolicLink (string path, out NSError error);

		[Export ("fileSystemRepresentationForPath:")]
		[Abstract]
		IntPtr GetFileSystemRepresentation (string path);

		[Export ("fileHandleForReadingFromURL:error:withBlock:")]
		[Abstract]
		bool GetFileHandleForReading (NSUrl url, out NSError error, Func<NSFileHandle, bool> reader);

		[Export ("fileHandleForWritingToURL:error:withBlock:")]
		[Abstract]
		bool GetFileHandleForWriting (NSUrl url, out NSError error, Func<NSFileHandle, bool> writer);

		[Export ("fileHandleForUpdatingURL:error:withBlock:")]
		[Abstract]
		bool GetFileHandleForUpdating (NSUrl url, out NSError error, Func<NSFileHandle, bool> updater);

		[Abstract]
		[Export ("setUbiquitous:itemAtURL:destinationURL:error:")]
		bool SetUbiquitous (bool flag, NSUrl url, NSUrl destinationUrl, out NSError error);

		[Abstract]
		[Export ("isUbiquitousItemAtURL:")]
		bool IsUbiquitousItem (NSUrl url);

		[Abstract]
		[Export ("startDownloadingUbiquitousItemAtURL:error:")]
		bool StartDownloadingUbiquitousItem (NSUrl url, out NSError error);

		[Abstract]
		[Export ("evictUbiquitousItemAtURL:error:")]
		bool EvictUbiquitousItem (NSUrl url, out NSError error);

		[Abstract]
		[Export ("URLForUbiquityContainerIdentifier:")]
		[return: NullAllowed]
		NSUrl GetUrlForUbiquityContainerIdentifier ([NullAllowed] string containerIdentifier);

		[Abstract]
		[Export ("URLForPublishingUbiquitousItemAtURL:expirationDate:error:")]
		[return: NullAllowed]
		NSUrl GetUrlForPublishingUbiquitousItem (NSUrl url, out NSDate outDate, out NSError error);

		[Abstract]
		[NullAllowed, Export ("ubiquityIdentityToken", ArgumentSemantic.Copy)]
		NSObject UbiquityIdentityToken { get; }

		[Abstract]
		[Export ("performBlockWithoutCoordination:")]
		void PerformHandlerWithoutCoordination (Action handler);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFDefaultFileManager : PSPDFFileManager {

		[Export ("initWithOptions:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] NSDictionary<NSString, NSObject> options);
	}

	[Static]
	interface PSPDFFileManagerOptionKeys {

		[Field ("PSPDFFileManagerOptionCoordinatedAccess", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoordinatedAccessKey { get; }

		[Field ("PSPDFFileManagerOptionFilePresenter", PSPDFKitLibraryPath.LibraryPath)]
		NSString FilePresenterKey { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFilePresenterCoordinator {

		[Static]
		[Export ("sharedCoordinator")]
		PSPDFFilePresenterCoordinator SharedCoordinator { get; }

		[Export ("observeFilePresenter:")]
		void ObserveFilePresenter (NSFilePresenter filePresenter);

		[Export ("unobserveFilePresenter:")]
		void UnobserveFilePresenter (NSFilePresenter filePresenter);

		[Export ("observeFilePresenters:")]
		void ObserveFilePresenters ([NullAllowed] NSFilePresenter [] filePresenters);

		[Export ("unobserveFilePresenters:")]
		void UnobserveFilePresenters ([NullAllowed] NSFilePresenter [] filePresenters);
	}

	[BaseType (typeof (PSPDFWidgetAnnotation))]
	interface PSPDFFormElement {

		[NullAllowed, Export ("formField", ArgumentSemantic.Weak)]
		PSPDFFormField FormField { get; }

		[Export ("resettable")]
		bool Resettable { [Bind ("isResettable")] get; }

		[NullAllowed, Export ("defaultValue")]
		NSObject DefaultValue { get; }

		[NullAllowed, Export ("exportValue")]
		NSObject ExportValue { get; }

		[NullAllowed, Export ("highlightColor", ArgumentSemantic.Strong)]
		UIColor HighlightColor { get; set; }

		[NullAllowed, Export ("next", ArgumentSemantic.Weak)]
		PSPDFFormElement Next { get; set; }

		[NullAllowed, Export ("previous", ArgumentSemantic.Weak)]
		PSPDFFormElement Previous { get; set; }

		[Export ("calculationOrderIndex")]
		nuint CalculationOrderIndex { get; }

		[Export ("readOnly"), New]
		bool ReadOnly { [Bind ("isReadOnly")] get; }

		[Export ("required")]
		bool Required { [Bind ("isRequired")] get; }

		[Export ("noExport")]
		bool NoExport { [Bind ("isNoExport")] get; }

		[NullAllowed, Export ("fieldName")]
		string FieldName { get; }

		[NullAllowed, Export ("fullyQualifiedFieldName")]
		string FullyQualifiedFieldName { get; }

		[Export ("formTypeName")]
		string FormTypeName { get; }

		// PSPDFFormElement (Fonts) Category

		[Export ("maxLength")]
		nuint MaxLength { get; set; }

		[Export ("doNotScroll")]
		bool DoNotScroll { get; set; }

		[Export ("isMultiline")]
		bool IsMultiline { get; set; }

		// PSPDFFormElement (Drawing) Category

		[Export ("drawHighlightInContext:options:multiply:")]
		void DrawHighlight (CGContext context, [NullAllowed] NSDictionary renderOptions, bool shouldMultiply);
	}

	[BaseType (typeof (PSPDFModel))]
	[DisableDefaultCtor]
	interface PSPDFFormField : PSPDFUndoSupport {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("type")]
		PSPDFFormFieldType Type { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("fullyQualifiedName")]
		string FullyQualifiedName { get; }

		[NullAllowed, Export ("mappingName", ArgumentSemantic.Strong)]
		string MappingName { get; set; }

		[NullAllowed, Export ("alternateFieldName", ArgumentSemantic.Strong)]
		string AlternateFieldName { get; set; }

		[Export ("isEditable")]
		bool IsEditable { get; set; }

		[Export ("isReadOnly")]
		bool IsReadOnly { get; set; }

		[Export ("isRequired")]
		bool IsRequired { get; set; }

		[Export ("isNoExport")]
		bool IsNoExport { get; set; }

		[NullAllowed, Export ("defaultValue")]
		NSObject DefaultValue { get; }

		[NullAllowed, Export ("exportValue")]
		NSObject ExportValue { get; }

		[NullAllowed, Export ("value", ArgumentSemantic.Strong)]
		NSObject Value { get; set; }

		[Export ("calculationOrderIndex")]
		nuint CalculationOrderIndex { get; }

		[Export ("dirty")]
		bool Dirty { get; }

		[Export ("annotations")]
		PSPDFFormElement [] Annotations { get; }

		[Export ("nameForAnnotation:")]
		[return: NullAllowed]
		string GetName (PSPDFFormElement annotation);

		[Export ("fullyQualifiedNameForAnnotation:")]
		[return: NullAllowed]
		string GetFullyQualifiedName (PSPDFFormElement annotation);

		// PSPDFUndoSupport protocol support

		[Static]
		[Export ("keysForValuesToObserveForUndo")]
		NSSet<NSString> GetKeysForValuesToObserveForUndo ();

		[Static]
		[Export ("localizedUndoActionNameForKey:")]
		[return: NullAllowed]
		string LocalizedUndoActionName (string key);

		[Static]
		[Export ("undoCoalescingForKey:")]
		PSPDFUndoCoalescing GetUndoCoalescing (string key);
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

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFFormParser {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("forms", ArgumentSemantic.Copy)]
		PSPDFFormElement [] Forms { get; }

		[NullAllowed, Export ("formFields", ArgumentSemantic.Copy)]
		PSPDFFormField [] FormFields { get; }

		[NullAllowed, Export ("dirtyForms")]
		PSPDFFormElement [] DirtyForms { get; }

		[Export ("findAnnotationWithFieldName:")]
		PSPDFFormElement FindAnnotation (string fieldName);

		[Export ("findFieldWithFullFieldName:")]
		PSPDFFormField FindField (string fullFieldName);

		[Export ("removeFormElements:error:")]
		bool RemoveFormElements (PSPDFFormElement [] formElements, [NullAllowed] out NSError error);

		[Export ("removeFormFields:error:")]
		bool RemoveFormFields (PSPDFFormField [] formFields, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFFreeTextAnnotation : PSPDFRotatable {

		[Field ("PSPDFFreeTextAnnotationIntentTransformerName", PSPDFKitLibraryPath.LibraryPath)]
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
		CGSize GetSize (CGSize constraints);

		[Export ("enableVerticalResizing")]
		bool EnableVerticalResizing { get; set; }

		[Export ("enableHorizontalResizing")]
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

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFGlyph : INSCopying, INSSecureCoding {

		[Export ("frame")]
		CGRect Frame { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("lineBreaker")]
		bool LineBreaker { get; }

		[Export ("wordBreaker")]
		bool WordBreaker { [Bind ("isWordBreaker")] get; }

		[Export ("isWordOrLineBreaker")]
		bool IsWordOrLineBreaker { get; }

		[Export ("generated")]
		bool Generated { [Bind ("isGenerated")] get; }

		[Export ("indexOnPage")]
		nint IndexOnPage { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFGoToAction {

		[Export ("initWithPageIndex:")]
		IntPtr Constructor (nuint pageIndex);

		[Export ("pageIndex")]
		nuint PageIndex { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFHideAction {

		[Export ("initWithAssociatedAnnotations:shouldHide:")]
		IntPtr Constructor (PSPDFAnnotation [] annotations, bool shouldHide);

		[Export ("shouldHide")]
		bool ShouldHide { get; }

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }
	}

	[BaseType (typeof (PSPDFMarkupAnnotation))]
	interface PSPDFHighlightAnnotation {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		[return: NullAllowed]
		PSPDFHighlightAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs, nint pageRotation);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFHighlightAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFDocument))]
	interface PSPDFImageDocument {

		[Export ("initWithImageURL:")]
		IntPtr Constructor (NSUrl imageURL);

		[NullAllowed, Export ("imageURL")]
		NSUrl ImageUrl { get; }

		[Export ("compressionQuality")]
		nfloat CompressionQuality { get; set; }

		[Export ("imageSaveMode")]
		PSPDFImageSaveMode ImageSaveMode { get; set; }

		[Static]
		[Export ("supportedContentTypes")]
		NSSet<NSString> SupportedContentTypes { get; }

		[Export ("waitUntilLoaded")]
		void WaitUntilLoaded ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFImageInfo : INSCopying, INSSecureCoding {

		[Export ("index")]
		nuint Index { get; }

		[Export ("pixelSize")]
		CGSize PixelSize { get; }

		[Export ("transform")]
		CGAffineTransform Transform { get; }

		[Export ("vertices")]
		NSValue [] Vertices { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("displaySize")]
		CGSize DisplaySize { get; }

		[Export ("horizontalResolution")]
		nfloat HorizontalResolution { get; }

		[Export ("verticalResolution")]
		nfloat VerticalResolution { get; }

		[Export ("hitTest:")]
		bool HitTest (CGPoint point);

		[Export ("boundingBox")]
		CGRect BoundingBox { get; }

		[Export ("imageWithError:")]
		[return: NullAllowed]
		UIImage GetImage ([NullAllowed] out NSError error);

		[Export ("imageInRGBColorSpaceWithError:")]
		[return: NullAllowed]
		UIImage GetImageInRGBColorSpace ([NullAllowed] out NSError error);
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFInkAnnotation {

		[Export ("initWithLines:"), Internal]
		IntPtr InitWithLines (IntPtr lines);

		[Advice ("You can use '[Get|Set]Lines' for a more strongly typed access.")]
		[NullAllowed, Export ("lines", ArgumentSemantic.Copy)]
		NSArray<NSValue> [] Lines { get; set; }

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }

		[Export ("empty")]
		bool Empty { [Bind ("isEmpty")] get; }

		[Export ("naturalDrawingEnabled")]
		bool NaturalDrawingEnabled { get; set; }

		[Export ("isSignature")]
		bool IsSignature { get; set; }

		[Export ("setBoundingBox:transformLines:")]
		void SetBoundingBox (CGRect boundingBox, bool transformLines);

		[Export ("copyLinesByApplyingTransform:")]
		NSObject [] CopyLines (CGAffineTransform transform);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFJavaScriptAction {

		[Export ("initWithScript:")]
		IntPtr Constructor (string script);

		[NullAllowed, Export ("script")]
		string Script { get; }
	}

	[Category]
	[BaseType (typeof (PSPDFDocumentProvider))]
	interface PSPDFDocumentProvider_JavascriptAdditions {

		[Export ("configureDocumentScriptExecutor:")]
		void ConfigureDocumentScriptExecutor (NSObject vm);

		[Export ("configureJavaScriptPlatformDelegate:")]
		void ConfigureJavaScriptPlatformDelegate (NSObject platformDelegate);

		[Export ("updateCalculatedFieldsDependingOnForm:error:")]
		bool UpdateCalculatedFieldsDependingOnForm ([NullAllowed] PSPDFFormElement sourceForm, [NullAllowed] out NSError error);
	}

	interface IPSPDFSettings { }

	[Protocol]
	interface PSPDFSettings {

		[Abstract]
		[Export ("objectForKeyedSubscript:")]
		[return: NullAllowed]
		NSObject GetObject (NSObject key);

		[Abstract]
		[Export ("boolForKey:")]
		bool GetBool (string key);
	}

	delegate string PSPDFKitLogMessageHandler ();
	delegate void PSPDFKitLogHandler (PSPDFLogLevelMask type, IntPtr strTag, [BlockCallback] PSPDFKitLogMessageHandler message, IntPtr strFile, IntPtr strFunction, nuint line);
	delegate UIImage PSPDFKitImageLoadingHandler (string imageName);

	[BaseType (typeof (NSObject), Name = "PSPDFKit")]
	interface PSPDFKitGlobal : PSPDFSettings {

		[Field ("PSPDFXCallbackURLStringKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString XCallbackUrlStringKey { get; }

		[Field ("PSPDFApplicationPolicyKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ApplicationPolicyKey { get; }

		[Field ("PSPDFFileManagerKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FileManagerKey { get; }

		[Field ("PSPDFCoordinatedFileManagerKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CoordinatedFileManagerKey { get; }

		[Field ("PSPDFFileCoordinationEnabledKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FileCoordinationEnabledKey { get; }

		[Field ("PSPDFLibraryIndexingPriorityKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString LibraryIndexingPriorityKey { get; }

		[Field ("PSPDFWebKitLegacyModeKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString WebKitLegacyModeKey { get; }

		[Static]
		[Export ("sharedInstance")]
		PSPDFKitGlobal SharedInstance { get; }

		[Static]
		[Export ("setLicenseKey:")]
		void SetLicenseKey (string licenseKey);

		[Static]
		[Export ("setLicenseKey:options:")]
		void SetLicenseKey (string licenseKey, [NullAllowed] NSDictionary options);

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
		nuint BuildNumber { get; }

		[Static]
		[Export ("isFeatureEnabled:")]
		bool IsFeatureEnabled (PSPDFFeatureMask feature);

		[Export ("setObject:forKeyedSubscript:")]
		void SetObject (NSObject @object, NSString key);

		[Export ("cache")]
		PSPDFCache Cache { get; }

		[Export ("fileManager")]
		IPSPDFFileManager FileManager { get; }

		[Export ("renderManager")]
		IPSPDFRenderManager RenderManager { get; }

		[Export ("styleManager")]
		IPSPDFAnnotationStyleManager StyleManager { get; }

		[Export ("signatureManager")]
		PSPDFSignatureManager SignatureManager { get; }

		[Export ("policy")]
		IPSPDFApplicationPolicy Policy { get; }

		[NullAllowed, Export ("library", ArgumentSemantic.Strong)]
		PSPDFLibrary Library { get; set; }

		[NullAllowed, Export ("databaseEncryptionProvider", ArgumentSemantic.Strong)]
		IPSPDFDatabaseEncryptionProvider DatabaseEncryptionProvider { get; set; }

		[Export ("injectDependentProperties:")]
		nint InjectDependentProperties (NSObject @object);

		// PSPDFKit (ImageLoading) Category
		[Static]
		[Export ("imageNamed:")]
		[return: NullAllowed]
		UIImage GetImage (string name);

		[NullAllowed, Export ("imageLoadingHandler", ArgumentSemantic.Strong)]
		PSPDFKitImageLoadingHandler ImageLoadingHandler { get; set; }

		[Export ("logLevel", ArgumentSemantic.Assign)]
		PSPDFLogLevelMask LogLevel { get; set; }

		[NullAllowed, Export ("logHandler", ArgumentSemantic.Strong)]
		PSPDFKitLogHandler LogHandler { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLabelParser {

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageLabelForPageAtIndex:")]
		[return: NullAllowed]
		string GetPageLabel (nuint pageIndex);

		[Export ("pageForPageLabel:partialMatching:")]
		nuint GetPage (string pageLabel, bool partialMatching);

		[Export ("labels", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, NSString> Labels { get; }
	}

	[Static]
	interface PSPDFLibrarySearchResultsOptionsKeys {

		[Field ("PSPDFLibraryMaximumSearchResultsTotalKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumSearchResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumSearchResultsPerDocumentKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumSearchResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryMaximumPreviewResultsTotalKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumPreviewResultsTotalKey { get; }

		[Field ("PSPDFLibraryMaximumPreviewResultsPerDocumentKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MaximumPreviewResultsPerDocumentKey { get; }

		[Field ("PSPDFLibraryMatchExactWordsOnlyKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MatchExactWordsOnlyKey { get; }

		[Field ("PSPDFLibraryMatchExactPhrasesOnlyKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString MatchExactPhrasesOnlyKey { get; }

		[Field ("PSPDFLibraryExcludeAnnotationsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExcludeAnnotationsKey { get; }

		[Field ("PSPDFLibraryExcludeDocumentTextKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString ExcludeDocumentTextKey { get; }

		[Field ("PSPDFLibraryPreviewRangeKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PreviewRangeKey { get; }
	}

	[StrongDictionary ("PSPDFLibrarySearchResultsOptionsKeys")]
	interface PSPDFLibrarySearchResultsOptions {
		nuint MaximumSearchResultsTotal { get; set; }
		nuint MaximumSearchResultsPerDocument { get; set; }
		nuint MaximumPreviewResultsTotal { get; set; }
		nuint MaximumPreviewResultsPerDocument { get; set; }
		bool MatchExactWordsOnly { get; set; }
		bool MatchExactPhrasesOnly { get; set; }
		bool ExcludeAnnotations { get; set; }
		bool ExcludeDocumentText { get; set; }

		[Export ("PreviewRangeKey"), Internal]
		NSValue WeakPreviewRange { get; set; }
	}

	interface PSPDFLibraryDidFinishIndexingDocumentNotificationEventArgs {

		[Export ("PSPDFLibraryNotificationUIDKey")]
		string Uid { get; }

		[Export ("PSPDFLibraryNotificationSuccessKey")]
		[ProbePresence]
		bool Success { get; }
	}

	delegate void PSPDFLibraryFindDocumentUidsCompletionHandler (string searchString, NSDictionary<NSString, NSIndexSet> resultSet);
	delegate void PSPDFLibraryFindDocumentUidsPreviewTextHandler (string searchString, NSDictionary<NSString, NSSet<PSPDFLibraryPreviewResult>> resultSet);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLibrary {

		[Field ("PSPDFLibraryWillStartIndexingDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString WillStartIndexingDocumentNotification { get; }

		[Field ("PSPDFLibraryDidFinishIndexingDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification (typeof (PSPDFLibraryDidFinishIndexingDocumentNotificationEventArgs))]
		NSString DidFinishIndexingDocumentNotification { get; }

		[Field ("PSPDFLibraryDidRemoveDocumentNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidRemoveDocumentNotification { get; }

		[Field ("PSPDFLibraryDidClearIndexesNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString DidClearIndexesNotification { get; }

		[Static]
		[Export ("libraryWithPath:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:ftsVersion:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("libraryWithPath:indexingPriority:ftsVersion:tokenizer:error:")]
		[return: NullAllowed]
		PSPDFLibrary FromPath (string path, PSPDFLibraryIndexingPriority priority, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("defaultLibraryPath")]
		string DefaultLibraryPath { get; }

		[Export ("path")]
		string Path { get; }

		[Export ("spotlightIndexingType", ArgumentSemantic.Assign)]
		PSPDFLibrarySpotlightIndexingType SpotlightIndexingType { get; set; }

		[Export ("shouldIndexAnnotations")]
		bool ShouldIndexAnnotations { get; set; }

		[NullAllowed, Export ("tokenizer")]
		string Tokenizer { get; }

		[Export ("saveReversedPageText")]
		bool SaveReversedPageText { get; set; }

		[Export ("suspended")]
		bool Suspended { get; set; }

		[Export ("automaticallyPauseLongRunningTasks")]
		bool AutomaticallyPauseLongRunningTasks { get; set; }

		[Async (ResultTypeName = "PSPDFLibraryFindDocumentUidsResults")]
		[Export ("documentUIDsMatchingString:options:completionHandler:")]
		void FindDocumentUids (string searchString, [NullAllowed] NSDictionary options, PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFLibraryFindDocumentUidsResults")]
		[Wrap ("FindDocumentUids (searchString, searhcOptions?.Dictionary, completionHandler)")]
		void FindDocumentUids (string searchString, PSPDFLibrarySearchResultsOptions searhcOptions, PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler);

		[Export ("documentUIDsMatchingString:options:completionHandler:previewTextHandler:")]
		void FindDocumentUids (string searchString, [NullAllowed] NSDictionary options, [NullAllowed] PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler, [NullAllowed] PSPDFLibraryFindDocumentUidsPreviewTextHandler previewTextHandler);

		[Wrap ("FindDocumentUids (searchString, searhcOptions?.Dictionary, completionHandler, previewTextHandler)")]
		void FindDocumentUids (string searchString, PSPDFLibrarySearchResultsOptions searhcOptions, [NullAllowed] PSPDFLibraryFindDocumentUidsCompletionHandler completionHandler, [NullAllowed] PSPDFLibraryFindDocumentUidsPreviewTextHandler previewTextHandler);

		[Export ("indexStatusForUID:withProgress:")]
		PSPDFLibraryIndexStatus GetIndexStatus (string uid, out nfloat outProgress);

		[Export ("indexing")]
		bool Indexing { [Bind ("isIndexing")] get; }

		[Export ("queuedUIDs")]
		NSOrderedSet<NSString> QueuedUids { get; }

		[NullAllowed, Export ("indexedUIDs")]
		NSOrderedSet<NSString> IndexedUids { get; }

		[Export ("indexedUIDCount")]
		nint IndexedUidCount { get; }

		[Export ("indexedDocumentWithUID:")]
		[return: NullAllowed]
		PSPDFDocument GetIndexedDocument (string uid);

		[NullAllowed, Export ("dataSource", ArgumentSemantic.Strong)]
		IPSPDFLibraryDataSource DataSource { get; set; }

		[Async]
		[Export ("updateIndexWithCompletionHandler:")]
		void UpdateIndex ([NullAllowed] Action completionHandler);

		[Export ("removeIndexForUID:")]
		void RemoveIndex (string uid);

		[Export ("clearAllIndexes")]
		void ClearAllIndexes ();

		[Async]
		[Export ("fetchSpotlightIndexedDocumentForUserActivity:completionHandler:")]
		void FetchSpotlightIndexedDocument (NSUserActivity userActivity, Action<PSPDFDocument> completionHandler);

		[Export ("cancelAllPreviewTextOperations")]
		void CancelAllPreviewTextOperations ();

		// PSPDFLibrary (EncryptionSupport) Category

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:ftsVersion:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Static]
		[Export ("encryptedLibraryWithPath:encryptionKeyProvider:indexingPriority:ftsVersion:tokenizer:error:")]
		PSPDFLibrary CreateEncryptedLibrary (string path, [NullAllowed] Func<NSData> encryptionKeyProvider, PSPDFLibraryIndexingPriority priority, PSPDFLibraryFTSVersion ftsVersion, [NullAllowed] string tokenizer, [NullAllowed] out NSError error);

		[Export ("encrypted")]
		bool Encrypted { [Bind ("isEncrypted")] get; }
	}

	interface IPSPDFLibraryDataSource { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryDataSource {

		[Export ("libraryWillBeginIndexing:")]
		void WillBeginIndexing (PSPDFLibrary library);

		[Export ("library:didFinishIndexingDocumentWithUID:success:")]
		void DidFinishIndexingDocument (PSPDFLibrary library, string documentUid, bool success);

		[Export ("library:didRemoveDocumentWithUID:")]
		void DidRemoveDocument (PSPDFLibrary library, string documentUid);

		[Abstract]
		[Export ("uidsOfDocumentsToBeIndexedByLibrary:")]
		string [] GetUidsOfDocumentsToBeIndexed (PSPDFLibrary library);

		[Abstract]
		[Export ("uidsOfDocumentsToBeRemovedFromLibrary:")]
		string [] GetUidsOfDocumentsToBeRemoved (PSPDFLibrary library);

		[Abstract]
		[Export ("library:documentWithUID:")]
		[return: NullAllowed]
		PSPDFDocument GetDocument (PSPDFLibrary library, string uid);
	}

	delegate bool PSPDFLibraryFileSystemDataSourceDocumentHandler (PSPDFDocument document, ref bool stop);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFLibraryFileSystemDataSource : PSPDFLibraryDataSource {

		[Export ("initWithLibrary:documentsDirectoryURL:documentHandler:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFLibrary library, NSUrl url, [NullAllowed] PSPDFLibraryFileSystemDataSourceDocumentHandler documentHandler);

		[NullAllowed, Export ("library", ArgumentSemantic.Weak)]
		PSPDFLibrary Library { get; }

		[Export ("documentsDirectoryURL")]
		NSUrl DocumentsDirectoryUrl { get; }

		[NullAllowed, Export ("documentHandler", ArgumentSemantic.Copy)]
		PSPDFLibraryFileSystemDataSourceDocumentHandler DocumentHandler { get; set; }

		[Export ("directoryEnumerationOptions", ArgumentSemantic.Assign)]
		NSDirectoryEnumerationOptions DirectoryEnumerationOptions { get; set; }

		[Export ("allowedPathExtensions"), NullAllowed]
		NSSet<NSString> AllowedPathExtensions { get; set; }

		[Export ("indexItemDescriptorForDocumentWithUID:")]
		[return: NullAllowed]
		PSPDFFileIndexItemDescriptor GetIndexItemDescriptorForDocument (string documentUid);

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		IPSPDFLibraryFileSystemDataSourceDocumentProvider DocumentProvider { get; set; }

		[Export ("explicitModeEnabled")]
		bool ExplicitModeEnabled { [Bind ("isExplicitModeEnabled")] get; set; }

		[Export ("didAddOrModifyDocumentAtURL:")]
		void DidAddOrModifyDocument (NSUrl url);

		[Export ("didRemoveDocumentAtURL:")]
		void DidRemoveDocument (NSUrl url);
	}

	interface IPSPDFLibraryFileSystemDataSourceDocumentProvider { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFLibraryFileSystemDataSourceDocumentProvider {

		[Abstract]
		[Export ("dataSource:documentWithUID:atURL:")]
		[return: NullAllowed]
		PSPDFDocument GetDocument (PSPDFLibraryFileSystemDataSource dataSource, [NullAllowed] string uid, NSUrl fileUrl);
	}

	[BaseType (typeof (PSPDFSearchResult))]
	interface PSPDFLibraryPreviewResult : INativeObject {

		[Export ("annotationObjectNumber")]
		nint AnnotationObjectNumber { get; }

		[Export ("isAnnotationResult")]
		bool IsAnnotationResult { get; }
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
		IntPtr Constructor (PSPDFLinkAnnotationType linkAnnotationType);

		[Export ("initWithAction:")]
		IntPtr Constructor (PSPDFAction action);

		[Export ("initWithURL:")]
		IntPtr Constructor (NSUrl url);

		[Export ("linkType", ArgumentSemantic.Assign)]
		PSPDFLinkAnnotationType LinkType { get; set; }

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		PSPDFAction Action { get; set; }

		[NullAllowed, Export ("URLAction")]
		PSPDFURLAction UrlAction { get; }

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("showAsLinkView")]
		bool ShowAsLinkView { get; }

		[Export ("multimediaExtension")]
		bool MultimediaExtension { [Bind ("isMultimediaExtension")] get; }

		[Export ("controlsEnabled")]
		bool ControlsEnabled { get; set; }

		[Export ("autoplayEnabled")]
		bool AutoplayEnabled { [Bind ("isAutoplayEnabled")] get; set; }

		[Export ("loopEnabled")]
		bool LoopEnabled { [Bind ("isLoopEnabled")] get; set; }

		[Export ("fullscreenEnabled")]
		bool FullscreenEnabled { [Bind ("isFullscreenEnabled")] get; set; }

		[NullAllowed, Export ("targetString")]
		string TargetString { get; }
	}

	[Category]
	[BaseType (typeof (NSObject))]
	interface NSObject_PSPDFLocalizedAccessibility {

		[Export ("pspdf_accessibility")]
		string GetPSPDFAccessibility ();

		[Export ("setPspdf_accessibility:")]
		void SetPSPDFAccessibility (string str);
	}

	[Abstract]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFMarkupAnnotation {

		//[Static]
		//[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		//[return: NullAllowed] // TODO: Add to subclasses
		//PSPDFMarkupAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph [] glyphs, nint pageRotation);

		//[Static]
		//[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		//[return: NullAllowed] // TODO: Add to subclasses
		//PSPDFMarkupAnnotation FromRects ([BindAs (typeof (CGRect []))] NSValue [] rects, CGRect boundingBox, nuint pageIndex);

		[Export ("highlightedString")]
		string HighlightedString { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFMemoryCache {

		[Export ("initWithSettings:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFKitGlobal settings);

		[Export ("count")]
		nuint Count { get; }

		[Export ("numberOfPixels")]
		long NumberOfPixels { get; }

		[Export ("maxNumberOfPixels")]
		long MaxNumberOfPixels { get; set; }

		[Export ("maxNumberOfPixelsUnderStress")]
		long MaxNumberOfPixelsUnderStress { get; set; }
	}

	[Abstract]
	[BaseType (typeof (NSObject))]
	interface PSPDFModel : INSCopying, INSCoding {

		[Static]
		[return: NullAllowed]
		[Export ("modelWithDictionary:error:")]
		PSPDFModel FromDictionary ([NullAllowed] NSDictionary dictionaryValue, [NullAllowed] out NSError error);

		[Export ("initWithDictionary:error:")]
		IntPtr Constructor ([NullAllowed] NSDictionary dictionaryValue, [NullAllowed] out NSError error);

		[Static]
		[Export ("propertyKeys")]
		NSOrderedSet<NSString> PropertyKeys { get; }

		[Export ("dictionaryValue", ArgumentSemantic.Copy)]
		NSDictionary DictionaryValue { get; }
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFNamedAction {

		[Field ("PSPDFNamedActionTypeTransformerName", PSPDFKitLibraryPath.LibraryPath)]
		NSString TransformerName { get; }

		[Export ("initWithActionNamedString:")]
		IntPtr Constructor ([NullAllowed] string actionNameString);

		[Export ("namedActionType")]
		PSPDFNamedActionType NamedActionType { get; }

		[NullAllowed, Export ("namedAction")]
		string NamedAction { get; }

		[Export ("pageIndexWithCurrentPage:fromDocument:")]
		nuint GetPageIndex (nuint currentPage, PSPDFDocument document);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFNewPageConfiguration {

		[Static]
		[Export ("newPageConfigurationWithPageTemplate:builderBlock:")]
		PSPDFNewPageConfiguration FromPageTemplate (PSPDFPageTemplate pageTemplate, [NullAllowed] Action<PSPDFNewPageConfigurationBuilder> builderBlock);

		[Export ("pageSize")]
		CGSize PageSize { get; }

		[Export ("pageRotation")]
		PSPDFRotation PageRotation { get; }

		[NullAllowed, Export ("backgroundColor")]
		UIColor BackgroundColor { get; }

		[Export ("pageMargins")]
		UIEdgeInsets PageMargins { get; }

		[NullAllowed, Export ("pageTemplate")]
		PSPDFPageTemplate PageTemplate { get; }

		[NullAllowed, Export ("item")]
		PSPDFProcessorItem Item { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFNewPageConfigurationBuilder {

		[Export ("pageSize", ArgumentSemantic.Assign)]
		CGSize PageSize { get; set; }

		[Export ("pageRotation")]
		nuint PageRotation { get; set; }

		[NullAllowed, Export ("backgroundColor", ArgumentSemantic.Strong)]
		UIColor BackgroundColor { get; set; }

		[NullAllowed, Export ("item", ArgumentSemantic.Strong)]
		PSPDFProcessorItem Item { get; set; }

		[Export ("pageMargins", ArgumentSemantic.Assign)]
		UIEdgeInsets PageMargins { get; set; }
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFNoteAnnotation {

		[Export ("initWithContents:")]
		IntPtr Constructor (string contents);

		[Export ("iconName")]
		string IconName { get; set; }

		[Export ("authorStateModel", ArgumentSemantic.Assign)]
		PSPDFAnnotationAuthorStateModel AuthorStateModel { get; set; }

		[Export ("authorState", ArgumentSemantic.Assign)]
		PSPDFAnnotationAuthorState AuthorState { get; set; }

		// PSPDFNoteAnnotation (SubclassingHooks) Category

		[NullAllowed, Export ("renderAnnotationIcon")]
		UIImage RenderAnnotationIcon { get; }

		[Export ("drawImageInContext:boundingBox:options:")]
		void DrawImage (CGContext context, CGRect boundingBox, [NullAllowed] NSDictionary options);

		[Export ("boundingBoxIfRenderedAsText")]
		CGRect BoundingBoxIfRenderedAsText { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFOutlineElement {

		[Export ("initWithTitle:color:fontTraits:action:children:level:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string title, [NullAllowed] UIColor color, UIFontDescriptorSymbolicTraits fontTraits, [NullAllowed] PSPDFAction action, [NullAllowed] PSPDFOutlineElement [] children, nuint level);

		[Export ("flattenedChildren")]
		PSPDFOutlineElement [] FlattenedChildren { get; }

		[Export ("allFlattenedChildren")]
		PSPDFOutlineElement [] AllFlattenedChildren { get; }

		[NullAllowed, Export ("parent", ArgumentSemantic.Weak)]
		PSPDFOutlineElement Parent { get; }

		[NullAllowed, Export ("title")]
		string Title { get; }

		[NullAllowed, Export ("action")]
		PSPDFAction Action { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[NullAllowed, Export ("color")]
		UIColor Color { get; }

		[Export ("fontTraits")]
		UIFontDescriptorSymbolicTraits FontTraits { get; }

		[NullAllowed, Export ("children", ArgumentSemantic.Copy)]
		PSPDFOutlineElement [] Children { get; }

		[Export ("level")]
		nuint Level { get; }

		[Export ("expanded")]
		bool Expanded { [Bind ("isExpanded")] get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFOutlineParser {

		[Export ("outlineElementForPageAtIndex:exactPageOnly:")]
		[return: NullAllowed]
		PSPDFOutlineElement GetOutlineElement (nuint pageIndex, bool exactPageOnly);

		[Export ("outline", ArgumentSemantic.Strong)]
		PSPDFOutlineElement Outline { get; set; }

		[Export ("outlineParsed")]
		bool OutlineParsed { [Bind ("isOutlineParsed")] get; }

		[Export ("outlineAvailable")]
		bool OutlineAvailable { [Bind ("isOutlineAvailable")] get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }
	}

	interface IPSPDFOverridable { }

	[Protocol]
	interface PSPDFOverridable {

		[Export ("classForClass:")]
		Class GetClassForClass (Class originalClass);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFPage {

		[Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("documentProvider")]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageInfo")]
		PSPDFPageInfo PageInfo { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		// PSPDFPage (AnnotationFactory) Category

		[Export ("createStampAnnotationWithImage:")]
		PSPDFStampAnnotation CreateStampAnnotation (UIImage iamge);

		[Export ("createFreeTextAnnotationWithContents:")]
		PSPDFFreeTextAnnotation CreateFreeTextAnnotation (string contents);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPageInfo : INSCopying, INSSecureCoding {

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("rect")]
		CGRect Rect { get; }

		[Export ("rotation")]
		nuint Rotation { get; }

		[NullAllowed, Export ("additionalActions", ArgumentSemantic.Copy)]
		NSDictionary<NSNumber, PSPDFAction> AdditionalActions { get; }

		[Export ("rotatedRect")]
		CGRect RotatedRect { get; }

		[Export ("rotationTransform")]
		CGAffineTransform RotationTransform { get; }

		[Export ("allowAnnotationCreation")]
		bool AllowAnnotationCreation { get; }

		[Export ("mediaBox")]
		CGRect MediaBox { get; }

		[Export ("cropBox")]
		CGRect CropBox { get; }
	}

	delegate void PSPDFPKCS12UnlockHandler ([NullAllowed] PSPDFX509 x509, [NullAllowed] PSPDFPrivateKey pk, [NullAllowed] NSError error);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPKCS12 : INSCoding {

		[Export ("initWithData:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSData data);

		[Async (ResultTypeName = "PSPDFPKCS12UnlockHandlerResult")]
		[Export ("unlockWithPassword:done:")]
		void Unlock (string password, [NullAllowed] PSPDFPKCS12UnlockHandler done);
	}

	delegate void PSPDFPKCS12SignerSignFormElementCompletionHandler (bool success, PSPDFDocument document, NSError error);

	[BaseType (typeof (PSPDFSigner))]
	[DisableDefaultCtor]
	interface PSPDFPKCS12Signer {

		[Export ("initWithDisplayName:PKCS12:")]
		[DesignatedInitializer]
		IntPtr Constructor (string displayName, PSPDFPKCS12 p12);

		[Export ("displayName"), New]
		string DisplayName { get; }

		[Export ("reason"), New]
		string Reason { get; set; }

		[Export ("location"), New]
		string Location { get; set; }

		[Export ("p12")]
		PSPDFPKCS12 P12 { get; }

		[NullAllowed, Export ("privateKey", ArgumentSemantic.Strong)]
		PSPDFPrivateKey PrivateKey { get; set; }

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeTo:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFPKCS12SignerSignFormElementCompletionHandler completionHandler);

		[Async (ResultTypeName = "PSPDFPKCS12SignerSignFormElementCompletionHandlerResult")]
		[Export ("signFormElement:usingPassword:writeTo:appearance:biometricProperties:completion:")]
		void SignFormElement (PSPDFSignatureFormElement element, string password, string path, [NullAllowed] PSPDFSignatureAppearance signatureAppearance, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties, [NullAllowed] PSPDFPKCS12SignerSignFormElementCompletionHandler completionBlock);
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolygonAnnotation {

		[Export ("initWithPoints:intentType:")]
		IntPtr Constructor (NSValue [] points, PSPDFPolygonAnnotationIntent intentType);

		[Export ("intentType", ArgumentSemantic.Assign)]
		PSPDFPolygonAnnotationIntent IntentType { get; set; }
	}

	[BaseType (typeof (PSPDFAbstractLineAnnotation))]
	interface PSPDFPolyLineAnnotation {

		[Export ("initWithPoints:")]
		IntPtr Constructor (NSValue [] points);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFPopupAnnotation {

		[Export ("open")]
		bool Open { [Bind ("isOpen")] get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFPrivateKey {

		[Export ("signatureEncryptionAlgorithm")]
		PSPDFSignatureEncryptionAlgorithm SignatureEncryptionAlgorithm { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessor {

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFProcessorDelegate Delegate { get; set; }

		[Export ("initWithConfiguration:securityOptions:")]
		IntPtr Constructor (PSPDFProcessorConfiguration configuration, [NullAllowed] PSPDFDocumentSecurityOptions securityOptions);

		[Export ("writeToFileURL:")]
		bool WriteToFile (NSUrl fileUrl);

		[Export ("writeToFileURL:error:")]
		bool WriteToFile (NSUrl fileURL, [NullAllowed] out NSError error);

		[NullAllowed, Export ("data")]
		NSData GetData ();

		[Export ("dataWithError:")]
		[return: NullAllowed]
		NSData GetData ([NullAllowed] out NSError error);

		[Export ("outputToDataSink:")]
		bool OutputToDataSink (IPSPDFDataSink outputDataSink);

		[Export ("outputToDataSink:error:")]
		bool OutputToDataSink (IPSPDFDataSink outputDataSink, [NullAllowed] out NSError error);

		[Export ("cancel")]
		void Cancel ();

#if __IOS__

		[Export ("initWithOptions:")]
		IntPtr Constructor ([NullAllowed] NSDictionary optionsDictionary);

		[Wrap ("this (options?.Dictionary)")]
		IntPtr Constructor (PSPDFProcessorGenerationOptions options);

		[Export ("convertHTMLString:outputFileURL:")]
		void ConvertHtml (string html, NSUrl fileUrl);

		[Async]
		[Export ("convertHTMLString:outputFileURL:completionBlock:")]
		void ConvertHtml (string html, NSUrl fileUrl, [NullAllowed] Action<NSError> completion);

		[Export ("convertHTMLString:")]
		void ConvertHtml (string html);

		[Async]
		[Export ("convertHTMLString:completionBlock:")]
		void ConvertHtml (string html, [NullAllowed] Action<NSData, NSError> completion);

		[Export ("generatePDFFromURL:outputFileURL:")]
		[return: NullAllowed]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl outputUrl);

		[Async]
		[Export ("generatePDFFromURL:outputFileURL:completionBlock:")]
		[return: NullAllowed]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl, NSUrl outputUrl, [NullAllowed] Action<NSUrl, NSError> completion);

		[Export ("generatePDFFromURL:")]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl);

		[Export ("generatePDFFromURL:completionBlock:")]
		PSPDFConversionOperation GeneratePdf (NSUrl inputUrl, [NullAllowed] Action<NSData, NSError> completion);
#endif
	}

	interface IPSPDFProcessorDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorDelegate {

		[Export ("processor:didProcessPage:totalPages:")]
		void DidProcessPage (PSPDFProcessor processor, nuint currentPage, nuint totalPages);

		[Export ("processor:didFinishWithError:")]
		void DidFinishWithError (PSPDFProcessor processor, [NullAllowed] NSError error);

		[Export ("processor:didFinishWithData:error:")]
		void DidFinishWithData (PSPDFProcessor processor, [NullAllowed] NSData data, [NullAllowed] NSError error);

		[Export ("processor:didFinishWithFileURL:error:")]
		void DidFinishWithFileURL (PSPDFProcessor processor, [NullAllowed] NSUrl fileURL, [NullAllowed] NSError error);
	}

#if __IOS__
	[Static]
	interface PSPDFProcessorGenerationOptionsKeys {

		[Field ("PSPDFProcessorAnnotationTypesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationTypesKey { get; }

		[Field ("PSPDFProcessorAnnotationDictKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationDictKey { get; }

		[Field ("PSPDFProcessorAnnotationAsDictionaryKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AnnotationAsDictionaryKey { get; }

		[Field ("PSPDFProcessorUserPasswordKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString UserPasswordKey { get; }

		[Field ("PSPDFProcessorOwnerPasswordKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString OwnerPasswordKey { get; }

		[Field ("PSPDFProcessorKeyLengthKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString KeyLengthKey { get; }

		[Field ("PSPDFProcessorPageRectKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageRectKey { get; }

		[Field ("PSPDFProcessorNumberOfPagesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString NumberOfPagesKey { get; }

		[Field ("PSPDFProcessorPageBorderMarginKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageBorderMarginKey { get; }

		[Field ("PSPDFProcessorAdditionalDelayKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AdditionalDelayKey { get; }

		[Field ("PSPDFProcessorStripEmptyPagesKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString StripEmptyPagesKey { get; }

		[Field ("PSPDFProcessorDocumentTitleKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DocumentTitleKey { get; }

		// Some other useful options
		[Field ("kCGPDFContextAllowsCopying", Constants.CoreGraphicsLibrary)]
		NSString AllowsCopyingKey { get; }

		[Field ("kCGPDFContextAllowsPrinting", Constants.CoreGraphicsLibrary)]
		NSString AllowsPrintingKey { get; }

		[Field ("kCGPDFContextKeywords", Constants.CoreGraphicsLibrary)]
		NSString KeywordsKey { get; }

		[Field ("kCGPDFContextAuthor", Constants.CoreGraphicsLibrary)]
		NSString AuthorKey { get; }

		[Field ("kCGPDFContextSubject", Constants.CoreGraphicsLibrary)]
		NSString SubjectKey { get; }
	}

	[StrongDictionary ("PSPDFProcessorGenerationOptionsKeys")]
	interface PSPDFProcessorGenerationOptions {
		NSDictionary AnnotationDict { get; set; }
		bool AnnotationAsDictionary { get; set; }
		string UserPassword { get; set; }
		string OwnerPassword { get; set; }
		CGRect PageRect { get; set; }
		int NumberOfPages { get; set; }
		double AdditionalDelay { get; set; }
		bool StripEmptyPages { get; set; }
		string DocumentTitle { get; set; }
		bool AllowsCopying { get; set; }
		bool AllowsPrinting { get; set; }
		string [] Keywords { get; set; }
		string Author { get; set; }
		string Subject { get; set; }
	}

	[BaseType (typeof (NSOperation))]
	[DisableDefaultCtor]
	interface PSPDFConversionOperation {

		[Export ("HTMLString")]
		string Html { get; }

		[Export ("inputURL", ArgumentSemantic.Copy)]
		NSUrl InputUrl { get; }

		[NullAllowed, Export ("outputURL", ArgumentSemantic.Copy)]
		NSUrl OutputUrl { get; }

		[NullAllowed, Export ("outputData")]
		NSData OutputData { get; }

		[NullAllowed, Export ("options", ArgumentSemantic.Copy)]
		NSDictionary WeakOptions { get; }

		[Wrap ("WeakOptions")]
		PSPDFProcessorGenerationOptions Options { get; }

		[NullAllowed, Export ("error")]
		NSError Error { get; }
	}
#endif

	[BaseType (typeof (NSObject))]
	interface PSPDFProcessorConfiguration : INSCopying {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; }

		[Export ("pageCount")]
		nint PageCount { get; }

		[Export ("formFieldNameMappings", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> FormFieldNameMappings { get; set; }

		[Export ("formMappingNameMappings", ArgumentSemantic.Copy)]
		NSDictionary<NSString, NSString> FormMappingNameMappings { get; set; }

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
		void ScalePageUsingMillimeterSize (nuint pageIndex, CGSize mmSize);

		[Export ("changeCropBoxForPageAtIndex:toRect:")]
		void ChangeCropBoxForPageAtIndex (nuint pageIndex, CGRect rect);

		[Export ("changeMediaBoxForPageAtIndex:toRect:")]
		void ChangeMediaBox (nuint pageIndex, CGRect rect);

		[Export ("addNewPageAtIndex:configuration:")]
		void AddNewPage (nuint destinationPageIndex, [NullAllowed] PSPDFNewPageConfiguration newPageConfiguation);

		[Export ("modifyAnnotationsOfTypes:change:")]
		void ModifyAnnotations (PSPDFAnnotationType annotationTypes, PSPDFAnnotationChange annotationChange);

		[Export ("modifyAnnotations:change:error:")]
		bool ModifyAnnotations (PSPDFAnnotation [] annotations, PSPDFAnnotationChange annotationChange, [NullAllowed] out NSError error);

		[Export ("modifyFormsOfType:change:")]
		void ModifyForms (PSPDFFormFieldType formFieldType, PSPDFAnnotationChange annotationChange);

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
		PSPDFProcessorItem FromImage (UIImage image, nfloat jpegCompressionQuality, [NullAllowed] Action<PSPDFProcessorItemBuilder> builder);

		[Static]
		[Export ("processorItemWithItemURL:builderBlock:")]
		PSPDFProcessorItem FromItemUrl (NSUrl itemUrl, [NullAllowed] Action<PSPDFProcessorItemBuilder> builder);

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[NullAllowed, Export ("itemURL")]
		NSUrl ItemUrl { get; }

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

		[Export ("shouldUseAlignment")]
		bool ShouldUseAlignment { get; set; }

		[Export ("zPosition", ArgumentSemantic.Assign)]
		PSPDFItemZPosition ZPosition { get; set; }
	}

	delegate void PSPDFRemoteContentObjectAuthenticationHandlerCallback (NSUrlSessionAuthChallengeDisposition disposition, NSUrlCredential credential);
	delegate void PSPDFRemoteContentObjectAuthenticationHandler (NSUrlAuthenticationChallenge challenge, [BlockCallback] PSPDFRemoteContentObjectAuthenticationHandlerCallback callback);
	delegate NSObject PSPDFRemoteContentObjectTransformerHandler (NSUrl location);

	interface IPSPDFRemoteContentObject { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFRemoteContentObject {

		[Abstract]
		[NullAllowed, Export ("URLRequestForRemoteContent")]
		NSUrlRequest UrlRequestForRemoteContent { get; }

		[Abstract]
		[NullAllowed, Export ("remoteContent", ArgumentSemantic.Strong)]
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

		[Export ("shouldCacheRemoteContent")]
		bool ShouldCacheRemoteContent ();

		[Export ("shouldRetryLoadingRemoteContentOnConnectionFailure")]
		bool ShouldRetryLoadingRemoteContentOnConnectionFailure ();

		[Export ("remoteContentAuthenticationChallengeBlock")]
		PSPDFRemoteContentObjectAuthenticationHandler GetRemoteContentAuthenticationChallengeHandler ();

		[Export ("remoteContentTransformerBlock")]
		PSPDFRemoteContentObjectTransformerHandler GetRemoteContentTransformerHandler ();

		[Export ("hasRemoteContent")]
		bool HasRemoteContent ();

		[Export ("completionBlock")]
		Action<PSPDFRemoteContentObject> GetCompletionHandler ();

		[Export ("setCompletionBlock:")]
		void SetCompletionHandler (Action<PSPDFRemoteContentObject> completionHandler);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRemoteFileObject : PSPDFRemoteContentObject {

		[Export ("initWithRemoteURL:targetURL:fileManager:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl remoteURL, NSUrl targetFileURL, IPSPDFFileManager fileManager);

		[Export ("remoteURL", ArgumentSemantic.Copy)]
		NSUrl RemoteUrl { get; }

		[Export ("targetURL", ArgumentSemantic.Copy)]
		NSUrl TargetUrl { get; }

		// They come from PSPDFRemoteContentObject protocol no need to bind
		//[NullAllowed, Export ("remoteContent", ArgumentSemantic.Strong)]
		//new NSUrl RemoteContent { get; set; }

		//[Export ("loadingRemoteContent")]
		//bool LoadingRemoteContent { [Bind ("isLoadingRemoteContent")] get; set; }

		//[Export ("remoteContentProgress")]
		//nfloat RemoteContentProgress { get; set; }

		//[NullAllowed, Export ("remoteContentError", ArgumentSemantic.Strong)]
		//NSError RemoteContentError { get; set; }

		//[NullAllowed, Export ("completionBlock", ArgumentSemantic.Copy)]
		//Action<PSPDFRemoteContentObject> CompletionHandler { get; set; }
	}

	[BaseType (typeof (PSPDFGoToAction))]
	interface PSPDFRemoteGoToAction {

		[Export ("initWithRelativePath:pageIndex:")]
		IntPtr Constructor ([NullAllowed] string remotePath, nuint pageIndex);

		[Export ("initWithRelativePath:namedDestination:")]
		IntPtr Constructor ([NullAllowed] string remotePath, string namedDestination);

		[NullAllowed, Export ("relativePath")]
		string RelativePath { get; }

		[NullAllowed, Export ("namedDestination")]
		string NamedDestination { get; }
	}

	interface PSPDFRenderManagerRenderResultDidChangeNotificationEventArgs {
		[Export ("PSPDFRenderManagerRenderResultChangedDocumentKey")]
		PSPDFDocument Document { get; }

		[Export ("PSPDFRenderManagerRenderResultChangedPagesKey")]
		NSIndexSet Pages { get; }

		[Export ("PSPDFPageRendererPageInfoKey")]
		PSPDFPageInfo PageInfo { get; }
	}

	interface IPSPDFRenderManager { }

	[Protocol]
	interface PSPDFRenderManager {

		[Abstract]
		[Export ("setupGraphicsContext:rectangle:pageInfo:")]
		void SetupGraphicsContext (CGContext context, CGRect displayRectangle, PSPDFPageInfo pageInfo);

		[Abstract]
		[Export ("renderQueue")]
		PSPDFRenderQueue RenderQueue { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRenderQueue {

		[Export ("scheduleTask:")]
		void Schedule (PSPDFRenderTask task);

		[Export ("scheduleTasks:")]
		void Schedule (PSPDFRenderTask [] tasks);

		// PSPDFRenderQueue (Debugging) Category

		[Advice ("You should not call this method in production code. This method should only be used for debugging.")]
		[Export ("cancelAllTasks")]
		void CancelAllTasks ();
	}

	delegate void PSPDFRenderDrawHandler (CGContext context, nuint page, CGRect cropBox, nuint rotation, [NullAllowed] NSDictionary options);

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRenderRequest : INSCopying, INSMutableCopying {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document")]
		PSPDFDocument Document { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pageIndex")]
		nuint PageIndex { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("imageSize")]
		CGSize ImageSize { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("pdfRect")]
		CGRect PdfRect { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("imageScale")]
		nfloat ImageScale { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("options", ArgumentSemantic.Copy)]
		NSDictionary WeakOptions { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("userInfo", ArgumentSemantic.Copy)]
		NSDictionary UserInfo { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("cachePolicy")]
		PSPDFRenderRequestCachePolicy CachePolicy { get; [NotImplemented ("Only available on PSPDFMutableRenderRequest")] set; }

		[Export ("isEqualRenderRequest:")]
		bool IsEqualTo (PSPDFRenderRequest renderRequest);
	}

	[BaseType (typeof (PSPDFRenderRequest))]
	interface PSPDFMutableRenderRequest {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("document", ArgumentSemantic.Strong), Override]
		PSPDFDocument Document { get; set; }

		[Export ("pageIndex"), Override]
		nuint PageIndex { get; set; }

		[Export ("imageSize", ArgumentSemantic.Assign), Override]
		CGSize ImageSize { get; set; }

		[Export ("pdfRect", ArgumentSemantic.Assign), Override]
		CGRect PdfRect { get; set; }

		[Export ("imageScale"), Override]
		nfloat ImageScale { get; set; }

		[NullAllowed, Export ("annotations", ArgumentSemantic.Copy), Override]
		PSPDFAnnotation [] Annotations { get; set; }

		[Export ("options", ArgumentSemantic.Copy), Override]
		NSDictionary WeakOptions { get; set; }

		[Export ("cachePolicy", ArgumentSemantic.Assign), Override]
		PSPDFRenderRequestCachePolicy CachePolicy { get; set; }

		[Export ("userInfo", ArgumentSemantic.Copy), Override]
		NSDictionary UserInfo { get; set; }
	}

	[Static]
	interface PSPDFRenderOptionsKeys {

		[Field ("PSPDFRenderOptionPreserveAspectRatioKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PreserveAspectRatioKey { get; }

		[Field ("PSPDFRenderOptionIgnoreDisplaySettingsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString IgnoreDisplaySettingsKey { get; }

		[Field ("PSPDFRenderOptionPageColorKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString PageColorKey { get; }

		[Field ("PSPDFRenderOptionInvertedKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString InvertedKey { get; }

		[Field ("PSPDFRenderOptionFiltersKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString FiltersKey { get; }

		[Field ("PSPDFRenderOptionInterpolationQualityKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString InterpolationQualityKey { get; }

		[Field ("PSPDFRenderOptionSkipPageContentKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SkipPageContentKey { get; }

		[Field ("PSPDFRenderOptionOverlayAnnotationsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString OverlayAnnotationsKey { get; }

		[Field ("PSPDFRenderOptionSkipAnnotationArrayKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString SkipAnnotationsKey { get; }

		[Field ("PSPDFRenderOptionIgnorePageClipKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString IgnorePageClipKey { get; }

		[Field ("PSPDFRenderOptionAllowAntiAliasingKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString AllowAntiAliasingKey { get; }

		[Field ("PSPDFRenderOptionBackgroundFillColorKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString BackgroundFillColorKey { get; }

		[Field ("PSPDFRenderOptionTextRenderingUseCoreGraphicsKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextRenderingUseCoreGraphicsKey { get; }

		[Field ("PSPDFRenderOptionTextRenderingClearTypeEnabledKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString TextRenderingClearTypeEnabledKey { get; }

		[Field ("PSPDFRenderOptionInteractiveFormFillColorKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString InteractiveFormFillColorKey { get; }

		[Field ("PSPDFRenderOptionDrawBlockKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString DrawHandlerKey { get; }

		[Field ("PSPDFRenderOptionDrawSignHereOverlay", PSPDFKitLibraryPath.LibraryPath)]
		NSString DrawSignHereOverlayKey { get; }

#if __IOS__
		[Field ("PSPDFRenderOptionCIFilterKey", PSPDFKitLibraryPath.LibraryPath)]
		NSString CIFiltersKey { get; }
#endif
	}

	[StrongDictionary ("PSPDFRenderOptionsKeys")]
	interface PSPDFRenderOptions {
		bool PreserveAspectRatio { get; set; }
		bool IgnoreDisplaySettings { get; set; }
		UIColor PageColor { get; set; }
		bool Inverted { get; set; }
		uint Filters { get; set; }
		NSNumber InterpolationQuality { get; set; }
		bool SkipPageContent { get; set; }
		bool OverlayAnnotations { get; set; }
		PSPDFAnnotation [] SkipAnnotations { get; set; }
		bool IgnorePageClip { get; set; }
		bool AllowAntiAliasing { get; set; }
		UIColor BackgroundFillColor { get; set; }
		bool TextRenderingUseCoreGraphics { get; set; }
		bool TextRenderingClearTypeEnabled { get; set; }
		UIColor InteractiveFormFillColor { get; set; }
		bool DrawSignHereOverlay { get; set; }
#if __IOS__
		[Advice ("This can be a 'CIFilter' or an 'NSArray<CIFilter>'.")]
		NSObject CIFilters { get; set; }
#endif
	}

	interface IPSPDFRenderTaskDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFRenderTaskDelegate {

		[Export ("renderTaskDidFinish:")]
		void DidFinish (PSPDFRenderTask task);

		[Export ("renderTask:didFailWithError:")]
		void DidFail (PSPDFRenderTask task, NSError error);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRenderTask {

		[Export ("initWithRequest:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFRenderRequest request);

		[Export ("request")]
		PSPDFRenderRequest Request { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFRenderTaskDelegate Delegate { get; set; }

		[NullAllowed, Export ("completionHandler", ArgumentSemantic.Copy)]
		Action<UIImage, NSError> CompletionHandler { get; set; }

		[Export ("priority", ArgumentSemantic.Assign)]
		PSPDFRenderQueuePriority Priority { get; set; }

		[NullAllowed, Export ("image")]
		UIImage Image { get; }

		[Export ("cancelled")]
		bool Cancelled { [Bind ("isCancelled")] get; }

		[Export ("completed")]
		bool Completed { [Bind ("isCompleted")] get; }

		[Export ("cancel")]
		void Cancel ();

		[Static]
		[Async]
		[Export ("groupTasks:completionHandler:")]
		void GroupTasks (PSPDFRenderTask [] tasks, Action completionHandler);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRenditionAction {

		[Export ("initWithActionType:javaScript:annotation:")]
		IntPtr Constructor (PSPDFRenditionActionType actionType, [NullAllowed] string javaScript, [NullAllowed] PSPDFScreenAnnotation annotation);

		[Export ("actionType")]
		PSPDFRenditionActionType ActionType { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFScreenAnnotation Annotation { get; }

		[NullAllowed, Export ("javaScript")]
		string JavaScript { get; }
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFResetFormAction {

		[Export ("initWithFlags:")]
		IntPtr Constructor (PSPDFResetFormActionFlag flags);

		[Export ("flags")]
		PSPDFResetFormActionFlag Flags { get; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFRichMediaAnnotation {

	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFRichMediaExecuteAction {

		[Export ("initWithCommand:argument:annotation:")]
		IntPtr Constructor ([NullAllowed] string command, [NullAllowed] NSObject argument, [NullAllowed] PSPDFRichMediaAnnotation annotation);

		[NullAllowed, Export ("command")]
		string Command { get; }

		[NullAllowed, Export ("argument", ArgumentSemantic.Copy)]
		NSObject Argument { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFRichMediaAnnotation Annotation { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFRSAKey {

		[Export ("publicKeyScheme")]
		string PublicKeyScheme { get; }

		[Export ("keyLength")]
		nint KeyLength { get; }
	}

	[BaseType (typeof (PSPDFAssetAnnotation))]
	interface PSPDFScreenAnnotation {

		[Export ("mediaScreenWindowType")]
		PSPDFMediaScreenWindowType MediaScreenWindowType { get; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFSearchResult {

		[Export ("initWithDocumentUID:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		[DesignatedInitializer]
		IntPtr Constructor (string documentUid, nuint pageIndex, NSRange range, string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("initWithDocument:pageIndex:range:previewText:rangeInPreviewText:selection:annotation:")]
		IntPtr Constructor (PSPDFDocument document, nuint pageIndex, NSRange range, string previewText, NSRange rangeInPreviewText, [NullAllowed] PSPDFTextBlock selection, [NullAllowed] PSPDFAnnotation annotation);

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("previewText")]
		string PreviewText { get; }

		[Export ("rangeInPreviewText")]
		NSRange RangeInPreviewText { get; }

		[Export ("range")]
		NSRange Range { get; }

		[NullAllowed, Export ("selection")]
		PSPDFTextBlock Selection { get; }

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFAnnotation Annotation { get; }

		[Export ("documentUID")]
		string DocumentUid { get; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFSignatureAppearanceBuilder {

		[Export ("appearanceMode")]
		PSPDFSignatureAppearanceMode AppearanceMode { get; set; }

		[Export ("showSignerName")]
		bool ShowSignerName { get; set; }

		[Export ("showSigningDate")]
		bool ShowSigningDate { get; set; }

		[Export ("showSignatureReason")]
		bool ShowSignatureReason { get; set; }

		[Export ("showSignatureLocation")]
		bool ShowSignatureLocation { get; set; }

		[NullAllowed, Export ("signatureGraphic")]
		PSPDFAnnotationAppearanceStream SignatureGraphic { get; set; }

		[Export ("reuseExistingAppearance")]
		bool ReuseExistingAppearance { get; set; }
	}

	[BaseType (typeof (PSPDFBaseConfiguration))]
	interface PSPDFSignatureAppearance {

		[Static, New]
		[Export ("defaultConfiguration")]
		PSPDFSignatureAppearance DefaultConfiguration { get; }

		[Export ("initWithBuilder:")]
		IntPtr Constructor (PSPDFSignatureAppearanceBuilder builder);

		[Static]
		[Export ("configurationWithBuilder:")]
		PSPDFSignatureAppearance FromConfigurationBuilder ([NullAllowed] Action<PSPDFSignatureAppearanceBuilder> builderHandler);

		[Static]
		[Export ("configurationUpdatedWithBuilder:")]
		PSPDFSignatureAppearance ConfigurationUpdated ([NullAllowed] Action<PSPDFSignatureAppearanceBuilder> builderHandler);

		[Export ("appearanceMode")]
		PSPDFSignatureAppearanceMode AppearanceMode { get; }

		[Export ("showSignerName")]
		bool ShowSignerName { get; }

		[Export ("showSigningDate")]
		bool ShowSigningDate { get; }

		[Export ("showSignatureReason")]
		bool ShowSignatureReason { get; }

		[Export ("showSignatureLocation")]
		bool ShowSignatureLocation { get; }

		[NullAllowed, Export ("signatureGraphic")]
		PSPDFAnnotationAppearanceStream SignatureGraphic { get; }

		[Export ("reuseExistingAppearance")]
		bool ReuseExistingAppearance { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureBiometricProperties : INSSecureCoding {

		[Export ("initWithPressureList:timePointsList:touchRadius:inputMethod:")]
		IntPtr Constructor ([NullAllowed] NSNumber [] pressureList, [NullAllowed] NSNumber [] timePointsList, [NullAllowed] NSNumber touchRadius, PSPDFSignatureInputMethod inputMethod);

		[NullAllowed, Export ("pressureList")]
		NSNumber [] PressureList { get; }

		[NullAllowed, Export ("timePointsList")]
		NSNumber [] TimePointsList { get; }

		[NullAllowed, Export ("touchRadius")]
		NSNumber TouchRadius { get; }

		[Export ("inputMethod")]
		PSPDFSignatureInputMethod InputMethod { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureContainer : INSCoding {

		[Export ("initWithAnnotation:signer:biometricProperties:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFInkAnnotation annotation, [NullAllowed] PSPDFSigner signer, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties);

		[Export ("annotation")]
		PSPDFInkAnnotation Annotation { get; }

		[NullAllowed, Export ("signer")]
		PSPDFSigner Signer { get; }

		[NullAllowed, Export ("biometricProperties")]
		PSPDFSignatureBiometricProperties BiometricProperties { get; }
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFSignatureFormElement {

		[Export ("isSigned")]
		bool IsSigned { get; }

		[NullAllowed, Export ("signatureInfo")]
		PSPDFSignatureInfo SignatureInfo { get; }

		[NullAllowed, Export ("overlappingInkSignature")]
		PSPDFInkAnnotation OverlappingInkSignature { get; }

		[Export ("signatureBiometricProperties:")]
		[return: NullAllowed]
		PSPDFSignatureBiometricProperties GetSignatureBiometricProperties (PSPDFPrivateKey privateKey);
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFSignatureFormField {

		[Static]
		[Export ("insertedSignatureFieldWithFullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFSignatureFormField Create (string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureInfo {

		[Export ("placeholderBytes")]
		nuint PlaceholderBytes { get; }

		[NullAllowed, Export ("contents", ArgumentSemantic.Copy)]
		NSData Contents { get; }

		[NullAllowed, Export ("byteRange", ArgumentSemantic.Copy)]
		NSObject [] ByteRange { get; }

		[NullAllowed, Export ("filter")]
		string Filter { get; }

		[NullAllowed, Export ("subFilter")]
		string SubFilter { get; }

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("creationDate", ArgumentSemantic.Copy)]
		NSDate CreationDate { get; }

		[NullAllowed, Export ("location")]
		string Location { get; }

		[NullAllowed, Export ("reason")]
		string Reason { get; }

		[NullAllowed, Export ("propBuild", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuild PropBuild { get; }

		[NullAllowed, Export ("references", ArgumentSemantic.Copy)]
		PSPDFDigitalSignatureReference [] References { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFSignatureManager {

		[Export ("registeredSigners")]
		PSPDFSigner [] RegisteredSigners { get; }

		[Export ("registerSigner:")]
		void RegisterSigner (PSPDFSigner signer);

		[Export ("clearRegisteredSigners")]
		void ClearRegisteredSigners ();

		[Export ("trustedCertificates")]
		PSPDFX509 [] TrustedCertificates { get; }

		[Export ("addTrustedCertificate:")]
		void AddTrustedCertificate (PSPDFX509 x509);

		[Export ("clearTrustedCertificates")]
		void ClearTrustedCertificates ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignaturePropBuild {

		[NullAllowed, Export ("filter", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry Filter { get; }

		[NullAllowed, Export ("pubSec", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry PubSec { get; }

		[NullAllowed, Export ("app", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry App { get; }

		[NullAllowed, Export ("sigQ", ArgumentSemantic.Copy)]
		PSPDFSignaturePropBuildEntry SigQ { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignaturePropBuildEntry {

		[NullAllowed, Export ("name")]
		string Name { get; }

		[NullAllowed, Export ("date")]
		string Date { get; }

		[Export ("revisionNumber")]
		nint RevisionNumber { get; }

		[NullAllowed, Export ("operatingSystem")]
		string OperatingSystem { get; }

		[Export ("preRelease")]
		bool PreRelease { get; }

		[Export ("nonEmbeddedFontNoWarning")]
		bool NonEmbeddedFontNoWarning { get; }

		[Export ("trustedMode")]
		bool TrustedMode { get; }

		[Export ("minimumVersion")]
		nint MinimumVersion { get; }

		[NullAllowed, Export ("textRevision")]
		string TextRevision { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureStatus {

		[Export ("initWithSigner:signingDate:wasModified:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] string signer, [NullAllowed] NSDate date, bool wasModified);

		[NullAllowed, Export ("signer")]
		string Signer { get; }

		[NullAllowed, Export ("signingDate", ArgumentSemantic.Copy)]
		NSDate SigningDate { get; }

		[Export ("coversEntireDocument")]
		bool CoversEntireDocument { get; }

		[Export ("problems")]
		string [] Problems { get; }

		[Export ("severity", ArgumentSemantic.Assign)]
		PSPDFSignatureStatusSeverity Severity { get; set; }

		[Export ("signatureIntegrityStatus", ArgumentSemantic.Assign)]
		PSPDFSignatureIntegrityStatus SignatureIntegrityStatus { get; }

		[Export ("summary")]
		string Summary { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSignatureValidator {

		[Export ("initWithSignatureFormElement:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFSignatureFormElement formElement);

		[Export ("signatureFormElement")]
		PSPDFSignatureFormElement SignatureFormElement { get; }

		[Export ("verifySignatureWithTrustedCertificates:error:")]
		[return: NullAllowed]
		PSPDFSignatureStatus VerifySignature ([NullAllowed] PSPDFX509 [] trustedCertificates, [NullAllowed] out NSError error);
	}

	delegate void PSPDFSignerSignFormElementHandler (bool success, PSPDFDocument document, NSError error);
	delegate void PSPDFSignerSignFormElementSinkHandler (bool success, IPSPDFDataSink document, NSError error);

	[BaseType (typeof (NSObject))]
	interface PSPDFSigner : PSPDFExternalSignature, INSCoding {

		[Export ("filter")]
		string Filter { get; }

		[Export ("subFilter")]
		string SubFilter { get; }

		[Export ("displayName")]
		string DisplayName { get; }

		[Export ("reason")]
		string Reason { get; }

		[Export ("location")]
		string Location { get; }

		[NullAllowed, Export ("externalSignatureDelegate", ArgumentSemantic.Weak)]
		IPSPDFExternalSignature ExternalSignatureDelegate { get; set; }

		[Async]
		[Export ("requestSigningCertificate:completionBlock:")]
		void RequestSigningCertificate (NSObject sourceController, [NullAllowed] Action<PSPDFX509, NSError> completion);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementResult")]
		[Export ("signFormElement:withCertificate:writeTo:appearance:biometricProperties:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 certificate, string path, [NullAllowed] PSPDFSignatureAppearance signatureAppearance, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties, [NullAllowed] PSPDFSignerSignFormElementHandler completion);

		[Async (ResultTypeName = "PSPDFSignerSignFormElementSinkResult")]
		[Export ("signFormElement:withCertificate:writeToDataSink:appearance:biometricProperties:completionBlock:")]
		[Advice ("Requires base call if override.")]
		void SignFormElement (PSPDFSignatureFormElement element, PSPDFX509 certificate, IPSPDFDataSink dataSink, [NullAllowed] PSPDFSignatureAppearance signatureAppearance, [NullAllowed] PSPDFSignatureBiometricProperties biometricProperties, [NullAllowed] PSPDFSignerSignFormElementSinkHandler completion);
	}

	[DisableDefaultCtor]
	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFSoundAnnotation {

		[Static]
		[Export ("recordingAnnotationAvailable")]
		bool RecordingAnnotationAvailable { get; }

		[Export ("initWithRecorder")]
		IntPtr Constructor ();

		[Export ("initWithRecorderOptions:")]
		IntPtr Constructor ([NullAllowed] NSDictionary options);

		[Export ("initWithURL:error:")]
		IntPtr Constructor (NSUrl soundUrl, [NullAllowed] out NSError error);

		[Export ("controller")]
		PSPDFSoundAnnotationController Controller { get; }

		[NullAllowed, Export ("iconName")]
		string IconName { get; set; }

		[Export ("canRecord")]
		bool CanRecord { get; }

		[NullAllowed, Export ("soundURL", ArgumentSemantic.Copy)]
		NSUrl SoundUrl { get; }

		[Export ("bits")]
		nuint Bits { get; }

		[Export ("rate")]
		nuint Rate { get; }

		[Export ("channels")]
		nuint Channels { get; }

		[NullAllowed, Export ("encoding")]
		NSString WeakEncoding { get; }

		[Wrap ("PSPDFSoundAnnotationEncodingExtensions.GetValue (WeakEncoding)")]
		PSPDFSoundAnnotationEncoding Encoding { get; }

		[Export ("loadAttributesFromAudioFile:")]
		bool LoadAttributesFromAudioFile ([NullAllowed] out NSError error);

		[NullAllowed, Export ("soundData")]
		NSData SoundData { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFSoundAnnotationController {

		[Field ("PSPDFSoundAnnotationChangedStateNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString ChangedStateNotification { get; }

		[Field ("PSPDFSoundAnnotationStopAllNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString StopAllNotification { get; }

		[Static]
		[Export ("stopRecordingOrPlaybackForAllExcept:")]
		void StopRecordingOrPlaybackForAllExcept ([NullAllowed] NSObject except);

		[Async]
		[Static]
		[Export ("requestRecordPermission:")]
		void RequestRecordPermission ([NullAllowed] Action<bool> handler);

		[Export ("initWithSoundAnnotation:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFSoundAnnotation annotation);

		[NullAllowed, Export ("annotation", ArgumentSemantic.Weak)]
		PSPDFSoundAnnotation Annotation { get; }

		[Export ("state")]
		PSPDFSoundAnnotationState State { get; }

		[Export ("playbackDuration")]
		double PlaybackDuration { get; }

		[Export ("startPlayback:")]
		bool StartPlayback ([NullAllowed] out NSError error);

		[Export ("pause")]
		void Pause ();

		[Export ("stop:")]
		bool Stop ([NullAllowed] out NSError error);

#if !__WATCH__

		[NullAllowed, Export ("audioPlayer")]
		AVAudioPlayer AudioPlayer { get; }
#endif

		[Export ("startRecording:")]
		bool StartRecording ([NullAllowed] out NSError error);

		[Export ("discardRecording")]
		void DiscardRecording ();
	}

	[BaseType (typeof (PSPDFAbstractShapeAnnotation))]
	interface PSPDFSquareAnnotation {

		[Export ("bezierPath")]
		UIBezierPath BezierPath { get; }
	}

	[BaseType (typeof (PSPDFMarkupAnnotation))]
	interface PSPDFSquigglyAnnotation {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		[return: NullAllowed]
		PSPDFSquigglyAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs, nint pageRotation);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFSquigglyAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFStampAnnotation : PSPDFRotatable {

		[Static]
		[Export ("stampColorForSubject:")]
		UIColor GetStampColor ([NullAllowed] string subject);

		[Export ("initWithSubject:")]
		IntPtr Constructor ([NullAllowed] string subject);

		[Export ("initWithImage:")]
		IntPtr Constructor ([NullAllowed] UIImage image);

		[NullAllowed, Export ("subtext")]
		string Subtext { get; set; }

		[NullAllowed, Export ("localizedSubject")]
		string LocalizedSubject { get; set; }

		[NullAllowed, Export ("image", ArgumentSemantic.Strong)]
		UIImage Image { get; set; }

		[Export ("loadImageWithTransform:error:")]
		[return: NullAllowed]
		UIImage LoadImage ([NullAllowed] CGAffineTransform transform, [NullAllowed] out NSError error);

		[Export ("imageTransform", ArgumentSemantic.Assign)]
		CGAffineTransform ImageTransform { get; set; }

		[Export ("sizeThatFits:")]
		CGSize GetSizeThatFits (CGSize size);

		[Export ("sizeToFit")]
		void SizeToFit ();
	}

	[BaseType (typeof (PSPDFMarkupAnnotation))]
	interface PSPDFStrikeOutAnnotation {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		[return: NullAllowed]
		PSPDFStrikeOutAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs, nint pageRotation);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFStrikeOutAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (PSPDFAbstractFormAction))]
	interface PSPDFSubmitFormAction {

		[Export ("initWithURL:flags:")]
		IntPtr Constructor ([NullAllowed] NSUrl url, PSPDFSubmitFormActionFlag flags);

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[Export ("flags")]
		PSPDFSubmitFormActionFlag Flags { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFTextBlock : INSCopying, INSSecureCoding {

		[Export ("initWithGlyphs:frame:")]
		IntPtr Constructor (PSPDFGlyph[] glyphs, CGRect frame);

		[Export ("initWithRange:text:frame:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSRange textRange, string text, CGRect frame);

		[Export ("frame")]
		CGRect Frame { get; }

		[Export ("range")]
		NSRange Range { get; }

		[Export ("words", ArgumentSemantic.Copy)]
		PSPDFWord [] Words { get; }

		[Export ("content")]
		string Content { get; }

		[Export ("isEqualToTextBlock:")]
		bool IsEqualTo (PSPDFTextBlock otherBlock);
	}

	[BaseType (typeof (PSPDFFormElement))]
	interface PSPDFTextFieldFormElement {

		[Export ("multiline")]
		bool Multiline { [Bind ("isMultiline")] get; }

		[Export ("password")]
		bool Password { [Bind ("isPassword")] get; }

		[NullAllowed, Export ("formattedContents")]
		string FormattedContents { get; }

		[Export ("inputFormat")]
		PSPDFTextInputFormat InputFormat { get; }

		[NullAllowed, Export ("textFormField")]
		PSPDFTextFormField TextFormField { get; }
	}

	[BaseType (typeof (PSPDFFormField))]
	[DisableDefaultCtor]
	interface PSPDFTextFormField {

		[Static]
		[Export ("insertedTextFieldWithFullyQualifiedName:documentProvider:formElement:error:")]
		[return: NullAllowed]
		PSPDFTextFormField Create (string fullyQualifiedName, PSPDFDocumentProvider documentProvider, PSPDFFormElement formElement, [NullAllowed] out NSError error);

		[Export ("isMultiLine")]
		bool IsMultiLine { get; set; }

		[Export ("isPassword")]
		bool IsPassword { get; set; }

		[Export ("isComb")]
		bool IsComb { get; set; }

		[Export ("doNotScroll")]
		bool DoNotScroll { get; set; }

		[Export ("isRichText")]
		bool IsRichText { get; set; }

		[Export ("doNotSpellCheck")]
		bool DoNotSpellCheck { get; set; }

		[Export ("fileSelect")]
		bool FileSelect { get; set; }

		[NullAllowed, Export ("text")]
		string Text { get; set; }

		[NullAllowed, Export ("richText")]
		string RichText { get; set; }

		[Export ("maxLength")]
		nuint MaxLength { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
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

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; }

		[Export ("textWithGlyphs:")]
		string GetText (PSPDFGlyph [] glyphs);

		[Export ("glyphsInRange:")]
		PSPDFGlyph [] GetGlyphs (NSRange range);
	}

	interface IPSPDFTextSearchDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFTextSearchDelegate {

		[Export ("willStartSearch:term:isFullSearch:")]
		void WillStartSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);

		[Export ("didUpdateSearch:term:newSearchResults:pageIndex:")]
		void DidUpdateSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, nuint pageIndex);

		[Export ("didFailSearch:withError:")]
		void DidFailSearch (PSPDFTextSearch textSearch, NSError error);

		[Export ("didFinishSearch:term:searchResults:isFullSearch:pageTextFound:")]
		void DidFinishSearch (PSPDFTextSearch textSearch, string searchTerm, PSPDFSearchResult [] searchResults, bool isFullSearch, bool pageTextFound);

		[Export ("didCancelSearch:term:isFullSearch:")]
		void DidCancelSearch (PSPDFTextSearch textSearch, string searchTerm, bool isFullSearch);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFTextSearch : INSCopying {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[Export ("searchForString:")]
		void Search (string searchTerm);

		[Export ("searchForString:inRanges:rangesOnly:cancelOperations:")]
		void Search (string searchTerm, [NullAllowed] NSIndexSet ranges, bool rangesOnly, bool cancelOperations);

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

		[Export ("maximumNumberOfSearchResults")]
		nuint MaximumNumberOfSearchResults { get; set; }

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFTextSearchDelegate Delegate { get; set; }

		// PSPDFTextSearch (SubclassingHooks) Category

		[Export ("searchQueue")]
		NSOperationQueue SearchQueue { get; }
	}

	[BaseType (typeof (PSPDFMarkupAnnotation))]
	interface PSPDFUnderlineAnnotation {

		[Static]
		[Export ("textOverlayAnnotationWithGlyphs:pageRotation:")]
		[return: NullAllowed]
		PSPDFUnderlineAnnotation FromGlyphs ([NullAllowed] PSPDFGlyph[] glyphs, nint pageRotation);

		[Static]
		[Export ("textOverlayAnnotationWithRects:boundingBox:pageIndex:")]
		[return: NullAllowed]
		PSPDFUnderlineAnnotation FromRects ([BindAs (typeof (CGRect[]))] NSValue[] rects, CGRect boundingBox, nuint pageIndex);
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFUndoController {

		[Field ("PSPDFUndoControllerAddedUndoActionNotification", PSPDFKitLibraryPath.LibraryPath)]
		[Notification]
		NSString AddedUndoActionNotification { get; }

		[Field ("PSPDFUndoControllerRemovedUndoActionNotification", PSPDFKitLibraryPath.LibraryPath)]
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
		void EndUndoGrouping (string changedProperty, [NullAllowed] NSObject @object);

		[Export ("removeAllActions")]
		void RemoveAllActions ();

		[Export ("removeAllActionsWithTarget:")]
		void RemoveAllActions (NSObject target);

		[Export ("registerObjectForUndo:")]
		void RegisterObjectForUndo (IPSPDFUndoSupport @object);

		[Export ("unregisterObjectForUndo:")]
		void UnregisterObjectForUndo (IPSPDFUndoSupport @object);

		[Export ("isObjectRegisteredForUndo:")]
		bool IsObjectRegisteredForUndo (IPSPDFUndoSupport @object);

		[Export ("prepareWithInvocationTarget:block:")]
		void Prepare (NSObject invocationTarget, Action<NSObject> handler);

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

		[NullAllowed, Export ("incompleteUndoActionName")]
		string IncompleteUndoActionName { get; }
	}

	interface IPSPDFUndoSupport { }

	[Protocol]
	interface PSPDFUndoSupport {

		// HACK: The following 3 static methods need to be manually inlined in any object implementing this protocol
		//[Static]
		//[Export ("keysForValuesToObserveForUndo")]
		//NSSet<NSString> GetKeysForValuesToObserveForUndo ();

		//[Static]
		//[Export ("localizedUndoActionNameForKey:")]
		//[return: NullAllowed]
		//string LocalizedUndoActionName (string key);

		//[Static]
		//[Export ("undoCoalescingForKey:")]
		//PSPDFUndoCoalescing GetUndoCoalescing (string key);

		[Export ("insertUndoObjects:forKey:")]
		void InsertUndoObjects (NSSet objects, string key);

		[Export ("removeUndoObjects:forKey:")]
		void RemoveUndoObjects (NSSet objects, string key);

		[Export ("didUndoOrRedoChange:")]
		void DidUndoOrRedoChange (string key);

		[Export ("performUndoAction:")]
		void PerformUndoAction (NSObject action);
	}

	[BaseType (typeof (PSPDFAction))]
	interface PSPDFURLAction {

		[Export ("initWithURLString:")]
		IntPtr Constructor (string urlString);

		[Export ("initWithURL:options:")]
		IntPtr Constructor ([NullAllowed] NSUrl url, [NullAllowed] NSDictionary<NSString, NSObject> options);

		[NullAllowed, Export ("URL", ArgumentSemantic.Copy)]
		NSUrl Url { get; }

		[NullAllowed, Export ("unmodifiedURL", ArgumentSemantic.Copy)]
		NSUrl UnmodifiedUrl { get; }

		[Export ("updateURLWithAnnotationManager:")]
		bool UpdateUrl (PSPDFAnnotationManager annotationManager);

		[Export ("isPSPDFPrefixed")]
		bool IsPSPDFPrefixed { get; }

		[Export ("pageIndex")]
		nuint PageIndex { get; set; }

		[Export ("modal")]
		bool Modal { [Bind ("isModal")] get; set; }

		[Export ("popover")]
		bool Popover { [Bind ("isPopover")] get; set; }

		[Export ("button")]
		bool Button { [Bind ("isButton")] get; set; }

		[Export ("size", ArgumentSemantic.Assign)]
		CGSize Size { get; set; }

		[Export ("offset")]
		nfloat Offset { get; set; }

		[Export ("prefixedURLStringWithAnnotationManager:")]
		[return: NullAllowed]
		string GetPrefixedUrlString (PSPDFAnnotationManager annotationManager);

		[Export ("emailURL")]
		bool EmailUrl { [Bind ("isEmailURL")] get; }

		[Export ("localPDFURL")]
		bool LocalPdfUrl { [Bind ("isLocalPDFURL")] get; }

#if __IOS__
		[Export ("configureMailComposeViewController:")]
		bool ConfigureMailComposeViewController (MFMailComposeViewController mailComposeViewController);
#endif
	}

	[BaseType (typeof (PSPDFAnnotation))]
	interface PSPDFWidgetAnnotation {

		[NullAllowed, Export ("action", ArgumentSemantic.Strong)]
		PSPDFAction Action { get; set; }

		[NullAllowed, Export ("borderColor", ArgumentSemantic.Strong), New]
		UIColor BorderColor { get; set; }

		[Export ("widgetRotation")]
		nint WidgetRotation { get; set; }

		[NullAllowed, Export ("appearanceCharacteristics", ArgumentSemantic.Strong)]
		PSPDFAppearanceCharacteristics AppearanceCharacteristics { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFWord : INSCopying, INSSecureCoding {

		[Export ("initWithGlyphs:frame:")]
		IntPtr Constructor (PSPDFGlyph [] wordGlyphs, CGRect frame);

		[Export ("initWithRange:text:frame:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSRange textRange, string text, CGRect frame);

		[Export ("stringValue")]
		string StringValue { get; }

		[Export ("frame", ArgumentSemantic.Assign)]
		CGRect Frame { get; set; }

		[Export ("range")]
		NSRange Range { get; }

		[Export ("lineBreaker")]
		bool LineBreaker { get; set; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFX509 {

		[Static]
		[Export ("adobeCA")]
		PSPDFX509 AdobeCA ();

		[Static]
		[Export ("certificatesFromPKCS7Data:error:")]
		[return: NullAllowed]
		PSPDFX509 [] GetCertificates (NSData pkcs7Data, [NullAllowed] out NSError error);

		[Export ("publicKey")]
		PSPDFRSAKey PublicKey { get; }

		[NullAllowed, Export ("commonName")]
		string CommonName { get; }
	}

	[BaseType (typeof (PSPDFContainerAnnotationProvider))]
	interface PSPDFXFDFAnnotationProvider {

		[Export ("initWithDocumentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider);

		[Export ("initWithDocumentProvider:fileURL:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, NSUrl xfdfFileUrl);

		[Export ("initWithDocumentProvider:dataProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocumentProvider documentProvider, IPSPDFDataProviding dataProvider);

		[NullAllowed, Export ("fileURL", ArgumentSemantic.Copy)]
		NSUrl FileUrl { get; }

		[NullAllowed, Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }

		[Export ("loadAllAnnotations")]
		void LoadAllAnnotations ();
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFXFDFParser {

		[Export ("initWithDataProvider:documentProvider:")]
		[DesignatedInitializer]
		IntPtr Constructor (IPSPDFDataProviding dataProvider, PSPDFDocumentProvider documentProvider);

		[Export ("parseWithError:")]
		[return: NullAllowed]
		PSPDFAnnotation [] Parse ([NullAllowed] out NSError error);

		[Export ("annotations", ArgumentSemantic.Copy)]
		PSPDFAnnotation [] Annotations { get; }

		[Export ("parsingEnded")]
		bool ParsingEnded { get; }

		[NullAllowed, Export ("documentProvider", ArgumentSemantic.Weak)]
		PSPDFDocumentProvider DocumentProvider { get; }

		[Export ("dataProvider")]
		IPSPDFDataProviding DataProvider { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFXFDFWriter {

		[Export ("writeAnnotations:toDataSink:documentProvider:error:")]
		bool WriteAnnotations (PSPDFAnnotation [] annotations, IPSPDFDataSink dataSink, PSPDFDocumentProvider documentProvider, [NullAllowed] out NSError error);
	}

	[BaseType (typeof (NSFormatter))]
	interface PSPDFPageLabelFormatter {

		[Static]
		[Export ("localizedStringFromPageRange:document:")]
		string GetLocalizedString (NSRange pageRange, PSPDFDocument document);

		[NullAllowed, Export ("document")]
		PSPDFDocument Document { get; set; }

		[Export ("stringFromRange:")]
		string GetString (NSRange pageRange);
	}

	interface IPSPDFDocumentFeaturesSource { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFDocumentFeaturesSource {

		[Abstract]
		[NullAllowed, Export ("features", ArgumentSemantic.Weak)]
		PSPDFDocumentFeatures Features { get; set; }

		[Export ("canModify")]
		bool GetCanModify ();

		[Export ("canEditBookmarks")]
		bool GetCanEditBookmarks ();

		[Export ("canPrint")]
		bool GetCanPrint ();

		[Export ("canShowAnnotationReplies")]
		bool GetCanShowAnnotationReplies ();
	}

	interface IPSPDFDocumentFeaturesObserver { }

	[Protocol]
	interface PSPDFDocumentFeaturesObserver {

		[Abstract]
		[Export ("bindToObjectLifetime:")]
		void BindToObjectLifetime (NSObject @object);
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFDocumentFeatures : PSPDFDocumentFeaturesSource {

		[Export ("initWithDocument:")]
		[DesignatedInitializer]
		IntPtr Constructor (PSPDFDocument document);

		[NullAllowed, Export ("document", ArgumentSemantic.Weak)]
		PSPDFDocument Document { get; }

		[Export ("addSources:")]
		void AddSources (IPSPDFDocumentFeaturesSource [] sources);

		[Export ("removeSources:")]
		void RemoveSources (IPSPDFDocumentFeaturesSource [] sources);

		[Export ("updateFeatures")]
		void UpdateFeatures ();

		[Export ("addObserverForFeature:updateHandler:")]
		IPSPDFDocumentFeaturesObserver AddObserver (Selector feature, Action<bool> handler);

		[Export ("removeObserver:")]
		void RemoveObserver (IPSPDFDocumentFeaturesObserver observer);

		[Export ("traceFeature:")]
		void TraceFeature (Selector selector);
	}

	interface IPSPDFExternalSignature { }

	[Protocol]
	interface PSPDFExternalSignature {

		[Abstract]
		[Export ("signData:hashAlgorithm:")]
		NSData SignData (NSData data, PSPDFSignatureHashAlgorithm hashAlgorithm);

		[Abstract]
		[Export ("encryptionAlgorithm")]
		PSPDFSignatureEncryptionAlgorithm EncryptionAlgorithm { get; }
	}

	delegate string PSPDFAESCryptoDataSinkPassphraseProvider ();

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFAESCryptoDataSink : PSPDFDataSink {

		[Export ("initWithUID:passphraseProvider:options:")]
		[DesignatedInitializer]
		IntPtr Constructor (string uid, PSPDFAESCryptoDataSinkPassphraseProvider passphraseProvider, PSPDFDataSinkOptions options);

		[Export ("initWithURL:passphraseProvider:options:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl fileUrl, PSPDFAESCryptoDataSinkPassphraseProvider passphraseProvider, PSPDFDataSinkOptions options);
	}

	[BaseType (typeof (PSPDFBaseConfigurationBuilder))]
	interface PSPDFDocumentEditorConfigurationBuilder {

		[Export ("pageTemplates", ArgumentSemantic.Copy)]
		PSPDFPageTemplate [] PageTemplates { get; set; }

		[NullAllowed, Export ("currentDocumentPageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize CurrentDocumentPageSize { get; set; }

		[Export ("pageSizes", ArgumentSemantic.Strong)]
		PSPDFPageSize [] PageSizes { get; set; }

		[NullAllowed, Export ("currentDocumentDirectory", ArgumentSemantic.Strong)]
		PSPDFDirectory CurrentDocumentDirectory { get; set; }

		[Export ("saveDirectories", ArgumentSemantic.Strong)]
		PSPDFDirectory [] SaveDirectories { get; set; }

		[Export ("compressions", ArgumentSemantic.Strong)]
		PSPDFCompression [] Compressions { get; set; }

		[NullAllowed, Export ("selectedTemplate", ArgumentSemantic.Strong)]
		PSPDFPageTemplate SelectedTemplate { get; set; }

		[NullAllowed, Export ("selectedPageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize SelectedPageSize { get; set; }

		[Export ("selectedOrientation", ArgumentSemantic.Assign)]
		PSPDFDocumentOrientation SelectedOrientation { get; set; }

		[NullAllowed, Export ("selectedColor", ArgumentSemantic.Strong)]
		UIColor SelectedColor { get; set; }

		[NullAllowed, Export ("selectedImage", ArgumentSemantic.Strong)]
		UIImage SelectedImage { get; set; }

		[NullAllowed, Export ("selectedImagePageSize", ArgumentSemantic.Strong)]
		PSPDFPageSize SelectedImagePageSize { get; set; }

		[NullAllowed, Export ("selectedCompression", ArgumentSemantic.Strong)]
		PSPDFCompression SelectedCompression { get; set; }

		[NullAllowed, Export ("selectedSaveDirectory", ArgumentSemantic.Strong)]
		PSPDFDirectory SelectedSaveDirectory { get; set; }

		[Export ("userFacingCompressionEnabled")]
		bool UserFacingCompressionEnabled { get; set; }

		[Export ("allowExternalFileSource")]
		bool AllowExternalFileSource { get; set; }
	}

	[BaseType (typeof (PSPDFModel))]
	interface PSPDFPageTemplate {
		
		[Internal]
		[Export ("initWithDocument:sourcePageIndex:")]
		IntPtr InitWithDocument (PSPDFDocument document, nuint sourcePageIndex);

		[Internal]
		[Export ("initWithTiledPatternFromDocument:sourcePageIndex:")]
		IntPtr InitWithTiledPattern (PSPDFDocument document, nuint sourcePageIndex);

		[Export ("initWithPageType:identifier:")]
		IntPtr Constructor (PSPDFNewPageType pageType, [BindAs (typeof (PSPDFTemplateIdentifier))] [NullAllowed] NSString identifier);

		[Async]
		[Export ("renderThumbnailWithSize:completion:")]
		void RenderThumbnail (CGSize size, Action<UIImage, NSError> completion);

		[Export ("pageType")]
		PSPDFNewPageType PageType { get; }

		[BindAs (typeof (PSPDFTemplateIdentifier))]
		[NullAllowed, Export ("identifier")]
		NSString Identifier { get; }

		[NullAllowed, Export ("templateName")]
		string TemplateName { get; set; }

		[Export ("localizedName")]
		string LocalizedName { get; }

		[NullAllowed, Export ("resourceURL")]
		NSUrl ResourceUrl { get; }

		[NullAllowed, Export ("sourceDocument")]
		PSPDFDocument SourceDocument { get; }

		[Export ("sourcePageIndex")]
		nuint SourcePageIndex { get; }

		// PSPDFPageTemplate (Convenience)

		[Static]
		[Export ("defaultTemplates")]
		PSPDFPageTemplate [] DefaultTemplates { get; }

		[Static]
		[Export ("blankTemplate")]
		PSPDFPageTemplate BlankTemplate { get; }
	}

	[BaseType (typeof (NSObject))]
	interface PSPDFRetainExistingAppearanceStreamGenerator : PSPDFAppearanceStreamGenerating {

	}

	[Protocol]
	interface PSPDFRotatable {

		[Abstract]
		[Export ("rotation")]
		nuint Rotation { get; set; }
	}
}
