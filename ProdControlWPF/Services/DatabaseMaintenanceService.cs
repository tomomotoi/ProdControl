using System;
using System.IO;
using System.Linq;
using ProdControlWPF.Data;

namespace ProdControlWPF.Services
{
    public sealed class DatabaseMaintenanceService : IDatabaseMaintenanceService
    {
        private const string DatabaseFileName = "OperationDB.mdb";
        private const string ArchiveDirectoryName = "Архив";

        private readonly IDialogService _dialogService;
        private readonly IOperationLogRepository _operationLogRepository;
        private readonly ICurrentDayStore _currentDayStore;

        public DatabaseMaintenanceService(
            IDialogService dialogService,
            IOperationLogRepository operationLogRepository,
            ICurrentDayStore currentDayStore)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
            _operationLogRepository = operationLogRepository ?? throw new ArgumentNullException(nameof(operationLogRepository));
            _currentDayStore = currentDayStore ?? throw new ArgumentNullException(nameof(currentDayStore));
        }

        public void BackupIfFirstDayOfMonth()
        {
            if (DateTime.Today.Day < _currentDayStore.Load())
            {
                BackupCurrentDatabase();
            }
        }

        public void BackupCurrentDatabase()
        {
            var currentPath = Path.Combine(Environment.CurrentDirectory, DatabaseFileName);
            var currentDate = DateTime.UtcNow.AddHours(3);
            var copyFileName = string.Format("OperationDB-{0}.mdb", currentDate.ToString("dd.MM.yyyy--HH-mm-ss-ffff"));
            var destinationPath = Path.Combine(Environment.CurrentDirectory, ArchiveDirectoryName, copyFileName);

            try
            {
                File.Copy(currentPath, destinationPath);
                _dialogService.ShowInfo("Копия базы данных была успешно создана. Имя копии: " + copyFileName, string.Empty);
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }

        public void ClearOperationLog()
        {
            try
            {
                var rowsAffected = _operationLogRepository.Clear();

                if (rowsAffected > 0)
                {
                    _dialogService.ShowInfo(
                        string.Format("Таблица OperationLog была успешно очищена, количество очищенных записей: {0}", rowsAffected),
                        "Уведомление");
                }
                else
                {
                    _dialogService.ShowInfo("Таблица OperationLog была пустой или не удалось очистить данные!", "Уведомление");
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }

        public void RestoreFromFileName(string fileName)
        {
            var currentFilePath = Path.Combine(Environment.CurrentDirectory, DatabaseFileName);

            try
            {
                File.Copy(fileName, currentFilePath, true);
                _dialogService.ShowInfo("База данных была успешно восстановлена!", "Уведомление");
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(
                    exception.Message + Environment.NewLine + "Не удалось восстановить базу данных!",
                    "Ошибка");
            }
        }

        public void RestoreLatestFromArchive()
        {
            var archivePath = Path.Combine(Environment.CurrentDirectory, ArchiveDirectoryName);

            try
            {
                if (Directory.Exists(archivePath))
                {
                    var directoryInfo = new DirectoryInfo(archivePath);

                    if (directoryInfo.GetFiles().Length > 0)
                    {
                        var latestFile = directoryInfo.EnumerateFiles("*.mdb")
                            .OrderByDescending(file => file.CreationTime)
                            .First()
                            .FullName;

                        RestoreFromFileName(latestFile);
                    }
                    else
                    {
                        _dialogService.ShowError("В папке 'Архив' отсутствуют копии базы данных!", "Ошибка");
                    }
                }
                else
                {
                    _dialogService.ShowWarning(
                        "Папка 'Архив' отсутствует! Она будет создана автоматически после закрытия данного окна. База данных не будет восстановлена.",
                        "Предупреждение");
                    Directory.CreateDirectory(ArchiveDirectoryName);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }

        public void ClearArchive()
        {
            var archivePath = Path.Combine(Environment.CurrentDirectory, ArchiveDirectoryName);

            if (!Directory.Exists(archivePath))
            {
                _dialogService.ShowWarning(
                    "Папка 'Архив' отсутствует! Она будет создана автоматически после закрытия данного окна.",
                    "Предупреждение");
                Directory.CreateDirectory(ArchiveDirectoryName);
                return;
            }

            try
            {
                var directoryInfo = new DirectoryInfo(archivePath);
                var allFilesCount = directoryInfo.GetFiles().Length;

                if (allFilesCount == 0)
                {
                    return;
                }

                var filesAvailableForCleanUp = allFilesCount - 1;

                if (filesAvailableForCleanUp == 0)
                {
                    _dialogService.ShowInfo("Нет файлов доступных для очистки!", "Уведомление");
                    return;
                }

                var prompt = string.Format(
                    "Доступно {0} файлов для очистки. Введите количество файлов, необходимое для очистки:",
                    filesAvailableForCleanUp);
                var userInput = _dialogService.Prompt(prompt, "Очистка архива");

                if (userInput == "0" || userInput == string.Empty)
                {
                    return;
                }

                if (!int.TryParse(userInput, out var fileCleanUpAmount))
                {
                    _dialogService.ShowError("Введен неверный формат числа!", "Ошибка");
                    return;
                }

                if (fileCleanUpAmount >= allFilesCount)
                {
                    _dialogService.ShowError(
                        "Указанное количество превышает доступное количество файлов для очистки!",
                        "Ошибка");
                    return;
                }

                for (var index = 0; index < fileCleanUpAmount; index++)
                {
                    var oldestFile = directoryInfo.EnumerateFiles("*.mdb")
                        .OrderByDescending(file => file.CreationTime)
                        .Last()
                        .FullName;

                    File.Delete(oldestFile);
                }

                _dialogService.ShowInfo("Архив был успешно очищен!", "Уведомление");
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }
    }
}
