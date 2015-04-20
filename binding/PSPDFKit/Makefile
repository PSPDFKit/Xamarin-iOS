MONOXBUILD=/Library/Frameworks/Mono.framework/Commands/xbuild

all: PSPDFKit.dll

PSPDFKit.dll: clean
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit.csproj
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit-Classic.csproj
	mkdir -p ../../Dll/classic
	mkdir -p ../../Dll/unified
	cp bin/classic/Release/PSPDFKit.dll ../../Dll/classic/PSPDFKit.dll
	cp bin/unified/Release/PSPDFKit.dll ../../Dll/unified/PSPDFKit.dll

sqlite: clean
	cd ../sqlite3 && make
	cp ../sqlite3/libSQLite.a ./libSQLite.a
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit-SQLite.csproj
	$(MONOXBUILD) /p:Configuration=Release PSPDFKit-SQLite-Classic.csproj
	mkdir -p ../../Dll/classic
	mkdir -p ../../Dll/unified
	cp bin/classic/Release/PSPDFKit-SQLite.dll ../../Dll/classic/PSPDFKit-SQLite.dll
	cp bin/unified/Release/PSPDFKit-SQLite.dll ../../Dll/unified/PSPDFKit-SQLite.dll

clean:
	-rm -rf *.a *.dll *.mdb *.sln bin/ obj/ ../../Dll/
	cd ../sqlite3 && make clean
