# The mars rover kata

In this first Kata we're gonna build a Mars rover (http://kata-log.rocks/mars-rover-kata). 
Together with your solution, I'd like to ask you to ad a brief Readme.
How did you tackle the solution? Imporant design decisions? Did you run into issues? How did you solve them?
This is no solitary mission! 
Please feel to reach out in the **pimp my code** teams channel in the Build teams if you have any questions!
 
## Your Task
 
Create an API that can translate commands to the rover. The rover will execute these commands by moving around in a grid. The rover should return its position when it's finished executing its commands.

## Non functional requirements
- Focus of this kata is the 4 rules of simple design: https://martinfowler.com/bliki/BeckDesignRules.html
- This means ofcourse that your solution should be covered by tests.
 
## Starting Requirements
- The grid on which the rover moves is 5x5 in size.
- The starting point (x,y) of the rover, together with its orientation (N,S,E,W) are given (eg. (2,1,E)). The (0,0) coordinate is in the bottom left.
- The input commands are given as an array. Bv: [F, F, L, B , R , F, F]
- There are commands to drive forward and backward (F/B).
- There are commands to turn left or right (L/R).
- You can ignore faulty output.
 
## Additional requirements
- Make sure any (rectangular) format of grid is supported.
- Make sure the grid wraps, after all, planets are round.
- Implement obstacle detection: If your rover encounters an obstacle while executing its commands, it should broadcast its last possible position, as well as the fact that it encountered an obstacle.
  
## Example scenario's
![rover example](Rover.png?raw=true "Rover")
