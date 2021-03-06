﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Infrastructure.Entity;
using Infrastructure;
using Newtonsoft.Json;
using Renci.SshNet;

namespace Phychips.PR9200
{
    public partial class FormLogin: BaseForm
    {
        public delegate void SetUserInfoHander();
        public SetUserInfoHander SetUserInfoEvent;
        private ClientSsh clientSSH = null;
        public FormLogin()
        {
            InitializeComponent();
            lblClose.Click += (s, e) => Close();
            lblClose.Focus();
            CreateSSH();
        }

        private void lblLogin_Click(object sender, EventArgs e)
        {   
            string user_account = txtUserName.Text.Trim();
            string user_password = txtPassword.Text.Trim();
            if (string.IsNullOrEmpty(user_account)) {
                MessageBox.Show("请输入用户名!", "系统提示");
                txtUserName.Focus();
                return;
            }
            var book_init_user=TagInfoDAL.GetBookInitUser(user_account);
            if (null != book_init_user)
            {
                if (book_init_user.user_password != Utility.DES3Encrypt(user_password))
                {
                    MessageBox.Show("用户密码错误!", "系统提示",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    txtPassword.Focus();
                    return;
                }
                else
                {
                    UserInfo.Instance.id = book_init_user.id;
                    UserInfo.Instance.user_account = book_init_user.user_account;
                    UserInfo.Instance.user_name = book_init_user.user_name;
                    UserInfo.Instance.user_password = book_init_user.user_password;
                    UserInfo.Instance.remarks = book_init_user.remarks;
                    new TagInitEntry(UserInfo.Instance).Show();
                    WinAPI.AnimateWindow(Handle, 500, WinAPI.AW_CENTER | WinAPI.AW_HIDE);
                }
            }
            else {
                MessageBox.Show("该用户不存在！", "系统提示");
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void FormLogin_KeyDown(object sender, KeyEventArgs e)
        {
            lblLogin_Click(null, null);
        }
        private void CreateSSH(Infrastructure.DbType dbType = Infrastructure.DbType.MySQL)
        {
            try
            {
                SSHInfo.Instance = Utility.SetSSH(dbType);
                ClientSsh.Instance.CreateInstance();
            }
            catch (Exception ee)
            {
                MessageBox.Show("远程数据库连接失败!失败原因:" + ee.Message, "系统提示");
            }
        }
        private void radio_EditValueChanged(object sender, EventArgs e)
        {
            Infrastructure.DbType dbType = (Infrastructure.DbType)radioDatabase.EditValue;
            switch (dbType) {
                case Infrastructure.DbType.MYSQ_DEV:
                case Infrastructure.DbType.MySQL:
                    ClientSsh.Instance.Disconnect();
                    CreateSSH(dbType);
                    break;
                default:
                    if (null != clientSSH&&dbType==Infrastructure.DbType.MYSQL_UAT) {
                        ClientSsh.Instance.Disconnect();
                    }
                    break;
            }
            ConnectInit.DbType = dbType;
        }
        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            radioDatabase.Visible =txtUserName.Text.Equals("admin")|| txtUserName.Text.Equals("tianyingwen");
        }
    }

}