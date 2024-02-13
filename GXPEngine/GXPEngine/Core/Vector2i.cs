namespace GXPEngine.Core
{
    public struct Vector2i
    {
        public int x;
        public int y;

        public Vector2i(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static Vector2i operator +(Vector2i a, Vector2i b)
        {
            return new Vector2i(b.x + a.x, a.y + b.y);
        }
        public static Vector2i operator -(Vector2i a, Vector2i b)
        {
            return new Vector2i(a.x - b.x, a.y - b.y);
        }
        public static Vector2i operator *(Vector2i a, int b)
        {
            return new Vector2i(a.x * b, a.y * b);
        }

        public static int Dot(Vector2i a, Vector2i b)
        {
            return a.x * b.x + a.y * b.y;
        }

        public int Dot(Vector2i b)
        {
            return x * b.x + y * b.y;
        }
        public float MagSquared()
        {
            return Dot(this);
        }
        override public string ToString()
        {
            return "[Vector2i " + x + ", " + y + "]";
        }
    }
}

