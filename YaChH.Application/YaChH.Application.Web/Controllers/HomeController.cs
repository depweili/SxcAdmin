﻿using YaChH.Application.Busines;
using YaChH.Application.Busines.BaseManage;
using YaChH.Application.Busines.SystemManage;
using YaChH.Application.Code;
using YaChH.Application.Entity;
using YaChH.Application.Entity.SystemManage;
using YaChH.Util;
using YaChH.Util.Attributes;
using YaChH.Util.Log;
using YaChH.Util.Offices;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YaChH.Application.Busines.PublicInfoManage;
using YaChH.Util.WebControl;

namespace YaChH.Application.Web.Controllers
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创建人：佘赐雄
    /// 日 期：2015.09.01 13:32
    /// 描 述：系统首页
    /// </summary>
    [HandlerLogin(LoginMode.Enforce)]
    public class HomeController : Controller
    {
        UserBLL user = new UserBLL();
        DepartmentBLL department = new DepartmentBLL();
        private NoticeBLL noticeBLL = new NoticeBLL();
        private NewsBLL newsBLL = new NewsBLL();
        #region 视图功能
        /// <summary>
        /// 后台框架页
        /// </summary>
        /// <returns></returns>
        public ActionResult AdminDefault()
        {

            //Pagination pagination = new Pagination
            //{
            //    page = 1,
            //    rows = 30,

            //};
            //var queryJson = "";
            //var data = newsBLL.GetPageList(pagination, queryJson);
            //var data1 = noticeBLL.GetPageList(pagination, queryJson);
            return View();
        }
        public ActionResult AdminLTE()
        {
            return View();
        }
        public ActionResult AdminWindos()
        {
            return View();
        }
        public ActionResult AdminPretty()
        {
            return View();
        }
        public ActionResult AdminDefaultDesktop()
        {
            return View();
        }
        public ActionResult AdminLTEDesktop()
        {
            return View();
        }
        public ActionResult AdminWindosDesktop()
        {
            return View();
        }
        public ActionResult AdminPrettyDesktop()
        {
            return View();
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 访问功能
        /// </summary>
        /// <param name="moduleId">功能Id</param>
        /// <param name="moduleName">功能模块</param>
        /// <param name="moduleUrl">访问路径</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult VisitModule(string moduleId, string moduleName, string moduleUrl)
        {
            LogEntity logEntity = new LogEntity();
            logEntity.CategoryId = 2;
            logEntity.OperateTypeId = ((int)OperationType.Visit).ToString();
            logEntity.OperateType = EnumAttribute.GetDescription(OperationType.Visit);
            logEntity.OperateAccount = OperatorProvider.Provider.Current().Account;
            logEntity.OperateUserId = OperatorProvider.Provider.Current().UserId;
            logEntity.ModuleId = moduleId;
            logEntity.Module = moduleName;
            logEntity.ExecuteResult = 1;
            logEntity.ExecuteResultJson = "访问地址：" + moduleUrl;
            logEntity.WriteLog();
            return Content(moduleId);
        }
        /// <summary>
        /// 离开功能
        /// </summary>
        /// <param name="moduleId">功能模块Id</param>
        /// <returns></returns>
        public ActionResult LeaveModule(string moduleId)
        {
            return null;
        }
        #endregion
    }
}
