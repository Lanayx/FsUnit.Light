namespace FsUnit.Light.MSTest.Tests

open System
open Microsoft.VisualStudio.TestTools.UnitTesting
open FsUnit.Light

[<TestClass>]
type ShouldBeEmptyTests() =

    [<TestMethod>]
    member _.``empty collections should be Empty``() =
        [] |> shouldBeEmpty
        [||] |> shouldBeEmpty
        Seq.empty |> shouldBeEmpty

    [<TestMethod>]
    member _.``non-empty collections fail to be Empty``() =
        shouldFail<AssertFailedException>(fun () -> [ 1 ] |> shouldBeEmpty)
        shouldFail<AssertFailedException>(fun () -> [| 1 |] |> shouldBeEmpty)
        shouldFail<AssertFailedException>(fun () -> seq { 1 } |> shouldBeEmpty)

[<TestClass>]
type ShouldHaveLengthTests() =

    [<TestMethod>]
    member _.``collections with expected length should pass``() =
        [ 1 ] |> shouldHaveLength 1
        seq { 1 } |> shouldHaveLength 1
        "a" |> shouldHaveLength 1

    [<TestMethod>]
    member _.``collections with unexpected length fail``() =
        shouldFail<AssertFailedException>(fun () -> [] |> shouldHaveLength 1)
        shouldFail<AssertFailedException>(fun () -> seq {} |> shouldHaveLength 1)
        shouldFail<AssertFailedException>(fun () -> "" |> shouldHaveLength 1)

[<TestClass>]
type ShouldBeGreaterThanTests() =

    [<TestMethod>]
    member _.``shouldBeGreaterThan passes for greater values``() =
        2 |> shouldBeGreaterThan 1
        "b" |> shouldBeGreaterThan "a"
        2.0 |> shouldBeGreaterThan 1.0

    [<TestMethod>]
    member _.``shouldBeGreaterThan fails for lesser or equal values``() =
        shouldFail<AssertFailedException>(fun () -> 1 |> shouldBeGreaterThan 2)
        shouldFail<AssertFailedException>(fun () -> "a" |> shouldBeGreaterThan "a")
        shouldFail<AssertFailedException>(fun () -> 1.0 |> shouldBeGreaterThan 2.0)

[<TestClass>]
type ShouldBeSmallerThanTests() =

    [<TestMethod>]
    member _.``shouldBeSmallerThan passes for smaller values``() =
        1 |> shouldBeSmallerThan 2
        "a" |> shouldBeSmallerThan "b"
        1.0 |> shouldBeSmallerThan 2.0

    [<TestMethod>]
    member _.``shouldBeSmallerThan fails for greater or equal values``() =
        shouldFail<AssertFailedException>(fun () -> 2 |> shouldBeSmallerThan 1)
        shouldFail<AssertFailedException>(fun () -> "a" |> shouldBeSmallerThan "a")
        shouldFail<AssertFailedException>(fun () -> 2.0 |> shouldBeSmallerThan 1.0)

[<TestClass>]
type ShouldContainTests() =

    [<TestMethod>]
    member _.``shouldContain passes for contained values``() =
        [ 1; 2; 3 ] |> shouldContain 2
        "hello" |> shouldContain 'e'
        [| 1; 2; 3 |] |> shouldContain 2

    [<TestMethod>]
    member _.``shouldContain fails for non-contained values``() =
        shouldFail<AssertFailedException>(fun () -> [ 1; 2; 3 ] |> shouldContain 4)
        shouldFail<AssertFailedException>(fun () -> "hello" |> shouldContain 'x')
        shouldFail<AssertFailedException>(fun () -> [| 1; 2; 3 |] |> shouldContain 4)

[<TestClass>]
type ShouldNotContainTests() =

    [<TestMethod>]
    member _.``shouldNotContain passes for non-contained values``() =
        [ 1; 2; 3 ] |> shouldNotContain 4
        "hello" |> shouldNotContain 'x'
        [||] |> shouldNotContain ""

    [<TestMethod>]
    member _.``shouldNotContain fails for contained values``() =
        shouldFail<AssertFailedException>(fun () -> [ 1; 2; 3 ] |> shouldNotContain 2)
        shouldFail<AssertFailedException>(fun () -> "hello" |> shouldNotContain 'e')
        shouldFail<AssertFailedException>(fun () -> [| 1; 2; 3 |] |> shouldNotContain 2)

