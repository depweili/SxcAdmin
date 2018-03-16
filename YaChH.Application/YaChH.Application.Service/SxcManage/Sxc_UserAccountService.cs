using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Data.Repository;
using YaChH.Util.WebControl;
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
    /// 日 期：2018-03-16 15:59
    /// 描 述：Sxc_UserAccount
    /// </summary>
    public class Sxc_UserAccountService : RepositoryFactory<Sxc_UserAccountEntity>, Sxc_UserAccountIService
    {
        public string DbName = "SXC";
        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_UserAccountEntity> GetList(string queryJson)
        {
            return this.BaseRepository(DbName).IQueryable().ToList();
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_UserAccountEntity GetEntity(string keyValue)
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
        public void SaveForm(string keyValue, Sxc_UserAccountEntity entity)
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
        #endregion
    }
}
