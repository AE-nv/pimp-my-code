
import org.assertj.core.api.Assertions.assertThat
import org.junit.Test
import pimpmycode.Direction
import pimpmycode.Grid
import pimpmycode.Location

class RoverTest {

    private val defaultGrid = Grid(5, 5)
    private val startLocation = Location(3, 4, Direction.S)


    @Test
    fun `correct start location` () {
        val rover = createRover()
        assertThat(rover.currentLocation).isEqualToComparingFieldByField(startLocation)
    }

    @Test
    fun `no movent results in same location` () {
        val rover = createRover()
        val resultLocation = rover.move("")
        assertThat(resultLocation).isEqualToComparingFieldByField(startLocation)
    }

    @Test
    fun `Rover goes down when input is F and direction S` () {
        val rover = createRover()
        val result = rover.move("F")
        assertThat(result).isEqualToComparingFieldByField(Location(3,3, Direction.S))
    }

    @Test
    fun `Rover goes right when input is F and direction E` () {
        val rover = createRover()
        rover.currentLocation = Location(3,4, Direction.E)
        val result = rover.move("F")
        assertThat(result).isEqualToComparingFieldByField(Location(4,4, Direction.E))
    }

    @Test
    fun `Rover has same location when going back and forth` () {
        val rover = createRover()
        val result = rover.move("FB")
        assertThat(result).isEqualToComparingFieldByField(startLocation)
    }

    @Test
    fun `Rover has same direction when going left and right` () {
        val rover = createRover()
        val result = rover.move("LR")
        assertThat(result).isEqualToComparingFieldByField(startLocation)
    }



    @Test
    fun `Rover can move along a path` () {
        val rover = createRover()
        val result = rover.move("FFLBLFFFRF")
        assertThat(result).isEqualToComparingFieldByField(Location(3,5, Direction.E))
    }





    fun createRover() : Rover {
        return Rover(startLocation, defaultGrid)
    }
}