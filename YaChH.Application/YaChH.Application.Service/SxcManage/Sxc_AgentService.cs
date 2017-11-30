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
        public IEnumerable<Sxc_AgentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return this.BaseRepository(DbName).FindList(pagination);
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
            strSql.Append(@"SELECT Sxc_Agent.ID Id,
            Sxc_Agent.PID PId,
            	  (case isnull(RealName,'') when '' then NickName  else RealName end)Name,
            	  '' Value,
            	 Sxc_UserProfile.AvatarUrl Symbol

              FROM Sxc_UserProfile  inner join Sxc_Agent 
              on Sxc_UserProfile.ID=Sxc_Agent.ID
            ");
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
