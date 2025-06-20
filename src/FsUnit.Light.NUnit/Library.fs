namespace FsUnit.Light

open System
open System.Collections
open NUnit.Framework

[<AutoOpen>]
module NUnit =

    let inline shouldEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.EqualTo<'a>(expected))

    let inline shouldNotEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.Not.EqualTo<'a>(expected))

    let inline shouldContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Contain(expected))

    let inline shouldBeEmpty (actual: 'a seq) =
        Assert.That(actual, Is.Empty)

    let inline shouldNotContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Not.Contain(expected))

    let inline shouldBeSmallerThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.LessThan(expected))

    let inline shouldBeGreaterThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.GreaterThan(expected))

    let inline shouldFail<'ex when 'ex :> exn> (f: unit -> unit) =
        f |> Assert.Throws<'ex> |> ignore

    let inline shouldFailWithMessage<'ex when 'ex :> exn> (expected: string) (f: unit -> unit) =
        f |> Assert.Throws<'ex> |> function
            | null -> Assert.Fail("Actual exception is null.")
            | ex -> ex.Message |> shouldEqual expected

    let inline shouldContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Contain(expected))

    let inline shouldNotContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Not.Contain(expected))

    let inline shouldHaveLength (expected: int) actual =
        Assert.That(Seq.length actual, Is.EqualTo(expected))

    let inline shouldEquivalent (expected: 'a) (actual: 'a) =
        match box expected, box actual with
        | :? IEnumerable as expectedEnum, (:? IEnumerable as actualEnum) ->
            Assert.That(actualEnum, Is.EquivalentTo(expectedEnum))
        | _ ->
            Assert.That(expected, Is.EqualTo<'a>(actual).UsingPropertiesComparer())
