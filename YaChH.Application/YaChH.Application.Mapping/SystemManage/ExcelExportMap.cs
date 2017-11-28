using YaChH.Application.Entity.SystemManage;
using System.Data.Entity.ModelConfiguration;

namespace YaChH.Application.Mapping.SystemManage
{
    /// <summary>
    /// �� ��
    /// Copyright (c) 2013-2016 �����Ǵ�����Ϣ�Ƽ����޹�˾
    /// �� ������������Ա
    /// �� �ڣ�2017-04-17 12:09
    /// �� ����Excel����
    /// </summary>
    public class ExcelExportMap : EntityTypeConfiguration<ExcelExportEntity>
    {
        public ExcelExportMap()
        {
            #region ������
            //��
            this.ToTable("Base_ExcelExport");
            //����
            this.HasKey(t => t.F_Id);
            #endregion

            #region ���ù�ϵ
            #endregion
        }
    }
}
