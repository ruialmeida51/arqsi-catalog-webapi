/* Delete data from all the tables */
DELETE FROM Depth_Measures;
DELETE FROM Height_Measures;
DELETE FROM Width_Measures;
DELETE FROM Depth;
DELETE FROM Height;
DELETE FROM Width;
DELETE FROM Products;
DELETE FROM ProductsAggregations;
DELETE FROM AestheticLines;
DELETE FROM Catalogs;
DELETE FROM Categories;
DELETE FROM Collections;
DELETE FROM Finishes_PriceHistoryFuture;
DELETE FROM Finishes_PriceHistoryPast;
DELETE FROM Finishes;
DELETE FROM Materials_PriceHistoryFuture;
DELETE FROM Materials_PriceHistoryPast;
DELETE FROM Materials;
DELETE FROM ProductMaterialFinishes;
DELETE FROM ProductCatalogs;
DELETE FROM ProductCollections;
DELETE FROM FinishRestriction;
DELETE FROM MaterialRestriction;
DELETE FROM CategoryAggregation;

/* Bootstrap Aesthetic Lines */
insert into AestheticLines(Id, Name, Description) values(1,'Spring Line', 'Spring line with fresh tones.');
insert into AestheticLines(Id, Name, Description) values(2,'Winter Line', 'Winter line with warm tones.');

/* Bootstrap Collections */
insert into Collections(Id, Name, Description, AestheticLineID) values(1,'SpringColl', 'Spring collection designed for you.', 1);
insert into Collections(Id, Name, Description, AestheticLineID) values(2,'WinterColl', 'Winter collection designed for comfort.', 2);

/* Bootstrap Catalogs */
insert into Catalogs(Id, Name, Description, CreationDate) values(1,'2017', 'The 2017 catalog.', CURRENT_TIMESTAMP);
insert into Catalogs(Id, Name, Description, CreationDate) values(2,'2018', 'The 2018 catalog.', CURRENT_TIMESTAMP);


