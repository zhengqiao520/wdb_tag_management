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
using Infrastructure.Entity;
using Infrastructure;
namespace Phychips.PR9200
{
    public partial class FormZTUser : DevExpress.XtraEditors.XtraForm
    {
        public Form fatherForm;
        public FormZTUser(Form form)
        {
            this.fatherForm = form;
            InitializeComponent();
        }

        private void FormZTUser_Load(object sender, EventArgs e)
        {
            var userInfoList= TagInfoDAL.GetInfoListPagination<ZTUserInfo>();
            grdZTUser.DataSource = TagInfoDAL.GetInfoListPagination<ZTUserInfo>();
            BindingSource bindingSource = new BindingSource();
            bindingSource.DataSource = userInfoList;
            bindingNavigator1.BindingSource = bindingSource;
        }
    }
}