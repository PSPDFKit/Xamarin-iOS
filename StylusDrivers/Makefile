all:
	@echo "Usage:"
	@echo "Use the following make targets to build the drivers, don't try to build projects"
	@echo "independently (using xcode or Xamarin Studio) since makefiles will place files"
	@echo "where they are expected to be. Also please make sure you have all needed files"
	@echo "inside externals folder. For more information visit:"
	@echo "https://pspdfkit.com/guides/ios/current/features/stylus-support/"
	@echo ""
	@echo "Make Targets:\n"
	@echo "\talldrivers \tBuilds all available drivers."
	@echo "\tadonit \t\tBuilds Adonit driver."
	@echo "\tfiftythree \tBuilds FiftyThree driver."
	@echo "\tpogo \t\tBuilds Pogo driver."
	@echo "\twacom \t\tBuilds Wacom driver."
	@echo "\tclean \t\tCleans the build, won't clear externals folder."
	@echo "\nExample of usage:\n"
	@echo "\tmake alldrivers\n"

alldrivers: adonit fiftythree pogo wacom

adonit:
	@cd xcodeprojs/PSPDFAdonitStylusDriver && make
	@cd xambindings/PSPDFAdonitStylusDriver && make

fiftythree:
	@cd xcodeprojs/PSPDFFiftyThreeStylusDriver && make
	@cd xambindings/PSPDFFiftyThreeStylusDriver && make

pogo:
	@cd xcodeprojs/PSPDFPogoStylusDriver && make
	@cd xambindings/PSPDFPogoStylusDriver && make

wacom:
	@cd xcodeprojs/PSPDFWacomStylusDriver && make
	@cd xambindings/PSPDFWacomStylusDriver && make

clean:
	@rm -rf *.dll
	@cd xcodeprojs/PSPDFAdonitStylusDriver && make clean
	@cd xcodeprojs/PSPDFFiftyThreeStylusDriver && make clean
	@cd xcodeprojs/PSPDFPogoStylusDriver && make clean
	@cd xcodeprojs/PSPDFWacomStylusDriver && make clean
	@cd xambindings/PSPDFAdonitStylusDriver && make clean
	@cd xambindings/PSPDFFiftyThreeStylusDriver && make clean
	@cd xambindings/PSPDFPogoStylusDriver && make clean
	@cd xambindings/PSPDFWacomStylusDriver && make clean
