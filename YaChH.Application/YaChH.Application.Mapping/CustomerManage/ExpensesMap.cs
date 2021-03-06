using YaChH.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.CustomerManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：佘赐雄
    /// 日 期：2016-04-20 11:23
    /// 描 述：费用支出
    /// </summary>
    public class ExpensesMap : EntityTypeConfiguration<ExpensesEntity>
    {
        public ExpensesMap()
        {
            #region 表、主键
            //表
            this.ToTable("Client_Expenses");
            //主键
            this.HasKey(t => t.ExpensesId);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
