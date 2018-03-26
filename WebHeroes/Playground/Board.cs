using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Entities;

namespace WebHeroes.Playground
{
    public interface WeightedGraph<L>
    {
        double Cost(Position a, Position b);
        IEnumerable<Position> Neighbors(Position id);
    }
    public class Board : WeightedGraph<Position>
    {

        private BaseTerrain[][] _fields;
        public BaseTerrain[][] Fields { get { return _fields; } }

        private List<BaseEntity> _baseEntities;
        public List<BaseEntity> BaseEntities { get { return _baseEntities; } }

        public BaseTerrain this[Position position]
        {
            get
            {
                return this[position.y, position.x];
            }
            set
            {
                this[position.y, position.x] = value;
            }
        }

        public BaseTerrain[] this[int i]
        {
            get
            {
                return _fields[i];
            }
        }

        private BaseTerrain this[int i, int j]
        {
            get
            {
                try
                {
                    return _fields[i][j];
                }
                catch (IndexOutOfRangeException ex)
                {
                    return new Outer();
                }
            }
            set
            {
                try
                {
                    _fields[i][j] = value;
                }
                catch (IndexOutOfRangeException ex)
                {

                }
            }
        }

        #region BaseEntities

        public int AddEntity(BaseEntity entity)
        {
            // генерация id
            var newId = _baseEntities.GetMaxId() + 1;
            entity.Id = newId;
            _baseEntities.Add(entity);
            return newId;
        }

        public void UpdateEntity(BaseEntity entity)
        {
            _baseEntities.RemoveAll(x => x.Id == entity.Id);
            _baseEntities.Add(entity);
        }

        public void RemoveEntity(BaseEntity entity)
        {
            _baseEntities.RemoveAll(x => x.Id == entity.Id);
        }

        public void RemoveEntity(int entityId)
        {
            _baseEntities.RemoveAll(x => x.Id == entityId);
        }

        #endregion

        #region WeightedGraph

        public IEnumerable<Position> Neighbors(Position id)
        {
            Position[] DIRS = new Position[]
            {
                new Position(1, 1),
                new Position(1, -1),
                new Position(2, 0),
                new Position(-2, 0),
                new Position(-1, 1),
                new Position(-1, -1),
            };

            foreach (var dir in DIRS)
            {
                Position next = new Position(id.x + dir.x, id.y + dir.y);
                if (!this[next].Impassible)
                {
                    yield return next;
                }
                else
                {

                }
            }
        }

        public double Cost(Position a, Position b)
        {
            return 1;
        }

        #endregion

        /// <summary>
        /// Генерация локации
        /// </summary>
        /// <param name="i"></param>
        /// <param name="j"></param>
        public Board(int i = 10, int j = 10)
        {
            _baseEntities = new List<BaseEntity>();
            _fields = new BaseTerrain[i][];
            for (int row = 0; row < i; row++)
            {
                _fields[row] = new BaseTerrain[j*2];
                int oddRow = row % 2;
                for (int field = oddRow; field < j*2; field += 2)
                {
                    _fields[row][field] = new BaseTerrain();
                }
            }
            _fields[4][4] = new BaseTerrain() { Impassible = true, Type = TerrainType.Stone };
            //_fields[4][5] = new BaseTerrain() { Impassible = true, Type = TerrainType.Stone };
            _fields[4][6] = new BaseTerrain() { Impassible = true, Type = TerrainType.Stone };
        }

        public Board(PlayerBase player, int i = 10, int j = 10) : this(i, j)
        {
            _baseEntities.Add(player);
        }
    }
}