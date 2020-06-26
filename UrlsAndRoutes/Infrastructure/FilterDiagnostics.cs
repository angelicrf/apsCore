using System;
using System.Collections.Generic;

namespace UrlsAndRoutes.Infrastructure
{
    public interface FilterDiagnostics
    {
        IEnumerable<string> Messages { get; }
        void AddMessage(string message);
    }
}
