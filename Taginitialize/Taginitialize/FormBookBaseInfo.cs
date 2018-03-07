using DevExpress.XtraGrid;
using Infrastructure;
using Infrastructure.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Phychips.PR9200
{
    public partial class FormBookBaseInfo : BaseForm
    {
        public Form fatherForm;
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
        public FormBookBaseInfo()
        {
            InitializeComponent();
            gvSystem.CustomDrawRowIndicator += (s, e) => initRowIndicator(e);
            this.Load += delegate {
                LoadData();
                gvSystem.OptionsCustomization.AllowFilter = true;
                gvSystem.IndicatorWidth = 80;
                gvSystem.OptionsView.ShowIndicator = true;
            };
        }

        private void FormBookBaseInfo_Load(object sender, EventArgs e)
        {
        }

        private void LoadData() {
            this.btnDetail.DataSource = TagInfoDAL.SelectBookInfoExtendList();
        }
        private void gvSystem_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (e.RowHandle < 0) return;
                var row = gvSystem.GetRow(e.RowHandle) as BookInfoExtend;
                if (row == null) return;
                if (string.IsNullOrEmpty(row.book_name)
                    || string.IsNullOrEmpty(row.min_age)
                    || string.IsNullOrEmpty(row.max_age)
                    || string.IsNullOrEmpty(row.topical_name)
                    || string.IsNullOrEmpty(row.imgurl)
                    || string.IsNullOrEmpty(row.author)
                    || string.IsNullOrEmpty(row.brief)
                    || row.brief == "暂无简介"
                    || string.IsNullOrEmpty(row.describe)
                    || string.IsNullOrEmpty(row.min_max_age)
                    || string.IsNullOrEmpty(row.author)
                    )
                {
                    e.Appearance.BackColor = Color.Red;
                    e.Appearance.BackColor2 = Color.Red;
                }
            }
            catch { }
        }


        private void grdSystem_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                var focuseRow = gvSystem.GetFocusedRow();
                if (null == focuseRow) {

                }
                else
                {
                  var bookInfoExtend = focuseRow as BookInfoExtend;
                    FormBookBaseInfoDetail formBookBaseInfoDetail = new FormBookBaseInfoDetail(bookInfoExtend);
                    formBookBaseInfoDetail.fathForm = this;
                    formBookBaseInfoDetail.ShowDialog();
                }
            }
            catch(Exception ee) {
                MessageBox.Show(ee.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void UpdateGridRow(BookInfoExtend bookInfoExtend) {
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "book_name", bookInfoExtend.book_name);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "author", bookInfoExtend.author);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "brief", bookInfoExtend.brief);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "describe", bookInfoExtend.describe);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "imgurl", bookInfoExtend.imgurl);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "max_age", bookInfoExtend.max_age);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "min_age", bookInfoExtend.min_age);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "min_max_age", bookInfoExtend.min_max_age);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "price", bookInfoExtend.price);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "topical_code", bookInfoExtend.topical_code);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "topical_name", bookInfoExtend.topical_name);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "publication_date", bookInfoExtend.publication_date);
            gvSystem.SetRowCellValue(gvSystem.FocusedRowHandle, "press", bookInfoExtend.press);
            gvSystem.RefreshData();
        }
        public BookInfoExtend SetBookinfoEvent(RowEnum rowEnum)
        {
            if (gvSystem.IsLastRow&&rowEnum==RowEnum.NextRow) {
                MessageBox.Show("已经是最后一行啦！");
                return null;
            }
            if (gvSystem.IsFirstRow && rowEnum == RowEnum.PreviousRow) {
                MessageBox.Show("已经是第一行啦！");
                return null;
            }
            int rowHander = gvSystem.FocusedRowHandle;
            BookInfoExtend bookInfoExtend = null;
            if (rowEnum == RowEnum.NextRow) {
                rowHander = ++rowHander;
                bookInfoExtend = gvSystem.GetRow(rowHander) as BookInfoExtend;
            }
            if (rowEnum == RowEnum.PreviousRow) {
                rowHander = --rowHander;
                bookInfoExtend = gvSystem.GetRow(rowHander) as BookInfoExtend;
            }
            gvSystem.SelectRow(rowHander);
            gvSystem.FocusedRowHandle = rowHander;
            return bookInfoExtend;
        }

        private void 导出excelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ExportFile();
        }
        private void ExportFile() {
            SaveFileDialog sav = new SaveFileDialog();
            sav.Filter = "Excel文件(*.xls)|*.xls";
            DevExpress.XtraPrinting.XlsExportOptions xlsOptions = new DevExpress.XtraPrinting.XlsExportOptions();
            if (sav.ShowDialog() == DialogResult.OK)
            {
                btnDetail.ExportToXls(sav.FileName);
                DevExpress.XtraEditors.XtraMessageBox.Show("保存成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportFile();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            LoadData();
            gvSystem.RefreshData();
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            grdSystem_DoubleClick(null, null);
        }
        private void toggleSwitch_Toggled(object sender, EventArgs e)
        {
            if (toggleSwitch.IsOn) {
                gvSystem.ActiveFilter.Clear();
                gvSystem.ActiveFilter.NonColumnFilter = "min_age is null or book_name is null or max_age is null or topical_name is null or imgurl is null or author is null or describe is null or brief is null or brief='暂无简介' or  press is null";
                return;
            }
            gvSystem.ActiveFilter.Clear();
        }
    }
}
