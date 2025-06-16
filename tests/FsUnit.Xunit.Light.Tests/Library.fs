namespace FsUnit.Xunit.Light.Tests

open Xunit
open Xunit.Sdk
open FsUnit.Xunit.Light

module ShouldBeEmptyTests =

    [<Fact>]
    let ``empty collections should be Empty``() =
        [] |> shouldBeEmpty
        [||] |> shouldBeEmpty
        Seq.empty |> shouldBeEmpty

    [<Fact>]
    let ``non-empty collections fail to be Empty``() =
        shouldFail<EmptyException>(fun () -> [ 1 ] |> shouldBeEmpty)
        shouldFail<EmptyException>(fun () -> [| 1 |] |> shouldBeEmpty)
        shouldFail<EmptyException>(fun () -> seq { 1 } |> shouldBeEmpty)

module ShouldHaveLengthTests =

    [<Fact>]
    let ``collections with expected length should pass``() =
        [ 1 ] |> shouldHaveLength 1
        seq { 1 } |> shouldHaveLength 1
        "a" |> shouldHaveLength 1

    [<Fact>]
    let ``collections with unexpected length fail``() =
        shouldFail<EqualException>(fun () -> [] |> shouldHaveLength 1)
        shouldFail<EqualException>(fun () -> seq {} |> shouldHaveLength 1)
        shouldFail<EqualException>(fun () -> "" |> shouldHaveLength 1)

module ShouldBeGreaterThanTests =

    [<Fact>]
    let ``shouldBeGreaterThan passes for greater values``() =
        2 |> shouldBeGreaterThan 1
        "b" |> shouldBeGreaterThan "a"
        2.0 |> shouldBeGreaterThan 1.0

    [<Fact>]
    let ``shouldBeGreaterThan fails for lesser or equal values``() =
        shouldFail<TrueException>(fun () -> 1 |> shouldBeGreaterThan 2)
        shouldFail<TrueException>(fun () -> "a" |> shouldBeGreaterThan "a")
        shouldFail<TrueException>(fun () -> 1.0 |> shouldBeGreaterThan 2.0)

module ShouldBeSmallerThanTests =

    [<Fact>]
    let ``shouldBeSmallerThan passes for smaller values``() =
        1 |> shouldBeSmallerThan 2
        "a" |> shouldBeSmallerThan "b"
        1.0 |> shouldBeSmallerThan 2.0

    [<Fact>]
    let ``shouldBeSmallerThan fails for greater or equal values``() =
        shouldFail<TrueException>(fun () -> 2 |> shouldBeSmallerThan 1)
        shouldFail<TrueException>(fun () -> "a" |> shouldBeSmallerThan "a")
        shouldFail<TrueException>(fun () -> 2.0 |> shouldBeSmallerThan 1.0)