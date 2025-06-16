namespace FsUnit.Xunit.Light.Tests

open NUnit.Framework
open FsUnit.NUnit.Light

module ShouldBeEmptyTests =

    [<Test>]
    let ``empty List should be Empty``() =
        [] |> shouldBeEmpty

    [<Test>]
    let ``non-empty List should fail to be Empty``() =
        shouldFail<AssertionException>(fun () -> [ 1 ] |> shouldBeEmpty)

    [<Test>]
    let ``empty Array should be Empty``() =
        [||] |> shouldBeEmpty

    [<Test>]
    let ``non-empty Array should fail to be Empty``() =
        shouldFail<AssertionException>(fun () -> [| 1 |] |> shouldBeEmpty)

    [<Test>]
    let ``empty Seq should be Empty``() =
        Seq.empty |> shouldBeEmpty

    [<Test>]
    let ``non-empty Seq should fail to be Empty``() =
        shouldFail<AssertionException>(fun () -> seq { 1 } |> shouldBeEmpty)