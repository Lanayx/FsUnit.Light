namespace FsUnit.Light.Xunit.Tests

open System
open Xunit
open Xunit.Sdk
open FsUnit.Light

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

module ShouldEqualTests =

    type Item() =
        member val Id = "" with get, set

    [<Fact>]
    let ``shouldEqual passes for equal values``() =
        1 |> shouldEqual 1
        2.0 |> shouldEqual 2.0
        "hello" |> shouldEqual "hello"
        [ 1; 2; 3 ] |> shouldEqual [ 1; 2; 3 ]
        [| 1; 2; 3 |] |> shouldEqual [| 1; 2; 3 |]
        seq { 1; 2; 3 } |> shouldEqual (seq { 1; 2; 3 })
        Map.ofList [ "a", 1; "b", 2 ] |> shouldEqual (Map.ofList [ "b", 2; "a", 1 ])
        null |> shouldEqual null
        let x = obj() in x |> shouldEqual x
        {| Name = "John" |} |> shouldEqual {| Name = "John" |}

    [<Fact>]
    let ``shouldEqual fails for unequal values``() =
        shouldFail<EqualException>(fun () -> 1 |> shouldEqual 2)
        shouldFail<EqualException>(fun () -> 2.0 |> shouldEqual 3.0)
        shouldFail<EqualException>(fun () -> "hello" |> shouldEqual "world")
        shouldFail<EqualException>(fun () -> [ 1; 2; 3 ] |> shouldEqual [ 4; 5; 6 ])
        shouldFail<EqualException>(fun () -> obj() |> shouldEqual (obj()))
        shouldFail<EqualException>(fun () -> {| Name = "John" |} |> shouldEqual {| Name = "Jack" |})
        shouldFail<EqualException>(fun () -> Item(Id = "1") |> shouldEqual (Item(Id = "1")))

module ShouldNotEqualTests =

    type Item() =
        member val Id = "" with get, set

    [<Fact>]
    let ``shouldNotEqual passes for unequal values``() =
        1 |> shouldNotEqual 2
        2.0 |> shouldNotEqual 3.0
        "hello" |> shouldNotEqual "world"
        [ 1; 2; 3 ] |> shouldNotEqual [ 3; 2; 1 ]
        [| 1; 2; 3 |] |> shouldNotEqual [| 4; 5; 6 |]
        seq { 1; 2; 3 } |> shouldNotEqual (seq { 4; 5; 6 })
        Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "c", 3 ])
        obj() |> shouldNotEqual (obj())
        {| Name = "John" |} |> shouldNotEqual {| Name = "Jack" |}
        Item(Id = "1") |> shouldNotEqual (Item(Id = "1"))

    [<Fact>]
    let ``shouldNotEqual fails for equal values``() =
        shouldFail<NotEqualException>(fun () -> 1 |> shouldNotEqual 1)
        shouldFail<NotEqualException>(fun () -> "hello" |> shouldNotEqual "hello")
        shouldFail<NotEqualException>(fun () -> [ 1; 2; 3 ] |> shouldNotEqual [ 1; 2; 3 ])
        shouldFail<NotEqualException>(fun () -> [| 1; 2; 3 |] |> shouldNotEqual [| 1; 2; 3 |])
        shouldFail<NotEqualException>(fun () -> seq { 1; 2; 3 } |> shouldNotEqual (seq { 1; 2; 3 }))
        shouldFail<NotEqualException>(fun () -> Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "b", 2; "a", 1 ]))
        shouldFail<NotEqualException>(fun () -> null |> shouldNotEqual null)
        shouldFail<NotEqualException>(fun () -> let x = obj() in x |> shouldNotEqual x)
        shouldFail<NotEqualException>(fun () -> {| Name = "John" |} |> shouldNotEqual {| Name = "John" |})

module ShouldFailTests =

    [<Fact>]
    let ``shouldFail passes when the function throws the expected exception``() =
        shouldFail<ArgumentNullException>(fun () -> null |> Array.max |> ignore)
        shouldFail<ArgumentException>(fun () -> [||] |> Array.randomChoice |> ignore)
        shouldFail<exn>(fun () -> failwith "Test failure")

    [<Fact>]
    let ``shouldFail fails when the function does not throw the expected exception``() =
        shouldFail<ThrowsException>(fun () -> shouldFail<ArgumentNullException>(fun () -> [|1|] |> Array.max |> ignore))
        shouldFail<ThrowsException>(fun () -> shouldFail<ArgumentException>(fun () -> [|1|] |> Array.randomChoice |> ignore))
        shouldFail<ThrowsException>(fun () -> shouldFail id)