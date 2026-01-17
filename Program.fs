open fsharp_in_action

[<EntryPoint>]
let main argv =
    match argv with
    | [| "2" |] ->
        ChapterTwo.run()
        0
    | _ ->
        printfn "Please specify a chapter number (e.g., dotnet run -- 2)"
        0