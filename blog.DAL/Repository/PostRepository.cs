using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace blog.DAL
{
    public class PostRepository : IRepository<Post, int>, IDisposable
    {
        private ApplicationDbContext _context;
        public UserRepository userRepository = new UserRepository();
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
            var response = _context.Posts.Include(post => post.User).ToList(); 
            return response;
        }

        public Post ReadById(int id)
        {
            var response = _context.Posts.Include(post => post.User)
                .Where(post => post.PostId == id)
                .Include(post => post.User.Posts)
                .SingleOrDefault();
            return response;
        }

        public IList<Post> ReadByTitle(string title)
        {
            var response = _context.Posts.Include(post => post.User)
                .Where(post => post.Title == title)
                .Include(post => post.User.Posts)
                .ToList();
            return response;
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
