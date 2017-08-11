# DotNetSitemap
[![Build status](https://ci.appveyor.com/api/projects/status/0argkxy4c8n0xdpy/branch/develop?svg=true)](https://ci.appveyor.com/project/hung-doan/dotnetsitemap/branch/develop)

DotNetSitemap is a simple sitemap for your websites. 
DotNetSitemap support
* ASP .NET Core (including ASP .NET Core MVC)
* ASP .NET (including ASP .NET MVC, ASP .NET Webform)

Requirements: 
* For ASP .NET 
    * ASP .NET Framework 4.6.2 or above
* For ASP .NET Core
    * ASP .NET Core 1.1 or above


Get Started with ASP .NET

Installation: 

nuget install DotNetSitemap.AspNet

Configuration: 
In your *Global.asax* Application_Start(): 

```
protected void Application_Start()
{

    // Setting up
    DotNetSitemapConfig.Option.Register(Server, RouteTable.Routes);

    // Config function to get sitemap data
    DotNetSitemapConfig.Option.SetDataFunc("sitemap.xml", GetSitemapXMLData);
    ...
}

// Function to get your data
private SitemapXml GetSitemapXMLData()
{
    var result = SitemapXml();
    return result;
}

```

Get Started with ASP .NET Core

Installation: 
```
nuget install DotNetSitemap.AspNetCore
```


