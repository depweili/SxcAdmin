using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YaChH.Data.EF.Tool
{
    public static class Function
    {
        //db first
        public static string GetKeyPropertyForDBFirst(Type entityType)
        {
            foreach (var prop in entityType.GetProperties())
            {
                var attr = prop.GetCustomAttributes(typeof(EdmScalarPropertyAttribute), false).FirstOrDefault()
                    as EdmScalarPropertyAttribute;
                if (attr != null && attr.EntityKeyProperty)
                    return prop.Name;
            }
            return null;
        }


        public static string GetKeyProperty(Type entityType)
        {
            foreach (var prop in entityType.GetProperties())
            {
                if (prop.GetCustomAttributes(false).Where(att => att.GetType().Name == "KeyAttribute").FirstOrDefault() != null)
                {
                    return prop.Name;
                }
                //foreach (dynamic attr in prop.GetCustomAttributes(false))
                //{
                //}
            }
            return null;
        }

    }
}
