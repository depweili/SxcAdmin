using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System;
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
        public IEnumerable<Sxc_UserPaymentEntity> GetPageList(Pagination pagination, string queryJson)
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
            return this.BaseRepository(DbName).FindList<Sxc_CommissionRecordEntity>("select * from Sxc_CommissionRecord where UserPaymentID=" + keyValue + "");
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

            if (agent.Type == 1 && agent.IsValid.Value)
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
                    }
                }
            }

            if (agent.Type == 2 && agent.IsValid.Value)
            {
                if (agent.PID.HasValue)
                {
                    var agent1 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent.PID);

                    var amount1 = GetFzxsCommission(agent1.Level);

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

                    if (agent1.PID.HasValue)
                    {
                        var agent2 = db.FindEntity<Sxc_AgentEntity>(t => t.ID == agent1.PID);

                        var amount2 = 1000;

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

                        List<Sxc_AgentEntity> SupAgents = new List<Sxc_AgentEntity>();

                        GetSupAgents(agent2, SupAgents);

                        foreach (var sup in SupAgents)
                        {
                            var amountsup = 1000;

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
                        }
                    }
                }
            }

            entity.FinalAmount = entity.Amount - entity.Commission;
        }

        private void GetSupAgents(Sxc_AgentEntity agent2, List<Sxc_AgentEntity> supAgents)
        {
            if (agent2.PID.HasValue)
            {
                if (agent2.ParentAgent.Level > agent2.Level)
                {
                    supAgents.Add(agent2.ParentAgent);
                }

                GetSupAgents(agent2.ParentAgent, supAgents);
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
                    per = 8000;
                    break;
                case 2:
                    per = 7000;
                    break;
                case 3:
                    per = 6000;
                    break;
                case 4:
                    per = 5000;
                    break;
                default:
                    break;
            }
            return per;
        }

        #endregion

    }
}
