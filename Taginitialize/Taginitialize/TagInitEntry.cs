using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using RFIDStation;
using Infrastructure.Entity;
using DevExpress.XtraEditors;
using System.Diagnostics;
using Infrastructure;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace Phychips.PR9200
{
    public partial class TagInitEntry : XtraForm
    {
        private UserInfo user { get; set; }
        FromTaginitialize frmUtralHightReq = null;
        public TagInitEntry(UserInfo userinfo)
        {
            InitializeComponent();
            user = userinfo;
            Timer timer = new Timer();
            timer.Enabled = true;
            timer.Interval = 1000;
            timer.Start();
            timer.Tick += Timer_Tick;
            lblName.Text = $"欢迎,{UserInfo.Instance.user_name}";
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            lblTime.Text = $"当前时间:{DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss")}";
        }

        private void TagInitEntry_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
            Process.GetCurrentProcess().Kill();
        }

        private void windowsUIButtonPanel1_ButtonClick(object sender, DevExpress.XtraBars.Docking2010.ButtonEventArgs e)
        {
            DevExpress.XtraBars.Docking2010.WindowsUIButton button=e.Button as DevExpress.XtraBars.Docking2010.WindowsUIButton;
            switch (button.Tag.ToString()) {
                case "exit":
                    TagInitEntry_FormClosed(null, null);
                    break;
                case "change":
                    Process.Start(System.Reflection.Assembly.GetExecutingAssembly().Location); 
                    Close();
                    break;
            }
        }
        private void tile_ItemClick(object sender, TileItemEventArgs e) {
            try
            {
                TileItem tile = sender as TileItem;
                if (!string.IsNullOrEmpty(user.remarks))
                {
                    var orderObj = JsonConvert.DeserializeObject(user.remarks);
                    var res = ((JObject)orderObj).Properties().ToList().Any(pro => pro.Name == tile.Tag.ToString() && (bool)pro.Value);
                    if (!res)
                    {
                        MessageBox.Show("您无访问该菜单的权限!", "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                }

                switch (tile.Tag.ToString())
                {
                    case "FormBookImport":
                        FormBookImport frmBookImport = new PR9200.FormBookImport();
                        frmBookImport.fatherForm = this;
                        frmBookImport.Show();
                        break;
                    case "FromTaginitialize":

                        if (frmUtralHightReq == null || frmUtralHightReq.IsDisposed)
                        {
                            frmUtralHightReq = new PR9200.FromTaginitialize();
                            frmUtralHightReq.fatherForm = this;
                            this.Visible = false;
                            frmUtralHightReq.Show();
                            return;
                        }
                        frmUtralHightReq.Show();
                        break;
                    case "RFIDStation":
                        RFIDStation.RFIDStation frmHightFreq = new RFIDStation.RFIDStation();
                        frmHightFreq.fatherForm = this;
                        frmHightFreq.Show();
                        this.Visible = false;
                        break;
                    case "FormBookTagsInfo":
                        FormBookTagsInfo frmBookTagsInfo = new PR9200.FormBookTagsInfo();
                        frmBookTagsInfo.Show();
                        break;
                    case "FormBookBaseInfo":
                        FormBookBaseInfo frmBaseBookInfo = new PR9200.FormBookBaseInfo();
                        frmBaseBookInfo.fatherForm = this;
                        frmBaseBookInfo.Show();
                        break;
                    case "FormZTUser":
                        new FormZTUser(this).Show();
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ee)
            {
                MessageBox.Show(ee.Message, "系统提示");
            }
        }
        private void TagInitEntry_Load(object sender, EventArgs e)
        {
            string[] mixIp = SSHInfo.Instance.publish_ip.Split('.');
            mixIp[0] = mixIp[1] = "*";
            var ip = ConnectInit.DbType == Infrastructure.DbType.MYSQL_UAT ? SSHInfo.Instance.local_ip : string.Join(".", mixIp);
            lblDBEnvironment.Text = $"当前登录为：{ip+"【"+ UserReflect<object>.GetEnumDescription(ConnectInit.DbType)+"】"}";
        }

    }
}
