namespace FsUnit.Light.NUnit.Tests

open System
open NUnit.Framework
open FsUnit.Light

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

module ShouldEqualTests =

    type Item() =
        member val Id = "" with get, set

    [<Test>]
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

    [<Test>]
    let ``shouldEqual fails for unequal values``() =
        shouldFail<AssertionException>(fun () -> 1 |> shouldEqual 2)
        shouldFail<AssertionException>(fun () -> 2.0 |> shouldEqual 3.0)
        shouldFail<AssertionException>(fun () -> "hello" |> shouldEqual "world")
        shouldFail<AssertionException>(fun () -> [ 1; 2; 3 ] |> shouldEqual [ 4; 5; 6 ])
        shouldFail<AssertionException>(fun () -> obj() |> shouldEqual (obj()))
        shouldFail<AssertionException>(fun () -> {| Name = "John" |} |> shouldEqual {| Name = "Jack" |})
        shouldFail<AssertionException>(fun () -> Item(Id = "1") |> shouldEqual (Item(Id = "1")))

module ShouldNotEqualTests =

    type Item() =
        member val Id = "" with get, set

    [<Test>]
    let ``shouldNotEqual passes for unequal values``() =
        1 |> shouldNotEqual 2
        2.0 |> shouldNotEqual 3.0
        "hello" |> shouldNotEqual "world"
        [ 1; 2; 3 ] |> shouldNotEqual [ 3; 2; 1 ]
        [| 1; 2; 3 |] |> shouldNotEqual [| 4; 5; 6 |]
        seq { 1; 2; 3 } |> shouldNotEqual (seq { 4; 5; 6 })
        Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "c", 3 ])
        null |> shouldNotEqual (obj())
        let x = obj() in x |> shouldNotEqual (obj())
        {| Name = "John" |} |> shouldNotEqual {| Name = "Jack" |}
        Item(Id = "1") |> shouldNotEqual (Item(Id = "1"))

    [<Test>]
    let ``shouldNotEqual fails for equal values``() =
        shouldFail<AssertionException>(fun () -> 1 |> shouldNotEqual 1)
        shouldFail<AssertionException>(fun () -> "hello" |> shouldNotEqual "hello")
        shouldFail<AssertionException>(fun () -> [ 1; 2; 3 ] |> shouldNotEqual [ 1; 2; 3 ])
        shouldFail<AssertionException>(fun () -> [| 1; 2; 3 |] |> shouldNotEqual [| 1; 2; 3 |])
        shouldFail<AssertionException>(fun () -> seq { 1; 2; 3 } |> shouldNotEqual (seq { 1; 2; 3 }))
        shouldFail<AssertionException>(fun () -> Map.ofList [ "a", 1; "b", 2 ] |> shouldNotEqual (Map.ofList [ "b", 2; "a", 1 ]))
        shouldFail<AssertionException>(fun () -> null |> shouldNotEqual null)
        shouldFail<AssertionException>(fun () -> let x = obj() in x |> shouldNotEqual x)
        shouldFail<AssertionException>(fun () -> {| Name = "John" |} |> shouldNotEqual {| Name = "John" |})

module ShouldFailTests =

    [<Test>]
    let ``shouldFail passes when the function throws the expected exception``() =
        (fun () -> null |> Array.max |> ignore)
        |> shouldFail<ArgumentNullException>
        (fun () -> [||] |> Array.randomChoice |> ignore)
        |> shouldFail<ArgumentException>
        (fun () -> failwith "Test failure")
        |> shouldFail<exn>

    [<Test>]
    let ``shouldFail fails when the function does not throw the expected exception``() =
        shouldFail<AssertionException>(fun () ->
            (fun () -> [|1|] |> Array.max |> ignore)
            |> shouldFail<ArgumentNullException>
        )
        shouldFail<AssertionException>(fun () ->
            (fun () -> [|1|] |> Array.randomChoice |> ignore)
            |> shouldFail<ArgumentException>
        )
        shouldFail<AssertionException>(fun () -> id |> shouldFail)

module ShouldFailWithMessageTests =

    [<Test>]
    let ``shouldFailWithMessage passes when the function throws the expected exception``() =
        (fun () -> null |> Array.max |> ignore)
        |> shouldFailWithMessage<ArgumentNullException> "Value cannot be null. (Parameter 'array')"
        (fun () -> [||] |> Array.randomChoice |> ignore)
        |> shouldFailWithMessage<ArgumentException> "The input array was empty. (Parameter 'source')"
        (fun () -> failwith "Test failure")
        |> shouldFailWithMessage "Test failure"

    [<Test>]
    let ``shouldFailWithMessage fails when the function does not throw the expected exception``() =
        shouldFail<AssertionException>(fun () ->
            (fun () -> null |> Array.max |> ignore)
            |> shouldFailWithMessage<ArgumentNullException> "Wrong exception message."
        )
        shouldFail<AssertionException>(fun () ->
            (fun () -> [||] |> Array.randomChoice |> ignore)
            |> shouldFailWithMessage<ArgumentException> "Wrong exception message."
        )
        shouldFail<AssertionException>(fun () ->
            (fun () -> failwith "Test failure")
            |> shouldFailWithMessage "Wrong exception message.")
        shouldFail<AssertionException>(fun () -> id |> shouldFailWithMessage "")

module ShouldEquivalentTests =

    type Item() =
        member val Id = "" with get, set

    [<Test>]
    let ``shouldEquivalent passes for equivalent values``() =
        null |> shouldEquivalent null
        //obj() |> shouldEquivalent (obj())
        1 |> shouldEquivalent 1
        [ 1; 2; 3 ] |> shouldEquivalent [ 3; 2; 1 ]
        Item(Id = "1") |> shouldEquivalent (Item(Id = "1"))

    [<Test>]
    let ``shouldEquivalent fails for non-equivalent values``() =
        shouldFail<AssertionException>(fun () -> 1 |> shouldEquivalent 2)
        shouldFail<AssertionException>(fun () -> Item() |> shouldEquivalent (Item(Id = null)))
        shouldFail<AssertionException>(fun () -> [ 1; 2; 3 ] |> shouldEquivalent [ 1; 2 ])
        shouldFail<AssertionException>(fun () -> [| 1; 2; 3 |] |> shouldEquivalent [| 1; 2; 2; 3 |])
        shouldFail<AssertionException>(fun () -> seq { 1; 2; 3 } |> shouldEquivalent (seq { 1; 2; 3; 4 }))