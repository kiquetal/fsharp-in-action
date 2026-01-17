open System
open Microsoft.AspNetCore.Builder
open Microsoft.Extensions.DependencyInjection
open Microsoft.Extensions.Hosting
open Giraffe

let webApp =
    choose [
        GET >=> route "/" >=> text "Hello World from Giraffe on .NET 10!"
        GET >=> route "/ping" >=> text "pong"
    ]

[<EntryPoint>]
let main args =
    let builder = WebApplication.CreateBuilder(args)
    
    // Add Giraffe dependencies
    builder.Services.AddGiraffe() |> ignore

    let app = builder.Build()

    // Use Giraffe middleware
    app.UseGiraffe webApp

    app.Run()
    0
