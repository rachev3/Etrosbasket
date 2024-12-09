using Etrosbasket.Areas.Admin.ViewModels.Articles;
using Etrosbasket.Areas.Admin.ViewModels.Players;
using Etrosbasket.Data;
using Etrosbasket.Data.Migrations;
using Etrosbasket.Models;
using Etrosbasket.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Etrosbasket.Services.Implementations
{
    public class ArticleService : IArticleService
    {
        private readonly ApplicationDbContext dbContext;
        public ArticleService(ApplicationDbContext applicationDbContext)
        {
            dbContext = applicationDbContext;
        }
        public async Task<ArticleListViewModel> GetAll()
        {
            var result = await dbContext.Articles.ToListAsync();
            ArticleListViewModel viewModel = new()
            {
                Articles = result.Select(article => new ArticleViewModel
                {
                    ArticleId = article.ArticleId,
                    Title = article.Title,
                    CreatedDate = article.PublishDate
                }).ToList()
            };
            return viewModel;
        }
        public async Task Add(Article article)
        {
            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await dbContext.Articles.FirstOrDefaultAsync(r => r.ArticleId == id);
            dbContext.Articles.Remove(result);
            await dbContext.SaveChangesAsync();
        }


        public Task<Article> GetById(int articleId)
        {
            return null;
        }

        public Task<Article> Update(int id, Article article)
        {
            throw new NotImplementedException();
        }
    }
}
