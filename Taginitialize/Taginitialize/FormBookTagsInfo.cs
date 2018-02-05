using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Infrastructure;
using Infrastructure.Entity;
using DevExpress.XtraGrid;

namespace Phychips.PR9200
{
    public partial class FormBookTagsInfo : DevExpress.XtraEditors.XtraForm
    {
        private List<BookshelfInfo> listBookshelfInfo = new List<BookshelfInfo>();
        private Func<DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs, bool> initRowIndicator = e =>
        {
            e.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            if (e.Info.IsRowIndicator)
            {
                if (e.RowHandle == GridControl.AutoFilterRowHandle)
                {
                    e.Info.DisplayText = "筛选行";
                }
                if (e.RowHandle >= 0)
                {
                    e.Info.DisplayText = (e.RowHandle + 1).ToString();
                }
                else if (e.RowHandle < 0 && e.RowHandle > -1000)
                {
                    e.Info.Appearance.BackColor = System.Drawing.Color.AntiqueWhite;
                    e.Info.DisplayText = "G" + e.RowHandle.ToString();
                }
            }
            return false;
        };
        public FormBookTagsInfo()
        {
            InitializeComponent();
            gvRecordList.OptionsCustomization.AllowFilter = true;
            gvRecordList.IndicatorWidth = 80;
            gvRecordList.OptionsView.ShowIndicator = true;
            gvRecordList.CustomDrawRowIndicator += (s, e) => initRowIndicator(e);
            gvRecordList.RowStyle += GvRecordList_RowStyle;
        }

        private void FormBookTagsInfo_Load(object sender, EventArgs e)
        {
            listBookshelfInfo = TagInfoDAL.GetInfoList<BookshelfInfo>("select * from bookshelf_info", null, null);
            grdBookshelfInfo.DataSource = listBookshelfInfo;
            grdRecordList.DataSource = GetRecordList();
        }

        private void grdBookshelfInfo_DoubleClick(object sender, EventArgs e)
        {

        }

        private DataTable GetRecordList(string filter=null)
        {
            return TagInfoDAL.GetBookInitMappingBooks(filter);
        }
        private void GvRecordList_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0) return;
            var row = gvRecordList.GetDataRow(e.RowHandle);
            if (row == null) return;
            if (row["borrowed_status"].ToString()==2.ToString()){
                e.Appearance.BackColor = Color.Green;
                e.Appearance.BackColor2 = Color.Green;
                e.Appearance.ForeColor = Color.White;
            }
        }
        private void grdBookshelfInfo_Click(object sender, EventArgs e)
        {
            var bookShelfInfo = gvBookshelfInfo.GetFocusedRow() as BookshelfInfo;
            if (bookShelfInfo != null)
            {
                gvRecordList.ActiveFilter.Clear();
                gvRecordList.ActiveFilter.NonColumnFilter = $"code='{bookShelfInfo.code}'";
            }
        }
    }
}