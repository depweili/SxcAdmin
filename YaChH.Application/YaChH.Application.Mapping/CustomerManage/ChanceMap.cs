using YaChH.Application.Entity.CustomerManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.CustomerManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �����Ǵ�����Ϣ�Ƽ����޹�˾
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-12 10:50
    /// �� �����̻���Ϣ
    /// </summary>
    public class ChanceMap : EntityTypeConfiguration<ChanceEntity>
    {
        public ChanceMap()
        {
            #region ������
            //��
            this.ToTable("Client_Chance");
            //����
            this.HasKey(t => t.ChanceId);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
