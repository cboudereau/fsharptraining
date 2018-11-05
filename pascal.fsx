//https://www.youtube.com/watch?v=6JGrHD5nAoc

fsi.AddPrinter<int list list> (List.map (sprintf "%A") >> fun x -> System.String.Join(System.Environment.NewLine, x) |> sprintf "%s%s" System.Environment.NewLine)

let pascal n =
    let rec _pascal accu x top =
        match x with
        | x when x = top -> accu
        | x -> let nv = let h = List.head accu 
                        List.map2 (+) (h @ [0]) (0 :: h) 
               _pascal (nv :: accu)  (x + 1) top
    _pascal [[1]] 1 n

pascal 11

let coefficient n p = 
    let rec coef top (head:int list) = 
        let newHead = List.map2 (+) (head @ [0]) (0 :: head) 
        if n = top then newHead.[p - 1]
        else
            coef (top + 1) newHead
    coef 1 [1]

coefficient 10 5
coefficient 3 2 
coefficient 4 3 = 6 
