module fsharp_in_action.ChapterTwo


let greet name =
    sprintf "Hello, %s!" name

let run () =
    let greeting = greet "F# Developer"
    printfn "%s" greeting