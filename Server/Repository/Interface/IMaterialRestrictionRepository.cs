using System.Collections.Generic;
using System.Threading.Tasks;
using Server.Model.Restriction;
using Server.Repository.Base;

namespace Server.Repository.Interface
{
    public interface IMaterialRestrictionRepository: IRepositoryBase<MaterialRestriction>
    {
        Task<IEnumerable<MaterialRestriction>> GetAllMaterialRestrictions();
        Task<IEnumerable<MaterialRestriction>> GetMaterialRestrictionById(long productID);
        Task<bool> CreateMaterialRestriction(MaterialRestriction materialRestriction);
        Task<bool> RemoveMaterialRestriction(long productID);
    }
}