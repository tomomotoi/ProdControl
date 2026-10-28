using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace ProdControlWPF.Services
{
    public sealed class CurrentDayStore : ICurrentDayStore
    {
        private const string FileName = "CurrentDay.bin";

        private readonly IDialogService _dialogService;

        public CurrentDayStore(IDialogService dialogService)
        {
            _dialogService = dialogService ?? throw new ArgumentNullException(nameof(dialogService));
        }

        public int Load()
        {
            try
            {
                using (var stream = new FileStream(FileName, FileMode.Open))
                {
                    var formatter = new BinaryFormatter();
                    return (int)formatter.Deserialize(stream);
                }
            }
            catch (FileNotFoundException)
            {
                return -1;
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
                return -1;
            }
        }

        public void Save(int day)
        {
            try
            {
                using (var stream = new FileStream(FileName, FileMode.Create))
                {
                    var formatter = new BinaryFormatter();
                    formatter.Serialize(stream, day);
                }
            }
            catch (Exception exception)
            {
                _dialogService.ShowError(exception.Message, "Ошибка");
            }
        }
    }
}
