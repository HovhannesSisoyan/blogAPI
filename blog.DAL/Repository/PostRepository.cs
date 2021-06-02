using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace blog.DAL
{
    public class PostRepository : IRepository<Post, int>, IDisposable
    {
        private ApplicationDbContext _context;
        public PostRepository()
        {
            ApplicationDbContextFactory applicationDbContextFactory = new ApplicationDbContextFactory();
            _context = applicationDbContextFactory.CreateDbContext(null);
        }
        public Post Create(Post entity)
        {
            _context.Posts.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public IList<Post> ReadAll()
        {
            return _context.Posts.ToList();
        }

        public Post ReadById(int id)
        {
            return _context.Posts
                               .Where(post => post.PostId == id)
                               .SingleOrDefault();
        }
        public Post Update(Post entity)
        {
            Post trackedEntity = _context.Posts.Find(entity.PostId);

            if (trackedEntity == null)
            {
                trackedEntity = _context.Posts.Add(entity).Entity;
            }
            else
            {
                trackedEntity.Title = entity.Title;
                trackedEntity.Category = entity.Category;
                trackedEntity.Body = entity.Body;
                trackedEntity.UserId = entity.UserId;
                trackedEntity.Image = entity.Image;
            }
            _context.SaveChanges();
            return trackedEntity;
        }
        public void Delete(Post entity)
        {
            Post trackedEntity = _context.Posts.Find(entity.PostId);

            if (trackedEntity != null)
            {
                _context.Posts.Remove(trackedEntity);
            }
            _context.SaveChanges();
        }
        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
        }

    }
}
