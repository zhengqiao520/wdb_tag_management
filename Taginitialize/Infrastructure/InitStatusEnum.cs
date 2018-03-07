using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
namespace Infrastructure
{
    public class UserReflect<T> {
        public static string GetEnumDescription(Enum enumValue)
        {
            string value = enumValue.ToString();
            FieldInfo field = enumValue.GetType().GetField(value);
            object[] objs = field.GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
            if (objs == null || objs.Length == 0)
                return value;
            System.ComponentModel.DescriptionAttribute descriptionAttribute = (System.ComponentModel.DescriptionAttribute)objs[0];
            return descriptionAttribute.Description;
        }
        public static string GetUserTableName() 
        {
            Type type = typeof(T);
            Attribute attribute = type.GetCustomAttribute(typeof(DefineAttribute), true);
            if (attribute is DefineAttribute && null != attribute) {
                return ((DefineAttribute)attribute).TableName;
            }
            return null;
        }
        public static string GetUserField(FieldTypeEnum fieldTypeEnum,T obj,ref string value) 
        {
            string filedAttribute = string.Empty;
            Type t = obj.GetType();
            foreach (var pro in t.GetProperties())
            {
                DefineAttribute defineAttribute = pro.GetCustomAttribute(typeof(DefineAttribute), false) as DefineAttribute;
                if (defineAttribute == null) { continue; }
                switch (fieldTypeEnum)
                {
                    case FieldTypeEnum.Identity:
                        if (defineAttribute.PrimaryKey) {
                            filedAttribute = pro.Name;
                            value = Convert.ToString(pro.GetValue(obj, null));
                            break;
                        }
                        break;
                    case FieldTypeEnum.Ignor:
                        if (defineAttribute.Ignor)
                        {
                            filedAttribute = pro.Name;
                            break;
                        }
                        break;
                }
            }
            return filedAttribute;
        }
    }
    /// <summary>
    /// 图书标签状态枚举
    /// </summary>
    public enum InitStatusEnum
    {
        /// <summary>
        /// 已建档
        /// </summary>
        RecordInit = 0,
        /// <summary>
        /// 已采集(未校对)
        /// </summary>
        RecordGrab = 1,
        /// <summary>
        /// 已采集(已校对)
        /// </summary>
        RecordComfirmed = 2,
        /// <summary>
        /// 图书已上架
        /// </summary>
        RecordPubshed = 3
    }
    public enum FieldTypeEnum
    {
        Identity,
        TableName,
        AutoIncrement,
        Ignor
    }

    /// <summary>
    /// 标签类型
    /// </summary>
    public enum TagTypeEnum {
        /// <summary>
        /// 高频标签
        /// </summary>
        HighFrequency=0,
        /// <summary>
        /// 超高频
        /// </summary>
        UltrahighFrequency=1
    }
    /// <summary>
    /// 数据库类型
    /// </summary>
    public enum DbType {

        [System.ComponentModel.Description("生产环境,请谨慎操作！")]
        MySQL,
    
        [System.ComponentModel.Description("线上开发环境")]
        MYSQ_DEV,

        [System.ComponentModel.Description("线下测试环境")]
        MYSQL_UAT,

        [System.ComponentModel.Description("SQLite数据库（Fake）")]
        SQLIITE,
    }

}
