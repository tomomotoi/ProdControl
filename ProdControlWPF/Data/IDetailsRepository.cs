using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IDetailsRepository
    {
        IList<DetailItem> GetDetails();

        int InsertDetail(string partName);

        int UpdateDetail(int id, string partName);

        int DeleteDetail(int id);
    }
}
