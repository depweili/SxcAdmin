using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using YaChH.Data.EF.Tool;
using YaChH.Data.EF.Extension;
using System.ComponentModel;
using System.Data.Entity;

namespace YaChH.Application.Service.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-16 15:59
    /// 描 述：Sxc_UserAccount
    /// </summary>
    public class Sxc_UserAccountService : RepositoryFactory<Sxc_UserAccountEntity>, Sxc_UserAccountIService
    {
        public string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_UserAccountEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }

        public IEnumerable<Sxc_UserAccountEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_UserAccountEntity>();
            var queryParam = queryJson.ToJObject();
            //客户名称
            if (!queryParam["UserName"].IsEmpty())
            {
                string CustomerName = queryParam["UserName"].ToString();
                expression = expression.And(t => t.User.UserProfile.NickName.Contains(CustomerName) || t.User.UserProfile.RealName.Contains(CustomerName));
            }


            //if (!OperatorProvider.Provider.Current().IsAdmin)
            //{
            //    var account = OperatorProvider.Provider.Current().Account;
            //    expression = expression.And(t => t.UserPayment.User.SystemAccount == account);
            //}
            var SortDirection = pagination.sord.ToLower() == "asc" ? ListSortDirection.Ascending : ListSortDirection.Descending;
            var SortProperty = pagination.sidx.IsEmpty() ? "ID" : pagination.sidx;
            PropertySortCondition[] ps = new[] { new PropertySortCondition(SortProperty, SortDirection) };
            //Include("Agent").Include("UserPayment").  Where(x=>true  
            //Include(t=>t.UserPayment.User.UserProfile).
            var query = this.BaseRepository(DbName).IQueryable().Include(t=>t.User.UserProfile).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserAccountEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DbName).FindEntity(int.Parse(keyValue));
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository(DbName).Delete(keyValue);
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sxc_UserAccountEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DbName).Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository(DbName).Insert(entity);
            }
        }
        #endregion
    }
}
