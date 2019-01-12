/* Delete data from all the tables */
DELETE FROM dbo.__EFMigrationsHistory;
DELETE FROM dbo.Users;
DELETE FROM dbo.Depth_Measures;
DELETE FROM dbo.Height_Measures;
DELETE FROM dbo.Width_Measures;
DELETE FROM dbo.Depth;
DELETE FROM dbo.Height;
DELETE FROM dbo.Width;
DELETE FROM dbo.Products;
DELETE FROM dbo.ProductsAggregations;
DELETE FROM dbo.AestheticLines;
DELETE FROM dbo.Catalogs;
DELETE FROM dbo.Categories;
DELETE FROM dbo.Collections;
DELETE FROM dbo.Finishes_PriceHistoryFuture;
DELETE FROM dbo.Finishes_PriceHistoryPast;
DELETE FROM dbo.Finishes;
DELETE FROM dbo.Materials_PriceHistoryFuture;
DELETE FROM dbo.Materials_PriceHistoryPast;
DELETE FROM dbo.Materials;
DELETE FROM dbo.ProductMaterialFinishes;
DELETE FROM dbo.ProductCatalogs;
DELETE FROM dbo.ProductCollections;
DELETE FROM dbo.FinishRestriction;
DELETE FROM dbo.MaterialRestriction;
DELETE FROM dbo.CategoryAggregation;

/* Bootstrap Aesthetic Lines */
SET IDENTITY_INSERT dbo.AestheticLines ON
insert into dbo.AestheticLines(Id, Name, Description) values(1,'Spring Line', 'Spring line with fresh tones.');
insert into dbo.AestheticLines(Id, Name, Description) values(2,'Winter Line', 'Winter line with warm tones.');
SET IDENTITY_INSERT dbo.AestheticLines OFF

/* Bootstrap Collections */
SET IDENTITY_INSERT dbo.Collections ON
insert into dbo.Collections(Id, Name, Description, AestheticLineID) values(1,'SpringColl', 'Spring collection designed for you.', 1);
insert into dbo.Collections(Id, Name, Description, AestheticLineID) values(2,'WinterColl', 'Winter collection designed for comfort.', 2);
SET IDENTITY_INSERT dbo.Collections OFF

/* Bootstrap Catalogs */
SET IDENTITY_INSERT dbo.Catalogs ON
insert into dbo.Catalogs(Id, Name, Description, CreationDate) values(1,'2017', 'The 2017 catalog.', (convert(datetime,'15-12-17', 5)));
insert into dbo.Catalogs(Id, Name, Description, CreationDate) values(2,'2018', 'The 2018 catalog.', (convert(datetime,'10-05-18', 5)));
SET IDENTITY_INSERT dbo.Catalogs OFF

