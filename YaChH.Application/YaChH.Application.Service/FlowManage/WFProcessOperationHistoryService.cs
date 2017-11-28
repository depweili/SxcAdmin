using YaChH.Application.Entity.FlowManage;
using YaChH.Application.IService.FlowManage;
using YaChH.Data.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaChH.Application.Service.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例操作记录表操作（支持：SqlServer）
    /// </summary>
    public class WFProcessOperationHistoryService : RepositoryFactory, WFProcessOperationHistoryIService
    {
        #region 获取数据


        #endregion

        #region 提交数据
        /// <summary>
        /// 保存或更新实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        public void SaveEntity(string keyValue,WFProcessOperationHistoryEntity entity)
        {
            try
            {
                if(string.IsNullOrEmpty(keyValue))
                {
                    entity.Create();
                    this.BaseRepository().Insert(entity);
                }
                else
                {
                    entity.Modify(keyValue);
                    this.BaseRepository().Update(entity);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}
