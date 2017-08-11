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
    public class SiteMapGeneratorTest
    {
        [Test]
        public void Render_UrlSetWithOneUrl_ReturnXmlForUrlSetWithOneUrl()
        {
            ISiteMapGenerator generator = new SiteMapGenerator();
            var data = new SitemapXml
            {
                UrlSet = new UrlSet {
                    Urls = new List<Url> {
                        new Url {
                            Loc = "loc1",
                            ChangeFreq = ChangeFreq.Daily,
                            LastMod = new DateTime(2017,1,1),
                            Priority = 0.5
                        }
                    }
                }
            };
            using (var ms = new MemoryStream())
            {
                //generator.Render(ms, )
            }

        }
        [Test]
        public void Render_UrlNoLastMod_ReturnXmlWithNoLastMod()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void Render_UrlNoChangeFreq_ReturnXmlWithNoChangeFreq()
        {
            throw new NotImplementedException();
        }
        [Test]
        public void Render_UrlNoPriority_ReturnXmlWithNoPriority()
        {
            throw new NotImplementedException();
        }

    }
}
