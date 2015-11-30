MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

all: PSPDFKit.iOS.dll

PSPDFKit.iOS.dll: clean
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit.iOS.csproj
	mkdir -p ../Dll
	cp bin/Release/PSPDFKit.iOS.dll ../Dll/PSPDFKit.iOS.dll

clean:
	-rm -rf *.dll *.mdb *.userprefs bin/ obj/ ../Dll/
