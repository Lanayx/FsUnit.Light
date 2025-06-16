namespace FsUnit.Xunit.Light.Tests

open NUnit.Framework
open FsUnit.NUnit.Light

module ShouldBeEmptyTests =

    [<Test>]
    let ``empty collections should be Empty``() =
        [] |> shouldBeEmpty
        [||] |> shouldBeEmpty
        Seq.empty |> shouldBeEmpty

    [<Test>]
    let ``non-empty collections fail to be Empty``() =
        shouldFail<AssertionException>(fun () -> [ 1 ] |> shouldBeEmpty)
        shouldFail<AssertionException>(fun () -> [| 1 |] |> shouldBeEmpty)
        shouldFail<AssertionException>(fun () -> seq { 1 } |> shouldBeEmpty)

module ShouldHaveLengthTests =

    [<Test>]
    let ``collections with expected length should pass``() =
        [ 1 ] |> shouldHaveLength 1
        seq { 1 } |> shouldHaveLength 1
        "a" |> shouldHaveLength 1

    [<Test>]
    let ``collections with unexpected length fail``() =
        shouldFail<AssertionException>(fun () -> [] |> shouldHaveLength 1)
        shouldFail<AssertionException>(fun () -> seq {} |> shouldHaveLength 1)
        shouldFail<AssertionException>(fun () -> "" |> shouldHaveLength 1)

module ShouldBeGreaterThanTests =

    [<Test>]
    let ``shouldBeGreaterThan passes for greater values``() =
        2 |> shouldBeGreaterThan 1
        "b" |> shouldBeGreaterThan "a"
        2.0 |> shouldBeGreaterThan 1.0

    [<Test>]
    let ``shouldBeGreaterThan fails for lesser or equal values``() =
        shouldFail<AssertionException>(fun () -> 1 |> shouldBeGreaterThan 2)
        shouldFail<AssertionException>(fun () -> "a" |> shouldBeGreaterThan "a")
        shouldFail<AssertionException>(fun () -> 1.0 |> shouldBeGreaterThan 2.0)

module ShouldBeSmallerThanTests =

    [<Test>]
    let ``shouldBeSmallerThan passes for smaller values``() =
        1 |> shouldBeSmallerThan 2
        "a" |> shouldBeSmallerThan "b"
        1.0 |> shouldBeSmallerThan 2.0

    [<Test>]
    let ``shouldBeSmallerThan fails for greater or equal values``() =
        shouldFail<AssertionException>(fun () -> 2 |> shouldBeSmallerThan 1)
        shouldFail<AssertionException>(fun () -> "a" |> shouldBeSmallerThan "a")
        shouldFail<AssertionException>(fun () -> 2.0 |> shouldBeSmallerThan 1.0)