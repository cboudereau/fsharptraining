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

let logs = 
    let l1 = 
        let l = Log1.Parse log1
        { Level = match l.Lvl with "ERR" -> ERROR | _ -> INFO
          Message = l.Message }
    let l2 = 
        let l = Log2.Parse log2
        { Level = match l.Level with "ERROR" -> ERROR | _ -> INFO
          Message = l.Msg }
    [l1;l2]