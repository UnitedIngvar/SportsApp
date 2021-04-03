using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;
using System;

namespace SportsStore.Infrastructure
{
    [HtmlTargetElement("div", Attributes = "page-model")]
    public class PageLinkTagHelper : TagHelper 
    {
        private IUrlHelperFactory urlHelperFactory;
        public PageLinkTagHelper(IUrlHelperFactory helperFactory)
        {
            urlHelperFactory = helperFactory;
        }

        [ViewContext]
        [HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }
        public PagingInfo PageModel { get; set; }
        public string PageAction { get; set; }
        [HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
        public Dictionary<string, object> PageUrlValues { get; set; }
            = new Dictionary<string, object>();
        public bool PageClassesEnabled { get; set; } = false;
        public string PageClass { get; set; }
        public string PageClassNormal { get; set; }
        public string PageClassSelected { get; set; }
        public override void Process(TagHelperContext context, 
            TagHelperOutput output)
        {
            System.Diagnostics.Debug.WriteLine(context.ToString());
            System.Diagnostics.Debug.WriteLine(ViewContext.ViewData.Model.ToString());
            IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
            TagBuilder result = new TagBuilder("div");
            System.Diagnostics.Debug.WriteLine(result.ToString());
            System.Diagnostics.Debug.WriteLine(urlHelper.Action());
            for (int i = 1 ; i <= PageModel.TotalPages; i++)
			{
                TagBuilder tag = new TagBuilder("a");
                System.Diagnostics.Debug.WriteLine(tag.InnerHtml.ToString());
                PageUrlValues["productPage"] = i;
                tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
                if (PageClassesEnabled)
                {
                    tag.AddCssClass(PageClass);
                    tag.AddCssClass(i == PageModel.CurrentPage
                        ? PageClassSelected : PageClassNormal);
                }
                System.Diagnostics.Debug.WriteLine(tag.InnerHtml.ToString());

                tag.InnerHtml.Append(i.ToString() + " ");
                System.Diagnostics.Debug.WriteLine(tag.InnerHtml.ToString());
                System.Diagnostics.Debug.WriteLine(result);
                result.InnerHtml.AppendHtml(tag);
                System.Diagnostics.Debug.WriteLine(result);
            }
            System.Diagnostics.Debug.WriteLine(output.Content.GetContent()); 
            output.Content.AppendHtml(result.InnerHtml);
            System.Diagnostics.Debug.WriteLine(output.Content.GetContent());
        }
    }
}
