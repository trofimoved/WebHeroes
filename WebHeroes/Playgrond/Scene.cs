using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Entities;
using WebHeroes.Entities.Enemies;

namespace WebHeroes.Playground
{
    public class Scene
    {
        public Board Board { get; set; }
        public string Input { get; set; }

        private List<Entity> _entities;
        public List<Entity> Entities { get { return _entities; } }

        public void AddEntity(Entity entity)
        {
            var newId = Board.AddEntity(entity);
            entity.Id = newId;
            _entities.Add(entity);
            entity.SetScene(this);
        }

        public Scene()
        {
            _entities = new List<Entity>();
            Board = new Board();
        }
        /// <summary>
        /// Создание сцены
        /// </summary>
        public void CreateScene()
        {
            this.AddEntity(new Player() { });
            this.AddEntity(new SkeletonSwordsman() { Position = new Position(5, 5) });
        }
        /// <summary>
        /// Создание сцены
        /// </summary>
        /// <param name="player"></param>
        public void CreateScene(Player player)
        {
            this.AddEntity(player);
            this.AddEntity(new Enemy() { Position = new Position(5, 5) });
        }
    }
}