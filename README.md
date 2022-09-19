# ricaun.DA4R.NUnit

Unit Test Framework for DA4R.

[![Revit 2018](https://img.shields.io/badge/Revit-2018+-blue.svg)](../..)
[![Visual Studio 2022](https://img.shields.io/badge/Visual%20Studio-2022-blue)](../..)
[![Nuke](https://img.shields.io/badge/Nuke-Build-blue)](https://nuke.build/)
[![License MIT](https://img.shields.io/badge/License-MIT-blue.svg)](LICENSE)
[![Build](../../actions/workflows/Build.yml/badge.svg)](../../actions)

## DA4R

### Input / Output
```
├── ...
├── input.zip
│   ├── RevitAddin.Tests.dll
│   └── nunit.framework.dll
├── output.json
└── ...
```

### Input (Multi-Version) / Output
```
├── ...
├── input.zip
│   ├── 2018
│   │   ├── RevitAddin.Tests.dll
│   │   └── nunit.framework.dll
│   ├── 2019
│   │   ├── RevitAddin.Tests.dll
│   │   └── nunit.framework.dll
│   ├── 2020
│   │   ├── RevitAddin.Tests.dll
│   │   └── nunit.framework.dll
│   ├── 2021
│   │   ├── RevitAddin.Tests.dll
│   │   └── nunit.framework.dll
│   └── ...
├── output.json
└── ...
```

## Release

* [Latest release](../../releases/latest)

## License

This project is [licensed](LICENSE) under the [MIT Licence](https://en.wikipedia.org/wiki/MIT_License).

---

Do you like this project? Please [star this project on GitHub](../../stargazers)!