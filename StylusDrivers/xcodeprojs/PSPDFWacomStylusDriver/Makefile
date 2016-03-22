SDKLIPO=xcrun -sdk iphoneos lipo
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT=PSPDFWacomStylusDriver.xcodeproj
TARGET=PSPDFWacomStylusDriver

all: build

libPSPDFWacomStylusDriver-sim.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv build/Release-iphonesimulator/libPSPDFWacomStylusDriver.a $@

libPSPDFWacomStylusDriver-dev.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -configuration Release clean build
	-mv build/Release-iphoneos/libPSPDFWacomStylusDriver.a $@

libPSPDFWacomStylusDriver.a: libPSPDFWacomStylusDriver-sim.a libPSPDFWacomStylusDriver-dev.a
	$(SDKLIPO) -create -output $@ $^

build: libPSPDFWacomStylusDriver.a

clean:
	-rm -rf build/ *.a
