namespace Etrosbasket.Areas.Admin.ViewModels.Articles
{
    public class ArticleViewModel
    {
        public int ArticleId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string Title { get; set; }
        public string FormattedCreatedDate => CreatedDate.ToString("yyyy-MM-dd");
    }
}
