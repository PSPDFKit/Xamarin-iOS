using System;

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
		SavingDisabled = 9,
		RequestFailed = 16,
		InvalidServerData = 17,
		InvalidRequest = 18
	}

	[Static]
	interface PSPDFInstantErrorKeys {

		[Field ("PSPDFInstantErrorDomain", PSPDFKitGlobal.LibraryPath)]
		NSString ErrorDomain { get; }

		[Field ("PSPDFInstantErrorDocumentDescriptorKey", PSPDFKitGlobal.LibraryPath)]
		NSString ErrorDocumentDescriptorKey { get; }

		[Field ("PSPDFInstantErrorDocumentKey", PSPDFKitGlobal.LibraryPath)]
		NSString DocumentKey { get; }

		[Field ("PSPDFInstantErrorSQLiteExtendedErrorCodeKey", PSPDFKitGlobal.LibraryPath)]
		NSString SQLiteExtendedErrorCodeKey { get; }
	}

	interface PSPDFInstantErrorNotificationEventArgs {
		[Export ("PSPDFInstantErrorKey")]
		NSError Error { get; }
	}

	interface PSPDFInstantDidUpdateAuthenticationTokenNotificationEventArgs {
		[Export ("PSPDFInstantAuthenticationTokenKey")]
		string AuthenticationToken { get; }
	}

	[BaseType (typeof (NSObject))]
	[DisableDefaultCtor]
	interface PSPDFInstantClient {

		[Field ("PSPDFInstantDidFinishDownloadNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFinishDownloadNotification { get; }

		[Field ("PSPDFInstantDidFailDownloadNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailDownloadNotification { get; }

		[Field ("PSPDFInstantDidFailSyncingNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailSyncingNotification { get; }

		[Field ("PSPDFInstantDidFailAuthenticationNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification]
		NSString DidFailAuthenticationNotification { get; }

		[Field ("PSPDFInstantDidUpdateAuthenticationTokenNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantDidUpdateAuthenticationTokenNotificationEventArgs))]
		NSString DidUpdateAuthenticationTokenNotification { get; }

		[Field ("PSPDFInstantDidFailUpdatingAuthenticationTokenNotification", PSPDFKitGlobal.LibraryPath)]
		[Notification (typeof (PSPDFInstantErrorNotificationEventArgs))]
		NSString DidFailUpdatingAuthenticationTokenNotification { get; }

		[Field ("PSPDFInstantSyncingLocalChangesDisabled", PSPDFKitGlobal.LibraryPath)]
		double SyncingLocalChangesDisabled { get; }

		[Static]
		[Export ("dataDirectory")]
		NSUrl DataDirectory { get; }

		[Export ("initWithServerURL:")]
		[DesignatedInitializer]
		IntPtr Constructor (NSUrl serverUrl);

		[Export ("serverURL")]
		NSUrl ServerUrl { get; }

		[Export ("documentDescriptorWithIdentifier:error:")]
		[return: NullAllowed]
		IPSPDFInstantDocumentDescriptor GetDocumentDescriptor (string documentIdentifier, [NullAllowed] out NSError error);

		[Export ("removeLocalStorageWithError:")]
		bool RemoveLocalStorage ([NullAllowed] out NSError error);

		[NullAllowed, Export ("delegate", ArgumentSemantic.Weak)]
		IPSPDFInstantClientDelegate Delegate { get; set; }
	}

	interface IPSPDFInstantClientDelegate { }

	[Protocol, Model]
	[BaseType (typeof (NSObject))]
	interface PSPDFInstantClientDelegate {

		[Abstract]
		[Export ("instantClient:didFinishDownloadForDocument:")]
		void DidFinishDownload (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Abstract]
		[Export ("instantClient:didFailDownloadForDocument:error:")]
		void DidFailDownload (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);

		[Abstract]
		[Export ("instantClient:didFailSyncingDocument:error:")]
		void DidFailSyncingDocument (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);

		[Abstract]
		[Export ("instantClient:didFailAuthenticationForDocument:")]
		void DidFailAuthentication (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Abstract]
		[Export ("instantClient:didUpdateAuthenticationToken:forDocument:")]
		void DidUpdateAuthenticationToken (PSPDFInstantClient instantClient, string validJwt, IPSPDFInstantDocumentDescriptor documentDescriptor);

		[Abstract]
		[Export ("instantClient:didFailUpdatingAuthenticationTokenForDocument:error:")]
		void DidFailUpdatingAuthenticationToken (PSPDFInstantClient instantClient, IPSPDFInstantDocumentDescriptor documentDescriptor, NSError error);
	}

	interface IPSPDFInstantDocumentDescriptor { }

	[Protocol]
	interface PSPDFInstantDocumentDescriptor {

		[Abstract]
		[Export ("identifier")]
		string Identifier { get; }

		[Abstract]
		[Export ("downloaded")]
		bool Downloaded { [Bind ("isDownloaded")] get; }

		[Abstract]
		[Export ("downloadDocumentUsingAuthenticationToken:error:")]
		bool DownloadDocument (string authenticationToken, [NullAllowed] out NSError error);

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
		[Export ("updateAuthenticationToken:")]
		void UpdateAuthenticationToken (string authenticationToken);

		[Abstract]
		[Export ("sync")]
		void Sync ();

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

		[Export ("shouldListenForServerChangesWhenVisible")]
		bool ShouldListenForServerChangesWhenVisible { get; set; }

		[Export ("syncChanges:")]
		void SyncChanges ([NullAllowed] NSObject sender);
	}
}
