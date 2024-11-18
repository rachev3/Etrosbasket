namespace Etrosbasket.Models
{
    public class Article
    {
        public int Article_Id { get; set; }
        public string Title { get; set; }
        public string Slug { get; set; }
        public string Content { get; set; }
        public string Summary { get; set; }

        public string CoverImageUrl { get; set; }
        public List<string> AdditionalImages { get; set; }

        public DateTime PublishDate { get; set; }
        
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string MetaKeywords { get; set; }

        public int Views { get; set; }



    }
}
