type Location = {x : int; y : int}
type Orientation = N | E | S | W
type Position = { location : Location; orientation : Orientation }
type Command = F | B | L | R

let forwardDelta position =
    match position.orientation with
    | N -> (0,1)
    | S -> (0,-1)
    | E -> (1,0)
    | W -> (-1,0)
    
let backwardDelta position =
    match position.orientation with
    | N -> (0,-1)
    | S -> (0,1)
    | E -> (-1,0)
    | W -> (1,0)

let orientationLeftOf = 
    function
    | N -> W
    | W -> S
    | S -> E
    | E -> N

let orientationRightOf = 
    function
    | N -> E
    | E -> S
    | S -> W
    | W -> N

let add location (dx,dy) = 
    { x = location.x + dx
      y = location.y + dy }

let locationForwardOf position =
    add position.location (forwardDelta position)

let locationBackwardOf position =
    add position.location (backwardDelta position)

let wrap (xMax, yMax) location = 
    let wrap x max = (x % max + max) % max

    { x = wrap location.x xMax
    ; y = wrap location.y yMax }

let detectCollisions obstacles startingPosition newPosition = 
    if obstacles |> Seq.exists ((=) newPosition.location)
    then Error startingPosition
    else Ok newPosition

let takeStep command currentPosition =
    match command with
    | F -> { currentPosition with location = locationForwardOf currentPosition } 
    | B -> { currentPosition with location = locationBackwardOf currentPosition }
    | L -> { currentPosition with orientation = orientationLeftOf currentPosition.orientation}
    | R -> { currentPosition with orientation = orientationRightOf currentPosition.orientation}

let executeCommand dimensions obstacles command currentPosition = 
    let wrap position = { position with location = wrap dimensions position.location }
    let checkCollisions = detectCollisions obstacles currentPosition

    takeStep command currentPosition
    |> wrap
    |> checkCollisions

let run dimensions startingPosition commands obstacles =
    let folder position command = position |> Result.bind (executeCommand dimensions obstacles command)
    commands |> Seq.fold folder (Ok startingPosition)

#r @".paket\packages\Unquote\lib\net45\Unquote.dll"
open Swensen.Unquote
printf "Testing..."
//Example scenario: wrapping grid, no obstacles
test <@ Ok     { location = { x = 1; y = 4}; orientation = W} = run (5,5) { location = { x = 1; y = 2 }; orientation = E } [F;L;F;L;B;L;F;F;R;F;R;B;B;L;F] []  @>
//Example scenario: wrapping grid, obstacles
test <@ Error  { location = { x = 2; y = 1}; orientation = N} = run (5,5) { location = { x = 1; y = 2 }; orientation = E } [F;L;F;L;B;L;F;F;R;F;R;B;B;L;F] [ {x = 0; y = 3}; {x = 2; y = 0}; {x = 4; y = 3}] @>
printfn "..done!"