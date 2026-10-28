using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IMachinesRepository
    {
        IList<MachineItem> GetMachines();

        int InsertMachine(string make, string model);

        int UpdateMachine(int id, string make, string model);

        int DeleteMachine(int id);
    }
}
