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
        System.Windows.Visibility MenuVisibility { get; set; }
        System.Windows.Visibility QjMenuVisibility { get; set; }
        System.Windows.Visibility XhMenuVisibility { get; set; }
        System.Windows.Visibility DfxMenuVisibility { get; set; }
        bool CanMoveSignal { get; set; }
        bool ShowDataProperty { get; set; }
        bool IsShowQujian { get; set; }
        bool IsShowSignale { get; set; }
        bool IsShowDianFX { get; set; }
        bool IsSingleQJ { get; set; }
        string TypeNameProperty { get; set; }
        int TypeNum { get; set; }
        int SelectedIndex { get; set; }
        RelayCommand ShowDataChangedCommand { get; }
        RelayCommand<System.Windows.Controls.SelectionChangedEventArgs> SelectionChangedCommand { get; }
        RelayCommand ShowRightDialogCommand { get; }        
        RelayCommand<string> ExCommand { get; }
    }
}
