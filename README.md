# FsUnit.Light
Minimalistic version of [FsUnitTyped](https://fsprojects.github.io/FsUnit/FsUnitTyped.html) for xUnit, NUnit ans MsTest.

It provides zero-cost abstraction over native assertions with no dependency on `FsUnit`.
You can find some other considerations [in the initial issue](https://github.com/fsprojects/FsUnit/issues/304).

## Migration from FsUnitTyped

You need to change
```fsharp
open FsUnitTyped
```
to
```fsharp
open FsUnit.Light
```