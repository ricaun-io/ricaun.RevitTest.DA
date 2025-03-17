# ricaun.RevitTest.DA

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](https://github.com/ricaun-io/ricaun.RevitTest.DA)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](https://github.com/ricaun-io/ricaun.RevitTest.DA)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](https://github.com/ricaun-io/ricaun.RevitTest.DA/actions/workflows/Build.yml/badge.svg)](https://github.com/ricaun-io/ricaun.RevitTest.DA/actions)

Run Revit tests using the Design Automation for Revit.

This project is design to work with the [ricaun.RevitTest](https://ricaun.com/RevitTest) test framework.

## Samples

* [RevitTest.DA.Sample](https://github.com/ricaun-io/RevitTest.DA)

### Install in `ricaun.RevitTest.TestAdapter`

To install the `ricaun.RevitTest.DA` in the `ricaun.RevitTest.TestAdapter` project, you need to add the following metadata configuration in your test project.

```C#
[assembly: AssemblyMetadata("NUnit.Application", "https://github.com/ricaun-io/ricaun.RevitTest.DA/releases/latest/download/ricaun.RevitTest.DA.Console.zip")]
```

Or using the `RICAUN_REVITTEST_TESTADAPTER_NUNIT_APPLICATION` environment variable.

```
RICAUN_REVITTEST_TESTADAPTER_NUNIT_APPLICATION=https://github.com/ricaun-io/ricaun.RevitTest.DA/releases/latest/download/ricaun.RevitTest.DA.Console.zip
```

This metadata will be used to download the `ricaun.RevitTest.DA.Console.zip` and used in run the tests using the `ricaun.RevitTest.DA` application.

The `ricaun.RevitTest.DA` application requires the configuration below with the Aps credentials.

### Configuration

By default the Aps/Forge credentials could be defined with the following environment variables:

```bash
APS_CLIENT_ID=<your client id>
APS_CLIENT_SECRET=<your client secret>
```

To create a new application in the Autodesk Platform Service, check the [Autodesk](https://aps.autodesk.com/) website. The application needs to have API Access to `Design Automation API` and `Data Management API`.

#### Three-legged Token

To use the three-legged token, you need to define the `APS_ACCESS_TOKEN` environment variable:

```bash
APS_ACCESS_TOKEN=<your access token>
```

The `APS_ACCESS_TOKEN` environment variable is used to pass the `adsk3LeggedToken` in DA4R. The token is tested to get the user information, if successful, the token is used in the DA4R.

## Dependencies

### Application
* [ricaun.NUnit](https://github.com/ricaun-io/ricaun.NUnit)
* [ricaun.Revit.DA](https://github.com/ricaun-io/ricaun.Revit.DA)
### Console
* [ricaun.Revit.Installation](https://github.com/ricaun-io/ricaun.Revit.Installation)
* [ricaun.RevitTest.Command](https://github.com/ricaun-io/ricaun.RevitTest)
* [ricaun.Autodesk.Forge.Oss.DesignAutomation](https://github.com/ricaun-io/forge-api-dotnet-oss.design.automation)

## Release

* [Latest release](https://github.com/ricaun-io/ricaun.RevitTest.DA/releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](https://github.com/ricaun-io/ricaun.RevitTest.DA/stargazers)!