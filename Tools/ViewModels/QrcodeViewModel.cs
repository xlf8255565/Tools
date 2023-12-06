using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows.Media;

namespace Tools.ViewModels
{
    public partial class QrcodeViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ImageSource image;
    }
}
