# Etch.OrchardCore.CivicCookieControl

Module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore) for adding [Cookie Control by CIVIC](https://www.civicuk.com/cookie-control).

## Build Status

[![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.CivicCookieControl.svg)](https://www.nuget.org/packages/Etch.OrchardCore.CivicCookieControl)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.4.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.4.0)).

## Installing

This module is available on NuGet. Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.CivicCookieControl", ensuring include prereleases is checked.

Alternatively, [download the source](https://github.com/etchuk/Etch.OrchardCore.CivicCookieControl/archive/main.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.CivicCookieControl.

## Usage

First step is to go to the [CIVIC site](https://www.civicuk.com/cookie-control/download) and create a license. The cookie control won't work until you've got a API key and configured the domain(s) that will use the key.

Once a reference to this module has been configured there will be a "CIVIC Cookie Control" feature available within the CMS instance. Enabling this feature will add a new menu option to the settings for configuring your CIVIC Cookie Control code snippet ("Configuration" -> "Settings" -> "CIVIC Cookie Control"). The cookie control will only show on the front-end your site when a valid API key and product have been applied to the settings.

## Custom Cookie Types

Out the box this module provides common cookies (e.g. Google Analytics, Facebook Pixel) as well as a "Raw Cookie" that gives control of all the possible properties on a [cookie category](https://www.civicuk.com/cookie-control/documentation#purpose-object). Follow the steps below to add your own custom cookie types that will be available when defining cookies within the "Cookies" tab of the CIVIC cookie control settings.

### Add Content Type

Create a content type definition that has a stereotype of "Cookie". This content type should contain fields that can be accessed when generating the cookie category object JSON that's added to the CIVIC cookie control configuration.

### Implement `ICookieType`

Use a custom module to create a new class that implements `ICookieType`. `ContentType` property should match the name of the content type created in the previous step. `ToJson` must return a JSON object that matches the API of the [purpose object](https://www.civicuk.com/cookie-control/documentation#purpose-object) expected by the CIVIC cookie control.

### Register Cookie Type

Within the custom modules `Startup`, register the class created in the previous step, as shown below.

```
public class Startup : StartupBase
{
    public override void ConfigureServices(IServiceCollection services)
    {
        services.AddCookieType<MyCustomCookie>();
    }
}
```
