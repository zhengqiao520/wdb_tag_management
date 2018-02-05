namespace Phychips.PR9200
{
    partial class FormBookTagsInfo
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainerControl1 = new DevExpress.XtraEditors.SplitContainerControl();
            this.groupControl1 = new DevExpress.XtraEditors.GroupControl();
            this.grdBookshelfInfo = new DevExpress.XtraGrid.GridControl();
            this.gvBookshelfInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.书柜编号 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.投放点 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.投放地址 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.grdRecordList = new DevExpress.XtraGrid.GridControl();
            this.gvRecordList = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repositoryItemTextEdit1 = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).BeginInit();
            this.splitContainerControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).BeginInit();
            this.groupControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdBookshelfInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBookshelfInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdRecordList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecordList)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).BeginInit();
            this.SuspendLayout();
            // 
            // splitContainerControl1
            // 
            this.splitContainerControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainerControl1.Location = new System.Drawing.Point(0, 0);
            this.splitContainerControl1.Name = "splitContainerControl1";
            this.splitContainerControl1.Panel1.Controls.Add(this.groupControl1);
            this.splitContainerControl1.Panel1.Text = "Panel1";
            this.splitContainerControl1.Panel2.Controls.Add(this.groupControl2);
            this.splitContainerControl1.Panel2.Text = "Panel2";
            this.splitContainerControl1.Size = new System.Drawing.Size(1082, 734);
            this.splitContainerControl1.SplitterPosition = 357;
            this.splitContainerControl1.TabIndex = 0;
            this.splitContainerControl1.Text = "splitContainerControl1";
            // 
            // groupControl1
            // 
            this.groupControl1.Controls.Add(this.grdBookshelfInfo);
            this.groupControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl1.Location = new System.Drawing.Point(0, 0);
            this.groupControl1.Name = "groupControl1";
            this.groupControl1.Size = new System.Drawing.Size(357, 734);
            this.groupControl1.TabIndex = 0;
            this.groupControl1.Text = "投放点信息";
            // 
            // grdBookshelfInfo
            // 
            this.grdBookshelfInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdBookshelfInfo.Location = new System.Drawing.Point(2, 21);
            this.grdBookshelfInfo.MainView = this.gvBookshelfInfo;
            this.grdBookshelfInfo.Name = "grdBookshelfInfo";
            this.grdBookshelfInfo.Size = new System.Drawing.Size(353, 711);
            this.grdBookshelfInfo.TabIndex = 0;
            this.grdBookshelfInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvBookshelfInfo});
            this.grdBookshelfInfo.Click += new System.EventHandler(this.grdBookshelfInfo_Click);
            this.grdBookshelfInfo.DoubleClick += new System.EventHandler(this.grdBookshelfInfo_DoubleClick);
            // 
            // gvBookshelfInfo
            // 
            this.gvBookshelfInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.书柜编号,
            this.投放点,
            this.投放地址});
            this.gvBookshelfInfo.GridControl = this.grdBookshelfInfo;
            this.gvBookshelfInfo.Name = "gvBookshelfInfo";
            this.gvBookshelfInfo.OptionsView.ShowFooter = true;
            this.gvBookshelfInfo.OptionsView.ShowGroupPanel = false;
            // 
            // 书柜编号
            // 
            this.书柜编号.Caption = "书柜编号";
            this.书柜编号.FieldName = "code";
            this.书柜编号.Name = "书柜编号";
            this.书柜编号.OptionsColumn.AllowEdit = false;
            this.书柜编号.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "code", "计数:{0}")});
            this.书柜编号.Visible = true;
            this.书柜编号.VisibleIndex = 0;
            // 
            // 投放点
            // 
            this.投放点.Caption = "投放点";
            this.投放点.FieldName = "area";
            this.投放点.Name = "投放点";
            this.投放点.OptionsColumn.AllowEdit = false;
            this.投放点.Visible = true;
            this.投放点.VisibleIndex = 1;
            // 
            // 投放地址
            // 
            this.投放地址.Caption = "投放地址";
            this.投放地址.FieldName = "address";
            this.投放地址.Name = "投放地址";
            this.投放地址.OptionsColumn.AllowEdit = false;
            this.投放地址.Visible = true;
            this.投放地址.VisibleIndex = 2;
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.grdRecordList);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(720, 734);
            this.groupControl2.TabIndex = 0;
            this.groupControl2.Text = "详细信息";
            // 
            // grdRecordList
            // 
            this.grdRecordList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdRecordList.Location = new System.Drawing.Point(2, 21);
            this.grdRecordList.MainView = this.gvRecordList;
            this.grdRecordList.Name = "grdRecordList";
            this.grdRecordList.Size = new System.Drawing.Size(716, 711);
            this.grdRecordList.TabIndex = 4;
            this.grdRecordList.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRecordList});
            // 
            // gvRecordList
            // 
            this.gvRecordList.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn5,
            this.gridColumn4});
            this.gvRecordList.GridControl = this.grdRecordList;
            this.gvRecordList.Name = "gvRecordList";
            this.gvRecordList.OptionsNavigation.AutoFocusNewRow = true;
            this.gvRecordList.OptionsSelection.MultiSelect = true;
            this.gvRecordList.OptionsView.ShowFooter = true;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "标签ID";
            this.gridColumn1.FieldName = "tag_id";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "tag_id", "记录数：{0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 0;
            this.gridColumn1.Width = 120;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "ISBN号";
            this.gridColumn2.FieldName = "isbn";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 1;
            this.gridColumn2.Width = 90;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "图书名称";
            this.gridColumn6.FieldName = "book_name";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 2;
            this.gridColumn6.Width = 135;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "标签录入时间";
            this.gridColumn4.ColumnEdit = this.repositoryItemTextEdit1;
            this.gridColumn4.DisplayFormat.FormatString = "yyyy/MM/dd HH:mm:ss";
            this.gridColumn4.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.gridColumn4.FieldName = "create_time";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 3;
            this.gridColumn4.Width = 77;
            // 
            // repositoryItemTextEdit1
            // 
            this.repositoryItemTextEdit1.AutoHeight = false;
            this.repositoryItemTextEdit1.Name = "repositoryItemTextEdit1";
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "标签操作人";
            this.gridColumn5.FieldName = "account";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 8;
            this.gridColumn5.Width = 90;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "借阅状态";
            this.gridColumn7.FieldName = "borrowed_status_text";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 4;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "投放地点";
            this.gridColumn8.FieldName = "area";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 7;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "书柜编号";
            this.gridColumn9.FieldName = "code";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 6;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "当前书格";
            this.gridColumn10.FieldName = "current_grid_code";
            this.gridColumn10.Name = "gridColumn10";
            this.gridColumn10.Visible = true;
            this.gridColumn10.VisibleIndex = 5;
            // 
            // FormBookTagsInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1082, 734);
            this.Controls.Add(this.splitContainerControl1);
            this.Name = "FormBookTagsInfo";
            this.Text = "FormBookTagsInfo";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.FormBookTagsInfo_Load);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainerControl1)).EndInit();
            this.splitContainerControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.groupControl1)).EndInit();
            this.groupControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdBookshelfInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvBookshelfInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdRecordList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRecordList)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemTextEdit1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.SplitContainerControl splitContainerControl1;
        private DevExpress.XtraEditors.GroupControl groupControl1;
        private DevExpress.XtraGrid.GridControl grdBookshelfInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvBookshelfInfo;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraGrid.GridControl grdRecordList;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRecordList;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repositoryItemTextEdit1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn 书柜编号;
        private DevExpress.XtraGrid.Columns.GridColumn 投放点;
        private DevExpress.XtraGrid.Columns.GridColumn 投放地址;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
    }
}