using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaChH.Application.Entity.SxcManage.ViewModel
{
   public class Sxc_CooperationModel
    {
     
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// MobilePhone
        /// </summary>
        /// <returns></returns>
        public string MobilePhone { get; set; }
        /// <summary>
        /// AreaInfo
        /// </summary>
        /// <returns></returns>
        public string AreaInfo { get; set; }
        /// <summary>
        /// Address
        /// </summary>
        /// <returns></returns>
        public string Address { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        /// <returns></returns>
        public int? Type { get; set; }

        /// <summary>
        /// Level
        /// </summary>
        /// <returns></returns>
        public int? Level { get; set; }

        /// <summary>
        /// AgentAreaInfo
        /// </summary>
        /// <returns></returns>
        public string AgentAreaInfo { get; set; }
        /// <summary>
        /// Memo
        /// </summary>
        /// <returns></returns>
        public string Memo { get; set; }
        /// <summary>
        /// ProcessDetail
        /// </summary>
        /// <returns></returns>
        public string ProcessDetail { get; set; }
        /// <summary>
        /// CreateTime
        /// </summary>
        /// <returns></returns>
        public DateTime? CreateTime { get; set; }
        /// <summary>
        /// UserID
        /// </summary>
        /// <returns></returns>
        public int? UserID { get; set; }
        /// <summary>
        /// AreaID
        /// </summary>
        /// <returns></returns>
        public int? AreaID { get; set; }
     
        public string TypeName { get; set; }
        public string LevelName { get; set; }

        public string NickName { get; set; }
        public string RealName { get; set; }
    }
}
