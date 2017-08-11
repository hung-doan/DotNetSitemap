﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace DotNetSitemap.Core.Models
{
    public interface ISitemapData
    {
        void Render(Stream outputStream, Uri requestUri);
    }
}
