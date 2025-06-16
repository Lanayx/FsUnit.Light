namespace FsUnit.Light

open Xunit

[<AutoOpen>]
module XUnit =

    let inline shouldEqual<'a> (expected: 'a) (actual: 'a) =
        Assert.Equal(expected, actual)

    let inline shouldNotEqual<'a> (expected: 'a) (actual: 'a) =
        Assert.NotEqual(expected, actual)

    let inline shouldContain<'a when 'a: equality> (expected: 'a) (actual: 'a seq) =
        Assert.Contains(expected, actual)

    let inline shouldBeEmpty<'a>(actual: 'a seq) =
        Assert.Empty actual

    let inline shouldNotContain<'a when 'a: equality> (expected: 'a) (actual: 'a seq) =
        Assert.DoesNotContain(expected, actual)

    let inline shouldBeSmallerThan<'a when 'a: comparison> (expected: 'a) (actual: 'a) =
        Assert.True(actual < expected, $"Assert.True() Failure
Expected: Smaller than {expected}
Actual:   {actual}")

    let inline shouldBeGreaterThan<'a when 'a: comparison> (expected: 'a) (actual: 'a) =
        Assert.True(actual > expected, $"Assert.True() Failure
Expected: Greater than {expected}
Actual:   {actual}")

    let inline shouldFail<'exn when 'exn :> exn>(f: unit -> unit) =
        Assert.Throws<'exn>(f) |> ignore

    let inline shouldContainText (expected: string) (actual: string) =
        Assert.Contains(expected, actual)

    let inline shouldNotContainText (expected: string) (actual: string) =
        Assert.DoesNotContain(expected, actual)

    let inline shouldHaveLength<'a> (expected: int) (actual: 'a seq) =
        Assert.Equal(expected, Seq.length actual)
