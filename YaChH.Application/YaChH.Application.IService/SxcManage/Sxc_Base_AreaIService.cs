using YaChH.Application.Entity.SxcManage;
using YaChH.Util.WebControl;
using System.Collections.Generic;

namespace YaChH.Application.IService.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-03 20:43
    /// 描 述：Sxc_Base_Area
    /// </summary>
    public interface Sxc_Base_AreaIService
    {
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        IEnumerable<Sxc_Base_AreaEntity> GetList(string queryJson);
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        Sxc_Base_AreaEntity GetEntity(string keyValue);

        Sxc_Base_AreaEntity QueryEntity(string queryJson);
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        void SaveForm(string keyValue, Sxc_Base_AreaEntity entity);
        
        #endregion
    }
}
