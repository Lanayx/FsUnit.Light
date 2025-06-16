namespace FsUnit.Xunit.Light

open Xunit

[<AutoOpen>]
module TopLevelOperators =

    let shouldEqual<'a> (expected: 'a) (actual: 'a) =
        Assert.Equal(expected, actual)

    let shouldNotEqual<'a> (expected: 'a) (actual: 'a) =
        Assert.NotEqual(expected, actual)

    let shouldContain<'a when 'a: equality> (expected: 'a) (actual: 'a seq) =
        Assert.Contains(expected, actual)

    let shouldBeEmpty<'a>(actual: 'a seq) =
        Assert.Empty actual

    let shouldNotContain<'a when 'a: equality> (expected: 'a) (actual: 'a seq) =
        Assert.DoesNotContain(expected, actual)

    let shouldBeSmallerThan<'a when 'a: comparison> (expected: 'a) (actual: 'a) =
        Assert.True(actual < expected)

    let shouldBeGreaterThan<'a when 'a: comparison> (expected: 'a) (actual: 'a) =
        Assert.True(actual > expected)

    let shouldFail<'exn when 'exn :> exn>(f: unit -> unit) =
        Assert.Throws<'exn>(f) |> ignore

    let shouldContainText (expected: string) (actual: string) =
        Assert.Contains(expected, actual)

    let shouldNotContainText (expected: string) (actual: string) =
        Assert.DoesNotContain(expected, actual)

    let shouldHaveLength<'a> (expected: int) (actual: 'a seq) =
        Assert.Equal(expected, Seq.length actual)
