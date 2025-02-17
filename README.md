# ricaun.DA4R.NUnit

[![Revit 2019](https://img.shields.io/badge/Revit-2019+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

Unit Test Framework for DA4R.

This project is design to work with the [ricaun.RevitTest](https://ricaun.com/RevitTest) test framework.

## DA4R

### Application
* [ricaun.NUnit](https://github.com/ricaun-io/ricaun.NUnit)
* [ricaun.Revit.DA](https://github.com/ricaun-io/ricaun.Revit.DA)
### Console
* [ricaun.Revit.Installation](https://github.com/ricaun-io/ricaun.Revit.Installation)
* [ricaun.RevitTest.Command](https://github.com/ricaun-io/ricaun.RevitTest)
* [ricaun.Autodesk.Forge.Oss.DesignAutomation](https://github.com/ricaun-io/forge-api-dotnet-oss.design.automation)

### Install in `ricaun.RevitTest`

To install the `ricaun.DA4R.NUnit` in the `ricaun.RevitTest` project, you need to add the following metadata configuration in your test project.

```C#
[assembly: AssemblyMetadata("NUnit.Application", "https://github.com/ricaun-io/ricaun.DA4R.NUnit/releases/latest/download/ricaun.DA4R.NUnit.Console.zip")]
```

This metadata will be used to download the `ricaun.DA4R.NUnit.Console.zip` and used in run the tests using the `ricaun.DA4R.NUnit` application.

The `ricaun.DA4R.NUnit` application requires the configuration below with the Aps credentials.

### Configuration

By default the Aps/Forge credentials could be defined with the following environment variables:

```bash
APS_CLIENT_ID=<your client id>
APS_CLIENT_SECRET=<your client secret>
```

#### Three-legged Token

To use the three-legged token, you need to define the `APS_ACCESS_TOKEN` environment variable:

```bash
APS_ACCESS_TOKEN=<your access token>
```

The `APS_ACCESS_TOKEN` environment variable is used to pass the `adsk3LeggedToken` in DA4R. The token is tested to get the user information, if successful, the token is used in the DA4R.

## Release

* [Latest release](../../releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT License](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!