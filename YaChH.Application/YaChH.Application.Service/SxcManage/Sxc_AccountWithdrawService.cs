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
using System.Data;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;

namespace YaChH.Application.Service.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-05 15:17
    /// 描 述：Sxc_AccountWithdraw
    /// </summary>
    public class Sxc_AccountWithdrawService : RepositoryFactory<Sxc_AccountWithdrawEntity>, Sxc_AccountWithdrawIService
    {
        private string DbName = "SXC";

        #region 获取数据
        public IRepository<Sxc_AccountWithdrawEntity> Repository()
        {
            return this.BaseRepository(DbName);
        }


        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_AccountWithdrawEntity> GetPageList1(Pagination pagination, string queryJson)
        {
            return this.BaseRepository(DbName).FindList(pagination);
        }

        public IEnumerable<Sxc_AccountWithdrawEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;

            var expression = LinqExtensions.True<Sxc_AccountWithdrawEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateTime >= startTime && t.CreateTime <= endTime);
            }
            //环节
            if (!queryParam["Step"].IsEmpty() && !queryParam["Step"].IsEmpty())
            {
                if (queryParam["Step"].ToString() == "2")
                {
                    expression = expression.And(t => t.State >= 2);
                }
                
            }

            //客户名称
            //if (!queryParam["UserName"].IsEmpty())
            //{
            //    string CustomerName = queryParam["UserName"].ToString();
            //    expression = expression.And(t => t.UserPayment.User.UserProfile.NickName.Contains(CustomerName) || t.UserPayment.User.UserProfile.RealName.Contains(CustomerName));
            //}

            //客户名称
            if (!queryParam["UserName"].IsEmpty())
            {
                string CustomerName = queryParam["UserName"].ToString();
                expression = expression.And(t => t.Name== CustomerName);
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
            var query = this.BaseRepository(DbName).IQueryable().Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        //IEnumerable<Sxc_AccountWithdrawEntity>
        public dynamic GetList(string queryJson)
        {
            //return this.BaseRepository(DbName).IQueryable().ToList();

            var expression = LinqExtensions.True<Sxc_AccountWithdrawEntity>();
            var queryParam = queryJson.ToJObject();
            //单据日期
            if (!queryParam["StartTime"].IsEmpty() && !queryParam["EndTime"].IsEmpty())
            {
                DateTime startTime = queryParam["StartTime"].ToDate();
                DateTime endTime = queryParam["EndTime"].ToDate().AddDays(1);
                expression = expression.And(t => t.CreateTime >= startTime && t.CreateTime <= endTime);
            }
            //环节
            if (!queryParam["Step"].IsEmpty() && !queryParam["Step"].IsEmpty())
            {
                if (queryParam["Step"].ToString() == "2")
                {
                    expression = expression.And(t => t.State >= 2);
                }
            }

            //客户名称
            if (!queryParam["UserName"].IsEmpty())
            {
                string CustomerName = queryParam["UserName"].ToString();
                expression = expression.And(t => t.Name == CustomerName);
            }

            var query = this.BaseRepository(DbName).IQueryable().Where(expression).OrderByDescending(t=>t.ID).Select(t=>new {
                t.AccountRecordID,
                t.Amount,
                t.BankCard,
                t.BankName,
                t.BranchBankName,
                t.CompleteTime,
                t.CreateTime,
                t.Memo,
                t.MobilePhone,
                t.Name,
                State=t.State==0? "申请中":(t.State==1? "驳回" : (t.State==2? "批准" : (t.State==3? "完成" : (t.State==4? "失败" : ""))))
            }).AsEnumerable();
            

            return query;

        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_AccountWithdrawEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Sxc_AccountWithdrawEntity entity)
        {
            IRepository db = new RepositoryFactory().BaseRepository(DbName).BeginTrans();
            try
            {
                if (!string.IsNullOrEmpty(keyValue))
                {
                    var wd = db.FindEntity<Sxc_AccountWithdrawEntity>(int.Parse(keyValue));
                    if (wd.State == 0|| wd.State == 2)
                    {
                        wd.Memo = entity.Memo;
                        wd.State = entity.State;

                        if (entity.State != 0|| entity.State != 2)
                        {
                            wd.CompleteTime = DateTime.Now;
                            var account = db.FindEntity<Sxc_UserAccountEntity>(wd.UserAccountID);

                            if (entity.State == 3)//成功
                            {
                                account.LockBalance = account.LockBalance - wd.Amount;
                                account.Cash = account.Cash + wd.Amount;
                            }

                            if (entity.State == 1|| entity.State == 4)//驳回和失败
                            {
                                account.LockBalance = account.LockBalance - wd.Amount;
                                account.Balance = account.Balance + wd.Amount;
                            }

                            db.Update(account);
                            
                        }
                        db.Update(wd);


                        //this.BaseRepository(DbName).Update(entity);
                    }
                    db.Commit();
                }
                else
                {
                    entity.Create();
                    this.BaseRepository(DbName).Insert(entity);
                }
            }
            catch (Exception)
            {
                db.Rollback();
                throw;
            }
            
            
        }
        #endregion

        public DataTable GetExportData(string queryJson)
        {
            try
            {
                var timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy-MM-dd HH:mm:ss" };
                var json = JsonConvert.SerializeObject(GetList(queryJson), timeConverter);

                //var json = GetList(queryJson).ToJson();

                //var data = json.ToTable();
                var data = JsonConvert.DeserializeObject<DataTable>(json);

                return data;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
    }
}
