using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Playground;
using WebHeroes.Actions;
using Action = WebHeroes.Actions.Action;


namespace WebHeroes.Entities
{
    /// <summary>
    /// Базовая сущность, описывает любой объект на доске
    /// </summary>
    public class BaseEntity
    {
        public string Name { get; set; }
        public int Id { get; set; }
        public Position Position { get; set; }
        public EntityType Type { get; set; }
        public bool Blocking { get; set; }

        protected Scene _scene;

        public void SetScene(Scene scene)
        {
            _scene = scene;
        }
        public virtual void Update()
        {
            _scene.Entities.Update((Entity)this);
        }
    }
    /// <summary>
    /// Сущность для описания объектов, с изменяемым состоянием (статусом)
    /// </summary>
    public class Entity : BaseEntity
    {
        protected Entity _target;

        public Entity Target { get { return _target; } }
        /// <summary>
        /// Статус
        /// </summary>
        public Status Status;
        /// <summary>
        /// Инвентарь
        /// </summary>
        public Inventory Inventory;
        /// <summary>
        /// Список доступных действий
        /// </summary>
        public List<Action> Actions { get; set; }

        public virtual void GetDamage(int points)
        {
            this.Status.Health -= points;
            Logger.Write($"{this.Type} {this.Name} {this.Id} get {points} damage");
            if (this.Status.Health <= 0)
            {
                this.Die();
            }
            this.Update();
        }

        public virtual void GetHeal(int points)
        {
            this.Status.Health += points;
            if (this.Status.Health > this.Status.MaxHealth)
                this.Status.Health = this.Status.MaxHealth;
        }

        public virtual void GetEffect(Effect effect)
        {
            this.Status.Effects.Add(effect);
        }

        public virtual void Die() { }

        public override void Update()
        {
            _scene.Entities.Update(this);
        }

        public Entity() : base()
        {
            Status = new Status();
            Inventory = new Inventory();
            Actions = new List<Action>();
            Actions.Add(new Attack(1, this) { });
            Status.Effects = new List<Effect>();
        }
    }

    #region Extensions
    public static class BaseEntityExtensions
    {
        public static BaseEntity FindByPosition(this List<BaseEntity> baseEntities, Position position)
        {
            return baseEntities.Where(x => x.Position.x == position.x && x.Position.y == position.y).FirstOrDefault();
        }

        public static int GetMaxId(this List<BaseEntity> baseEntities)
        {
            if (baseEntities == null || baseEntities.Count == 0)
                return 0;
            return baseEntities.Max(x => x.Id);
        }
    }
    public static class EntityExtensions
    {
        public static Entity FindByPosition(this List<Entity> entities, Position position)
        {
            return entities.Where(x => x.Position.x == position.x && x.Position.y == position.y).FirstOrDefault();
        }

        public static Entity GetNearest(this List<Entity> entities, Entity entity)
        {
            var minDistance = entities.Min(x => Position.Distance(x.Position, entity.Position));
            return entities.FirstOrDefault(x => Position.Distance(x.Position, entity.Position) == minDistance);
        }

        public static Entity GetNearest(this List<Entity> entities, Entity entity, Func<Entity,bool> predicate)
        {
            var minDistance = entities.Where(predicate).Min(x => Position.Distance(x.Position, entity.Position));
            return entities.Where(predicate).FirstOrDefault(x => Position.Distance(x.Position, entity.Position) == minDistance);
        }

        public static void Update(this List<Entity> entities, Entity entity)
        {
            var index = entities.IndexOf(entities.FirstOrDefault(x => x.Id == entity.Id));
            entities[index] = entity;
        }
    }

    public static class MovingExtensions
    {
        /// <summary>
        /// Двигаем объект
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="board"></param>
        /// <param name="inputKey"></param>
        /// <param name="caughtEntity">Объект, который встретился в точке перемещения</param>
        /// <returns>Можно ли пройти в эту точку</returns>
        public static bool TryMove(this BaseEntity entity, Board board, InputKey inputKey, out BaseEntity caughtEntity, out Position moveTo)
        {
            Position newPosition = new Position(entity.Position);
            moveTo = new Position(entity.Position);
            caughtEntity = null;
            switch (inputKey)
            {
                case InputKey.UpLeft: newPosition.y -= 1; newPosition.x -=1; break;
                case InputKey.UpRight: newPosition.y -= 1; newPosition.x += 1; break;

                case InputKey.Left: newPosition.x -= 2; break;
                case InputKey.Right: newPosition.x += 2; break;

                case InputKey.DownLeft: newPosition.y += 1; newPosition.x -= 1; break;
                case InputKey.DownRight: newPosition.y += 1; newPosition.x += 1; break;

                default: break;
            }
            if (board[newPosition].Impassible)
                return false;
            caughtEntity = board.BaseEntities.FindByPosition(newPosition);

            if (caughtEntity == null || !caughtEntity.Blocking)
            {
                moveTo = newPosition;
                return true;
            }
            return false;
        }

        public static void MoveTo(this BaseEntity entity, Board board, Position newPosition)
        {
            entity.Position = newPosition;
            board.UpdateEntity(entity);
            entity.Update();
        }
    }
    #endregion

    public enum EntityType
    {
        None,
        Player,
        Enemy,
        Object
    }
}