# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](http://keepachangelog.com/en/1.0.0/)
and this project adheres to [Semantic Versioning](http://semver.org/spec/v2.0.0.html).

## [1.1.0] / 2023-08-23
### Console
- Support Language command
- Update `ricaun.RevitTest.Command` to version `1.1.0` - support language
- Add `LanguageUtils` to convert to Revit language.

## [1.0.9] / 2023-08-18
### Console
- Update `ricaun.Autodesk.Forge.Oss.DesignAutomation` to `1.0.5`

## [1.0.8] / 2023-07-24
### Application
- Add `TestEngine.Fail` when fail.
### Console
- Add `Console` log information in `RunTests`

## [1.0.7] / 2023-06-30
### Console
- Add `ForgeEnvironment` to `release`
- Update `Oss.DesignAutomation` package to `1.0.4`

## [1.0.6] / 2023-06-27
### Console
- Add `DirectoryResolver` with `RevitAPI` and `RevitAPIUI` mocked without `Revit` reference.

## [1.0.5] / 2023-06-26
### Application
- Remove `DesignAutomationReadyEvent` when is trigger
### Console
- Add `TestExceptionUtils` to show Exception in Console
- Show `Revit version not supported` in Console

## [1.0.4] / 2023-06-15
### Console
- Add Timeout Error Exception
### Application
- Update Start/Finish in `TestService`

## [1.0.3] / 2023-06-13
### Console
- Add Console Project to run `TestService`
- Exception is shown in Tests with `CreateTestAssemblyModelWithException`
### Updated
- Update Build to compile `Console` Project

## [1.0.2] / 2023-05-30
### Local
- Add `AppDomainUtils` to force Load `ricaun.NUnit` dependencies.
- Add `SampleTest.zip` file in `Debug`
### Updated
- Update `TestService`
- Update `ricaun.NUnit` to version `1.2.9`

## [1.0.1] / 2022-12-13
### Updated
- Update ricaun.NUnit to version `1.1.0`

## [1.0.0] / 2022-09-19
### Updated
- Update ricaun.NUnit Library - Remove await/async
### Fixed
- Fix UnZip File
- Fix Test Parameters, `RevitApp` is a RevitNET type.  
### Features
- Auto Find Directory Version
- Add RevitParameters
- TestAssembly in `*.zip` and `output.json`

[vNext]: ../../compare/1.0.0...HEAD
[1.0.9]: ../../compare/1.0.8...1.0.9
[1.0.8]: ../../compare/1.0.7...1.0.8
[1.0.7]: ../../compare/1.0.6...1.0.7
[1.0.6]: ../../compare/1.0.5...1.0.6
[1.0.5]: ../../compare/1.0.4...1.0.5
[1.0.4]: ../../compare/1.0.3...1.0.4
[1.0.3]: ../../compare/1.0.2...1.0.3
[1.0.2]: ../../compare/1.0.1...1.0.2
[1.0.1]: ../../compare/1.0.0...1.0.1
[1.0.0]: ../../compare/1.0.0