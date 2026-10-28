namespace ProdControlWPF.Domain
{
    public static class TariffsCheckRules
    {
        public static double CalculateTariff(double timeExecute, double positionRateTime)
        {
            return (double)(decimal)(timeExecute * positionRateTime);
        }
    }
}
