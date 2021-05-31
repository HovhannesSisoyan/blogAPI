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
            _context.Users.Add(entity);
            return entity;
        }
        public IList<User> ReadAll()
        {
            return _context.Users.ToList();
        }

        public User ReadById(int id)
        {
            return _context.Users
                               .Where(user => user.UserId == id)
                               .SingleOrDefault();
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
            return trackedEntity;
        }
        public void Delete(User entity)
        {
            User trackedEntity = _context.Users.Find(entity.UserId);

            if (trackedEntity != null)
            {
                _context.Users.Remove(trackedEntity);
            }
        }
        public void Dispose()
        {
            _context?.Dispose();
            _context = null;
        }

    }
}
