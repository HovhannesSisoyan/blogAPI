using System.Collections.Generic;

namespace blog.DAL
{
    public interface IRepository<TEntity, TIdentity>
    {
        IList<TEntity> ReadAll();
        TEntity ReadById(TIdentity id);
        TEntity Create(TEntity entity);
        TEntity Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
