using Abp.Domain.Entities;
using Abp.EntityFramework;
using Abp.EntityFramework.Repositories;

namespace BS_Zoom_Demo.EntityFramework.Repositories
{
    public abstract class BS_Zoom_DemoRepositoryBase<TEntity, TPrimaryKey> : EfRepositoryBase<BS_Zoom_DemoDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected BS_Zoom_DemoRepositoryBase(IDbContextProvider<BS_Zoom_DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //add common methods for all repositories
    }

    public abstract class BS_Zoom_DemoRepositoryBase<TEntity> : BS_Zoom_DemoRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected BS_Zoom_DemoRepositoryBase(IDbContextProvider<BS_Zoom_DemoDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //do not add any method here, add to the class above (since this inherits it)
    }
}
