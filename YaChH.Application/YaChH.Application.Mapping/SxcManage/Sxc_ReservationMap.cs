using YaChH.Application.Entity.SxcManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-12-25 09:17
    /// 描 述：Sxc_Reservation
    /// </summary>
    public class Sxc_ReservationMap : EntityTypeConfiguration<Sxc_ReservationEntity>
    {
        public Sxc_ReservationMap()
        {
            #region 表、主键
            //表
            this.ToTable("Sxc_Reservation");
            //主键
            this.HasKey(t => t.ID);
            #endregion

            #region 配置关系
            #endregion
        }
    }
}
