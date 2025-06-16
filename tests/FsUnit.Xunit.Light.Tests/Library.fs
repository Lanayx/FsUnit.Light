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