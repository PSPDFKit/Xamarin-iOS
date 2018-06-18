﻿using System;

using UIKit;
using Foundation;
using ObjCRuntime;
using CoreGraphics;
using PSPDFKit.Core;
using PSPDFKit.UI;

namespace PSPDFKit.Instant {

	[Native]
	//[ErrorDomain ("PSPDFInstantErrorDomain")]
	public enum PSPDFInstantError : long {
		Unknown = 1,
		FeatureUnsupported = 2,
		InvalidDocument = 3,
		AccessDenied = 4,
		AlreadyDownloaded = 5,
		DatabaseAccessFailed = 6,
		CouldNotWriteToDisk = 7,
		InvalidUrl = 8,
		SavingDisabled = 9,
		UnmanagedDocument = 10,
		NoSuchAnnotation = 11,
		UnmanagedAnnotation = 12,
		CouldNotReadFromDiskCache = 13,
		CouldNotRemoveDiskCacheEntries = 14,
		RequestFailed = 16,
		InvalidServerData = 17,
		InvalidRequest = 18,
		OldClient = 21,
		OldServer = 22,
		InvalidJwt = 32,
		UserIdMismatch = 33,
	}

	[Native]
	public enum PSPDFInstantDocumentState : long {
		Unknown,
		Clean,
		Dirty,
		SendingChanges,
		ReceivingChanges,
		Invalid,
	}

	[Flags]
	[Native]
	public enum PSPDFInstantCacheEntryState : ulong {
		Corrupted = 1uL << 0,
		Unreferenced = 1uL << 1,
		LayerAbsurdity = 1uL << 2,
	}

	[Static]
	interface PSPDFInstantErrorKeys {

		[Field ("PSPDFInstantErrorDomain", PSPDFKitGlobal.LibraryPath)]
		NSString ErrorDomain { get; }

		[Field ("PSPDFInstantErrorDocumentDescriptorKey", PSPDFKitGlobal.LibraryPath)]
		NSString ErrorDocumentDescriptorKey { get; }

