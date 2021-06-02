using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace blog.DAL
{
    public class ImageRepository : IRepository<Image, int>, IDisposable
    {
        private ApplicationDbContext _context;
        public ImageRepository()
        {
            ApplicationDbContextFactory applicationDbContextFactory = new ApplicationDbContextFactory();
            _context = applicationDbContextFactory.CreateDbContext(null);
        }
        public Image Create(Image entity)
        {
            _context.Images.Add(entity);
            _context.SaveChanges();
            return entity;
        }
        public IList<Image> ReadAll()
        {
            return _context.Images.ToList();
        }

        public Image ReadById(int id)
        {
            return _context.Images
                               .Where(image => image.ImageId == id)
                               .SingleOrDefault();
        }
        public Image Update(Image entity)
        {
            Image trackedEntity = _context.Images.Find(entity.ImageId);

            if (trackedEntity == null)
            {
                trackedEntity = _context.Images.Add(entity).Entity;
            }
            else
            {
                trackedEntity.PostId = entity.PostId;
            }
            _context.SaveChanges();
            return trackedEntity;
        }
        public void Delete(Image entity)
        {
            Image trackedEntity = _context.Images.Find(entity.ImageId);

            if (trackedEntity != null)
            {
                _context.Images.Remove(trackedEntity);
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
