using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;

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

            var expression = LinqExtensions.True<Sxc_OrderInfoEntity>();
            var queryParam = queryJson.ToJObject();

            //公司主键
            if (!queryParam["organizeId"].IsEmpty())
            {
                string organizeId = queryParam["organizeId"].ToString();
               // expression = expression.And(t => t.OrganizeId.Equals(organizeId));
            }
            //部门主键
            if (!queryParam["departmentId"].IsEmpty())
            {
                string departmentId = queryParam["departmentId"].ToString();
                //expression = expression.And(t => t.DepartmentId.Equals(departmentId));
            }
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                //string condition = queryParam["condition"].ToString();
                //string keyord = queryParam["keyword"].ToString();
                //switch (condition)
                //{
                //    case "Account":            //账户
                //        expression = expression.And(t => t.Account.Contains(keyord));
                //        break;
                //    case "RealName":          //姓名
                //        expression = expression.And(t => t.RealName.Contains(keyord));
                //        break;
                //    case "Mobile":          //手机
                //        expression = expression.And(t => t.Mobile.Contains(keyord));
                //        break;
                //    default:
                //        break;
                //}
            }
            //expression = expression.And(t => t.UserId != "System");

            return this.BaseRepository(DbName).FindList(expression, pagination);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_OrderInfoEntity GetEntity(string keyValue)
        {
            return this.BaseRepository().FindEntity(int.Parse(keyValue));
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            this.BaseRepository().Delete(keyValue);
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
                this.BaseRepository().Update(entity);
            }
            else
            {
                entity.Create();
                this.BaseRepository().Insert(entity);
            }
        }
        #endregion
    }
}
