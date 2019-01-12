using Server.Repository.Base;
using Server.Repository.Interface;

namespace Server.Repository.Wrapper
{
    public interface IRepositoryWrapper
    {
        RepositoryContext Context { get; }
        IProductRepository Product { get; }
        IUserRepository User { get; }
        ICatalogRepository Catalog { get; }
        ICategoryRepository Category { get; }
        ICollectionRepository Collection { get; }
        IFinishRepository Finish { get; }
        IMaterialRepository Material { get; }
        IAestheticLineRepository AestheticLine { get; }
        IProductAggregationRepository ProductAggregation { get; }
        IProductCollectionRepository ProductCollection { get; }
        IProductCatalogRepository ProductCatalog { get; }
        IProductMaterialFinishRepository ProductMaterialFinish { get;  }
        IFinishRestrictionRepository FinishRestriction { get;  }
        IMaterialRestrictionRepository MaterialRestriction { get;  }
        ICategoryAggregationRepository CategoryAggregation { get;  }
    }
}