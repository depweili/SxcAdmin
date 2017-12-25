using YaChH.Application.Entity.SxcManage;
using YaChH.Application.Busines.SxcManage;
using YaChH.Util;
using YaChH.Util.WebControl;
using System.Web.Mvc;
using YaChH.Application.Code;

namespace YaChH.Application.Web.Areas.SxcManage.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-07 11:21
    /// 描 述：Sxc_Agent
    /// </summary>
    public class Sxc_AgentController : MvcControllerBase
    {
        private Sxc_AgentBLL sxc_agentbll = new Sxc_AgentBLL();

        #region 视图功能
        /// <summary>
        /// 列表页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sxc_AgentIndex()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Sxc_AgentIndex2()
        {
            return View();
        }
        /// <summary>
        /// 表单页面
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Sxc_AgentForm()
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
            var data = sxc_agentbll.GetPageList(pagination, queryJson);
            var jsonData = new
            {
                rows = data,
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
            var data = sxc_agentbll.GetList(queryJson);
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
            var data = sxc_agentbll.GetEntity(keyValue);
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
            sxc_agentbll.RemoveForm(keyValue);
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
        public ActionResult SaveForm(string keyValue, Sxc_AgentEntity entity)
        {
            sxc_agentbll.SaveForm(keyValue, entity);
            return Success("操作成功。");
        }
        #endregion


        #region 我的成员

        /// <summary>
        /// 我的成员视图
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult MyMembers()
        {
            return View();
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表Json</returns>
        [HttpGet]
        public ActionResult GetMyMemeberJson()
        {
            var userId = OperatorProvider.Provider.Current().Account;
            var data = sxc_agentbll.GetMyMemberList(userId);
            //if (!string.IsNullOrEmpty(userId))
            //{
            //    data = data.TreeWhere(t => t..Contains(keyword), "DepartmentId");
            //}
            //var treeList = new List<TreeEntity>();
            //foreach (DepartmentEntity item in data)
            //{
            //    TreeEntity tree = new TreeEntity();
            //    bool hasChildren = data.Count(t => t.ParentId == item.DepartmentId) == 0 ? false : true;
            //    tree.id = item.DepartmentId;
            //    tree.text = item.FullName;
            //    tree.value = item.DepartmentId;
            //    tree.isexpand = true;
            //    tree.complete = true;
            //    tree.hasChildren = hasChildren;
            //    tree.parentId = item.ParentId;
            //    treeList.Add(tree);
            //}
            //return Content(treeList.TreeToJson());

        
            //var data = sxc_agentbll.GetMyMemberList(userId);
            return ToCamelCaseJson(data);
        }

        [HttpGet]
        public ActionResult GetMemeberJson(string agentID)
        {
            var data = sxc_agentbll.GetMemberList(agentID);
            return ToCamelCaseJson(data);
        }
            #endregion
    }
}
