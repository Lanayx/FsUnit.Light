namespace FsUnit.Light

open System.Collections
open System.Linq
open System.Text.Json
open Microsoft.VisualStudio.TestTools.UnitTesting

[<AutoOpen>]
module MSTest =

    let inline shouldEqual (expected: 'a) (actual: 'a) =
        Assert.AreEqual<'a>(expected, actual)

    let inline shouldNotEqual (expected: 'a) (actual: 'a) =
        Assert.AreNotEqual<'a>(expected, actual)

    let inline shouldContain (expected: 'a) (actual: 'a seq) =
        Assert.Contains(expected, actual)

    let inline shouldBeEmpty(actual: 'a seq) =
        Assert.IsEmpty(actual)

    let inline shouldNotContain (expected: 'a) (actual: 'a seq) =
        Assert.DoesNotContain(expected, actual)

    let inline shouldBeSmallerThan (expected: 'a) (actual: 'a) =
        Assert.IsTrue(actual < expected, $"Expected:<Smaller than {expected}>, Actual:<{actual}>")

    let inline shouldBeGreaterThan (expected: 'a) (actual: 'a) =
        Assert.IsTrue(actual > expected, $"Expected:<Greater than {expected}>, Actual:<{actual}>")

    let inline shouldFail<'exn when 'exn :> exn>(f: unit -> unit) =
        f |> Assert.Throws<'exn> |> ignore

    let inline shouldFailWithMessage<'ex when 'ex :> exn> (expected: string) (f: unit -> unit)  =
        f |> Assert.Throws<'ex> |> _.Message |> shouldEqual expected

    let inline shouldContainText (expected: string) (actual: string) =
        Assert.Contains(expected, actual)

    let inline shouldNotContainText (expected: string) (actual: string) =
        Assert.DoesNotContain(expected, actual)

    let inline shouldHaveLength (expected: int) (actual: 'a seq) =
        Assert.AreEqual<int>(expected, Seq.length actual)

    let inline shouldEquivalent (expected: 'a) (actual: 'a) =
        match box expected, box actual with
        | :? IEnumerable as expectedEnum, (:? IEnumerable as actualEnum) ->
            CollectionAssert.AreEquivalent(
                expectedEnum.Cast<obj>(),
                actualEnum.Cast<obj>(),
                System.Collections.Generic.EqualityComparer<obj>.Default)
        | _ ->
            Assert.AreEqual<string>(
                JsonSerializer.Serialize(expected),
                JsonSerializer.Serialize(actual))
