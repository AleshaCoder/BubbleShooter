using System.Collections.Generic;
using UnityEngine;

namespace Services.MapGenerator
{
    public class Map
    {
        private readonly int _width;
        private readonly int _height;
        private MapCell[,] _mapCells;
        private Position _position;

        public Position Position => _position;

        public Map(int width, int height)
        {
            _position.X = 0;
            _position.Y = 0;
            _width = width;
            _height = height;
            _mapCells = new MapCell[_width, _height];
        }

        public int Width => _width;

        public int Height => _height;

        public void Add(in MapCell mapCell)
        {
            if (_position.Y == _height)
            {
                Debug.LogError("Map already full");
                return;
            }
            mapCell.SetPosition(_position);
            _mapCells[_position.X, _position.Y] = mapCell;
            _position.X++;
            if (_position.X == _width)
            {
                _position.X = 0;
                _position.Y++;
            }
        }

        public MapCell Get(int x, int y)
        {
            if (y >= _height || x >= _width)
            {
                Debug.LogError($"Cell has not found at {x},{y}");
                return default;
            }

            return _mapCells[x, y];
        }

        public void FindNeighbours()
        {
            List<MapCell> neighbours = new List<MapCell>(4);

            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                {
                    if (x > 0)
                        neighbours.Add(_mapCells[x - 1, y]);
                    if (x < Width - 1)
                        neighbours.Add(_mapCells[x + 1, y]);
                    if (y > 0)
                        neighbours.Add(_mapCells[x, y - 1]);
                    if (y < Height - 1)
                        neighbours.Add(_mapCells[x, y + 1]);

                    if (y % 2 == 0)
                    {
                        if (x > 0)
                        {
                            if (y > 0)
                                neighbours.Add(_mapCells[x - 1, y - 1]);
                            if (y < Height - 1)
                                neighbours.Add(_mapCells[x - 1, y + 1]);
                        }
                    }
                    else
                    {
                        if (x < Width - 1)
                        {
                            if (y > 0)
                                neighbours.Add(_mapCells[x + 1, y - 1]);
                            if (y < Height - 1)
                                neighbours.Add(_mapCells[x + 1, y + 1]);
                        }
                    }

                    _mapCells[x, y].SetNeighbours(neighbours);
                    neighbours.Clear();
                }
        }
    }
}
