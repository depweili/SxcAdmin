using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using System;
using YaChH.Data.EF.Tool;
using System.ComponentModel;
using System.Data.Entity;
using YaChH.Data.EF.Extension;

namespace YaChH.Application.Service.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-10-29 21:37
    /// 描 述：Sxc_OrderInfo
    /// </summary>
    public class Sxc_OrderInfoService : RepositoryFactory<Sxc_OrderInfoEntity>, Sxc_OrderInfoIService
    {
        public string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_OrderInfoEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }

        /// <summary>
        /// 用户列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns></returns>
        public IEnumerable<Sxc_OrderInfoEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_OrderInfoEntity>();
            var queryParam = queryJson.ToJObject();

            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateTime >= startTime && t.CreateTime <= endTime);
            }
            //客户名称
            if (!queryParam["UserName"].IsEmpty())
            {
                string CustomerName = queryParam["UserName"].ToString();
                expression = expression.And(t => t.UserIntegral.User.UserProfile.NickName.Contains(CustomerName) || t.UserIntegral.User.UserProfile.RealName.Contains(CustomerName));
            }
            //电话
            if (!queryParam["Telephone"].IsEmpty())
            {
                string Telephone = queryParam["Telephone"].ToString();
                expression = expression.And(t => t.Telephone.Contains(Telephone));
            }

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Descending) };

            //return this.BaseRepository(DbName).FindList(expression, pagination);
            var query = this.BaseRepository(DbName).IQueryable().Include(t => t.UserIntegral.User.UserProfile).Include(t=>t.OrderCommoditys).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_OrderInfoEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Sxc_OrderInfoEntity entity)
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
