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

module ShouldContainTests =

    [<Test>]
    let ``shouldContain passes for contained values``() =
        [ 1; 2; 3 ] |> shouldContain 2
        "hello" |> shouldContain 'e'
        [| 1; 2; 3 |] |> shouldContain 2

    [<Test>]
    let ``shouldContain fails for non-contained values``() =
        shouldFail<AssertionException>(fun () -> [ 1; 2; 3 ] |> shouldContain 4)
        shouldFail<AssertionException>(fun () -> "hello" |> shouldContain 'x')
        shouldFail<AssertionException>(fun () -> [| 1; 2; 3 |] |> shouldContain 4)

module ShouldNotContainTests =

    [<Test>]
    let ``shouldNotContain passes for non-contained values``() =
        [ 1; 2; 3 ] |> shouldNotContain 4
        "hello" |> shouldNotContain 'x'
        [||] |> shouldNotContain ""

    [<Test>]
    let ``shouldNotContain fails for contained values``() =
        shouldFail<AssertionException>(fun () -> [ 1; 2; 3 ] |> shouldNotContain 2)
        shouldFail<AssertionException>(fun () -> "hello" |> shouldNotContain 'e')
        shouldFail<AssertionException>(fun () -> [| 1; 2; 3 |] |> shouldNotContain 2)


module ShouldContainTextTests =

    [<Test>]
    let ``shouldContainText passes for contained text``() =
        "hello world" |> shouldContainText "world"
        "hello" |> shouldContainText ""

    [<Test>]
    let ``shouldContainText fails for non-contained text``() =
        shouldFail<AssertionException>(fun () -> "hello world" |> shouldContainText "planet")
        shouldFail<AssertionException>(fun () -> "" |> shouldContainText " ")

module ShouldNotContainTextTests =
    [<Test>]
    let ``shouldNotContainText passes for non-contained text``() =
        "hello world" |> shouldNotContainText "planet"
        "" |> shouldNotContainText " "

    [<Test>]
    let ``shouldNotContainText fails for contained text``() =
        shouldFail<AssertionException>(fun () -> "hello world" |> shouldNotContainText "world")
        shouldFail<AssertionException>(fun () -> "" |> shouldNotContainText "")