module Ook.Cli

open System

type Action = Encode | Decode

type CliArgs = {
    action : Action
    text: String
}

let helpString = "Invalid arguments:\nUsage:\tOok.exe -[e / d] <text>\n\t-e\t\tEncode mode\n\t-d\t\tDecode Mode\n\n\t<text>\tText to encode / ciphertext to decode\n"

let GetCliArgs (argv:String[]) : CliArgs option =

    match argv |> Array.toList with
    | "-e"::xs -> 
        Some {
            action = Encode;
            text = (xs |> String.concat " ");
        }
    | "-d"::xs ->
        Some {
            action = Decode;
            text = (xs |> String.concat " ");
        }
    | _ -> 
        None

