public struct GridPoint
{
    public int X { get; set; }
    public int Y { get; set; }

    public GridPoint(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator ==(GridPoint first, GridPoint second)
    {
        return first.X == second.X && first.Y == second.Y;
    }

    public static bool operator !=(GridPoint first, GridPoint second)
    {
        return first.X != second.X || first.Y != second.Y;
    }
}