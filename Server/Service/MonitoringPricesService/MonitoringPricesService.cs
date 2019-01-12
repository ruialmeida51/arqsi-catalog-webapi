using System;
using System.Collections.Generic;
using Server.Model;
using Server.Repository.Wrapper;

namespace Server.Service.MonitoringPricesService
{
    public abstract class MonitoringPricesService
    {
        public static void VerifyPricesOfFinishes(IRepositoryWrapper _repositoryWrapper, IEnumerable<Finish> finishes)
        {
            foreach (var finish in finishes)
            {
                VerifyPricesFinish(_repositoryWrapper, finish);
            }    
        }
        
        public static void VerifyPricesFinish(IRepositoryWrapper _repositoryWrapper, Finish finish)
        {
            PriceHistory mostUpToDate = null;
            foreach (var item in finish.PricePSM.PriceHistoryFuture)
            {
                if (mostUpToDate == null 
                    &&  item.Timestamp < DateTime.Now  
                    || mostUpToDate != null 
                    && item.Timestamp < DateTime.Now 
                    && mostUpToDate.Timestamp < item.Timestamp)
                {
                        mostUpToDate = item;
                }
            }
            
            if (mostUpToDate == null) return;
            if (!finish.PricePSM.EditPrice(mostUpToDate.Price, mostUpToDate.Timestamp)) return;
            finish.PricePSM.PriceHistoryFuture.Remove(mostUpToDate);
            _repositoryWrapper.Finish.UpdateFinish(finish);

        }
        
        public static void VerifyPricesOfMaterials(IRepositoryWrapper _repositoryWrapper, IEnumerable<Material> materials)
        {
            foreach (var material in materials)
            {
                VerifyPricesMaterial(_repositoryWrapper, material);
            }    
        }
        
        public static void VerifyPricesMaterial(IRepositoryWrapper _repositoryWrapper, Material material)
        {
            PriceHistory mostUpToDate = null;
            foreach (var item in material.PricePSM.PriceHistoryFuture)
            {
                if (mostUpToDate == null 
                    &&  item.Timestamp < DateTime.Now  
                    || mostUpToDate != null 
                    && item.Timestamp < DateTime.Now 
                    && mostUpToDate.Timestamp < item.Timestamp)
                {
                    mostUpToDate = item;
                }
            }
            
            if (mostUpToDate == null) return;
            if (!material.PricePSM.EditPrice(mostUpToDate.Price, mostUpToDate.Timestamp)) return;
            material.PricePSM.PriceHistoryFuture.Remove(mostUpToDate);
            _repositoryWrapper.Material.UpdateMaterial(material);
        }
    }
}

 