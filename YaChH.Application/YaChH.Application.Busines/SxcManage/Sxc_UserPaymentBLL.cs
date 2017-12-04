using YaChH.Application.Entity.SxcManage;
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
    /// 日 期：2017-11-27 02:06
    /// 描 述：Sxc_UserPayment
    /// </summary>
    public class Sxc_UserPaymentBLL
    {
        private Sxc_UserPaymentIService service = new Sxc_UserPaymentService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_UserPaymentEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }

        public IEnumerable<dynamic> GetPageList(Pagination pagination, string queryJson)
        {
            var data = service.GetPageList(pagination, queryJson);

            var res = data.Select(t => new
            {
                t.ID,
                t.IsDistr,
                t.PayItemID,
                t.Commission,
                NickName = t.User.UserProfile.NickName,
                RealName = t.User.UserProfile.RealName,
                CreateTime = t.CreateTime.ToString("yyyy-MM-dd"),
                t.Memo,
                t.AccountTime,
                t.State,
                t.Amount,
                t.DistrTime,
                t.DistrAmount,
                t.FinalAmount,
                t.PayUID,
                t.PaySN,
                t.UserID
            });

            return res;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserPaymentEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Sxc_CommissionRecordEntity> GetDetails1(string keyValue)
        {
            return service.GetDetails(keyValue);
        }

        public IEnumerable<dynamic> GetDetails(string keyValue)
        {
            var data = service.GetDetails(keyValue);

            var res = data.Select(t => new
            {
                t.ID,
                t.UserPaymentID,
                t.AgentID,
                t.Commission,
                NickName = t.Agent.User.UserProfile.NickName,
                RealName = t.Agent.User.UserProfile.RealName,
                CreateTime = t.CreateTime.Value.ToShortDateString(),
                t.Memo,
                t.CheckTime,
                t.State
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
        public void SaveForm(string keyValue, Sxc_UserPaymentEntity entity,List<Sxc_CommissionRecordEntity> entryList)
        {
            try
            {
                service.SaveForm(keyValue, entity, entryList);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void SaveForm(string keyValue, Sxc_UserPaymentEntity entity)
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

        public void DistrCommission(string ids)
        {
            try
            {
                service.DistrCommission(ids);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
