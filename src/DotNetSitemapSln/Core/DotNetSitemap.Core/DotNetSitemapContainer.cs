using System;
using System.Collections.Generic;
using System.Text;

namespace DotNetSitemap.Core
{
    public class DotNetSitemapContainer : IDotNetSitemapContainer
    {
        Dictionary<Type, Type> _container = new Dictionary<Type, Type>();
        public void Register<src, dst>() where dst : src
        {
            
            _container.Add(typeof(src), typeof(dst));
        }

        public T Resolve<T>() where T : class
        {
            var dst = _container[typeof(T)];
            var dstInstance = Activator.CreateInstance(dst);
            return dstInstance as T;
        }
    }
}