[<TestClass>]
type ShouldContainTextTests() =

    [<TestMethod>]
    member _.``shouldContainText passes for contained text``() =
        "hello world" |> shouldContainText "world"
        "hello" |> shouldContainText ""

    [<TestMethod>]
    member _.``shouldContainText fails for non-contained text``() =
        shouldFail<AssertFailedException>(fun () -> "hello world" |> shouldContainText "planet")
        shouldFail<AssertFailedException>(fun () -> "" |> shouldContainText " ")

[<TestClass>]
type ShouldNotContainTextTests() =

    [<TestMethod>]
    member _.``shouldNotContainText passes for non-contained text``() =
        "hello world" |> shouldNotContainText "planet"
        "" |> shouldNotContainText " "

    [<TestMethod>]
    member _.``shouldNotContainText fails for contained text``() =
        shouldFail<AssertFailedException>(fun () -> "hello world" |> shouldNotContainText "world")
        shouldFail<AssertFailedException>(fun () -> "" |> shouldNotContainText "")

type Item() =
    member val Id = "" with get, set

[<TestClass>]
type ShouldEqualTests() =

    [<TestMethod>]
    member _.``shouldEqual passes for equal values``() =
        1 |> shouldEqual 1
        2.0 |> shouldEqual 2.0
        "hello" |> shouldEqual "hello"
        [ 1; 2; 3 ] |> shouldEqual [ 1; 2; 3 ]
        //[| 1; 2; 3 |] |> shouldEqual [| 1; 2; 3 |]
        //seq { 1; 2; 3 } |> shouldEqual (seq { 1; 2; 3 })
        Map.ofList [ "a", 1; "b", 2 ] |> shouldEqual (Map.ofList [ "b", 2; "a", 1 ])
        null |> shouldEqual null
        let x = obj() in x |> shouldEqual x
        {| Name = "John" |} |> shouldEqual {| Name = "John" |}

    [<TestMethod>]
    member _.``shouldEqual fails for unequal values``() =
        shouldFail<AssertFailedException>(fun () -> 1 |> shouldEqual 2)
        shouldFail<AssertFailedException>(fun () -> 2.0 |> shouldEqual 3.0)
        shouldFail<AssertFailedException>(fun () -> "hello" |> shouldEqual "world")
        shouldFail<AssertFailedException>(fun () -> [ 1; 2; 3 ] |> shouldEqual [ 4; 5; 6 ])
        shouldFail<AssertFailedException>(fun () -> obj() |> shouldEqual (obj()))
        shouldFail<AssertFailedException>(fun () -> {| Name = "John" |} |> shouldEqual {| Name = "Jack" |})
        shouldFail<AssertFailedException>(fun () -> Item(Id = "1") |> shouldEqual (Item(Id = "1")))

[<TestClass>]
type ShouldNotEqualTests() =

    [<TestMethod>]
    member _.``shouldNotEqual passes for unequal values``() =
        1 |> shouldNotEqual 2
        2.0 |> shouldNotEqual 3.0
        "hello" |> shouldNotEqual "world"
        [ 1; 2; 3 ] |> shouldNotEqual [ 3; 2; 1 ]
        // [| 1; 2; 3 |] |> shouldNotEqual [| 4; 5; 6 |]
        // seq { 1; 2; 3 } |> shouldNotEqual (seq { 4; 5; 6 })
        Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "c", 3 ])
        null |> shouldNotEqual (obj())
        let x = obj() in x |> shouldNotEqual (obj())
        {| Name = "John" |} |> shouldNotEqual {| Name = "Jack" |}
        Item(Id = "1") |> shouldNotEqual (Item(Id = "1"))

    [<TestMethod>]
    member _.``shouldNotEqual fails for equal values``() =
        shouldFail<AssertFailedException>(fun () -> 1 |> shouldNotEqual 1)
        shouldFail<AssertFailedException>(fun () -> "hello" |> shouldNotEqual "hello")
        shouldFail<AssertFailedException>(fun () -> [ 1; 2; 3 ] |> shouldNotEqual [ 1; 2; 3 ])
        shouldFail<AssertFailedException>(fun () -> Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "b", 2; "a", 1 ]))
        shouldFail<AssertFailedException>(fun () -> null |> shouldNotEqual null)
        shouldFail<AssertFailedException>(fun () -> let x = obj() in x |> shouldNotEqual x)
        shouldFail<AssertFailedException>(fun () -> {| Name = "John" |} |> shouldNotEqual {| Name = "John" |})

