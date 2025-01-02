using Etrosbasket.Areas.Admin.ViewModels.Articles;
using Etrosbasket.Models;
using Etrosbasket.ViewModels.Home;

namespace Etrosbasket.Services.Interfaces
{
    public interface IArticleService
    {
       
        Task<ArticleListViewModel> GetAll();
        Task<List<ArticleHome>> GetTopTwoArticlesForHome();

        Task<ArticleEditViewModel> GetById(int articleId);
        Task Add(ArticleCreateViewModel article);
        Task Update(ArticleEditViewModel viewModel);
        Task Delete(int id);
    }
}
