#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"

open FSharp.Data

let [<Literal>] log1 = """
{ 
  "lvl":"ERR",
  "message":"There was a problem"
}
"""

let [<Literal>] log2 = """
{ 
  "level":"ERROR",
  "msg":"There was a problem"
}
"""

type Log1 = JsonProvider< log1 >
type Log2 = JsonProvider< log2 >

type LogLevel = ERROR | INFO

type Log = 
    { Level : LogLevel
      Message : string }

module Log1 = 
    let parseLogLevel =
        function 
        | "ERR" -> Ok ERROR 
        | "INF" -> Ok INFO
        | other -> sprintf "unexpected loglevel %s" other |> Error
     
    let parse log = 
        let l = Log1.Parse log
        match parseLogLevel l.Lvl with
        | Ok lvl -> 
            { Level = lvl
              Message = l.Message } |> Ok
        | Error e -> Error e

module Log2 = 
    let parseLogLevel =
        function 
        | "ERROR" -> Ok ERROR 
        | "INFO" -> Ok INFO
        | other -> sprintf "unexpected loglevel %s" other |> Error
     
    let parse log = 
        let l = Log2.Parse log
        parseLogLevel l.Level
        |> Result.map (fun lvl -> { Level = lvl; Message = l.Msg })

let l1 = Log1.parse log1
let l2 = Log2.parse log2

l1 |> Result.bind (fun x -> l2 |> Result.map (fun y -> [x;y]))

let logs, errors = 
    [l1;l2] 
    |> List.fold (fun (result, errors) l -> 
        match l with 
        | Ok x -> x::result, errors 
        | Error e -> result, e::errors) (List.empty, List.empty)