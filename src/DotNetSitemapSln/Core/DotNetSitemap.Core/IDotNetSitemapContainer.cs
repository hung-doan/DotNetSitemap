using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core
{
    public interface IDotNetSitemapContainer
    {
        void Register<src, dst>() where dst : src;
        T Resolve<T>() where T : class;

    }
}
