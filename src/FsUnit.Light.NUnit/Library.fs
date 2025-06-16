namespace FsUnit.Light

open NUnit.Framework

[<AutoOpen>]
module NUnit =

    let inline shouldEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.EqualTo<'a>(expected))

    let inline shouldNotEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.Not.EqualTo<'a>(expected))

    let inline shouldContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Contain(expected))

    let inline shouldBeEmpty(actual: 'a seq) =
        Assert.That(actual, Is.Empty)

    let inline shouldNotContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Not.Contain(expected))

    let inline shouldBeSmallerThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.LessThan(expected))

    let inline shouldBeGreaterThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.GreaterThan(expected))

    let inline shouldFail<'exn when 'exn :> exn>(f: unit -> unit) =
        Assert.Throws<'exn>(f) |> ignore

    let inline shouldContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Contain(expected))

    let inline shouldNotContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Not.Contain(expected))

    let inline shouldHaveLength (expected: int) actual =
        Assert.That(Seq.length actual, Is.EqualTo(expected))
