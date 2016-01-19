module plotting
open System.Drawing
open System.IO
open System

type plotter = {
    position: int * int
    colour: Color
    direction: float
    bitmap: Bitmap}

let naiveLine (x1, y1) plotter =
    // capture the plotter's properties
    let updatedPlotter = {plotter with position = (x1,y1)}
    let (x0, y0) = plotter.position

    // calculate the projections
    let xLen = float(x1 - x0)
    let yLen = float(y1 - y0)

    // correct any backwards movement
    let x0,y0,x1,y1 = if x0 > x1 then x1,y1,x0,y0 else x0,y0,x1,y1

    // draw the pixels 
    if xLen <> 0.0 then
        for x in x0..x1 do
            let propnx = float(x - x0) / xLen
            let y = (int(Math.Round(propnx * yLen))) + y0
            printfn "%i" y
            plotter.bitmap.SetPixel(x, y, plotter.colour)
    
    // correct any backwards movement
    let x0,y0,x1,y1 = if y0 > y1 then x1,y1,x0,y0 else x0,y0,x1,y1

    // draw the pixels
    if yLen <> 0.0 then
        for y in y0..y1 do
            let propny = float(y - y0) / yLen
            let x = (int(Math.Round(propny * xLen))) + x0
            printfn "%i" x
            plotter.bitmap.SetPixel(x, y, plotter.colour)
    // return the plotter
    updatedPlotter

let turn ang plotter = 
    let newDir = plotter.direction + ang
    let turned = { plotter with direction = newDir }
    printfn "%A" turned
    turned

let move dist plotter = 
    let curPos = plotter.position
    let ang = plotter.direction
    let startX = fst curPos
    let startY = snd curPos
    let rads = (ang - 90.0) * Math.PI/180.0
    let endX = (float startX) + (float dist) * cos rads
    let endY = (float startY) + (float dist) * sin rads
    let moved = naiveLine (int(Math.Round(endX)), int(Math.Round(endY))) plotter
    printfn "%A" moved
    moved

let rectangle x y plotter =
    plotter
    |> move x
    |> turn 90.0
    |> move y
    |> turn 90.0
    |> move x
    |> turn 90.0
    |> move y
    |> turn 90.0

let polygon (sides:int) length plotter =
    let angle = Math.Round(360.0/float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides)]

let semiCircle (sides:int) length plotter = 
    let angle = Math.Round(360.0/float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/2.0)]

let thirdCircle (sides:int) length plotter = 
    let angle = Math.Round(360.0/float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/3.0)]

let fifthCircle (sides:int) length plotter = 
    let angle = Math.Round(360.0/float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/5.0)]

let fifteenthCircle (sides:int) length plotter = 
    let angle = Math.Round(360.0/float sides)
    Seq.fold (fun s i -> turn angle (move length s)) plotter [1.0..(float sides/15.0)]

let moveTo (x1,y1) plotter = 
    { plotter with position = (x1,y1) }

let changeColour colour plotter = 
    { plotter with colour=colour }

let generate cmdStrip times fromPlotter = 
    let cmdGen = seq { while true do yield! cmdStrip }
    let imageCmds = cmdGen |> Seq.take (times*(List.length cmdStrip))
    imageCmds |> Seq.fold (fun plot cmds -> plot |> cmds) fromPlotter

let pathToFile p f = Path.Combine(p, f)

let saveAs name plotter =
    let path = @"C:\Dev\Study\FSharp\Spyro"
    plotter.bitmap.Save(pathToFile path name)