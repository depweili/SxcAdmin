﻿using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Application.Service.SxcManage;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System;
using System.Linq;

namespace YaChH.Application.Busines.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-10-29 21:37
    /// 描 述：Sxc_OrderInfo
    /// </summary>
    public class Sxc_OrderInfoBLL
    {
        private Sxc_OrderInfoIService service = new Sxc_OrderInfoService();

        private Sxc_CommodityIService commodityService = new Sxc_CommodityService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_OrderInfoEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_OrderInfoEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }

        public IEnumerable<Sxc_OrderInfoEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        public dynamic GetPageList(Pagination pagination, string queryJson)
        {
            var data = service.GetPageList(pagination, queryJson);

            var res = data.Select(t => new
            {
                t.ID,
                RealName=t.UserIntegral.User.UserProfile.RealName,
                t.CreateTime,
                t.Consignee,
                t.AddressRegion,
                t.AddressDetail,
                t.Zipcode,
                t.Telephone,
                t.TotalAmount,
                t.TotalPoints,
                t.UserNote,
                t.ToUserNote,
                //CommodityName=t.OrderCommoditys.FirstOrDefault().Commodity.Name
                CommodityName=commodityService.GetEntity(t.OrderCommoditys.FirstOrDefault().CommodityID.ToString()).Name
            });

            return res;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sxc_OrderInfoEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }
        #endregion
    }
}
