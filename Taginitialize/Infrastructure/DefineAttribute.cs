using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure
{
    
    [AttributeUsage(AttributeTargets.Field|AttributeTargets.Class|AttributeTargets.Property,AllowMultiple =true)]
    public class DefineAttribute:Attribute
    {
        public string TableName { get; set; }
        public string Description { get; set; }
        public bool PrimaryKey { get; set; }
        /// <summary>
        /// 或略属性值
        /// </summary>
        public bool Ignor { get; set; }
        /// <summary>
        /// 必须大于0
        /// </summary>
        public bool GtrZero { get;set; }
    }
}
