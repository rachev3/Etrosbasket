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
        public async Task Add(ArticleCreateViewModel viewModel)
        {
            Article article =  new Article();
            article.Title = viewModel.Title;
            article.Content = viewModel.Content;
            article.Summary = viewModel.Summary;
            article.CoverImageUrl = viewModel.CoverImageUrl;
            article.AdditionalImages = viewModel.AdditionalImages;
            article.PublishDate = viewModel.PublishDate;
            article.MetaTitle = viewModel.MetaTitle;
            article.MetaDescription = viewModel.MetaDescription;
            article.MetaKeywords = viewModel.MetaKeywords;

            await dbContext.Articles.AddAsync(article);
            await dbContext.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var result = await dbContext.Articles.FirstOrDefaultAsync(r => r.ArticleId == id);
            dbContext.Articles.Remove(result);
            await dbContext.SaveChangesAsync();
        }


        public async Task<ArticleEditViewModel> GetById(int articleId)
        {
            var result = await dbContext.Articles.FirstOrDefaultAsync(r => r.ArticleId == articleId);
            ArticleEditViewModel viewModel = new()
            {
                ArticleId = result.ArticleId,
                Title = result.Title,
                Content = result.Content,
                Summary = result.Summary,
                PublishDate = result.PublishDate,
                MetaTitle = result.MetaTitle,
                MetaDescription = result.MetaDescription,
                MetaKeywords = result.MetaKeywords
            };

            return viewModel;
        }

        public Task<Article> Update(int id, Article article)
        {
            throw new NotImplementedException();
        }
    }
}
