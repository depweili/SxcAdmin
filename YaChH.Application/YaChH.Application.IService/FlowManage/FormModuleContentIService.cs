﻿using YaChH.Application.Entity.FlowManage;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System.Data;

namespace YaChH.Application.IService.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-04-23 21:30
    /// 描 述：表单模板表
    /// </summary>
    public interface FormModuleContentIService
    {
        #region 获取数据
       
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        FormModuleContentEntity GetEntity(string frmId, string version);
        FormModuleContentEntity GetEntity(string keyValue);
        IEnumerable<FormModuleContentEntity> GetEntityList(string frmId);
        DataTable GetTableList(string frmId);
        #endregion

        
    }
}
