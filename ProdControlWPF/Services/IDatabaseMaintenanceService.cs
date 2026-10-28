namespace ProdControlWPF.Services
{
    public interface IDatabaseMaintenanceService
    {
        void BackupIfFirstDayOfMonth();
        void BackupCurrentDatabase();
        void ClearOperationLog();
        void RestoreFromFileName(string fileName);
        void RestoreLatestFromArchive();
        void ClearArchive();
    }
}
