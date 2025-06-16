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

module ShouldContainTests =

    [<Fact>]
    let ``shouldContain passes for contained values``() =
        [ 1; 2; 3 ] |> shouldContain 2
        "hello" |> shouldContain 'e'
        [| 1; 2; 3 |] |> shouldContain 2

    [<Fact>]
    let ``shouldContain fails for non-contained values``() =
        shouldFail<ContainsException>(fun () -> [ 1; 2; 3 ] |> shouldContain 4)
        shouldFail<ContainsException>(fun () -> "hello" |> shouldContain 'x')
        shouldFail<ContainsException>(fun () -> [| 1; 2; 3 |] |> shouldContain 4)

module ShouldNotContainTests =

    [<Fact>]
    let ``shouldNotContain passes for non-contained values``() =
        [ 1; 2; 3 ] |> shouldNotContain 4
        "hello" |> shouldNotContain 'x'
        [||] |> shouldNotContain ""

    [<Fact>]
    let ``shouldNotContain fails for contained values``() =
        shouldFail<DoesNotContainException>(fun () -> [ 1; 2; 3 ] |> shouldNotContain 2)
        shouldFail<DoesNotContainException>(fun () -> "hello" |> shouldNotContain 'e')
        shouldFail<DoesNotContainException>(fun () -> [| 1; 2; 3 |] |> shouldNotContain 2)

module ShouldContainTextTests =

    [<Fact>]
    let ``shouldContainText passes for contained text``() =
        "hello world" |> shouldContainText "world"
        "hello" |> shouldContainText ""

    [<Fact>]
    let ``shouldContainText fails for non-contained text``() =
        shouldFail<ContainsException>(fun () -> "hello world" |> shouldContainText "planet")
        shouldFail<ContainsException>(fun () -> "" |> shouldContainText " ")

module ShouldNotContainTextTests =
    [<Fact>]
    let ``shouldNotContainText passes for non-contained text``() =
        "hello world" |> shouldNotContainText "planet"
        "" |> shouldNotContainText " "

    [<Fact>]
    let ``shouldNotContainText fails for contained text``() =
        shouldFail<DoesNotContainException>(fun () -> "hello world" |> shouldNotContainText "world")
        shouldFail<DoesNotContainException>(fun () -> "" |> shouldNotContainText "")