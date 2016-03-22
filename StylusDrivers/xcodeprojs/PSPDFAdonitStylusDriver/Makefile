SDKLIPO=xcrun -sdk iphoneos lipo
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT=PSPDFAdonitStylusDriver.xcodeproj
TARGET=PSPDFAdonitStylusDriver

all: build

libPSPDFAdonitStylusDriver-sim.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv build/Release-iphonesimulator/libPSPDFAdonitStylusDriver.a $@

libPSPDFAdonitStylusDriver-dev.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -configuration Release clean build
	-mv build/Release-iphoneos/libPSPDFAdonitStylusDriver.a $@

libPSPDFAdonitStylusDriver.a: libPSPDFAdonitStylusDriver-sim.a libPSPDFAdonitStylusDriver-dev.a
	$(SDKLIPO) -create -output $@ $^

build: libPSPDFAdonitStylusDriver.a

clean:
	-rm -rf build/ *.a
