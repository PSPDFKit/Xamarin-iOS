MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

all: PSPDFKit.dll

PSPDFKit.dll: clean
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit.csproj
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit-Classic.csproj
	mkdir -p ../../Dll/classic
	mkdir -p ../../Dll/unified
	cp bin/classic/Release/PSPDFKit.dll ../../Dll/classic/PSPDFKit.dll
	cp bin/unified/Release/PSPDFKit.dll ../../Dll/unified/PSPDFKit.dll

clean:
	-rm -rf *.dll *.mdb *.sln bin/ obj/ ../../Dll/
