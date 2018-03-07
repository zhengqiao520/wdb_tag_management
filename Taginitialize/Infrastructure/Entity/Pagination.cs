using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Entity
{
    public class Pagination
    {
        public int PageCode { get; set; }
        public int PageSize { get; set; } = 200;
        public int TotalCount { get; set; }
        public string Fileds { get; set; } = null;
        public string Filter { get; set; }
        public bool EnablePaging { get; set; } = true;
        public int TotalPage {
            get {
                if (this.TotalCount == 0)
                    return 0;
                if (TotalCount % PageSize == 0) {
                    return this.TotalCount / this.PageSize;
                }
                return this.TotalCount / this.PageSize + 1;
            }
        }
    }
}
