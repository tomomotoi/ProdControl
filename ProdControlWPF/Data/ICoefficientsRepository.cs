using System.Collections.Generic;
using ProdControlWPF.Models;

namespace ProdControlWPF.Data
{
    public interface ICoefficientsRepository
    {
        IList<CoefficientItem> GetCoefficients();

        bool CoefficientExists(double value);

        int InsertCoefficient(double value);

        int DeleteCoefficient(int id, double value);
    }
}
