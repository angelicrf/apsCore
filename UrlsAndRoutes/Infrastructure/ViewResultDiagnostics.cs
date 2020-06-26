﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UrlsAndRoutes.Infrastructure
{
    public class ViewResultDiagnostics : IActionFilter
    {
        private FilterDiagnostics diagnostics;
        public ViewResultDiagnostics(FilterDiagnostics diags)
        {
            diagnostics = diags;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            // do nothing - not used in this filter
        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            ViewResult vr;
            if ((vr = context.Result as ViewResult) != null)
            {
                diagnostics.AddMessage($"View name: {vr.ViewName}");
                diagnostics.AddMessage($@"Model type:
                {vr.ViewData.Model.GetType().Name}");
            }
        }
    }
}