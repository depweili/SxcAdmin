﻿using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Linq;

using YaChH.Util;

using YaChH.Util.Extension;
using System;
using System.Data.Entity;
using YaChH.Data.EF.Extension;
using YaChH.Data.EF.Tool;
using System.ComponentModel;

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
        Sxc_Base_AreaIService areaService = new Sxc_Base_AreaService();
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_CooperationEntity> GetPageList(Pagination pagination, string queryJson)
        {
            int total;
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

            PropertySortCondition[] ps = new[] { new PropertySortCondition("ID", ListSortDirection.Descending) };
            var query = this.BaseRepository(DbName).IQueryable().Include(t=>t.User.UserProfile).Where(expression, pagination.page, pagination.rows, out total, ps).AsEnumerable();
            pagination.records = total;
            return query;

            //return this.BaseRepository(DbName).FindList(expression, pagination);
            //return this.BaseRepository(DbName).FindList(pagination);
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

        public string AuditingApplication(string keyValue, int result)
        {
            string msg = string.Empty;
            var cooper = GetEntity(keyValue);
            if (result == 1)
            {
                var queryJson = "{" + string.Format(@"'condition': 'Area','keyword': '{0}'", cooper.AreaInfo) + "}";
                var area3 = areaService.QueryEntity(queryJson);
                Sxc_Base_AreaEntity area2 = null;
                Sxc_Base_AreaEntity area1 = null;

                Sxc_Base_AreaEntity area = null;

                if (area3 == null)
                {
                    msg = "地区未找到";
                }
                else
                {
                    area2 = areaService.GetEntity(area3.PID.Value.ToString());
                    area1 = areaService.GetEntity(area2.PID.Value.ToString());
                }

                if (msg.IsEmpty())
                {
                    if (cooper.Type == 1)//直辖市
                    {
                        area = area3;

                        if (area1.Type.Value != 1)
                        {
                            msg = "地区错误";
                        }
                    }
                    else
                    {
                        if (cooper.Type == 2)//非直辖市
                        {
                            if (area1.Type.Value == 1)
                            {
                                msg = "地区错误";
                            }
                            else
                            {
                                if (cooper.Level != null)
                                {
                                    switch (cooper.Level)
                                    {
                                        case 1:
                                            area = area1;
                                            break;
                                        case 2:
                                            area = area2;
                                            break;
                                        case 3:
                                            area = area3;
                                            break;
                                        default:
                                            area = area3;
                                            break;
                                    }
                                }
                            }
                        }
                        else//直属
                        {
                            area = area3;
                        }
                    }
                }

                    

                //重复
                //var areaName = "";
                //var areas = cooper.AreaInfo.Split(new char[] { ',' });
                //if (cooper.Type == 1)
                //{
                //    areaName = areas[0];
                //}
                //else if (cooper.Level != null)
                //{
                //    areaName = areas[cooper.Level.Value - 1];
                //}
                //var queryJson = "{" + string.Format(@"'condition': 'AreaName','keyword': '{0}'", areaName) + "}";
                //var area = areaService.GetEntity(queryJson);



                //if (cooper.UserID != null)
                if (msg.IsEmpty())
                {

                    var kv = cooper.UserID.Value.ToString();
                    var id = int.Parse(kv);
                    var entity = agentService.GetEntity1(kv);
                    //var entity = new Sxc_AgentEntity();
                    //var entity = agentService.Repository().IQueryable().Include(t => t.Area.SupArea).Single(t => t.ID == id);

                    //entity.ID = id;
                    entity.Level = cooper.Level.ToInt();
                    entity.Type = cooper.Type.ToInt();

                    //entity.Area = area;
                    entity.Area_ID = area.ID;
                    

                    //int midid = area.ID;
                    //entity.Area_ID = midid;


                    msg = agentService.CheckNewAgent(entity);

                    if (msg.IsEmpty())
                    {
                        entity.IsValid = true;
                        agentService.SaveForm(kv, entity);

                        cooper.State = result;
                        cooper.ProcessDetail = string.Format("审核通过【{0}】", DateTime.Now.ToChineseDateTimeString());
                    }
                    else
                    {
                        cooper.State = 2;
                        cooper.ProcessDetail = string.Format("审核失败【{0}】", msg);
                    }
                }
                else
                {
                    cooper.State = 2;
                    cooper.ProcessDetail = string.Format("审核失败【{0}】", msg);
                }
            }
            else
            {
                cooper.ProcessDetail = string.Format("拒绝申请【{0}】", DateTime.Now.ToChineseDateTimeString());
                cooper.State = result;

            }
            //保存加盟信息
            SaveForm(keyValue, cooper);

            return msg;
        }

        #endregion
    }
}
