namespace Phychips.PR9200
{
    partial class FormBookBaseInfo
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
            this.components = new System.ComponentModel.Container();
            this.btnDetail = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.导出excelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.刷新列表ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.gvSystem = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn25 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn22 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn23 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn21 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn9 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn10 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn11 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.groupControl2 = new DevExpress.XtraEditors.GroupControl();
            this.panel2 = new System.Windows.Forms.Panel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.toggleSwitch = new DevExpress.XtraEditors.ToggleSwitch();
            this.btnView = new DevExpress.XtraEditors.SimpleButton();
            this.btnExport = new DevExpress.XtraEditors.SimpleButton();
            this.btnReload = new DevExpress.XtraEditors.SimpleButton();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.gridColumn12 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.btnDetail)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSystem)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).BeginInit();
            this.groupControl2.SuspendLayout();
            this.panel2.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnDetail
            // 
            this.btnDetail.ContextMenuStrip = this.contextMenuStrip1;
            this.btnDetail.Dock = System.Windows.Forms.DockStyle.Fill;
            this.btnDetail.Location = new System.Drawing.Point(0, 0);
            this.btnDetail.MainView = this.gvSystem;
            this.btnDetail.Name = "btnDetail";
            this.btnDetail.Size = new System.Drawing.Size(1290, 794);
            this.btnDetail.TabIndex = 2;
            this.btnDetail.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSystem});
            this.btnDetail.DoubleClick += new System.EventHandler(this.grdSystem_DoubleClick);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.导出excelToolStripMenuItem,
            this.刷新列表ToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 48);
            this.contextMenuStrip1.Text = "导出excel";
            // 
            // 导出excelToolStripMenuItem
            // 
            this.导出excelToolStripMenuItem.Name = "导出excelToolStripMenuItem";
            this.导出excelToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.导出excelToolStripMenuItem.Text = "导出Excel";
            this.导出excelToolStripMenuItem.Click += new System.EventHandler(this.导出excelToolStripMenuItem_Click);
            // 
            // 刷新列表ToolStripMenuItem
            // 
            this.刷新列表ToolStripMenuItem.Name = "刷新列表ToolStripMenuItem";
            this.刷新列表ToolStripMenuItem.Size = new System.Drawing.Size(129, 22);
            this.刷新列表ToolStripMenuItem.Text = "刷新列表";
            // 
            // gvSystem
            // 
            this.gvSystem.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn25,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn5,
            this.gridColumn6,
            this.gridColumn7,
            this.gridColumn8,
            this.gridColumn22,
            this.gridColumn23,
            this.gridColumn21,
            this.gridColumn9,
            this.gridColumn10,
            this.gridColumn11,
            this.gridColumn12});
            this.gvSystem.GridControl = this.btnDetail;
            this.gvSystem.Name = "gvSystem";
            this.gvSystem.OptionsView.ShowAutoFilterRow = true;
            this.gvSystem.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gvSystem.OptionsView.ShowFooter = true;
            this.gvSystem.OptionsView.WaitAnimationOptions = DevExpress.XtraEditors.WaitAnimationOptions.Indicator;
            this.gvSystem.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gvSystem_RowStyle);
            // 
            // gridColumn25
            // 
            this.gridColumn25.Caption = "是否丛书";
            this.gridColumn25.FieldName = "isMuliti";
            this.gridColumn25.Name = "gridColumn25";
            this.gridColumn25.OptionsColumn.Printable = DevExpress.Utils.DefaultBoolean.False;
            this.gridColumn25.Visible = true;
            this.gridColumn25.VisibleIndex = 0;
            // 
            // gridColumn1
            // 
            this.gridColumn1.Caption = "ISBN";
            this.gridColumn1.FieldName = "isbn_no";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Count, "isbn_no", "图书总数：{0}")});
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 1;
            this.gridColumn1.Width = 78;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "图书名称";
            this.gridColumn2.FieldName = "book_name";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 2;
            this.gridColumn2.Width = 120;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "作者";
            this.gridColumn3.FieldName = "author";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 3;
            this.gridColumn3.Width = 120;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "出版社";
            this.gridColumn4.FieldName = "press";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 4;
            this.gridColumn4.Width = 120;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "出版日期";
            this.gridColumn5.FieldName = "publication_date";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 5;
            this.gridColumn5.Width = 70;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "价格";
            this.gridColumn6.FieldName = "price";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 62;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "简介";
            this.gridColumn7.FieldName = "brief";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 7;
            this.gridColumn7.Width = 131;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "描述";
            this.gridColumn8.FieldName = "describe";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.OptionsColumn.AllowEdit = false;
            this.gridColumn8.Visible = true;
            this.gridColumn8.VisibleIndex = 8;
            this.gridColumn8.Width = 131;
            // 
            // gridColumn22
            // 
            this.gridColumn22.Caption = "适读年龄";
            this.gridColumn22.FieldName = "min_max_age";
            this.gridColumn22.Name = "gridColumn22";
            this.gridColumn22.Visible = true;
            this.gridColumn22.VisibleIndex = 10;
            this.gridColumn22.Width = 55;
            // 
            // gridColumn23
            // 
            this.gridColumn23.Caption = "类别标签";
            this.gridColumn23.FieldName = "topical_name";
            this.gridColumn23.Name = "gridColumn23";
            this.gridColumn23.Visible = true;
            this.gridColumn23.VisibleIndex = 11;
            this.gridColumn23.Width = 81;
            // 
            // gridColumn21
            // 
            this.gridColumn21.Caption = "封面图片";
            this.gridColumn21.FieldName = "imgurl";
            this.gridColumn21.Name = "gridColumn21";
            this.gridColumn21.OptionsColumn.AllowEdit = false;
            this.gridColumn21.Visible = true;
            this.gridColumn21.VisibleIndex = 9;
            this.gridColumn21.Width = 91;
            // 
            // gridColumn9
            // 
            this.gridColumn9.Caption = "创建日期";
            this.gridColumn9.FieldName = "create_time";
            this.gridColumn9.Name = "gridColumn9";
            this.gridColumn9.OptionsColumn.AllowEdit = false;
            this.gridColumn9.Visible = true;
            this.gridColumn9.VisibleIndex = 12;
            this.gridColumn9.Width = 90;
            // 
            // gridColumn10
            // 
            this.gridColumn10.Caption = "gridColumn10";
            this.gridColumn10.FieldName = "min_age";
            this.gridColumn10.Name = "gridColumn10";
            // 
            // gridColumn11
            // 
            this.gridColumn11.Caption = "gridColumn11";
            this.gridColumn11.FieldName = "topical_code";
            this.gridColumn11.Name = "gridColumn11";
            // 
            // groupControl2
            // 
            this.groupControl2.Controls.Add(this.panel2);
            this.groupControl2.Controls.Add(this.panel1);
            this.groupControl2.Controls.Add(this.panelControl1);
            this.groupControl2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupControl2.Location = new System.Drawing.Point(0, 0);
            this.groupControl2.Name = "groupControl2";
            this.groupControl2.Size = new System.Drawing.Size(1294, 867);
            this.groupControl2.TabIndex = 4;
            this.groupControl2.Text = "图书信息列表";
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnDetail);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(2, 71);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(1290, 794);
            this.panel2.TabIndex = 7;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.toggleSwitch);
            this.panel1.Controls.Add(this.btnView);
            this.panel1.Controls.Add(this.btnExport);
            this.panel1.Controls.Add(this.btnReload);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(2, 21);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1290, 50);
            this.panel1.TabIndex = 6;
            // 
            // toggleSwitch
            // 
            this.toggleSwitch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.toggleSwitch.Location = new System.Drawing.Point(789, 12);
            this.toggleSwitch.Name = "toggleSwitch";
            this.toggleSwitch.Properties.OffText = "点击查【不完整】信息";
            this.toggleSwitch.Properties.OnText = "点击查看【所有】信息";
            this.toggleSwitch.Size = new System.Drawing.Size(207, 25);
            this.toggleSwitch.TabIndex = 6;
            this.toggleSwitch.Toggled += new System.EventHandler(this.toggleSwitch_Toggled);
            // 
            // btnView
            // 
            this.btnView.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnView.Location = new System.Drawing.Point(997, 8);
            this.btnView.Name = "btnView";
            this.btnView.Size = new System.Drawing.Size(89, 32);
            this.btnView.TabIndex = 5;
            this.btnView.Text = "查看详情";
            this.btnView.Click += new System.EventHandler(this.btnView_Click);
            // 
            // btnExport
            // 
            this.btnExport.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExport.Location = new System.Drawing.Point(1190, 8);
            this.btnExport.Name = "btnExport";
            this.btnExport.Size = new System.Drawing.Size(91, 32);
            this.btnExport.TabIndex = 3;
            this.btnExport.Text = "导出Excel";
            this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
            // 
            // btnReload
            // 
            this.btnReload.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReload.Location = new System.Drawing.Point(1092, 8);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(91, 32);
            this.btnReload.TabIndex = 4;
            this.btnReload.Text = "刷新列表";
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // panelControl1
            // 
            this.panelControl1.Location = new System.Drawing.Point(-885, 34);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(228, 71);
            this.panelControl1.TabIndex = 5;
            // 
            // gridColumn12
            // 
            this.gridColumn12.Caption = "gridColumn12";
            this.gridColumn12.FieldName = "max_age";
            this.gridColumn12.Name = "gridColumn12";
            // 
            // FormBookBaseInfo
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1294, 867);
            this.Controls.Add(this.groupControl2);
            this.Name = "FormBookBaseInfo";
            this.Text = "基础信息明细";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            ((System.ComponentModel.ISupportInitialize)(this.btnDetail)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSystem)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.groupControl2)).EndInit();
            this.groupControl2.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.toggleSwitch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraGrid.GridControl btnDetail;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSystem;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn25;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn9;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn21;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn22;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn23;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 导出excelToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 刷新列表ToolStripMenuItem;
        private DevExpress.XtraEditors.GroupControl groupControl2;
        private DevExpress.XtraEditors.SimpleButton btnReload;
        private DevExpress.XtraEditors.SimpleButton btnExport;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel1;
        private DevExpress.XtraEditors.SimpleButton btnView;
        private DevExpress.XtraEditors.ToggleSwitch toggleSwitch;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn10;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn11;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn12;
    }
}