﻿using YaChH.Application.Entity.SxcManage;
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
using YaChH.Application.Code;

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

        Sxc_Base_AreaIService areaService = new Sxc_Base_AreaService();
        Sxc_UserProfileIService userProfileService = new Sxc_UserProfileService();
        #region 获取数据

        public IRepository<Sxc_AgentEntity> Repository()
        {
            return this.BaseRepository(DbName);
        }
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

            bool isQueryByParent = false;

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
                        isQueryByParent = true;
                        break;
                    case "SupAgentID":
                        var supid = int.Parse(keyord);
                        expression = expression.And(t => t.ParentAgent.ID == supid);
                        isQueryByParent = true;
                        break;
                    default:
                        break;
                }
            }

            if (!OperatorProvider.Provider.Current().IsAdmin && !isQueryByParent)
            {
                var account = OperatorProvider.Provider.Current().Account;
                expression = expression.And(t => t.ParentAgent.User.SystemAccount == account || t.User.SystemAccount == account);
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
            //return this.BaseRepository(DbName).IQueryable().ToList();

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
                    case "SupAgentID":
                        var supid = int.Parse(keyord);
                        expression = expression.And(t => t.ParentAgent.ID == supid);
                        break;
                    default:
                        break;
                }
            }
            var query = this.BaseRepository(DbName).IQueryable().Include(t => t.User.UserProfile).Where(expression).AsEnumerable();

            return query;
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_AgentEntity GetEntity(string keyValue)
        {
            //return this.BaseRepository(DbName).FindEntity(int.Parse(keyValue));

            var id = int.Parse(keyValue);
            var entity = this.BaseRepository(DbName).IQueryable().Include(t => t.User.UserProfile).Include(t => t.Area).Include(t => t.ParentAgent.User.UserProfile).Single(t => t.ID == id);
            return entity;
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

        public IEnumerable<AgentMemberTreeModel> GetMemberList(string agentID)
        {
            var strSql = new StringBuilder();
            strSql.Append(string.Format(@"with Agent as
                                             (select {0} as ID
                                              union all
                                              select d.Id
                                                from Agent
                                               inner join Sxc_Agent d
                                                  on Agent.Id = d.PID)
      
                                            SELECT Sxc_Agent.ID Id,
                                                   Sxc_Agent.PID PId,
                                                   (case isnull(RealName, '')
                                                     when '' then
                                                      NickName
                                                     else
                                                      RealName
                                                   end) Name,
                                                   '加入日期：' + CONVERT(varchar(100), Sxc_Agent.SupAgentBindTime, 23) Value,
                                                   'image://' + Sxc_UserProfile.AvatarUrl Symbol
                                              FROM Sxc_UserProfile
                                             inner join Sxc_Agent
                                                on Sxc_UserProfile.ID = Sxc_Agent.ID
                                             where Sxc_Agent.ID in (select id from Agent)
                                             order by pid ", agentID));
            //            return this.BaseRepository().FindList(strSql.ToString());

            RepositoryFactory<AgentMemberTreeModel> rf = new RepositoryFactory<AgentMemberTreeModel>();
            var agentList = rf.BaseRepository(DbName).FindList(strSql.ToString());

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

                AfterCreateNewAgent(entity);
            }
        }


        #endregion


        public string CheckNewAgent(Sxc_AgentEntity entity)
        {
            string msg = string.Empty;

            try
            {
                var up = userProfileService.GetEntity(entity.ID.ToString());

                if (!up.IsVerified ?? false)
                {
                    msg = "用户真实信息尚未验证";

                    return msg;
                }

                var rep = this.BaseRepository(DbName);

                if (entity.Type == 2 && entity.Level < 4)
                {
                    if (entity.Level == 3)
                    {
                        if (rep.IQueryable().Any(t => t.Type == 2 && t.Level == entity.Level && t.Area_ID == entity.Area_ID))
                        {
                            msg = "代理资格已占用";
                        }
                        
                    }

                    if (entity.Level == 2)
                    {
                        if (rep.IQueryable().Any(t => t.Type == 2 && t.Level == entity.Level && t.Area.PID == entity.Area.PID))
                        {
                            msg = "代理资格已占用";
                        }

                    }

                    if (entity.Level == 1)
                    {
                        if (rep.IQueryable().Any(t => t.Type == 2 && t.Level == entity.Level && t.Area.SupArea.ID == entity.Area.SupArea.ID))
                        {
                            msg = "代理资格已占用";
                        }
                    }
                }

                //rep.IQueryable().Where()
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }


        public string ChangeSupAgent(string keyValue, int newAgentID)
        {
            string msg = string.Empty;

            try
            {
                var entity = this.BaseRepository(DbName).FindEntity(int.Parse(keyValue));

                if(newAgentID!=0)
                {
                    var newagent = this.BaseRepository(DbName).FindEntity(newAgentID);

                    //!entity.IsValid.Value || 
                    if (!newagent.IsValid.Value)
                    {
                        msg = "上级代理信息无效";
                    }
                    else
                    {
                        msg = CheckAgentRule(entity, newagent);
                    }
                }

                

                if (string.IsNullOrEmpty(msg))
                {
                    entity.PID = newAgentID;
                    this.BaseRepository(DbName).Update(entity);
                }
                
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }


        public string CheckAgentRule(Sxc_AgentEntity current, Sxc_AgentEntity parent)
        {
            string msg = string.Empty;

            //初始的代理暂时均可修改
            if (current.Type == parent.Type|| current.Type == 0)
            {
                if (current.Type == 2)
                {
                    if (current.Level != 4)
                    {
                        msg = "非初级代理不可修改";
                    }
                    else
                    {
                        if (!CheckArea(current, parent))
                        {
                            msg = "地区不相符";
                        }
                    }
                }
            }
            else
            {
                msg = "代理类型不匹配";
            }


            if (msg.IsEmpty())
            {
                if (IsInTreeBySql(current.ID, parent.ID, "Sxc_Agent"))
                {
                    msg = "循环上下级";
                }
            }

            return msg;
        }

        public bool CheckArea(Sxc_AgentEntity current, Sxc_AgentEntity parent)
        {
            //没有地理信息
            if (current.Area_ID == null)
            {
                return true;
            }

            RepositoryFactory<Sxc_Base_AreaEntity> arearf = new RepositoryFactory<Sxc_Base_AreaEntity>();
            var arearep = arearf.BaseRepository(DbName);

            var carea3 = arearep.FindEntity(current.Area_ID);
            var carea2 = arearep.FindEntity(carea3.PID);
            var carea1 = arearep.FindEntity(carea2.PID);

            var parea3 = arearep.FindEntity(parent.Area_ID);
            var parea2 = arearep.FindEntity(parea3.PID);
            var parea1 = arearep.FindEntity(parea2.PID);

            if (parent.Level >= 3&&carea3.ID!=parea3.ID)
            {
                return false;
            }

            if (parent.Level == 2 && carea2.ID != parea2.ID)
            {
                return false;
            }

            if (parent.Level == 1 && carea1.ID != parea1.ID)
            {
                return false;
            }

            return true;

        }

        public bool IsInTreeBySql(int cid, int tid, string table, string key = "ID", string pkey = "PID")
        {
            var supsql = string.Format(@"WITH temp AS
                                        (
                                        SELECT {0} FROM {1}  WHERE {0} = {3}
                                        UNION ALL
                                        SELECT d.{0} FROM {1} AS d
                                        INNER JOIN temp ON d.{2} = temp.{0}
                                        )
                                        SELECT * FROM temp where {0}={4}", key, table, pkey, cid, tid);

            var data = this.BaseRepository(DbName).FindObject(supsql);

            return data != null;
        }


        public void AfterCreateNewAgent(Sxc_AgentEntity entity)
        {
            if (entity.Type == 2)
            {
                //归并下级
                if (entity.Level < 3)
                {
                    this.BaseRepository(DbName).ExecuteBySql(string.Format(@"UPDATE Sxc_Agent t SET PID={0} WHERE Area_ID IN (SELECT ID FROM Sxc_Base_Area WHERE PID={1})", entity.ID, entity.Area_ID));
                }

                //归属上级
                if (entity.Level < 4 && entity.Level > 1)
                {
                    var area = areaService.GetEntity(entity.Area_ID.Value.ToString());
                    var supagent = this.BaseRepository(DbName).FindEntity(t => t.Area_ID == area.PID&&t.IsValid.Value);

                    if (supagent != null)
                    {
                        entity.PID = supagent.ID;
                    }
                    else
                    {
                        entity.PID = null;
                    }


                    this.BaseRepository(DbName).Update(entity);
                }

                
            }
        }
    }
}
