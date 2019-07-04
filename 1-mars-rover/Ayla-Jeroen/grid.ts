export class Grid {
    private rover: Rover;

    constructor(
        private readonly width: number,
        private readonly height: number
    ) {
    }

    public configureRover(x: number, y: number, orientation: Orientation): Rover {
        this.rover = new Rover(x, y, orientation, this.width - 1, this.height - 1);
        return this.rover;
    }

    public moveRover(action: Action): void {
        if (action === 'F') {
            this.rover.moveForward();
        } else if (action === 'B') {
            this.rover.moveBackward();
        } else if (action === 'L') {
            this.rover.turnLeft();
        } else if (action === 'R') {
            this.rover.turnRight();
        }
        console.log(this.rover.getPosition());
    }

    public commandRover(actions: Action[]) {
        actions.forEach((action) => this.moveRover(action));
    }

    public getResult(): Position {
        return this.rover.getPosition();
    }
}

export class Rover {
    constructor(
        private x: number,
        private y: number,
        private orientation: Orientation,
        private max_x: number,
        private max_y: number
    ) {
    }

    public getPosition(): Position {
        return {x: this.x, y: this.y, orientation: this.orientation};
    }

    public moveForward(): void {
        this.moveOffset(1);
    }

    public moveBackward() {
        this.moveOffset(-1);
    }

    public turnLeft() {
        switch (this.orientation) {
            case Orientation.North:
                this.orientation = Orientation.West;
                break;
            case Orientation.East:
                this.orientation = Orientation.North;
                break;
            case Orientation.South:
                this.orientation = Orientation.East;
                break;
            case Orientation.West:
                this.orientation = Orientation.South;
                break;
        }
    }

    public turnRight() {
        this.turnLeft();
        this.turnLeft();
        this.turnLeft();
    }

    private moveOffset(offset: number) {
        switch (this.orientation) {
            case Orientation.North:
                this.y = (this.y + offset) % (this.max_y + 1);
                break;
            case Orientation.East:
                this.x = (this.x + offset) % (this.max_x + 1);
                break;
            case Orientation.South:
                this.y = (this.y - offset) % (this.max_y + 1);
                break;
            case Orientation.West:
                this.x = (this.x - offset) % (this.max_x + 1);
                break;
        }
        if (this.y < 0) {
            this.y = this.max_y;
        }
        if (this.x < 0) {
            this.x = this.max_x;
        }
    }
}

export enum Orientation {North, East, South, West}

export type Action = 'L' | 'R' | 'B' | 'F';
export type Position = { x: number, y: number, orientation: Orientation };
