using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace AspNetCoreHero.Web.Helpers
{
    [HtmlTargetElement(Attributes = "is-active-route")]
    public class ActivePageTagHelper : TagHelper
    {
        /// <summary>The name of the action method.</summary>
        /// <remarks>Must be <c>null</c> if <see cref="P:Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper.Route" /> is non-<c>null</c>.</remarks>
        [HtmlAttributeName("asp-page")]
        public string Page { get; set; }

        [HtmlAttributeName("asp-area")]
        public string Area { get; set; }

        /// <summary>
        /// Gets or sets the <see cref="T:Microsoft.AspNetCore.Mvc.Rendering.ViewContext" /> for the current request.
        /// </summary>
        [HtmlAttributeNotBound]
        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            try
            {
                base.Process(context, output);

                if (ShouldBeActive())
                {
                    MakeActive(output);
                }

                output.Attributes.RemoveAll("is-active-route");
            }
            catch 
            {

            }
        }

        private bool ShouldBeActive()
        {
            if (ViewContext.RouteData.Values["Area"] == null) 
                return false;
            string currentArea = ViewContext.RouteData.Values["Area"].ToString();

            if (!string.IsNullOrWhiteSpace(Area) && Area.ToLower() != currentArea.ToLower())
            {
                return false;
            }

            return true;
        }

        private void MakeActive(TagHelperOutput output)
        {
            var classAttr = output.Attributes.FirstOrDefault(a => a.Name == "class");
            if (classAttr == null)
            {
                classAttr = new TagHelperAttribute("class", "active");
                output.Attributes.Add(classAttr);
            }
            else if (classAttr.Value == null || classAttr.Value.ToString().IndexOf("active") < 0)
            {
                output.Attributes.SetAttribute("class", classAttr.Value == null
                    ? "active"
                    : classAttr.Value.ToString() + " active");
            }
        }
    }
}
