using System.Collections.Generic;

public class MapCell
{
    private readonly int _id;
    private readonly List<MapCell> _neighbours;

    private Position _position;

    public int Id => _id;
    public Position Position => _position;
    public IReadOnlyCollection<MapCell> Neighbours => _neighbours;

    public MapCell(int id)
    {
        _id = id;
        _neighbours = new List<MapCell>();
        _position = Position.Zero;
    }

    public void SetNeighbours(IReadOnlyCollection<MapCell> cells)
    {
        _neighbours.Clear();
        _neighbours.AddRange(cells);
    }

    public void SetPosition(Position position)
    {
        _position = position;
    }
}