		[Field ("PSPDFInstantErrorDocumentKey", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentKey { get; }

		[Field ("PSPDFInstantErrorAnnotationIdentifierKey", PSPDFKitGlobal.LibraryPath)]
		NSString AnnotationIdentifierKey { get; }

		[Field ("PSPDFInstantErrorSQLiteExtendedErrorCodeKey", PSPDFKitGlobal.LibraryPath)]
		NSString SQLiteExtendedErrorCodeKey { get; }
	}

	interface PSPDFInstantErrorNotificationEventArgs {
		[Export ("PSPDFInstantErrorKey")]
		NSError Error { get; }
	}

	interface PSPDFInstantDidFinishReauthenticationNotificationEventArgs {
		[Export ("PSPDFInstantJWTKey")]
		string Jwt { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFInstantClient {

		[Field ("PSPDFInstantSyncingLocalChangesDisabled", PSPDFKitGlobal.LibraryPath)]
		double SyncingLocalChangesDisabled { get; }

		[Field ("PSPDFInstantDidFinishDownloadNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFinishDownloadNotification { get; }

		[Field ("PSPDFInstantDidFailDownloadNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailDownloadNotification { get; }

		[Field ("PSPDFInstantDidBeginSyncingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidBeginSyncingNotification { get; }

		[Field ("PSPDFInstantSyncCycleDidChangeStateNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString SyncCycleDidChangeStateNotification { get; }

		[Field ("PSPDFInstantDidFailSyncingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailSyncingNotification { get; }

		[Field ("PSPDFInstantDidFailAuthenticationNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFailAuthenticationNotification { get; }

		[Field ("PSPDFInstantDidFinishSyncingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFinishSyncingNotification { get; }

		[Field ("PSPDFInstantDidFinishReauthenticationNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantDidFinishReauthenticationNotificationEventArgs))]
		NSString DidFinishReauthenticationNotification { get; }

		[Field ("PSPDFInstantDidFailReauthenticationNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailReauthenticationNotification { get; }

		[Static]
		[Export ("dataDirectory")]
		NSUrl DataDirectory { get; }

		[Export ("initWithServerURL:error:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl serverUrl, [NullAllowed] out NSError error);

		[Export ("serverURL")]
		NSUrl ServerUrl { get; }

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFInstantClientDelegate Delegate { get; set; }

		[Export ("documentDescriptorForJWT:error:")]
		[return: NullAllowed]
		IPSPDFInstantDocumentDescriptor GetDocumentDescriptor (string jwt, [NullAllowed] out NSError error);

		[Export ("localDocumentDescriptors:")]
		[return: NullAllowed]
		NSDictionary<NSString, NSSet<IPSPDFInstantDocumentDescriptor>> GetLocalDocumentDescriptors ([NullAllowed] out NSError error);

		[Export ("removeUnreferencedCacheEntries:")]
		[return: NullAllowed]
		NSSet<NSString> RemoveUnreferencedCacheEntries ([NullAllowed] out NSError error);

		[Export ("listCacheEntries:")]
		[return: NullAllowed]
		NSSet<IPSPDFInstantDocumentCacheEntry> ListCacheEntries ([NullAllowed] out NSError error);

		[Export ("removeLocalStorageForDocumentIdentifier:error:")]
		bool RemoveLocalStorage (string documentIdentifier, [NullAllowed] out NSError error);

		[Export ("removeLocalStorageWithError:")]
		bool RemoveLocalStorage ([NullAllowed] out NSError error);
	}

	interface IPSPDFInstantClientDelegate { }

	[Protocol, Model (AutoGeneratedName = true)]
	[BaseType (typeof (NSObject))]
	interface PSPDFInstantClientDelegate {

		[Abstract]
		[Export ("instantClient:didFinishDownloadForDocumentDescriptor:")]
		void DidFinishDownload (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Abstract]
		[Export ("instantClient:documentDescriptor:didFailDownloadWithError:")]
		void DidFailDownload (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);

		[Abstract]
		[Export ("instantClient:didFailAuthenticationForDocumentDescriptor:")]
		void DidFailAuthentication (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Abstract]
		[Export ("instantClient:documentDescriptor:didFinishReauthenticationWithJWT:")]
		void DidFinishReauthentication (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, string validJwt);

		[Abstract]
		[Export ("instantClient:documentDescriptor:didFailReauthenticationWithError:")]
		void DidFailReauthentication (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);

		[Export ("instantClient:didBeginSyncForDocumentDescriptor:")]
		void DidBeginSync (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Export ("instantClient:didChangeSyncStateForDocumentDescriptor:")]
		void DidChangeSyncState (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Export ("instantClient:documentDescriptor:didFailSyncWithError:")]
		void DidFailSync (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);

		[Export ("instantClient:didFinishSyncForDocumentDescriptor:")]
		void DidFinishSync (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);
	}

	interface IPSPDFInstantDocumentDescriptor : INativeObject { }

	[Protocol]
	interface PSPDFInstantDocumentDescriptor {

		[Abstract]
		[Export ("identifier")]
		string Identifier { get; }

		[Abstract]
		[Export ("layerName")]
		string LayerName { get; }

		[Abstract]
		[NullAllowed, Export ("userID")]
		string UserId { get; }

		[Abstract]
		[Export ("downloaded")]
		bool Downloaded { [Bind ("isDownloaded")] get; }

		[Abstract]
		[Export ("documentState")]
		PSPDFInstantDocumentState DocumentState { get; }

		[Abstract]
		[Export ("downloadUsingJWT:error:")]
		bool Download (string jwt, [NullAllowed] out NSError error);

		[Abstract]
		[NullAllowed, Export ("downloadProgress")]
		NSProgress DownloadProgress { get; }

		[Abstract]
		[Export ("editableDocument")]
		PSPDFDocument EditableDocument { get; }

		[Abstract]
		[Export ("readOnlyDocument")]
		PSPDFDocument GetReadOnlyDocument ();

		[Abstract]
		[Export ("removeLocalStorageWithError:")]
		bool RemoveLocalStorage ([NullAllowed] out NSError error);

		[Abstract]
		[Export ("reauthenticateWithJWT:")]
		void Reauthenticate (string jwt);

		[Abstract]
		[Export ("identifierForAnnotation:error:")]
		[return: NullAllowed]
		string GetIdentifier (PSPDFAnnotation annotation, [NullAllowed] out NSError error);

		[Abstract]
		[Export ("annotationWithIdentifier:forDocument:error:")]
		[return: NullAllowed]
		PSPDFAnnotation GetAnnotation (string identifier, PSPDFDocument document, [NullAllowed] out NSError error);

		[Abstract]
		[Export ("sync")]
		void Sync ();

		[Abstract]
		[Export ("stopSyncing:")]
		void StopSyncing (bool cancelCurrentRequest);

		[Abstract]
		[Export ("delayForSyncingLocalChanges")]
		double DelayForSyncingLocalChanges { get; set; }

		[Abstract]
		[Export ("startListeningForServerChanges")]
		void StartListeningForServerChanges ();

		[Abstract]
		[Export ("stopListeningForServerChanges")]
		void StopListeningForServerChanges ();
	}

	[BaseType (typeof (PSPDFViewController))]
	interface PSPDFInstantViewController {

		[Export ("initWithDocument:configuration:")]
		[DesignatedInitializer]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document, [NullAllowed] PSPDFConfiguration configuration);

		[Export ("initWithDocument:")]
		IntPtr Constructor ([NullAllowed] PSPDFDocument document);

		[Export ("shouldListenForServerChangesWhenVisible")]
		bool ShouldListenForServerChangesWhenVisible { get; set; }

		[Export ("syncChanges:")]
		void SyncChanges ([NullAllowed] NSObject sender);

		[Export ("shouldShowCriticalErrors")]
		bool ShouldShowCriticalErrors { get; set; }
	}

	interface IPSPDFInstantDocumentCacheEntry : INativeObject { }

	[Protocol]
	interface PSPDFInstantDocumentCacheEntry {

		[Abstract]
		[Export ("documentIdentifier")]
		string DocumentIdentifier { get; }

		[Abstract]
		[Export ("overallDiskSpace")]
		ulong OverallDiskSpace { get; }

		[Abstract]
		[Export ("downloadedLayerNames")]
		NSSet<NSString> DownloadedLayerNames { get; }

		[Abstract]
		[Export ("entryState")]
		PSPDFInstantCacheEntryState EntryState { get; }
	}
}
