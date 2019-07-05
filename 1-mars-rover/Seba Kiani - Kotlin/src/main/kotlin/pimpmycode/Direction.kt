package pimpmycode

enum class Direction {
    E {
        override fun forward(): Pair<Int, Int> {
            return Pair(1, 0)
        }

        override fun backward(): Pair<Int, Int> {
            return Pair(-1,0)
        }

        override fun left(): Direction {
            return N
        }

        override fun right(): Direction {
            return S
        }
    },S {
        override fun forward(): Pair<Int, Int> {
            return Pair(0, -1)
        }

        override fun backward(): Pair<Int, Int> {
            return Pair(0, 1)
        }

        override fun left(): Direction {
            return E
        }

        override fun right(): Direction {
            return W
        }
    },W {
        override fun forward(): Pair<Int, Int> {
            return Pair(-1,0)
        }

        override fun backward(): Pair<Int, Int> {
            return Pair(1,0)
        }

        override fun left(): Direction {
            return S
        }

        override fun right(): Direction {
            return N
        }
    }, N {
        override fun forward(): Pair<Int, Int> {
            return Pair(0, 1)
        }

        override fun backward(): Pair<Int, Int> {
            return Pair(0, -1)
        }

        override fun left(): Direction {
            return W
        }

        override fun right(): Direction {
            return E
        }
    };

    abstract fun forward() : Pair<Int, Int>
    abstract fun backward() : Pair<Int, Int>
    abstract fun left() : Direction
    abstract fun right() : Direction
}