import pimpmycode.Grid
import pimpmycode.Location
import pimpmycode.MoveCommand

class Rover( startLocation : Location, grid : Grid){
    var currentLocation: Location = startLocation
    val grid : Grid = grid

    fun move(input: String): Location {
        if (input.isBlank())
            return currentLocation
        else {
            for (character in input) {
                moveSingle(character)
            }
            return currentLocation
        }

    }

    private fun moveSingle(input : Char) {
        val move = MoveCommand.valueOf(input.toString())

        when(move) {
            MoveCommand.F -> moveForward()
            MoveCommand.B -> moveBackward()
            MoveCommand.L -> moveLeft()
            MoveCommand.R -> moveRight()
        }


    }

    private fun moveRight() {
        currentLocation = Location(currentLocation.xLoc, currentLocation.yLoc, currentLocation.direction.right())

    }

    private fun moveLeft() {
        currentLocation = Location(currentLocation.xLoc, currentLocation.yLoc, currentLocation.direction.left())
    }

    private fun moveBackward() {
        return addToLocation(currentLocation.direction.backward())
    }


    private fun moveForward() {
        return addToLocation(currentLocation.direction.forward())
    }

    private fun addToLocation(input: Pair<Int, Int>) {
        currentLocation = Location(currentLocation.xLoc+input.first, currentLocation.yLoc+input.second, currentLocation.direction)
    }
}