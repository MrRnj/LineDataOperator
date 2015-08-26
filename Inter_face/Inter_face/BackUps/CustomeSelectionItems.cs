using Inter_face.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Inter_face.BackUps
{
    public class CustomeListBox : ListBox
    {
        public IList<IDataModel> cusSelectedItems
        {
            get { return (IList<IDataModel>)GetValue(cusSelectedItemsProperty); }
            set { SetValue(cusSelectedItemsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for SelectedItems.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty cusSelectedItemsProperty =
            DependencyProperty.Register("cusSelectedItems", typeof(IList<IDataModel>), typeof(CustomeListBox), new PropertyMetadata(new List<IDataModel>()));

        protected override void OnSelectionChanged(SelectionChangedEventArgs e)
        {
           base.OnSelectionChanged(e);
           //添加用户选中的当前项.
           foreach (IDataModel item in e.AddedItems)
           {
               cusSelectedItems.Add(item);
           }
           //删除用户取消选中的当前项
           foreach (IDataModel item in e.RemovedItems)
           {
               cusSelectedItems.Remove(item);
           }
        }
    }
}
