# FsUnit.Light

<img align="right" width="100" style="margin-left:20px" src="https://github.com/Lanayx/FsUnit.Light/raw/refs/heads/main/images/fsunit-light.png">

Minimalistic version of [FsUnitTyped](https://fsprojects.github.io/FsUnit/FsUnitTyped.html) for xUnit, NUnit ans MSTest.

It provides zero-cost abstraction over native assertions with no dependency on `FsUnit`.
You can find some considerations [in the initial issue](https://github.com/fsprojects/FsUnit/issues/304).

## Usage
```fsharp
1 |> shouldEqual 1 // pass
1 |> shouldNotEqual 2 // pass
[2] |> shouldContain 1 // pass
[] |> shouldNotContain 1 // pass
"Hello" |> shouldContainText "He" // pass
"Hello" |> shouldNotContainText "He" // fail
[] |> shouldBeEmpty // pass
[1] |> shouldHaveLength 1 // pass
2 |> shouldBeGreaterThan 1 // pass
1 |> shouldBeSmallerThan 2 // pass
(fun () -> null |> Array.sortInPlace) |> shouldFail<ArgumentNullException> // pass
(fun () -> failwith "error") |> shouldFailWithMessage "error" // pass
task { raise <| ArgumentNullException() } |> shouldFailTask<ArgumentNullException> // pass (should be awaited)
task { failwith "error" } |> shouldFailTaskWithMessage "error" // pass (should be awaited)
[1;2] |> shouldEquivalent [2;1] // pass
Item(Id="1") |> shouldEquivalent (Item(Id ="1")) // pass
```

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