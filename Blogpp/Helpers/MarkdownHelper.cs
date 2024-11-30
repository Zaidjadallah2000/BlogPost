using Ganss.Xss;
using Markdig;
namespace Blogpp.Helpers
{

    public static class MarkdownHelper
    {
        public static string ConvertMarkdownToHtml(string markdown)
        {
            var pipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
            string html = Markdown.ToHtml(markdown, pipeline);

            // تنظيف HTML الناتج لضمان الأمان.
            var sanitizer = new HtmlSanitizer();
            return sanitizer.Sanitize(html);
        }
    }
}
