using YaChH.Application.Entity.CustomerManage;
using YaChH.Util.WebControl;
using System.Collections.Generic;

namespace YaChH.Application.IService.CustomerManage
{
    /// <summary>
    /// �� �� 6.1
    /// Copyright (c) 2013-2016 �����Ǵ�����Ϣ�Ƽ����޹�˾
    /// �� �����ܴ���
    /// �� �ڣ�2016-03-12 10:50
    /// �� �����̻���Ϣ
    /// </summary>
    public interface IChanceService
    {
        #region ��ȡ����
        /// <summary>
        /// ��ȡ�б�
        /// </summary>
        /// <param name="pagination">��ҳ</param>
        /// <param name="queryJson">��ѯ����</param>
        /// <returns>���ط�ҳ�б�</returns>
        IEnumerable<ChanceEntity> GetPageList(Pagination pagination, string queryJson);
        /// <summary>
        /// ��ȡʵ��
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <returns></returns>
        ChanceEntity GetEntity(string keyValue);
        #endregion

        #region ��֤����
        /// <summary>
        /// �̻����Ʋ����ظ�
        /// </summary>
        /// <param name="fullName">����</param>
        /// <param name="keyValue">����</param>
        /// <returns></returns>
        bool ExistFullName(string fullName, string keyValue);
        #endregion

        #region �ύ����
        /// <summary>
        /// ɾ������
        /// </summary>
        /// <param name="keyValue">����</param>
        void RemoveForm(string keyValue);
        /// <summary>
        /// ��������������޸ģ�
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        /// <param name="entity">ʵ�����</param>
        /// <returns></returns>
        void SaveForm(string keyValue, ChanceEntity entity);
        /// <summary>
        /// �̻�����
        /// </summary>
        /// <param name="keyValue">����ֵ</param>
        void Invalid(string keyValue);
        /// <summary>
        /// �̻�ת���ͻ�
        /// </summary>
        /// <param name="keyValue">����</param>
        void ToCustomer(string keyValue);
        #endregion
    }
}
