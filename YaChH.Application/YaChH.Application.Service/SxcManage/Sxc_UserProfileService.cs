using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
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
    /// 日 期：2017-11-29 11:51
    /// 描 述：Sxc_UserProfile
    /// </summary>
    public class Sxc_UserProfileService : RepositoryFactory<Sxc_UserProfileEntity>, Sxc_UserProfileIService
    {
        public string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_UserProfileEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            return this.BaseRepository(DbName).FindList(pagination);
        }

        public IEnumerable<Sxc_UserProfileEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_UserProfileEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Name":            
                        expression = expression.And(t => t.NickName.Contains(keyord) || t.RealName.Contains(keyord));
                        break;
                    case "IDCard":          //姓名
                        expression = expression.And(t => t.IDCard.Contains(keyord));
                        break;
                    case "Mobile":          //手机
                        expression = expression.And(t => t.MobilePhone.Contains(keyord));
                        break;
                    default:
                        break;
                }
            }
            

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Ascending) };
            //Include("Agent").Include("UserPayment").  Where(x=>true  
            //Include(t=>t.UserPayment.User.UserProfile).
            var query = this.BaseRepository(DbName).IQueryable().Include(t => t.User).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_UserProfileEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserProfileEntity GetEntity(string keyValue)
        {
            //return this.BaseRepository(DbName).FindEntity(int.Parse(keyValue));

            var id = int.Parse(keyValue);
            var obj = this.BaseRepository(DbName).IQueryable().Include(t => t.User).First(t => t.ID == id);
            obj.SystemAccount = obj.User.SystemAccount;
            return obj;
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
        public void SaveForm(string keyValue, Sxc_UserProfileEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DbName).Update(entity);

                RepositoryFactory<Sxc_UserEntity> rf = new RepositoryFactory<Sxc_UserEntity>();

                var db = rf.BaseRepository(DbName);

                var user = db.FindEntity(entity.ID);
                user.SystemAccount = entity.SystemAccount;
                db.Update(user);

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
