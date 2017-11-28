using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaChH.Application.Entity.AuthorizeManage.ViewModel
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.11.27
    /// 描 述：授权功能Url、操作Url
    /// </summary
    public class AuthorizeUrlModel
    {
        /// <summary>
        /// 授权主键
        /// </summary>		
        public string AuthorizeId { get; set; }
        /// <summary>
        /// 功能主键
        /// </summary>
        public string ModuleId { set; get; }
        /// <summary>
        /// Url地址
        /// </summary>
        public string UrlAddress { set; get; }
        /// <summary>
        /// 名称
        /// </summary>
        public string FullName { set; get; }
    }
}
