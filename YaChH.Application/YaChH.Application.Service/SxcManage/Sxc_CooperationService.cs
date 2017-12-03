﻿using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using System;

namespace YaChH.Application.Service.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-29 23:25
    /// 描 述：Sxc_Cooperation
    /// </summary>
    public class Sxc_CooperationService : RepositoryFactory<Sxc_CooperationEntity>, Sxc_CooperationIService
    {
        public string DbName = "SXC";
        Sxc_AgentIService agentService = new Sxc_AgentService();
        private Sxc_Base_AreaIService areaService = new Sxc_Base_AreaService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_CooperationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            var expression = LinqExtensions.True<Sxc_CooperationEntity>();
            var queryParam = queryJson.ToJObject();


            //查询条件
            if (!queryParam["condition"].IsEmpty() && !queryParam["keyword"].IsEmpty())
            {
                string condition = queryParam["condition"].ToString();
                string keyword = queryParam["keyword"].ToString();
                switch (condition)
                {
                    case "MobilePhone":           
                        expression = expression.And(t => t.MobilePhone.Contains(keyword));
                        break;
                    case "Name":        
                        expression = expression.And(t => t.Name.Contains(keyword));
                        break;
                    default:
                        break;
                }
            }       
           
            return this.BaseRepository(DbName).FindList(expression, pagination);

           // return this.BaseRepository(DbName).FindList(pagination);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_CooperationEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_CooperationEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Sxc_CooperationEntity entity)
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

        public void AuditingApplication(string keyValue, int result)
        {
            var cooper = GetEntity(keyValue);
            if (result == 1)
            {
                var areaName="";
                var areas = cooper.AreaInfo.Split(new char[] { ',' });
                if (cooper.Type == 1)
                {
                    areaName = areas[0];
                }
                else if (cooper.Level != null)
                {
                    areaName = areas[cooper.Level.Value - 1];
                }
                var queryJson = "{" + string.Format(@"'condition': 'AreaName','keyword': '{0}'", areaName) + "}";        
               var area= areaService.GetEntity(queryJson);
                cooper.State = result;
                
                cooper.ProcessDetail = string.Format("审核通过【{0}】", DateTime.Now.ToChineseDateTimeString());
                if (cooper.UserID != null)
                {
                   
                    var kv = cooper.UserID.Value.ToString();
                    var entity = agentService.GetEntity(kv);
                    entity.Level = cooper.Level.ToInt();
                    entity.Type = cooper.Type.ToInt();
                    entity.Area_ID = area.ID;
                    agentService.SaveForm(kv, entity);

                }
            }
            else
            {
                cooper.ProcessDetail = string.Format("拒绝申请【{0}】", DateTime.Now.ToChineseDateTimeString());
                cooper.State = result;

            }
            SaveForm(keyValue, cooper);
            
        }
        #endregion
    }
}
