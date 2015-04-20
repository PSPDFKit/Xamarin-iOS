XBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT_ROOT=.
PROJECT=$(PROJECT_ROOT)/SQLite.xcodeproj
TARGET=SQLite

all: libSQLite.a

sqlite-sim:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build

sqlite-dev:
	$(XBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -configuration Release clean build

lipoallthethings: sqlite-sim sqlite-dev
	xcrun -sdk iphoneos lipo -create -output libSQLite.a ./build/Release-iphoneos/libSQLite.a ./build/Release-iphonesimulator/libSQLite.a

libSQLite.a: clean lipoallthethings
	xcrun -sdk iphoneos lipo -info libSQLite.a

clean:
	rm -rf *.a build/