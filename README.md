# GladLive.Web.Payloads

GladLive is network session service comparable to XboxLive or Steam. 

GladLive.Web.Payloads is shared code between the GladLive network services on the web and non-web backend.

## GladLive Services

GladLive.ProxyLoadBalancer: https://github.com/GladLive/GladLive.ProxyLoadBalancer

GladLive.AuthService.ASP: https://github.com/GladLive/GladLive.AuthService.ASP

## Setup

To use this project you'll first need a couple of things:
  - Visual Studio 2015
  - DNX/DNVM
  - Add Nuget Feed https://www.myget.org/F/hellokitty/api/v2 in VS (Options -> NuGet -> Package Sources)

## Builds

Available on a Nuget Feed: https://www.myget.org/F/hellokitty/api/v2 [![hellokitty MyGet Build Status](https://www.myget.org/BuildSource/Badge/hellokitty?identifier=49afe5c8-2b28-4524-9a14-4f3d8be56cab)](https://www.myget.org/gallery/hellokitty)

##Tests

#### Linux/Mono - Unit Tests
||Debug x86|Debug x64|Release x86|Release x64|
|:--:|:--:|:--:|:--:|:--:|:--:|
|**master**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.Web.Payloads.svg?branch=master)](https://travis-ci.org/HelloKitty/GladLive/GladLive.Web.Payloads) |
|**dev**| N/A | N/A | N/A | [![Build Status](https://travis-ci.org/GladLive/GladLive.Web.Payloads.svg?branch=dev)](https://travis-ci.org/GladLive/GladLive.Web.Payloads)|

#### Windows - Unit Tests

(Done locally)

##Licensing

This project is licensed under the MIT license with the additional requirement of adding the GladLive splashscreen to your product.
