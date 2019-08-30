module Ook.Main

open System
open Cli
open Cipher

let sanitise c = c |> Char.ToLower |> Char.ToString

[<EntryPoint>]
let main argv =
    
    match argv |> GetCliArgs with
    | None -> Console.Out.WriteLine helpString
    | Some x ->
        match x.action with
        | Encode ->
            (x.text.ToCharArray()
            |> Array.map (fun x -> x |> sanitise |> applyCipher Encode)
            |> String.concat "0"
            ) + "k"

        | Decode ->
            x.text.Split("0")
            |> Array.map (fun x -> x.TrimEnd('k') |> applyCipher Decode)
            |> String.concat ""

        |> Console.Out.WriteLine

    0
