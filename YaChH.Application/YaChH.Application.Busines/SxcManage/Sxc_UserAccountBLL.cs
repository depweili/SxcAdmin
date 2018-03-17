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
    /// 日 期：2018-03-16 15:59
    /// 描 述：Sxc_UserAccount
    /// </summary>
    public class Sxc_UserAccountBLL
    {
        private Sxc_UserAccountIService service = new Sxc_UserAccountService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<dynamic> GetPageList(Pagination pagination, string queryJson)
        {
            //return service.GetPageList(pagination, queryJson);

            var data = service.GetPageList(pagination, queryJson);

            var res = data.Select(t => new
            {
                t.ID,
                t.User.UserProfile.NickName,
                t.User.UserProfile.RealName,
                t.AccountID,
                t.Balance,
                t.BankCard,
                t.BankName,
                t.BranchBankName,
                t.Cash,
                t.Expense,
                t.IsValid,
                t.LockBalance,
                t.MobilePhone
            });

            return res;
            
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_UserAccountEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserAccountEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
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
        public void SaveForm(string keyValue, Sxc_UserAccountEntity entity)
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
