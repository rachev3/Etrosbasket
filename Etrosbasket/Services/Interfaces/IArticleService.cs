using Etrosbasket.Areas.Admin.ViewModels.Articles;
using Etrosbasket.Models;

namespace Etrosbasket.Services.Interfaces
{
    public interface IArticleService
    {
       
        Task<ArticleListViewModel> GetAll();
        Task<ArticleEditViewModel> GetById(int articleId);
        Task Add(ArticleCreateViewModel article);
        Task<Article> Update(int id, Article article);
        Task Delete(int id);
    }
}
