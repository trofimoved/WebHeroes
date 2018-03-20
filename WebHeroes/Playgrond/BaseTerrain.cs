using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;

namespace WebHeroes.Playground
{
    /// <summary>
    /// Базовый класс, описывающий местность
    /// </summary>
    public class BaseTerrain
    {
        public int Id { get; set; }
        public Position Position { get; set; }
        public bool Impassible { get; set; }
        public TerrainType Type { get; set; }
    }
    public class Terrain : BaseTerrain
    {

    }

    public class Outer : BaseTerrain
    {
        public Outer()
        {
            Impassible = true;
            Type = TerrainType.Outer;
        }
    }

    public enum TerrainType
    {
        Grass,
        Stone,

        Outer = 1000
    }
}