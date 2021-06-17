using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace blog.DAL
{
    public class UserRepository : IRepository<User, int>, IDisposable
    {
        private ApplicationDbContext _context;
        public UserRepository()
        {
            ApplicationDbContextFactory applicationDbContextFactory = new ApplicationDbContextFactory();
            _context = applicationDbContextFactory.CreateDbContext(null);
        }
        public User Create(User entity)
        {
            var response = _context.Users.Find(entity.UserId);
            if (response == null)
            {
                _context.Users.Add(entity);
            }
            else
            {
                return null;
            }
            _context.SaveChanges();
            return entity;
        }
        public IList<User> ReadAll()
        {
            return _context.Users.ToList();
        }

        public User ReadById(int id)
        {
            PostRepository a = new PostRepository();
            var user = _context.Users
                               .Where(user => user.UserId == id)
                               .SingleOrDefault();
            for (int i = 0; i < user.Posts.Count; i++)
            {
                Post post = a.ReadById(user.Posts[i].PostId);
                user.Posts.Add(post);
            }
            return user;
        }
        public User Update(User entity)
        {
            User trackedEntity = _context.Users.Find(entity.UserId);

            if (trackedEntity == null)
            {
                trackedEntity = _context.Users.Add(entity).Entity;
            }
            else
            {
                trackedEntity.Email = entity.Email;
                trackedEntity.Username = entity.Username;
                trackedEntity.Password = entity.Password;
                trackedEntity.FirstName = entity.FirstName;
                trackedEntity.LastName = entity.LastName;
                trackedEntity.BirthDate = entity.BirthDate;
                trackedEntity.Gender = entity.Gender;
                trackedEntity.Posts = entity.Posts;
            }
            _context.SaveChanges();
            return trackedEntity;
        }
        public void Delete(User entity)
        {
            User trackedEntity = _context.Users.Find(entity.UserId);

            if (trackedEntity != null)
            {
                _context.Users.Remove(trackedEntity);
            }
            _context.SaveChanges();
        }
        public User getUserByUsername(string username) {
            return _context.Users.Where(user => user.Username == username).FirstOrDefault();
        }
        public User getUserByEmail(string email) {
            return _context.Users.Where(user => user.Email == email).FirstOrDefault();
        }
        public User getUserById(int id) {
            return _context.Users.Where(user => user.UserId == id).FirstOrDefault();
        }
        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
        }

    }
}
