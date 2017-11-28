using YaChH.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �����Ǵ�����Ϣ�Ƽ����޹�˾
    /// �� �����ܴ���
    /// �� �ڣ�2016-04-28 16:48
    /// �� �����ֽ����
    /// </summary>
    public class CashBalanceMap : EntityTypeConfiguration<CashBalanceEntity>
    {
        public CashBalanceMap()
        {
            #region ������
            //��
            this.ToTable("Client_CashBalance");
            //����
            this.HasKey(t => t.CashBalanceId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
