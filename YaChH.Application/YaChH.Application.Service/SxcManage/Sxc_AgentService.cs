using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using System;
using YaChH.Application.Entity.SxcManage.ViewModel;
using System.Text;
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
    /// 日 期：2017-11-07 11:21
    /// 描 述：Sxc_Agent
    /// </summary>
    public class Sxc_AgentService : RepositoryFactory<Sxc_AgentEntity>, Sxc_AgentIService
    {
        
        private string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_AgentEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            return this.BaseRepository(DbName).FindList(pagination);
        }

        public IEnumerable<Sxc_AgentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_AgentEntity>();
            var queryParam = queryJson.ToJObject();

            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyord = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "Name":
                        expression = expression.And(t => t.User.UserProfile.NickName.Contains(keyord) || t.User.UserProfile.RealName.Contains(keyord));
                        break;
                    case "IDCard":          
                        expression = expression.And(t => t.User.UserProfile.IDCard.Contains(keyord));
                        break;
                    case "Mobile":          
                        expression = expression.And(t => t.User.UserProfile.MobilePhone.Contains(keyord));
                        break;
                    case "Area":
                        expression = expression.And(t => t.Area.Area.Contains(keyord));
                        break;
                    case "SupAgent":
                        expression = expression.And(t => t.ParentAgent.User.UserProfile.RealName.Contains(keyord));
                        break;
                    default:
                        break;
                }
            }

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Ascending) };
            //Include("Agent").Include("UserPayment").  Where(x=>true  
            //Include(t=>t.UserPayment.User.UserProfile).
            var query = this.BaseRepository(DbName).IQueryable().Include(t => t.User.UserProfile).Include(t=>t.Area).Include(t=>t.ParentAgent.User.UserProfile).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_AgentEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_AgentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DbName).FindEntity(int.Parse(keyValue));
        }

        public IEnumerable<AgentMemberTreeModel> GetMyMemberList(string userId)
        {
            var strSql = new StringBuilder();
            strSql.Append(string.Format(@"with Agent as ( select Sxc_Agent.Id from Sxc_Agent inner join Sxc_User
			  on Sxc_Agent.ID=Sxc_User.ID where Sxc_User.SystemAccount='{0}' 

    union all   select d.Id from   Agent
    inner join Sxc_Agent d on Agent.Id = d.PID ) 
	
	SELECT Sxc_Agent.ID Id,(case  Sxc_Agent.ID when Sxc_User.ID then 0 else Sxc_Agent.PID end )PId ,

            	  (case isnull(RealName,'') when '' then NickName  else RealName end)Name,
            	  '加入日期：'+CONVERT(varchar(100), Sxc_Agent.SupAgentBindTime, 23) Value, 'image://'+Sxc_UserProfile.AvatarUrl Symbol
              FROM Sxc_UserProfile  inner join Sxc_Agent 
              on Sxc_UserProfile.ID=Sxc_Agent.ID 
			  left join  (select ID from Sxc_User where Sxc_User.SystemAccount='{0}')Sxc_User
			  on Sxc_Agent.ID=Sxc_User.ID  
			     where Sxc_Agent.ID in (select id from Agent ) order by pid", userId));
            //            return this.BaseRepository().FindList(strSql.ToString());

            RepositoryFactory<AgentMemberTreeModel> rf = new RepositoryFactory<AgentMemberTreeModel>();
            var agentList= rf.BaseRepository(DbName).FindList(strSql.ToString());

            return agentList;
           
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
        public void SaveForm(string keyValue, Sxc_AgentEntity entity)
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
