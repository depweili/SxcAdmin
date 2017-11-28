using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaChH.Application.Entity.SxcManage.ViewModel
{
    public class Sxc_UserPaymentModel
    {
        public int ID { get; set; }
        public int? UserID { get; set; }
        public string UserName { get; set; }

        public int? PayItemID { get; set; }
        public Guid PayUID { get; set; }
        public string PaySN { get; set; }
        public bool IsDistr { get; set; }
        public int? DistrType { get; set; }
        public int? State { get; set; }
        public decimal Amount { get; set; }
        public decimal DistrAmount { get; set; }
        public decimal Commission { get; set; }
        public decimal FinalAmount { get; set; }
        public DateTime? CreateTime { get; set; }
        public DateTime? DistrTime { get; set; }
        public DateTime? AccountTime { get; set; }
        public string Memo { get; set; }

        public string OperatorID { get; set; }
        public string OperatorName { get; set; }
    }
}
