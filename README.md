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
### Console
* [ricaun.Revit.Installation](https://github.com/ricaun-io/ricaun.Revit.Installation)
* [ricaun.RevitTest.Command](https://github.com/ricaun-io/ricaun.RevitTest)
* [ricaun.Autodesk.Forge.Oss.DesignAutomation](https://github.com/ricaun-io/forge-api-dotnet-oss.design.automation)

### Input / Output
```
├── ...
├── input.zip
│   ├── RevitAddin.Tests.dll
│   └── nunit.framework.dll
├── output.json
└── ...
```

## Console

```
ricaun.DA4R.NUnit.Console.exe --file "C:\Users\ricau\Downloads\SampleTest\RevitAddin.RevitApplication.Tests\2021\RevitAddin.RevitApplication.Tests.dll" -o "console" -l
```

### Test Language

```
.\ricaun.DA4R.NUnit.Console.exe --file "D:\Users\ricau\source\repos\RevitTest.Language\RevitTest.Language\bin\Debug\net45\RevitTest.Language.dll" -v 2021 -l -o "console" --language "en"
.\ricaun.DA4R.NUnit.Console.exe --file "D:\Users\ricau\source\repos\RevitTest.Language\RevitTest.Language\bin\Debug\net45\RevitTest.Language.dll" -v 2021 -l -o "console" --language "pt"
.\ricaun.DA4R.NUnit.Console.exe --file "D:\Users\ricau\source\repos\RevitTest.Language\RevitTest.Language\bin\Debug\net45\RevitTest.Language.dll" -v 2021 -l -o "console" --language "es"
.\ricaun.DA4R.NUnit.Console.exe --file "D:\Users\ricau\source\repos\RevitTest.Language\RevitTest.Language\bin\Debug\net45\RevitTest.Language.dll" -v 2021 -l -o "console" --language "ru"
```

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