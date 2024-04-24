using System.ComponentModel.DataAnnotations;

namespace SitemapGenerator.ViewModels
{
    public class UrlViewModel
    {
        [Display(Name = "Url")]
        public string Url { get; set; }

        [Display(Name = "Generate Sitemap")]
        public bool GenerateSitemap { get; set; }

        [Display(Name = "Generate Html Data")]
        public bool GenerateHtmlData { get; set; }
    }
}
