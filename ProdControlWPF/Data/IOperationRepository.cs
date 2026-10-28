using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface IOperationRepository
    {
        IList<OperationItem> GetOperations();

        IList<PositionItem> GetPositions();

        bool OperationNameExists(string operationName);

        int InsertOperation(string operationName, int positionId);

        bool IsOperationUsed(int operationId);

        int DeleteOperation(int operationId);
    }
}
