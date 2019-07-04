import {Grid, Orientation} from "./grid";

test('Aylas first test', () => {
    const grid = new Grid(7, 7);
    grid.configureRover(3, 3, Orientation.North);

    expect(grid.getResult()).toEqual({x: 3, y: 3, orientation: Orientation.North});
});

test('Aylas seconds test', () => {
    const grid = new Grid(7, 7);
    grid.configureRover(3, 3, Orientation.North);

    grid.moveRover('F');

    expect(grid.getResult()).toEqual({x: 3, y: 4, orientation: Orientation.North});
});

test('Aylas thirdsies test', () => {
    const grid = new Grid(7, 7);
    grid.configureRover(3, 3, Orientation.North);

    grid.moveRover('F');
    grid.moveRover('B');

    expect(grid.getResult()).toEqual({x: 3, y: 3, orientation: Orientation.North});
});

test('xzibit!', () => {
    const grid = new Grid(1, 2);
    grid.configureRover(0, 1, Orientation.North);

    grid.moveRover('F');

    expect(grid.getResult()).toEqual({x: 0, y: 0, orientation: Orientation.North});
});

test('turn a left!', () => {
    const grid = new Grid(1, 2);
    grid.configureRover(0, 1, Orientation.North);

    grid.moveRover('L');

    expect(grid.getResult()).toEqual({x: 0, y: 1, orientation: Orientation.West});
});

test('turn a right!', () => {
    const grid = new Grid(1, 2);
    grid.configureRover(0, 1, Orientation.North);

    grid.moveRover('R');

    expect(grid.getResult()).toEqual({x: 0, y: 1, orientation: Orientation.East});
});

test('move a lot!', () => {
    const grid = new Grid(5, 5);
    grid.configureRover(1, 2, Orientation.East);

    grid.commandRover(['F', 'L', 'F', 'L', 'B', 'L', 'F', 'F', 'R', 'F', 'R', 'B', 'B', 'L', 'F']);

    expect(grid.getResult()).toEqual({x: 1, y: 4, orientation: Orientation.West});
});
