using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IWorkOrderRepository
    {
        IReadOnlyList<WorkOrderRow> GetRows();

        IReadOnlyList<WorkOrderOrderInfo> GetOrders();

        IReadOnlyList<WorkOrderLookup> GetKnitters();

        IReadOnlyList<WorkOrderLookup> GetFormationWorkers();

        void Update(WorkOrderUpdate update);
    }
}
