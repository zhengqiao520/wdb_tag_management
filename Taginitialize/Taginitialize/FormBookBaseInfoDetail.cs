﻿using Infrastructure;
using Infrastructure.Entity;
using NLog;
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
    public partial class FormBookBaseInfoDetail : Form
    {
        public class BookTopicalCheckItem : BooktopicalMappings {
            public bool isChecked { get; set; } = false;
        }
        private static Logger logger = LogManager.GetCurrentClassLogger();
        public delegate BookInfoExtend SetBookinfoEventHander(RowEnum rowEnum);
        public event SetBookinfoEventHander SetBookinfoEvent;
        public Form fathForm = null;
        public FormBookBaseInfoDetail(BookInfoExtend bookInfoExtend)
        {
            InitializeComponent();
            InitData();
            InitBookInfoExtend(bookInfoExtend);
          
        }

        public void InitBookInfoExtend(BookInfoExtend bookInfoExtend) {
            if (null != bookInfoExtend)
            {
                this.barCodeISBN.Text = bookInfoExtend.isbn_no;
                this.txtBookName.Text = bookInfoExtend.book_name;
                this.txtAuthor.Text = bookInfoExtend.author;
                this.txtBrief.Text = bookInfoExtend.brief;
                this.txtCreateTime.Text = bookInfoExtend.create_time.ToString();
                this.txtDescribe.Text = bookInfoExtend.describe;
                this.txtPress.Text = bookInfoExtend.press;
                this.txtPrice.Text = bookInfoExtend.price.ToString();
                this.txtPublicate_date.Text = bookInfoExtend.publication_date.ToString();
                this.txtImgurl.Text = bookInfoExtend.imgurl == null ? "" : bookInfoExtend.imgurl.Trim();
                txtISBN.Text = bookInfoExtend.isbn_no.Trim();
                txtMinAge.Text = bookInfoExtend.min_age;
                txtMaxAge.Text = bookInfoExtend.max_age;
                txtRating.EditValue = bookInfoExtend.recommendation;
                if (!string.IsNullOrEmpty(bookInfoExtend.topical_code) && !string.IsNullOrEmpty(bookInfoExtend.topical_name)) {
                    var topicalSource = chkTopicalName.DataSource as List<BookTopicalCheckItem>;
                    topicalSource.ForEach(item => item.isChecked = false);
                    var topical_codes = bookInfoExtend.topical_code.Split(new char[] { ',' });
                    var topical_names = bookInfoExtend.topical_name.Split(new char[] { ',' });
                    if (topical_names.Count() == topical_names.Count()) {
                        topical_codes.ToList().ForEach(code => {
                            for (int i = 0; i < topicalSource.Count; i++)
                            {
                                var item = topicalSource[i];
                                if (item.topical_code ==code)
                                {
                                    item.isChecked = true;
                                    continue;
                                }
                            }
                        });
                    }
                }
                chkTopicalName.Refresh();
                imageSlider1.Images.Clear();
                if (!string.IsNullOrEmpty(bookInfoExtend.imgurl))
                {
                    this.imageSlider1.Images.Add(ImageExtensions.GetImageFromNet($"http://wdb007.oss-cn-hangzhou.aliyuncs.com/" + bookInfoExtend.imgurl));
                }
            }
        }
        private void InitData() {
            var topicalMappingList = TagInfoDAL.GetBookTopicalMappingList().Select(item => new BookTopicalCheckItem { id = item.id, topical_code = item.topical_code, topical_name = item.topical_name }).ToList();
            chkTopicalName.DataSource = topicalMappingList.Where(item => item.topical_code != "0000").ToList();
            chkTopicalName.DisplayMember = "topical_name";
            chkTopicalName.ValueMember = "topical_code";
            chkTopicalName.CheckMember = "isChecked";
        }
        private void Btn_Click(object sender, EventArgs e)
        {
            FormBookBaseInfo parentForm = (FormBookBaseInfo)fathForm;
            DevExpress.XtraEditors.SimpleButton btn = sender as DevExpress.XtraEditors.SimpleButton;
            switch (btn.Name) {
                case "btnPrevious":
                    InitBookInfoExtend(parentForm.SetBookinfoEvent(RowEnum.PreviousRow));
                    break;
                case "btnNext":
                    InitBookInfoExtend(parentForm.SetBookinfoEvent(RowEnum.NextRow));
                    break;
                default:
                    break;
            }
        }

        private void btnSaveBookName_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtISBN.Text.Trim()))
                {
                    MessageBox.Show("图书名称不能为空！", "系统提示");
                    return;
                }
                if (int.Parse(txtMinAge.Text) > int.Parse(txtMaxAge.Text)) {
                    MessageBox.Show("结束年龄不能大于开始年龄！", "系统提示");
                    return;
                }
                var topicalList = chkTopicalName.DataSource as List<BookTopicalCheckItem>;
                var bookInfoExtend = new BookInfoExtend
                {
                    isbn_no = txtISBN.Text.Trim(),
                    book_name = txtBookName.Text.Trim(),
                    author = txtAuthor.Text.Trim(),
                    press = txtPress.Text.Trim(),
                    publication_date = string.IsNullOrEmpty(txtPublicate_date.Text.Trim()) ? DateTime.Now : DateTime.Parse(txtPublicate_date.Text),
                    price = decimal.Parse(txtPrice.Text),
                    min_age = txtMinAge.Text,
                    max_age = txtMaxAge.Text,
                    brief = txtBrief.Text,
                    describe = txtDescribe.Text,
                    recommendation = txtRating.Text,
                    imgurl=txtImgurl.Text.Trim()
                };
                if (null != topicalList)
                {
                    var selectedTopical = topicalList.Where(item => item.isChecked == true);
                    if (selectedTopical != null)
                    {
                        bookInfoExtend.topical_code = string.Join(",", selectedTopical.Select(item => item.topical_code).ToArray());
                        bookInfoExtend.topical_name = string.Join(",", selectedTopical.Select(item => item.topical_name).ToArray());
                    }
                    var res = TagInfoDAL.UpdateSingleBookInfo(bookInfoExtend);
                    if (res)
                    {
                        FormBookBaseInfo parentForm = (FormBookBaseInfo)fathForm;
                        parentForm.UpdateGridRow(bookInfoExtend);
                        logger.Log(new LogEventInfo { Level = LogLevel.Info, LoggerName = "修改图书信息", Message = $"用户{UserInfo.Instance.user_name}修改ISBN:{txtISBN.Text}对应图书名称为{bookInfoExtend.book_name}" });
                    }
                    else {
                        MessageBox.Show("保存修改失败！", "系统提示");
                        return;
                    }
                }
            }
            catch(Exception ee) {
                MessageBox.Show("保存修改失败！", "系统提示");
                logger.Log(LogLevel.Error, $"修改isbn：{txtISBN.Text}对应图书信息失败" + ee.Message);
            }
        }
    }
}