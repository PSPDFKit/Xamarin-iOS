using System;
using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog.Catalog.Customization
{
    public class CustomizedAnnotationToolbar : PSPDFAnnotationToolbar
    {
        protected internal CustomizedAnnotationToolbar(IntPtr handle) : base(handle)
        {
            var highlight = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Highlight);
            var image = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Image);

            var callout = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.FreeText, PSPDFAnnotationVariantString.FreeTextCallout, PSPDFAnnotationGroupItem_PSPDFPresets.GetFreeTextConfigurationHandler(default));

            var ink = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Ink, PSPDFAnnotationVariantString.InkPen, PSPDFAnnotationGroupItem_PSPDFPresets.GetInkConfigurationHandler(default));
            var line = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Line);
            var square = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Square);

            var compactGroups = new PSPDFAnnotationGroup[] {
                PSPDFAnnotationGroup.FromItems(new PSPDFAnnotationGroupItem[] { highlight, image }),
                PSPDFAnnotationGroup.FromItems(new PSPDFAnnotationGroupItem[] { callout }),
                PSPDFAnnotationGroup.FromItems(new PSPDFAnnotationGroupItem[] { ink, line, square })
            };

            PSPDFAnnotationToolbarConfiguration compactConfiguration = new PSPDFAnnotationToolbarConfiguration(compactGroups);

            Configurations = new PSPDFAnnotationToolbarConfiguration[] { compactConfiguration };

        }
    }
}
