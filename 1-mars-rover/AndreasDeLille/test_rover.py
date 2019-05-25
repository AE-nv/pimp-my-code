import pytest
from rover import Rover, Direction, Board


def test_commands_no_obstacles():

    cases = [{
            'x': 1, 'y': 2, 'direction': Direction.North,
            'commands': "LFLFLFLFF",
            'result_x': 1, 'result_y': 3, 'result_direction': Direction.North,
        }, {
            'x': 3, 'y': 3, 'direction': Direction.East,
            'commands': "FFRFFRFRRF",
            'result_x': 0, 'result_y': 1, 'result_direction': Direction.East,
        }, {
            'x': 1, 'y': 2, 'direction': Direction.East,
            'commands': "FLFLBLFFRFRBBLF",
            'result_x': 1, 'result_y': 4, 'result_direction': Direction.West,
        }
    ]

    for case in cases:
        board = Board(5,5)
        rover = Rover(board, case["x"], case["y"], case["direction"])
        rover.handleCommands(case["commands"])

        assert rover.x == case["result_x"]
        assert rover.y == case["result_y"]
        assert rover.direction == case["result_direction"]

def test_faulty_input():
    board = Board(5,5)
    rover = Rover(board, 2, 2, Direction.West)
    rover.handleCommands("0123456789ZETUWX-?")
    assert rover.x == 2
    assert rover.y == 2
    assert rover.direction == Direction.West

def test_commands_obstacles():
    cases = [{
            'x': 1, 'y': 2, 'direction': Direction.East,
            'commands': "FLFLBLFFRFRBBLF",
            'obstacles': [(0,3),(2,0),(4,3)],
            'result_x': 2, 'result_y': 1, 'result_direction': Direction.North,
        }
    ]

    for case in cases:
        board = Board(5,5,case["obstacles"])
        rover = Rover(board, case["x"], case["y"], case["direction"])
        rover.handleCommands(case["commands"])

        assert rover.x == case["result_x"]
        assert rover.y == case["result_y"]
        assert rover.direction == case["result_direction"]

