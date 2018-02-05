using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class BookshelfInfo
    {

        /// <summary>
        /// code
        /// </summary>		
        public string code
        {
            get;
            set;
        }
        /// <summary>
        /// area
        /// </summary>		
        public string area
        {
            get;
            set;
        }
        /// <summary>
        /// address
        /// </summary>		
        public string address
        {
            get;
            set;
        }
        /// <summary>
        /// longitude
        /// </summary>		
        public decimal longitude
        {
            get;
            set;
        }
        /// <summary>
        /// latitude
        /// </summary>		
        public decimal latitude
        {
            get;
            set;
        }
        /// <summary>
        /// row_count
        /// </summary>		
        public int row_count
        {
            get;
            set;
        }
        /// <summary>
        /// col_count
        /// </summary>		
        public int col_count
        {
            get;
            set;
        }
        /// <summary>
        /// damage_report
        /// </summary>		
        public int damage_report
        {
            get;
            set;
        }
        /// <summary>
        /// available
        /// </summary>		
        public int available
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
        /// gcj02_longitude
        /// </summary>		
        public decimal gcj02_longitude
        {
            get;
            set;
        }
        /// <summary>
        /// gcj02_latitude
        /// </summary>		
        public decimal gcj02_latitude
        {
            get;
            set;
        }
        /// <summary>
        /// bd09_longitude
        /// </summary>		
        public decimal bd09_longitude
        {
            get;
            set;
        }
        /// <summary>
        /// bd09_latitude
        /// </summary>		
        public decimal bd09_latitude
        {
            get;
            set;
        }
        /// <summary>
        /// district_code
        /// </summary>		
        public string district_code
        {
            get;
            set;
        }
        /// <summary>
        /// extra_info
        /// </summary>		
        public string extra_info
        {
            get;
            set;
        }
        /// <summary>
        /// initialize_time
        /// </summary>		
        public DateTime initialize_time
        {
            get;
            set;
        }

    }
}
