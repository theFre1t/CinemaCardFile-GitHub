using CinemaCardFile.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CinemaCardFile.TagHelpers
{
    public class LinkPageTagHelper : TagHelper
    {
        private IUrlHelperFactory UrlHelperFactory;
        public LinkPageTagHelper(IUrlHelperFactory urlHF)
        {
            UrlHelperFactory = urlHF;
        }
        [ViewContext][HtmlAttributeNotBound] public ViewContext ViewContext { get; set; }
        public PaginationModel PaginModel { get; set; }
        public string currPage { get; set; }

        TagBuilder tag, prevItem, currentItem, nextItem;

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            IUrlHelper urlHelper = UrlHelperFactory.GetUrlHelper(ViewContext);

            tag = new TagBuilder("ul");
            tag.AddCssClass("pagination");

            if (PaginModel.GoToFirstPage)
            {
                prevItem = CreateTag(PaginModel.currPage - 1, urlHelper);
                tag.InnerHtml.AppendHtml(prevItem);
            }

            currentItem = CreateTag(PaginModel.currPage, urlHelper);
            tag.InnerHtml.AppendHtml(currentItem);

            if (PaginModel.GoToLastPage)
            {
                nextItem = CreateTag(PaginModel.currPage + 1, urlHelper);
                tag.InnerHtml.AppendHtml(nextItem);
            }
            output.Content.AppendHtml(tag);
        }

        TagBuilder item, link;
        TagBuilder CreateTag(int pageNumber, IUrlHelper urlHelper)
        {
            item = new TagBuilder("li");
            link = new TagBuilder("a");
            if (pageNumber == this.PaginModel.currPage)
            {
                item.AddCssClass("active");
            }
            else
            {
                link.Attributes["href"] = urlHelper.Action(currPage, new {page = pageNumber });
            }
            item.AddCssClass("page-item");
            link.AddCssClass("page-link");
            link.InnerHtml.Append(pageNumber.ToString());
            item.InnerHtml.AppendHtml(link);
            return item;
        }
    }
}
