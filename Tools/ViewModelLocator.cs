using CommunityToolkit.Mvvm.DependencyInjection;
using Tools.ViewModels;

namespace Tools
{
    public class ViewModelLocator
    {
        public MainWindowViewModel MainWindowViewModel => Ioc.Default.GetRequiredService<MainWindowViewModel>();
        public SunflowerViewModel SunflowerViewModel => Ioc.Default.GetRequiredService<SunflowerViewModel>();
        public QrcodeViewModel QrcodeViewModel => Ioc.Default.GetRequiredService<QrcodeViewModel>();
        public CommandsViewModel CommandsViewModel => Ioc.Default.GetRequiredService<CommandsViewModel>();
    }
}
