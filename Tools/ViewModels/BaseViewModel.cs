using CommunityToolkit.Mvvm.ComponentModel;
using System.Windows;

namespace Tools.ViewModels
{
    public class BaseViewModel : ObservableObject, IViewModel
    {
        public Window View { get; set; }
    }
    public interface IViewModel
    {
        Window View { get; set; }
    }
}
