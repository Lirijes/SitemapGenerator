using HtmlAgilityPack;
using Microsoft.AspNetCore.Mvc;
using SitemapGenerator.ViewModels;
using System.Xml.Linq;

namespace SitemapGenerator.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View(new UrlViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> GenerateXML(UrlViewModel model, string actionType)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            string xmlData;
            if (model.GenerateHtmlData)
            {
                xmlData = await ConvertUrlToXmlForHtmlData(model.Url);
            }
            else
            {
                xmlData = await ConvertUrlToXmlForSitemap(model.Url);
            }
            if (actionType == "download")
            {
                var contentDisposition = new System.Net.Mime.ContentDisposition
                {
                    FileName = "generated.xml",
                    Inline = false,  // Force download
                };
                Response.Headers.Append("Content-Disposition", contentDisposition.ToString());
                return Content(xmlData, "text/xml", System.Text.Encoding.UTF8);
            }
            else
            {
                return Content(xmlData, "text/xml");
            }
        }

        private async Task<string> ConvertUrlToXmlForHtmlData(string url)
        {
            // Fetch the HTML content from the URL
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string htmlContent = await client.GetStringAsync(url);
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    // Create a new XML document
                    XDocument xmlDoc = new XDocument();
                    XElement rootElement = new XElement("WebsiteContent");
                    xmlDoc.Add(rootElement);

                    // Parse HTML and fill XML
                    foreach (var node in htmlDoc.DocumentNode.SelectNodes("//body//*"))
                    {
                        XElement xmlElement = new XElement("Element",
                            new XAttribute("TagName", node.Name),
                            new XAttribute("InnerText", node.InnerText.Trim()));

                        // Add attributes as child elements
                        foreach (var attribute in node.Attributes)
                        {
                            xmlElement.Add(new XElement("Attribute",
                                new XAttribute("Name", attribute.Name),
                                new XAttribute("Value", attribute.Value)));
                        }

                        rootElement.Add(xmlElement);
                    }

                    // Convert the XmlDocument to a string
                    return xmlDoc.ToString();
                }
                catch (Exception e)
                {
                    return $"<error>Failed to convert URL to XML: {e.Message}</error>";
                }
            }
        }

        private async Task<string> ConvertUrlToXmlForSitemap(string baseUrl)
        {
            using (HttpClient client = new HttpClient())
            {
                try
                {
                    string htmlContent = await client.GetStringAsync(baseUrl);
                    HtmlDocument htmlDoc = new HtmlDocument();
                    htmlDoc.LoadHtml(htmlContent);

                    XNamespace xmlns = "http://www.sitemaps.org/schemas/sitemap/0.9";
                    XNamespace xsi = "http://www.w3.org/2001/XMLSchema-instance";
                    XDocument xmlDoc = new XDocument(
                        new XElement(xmlns + "urlset",
                            new XAttribute(XNamespace.Xmlns + "xsi", xsi),
                            new XAttribute(xsi + "schemaLocation", "http://www.sitemaps.org/schemas/sitemap/0.9 http://www.sitemaps.org/schemas/sitemap/0.9/sitemap.xsd")
                        )
                    );

                    var links = htmlDoc.DocumentNode.SelectNodes("//a[@href]");
                    if (links != null)
                    {
                        foreach (var link in links)
                        {
                            string href = link.GetAttributeValue("href", string.Empty);
                            if (!string.IsNullOrEmpty(href))
                            {
                                // Ensure absolute URL
                                Uri fullUri = new Uri(new Uri(baseUrl), href);
                                double priority = CalculatePriority(fullUri, baseUrl);
                                XElement urlElement = new XElement(xmlns + "url",
                                    new XElement(xmlns + "loc", fullUri.AbsoluteUri),
                                    new XElement(xmlns + "lastmod", DateTime.UtcNow.ToString("s") + "+00:00"),
                                    new XElement(xmlns + "priority", priority.ToString("F2"))
                                );
                                xmlDoc.Root.Add(urlElement);
                            }
                        }
                    }

                    return xmlDoc.ToString();
                }
                catch (Exception e)
                {
                    return $"<error>Failed to convert URL to XML: {e.Message}</error>";
                }
            }
        }

        private double CalculatePriority(Uri uri, string baseUrl)
        {
            // Example: Decrease priority based on URL depth
            int depth = uri.AbsolutePath.Count(c => c == '/');
            // Prioritize root/home page highest, decrease as depth increases
            return Math.Max(0.5, 1.0 - (depth * 0.1));
        }
    }
}
