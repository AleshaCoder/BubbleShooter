namespace Services.MapGenerator
{
    public struct Position
    {
        public int X, Y;
        public Position(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator ==(Position obj1, Position obj2)
        {
            if (obj1.X == obj2.X && obj1.Y == obj2.Y)
                return true;
            return false;
        }

        public static bool operator !=(Position obj1, Position obj2)
        {
            return !(obj1 == obj2);
        }

        public static Position Zero => new Position(0, 0);
    }
}
