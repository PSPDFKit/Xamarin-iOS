#!/usr/bin/env bash

# Files that contain our PSPDFKit version string.
file1="README.md"
file2="build.cake"
file3="PSPDFKit.iOS.UI/Properties/AssemblyInfo.cs"
file4="PSPDFKit.iOS.Model/Properties/AssemblyInfo.cs"
file5="PSPDFKit.iOS.Instant/Properties/AssemblyInfo.cs"

read -p "Enter the search string: " search

read -p "Enter the replace string: " replace

# Replace old PSPDFKit version string with new one for each file.
sed -i '' "s/$search/$replace/g" $file1
sed -i '' "s/$search/$replace/g" $file2
sed -i '' "s/$search/$replace/g" $file3
sed -i '' "s/$search/$replace/g" $file4
sed -i '' "s/$search/$replace/g" $file5
