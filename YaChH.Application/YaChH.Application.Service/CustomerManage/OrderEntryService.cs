using YaChH.Application.Entity.CustomerManage;
using YaChH.Application.IService.CustomerManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

namespace YaChH.Application.Service.CustomerManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-03-16 13:54
    /// 描 述：订单明细
    /// </summary>
    public class OrderEntryService : RepositoryFactory<OrderEntryEntity>, IOrderEntryService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="orderId">订单主键</param>
        /// <returns>返回列表</returns>
        public IEnumerable<OrderEntryEntity> GetList(string orderId)
        {
            return this.BaseRepository().IQueryable(t => t.OrderId.Equals(orderId)).OrderByDescending(t => t.SortCode).ToList();
        }
        #endregion
    }
}