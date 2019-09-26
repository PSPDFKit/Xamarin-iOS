using System;
using Foundation;
using PSPDFKit.Model;
using PSPDFKit.UI;

namespace PSPDFCatalog.Catalog.Customization
{
    public class CustomizedAnnotationToolbar : PSPDFAnnotationToolbar
    {
        public CustomizedAnnotationToolbar(PSPDFAnnotationStateManager annotationStateManager) : base(annotationStateManager)
        {

        }

        protected internal CustomizedAnnotationToolbar(IntPtr handle) : base(handle)
        {
            var highlight = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Highlight);
            var image = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.Image);
            PSPDFAnnotationGroupItem callout;
            callout = PSPDFAnnotationGroupItem.FromType(PSPDFAnnotationString.FreeText, PSPDFAnnotationVariantString.FreeTextCallout, PSPDFAnnotationGroupItem_PSPDFPresets.GetFreeTextConfigurationHandler(image));

            var compactGroups = PSPDFAnnotationGroup.FromItems(new PSPDFAnnotationGroupItem[] { highlight, callout, image });

            PSPDFAnnotationToolbarConfiguration compactConfiguration = new PSPDFAnnotationToolbarConfiguration(new PSPDFAnnotationGroup[] { compactGroups });

            Configurations = new PSPDFAnnotationToolbarConfiguration[] { compactConfiguration };

        }
    }
}
