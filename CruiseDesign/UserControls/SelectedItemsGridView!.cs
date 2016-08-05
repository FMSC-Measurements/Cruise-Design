using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace FMSC.Controls
{
    public class SelectedItemsGridView : DataGridView
    {
        private IList _SelectedItems;

        private DataGridViewCheckBoxColumn _selectItemColumn = new DataGridViewCheckBoxColumn(false)
            {
                AutoSizeMode = DataGridViewAutoSizeColumnMode.ColumnHeader,
                HeaderText = "Select"
            };

        public IList SelectedItems 
        {
            get { return _SelectedItems; }
            set
            {
                _SelectedItems = value;
                ClearSelection();
                Refresh();
            }
        }

        public SelectedItemsGridView()
            : base()
        {
            SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            RowHeadersVisible = false;
            VirtualMode = true;
        }

        protected override void OnCreateControl()
        {
            base.OnCreateControl();
            if (DesignMode == true) { return; }
            Columns.Insert(0, _selectItemColumn);
        }

        public bool IsItemSelected(object obj)
        {
            if (SelectedItems == null) { return false; }
            return SelectedItems.Contains(obj);
        }

        protected override void OnCellValueNeeded(DataGridViewCellValueEventArgs e)
        {
            base.OnCellValueNeeded(e);
            IList list = this.DataSource as IList;
            int index = e.RowIndex;
            if (list == null) { return; }
            if (index < 0 || index > list.Count) { return; }
            if (e.ColumnIndex == 0 )
            {
                e.Value = IsItemSelected(list[index]);
            }
        }

        protected override void OnCellContentClick(DataGridViewCellEventArgs e)
        {
            base.OnCellContentClick(e);
            if (e.RowIndex == -1) { return; }
            if (SelectedItems == null) { return; }
            if (DataSource == null || (DataSource is IList) == false) { return; }
            Object item = ((IList)DataSource)[e.RowIndex];
            if(e.ColumnIndex == 0)
            {
                var cellValue = this[e.ColumnIndex, e.RowIndex].Value;
                if (cellValue != null && (bool)cellValue == true)
                {
                    SelectedItems.Remove(item);
                    
                }
                else if (cellValue != null && (bool)cellValue == false)
                {
                    SelectedItems.Add(item);
                }
            }
        }

        protected override void OnColumnRemoved(DataGridViewColumnEventArgs e)
        {
            base.OnColumnRemoved(e);
            if (Columns.Contains(_selectItemColumn) == false)
            {
                Columns.Insert(0, _selectItemColumn);
            }
        }
    }
}
