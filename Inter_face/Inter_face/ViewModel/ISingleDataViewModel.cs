using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
namespace Inter_face.ViewModel
{
    public interface ISingleDataViewModel
    {
        Inter_face.Models.IDataModel CurrentDataProperty { get; set; }
        System.Collections.ObjectModel.ObservableCollection<Inter_face.Models.IDataModel> DataCollection { get; set; }
        System.Collections.ObjectModel.ObservableCollection<Inter_face.Models.IDataModel> SelectedItems { get; set; }
        bool ShowDataProperty { get; set; }
        bool IsShowQujian { get; set; }
        bool IsShowSignale { get; set; }
        bool IsShowDianFX { get; set; }
        string TypeNameProperty { get; set; }
        int TypeNum { get; set; }
        int SelectedIndex { get; set; }
        RelayCommand ShowDataChangedCommand { get; }
        RelayCommand<System.Windows.Controls.SelectionChangedEventArgs> SelectionChangedCommand { get; }
        RelayCommand ShowRightDialogCommand { get; }
    }
}
