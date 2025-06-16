namespace FsUnit.Xunit.Light.Tests

open Xunit
open Xunit.Sdk
open FsUnit.Xunit.Light

module ShouldBeEmptyTests =

    [<Fact>]
    let ``empty List should be Empty``() =
        [] |> shouldBeEmpty

    [<Fact>]
    let ``non-empty List should fail to be Empty``() =
        shouldFail<EmptyException>(fun () -> [ 1 ] |> shouldBeEmpty)

    [<Fact>]
    let ``empty Array should be Empty``() =
        [||] |> shouldBeEmpty

    [<Fact>]
    let ``non-empty Array should fail to be Empty``() =
        shouldFail<EmptyException>(fun () -> [| 1 |] |> shouldBeEmpty)

    [<Fact>]
    let ``empty Seq should be Empty``() =
        Seq.empty |> shouldBeEmpty

    [<Fact>]
    let ``non-empty Seq should fail to be Empty``() =
        shouldFail<EmptyException>(fun () -> seq { 1 } |> shouldBeEmpty)