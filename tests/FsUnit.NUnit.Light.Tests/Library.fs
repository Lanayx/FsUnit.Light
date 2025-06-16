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
