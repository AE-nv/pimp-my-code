class Direction():
    North = 0
    East = 1
    South = 2
    West = 3

class Board():
    def __init__(self, maxx, maxy, obstacles=list()):
        self.maxx = maxx
        self.maxy = maxy

        self.obstacles = dict()
        for x,y in obstacles:
            if x not in self.obstacles:
                self.obstacles[x] = dict()
            self.obstacles[x][y] = True

    def checkObstacle(self, x,y):
        if x not in self.obstacles:
            return False
        if y not in self.obstacles[x]:
            return False
        return True


class Rover():
    dx = [0, 1,  0, -1]
    dy = [1, 0, -1,  0]

    def __init__(self, board, x, y, direction):
        self.board = board
        self.x, self.y = x, y
        self.direction = direction

    def handleCommands(self, commands):
        print(commands)

        for cmd in commands:
            newx, newy = self.x, self.y

            if cmd == "F":
                newx = (newx + Rover.dx[ self.direction ]) % self.board.maxx
                newy = (newy + Rover.dy[ self.direction ]) % self.board.maxy
            elif cmd == "B":
                newx = (newx - Rover.dx[ self.direction ]) % self.board.maxx
                newy = (newy - Rover.dy[ self.direction ]) % self.board.maxy
            elif cmd == "L":
                self.direction = (self.direction -1) % 4
            elif cmd == "R":
                self.direction = (self.direction +1) % 4

            if self.board.checkObstacle(newx, newy):
                print("ERROR", self.x, self.y)
                break

            self.x = newx
            self.y = newy

            print(cmd, self.x, self.y, self.direction)
