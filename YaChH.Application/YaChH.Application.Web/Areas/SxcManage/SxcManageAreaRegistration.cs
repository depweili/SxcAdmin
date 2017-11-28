using System.Web.Mvc;

namespace LeaRun.Application.Web.Areas.SxcManage
{
    public class SxcManageAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "SxcManage";
            }
        }

        //public override void RegisterArea(AreaRegistrationContext context) 
        //{
        //    context.MapRoute(
        //        "SxcManage_default",
        //        "SxcManage/{controller}/{action}/{id}",
        //        new { action = "Index", id = UrlParameter.Optional }
        //    );
        //}


        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
              this.AreaName + "_Default",
              this.AreaName + "/{controller}/{action}/{id}",
              new { area = this.AreaName, controller = "Home", action = "Index", id = UrlParameter.Optional },
              new string[] { "YaChH.Application.Web.Areas." + this.AreaName + ".Controllers" }
            );
        }
    }
}