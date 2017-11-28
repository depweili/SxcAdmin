using YaChH.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �����Ǵ�����Ϣ�Ƽ����޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2016-04-01 14:33
    /// �� ������Ʊ��Ϣ
    /// </summary>
    public class InvoiceMap : EntityTypeConfiguration<InvoiceEntity>
    {
        public InvoiceMap()
        {
            #region ������
            //��
            this.ToTable("Client_Invoice");
            //����
            this.HasKey(t => t.InvoiceId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
