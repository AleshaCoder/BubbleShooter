using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MapFromFileLoader: IMapLoader
{
    public Map Load(string path)
    {
        int width, height;
        string text;
        using (TextReader reader = File.OpenText(path))
        {
            width = int.Parse(reader.ReadLine());
            height = int.Parse(reader.ReadLine());
            text = reader.ReadToEnd();
        }

        Map map = new Map(width, height);

        foreach (char ch in text)
        {
            if (int.TryParse(ch.ToString(), out int id))
            {
                MapCell mapCell = new MapCell(id);
                map.Add(mapCell);
            }
        }

        map.FindNeighbours();

        return map;
    }
}
