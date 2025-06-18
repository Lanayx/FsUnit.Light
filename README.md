# FsUnit.Light
Minimalistic version of [FsUnitTyped](https://fsprojects.github.io/FsUnit/FsUnitTyped.html) for xUnit, NUnit ans MSTest.

It provides zero-cost abstraction over native assertions with no dependency on `FsUnit`.
You can find some considerations [in the initial issue](https://github.com/fsprojects/FsUnit/issues/304).

## Migration from FsUnitTyped

You need to change
```fsharp
open FsUnitTyped
```
to
```fsharp
open FsUnit.Light
```

## Nuget Packages:



| Framework  | Package                                                                    |
|------------|----------------------------------------------------------------------------|
| **xUnit**  | [FsUnit.Light.xUnit](https://www.nuget.org/packages/FsUnit.Light.xUnit)    |
| **NUnit**  | [FsUnit.Light.NUnit](https://www.nuget.org/packages/FsUnit.Light.NUnit)    |
| **MSTest** | [FsUnit.Light.MSTest](https://www.nuget.org/packages/FsUnit.Light.MSTest)  |