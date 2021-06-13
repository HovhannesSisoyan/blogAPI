using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace blog.DAL.Repository
{
    public class CommentRepository : IRepository<Comment, int>, IDisposable
    {
        private ApplicationDbContext _context;
        public CommentRepository()
        {
            ApplicationDbContextFactory applicationDbContextFactory = new ApplicationDbContextFactory();
            _context = applicationDbContextFactory.CreateDbContext(null);
        }
        public IList<Comment> ReadAll(int postId)
        {
            return _context.Comments
                    .Where(comment => comment.PostId == postId)
                    .ToList();
        }

        public IList<Comment> ReadAll()
        {
            throw new NotImplementedException();
        }

        public Comment ReadById(int id)
        {
            return _context.Comments
                .Where(comment => comment.CommentId == id)
                .SingleOrDefault();
        }

        public Comment Update(Comment entity)
        {
            _context.Comments.Update(entity);
            return entity;
        }

        public Comment Create(Comment entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
            return entity;
        }

        public void Delete(Comment entity)
        {
            if (_context.Comments.Find(entity) != null)
            {
                _context.Comments.Remove(entity);
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