/* Bootstrap Finishes */
SET IDENTITY_INSERT dbo.Finishes ON
insert into dbo.Finishes(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(1, 7.50, CURRENT_TIMESTAMP, 'Wax', 'Wax finish good for wood materials.');
insert into dbo.Finishes(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(2, 5.50, CURRENT_TIMESTAMP, 'Polish', 'Polish finish for a bright touch.');
SET IDENTITY_INSERT dbo.Finishes OFF

/* Bootstrap Materials */
SET IDENTITY_INSERT dbo.Materials ON
insert into dbo.Materials(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(1, 10.50, CURRENT_TIMESTAMP, 'Iron', 'Iron material good for a storage shelfs.');
insert into dbo.Materials(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(2, 15.50, CURRENT_TIMESTAMP, 'Wood', 'Wood material good for residential shelfs.');
SET IDENTITY_INSERT dbo.Materials OFF

/* Bootstrap Category */
SET IDENTITY_INSERT dbo.Categories ON
insert into dbo.Categories(Id, Name) values(1, 'Closet');
insert into dbo.Categories(Id, Name) values(2, 'Drawer');
insert into dbo.Categories(Id, Name) values(3, 'Hanger');
insert into dbo.Categories(Id, Name) values(4, 'Shelf');
insert into dbo.Categories(Id, Name) values(5, 'Bedroom Closet');
SET IDENTITY_INSERT dbo.Categories OFF

/* Bootstrap Products */
SET IDENTITY_INSERT dbo.Products ON
insert into dbo.Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(1, 'TRYSIL', 'A clean looking closet with sliding doors.', 0, 100, 1);
insert into dbo.Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(2, 'MALM', 'A chest of four drawers.', 10, 80, 2);
insert into dbo.Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(3, 'BUMERANG', 'These hangers of solid wood add a sense of quality.', 100, 100, 3);
insert into dbo.Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(4, 'KALLAX', 'This shelf unit will adapt to your needs.', 20, 95, 4);
insert into dbo.Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(5, 'KVIKNE', 'A bedroom closet that allows more space for furniture.', 0, 100, 5);
SET IDENTITY_INSERT dbo.Products OFF

/*Bootstrap Category Aggregation*/
insert into dbo.CategoryAggregation(RootCategoryId, SubCategoryId) values (1, 5);

/* Bootstrap Invalid finishes */
insert into dbo.FinishRestriction(ProductId, InvalidFinishId) values(1, 1);
insert into dbo.FinishRestriction(ProductId, InvalidFinishId) values(1, 2);
insert into dbo.FinishRestriction(ProductId, InvalidFinishId) values(2, 1);
insert into dbo.FinishRestriction(ProductId, InvalidFinishId) values(2, 2);

/* Bootstrap invalid materials */
insert into dbo.MaterialRestriction(ProductId, InvalidMaterialId) values(1, 2);
insert into dbo.MaterialRestriction(ProductId, InvalidMaterialId) values(1, 1);
insert into dbo.MaterialRestriction(ProductId, InvalidMaterialId) values(2, 1);
insert into dbo.MaterialRestriction(ProductId, InvalidMaterialId) values(2, 2);

/* Bootstrap Product Catalogs */
insert into dbo.ProductCatalogs(ProductId, CatalogId) values(1, 1);
insert into dbo.ProductCatalogs(ProductId, CatalogId) values(1, 2);
insert into dbo.ProductCatalogs(ProductId, CatalogId) values(2, 1);
insert into dbo.ProductCatalogs(ProductId, CatalogId) values(3, 2);

/* Bootstrap Product Collection */
insert into dbo.ProductCollections(ProductId, CollectionId) values(1, 1);
insert into dbo.ProductCollections(ProductId, CollectionId) values(1, 2);
insert into dbo.ProductCollections(ProductId, CollectionId) values(2, 1);
insert into dbo.ProductCollections(ProductId, CollectionId) values(3, 2);

/* Bootstrap Product Collection */
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(1, 1, 2);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(1, 1, 1);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(1, 2, 1);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(1, 2, 2);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(2, 2, 1);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(3, 1, 2);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(4, 2, 2);
insert into dbo.ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(5, 1, 1);

/* Bootstrap Product Aggregations */
insert into dbo.ProductsAggregations(RootProductId, SubproductId, isRequired) values(1, 3, 'false');
insert into dbo.ProductsAggregations(RootProductId, SubproductId, isRequired) values(1, 4, 'false');
insert into dbo.ProductsAggregations(RootProductId, SubproductId, isRequired) values(5, 3, 'true');

/* Bootstrap Dimensions */
/* PRODUCT 1 */
insert into dbo.Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, 'true', 'false', 300, 500);
insert into dbo.Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, 'true', 'false', 200, 600);
insert into dbo.Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, 'true', 'false', 300, 500);
/* PRODUCT 2 */
insert into dbo.Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, 'true', 'false', 100, 300);
insert into dbo.Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, 'true', 'false', 100, 150);
insert into dbo.Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, 'true', 'false', 50, 75);
/* PRODUCT 3 */
insert into dbo.Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, 'true', 'false', 20, 35);
insert into dbo.Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, 'true', 'false', 20, 35);
insert into dbo.Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, 'true', 'false', 20, 35);

/* PRODUCT 4 */
insert into dbo.Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, 'true', 'false', 300, 500);
insert into dbo.Width_Measures(Id, Value) values(4, 200);
insert into dbo.Width_Measures(Id, Value) values(4, 300);
insert into dbo.Width_Measures(Id, Value) values(4, 400);
insert into dbo.Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, 'true', 'false', 200, 600);
insert into dbo.Height_Measures(Id, Value) values(4, 200);
insert into dbo.Height_Measures(Id, Value) values(4, 300);
insert into dbo.Height_Measures(Id, Value) values(4, 400);
insert into dbo.Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, 'false', 'true', 0, 0);
insert into dbo.Depth_Measures(Id, Value) values(4, 200);
insert into dbo.Depth_Measures(Id, Value) values(4, 300);
insert into dbo.Depth_Measures(Id, Value) values(4, 400);

/* PRODUCT 5 */
insert into dbo.Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, 'true', 'false', 300, 500);
insert into dbo.Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, 'true', 'false', 200, 600);
insert into dbo.Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, 'false', 'true', 0, 0);
insert into dbo.Depth_Measures(Id, Value) values(5, 200);
insert into dbo.Depth_Measures(Id, Value) values(5, 300);
insert into dbo.Depth_Measures(Id, Value) values(5, 400);


