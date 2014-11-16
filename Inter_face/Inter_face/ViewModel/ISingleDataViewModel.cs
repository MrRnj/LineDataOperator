using GalaSoft.MvvmLight.Command;
using System;
namespace Inter_face.ViewModel
{
    public interface ISingleDataViewModel
    {
        Inter_face.Models.IDataModel CurrentDataProperty { get; set; }
        System.Collections.ObjectModel.ObservableCollection<Inter_face.Models.IDataModel> DataCollection { get; set; }
        bool ShowDataProperty { get; set; }
        string TypeNameProperty { get; set; }
        int TypeNum { get; set; }
        int SelectedIndex { get; set; }
        RelayCommand ShowDataChangedCommand { get; }
    }
}
