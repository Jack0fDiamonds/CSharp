using System.ComponentModel;
using System.Windows;

namespace Lab01.Tools
{
    interface ILoaderOwner:INotifyPropertyChanged
    {
        Visibility LoaderVisibility { get; set; }
        bool IsControlEnabled { get; set; }
    }
}
