using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Infrastructure.Entity;
using System.Data.SQLite;
using System.Data;
using MySql.Data.MySqlClient;
using Renci.SshNet;
using NLog;
using System.Reflection;

namespace Infrastructure
{
    public class TagInfoDAL
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static Utility MySqlInstance = Utility.MySqlInstance;
        public static readonly string SSHString = System.Configuration.ConfigurationManager.AppSettings["ssh"].ToString();
        #region 同步mysql操作

        /// <summary>
        /// 获取服务器时间
        /// </summary>
        /// <returns></returns>
        public static DateTime GetMysqlSeverDateTime()
        {
            var datetime = MySqlInstance.ExecuteScalar(CommandType.Text, "SELECT NOW()", null);
            if (datetime != null)
            {
                return Convert.ToDateTime(datetime);
            }
            return DateTime.Now;
        }
        /// <summary>
        /// 获取图书实体信息
        /// </summary>
        /// <param name="rfid"></param>
        /// <returns></returns>
        public static BookEntity GetBookEntityByRFIDCode(string rfid)
        {
            string sql = "select * from book_entity where rfid_code=@rfid";
            var res = MySqlInstance.ExecuteList<BookEntity>(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
                new MySqlParameter("@rfid",rfid)
                });
            if (null != res && res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
        /// <summary>
        /// 保存图书信息
        /// </summary>
        /// <param name="bookEntity"></param>
        /// <returns></returns>
        public static bool InsertBookEntity(BookEntity bookEntity)
        {
            bool result = false;
            string sql = "insert into book_entity(isbn_no,rfid_code,status,create_time)values(@isbn_no,@rfid_code,@status,@create_time)";
            using (MySqlConnection con = new MySqlConnection(Utility.MysqlDES3DecryptConnctionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    var res = MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, sql, new System.Data.Common.DbParameter[] {
                    new MySqlParameter("@isbn_no",bookEntity.isbnNo),
                    new MySqlParameter("@rfid_code",bookEntity.rfidCode),
                    new MySqlParameter("@status",bookEntity.status),
                    new MySqlParameter("@create_time",bookEntity.createTime)
                });
                    result = res > 0;
                }
            }
            return result;
        }

        /// <summary>
        /// 根据图书名称和isbn获取图书信息
        /// </summary>
        /// <param name="isbn_no"></param>
        /// <param name="book_name"></param>
        /// <param name="isVagueQuery"></param>
        /// <returns></returns>
        public static List<BookInfo> SelectBookInfoListBy(string isbn_no, string book_name = "", bool isVagueQuery = false)
        {
            string where = isVagueQuery ? $"  like @isbn_no" : "=@isbn_no";
            string sql = $"select * from book_info where isbn_no{where}";
            List<MySqlParameter> param = new List<MySqlParameter>() {
                new MySqlParameter("@isbn_no", isVagueQuery?"%" + isbn_no + "%":isbn_no)
            };
            if (!string.IsNullOrEmpty(book_name))
            {
                sql += $" and book_name=@book_name";
                param.Add(new MySqlParameter("@book_name", book_name));
            }
            var listBookInfo = MySqlInstance.ExecuteList<BookInfo>(CommandType.Text, sql, param.ToArray());

            if (null != listBookInfo && listBookInfo.Count > 0)
            {
                return listBookInfo;
            }
            return null;
        }
        /// <summary>
        /// 获取指定isbn图书信息
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public static List<BookInfo> SelectBookInfoList(string isbn = "")
        {
            try
            {
                string where = !string.IsNullOrEmpty(isbn) ? $" isbn_no='{isbn}'" : " 1=1";
                var res = MySqlInstance.ExecuteList<BookInfo>(CommandType.Text, $"select * from book_info where {where}", null);
                return res;
            }
            catch
            {
                return null;
            }
        }

        public static List<BookInfoExtend> SelectBookInfoExtendList(string isbn = "")
        {
            try
            {
                string where = !string.IsNullOrEmpty(isbn) ? $" a.isbn_no='{isbn}'" : " 1=1";
                var res = MySqlInstance.ExecuteList<BookInfoExtend>(CommandType.Text, $"select a.*,b.min_age,b.max_age,concat(b.min_age,'-',b.max_age) min_max_age, c.topical_name,c.topical_code,CONCAT(e.recommendation,'') recommendation  from book_info a left join  book_readable b on a.isbn_no = b.isbn left join(select GROUP_CONCAT(d.topical_name separator ',')topical_name, GROUP_CONCAT(d.topical_code separator ',') topical_code, isbn from book_topical c join book_topical_mappings d on c.topical_code = d.topical_code group by c.isbn) c on c.isbn = a.isbn_no  LEFT JOIN book_recommendation e ON a.isbn_no=e.isbn_no where {where}", null);
                return res;
            }
            catch(Exception ee)
            {
                return null;
            }
        }

