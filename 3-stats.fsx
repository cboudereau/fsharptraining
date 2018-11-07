#r "packages/FSharp.Data/lib/net45/FSharp.Data.dll"

#I "packages/Google.DataTable.Net.Wrapper/lib/"
#I "packages/XPlot.GoogleCharts/lib/net45/"
#r "XPlot.GoogleCharts.dll"

open FSharp.Data

let worldbank = WorldBankData.GetDataContext()
let internet = worldbank.Regions.``European Union``.Countries.France.Indicators.``Individuals using the Internet (% of population)``

open XPlot.GoogleCharts
List.map2 (fun year v -> year, v) (internet.Years |> Seq.map (fun y -> System.DateTime(y,1,1,0,0,0,System.DateTimeKind.Utc)) |> Seq.toList) (internet.Values |> Seq.toList)
|> Chart.Column
|> Chart.WithXTitle "Year"
|> Chart.WithYTitle "Volume"
|> Chart.WithTitle "People using Internet over Years"
|> Chart.Show


type LogLevel = ERROR | INFO
type Log = 
    { Level : LogLevel
      Message : string }

let log lvl _ = { Level=lvl; Message = "hello" }

let logs = 
    [ (1000,LogLevel.INFO)
      (300, LogLevel.ERROR) ]
    |> List.collect (fun (n, level) -> Seq.init n (log level) |> Seq.toList)

logs 
|> List.map (fun log -> string log.Level)
|> List.countBy id
|> Chart.Column
|> Chart.WithXTitle "Log level"
|> Chart.WithYTitle "Count"
|> Chart.WithTitle "Log distribution"
|> Chart.Show

