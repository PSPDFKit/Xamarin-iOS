SDKLIPO=xcrun -sdk iphoneos lipo
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT=PSPDFPogoStylusDriver.xcodeproj
TARGET=PSPDFPogoStylusDriver

all: build

libPSPDFPogoStylusDriver-sim.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv build/Release-iphonesimulator/libPSPDFPogoStylusDriver.a $@

libPSPDFPogoStylusDriver-dev.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -configuration Release clean build
	-mv build/Release-iphoneos/libPSPDFPogoStylusDriver.a $@

libPSPDFPogoStylusDriver.a: libPSPDFPogoStylusDriver-sim.a libPSPDFPogoStylusDriver-dev.a
	$(SDKLIPO) -create -output $@ $^

build: libPSPDFPogoStylusDriver.a

clean:
	-rm -rf build/ *.a
