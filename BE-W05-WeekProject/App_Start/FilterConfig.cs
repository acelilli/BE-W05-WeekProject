﻿using System.Web;
using System.Web.Mvc;

namespace BE_W05_WeekProject
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
