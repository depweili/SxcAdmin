using System;
using YaChH.Application.Code;

namespace YaChH.Application.Entity.SxcManage
{
    /// <summary>
    /// 版 本
    /// Copyright (c) 2013-2016 北京亚春华信息科技有限公司
    /// 创 建：超级管理员
    /// 日 期：2017-11-13 21:12
    /// 描 述：Sxc_Teacher
    /// </summary>
    public class Sxc_TeacherEntity : BaseEntity
    {
        #region 实体成员
        /// <summary>
        /// ID
        /// </summary>
        /// <returns></returns>
        public int? ID { get; set; }
        /// <summary>
        /// Name
        /// </summary>
        /// <returns></returns>
        public string Name { get; set; }
        /// <summary>
        /// IdentifyCode
        /// </summary>
        /// <returns></returns>
        public string IdentifyCode { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        /// <returns></returns>
        public int? Type { get; set; }
        /// <summary>
        /// Title
        /// </summary>
        /// <returns></returns>
        public string Title { get; set; }
        /// <summary>
        /// Introduction
        /// </summary>
        /// <returns></returns>
        public string Introduction { get; set; }
        /// <summary>
        /// Pic
        /// </summary>
        /// <returns></returns>
        public string Pic { get; set; }
        /// <summary>
        /// Order
        /// </summary>
        /// <returns></returns>
        public int? Order { get; set; }
        /// <summary>
        /// IsValid
        /// </summary>
        /// <returns></returns>
        public bool? IsValid { get; set; }
        /// <summary>
        /// ArticleID
        /// </summary>
        /// <returns></returns>
        public int? ArticleID { get; set; }
        /// <summary>
        /// Character
        /// </summary>
        /// <returns></returns>
        public string Character { get; set; }
        #endregion

        #region 扩展操作
        /// <summary>
        /// 新增调用
        /// </summary>
        public override void Create()
        {
            this.ID = 0;//Guid.NewGuid().ToString();
                                            }
        /// <summary>
        /// 编辑调用
        /// </summary>
        /// <param name="keyValue"></param>
        public override void Modify(string keyValue)
        {
            this.ID = int.Parse(keyValue);
        }
        #endregion
    }
}