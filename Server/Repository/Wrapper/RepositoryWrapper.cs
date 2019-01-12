using Server.Repository.Base;
using Server.Repository.Implementation;
using Server.Repository.Interface;

namespace Server.Repository.Wrapper
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private readonly RepositoryContext repoContext;
        private IProductRepository product;
        private IUserRepository user;
        private ICatalogRepository catalog;
        private ICategoryRepository category;
        private ICollectionRepository collection;
        private IFinishRepository finish;
        private IMaterialRepository material;
        private IAestheticLineRepository aestheticLine;
        private IProductAggregationRepository productAggregation;
        private IProductCatalogRepository productCatalog;
        private IProductCollectionRepository productCollection;
        private IProductMaterialFinishRepository productMaterialFinish;
        private IFinishRestrictionRepository finishRestriction;
        private IMaterialRestrictionRepository materialRestriction;
        private ICategoryAggregationRepository categoryAggregation;

        public RepositoryWrapper(RepositoryContext repositoryContext)
        {
            repoContext = repositoryContext;
        }
        
        /**
         * Returns the repository DB context.
         */
        public RepositoryContext Context { get; }

        /**
         * Returns the singleton Product Repository.
         */
        public IProductRepository Product
        {
            get
            {
                if (product == null) product = new ProductRepository(repoContext);

                return product;
            }
        }

        /**
         * Returns the singleton User repository.
         */
        public IUserRepository User
        {
            get
            {
                if (user == null) user = new UserRepository(repoContext);
                return user;
            }
        }

        /**
         * Returns the singleton Catalog Repository.
         */
        public ICatalogRepository Catalog
        {
            get
            {
                if (catalog == null) catalog = new CatalogRepository(repoContext);
                return catalog;
            }
        }
        /**
         * Returns the singleton Category Repository.
         */
        public ICategoryRepository Category
        {
            get
            {
                if (category == null) category = new CategoryRepository(repoContext);
                return category;
            }
        }

        /**
         * Returns the singleton Collection Repository.
         */
        public ICollectionRepository Collection
        {
            get
            {
                if (collection == null) collection = new CollectionRepository(repoContext);
                return collection;
            }
        }

        /**
         * Returns the singleton Finish Repository.
         */
        public IFinishRepository Finish
        {
            get
            {
                if (finish == null) finish = new FinishRepository(repoContext);
                return finish;
            }
        }

        /**
         * Returns the singleton Material Repository.
         */
        public IMaterialRepository Material
        {
            get
            {
                if (material == null) material = new MaterialRepository(repoContext);
                return material;
            }
        }

        /**
         * Returns the singleton AestheticLine Repository.
         */
        public IAestheticLineRepository AestheticLine
        {
            get
            {
                if (aestheticLine == null) aestheticLine = new AestheticLineRepository(repoContext);
                return aestheticLine;
            }
        }

        /**
         * Returns the singleton ProductAggregation repository.
         */
        public IProductAggregationRepository ProductAggregation
        {
            get
            {
                if (productAggregation == null) productAggregation = new ProductAggregationRepository(repoContext);
                return productAggregation;
            }
        }

        /**
         * Returns the singleton Product Collection repository.
         */
        public IProductCollectionRepository ProductCollection
        {
            get
            {
                if (productCollection == null) productCollection = new ProductCollectionRepository(repoContext);
                return productCollection;
            }
        }

        /**
         * Returns the singleton Product Catalog repository.
         */
        public IProductCatalogRepository ProductCatalog
        {
            get
            {
                if (productCatalog == null) productCatalog = new ProductCatalogRepository(repoContext);
                return productCatalog;
            }
        }
        
        /**
         * Returns the singleton Product Material Finish repository.
         */
        public IProductMaterialFinishRepository ProductMaterialFinish
        {
            get
            {
                if (productMaterialFinish == null) productMaterialFinish = new ProductMaterialFinishRepository(repoContext);
                return productMaterialFinish;
            }
        }

        public IFinishRestrictionRepository FinishRestriction
        {
            get
            {
                if (finishRestriction == null) finishRestriction = new FinishRestrictionRepository(repoContext);
                return finishRestriction;
            }
        }

        public IMaterialRestrictionRepository MaterialRestriction
        {
            get
            {
                if (materialRestriction == null) materialRestriction = new MaterialRestrictionRepository(repoContext);
                return materialRestriction;
            }
        }
        
        public ICategoryAggregationRepository CategoryAggregation
        {
            get
            {
                if (categoryAggregation == null) categoryAggregation = new CategoryAggregationRepository(repoContext);
                return categoryAggregation;
            }
        }
    }
}