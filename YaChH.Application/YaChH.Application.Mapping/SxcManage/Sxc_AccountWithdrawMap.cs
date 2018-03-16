using YaChH.Application.Entity.SxcManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-05 15:17
    /// 描 述：Sxc_AccountWithdraw
    /// </summary>
    public class Sxc_AccountWithdrawMap : EntityTypeConfiguration<Sxc_AccountWithdrawEntity>
    {
        public Sxc_AccountWithdrawMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sxc_AccountWithdraw");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