[<TestClass>]
type ShouldFailTests() =

    [<TestMethod>]
    member _.``shouldFail passes when the function throws the expected exception``() =
        (fun () -> null |> Array.max |> ignore)
        |> shouldFail<ArgumentNullException>
        (fun () -> [||] |> Array.randomChoice |> ignore)
        |> shouldFail<ArgumentException>
        (fun () -> failwith "Test failure")
        |> shouldFail<exn>

    [<TestMethod>]
    member _.``shouldFail fails when the function does not throw the expected exception``() =
        shouldFail<AssertFailedException>(fun () ->
            (fun () -> [|1|] |> Array.max |> ignore)
            |> shouldFail<ArgumentNullException>
        )
        shouldFail<AssertFailedException>(fun () ->
            (fun () -> [|1|] |> Array.randomChoice |> ignore)
            |> shouldFail<ArgumentException>
        )
        shouldFail<AssertFailedException>(fun () -> id |> shouldFail)

[<TestClass>]
type ShouldFailWithMessageTests() =

    [<TestMethod>]
    member _.``shouldFailWithMessage passes when the function throws the expected exception``() =
        (fun () -> null |> Array.max |> ignore)
        |> shouldFailWithMessage<ArgumentNullException> "Value cannot be null. (Parameter 'array')"
        (fun () -> [||] |> Array.randomChoice |> ignore)
        |> shouldFailWithMessage<ArgumentException> "The input array was empty. (Parameter 'source')"
        (fun () -> failwith "Test failure")
        |> shouldFailWithMessage "Test failure"

    [<TestMethod>]
    member _.``shouldFailWithMessage fails when the function does not throw the expected exception``() =
        shouldFail<AssertFailedException>(fun () ->
            (fun () -> null |> Array.max |> ignore)
            |> shouldFailWithMessage<ArgumentNullException> "Wrong exception message."
        )
        shouldFail<AssertFailedException>(fun () ->
            (fun () -> [||] |> Array.randomChoice |> ignore)
            |> shouldFailWithMessage<ArgumentException> "Wrong exception message."
        )
        shouldFail<AssertFailedException>(fun () ->
            (fun () -> failwith "Test failure")
            |> shouldFailWithMessage "Wrong exception message.")
        shouldFail<AssertFailedException>(fun () -> id |> shouldFailWithMessage "")

[<TestClass>]
type ShouldEquivalentTests() =

    [<TestMethod>]
    member _.``shouldEquivalent passes for equivalent values``() =
        null |> shouldEquivalent null
        obj() |> shouldEquivalent (obj())
        1 |> shouldEquivalent 1
        [ 1; 2; 3 ] |> shouldEquivalent [ 3; 2; 1 ]
        Item(Id = "1") |> shouldEquivalent (Item(Id = "1"))

    [<TestMethod>]
    member _.``shouldEquivalent fails for non-equivalent values``() =
        shouldFail<AssertFailedException>(fun () -> 1 |> shouldEquivalent 2)
        shouldFail<AssertFailedException>(fun () -> Item() |> shouldEquivalent (Item(Id = null)))
        shouldFail<AssertFailedException>(fun () -> [ 1; 2; 3 ] |> shouldEquivalent [ 1; 2 ])
        shouldFail<AssertFailedException>(fun () -> [| 1; 2; 3 |] |> shouldEquivalent [| 1; 2; 2; 3 |])
        shouldFail<AssertFailedException>(fun () -> seq { 1; 2; 3 } |> shouldEquivalent (seq { 1; 2; 3; 4 }))
