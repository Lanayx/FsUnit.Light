namespace FsUnit.NUnit.Light

open NUnit.Framework

[<AutoOpen>]
module TopLevelOperators =

    let shouldEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.EqualTo<'a>(expected))

    let shouldNotEqual (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.Not.EqualTo<'a>(expected))

    let shouldContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Contain(expected))

    let shouldBeEmpty(actual: 'a seq) =
        Assert.That(actual, Is.Empty)

    let shouldNotContain (expected: 'a) (actual: 'a seq) =
        Assert.That(actual, Does.Not.Contain(expected))

    let shouldBeSmallerThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.LessThan(expected))

    let shouldBeGreaterThan (expected: 'a) (actual: 'a) =
        Assert.That(actual, Is.GreaterThan(expected))

    let shouldFail<'exn when 'exn :> exn>(f: unit -> unit) =
        Assert.Throws<'exn>(f) |> ignore

    let shouldContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Contain(expected))

    let shouldNotContainText (expected: string) (actual: string) =
        Assert.That(actual, Does.Not.Contain(expected))

    let shouldHaveLength (expected: int) actual =
        Assert.That(Seq.length actual, Is.EqualTo(expected))
