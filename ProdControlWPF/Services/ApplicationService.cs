using System.Windows;

namespace ProdControlWPF.Services
{
    public sealed class ApplicationService : IApplicationService
    {
        public void Exit()
        {
            var application = Application.Current;

            if (application != null)
            {
                application.Shutdown();
            }
        }
    }
}
