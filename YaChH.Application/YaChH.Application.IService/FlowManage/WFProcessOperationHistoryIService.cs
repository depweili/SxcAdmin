
using YaChH.Application.Entity.FlowManage;
namespace YaChH.Application.IService.FlowManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创建人：陈彬彬
    /// 日 期：2016.03.18 15:54
    /// 描 述：工作流实例操作记录表操作接口（支持：SqlServer）
    /// </summary>
    public interface WFProcessOperationHistoryIService
    {
        /// <summary>
        /// 保存或更新实体对象
        /// </summary>
        /// <param name="keyValue"></param>
        /// <param name="entity"></param>
        void SaveEntity(string keyValue, WFProcessOperationHistoryEntity entity);
    }
}
