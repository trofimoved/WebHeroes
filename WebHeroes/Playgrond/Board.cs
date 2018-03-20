using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Entities;

namespace WebHeroes.Playground
{
    public class Board
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

        public BaseTerrain this[int i, int j]
        {
            get
            {
                try
                {
                    return _fields[i][j];
                }
                catch(IndexOutOfRangeException ex)
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
                _fields[row] = new BaseTerrain[j];
                for (int field = 0; field < j; field++)
                {
                    _fields[row][field] = new BaseTerrain();
                }
            }
        }

        public Board(PlayerBase player, int i = 10, int j = 10) : this(i, j)
        {
            _baseEntities.Add(player);
        }
    }
}