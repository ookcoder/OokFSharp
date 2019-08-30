module Ook.Cipher

open System
open Cli

type MappingException(msg) = inherit Exception(msg)

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
    ("", "");
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

let applyCipher action text =
    match action with
    | Encode -> 
        let successful, cipher = text |> cipherMap.TryGetValue
        if not successful then ""
        else cipher
    | Decode -> 
        let successful, plaintext = text |> (cipherMap |> rev).TryGetValue
        if not successful then raise (MappingException("Invalid ciphertext: \"" + text + "\" does not have a valid reverse mapping\n"))
        else plaintext


