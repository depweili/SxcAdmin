using YaChH.Application.Entity.SxcManage;
using YaChH.Application.Busines.SxcManage;
using YaChH.Util;
using YaChH.Util.WebControl;
using System.Web.Mvc;
using YaChH.Application.Cache;
using System.Collections.Generic;
using YaChH.Application.Entity.SxcManage.ViewModel;
using System.Linq;
using YaChH.Application.Code;

namespace YaChH.Application.Web.Areas.SxcManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-29 23:25
    /// 描 述：Sxc_Cooperation
    /// </summary>
    public class Sxc_CooperationController : MvcControllerBase
    {
        private Sxc_CooperationBLL sxc_cooperationbll = new Sxc_CooperationBLL();
        private DataItemCache dataItemCache = new DataItemCache();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sxc_CooperationIndex()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sxc_CooperationForm()
        {
            return View();
        }
        #endregion

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页参数</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表Json</returns>
        [HttpGet]
        public ActionResult GetPageListJson(Pagination pagination, string queryJson)
        {
            var watch = CommonHelper.TimerStart();
            var keyValues = dataItemCache.GetDataItemList();
            IEnumerable<Sxc_CooperationEntity> data = sxc_cooperationbll.GetPageList(pagination, queryJson);
            List<Sxc_CooperationModel> viewData = new List<Sxc_CooperationModel>();
            foreach(var d in data)
            {
                var type = d.Type.ToString();
                var level = d.Level.ToString();
                var tkey = "AgentType";
                var lkey = string.Format("AgentLevel{0}", type);
                var ln = keyValues.FirstOrDefault(r => r.EnCode == lkey&&r.ItemValue==level);
                var tn = keyValues.FirstOrDefault(r => r.EnCode == tkey&&r.ItemValue==type);
                Sxc_CooperationModel item = new Sxc_CooperationModel
                {
                    ID=d.ID,
                    Address=d.Address,
                    AreaID=d.AreaID,
                    CreateTime=d.CreateTime,
                    AgentAreaInfo=d.AgentAreaInfo,
                    AreaInfo=d.AreaInfo,
                    Level=d.Level,
                    LevelName=(ln==null?"":ln.ItemName),
                    Memo=d.Memo,
                    MobilePhone=d.MobilePhone,
                    Name=d.Name,
                    ProcessDetail=d.ProcessDetail,
                    Type=d.Type,
                    UserID=d.UserID,
                    TypeName = (tn == null ? "" : tn.ItemName),
                };
                viewData.Add(item);
            }
            var jsonData = new
            {
                rows = viewData,
                total = pagination.total,
                page = pagination.page,
                records = pagination.records,
                costtime = CommonHelper.TimerEnd(watch)
            };
            return ToJsonResult(jsonData);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetListJson(string queryJson)
        {
            var data = sxc_cooperationbll.GetList(queryJson);
            return ToJsonResult(data);
        }
        /// <summary>
        /// 获取实体 
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns>返回对象Json</returns>
        [HttpGet]
        public ActionResult GetFormJson(string keyValue)
        {
            var data = sxc_cooperationbll.GetEntity(keyValue);
            return ToJsonResult(data);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult RemoveForm(string keyValue)
        {
            sxc_cooperationbll.RemoveForm(keyValue);
            return Success("删除成功。");
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AjaxOnly]
        public ActionResult SaveForm(string keyValue, Sxc_CooperationEntity entity)
        {
            sxc_cooperationbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        /// <summary>
        /// 执行申请
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult ApproveApplication(string keyValue)
        {
            sxc_cooperationbll.AuditingApplication(keyValue,1);
            return Success("申请成功通过！");
        }

        /// <summary>
        /// 拒绝申请
        /// </summary>
        /// <param name="keyValue"></param>
        /// <returns></returns>
        [HttpPost]
        [AjaxOnly]
        [HandlerAuthorize(PermissionMode.Ignore)]
        public ActionResult RefuseApplication(string keyValue)
        {
            sxc_cooperationbll.AuditingApplication(keyValue, -1);
            return Success("已经拒绝！");
        }
        #endregion
    }
}
