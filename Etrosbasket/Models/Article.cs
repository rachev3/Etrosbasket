using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Etrosbasket.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }

        private string title;
        public string Title
        {
            get => title;
            set
            {
                title = value;
                Slug = GenerateSlug(title); // Automatically generate slug when title is set
            }
        }

        public string Slug { get; private set; } // Slug is read-only and auto-generated

        public string Content { get; set; }
        public string? Summary { get; set; }
        public string CoverImageUrl { get; set; }
        public List<string>? AdditionalImages { get; set; }
        public DateTime PublishDate { get; set; }
        public string? MetaTitle { get; set; }
        public string? MetaDescription { get; set; }
        public string? MetaKeywords { get; set; }
        

        // Slug generation logic
        private static string GenerateSlug(string title)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;

            // Convert to lowercase
            string slug = title.ToLowerInvariant();

            // Replace spaces with hyphens
            slug = slug.Replace(" ", "-");

            // Remove invalid URL characters
            slug = Regex.Replace(slug, @"[^a-z0-9\-]", string.Empty);

            // Remove multiple consecutive hyphens
            slug = Regex.Replace(slug, @"-+", "-");

            // Trim hyphens from the start and end
            slug = slug.Trim('-');

            return slug;
        }
    }
}
