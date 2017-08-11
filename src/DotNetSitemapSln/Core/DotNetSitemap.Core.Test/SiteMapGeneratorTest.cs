using DotNetSitemap.Core.Constrains;
using DotNetSitemap.Core.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DotNetSitemap.Core.Models.SingleSitemap;

namespace DotNetSitemap.Core.Test
{
    [TestFixture]
    public class SitemapGeneratorTest
    {
        [Test]
        public void Render_UrlSetWithOneUrl_ReturnXmlForUrlSetWithOneUrl()
        {
            ISitemapGenerator generator = new SitemapGenerator();
            
            using (var ms = new MemoryStream())
            {
                //generator.Render(ms, )
            }

        }
        [Test]
        public void Render_UrlNoLastMod_ReturnXmlWithNoLastMod()
        {
            //throw new NotImplementedException();
        }
        [Test]
        public void Render_UrlNoChangeFreq_ReturnXmlWithNoChangeFreq()
        {
            //throw new NotImplementedException();
        }
        [Test]
        public void Render_UrlNoPriority_ReturnXmlWithNoPriority()
        {
            //throw new NotImplementedException();
        }

    }
}
