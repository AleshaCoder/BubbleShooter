using System.Linq;
using System.Collections.Generic;
using Infrastructure.AssetManagement;
using UnityEngine;

public class MapInstatiator : IMapInstatiator
{
    private readonly MapConfig _mapConfig;
    private readonly IAssetProvider _assetProvider;

    private List<MapCellBehaviour> _cellViews;

    public MapInstatiator(MapConfig mapConfig, IAssetProvider assetProvider)
    {
        _mapConfig = mapConfig;
        _assetProvider = assetProvider;
        _cellViews = new List<MapCellBehaviour>();
    }

    public void Instantiate(Map map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                MapCell cell = map.Get(x, y);
                Vector3 step = new Vector3(_mapConfig.StepX * x, _mapConfig.StepY * (y / 2));
                Vector3 position = _mapConfig.StartPosition + step;

                if (y % 2 != 0)
                    position = _mapConfig.SecondStartPosition + step;

                bool kinematic = y == 0;
                MapCellBehaviour cellView = _assetProvider.Instantiate(_mapConfig.Get(cell.Id), position);
                cellView.Construct(cell, kinematic, _mapConfig.Frequency);
                _cellViews.Add(cellView);
            }
        }
        SetNeighbours(map);
    }

    private void SetNeighbours(Map map)
    {
        for (int y = 0; y < map.Height; y++)
        {
            for (int x = 0; x < map.Width; x++)
            {
                MapCell cell = map.Get(x, y);
                MapCellBehaviour behaviour = _cellViews.First(beh => beh.HasCell(cell));

                foreach (var neighbour in cell.Neighbours)
                {
                    MapCellBehaviour neighbourBehaviour = _cellViews.First(beh => beh.HasCell(neighbour));
                    behaviour.AddNeighbour(neighbourBehaviour);
                }
            }
        }
    }
}
