using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using System.Text;
using YaChH.Data.EF.Tool;
using System.ComponentModel;
using YaChH.Data.EF.Extension;

namespace YaChH.Application.Service.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-25 09:17
    /// 描 述：Sxc_Reservation
    /// </summary>
    public class Sxc_ReservationService : RepositoryFactory<Sxc_ReservationEntity>, Sxc_ReservationIService
    {
        private string DbName = "SXC";
        #region 获取数据
        public IEnumerable<Sxc_ReservationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_ReservationEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["Name"].IsEmpty())
            {
                string keyord = queryParam["Name"].ToString();
                expression = expression.And(t => t.Name.Contains(keyord));
            }

            if (!queryParam["MobilePhone"].IsEmpty())
            {              
                string keyord = queryParam["MobilePhone"].ToString();
                expression = expression.And(t => t.MobilePhone.Contains(keyord));
                     
            }

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Ascending) };
            //Include("Agent").Include("UserPayment").  Where(x=>true  
            //Include(t=>t.UserPayment.User.UserProfile).
            var query = this.BaseRepository(DbName).IQueryable().Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_ReservationEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().OrderByDescending(r=>r.CreateTime).ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_ReservationEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DbName).FindEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {

            var strSql = new StringBuilder();
            strSql.Append(string.Format(@"delete Sxc_ReservationCourse where ReservationID={0}", keyValue));
            
            var agentList = this.BaseRepository(DbName).ExecuteBySql(strSql.ToString());

            this.BaseRepository(DbName).Delete(keyValue);

        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sxc_ReservationEntity entity)
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
