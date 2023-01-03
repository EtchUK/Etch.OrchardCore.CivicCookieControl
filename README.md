# Etch.OrchardCore.CivicCookieControl

Module for [Orchard Core](https://github.com/OrchardCMS/OrchardCore) for adding [Cookie Control by CIVIC](https://www.civicuk.com/cookie-control).

## Build Status

[![NuGet](https://img.shields.io/nuget/v/Etch.OrchardCore.CivicCookieControl.svg)](https://www.nuget.org/packages/Etch.OrchardCore.CivicCookieControl)

## Orchard Core Reference

This module is referencing a stable build of Orchard Core ([`1.5.0`](https://www.nuget.org/packages/OrchardCore.Module.Targets/1.5.0)).

## Installing

This module is available on NuGet. Add a reference to your Orchard Core web project via the NuGet package manager. Search for "Etch.OrchardCore.CivicCookieControl", ensuring include prereleases is checked.

Alternatively, [download the source](https://github.com/etchuk/Etch.OrchardCore.CivicCookieControl/archive/main.zip) or clone the repository to your local machine. Add the project to your solution that contains an Orchard Core project and add a reference to Etch.OrchardCore.CivicCookieControl.

## Usage

First step is to go to the [CIVIC site](https://www.civicuk.com/cookie-control/download) and create a license. The cookie control won't work until you've added a a "CIVIC Cookie Control" widget to a layer.

Once a reference to this module has been configured there will be a "CIVIC Cookie Control" feature available within the CMS instance. Enabling this feature will create a new "CIVIC Cookie Control" widget that when created contains various fields that are used to construct the [configuration properties](http://www.civicuk.com/cookie-control/documentation/getting-started#beginning-your-configuration) that are passed to the CIVIC cookie control JS library.