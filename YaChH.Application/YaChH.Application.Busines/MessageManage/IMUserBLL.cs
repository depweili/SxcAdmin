using YaChH.Application.Entity.MessageManage;
using YaChH.Application.IService.MessageManage;
using YaChH.Application.Service.MessageManage;
using System.Collections.Generic;

namespace YaChH.Application.Busines.MessageManage
{
    /// <summary>
    /// 版 本 V6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创建人：陈彬彬
    /// 日 期：2015.11.25 16:12
    /// 描 述：用户管理
    /// </summary>
    public class IMUserBLL
    {
        private IMsgUserService service = new IMUserService();
        /// <summary>
        /// 用户列表
        /// </summary>
        /// <returns></returns>
        public IEnumerable<IMUserModel> GetList(string OrganizeId)
        {
            return service.GetList(OrganizeId);
        }
    }
}
