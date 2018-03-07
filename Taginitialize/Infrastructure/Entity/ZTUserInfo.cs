using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    [Serializable]
    [Define(TableName ="user_info")]
    public class ZTUserInfo
    {   
        /// <summary>
        /// auto_increment
        /// </summary>		
        [Define(PrimaryKey=true)]
        public long id
        {
            get;
            set;
        }
        /// <summary>
        /// mobile_no
        /// </summary>		
        public string mobile_no
        {
            get;
            set;
        }
        /// <summary>
        /// user_name
        /// </summary>		
        public string user_name
        {
            get;
            set;
        }
        /// <summary>
        /// cert_type
        /// </summary>		
        public int cert_type
        {
            get;
            set;
        }
        /// <summary>
        /// cert_no
        /// </summary>		
        public string cert_no
        {
            get;
            set;
        }
        /// <summary>
        /// email
        /// </summary>		
        public string email
        {
            get;
            set;
        }
        /// <summary>
        /// imgurl
        /// </summary>		
        public string imgurl
        {
            get;
            set;
        }
        /// <summary>
        /// create_time
        /// </summary>		
        public DateTime create_time
        {
            get;
            set;
        }
        /// <summary>
        /// modify_time
        /// </summary>		
        public DateTime modify_time
        {
            get;
            set;
        }
        /// <summary>
        /// usertoken
        /// </summary>		
        public string usertoken
        {
            get;
            set;
        }
        /// <summary>
        /// new_usertoken
        /// </summary>		
        public string new_usertoken
        {
            get;
            set;
        }
        /// <summary>
        /// token_expire_times
        /// </summary>		
        public int token_expire_times
        {
            get;
            set;
        }
        /// <summary>
        /// token_generate_time
        /// </summary>		
        public DateTime token_generate_time
        {
            get;
            set;
        }
        /// <summary>
        /// status
        /// </summary>		
        public int status
        {
            get;
            set;
        }
        /// <summary>
        /// registration_id
        /// </summary>		
        public string registration_id
        {
            get;
            set;
        }
        /// <summary>
        /// user_type
        /// </summary>		
        public int user_type
        {
            get;
            set;
        }

    }
}
