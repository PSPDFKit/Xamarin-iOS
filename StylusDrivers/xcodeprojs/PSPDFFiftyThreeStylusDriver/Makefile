SDKLIPO=xcrun -sdk iphoneos lipo
XCODEBUILD=/Applications/Xcode.app/Contents/Developer/usr/bin/xcodebuild
PROJECT=PSPDFFiftyThreeStylusDriver.xcodeproj
TARGET=PSPDFFiftyThreeStylusDriver

all: build

libPSPDFFiftyThreeStylusDriver-sim.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphonesimulator -configuration Release clean build
	-mv build/Release-iphonesimulator/libPSPDFFiftyThreeStylusDriver.a $@

libPSPDFFiftyThreeStylusDriver-dev.a:
	$(XCODEBUILD) -project $(PROJECT) -target $(TARGET) -sdk iphoneos -configuration Release clean build
	-mv build/Release-iphoneos/libPSPDFFiftyThreeStylusDriver.a $@

libPSPDFFiftyThreeStylusDriver.a: libPSPDFFiftyThreeStylusDriver-sim.a libPSPDFFiftyThreeStylusDriver-dev.a
	$(SDKLIPO) -create -output $@ $^

build: libPSPDFFiftyThreeStylusDriver.a

clean:
	-rm -rf build/ *.a
