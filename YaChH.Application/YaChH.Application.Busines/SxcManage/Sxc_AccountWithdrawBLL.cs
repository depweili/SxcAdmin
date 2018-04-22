using YaChH.Application.Entity.SxcManage;
using YaChH.Application.IService.SxcManage;
using YaChH.Application.Service.SxcManage;
using YaChH.Util.WebControl;
using System.Collections.Generic;
using System;
using YaChH.Util.Offices;
using System.Data;

namespace YaChH.Application.Busines.SxcManage
{
    /// <summary>
    /// 版 本 6.1
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2018-03-05 15:17
    /// 描 述：Sxc_AccountWithdraw
    /// </summary>
    public class Sxc_AccountWithdrawBLL
    {
        private Sxc_AccountWithdrawIService service = new Sxc_AccountWithdrawService();

        #region 获取数据
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="pagination">分页</param>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回分页列表</returns>
        public IEnumerable<Sxc_AccountWithdrawEntity> GetPageList(Pagination pagination, string queryJson)
        {
            return service.GetPageList(pagination, queryJson);
        }
        /// <summary>
        /// 获取列表
        /// </summary>
        /// <param name="queryJson">查询参数</param>
        /// <returns>返回列表</returns>
        public IEnumerable<Sxc_AccountWithdrawEntity> GetList(string queryJson)
        {
            return service.GetList(queryJson);
        }
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <returns></returns>
        public Sxc_AccountWithdrawEntity GetEntity(string keyValue)
        {
            return service.GetEntity(keyValue);
        }
        #endregion

        #region 提交数据
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="keyValue">主键</param>
        public void RemoveForm(string keyValue)
        {
            try
            {
                service.RemoveForm(keyValue);
            }
            catch (Exception)
            {
                throw;
            }
        }
        /// <summary>
        /// 保存表单（新增、修改）
        /// </summary>
        /// <param name="keyValue">主键值</param>
        /// <param name="entity">实体对象</param>
        /// <returns></returns>
        public void SaveForm(string keyValue, Sxc_AccountWithdrawEntity entity)
        {
            try
            {
                service.SaveForm(keyValue, entity);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public void GetExportData(string queryJson)
        {
            try
            {
                DataTable exportTable = service.GetExportData(queryJson);
                //设置导出格式
                ExcelConfig excelconfig = new ExcelConfig();
                excelconfig.Title = "提现导出";
                excelconfig.TitleFont = "微软雅黑";
                excelconfig.TitlePoint = 25;
                excelconfig.FileName = "提现导出"+DateTime.Now.ToString("yyyyMMddHHmmss")+".xls";
                excelconfig.IsAllSizeColumn = true;
                //每一列的设置,没有设置的列信息，系统将按datatable中的列名导出
                List<ColumnEntity> listColumnEntity = new List<ColumnEntity>();
                excelconfig.ColumnEntity = listColumnEntity;
                ColumnEntity columnentity = new ColumnEntity();
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Name", ExcelColumn = "姓名" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BankCard", ExcelColumn = "卡号" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BankName", ExcelColumn = "开户行" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "BranchBankName", ExcelColumn = "开户支行" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Amount", ExcelColumn = "提现金额" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "MobilePhone", ExcelColumn = "手机" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CreateTime", ExcelColumn = "申请时间" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "CompleteTime", ExcelColumn = "完成时间" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "State", ExcelColumn = "状态" });
                excelconfig.ColumnEntity.Add(new ColumnEntity() { Column = "Memo", ExcelColumn = "备注" });
                //调用导出方法
                ExcelHelper.ExcelDownload(exportTable, excelconfig);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        #endregion
    }
}
