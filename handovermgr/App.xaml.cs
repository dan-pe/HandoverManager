using DependencyContainer;
using Ninject;
using System.Windows;

namespace handovermgr
{
    public partial class App : Application
    {
        private IKernel iocKernel;

        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            iocKernel = new StandardKernel();
            iocKernel.Load(new DependecyContainer());

            Current.MainWindow = iocKernel.Get<MainWindow>();
            Current.MainWindow.Show();
        }
    }
}
