#load "plotting.fsx"
open plotting
open System.Drawing
open System.IO

let bitmap = new Bitmap(2000,2000)

let (initialPlotter:plotter) = {
    position  = (1000,1000)
    colour    = Color.DarkGoldenrod
    direction = 0.0
    bitmap    = bitmap }

let cmdStrip = 
    [ changeColour Color.Orange
      move 51
      turn 12.3
      move 50
      turn 9.1
      move 100
      turn 95.1
      fifteenthCircle 20 20
      changeColour Color.Navy
      move 73
      polygon 7 200
      changeColour Color.PeachPuff
      move 173
      turn -23.1
      move 200
      fifthCircle 20 30
      changeColour Color.MediumVioletRed
      move 2
      changeColour Color.MidnightBlue
      turn 43.3
      move 57
      turn -173.3
      move 200
      changeColour Color.LimeGreen
      turn 87.4
      move 10
      turn -151.1
      semiCircle 30 30
      turn 101.1
      changeColour Color.DarkTurquoise
      move 100
      turn 10.1
      move 20
      turn 10.2
      move 20
      polygon 3 100
      moveTo (1000,1000)
     ]

generate cmdStrip 250 initialPlotter |> saveAs "xpm4.png"