/* Bootstrap Finishes */
insert into Finishes(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(1, 7.50, CURRENT_TIMESTAMP, 'Wax', 'Wax finish good for wood materials.');
insert into Finishes(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(2, 5.50, CURRENT_TIMESTAMP, 'Polish', 'Polish finish for a bright touch.');


/* Bootstrap Materials */
insert into Materials(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(1, 10.50, CURRENT_TIMESTAMP, 'Iron', 'Iron material good for a storage shelfs.');
insert into Materials(Id, PricePSM_CurrentValue, PricePSM_Timestamp, Name, Description) values(2, 15.50, CURRENT_TIMESTAMP, 'Wood', 'Wood material good for residential shelfs.');

/* Bootstrap Category */
insert into Categories(Id, Name) values(1, 'Closet');
insert into Categories(Id, Name) values(2, 'Drawer');
insert into Categories(Id, Name) values(3, 'Hanger');
insert into Categories(Id, Name) values(4, 'Shelf');
insert into Categories(Id, Name) values(5, 'Bedroom Closet');

/* Bootstrap Products */
insert into Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(1, 'TRYSIL', 'A clean looking closet with sliding doors.', 0, 100, 1);
insert into Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(2, 'MALM', 'A chest of four drawers.', 10, 80, 2);
insert into Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(3, 'BUMERANG', 'These hangers of solid wood add a sense of quality.', 100, 100, 3);
insert into Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(4, 'KALLAX', 'This shelf unit will adapt to your needs.', 20, 95, 4);
insert into Products(Id, Name, Description, MinOccupation, MaxOccupation, CategoryId) values(5, 'KVIKNE', 'A bedroom closet that allows more space for furniture.', 0, 100, 5);


/*Bootstrap Category Aggregation*/
insert into CategoryAggregation(RootCategoryId, SubCategoryId) values (1, 5);

/* Bootstrap Invalid finishes */
insert into FinishRestriction(ProductId, InvalidFinishId) values(1, 1);
insert into FinishRestriction(ProductId, InvalidFinishId) values(1, 2);
insert into FinishRestriction(ProductId, InvalidFinishId) values(2, 1);
insert into FinishRestriction(ProductId, InvalidFinishId) values(2, 2);

/* Bootstrap invalid materials */
insert into MaterialRestriction(ProductId, InvalidMaterialId) values(1, 2);
insert into MaterialRestriction(ProductId, InvalidMaterialId) values(1, 1);
insert into MaterialRestriction(ProductId, InvalidMaterialId) values(2, 1);
insert into MaterialRestriction(ProductId, InvalidMaterialId) values(2, 2);

/* Bootstrap Product Catalogs */
insert into ProductCatalogs(ProductId, CatalogId) values(1, 1);
insert into ProductCatalogs(ProductId, CatalogId) values(1, 2);
insert into ProductCatalogs(ProductId, CatalogId) values(2, 1);
insert into ProductCatalogs(ProductId, CatalogId) values(3, 2);

/* Bootstrap Product Collection */
insert into ProductCollections(ProductId, CollectionId) values(1, 1);
insert into ProductCollections(ProductId, CollectionId) values(1, 2);
insert into ProductCollections(ProductId, CollectionId) values(2, 1);
insert into ProductCollections(ProductId, CollectionId) values(3, 2);

/* Bootstrap Product Collection */
insert into ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(1, 1, 2);
insert into ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(2, 2, 1);
insert into ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(3, 1, 2);
insert into ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(4, 2, 2);
insert into ProductMaterialFinishes(ProductId, MaterialId, FinishId) values(5, 1, 1);

/* Bootstrap Product Aggregations */
insert into ProductsAggregations(RootProductId, SubproductId, isRequired) values(1, 3, '0');
insert into ProductsAggregations(RootProductId, SubproductId, isRequired) values(1, 4, '0');
insert into ProductsAggregations(RootProductId, SubproductId, isRequired) values(5, 3, '1');

/* Bootstrap Dimensions */
/* PRODUCT 1 */
insert into Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, '1', '0', 300, 500);
insert into Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, '1', '0', 200, 600);
insert into Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(1, '1', '0', 300, 500);
/* PRODUCT 2 */
insert into Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, '1', '0', 100, 300);
insert into Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, '1', '0', 100, 150);
insert into Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(2, '1', '0', 50, 75);
/* PRODUCT 3 */
insert into Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, '1', '0', 20, 35);
insert into Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, '1', '0', 20, 35);
insert into Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(3, '1', '0', 20, 35);

/* PRODUCT 4 */
insert into Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, '1', '0', 300, 500);
insert into Width_Measures(Id, DiscreteId, Value) values(4, 1, 200);
insert into Width_Measures(Id, DiscreteId, Value) values(4, 2, 300);
insert into Width_Measures(Id, DiscreteId, Value) values(4, 3, 400);
insert into Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, '1', '0', 200, 600);
insert into Height_Measures(Id, DiscreteId, Value) values(4, 4, 200);
insert into Height_Measures(Id, DiscreteId, Value) values(4, 5, 300);
insert into Height_Measures(Id, DiscreteId, Value) values(4, 6, 400);
insert into Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(4, '0', '1', 0, 0);
insert into Depth_Measures(Id, DiscreteId, Value) values(4, 7, 200);
insert into Depth_Measures(Id, DiscreteId, Value) values(4, 8, 300);
insert into Depth_Measures(Id, DiscreteId, Value) values(4, 9, 400);

/* PRODUCT 5 */
insert into Width(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, '1', '0', 300, 500);
insert into Height(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, '1', '0', 200, 600);
insert into Depth(Id, IsContinuous, IsDiscrete, MinMeasure_Value, MaxMeasure_Value) values(5, '0', '1', 0, 0);
insert into Depth_Measures(Id, DiscreteId, Value) values(5, 10, 200);
insert into Depth_Measures(Id, DiscreteId, Value) values(5, 11, 300);
insert into Depth_Measures(Id, DiscreteId, Value) values(5, 12, 400);