        public static BookInfoExt SelectBookinfoExt(string tag_id) {
            try
            {
                string sql = "select b.*,a.tag_id,a.isbn_type,a.create_time tag_create_time from book_init_mapping a join book_info b on a.isbn=b.isbn_no where a.tag_id=@tag_id";
                var res=MySqlInstance.ExecuteList<BookInfoExt>(CommandType.Text, sql, new MySqlParameter[] {
                    new MySqlParameter("@tag_id",tag_id)
                });
                if (null != res&&res.Count>0) {
                    return res.FirstOrDefault();
                }
                return null;
            }
            catch {
                return null;
            }
        }
        /// <summary>
        /// 更单条图书信息
        /// </summary>
        /// <param name="bookInfoExtend"></param>
        /// <returns></returns>
        public static bool UpdateSingleBookInfo(BookInfoExtend bookInfoExtend)
        {
            Func<BookInfoExtend, MySqlTransaction,bool> funcInsertBookTopic = (bookinfoExtend,trans) => {
                //保存图书主题标签信息
                if (!string.IsNullOrEmpty(bookinfoExtend.topical_name))
                {
                    string[] topicalNames = bookinfoExtend.topical_name.Split(',');
                    string[] topicalCodes = bookInfoExtend.topical_code.Split(',');
                    if (topicalNames.Count() > 0)
                    {

                        for (int i = 0; i < topicalNames.Count(); i++)
                        {
                            var bookTopical = GetBookTopical(bookinfoExtend.isbn_no, topicalNames[i]);
                            if (bookTopical == null)
                            {
                                InsertBookTopical(new BookTopical { isbn = bookInfoExtend.isbn_no, topical_code = topicalCodes[i], create_time = DateTime.Now },trans);
                            }
                        }
                    }
                }
                return false;
            };
            var result = false;
            using (MySqlConnection con = new MySqlConnection(Utility.MysqlDES3DecryptConnctionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string sql = "update book_info set book_name=@book_name,author=@author,press=@press,publication_date=@publication_date,price=@price,brief=@brief,`describe`=@describe,modify_time=@modify_time,imgurl=@imgurl where isbn_no=@isbn";
                        var res = MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, sql, new MySqlParameter[] {
                             new MySqlParameter("@isbn",bookInfoExtend.isbn_no),
                             new MySqlParameter("@book_name",bookInfoExtend.book_name),
                             new MySqlParameter("@author",bookInfoExtend.author),
                             new MySqlParameter("@press",bookInfoExtend.press),
                             new MySqlParameter("@publication_date",bookInfoExtend.publication_date),
                             new MySqlParameter("@price",bookInfoExtend.price),
                             new MySqlParameter("@brief",bookInfoExtend.brief),
                             new MySqlParameter("@describe",bookInfoExtend.describe),
                             new MySqlParameter("@modify_time",GetMysqlSeverDateTime()),
                             new MySqlParameter("@imgurl",bookInfoExtend.imgurl)}) > 0;
                        if (res)
                        {
                            if (GetBookReadable(bookInfoExtend.isbn_no) != null)
                            {
                                string update_read_sql = $"update book_readable set min_age='{bookInfoExtend.min_age}',max_age='{bookInfoExtend.max_age}',create_time='{DateTime.Now}' where isbn='{bookInfoExtend.isbn_no}'";
                                MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, update_read_sql, null);
                            }
                            else {
                                string insert_read_sql = $"insert into book_readable(isbn,min_age,max_age,create_time)values(@isbn,@min_age,@max_age,@create_time)";
                                MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, insert_read_sql, new MySqlParameter[] {
                                    new MySqlParameter("@isbn",bookInfoExtend.isbn_no),
                                    new MySqlParameter("@min_age",bookInfoExtend.min_age),
                                    new MySqlParameter("@max_age",bookInfoExtend.max_age)
                                });
                            }
                            string delete_topical_sql = $" delete from book_topical where isbn='{bookInfoExtend.isbn_no}'";
                            MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, delete_topical_sql, null);
                            funcInsertBookTopic(bookInfoExtend, trans);
                            result = true;
                            trans.Commit();
                        }
                        else {
                            trans.Rollback();
                        }
                    }
                    catch(Exception ee) {
                        logger.Log(LogLevel.Error, "更新单本图书信息异常:" + ee.Message);
                        result = false;
                        trans.Rollback();
                    }
                }
            }
            return result;
        }



        /// <summary>
        /// 保存图书信息
        /// </summary>
        /// <param name="bookinfo"></param>
        /// <param name="msg"></param>
        /// <param name="enableUpdate"></param>
        /// <returns></returns>
        public static bool InsertBookInfo(BookInfo bookinfo, out string msg, bool enableUpdate = false)
        {
            msg = string.Empty;
            List<BookInfo> listBookInfo = SelectBookInfoList(bookinfo.isbn_no.Trim());
            if (null != listBookInfo && listBookInfo.Count > 0)
            {  
                //是否更新图书信息
                if (enableUpdate)
                {
                    try
                    {
                        string update_sql = $"update book_info set brief='{bookinfo.brief}',`describe`='{bookinfo.describe}' where isbn_no='{bookinfo.isbn_no.Trim()}'";
                        //string update_sql = $"delete from  book_info where   isbn_no='{bookinfo.isbn_no.Trim()}'";
                        MySqlInstance.ExecuteNonQuery(CommandType.Text, update_sql, null);
                    }
                    catch (Exception ee){}
                }
                msg = "exists";
                return false;
            }
            using (MySqlConnection con = new MySqlConnection(Utility.MysqlDES3DecryptConnctionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    string sql = "insert into book_info(isbn_no, book_name, author, press, publication_date, category, price, readable, imgurl, brief, `describe`, create_time, modify_time)values(@isbn_no, @book_name, @author, @press, @publication_date, @category, @price, @readable, @imgurl, @brief, @describe, @create_time, @modify_time)";
                    MySqlParameter[] param = new MySqlParameter[] {
                    new MySqlParameter("@isbn_no",bookinfo.isbn_no),
                    new MySqlParameter("@book_name",bookinfo.book_name),
                    new MySqlParameter("@author",bookinfo.author),
                    new MySqlParameter("@press",bookinfo.press),
                    new MySqlParameter("@publication_date",bookinfo.publication_date),
                    new MySqlParameter("@category",bookinfo.category),
                    new MySqlParameter("@price",bookinfo.price),
                    new MySqlParameter("@readable",bookinfo.readable),
                    new MySqlParameter("@imgurl",bookinfo.imgurl),
                    new MySqlParameter("@brief",bookinfo.brief),
                    new MySqlParameter("@describe",bookinfo.describe),
                    new MySqlParameter("@create_time",bookinfo.create_time),
                    new MySqlParameter("@modify_time",bookinfo.modify_time)
                };
                    int result = MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                    trans.Commit();
                    return result > 0;
                }
            }
        }

        private bool UpdateBookInfos(BookInfoExtend bookInfoExtend) {
            List<BooktopicalMappings> listBooktopicalMappings = GetBookTopicalMappingList();
            Func<BookInfoExtend, bool> funcInsertBookTopic = bookinfo =>
            {
                //保存图书主题标签信息
                if (!string.IsNullOrEmpty(bookinfo.topical_name))
                {
                    string[] topicalNames = bookinfo.topical_name.Replace("，", ",").Replace(" ", "").Replace("、", ",").Replace(".", ",").Trim().Split(',');
                    if (topicalNames.Count() > 0)
                    {

                        topicalNames.ToList().ForEach(topicalName =>
                        {
                            var bookTopical = GetBookTopical(bookinfo.isbn_no, topicalName);
                            if (bookTopical == null)
                            {
                                var topical_code = listBooktopicalMappings.Where(item => item.topical_name == topicalName.Trim()).Select(item => item.topical_code).DefaultIfEmpty(null);
                                if (topical_code != null)
                                {
                                    var toppic = new BookTopical
                                    {
                                        create_time = DateTime.Now,
                                        isbn = bookinfo.isbn_no,
                                        topical_code = topical_code.FirstOrDefault()
                                    };
                                    InsertBookTopical(toppic);
                                }
                            }
                        });
                    }
                }
                return false;
            };
            try
            {
                string sql = $"update book_info set book_name='{bookInfoExtend.book_name}',brief='{bookInfoExtend.brief}',`describe`='{bookInfoExtend.describe}' where isbn_no='{bookInfoExtend.isbn_no.Trim()}',;";
                sql += $"update book_readable set min_age='{bookInfoExtend.min_age}',max_age='{bookInfoExtend.max_age}',create_time='{DateTime.Now}' where isbn='{bookInfoExtend.isbn_no}'";
                int updateResult = MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, null);
                if (updateResult > 0)
                {
                    //更新图书主题时，先删除再插入记录
                    sql = $" delete from book_topical where isbn='{bookInfoExtend.isbn_no}'";
                    MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, null);
                    funcInsertBookTopic(bookInfoExtend);
                }
            }
            catch (Exception ee)
            {
            }
            return true;
        }

        /// <summary>
        /// 批量导入保存图书信息
        /// </summary>
        /// <param name="bookInfos"></param>
        /// <param name="enableUpdate"></param>
        public static void BatchInsertBookInfo(List<BookInfoExt> bookInfos, out List<BookInfoExt> failBookList, out List<BookInfoExt> existsBookList , out List<BookInfoExt>  successBookListbool,bool enableUpdate = false)
        {
            failBookList = new List<BookInfoExt>();
            existsBookList = new List<BookInfoExt>();
            successBookListbool = new List<BookInfoExt>();
            using (MySqlConnection con = new MySqlConnection(Utility.MysqlDES3DecryptConnctionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                   
                    List<BooktopicalMappings> listBooktopicalMappings = GetBookTopicalMappingList();
                    Func<BookInfoExt,bool> funcInsertBookTopic = bookinfo =>
                    {
                        //保存图书主题标签信息
                        if (!string.IsNullOrEmpty(bookinfo.topical_name))
                        {
                            string[] topicalNames = bookinfo.topical_name.Replace("，", ",").Replace(" ", "").Replace("、", ",").Replace(".", ",") .Trim().Split(',');
                            if (topicalNames.Count() > 0)
                            {

                                topicalNames.ToList().ForEach(topicalName =>
                                {
                                    var bookTopical = GetBookTopical(bookinfo.isbn_no, topicalName);
                                    if (bookTopical == null)
                                    {
                                        var topical_code = listBooktopicalMappings.Where(item => item.topical_name == topicalName.Trim()).Select(item => item.topical_code).DefaultIfEmpty(null);
                                        if (topical_code != null)
                                        {
                                            var toppic = new BookTopical
                                            {
                                                create_time = DateTime.Now,
                                                isbn = bookinfo.isbn_no,
                                                topical_code = topical_code.FirstOrDefault()
                                            };
                                            InsertBookTopical(toppic);
                                        }
                                    }
                                });
                            }
                        }
                        return false;
                    };
                    for (int i = 0; i < bookInfos.Count; i++)
                    {
                        var bookinfo = bookInfos[i];
                        var res = MySqlInstance.ExecuteList<BookInfo>(CommandType.Text, $"select * from book_info where isbn_no='{bookinfo.isbn_no}'", null);
                        if (res != null && res.Count==1)
                        {
                            //是否更新图书信息
                            if (enableUpdate)
                            {
                                try
                                {
                                    string sql = $"update book_info set book_name='{bookinfo.book_name}',brief='{bookinfo.brief}',`describe`='{bookinfo.describe}' where isbn_no='{bookinfo.isbn_no.Trim()}';";
                                    sql += $"update book_readable set min_age='{bookinfo.min_age}',max_age='{bookinfo.max_age}',create_time='{DateTime.Now}' where isbn='{bookinfo.isbn_no}'";
                                    int updateResult=MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, null);
                                    if (updateResult>0)
                                    {
                                        //更新图书主题时，先删除再插入记录
                                        sql = $" delete from book_topical where isbn='{bookinfo.isbn_no}'";
                                        MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, null);
                                        funcInsertBookTopic(bookinfo);
                                        //if (MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, null) > 0) {
                                        //    funcInsertBookTopic(bookinfo);
                                        //}

                                    }
                                    existsBookList.Add(bookinfo);
                                }
                                catch (Exception ee) {
                                    failBookList.Add(bookinfo);
                                }
                            }
                        }
                        else
                        {
                            try
                            {
                                string sql = "insert into book_info(isbn_no, book_name, author, press, publication_date, category, price, readable, imgurl, brief, `describe`, create_time, modify_time)values(@isbn_no, @book_name, @author, @press, @publication_date, @category, @price, @readable, @imgurl, @brief, @describe, @create_time, @modify_time)";
                                MySqlParameter[] param = new MySqlParameter[] {
                                    new MySqlParameter("@isbn_no",bookinfo.isbn_no),
                                    new MySqlParameter("@book_name",bookinfo.book_name),
                                    new MySqlParameter("@author",bookinfo.author),
                                    new MySqlParameter("@press",bookinfo.press),
                                    new MySqlParameter("@publication_date",bookinfo.publication_date),
                                    new MySqlParameter("@category",bookinfo.category),
                                    new MySqlParameter("@price",bookinfo.price),
                                    new MySqlParameter("@readable",bookinfo.readable),
                                    new MySqlParameter("@imgurl",bookinfo.imgurl),
                                    new MySqlParameter("@brief",bookinfo.brief),
                                    new MySqlParameter("@describe",bookinfo.describe),
                                    new MySqlParameter("@create_time",bookinfo.create_time),
                                    new MySqlParameter("@modify_time",bookinfo.modify_time)
                                };
                                int result = MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                                if (result > 0) {

                                    //保存适读年龄
                                    if (GetBookReadable(bookinfo.isbn_no) == null)
                                    {
                                        var bookReadable = new BookReadable()
                                        {
                                            create_time = DateTime.Now,
                                            isbn = bookinfo.isbn_no,
                                            min_age = bookinfo.min_age != null ? bookinfo.min_age : null,
                                            max_age = bookinfo.max_age != null ? bookinfo.max_age : null,
                                        };
                                        InsertBookReadable(bookReadable);
                                    }
                                    //写入图书主题标签信息
                                    funcInsertBookTopic(bookinfo);

                                }
                                successBookListbool.Add(bookinfo);
                            }
                            catch {
                                failBookList.Add(bookinfo);
                                //trans.Rollback();
                            }
                        }
                    }
                    trans.Commit();
                }
            }
        }
        /// <summary>
        /// 保存标签-isbn建档信息
        /// </summary>
        /// <param name="book_init_mapping"></param>
        /// <returns></returns>
        public static int InsertBookInitMapping(Book_init_mapping book_init_mapping)
        {
            using (MySqlConnection con = new MySqlConnection(Utility.MysqlDES3DecryptConnctionString))
            {
                con.Open();
                using (MySqlTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string sql = "insert into book_init_mapping(tag_id,isbn,status,account,tag_type,create_time,isbn_type) values(@tag_id,@isbn,@status,@account,@tag_type,@create_time,isbn_type)";
                        MySqlParameter[] param = new MySqlParameter[]
                        {
                        new MySqlParameter("@tag_id",book_init_mapping.tag_id),
                        new MySqlParameter("@isbn",book_init_mapping.isbn),
                        new MySqlParameter("@status",book_init_mapping.status),
                        new MySqlParameter("@account",book_init_mapping.account),
                        new MySqlParameter("@tag_type",book_init_mapping.tag_type),
                        new MySqlParameter("@create_time",book_init_mapping.create_time),
                        new MySqlParameter("@isbn_type",book_init_mapping.isbn_type),
                        };
                        var res = MySqlInstance.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ee)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }
        /// <summary>
        /// 根据标签查询标签建档信息
        /// </summary>
        /// <param name="tag_id"></param>
        /// <returns></returns>
        public static Book_init_mapping SelectBookInitMapping(string tag_id)
        {
            string sql = $"select * from book_init_mapping where tag_id=@tag_id";
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@tag_id",tag_id)
                };
            var res = MySqlInstance.ExecuteList<Book_init_mapping>(CommandType.Text, sql, param);
            if (null != res && res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
        /// <summary>
        /// 获取标签图书相关信息列表
        /// </summary>
        /// <param name="tag_id"></param>
        /// <param name="isbn"></param>
        /// <param name="user_account"></param>
        /// <returns></returns>
        public static List<Book_init_mapping> SelectBookInitMappingListBy(string tag_id, string isbn = "", string user_account = "")
        {
            string sql = $"select * from book_init_mapping where tag_id=@tag_id";
            List<MySqlParameter> param = new List<MySqlParameter>() {
                new MySqlParameter("@tag_id",tag_id)
                };
            if (!string.IsNullOrEmpty(isbn))
            {
                sql += $" and isbn='{isbn}'";
                param.Add(new MySqlParameter("@isbn", isbn));
            }
            if (!string.IsNullOrEmpty(user_account))
            {
                sql += $" and user_account='{user_account}'";
                param.Add(new MySqlParameter("@user_account", user_account));
            }

            var res = MySqlInstance.ExecuteListText<Book_init_mapping>(sql, param.ToArray());
            return res;
        }
        /// <summary>
        /// 查询当日标签建档信息
        /// </summary>
        /// <param name="rangeType"></param>
        /// <param name="tagType"></param>
        /// <returns></returns>
        public static List<Book_init_mapping> GetBookInitMapping(string rangeType = null, int tagType = 0)
        {
            string range = rangeType == null ? DateTime.Now.ToString("yyyyMM") : rangeType;
            string sql = $"select * from book_init_mapping where tag_type={tagType} and  to_days(create_time)=to_days(now()) order by create_time desc";
            var res = MySqlInstance.ExecuteList<Book_init_mapping>(CommandType.Text, sql, null);
            return res;
        }

        public static List<Book_init_mappingExt> GetBookInitMappingExt(string rangeType = null, int tagType = 0)
        {
            string range = rangeType == null ? DateTime.Now.ToString("yyyyMM") : rangeType;
            string sql = $"select a.ID, a.tag_id, a.isbn, a.status, a.account, a.tag_type, a.create_time, a.gather_time, a.filing_time, a.isbn_type, a.isbn_sequence,b.book_name from book_init_mapping a left join book_info b on a.isbn = b.isbn_no    where a.tag_type={tagType} and  to_days(a.create_time)=to_days(now()) order by a.create_time desc";

            var res = MySqlInstance.ExecuteList<Book_init_mappingExt>(CommandType.Text, sql, null);

            return res;
        }
        public static DataTable GetBookInitMappingBooks(string where="") {
            string sql = "select map.ID, map.tag_id, map.isbn, map.status, map.account, map.tag_type, map.create_time,";
            sql += " map.isbn_type,bi.book_name,be.current_grid_code,concat(be.borrowed_status,'') borrowed_status,bsi.code,bsi.area,bsi.address,case be.borrowed_status when 0 then '上架' when 1 then '下架' when '2' then '借阅中' else '未知' end  borrowed_status_text,imgurl from book_init_mapping map";
            sql += " left join book_info bi on map.isbn = bi.isbn_no";
            sql += " left join book_entity be on be.rfid_code = map.tag_id";
            sql += " left join bookgrid_info bgi on bgi.bookgrid_code = be.current_grid_code";
            sql += " left join bookshelf_info bsi on bsi.code = bgi.bookshelf_code";
            sql += !string.IsNullOrEmpty(where) ? $" where {where}" : "";
            var res = MySqlInstance.ExecuteDataSet(CommandType.Text, sql, null);
            if (null != res && res.Tables[0].Rows.Count > 0) {
                return res.Tables[0];
            }
            return null;
        }
        /// <summary>
        /// 更新标签建档信息
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="tagID"></param>
        /// <returns></returns>
        public static bool UpdateBookInitMapping(string isbn, string tagID)
        {
            string sql = "update book_init_mapping set ISBN=@ISBN where tag_id=@tag_id";
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@ISBN",isbn),
                new MySqlParameter("@tag_id",tagID)
            };
            var res = MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, param);
            if (res > 0)
            {
                return true;
            }
            return false;
        }
        /// <summary>
        /// 查询用户
        /// </summary>
        /// <param name="user_account"></param>
        /// <returns></returns>
        public static Book_init_user GetBookInitUser(string user_account)
        {
                string sql = "select * from book_init_user where user_account=@user_name";
                var res = MySqlInstance.ExecuteList<Book_init_user>(CommandType.Text, sql, new MySqlParameter[] {
                new MySqlParameter("@user_name",user_account)
                 });
                if (null != res && res.Count > 0)
                {
                    return res[0];
                }
                return null;
        }


        /// <summary>
        /// 删除标签建档信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static bool DeletetBookInitMapping(string tag_id)
        {
            string sql = "delete from book_init_mapping where tag_id=@tag_id";
            var res = MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
            new MySqlParameter("@tag_id",tag_id)
            });
            return res > 0;
        }

        /// <summary>
        /// 保存BookRfidIsbnMapping
        /// </summary>
        /// <param name="bookRfidIsbnMapping"></param>
        /// <returns></returns>
        public static bool InsertBookRfidIsbnMapping(BookRfidIsbnMapping bookRfidIsbnMapping)
        {
                string sql = "insert into book_rfid_isbn_mapping(isbn,rfid_tag_id,isbn_sequence,isbn_type,status) values(@isbn,@rfid_tag_id,@isbn_sequence,@isbn_type,@status)";
                var res = MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
                new MySqlParameter("@isbn",bookRfidIsbnMapping.isbn),
                new MySqlParameter("@rfid_tag_id",bookRfidIsbnMapping.rfid_tag_id),
                new MySqlParameter("@isbn_sequence",bookRfidIsbnMapping.isbn_sequence),
                new MySqlParameter("@isbn_type",bookRfidIsbnMapping.isbn_type),
                new MySqlParameter("@status",bookRfidIsbnMapping.status)
            });
                return res > 0;
        }
        ///// <summary>
        ///// 处理BookRfidIsbnMapping表逻辑信息
        ///// </summary>
        ///// <param name="bookRfidIsbnMapping"></param>
        ///// <returns></returns>
        //public static bool ProcessBookRfidIsbnMappingLogical(BookRfidIsbnMapping bookRfidIsbnMapping)
        //{

        //    //查询isbn记录是否存在，存在则记录为多本，不存在则记录为单本
        //    var sql = "update book_rfid_isbn_mapping set isbn_sequence=@isbn_sequence,isbn_type=@isbn_type where rfid_tag_id=@rfid_tag_id";
        //    List<BookRfidIsbnMapping> listBookRfidIsbnMapping = GetBookRfidIsbnMappingByIsbn(bookRfidIsbnMapping.isbn);
        //    if (listBookRfidIsbnMapping == null || listBookRfidIsbnMapping.Count == 1)
        //    {
        //        bookRfidIsbnMapping.isbn_sequence = 1;
        //        bookRfidIsbnMapping.isbn_type = 1;
        //    }
        //    else
        //    {
        //        bookRfidIsbnMapping.isbn_sequence = listBookRfidIsbnMapping.Count;
        //        bookRfidIsbnMapping.isbn_type = 2;
        //        string update_sql = $"update book_rfid_isbn_mapping set isbn='{bookRfidIsbnMapping.isbn}'";
        //        MySqlInstance.ExecuteNonQueryText(update_sql, null);
        //    }
        //    //更新BookRfidIsbnMapping中isbn对应图书信息
        //    var res = MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
        //        new MySqlParameter("@isbn_sequence",bookRfidIsbnMapping.isbn_sequence),
        //        new MySqlParameter("@isbn_type", bookRfidIsbnMapping.isbn_type),
        //        new MySqlParameter("@rfid_tag_id", bookRfidIsbnMapping.rfid_tag_id)
        //    });
        //    return res > 0;
        //}

        /// <summary>
        /// 查询rfid标签信息
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public static List<BookRfidIsbnMapping> GetBookRfidIsbnMappingByIsbn(string isbn = "")
        {
            string sql = "select * from book_rfid_isbn_mapping where isbn=@isbn";
            var res = MySqlInstance.ExecuteList<BookRfidIsbnMapping>(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
            new MySqlParameter("@isbn",isbn)
            });
            return res;
        }
        /// <summary>
        /// 异步保存BookRfidIsbnMapping
        /// </summary>
        /// <param name="bookRfidIsbnMapping"></param>
        /// <returns></returns>
        public async static Task<bool> InsertBookRfidIsbnMappingAsync(BookRfidIsbnMapping bookRfidIsbnMapping)
        {
            return await Task.Run(() => InsertBookRfidIsbnMapping(bookRfidIsbnMapping));
        }
        /// <summary>
        /// 获取适读年龄
        /// </summary>
        /// <returns></returns>
        public static List<DictBookReadable> GetDicBookReadable() {
            return MySqlInstance.ExecuteList<DictBookReadable>(CommandType.Text, "select * from dict_book_readable", null);
        }
        /// <summary>
        /// 保存图书标签信息
        /// </summary>
        /// <param name="bookTopical"></param>
        /// <returns></returns>
        public static bool InsertBookTopical(BookTopical bookTopical,MySqlTransaction tran=null) {
            string sql = "insert into book_topical(isbn,topical_code,create_time)values(@isbn,@topical_code,@create_time)";
            MySqlParameter[] param = new MySqlParameter[] {
                new MySqlParameter("@isbn",bookTopical.isbn),
                new MySqlParameter("@topical_code",bookTopical.topical_code),
                new MySqlParameter("@create_time",bookTopical.create_time)
            };
            int res=MySqlInstance.ExecuteNonQuery(tran,CommandType.Text, sql, param);
            return res > 0;
        }
        /// <summary>
        /// 获取图书主题映射表
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="topical_name"></param>
        /// <returns></returns>
        public static List<BooktopicalMappings> GetBookTopicalMappingList() {
            string sql = $"select * from book_topical_mappings";
            return MySqlInstance.ExecuteList<BooktopicalMappings>(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 根据isbn和图书标签名获取对应关系
        /// </summary>
        /// <param name="isbn"></param>
        /// <param name="topical_name"></param>
        /// <returns></returns>
        public static BookTopicalExt GetBookTopical(string isbn, string topical_name) {
            string sql = $"select * from book_topical a join book_topical_mappings b  on a.topical_code=b.topical_code where a.isbn='{isbn}' and a.topical_code='{topical_name}'";
            var res = MySqlInstance.ExecuteList<BookTopicalExt>(CommandType.Text, sql, null);
            if (null != res && res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
        /// <summary>
        /// 根据isbn查询适读年龄
        /// </summary>
        /// <param name="isbn"></param>
        /// <returns></returns>
        public static BookReadable GetBookReadable(string isbn) {
            string sql = $"select * from book_readable where isbn='{isbn}'";
            var res=MySqlInstance.ExecuteList<BookReadable>(CommandType.Text, sql,null);
            if (null != res && res.Count > 0) {
                return res[0];
            }
            return null;
        }
        /// <summary>
        /// 保存适读年龄
        /// </summary>
        /// <param name="bookReadable"></param>
        /// <returns></returns>
        public static bool InsertBookReadable(BookReadable bookReadable) {
            string sql = "insert into Book_readable(isbn,min_age,max_age,create_time)value(@isbn,@min_age,@max_age,@create_time)";
            int res= MySqlInstance.ExecuteNonQuery(CommandType.Text, sql, new MySqlParameter[] {
                new MySqlParameter("@isbn",bookReadable.isbn),
                new MySqlParameter("@min_age",bookReadable.min_age),
                new MySqlParameter("@max_age",bookReadable.max_age),
                new MySqlParameter("@create_time",bookReadable.create_time)
            });
            return res > 0;
        }


        public static List<T> GetInfoList<T>(string sql,Func<T,bool> filter=null,params MySqlParameter[] sqlParams) where T:class
        {
            var res= MySqlInstance.ExecuteList<T>(CommandType.Text, sql, sqlParams);
            if (null != res&&filter!=null) {
                var filterRes = res.Where(filter).DefaultIfEmpty(null);
                if (filterRes != null) {
                    return filterRes.ToList();
                }
            }
            return res;
        }

        public static List<T> GetInfoListPagination<T>(Pagination page=null) where T:class,new()
        {
            if (page == null) {
                page = new Pagination();
            }
            string table = UserReflect<T>.GetUserTableName();
            var total_count = MySqlInstance.ExecuteScalarText($"select count(0) from {table}", null) ?? "0";
            page.TotalCount =int.Parse(total_count.ToString());
            string fileds = !string.IsNullOrEmpty(page.Fileds) ? page.Fileds : "*",
                   //limit = $"{(page.EnablePaging?"limit "+page.PageSize * (page.PageCode - 1):"")}",
                   limit="",
                   filter =$" where {(!string.IsNullOrEmpty(page.Filter) ? page.Filter : " 1=1")}";
            var sql = $"select {fileds} from {table} {filter}{limit}";
           var res=MySqlInstance.ExecuteList<T>(CommandType.Text, sql, null);
            if (res != null) {
                return res;
            }
            return null;
        }
        /// <summary>
        /// 非空信息更新
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static bool Update<T>(T t)
        {
          return  MySqlInstance.ExecuteNonQuery(CommandType.Text, GetUpdateSQL(t).ToString(), null)>0;
        }
        private static StringBuilder GetUpdateSQL<T>(T obj)
        {
            string primaryKey = string.Empty;
            string tableKey = UserReflect<T>.GetUserField(FieldTypeEnum.Identity,obj,ref primaryKey);
            string tableName = UserReflect<T>.GetUserTableName();
            StringBuilder strSQL = new StringBuilder();
            if (string.IsNullOrEmpty(primaryKey))
            {
                return strSQL;
            }
            Type t = obj.GetType();
            strSQL.Append("update " + tableName + " set ");
            string subSQL = "";
            string condition = " where " + tableKey + "='" + primaryKey.Replace("'", "''") + "'";
            foreach (PropertyInfo pi in t.GetProperties())
            {
                object name = pi.Name;
                Type piType = pi.PropertyType;
                DefineAttribute attr = pi.GetCustomAttribute(typeof(DefineAttribute), false) as DefineAttribute;
                string value1 = Convert.ToString(pi.GetValue(obj, null)).Replace("'", "''");
                if ((null != attr  && (attr.PrimaryKey || attr.Ignor))
                    ||string.IsNullOrEmpty(value1))
                {
                    continue;
                }
                if ((piType.FullName == "System.DateTime"&&!(Convert.ToDateTime(value1)>DateTime.MinValue))
                    || (piType.FullName == "System.Decimal"&& !(Convert.ToDecimal(value1) > 0)&&attr.GtrZero))
                {
                    continue;
                }
                string properName = name.ToString().ToLower();
                if (properName != tableKey.ToLower())
                {
                    value1 = value1== "@null" ? "" : value1;
                    subSQL += Convert.ToString(name) + "='" + value1 + "',";
                }
            }
            subSQL = subSQL.TrimEnd(',');
            strSQL.Append(subSQL);
            strSQL.Append(condition);
            return strSQL;
        }
        #endregion

        #region sqlite 操作
        /// <summary>
        /// 根据条件获取标签信息
        /// </summary>
        /// <param name="rangeType"></param>
        /// <returns></returns>
        public static List<TagInfo> GetTagInfoListByRange(string rangeType = null, int tagType = 0)
        {
            string range = rangeType == null ? DateTime.Now.ToString("yyyyMM") : rangeType;
            string sql = $"select * from TagInfo where tagType={tagType} order by createdate desc";
            return Utility.SQLiteInstance.ExecuteList<TagInfo>(CommandType.Text, sql, null);
        }
        /// <summary>
        /// 插入或修改标签isbn
        /// </summary>
        /// <param name="tagId">标签id</param>
        /// <param name="epc">epc</param>
        /// <param name="isbn"></param>
        /// <param name="cpuId"></param>
        /// <param name="tagType">标签类型0：高频，1超高频</param>
        /// <returns></returns>
        public static int InsertOrUpdateTagInfo(string tagId, string epc, string isbn, string cpuId, int tagType = 0)
        {
            var param = new object[] { tagId, epc, isbn, cpuId, tagType };
            var res = Utility.SQLiteInstance.ExecuteScalarSp("usp_CheckInsertTag", param) ?? 0;
            return (int)res;
        }

        public static TagInfo SelectTagInfoByID(string tagID)
        {
            string sql = $"select * from TagInfo where tagID=@tagID";
            SQLiteParameter[] param = new SQLiteParameter[] {
                 new SQLiteParameter("@tagID",tagID)
            };
            var res = Utility.SQLiteInstance.ExecuteList<TagInfo>(CommandType.Text, sql, param);
            if (null != res && res.Count > 0)
            {
                return res[0];
            }
            return null;
        }
        public static int InsertTagInfo(TagInfo tagInfo)
        {
            using (SQLiteConnection con = new SQLiteConnection(ConnectInit.SqliteConnectionString))
            {
                con.Open();
                using (SQLiteTransaction trans = con.BeginTransaction())
                {
                    try
                    {
                        string sql = "INSERT INTO TagInfo(TagID,EPC,ISBN,CpuID,TagType,CreateDate,Status) values(@TagID,@EPC,@ISBN,@CpuID,@TagType,@CreateDate,@Status)";
                        SQLiteParameter[] param = new SQLiteParameter[]
                        {
                            new SQLiteParameter("@TagID",tagInfo.TagID),
                            new SQLiteParameter("@EPC",tagInfo.EPC),
                            new SQLiteParameter("@ISBN",tagInfo.ISBN),
                            new SQLiteParameter("@CpuID",tagInfo.CpuID),
                            new SQLiteParameter("@TagType",tagInfo.TagType),
                            new SQLiteParameter("@CreateDate",tagInfo.CreateDate),
                            new SQLiteParameter("@Status",tagInfo.Status)
                        };
                        var res = Utility.SQLiteInstance.ExecuteNonQuery(trans, CommandType.Text, sql, param);
                        trans.Commit();
                        return 1;
                    }
                    catch (Exception ee)
                    {
                        trans.Rollback();
                        return 0;
                    }
                }
            }
        }

        public static bool UpdateTagInfo(string isbn, string tagID)
        {
            string sql = "update TagInfo set ISBN=@ISBN where TagID=@TagID";
            SQLiteParameter[] param = new SQLiteParameter[] {
                 new SQLiteParameter("@ISBN",isbn),
                 new SQLiteParameter("@TagID",tagID)
            };
            var res = Utility.SQLiteInstance.ExecuteNonQuery(CommandType.Text, sql, param);
            if (res > 0)
            {
                return true;
            }
            return false;
        }
        public static bool DeleteTag(string tagID)
        {
            string sql = $"delete from TagInfo where TagID=@TagID";
            int res = Utility.SQLiteInstance.ExecuteNonQuery(CommandType.Text, sql, new System.Data.Common.DbParameter[] {
                new SQLiteParameter("@TagID",tagID)
            });
            return res > 0;
        }
        #endregion
    }

}

