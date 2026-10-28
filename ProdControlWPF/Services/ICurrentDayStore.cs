namespace ProdControlWPF.Services
{
    public interface ICurrentDayStore
    {
        int Load();
        void Save(int day);
    }
}
