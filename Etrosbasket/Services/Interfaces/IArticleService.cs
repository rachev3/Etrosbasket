﻿using Etrosbasket.Models;

namespace Etrosbasket.Services.Interfaces
{
    public interface IArticleService
    {
       
        Task<List<Article>> GetAll();
        Task<Article> GetById(int articleId);
        Task Add(Article article);
        Task<Article> Update(int id, Article article);
        Task Delete(int id);
    }
}