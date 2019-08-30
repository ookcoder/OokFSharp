// Learn more about F# at http://fsharp.org

open System

type Action = Encode | Decode

type CliArgs = {
    action : Action
    text: String
}

type CliArgumentException(msg) = inherit Exception(msg)
type MappingException(msg) = inherit Exception(msg)
type NotYetImplementedException(msg) = inherit Exception(msg)

let GetCliArgs argv : CliArgs option =
    match argv with
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
        Console.Out.WriteLine("Invalid arguments:\nUsage:\tOok.exe -[e / d] <text>\n\t-e\t\tEncode mode\n\t-d\t\tDecode Mode\n\n\t<text>\tText to encode / ciphertext to decode\n")
        None


let cipherMap = Collections.Map [
    ("a", "oO");
    ("b", "Oooo");
    ("c", "OoOo");
    ("d", "Ooo");
    ("e", "o");
    ("f", "ooOo");
    ("g", "OOo");
    ("h", "oooo");
    ("i", "oo");
    ("j", "oOOO");
    ("k", "OoO");
    ("l", "oOoo");
    ("m", "OO");
    ("n", "Oo");
    ("o", "OOO");
    ("p", "oOOo");
    ("q", "OOoO");
    ("r", "oOo");
    ("s", "ooo");
    ("t", "O");
    ("u", "ooO");
    ("v", "oooO");
    ("w", "oOO");
    ("x", "OooO");
    ("y", "OoOO");
    ("z", "OOoo");
    (" ", "k ");
    ("0", "OOOOO");
    ("1", "oOOOO");
    ("2", "ooOOO");
    ("3", "oooOO");
    ("4", "ooooO");
    ("5", "ooooo");
    ("6", "Ooooo");
    ("7", "OOooo");
    ("8", "OOOoo");
    ("9", "OOOOo");
]

let rev map: Map<string,string> = 
    Map.fold (fun m key value -> m.Add(value,key)) Map.empty map

let append a b =
    Array.append b a

[<EntryPoint>]
let main argv =
    
    let args = argv |> Array.toList |> GetCliArgs
    
    match args with
    | Some x ->
        match x.action with
        | Encode ->
            (x.text.ToCharArray()
            |> Array.filter (fun x -> x |> Char.IsLetterOrDigit || x |> Char.IsSeparator)
            |> Array.map (fun x ->
                x |> Char.ToLower |> Char.ToString |> cipherMap.TryGetValue |> snd
            )
            |> String.concat "0"
            ) + "k"

        | Decode ->
            x.text.Split("0")
            |> Array.filter (fun x -> x = "k" |> not)
            |> Array.map (fun x -> x.TrimEnd('k'))
            |> Array.map (fun x ->
                let successful, result = x |> (cipherMap |> rev).TryGetValue
                if not successful 
                    then
                        raise (MappingException("Invalid ciphertext: \"" + x + "\" does not have a valid reverse mapping\n"))
                    else result
            )
            |> String.concat ""

        |> Console.Out.WriteLine

    | None -> Console.Out.WriteLine "Invalid arguments:\nUsage:\tOok.exe -[e / d] <text>\n\t-e\t\tEncode mode\n\t-d\t\tDecode Mode\n\n\t<text>\tText to encode / ciphertext to decode\n"

    0
