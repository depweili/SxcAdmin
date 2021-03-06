﻿using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System;
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
    /// 日 期：2017-11-27 02:06
    /// 描 述：Sxc_UserPayment
    /// </summary>
    public class Sxc_UserPaymentService : RepositoryFactory, Sxc_UserPaymentIService
    {
        public string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_UserPaymentEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Sxc_UserPaymentEntity>();
            var queryParam = queryJson.ToJObject();
            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "UserID":              //用户名
                        //expression = expression.And(t => t.UserID.Contains(keyword));
                        break;
                    case "PayItemID":              //项目
                        //expression = expression.And(t => t.PayItemID.Contains(keyword));
                        break;
                    case "CreateTime":              //创建时间
                        //expression = expression.And(t => t.CreateTime.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }

            //var list= this.BaseRepository(DbName).FindList(expression, pagination);

            return this.BaseRepository(DbName).FindList(expression, pagination);
        }

        public IEnumerable<Sxc_UserPaymentEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_UserPaymentEntity>();
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
                expression = expression.And(t => t.User.UserProfile.NickName.Contains(CustomerName) || t.User.UserProfile.RealName.Contains(CustomerName));
            }

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Descending) };
            //Include("Agent").Include("UserPayment").  Where(x=>true  
            //Include(t=>t.UserPayment.User.UserProfile).
            var query = this.BaseRepository(DbName).IQueryable<Sxc_UserPaymentEntity>().Include(t => t.User.UserProfile).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }


        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserPaymentEntity GetEntity(string keyValue)
        {
            return this.BaseRepository(DbName).FindEntity<Sxc_UserPaymentEntity>(int.Parse(keyValue));
        }
        /// <summary>
        /// 获取子表详细信息
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public IEnumerable<Sxc_CommissionRecordEntity> GetDetails(string keyValue)
        {
            //return this.BaseRepository(DbName).FindList<Sxc_CommissionRecordEntity>("select * from Sxc_CommissionRecord where UserPaymentID=" + keyValue + "");

            var payid = int.Parse(keyValue);
            var query = this.BaseRepository(DbName).IQueryable<Sxc_CommissionRecordEntity>().Include(t => t.Agent.User.UserProfile).Where(t => t.UserPaymentID == payid).OrderBy(t => t.ID).AsEnumerable();

            return query;
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            IRepository db = new RepositoryFactory().BaseRepository(DbName).BeginTrans();
            try
            {
                db.Delete<Sxc_UserPaymentEntity>(keyValue);
                db.Delete<Sxc_CommissionRecordEntity>(t => t.ID.Equals(keyValue));
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sxc_UserPaymentEntity entity, List<Sxc_CommissionRecordEntity> entryList)
        {
            IRepository db = this.BaseRepository(DbName).BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    //主表
                    entity.Modify(keyValue);
                    db.Update(entity);
                    //明细
                    db.Delete<Sxc_CommissionRecordEntity>(t => t.ID.Equals(keyValue));
                    foreach (Sxc_CommissionRecordEntity item in entryList)
                    {
                        item.Create();
                        item.ID = entity.ID;
                        db.Insert(item);
                    }
                }
                else
                {
                    //主表
                    entity.Create();
                    db.Insert(entity);
                    //明细
                    foreach (Sxc_CommissionRecordEntity item in entryList)
                    {
                        item.Create();
                        item.ID = entity.ID;
                        db.Insert(item);
                    }
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        public void SaveForm(string keyValue, Sxc_UserPaymentEntity entity)
        {
            if (!string.IsNullOrEmpty(keyValue))
            {
                entity.Modify(keyValue);
                this.BaseRepository(DbName).Update(entity);
            }
            else
            {
                entity.Create();
                entity.PaySN = CommonHelper.CreateTimeRandomNo();
                entity.DistrAmount = entity.Amount;
                this.BaseRepository(DbName).Insert(entity);
            }
        }

        #endregion

        #region 返佣
        public void DistrCommission(string ids)
        {
            var arrids = ids.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);

            IRepository db = new RepositoryFactory().BaseRepository(DbName).BeginTrans();
            try
            {
                var list = db.FindList<Sxc_UserPaymentEntity>(t => arrids.Contains(t.ID.ToString()) && t.State == 0);
                foreach (var item in list)
                {
                    DistrCommission(db, item);

                    db.Update(item);
                }
                db.Commit();
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
        }


        public void DistrCommission(IRepository db, Sxc_UserPaymentEntity entity)
        {
            entity.State = 1;
            entity.DistrTime = DateTime.Now;

            var agent = db.FindEntity<Sxc_AgentEntity>(t => t.ID == entity.UserID);

            //&& agent.IsValid.Value 不需要本身是有效代理
            if (agent.Type == 1)
            {
                if (agent.PID.HasValue)
                {
                    var agent1 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent.PID);

                    var per1 = GetZxsPercent(agent1.Level);

                    var amount1 = entity.DistrAmount * per1;

                    entity.Commission += amount1;
                    agent1.Commission += amount1;

                    var record1 = new Sxc_CommissionRecordEntity
                    {
                        AgentID = agent1.ID,
                        CreateTime = DateTime.Now,
                        State = 0,
                        UserPaymentID = entity.ID,
                        Commission = amount1
                    };

                    db.Insert(record1);
                    db.Update(agent1);

                    if (agent1.PID.HasValue)
                    {
                        var agent2 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent1.PID);

                        var per2 = GetZxsPercent(agent2.Level);

                        if (per2 > per1)
                        {
                            per2 = per2 - per1;
                        }
                        else
                        {
                            per2 = 0.05m;
                        }

                        var amount2 = entity.DistrAmount * per2;

                        entity.Commission += amount2;
                        agent2.Commission += amount2;

                        var record2 = new Sxc_CommissionRecordEntity
                        {
                            AgentID = agent2.ID,
                            CreateTime = DateTime.Now,
                            State = 0,
                            UserPaymentID = entity.ID,
                            Commission = amount2
                        };

                        db.Insert(record2);
                        db.Update(agent2);
                    }
                }
            }

            //&& agent.IsValid.Value
            if (agent.Type == 2 )
            {
                if (agent.PID.HasValue)
                {
                    var agent1 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent.PID);

                    //var amount1 = GetFzxsCommission(agent1.Level);
                    var amount1 = entity.DistrAmount * GetFzxsCommission(agent1.Level);

                    entity.Commission += amount1;
                    agent1.Commission+= amount1;

                    var record1 = new Sxc_CommissionRecordEntity
                    {
                        AgentID = agent1.ID,
                        CreateTime = DateTime.Now,
                        State = 0,
                        UserPaymentID = entity.ID,
                        Commission = amount1
                    };

                    db.Insert(record1);
                    db.Update(agent1);

                    if (agent1.PID.HasValue)
                    {
                        var agent2 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent1.PID);

                        //var amount2 = 1000;
                        var amount2 = entity.DistrAmount * 0.05m;

                        entity.Commission += amount2;
                        agent2.Commission += amount2;

                        var record2 = new Sxc_CommissionRecordEntity
                        {
                            AgentID = agent2.ID,
                            CreateTime = DateTime.Now,
                            State = 0,
                            UserPaymentID = entity.ID,
                            Commission = amount2
                        };

                        db.Insert(record2);
                        db.Update(agent2);

                        List<Sxc_AgentEntity> SupAgents = new List<Sxc_AgentEntity>();

                        GetSupAgents(db,agent2, SupAgents);

                        foreach (var sup in SupAgents)
                        {
                            //var amountsup = 1000;
                            var amountsup = entity.DistrAmount * 0.05m;

                            entity.Commission += amountsup;
                            sup.Commission += amountsup;

                            var recordsup = new Sxc_CommissionRecordEntity
                            {
                                AgentID = sup.ID,
                                CreateTime = DateTime.Now,
                                State = 0,
                                UserPaymentID = entity.ID,
                                Commission = amountsup
                            };

                            db.Insert(recordsup);
                            db.Update(sup);
                        }
                    }
                }
            }
            //直属
            if (agent.Type == 3)
            {
                if (agent.PID.HasValue)
                {
                    var agent1 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent.PID);

                    //公司直招
                    if (agent1.Level < 4)
                    {
                        var distrAmount = entity.DistrAmount * 0.3m;
                        var amount1 = distrAmount * GetFzxsCommission(agent1.Level);

                        entity.Commission += amount1;
                        agent1.Commission += amount1;

                        var record1 = new Sxc_CommissionRecordEntity
                        {
                            AgentID = agent1.ID,
                            CreateTime = DateTime.Now,
                            State = 0,
                            UserPaymentID = entity.ID,
                            Commission = amount1
                        };

                        db.Insert(record1);
                        db.Update(agent1);

                        List<Sxc_AgentEntity> SupAgents = new List<Sxc_AgentEntity>();
                        GetSupAgents(db, agent1, SupAgents);

                        foreach (var sup in SupAgents)
                        {
                            var amountsup = distrAmount * 0.15m;
                            entity.Commission += amountsup;
                            sup.Commission += amountsup;

                            var recordsup = new Sxc_CommissionRecordEntity
                            {
                                AgentID = sup.ID,
                                CreateTime = DateTime.Now,
                                State = 0,
                                UserPaymentID = entity.ID,
                                Commission = amountsup
                            };

                            db.Insert(recordsup);
                            db.Update(sup);
                        }
                    }
                    //学员发展
                    //level=4
                    else
                    {
                        var amount1 = entity.DistrAmount * GetFzxsCommission(agent1.Level);

                        entity.Commission += amount1;
                        agent1.Commission += amount1;

                        var record1 = new Sxc_CommissionRecordEntity
                        {
                            AgentID = agent1.ID,
                            CreateTime = DateTime.Now,
                            State = 0,
                            UserPaymentID = entity.ID,
                            Commission = amount1
                        };

                        db.Insert(record1);
                        db.Update(agent1);

                        if (agent1.PID.HasValue)
                        {
                            var agent2 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent1.PID);
                            
                            var amount2 = entity.DistrAmount * 0.05m;

                            entity.Commission += amount2;
                            agent2.Commission += amount2;

                            var record2 = new Sxc_CommissionRecordEntity
                            {
                                AgentID = agent2.ID,
                                CreateTime = DateTime.Now,
                                State = 0,
                                UserPaymentID = entity.ID,
                                Commission = amount2
                            };

                            db.Insert(record2);
                            db.Update(agent2);

                        }
                    }
                }
            }

            entity.FinalAmount = entity.Amount - entity.Commission;
        }

        private void GetSupAgents(IRepository db,Sxc_AgentEntity agent2, List<Sxc_AgentEntity> supAgents)
        {
            if (agent2.PID.HasValue)
            {
                var parentAgent= db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent2.PID.Value);

                //if (agent2.ParentAgent.Level > agent2.Level)
                if (parentAgent.Level > agent2.Level)
                {
                    supAgents.Add(parentAgent);
                }

                GetSupAgents(db, parentAgent, supAgents);
            }
        }

        public decimal GetZxsPercent(int level)
        {
            decimal per = 0.0m;

            switch (level)
            {
                case 1:
                    per = 0.4m;
                    break;
                case 2:
                    per = 0.35m;
                    break;
                case 3:
                    per = 0.3m;
                    break;
                case 4:
                    per = 0.25m;
                    break;
                default:
                    break;
            }
            return per;
        }


        public decimal GetFzxsCommission(int level)
        {
            decimal per = 0;

            switch (level)
            {
                case 1:
                    //per = 8000;
                    per = 0.4m;
                    break;
                case 2:
                    //per = 7000;
                    per = 0.35m;
                    break;
                case 3:
                    //per = 6000;
                    per = 0.3m;
                    break;
                case 4:
                    //per = 5000;
                    per = 0.25m;
                    break;
                default:
                    break;
            }
            return per;
        }


        public decimal GetCompanyCommission(int level)
        {
            decimal per = 0;

            switch (level)
            {
                case 1:
                    //per = 8000;
                    per = 1m;
                    break;
                case 2:
                    //per = 7000;
                    per = 0.85m;
                    break;
                case 3:
                    //per = 6000;
                    per = 0.7m;
                    break;
                case 4:
                    //per = 5000;
                    per = 0.25m;
                    break;
                default:
                    break;
            }
            return per;
        }

        #endregion

    }
}
