using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Entities;
using WebHeroes.Playground;
using WebHeroes.Code;

namespace WebHeroes.Actions
{
    public interface IAction
    {
        int ActionPoints();
        bool ExecuteAction();
    }
    /// <summary>
    /// Базовый класс действия сущности 
    /// </summary>
    public class BaseAction : IAction
    {
        protected int _actionPoints;
        protected Entity _executor;

        //public int ActionPoints { get { return _actionPoints; } }

        public virtual bool ExecuteAction()
        {
            return false;
        }
        public virtual int ActionPoints()
        {
            return _actionPoints;
        }

        public BaseAction(int AP, Entity executor)
        {
            _actionPoints = AP;
            _executor = executor;
        }
    }

    /// <summary>
    /// Действие
    /// </summary>
    public class Action : BaseAction
    {
        protected Position _area;
        protected Entity _target;
        protected Board _board;

        public string Name { get; set; }
        public string Discription { get; set; }
        public int Range { get; set; }
        //??
        public InputKey BindInputKey { get; set; }


        public bool NeedArea;
        public bool NeedTarget;


        public Action SetTarget(Entity target)
        {
            if (target == null)
                throw new Exceptions.NeedTargetAreaException();

            _target = target;
            return this;
        }

        public Action SetArea(Position area)
        {
            if (area == null)
                throw new Exceptions.NeedTargetAreaException();
            _area = area;
            return this;
        }

        public Action SetBoard(Board board)
        {
            _board = board;
            return this;
        }

        public Action(int AP, Entity executor) : base(AP, executor) { }
    }

    /// <summary>
    /// Движение
    /// </summary>
    //TODO: пределать чтобы не передовать всю доску целиком
    //передавать только список возжных для хода клеток
    //т.е. у которых BaseTerrain.Type != Impassible
    public class MoveAction : BaseAction
    {
        protected InputKey _inputKey;
        protected Board _board;

        public MoveAction SetBoard(Board board)
        {
            _board = board;
            return this;
        }

        public MoveAction SetInputKey(InputKey inputKey)
        {
            _inputKey = inputKey;
            return this;
        }

        public MoveAction(int AP, Entity executor) : base(AP, executor) { }
    }

    public class Moving : MoveAction
    {
         
        public override bool ExecuteAction()
        {
            BaseEntity metEntity;
            Position newPosition;
            var canMove = ((BaseEntity)_executor).TryMove(_board, _inputKey, out metEntity, out newPosition);
            if (canMove)
            {
                ((BaseEntity)_executor).MoveTo(_board, newPosition);
                return true;
            }

            return false;
        }

        public Moving(int AP, Entity entity) : base(AP, entity) { }
    }


    /// <summary>
    /// Один из вариантов скила, наносящего урон по одиночной цели
    /// </summary>
    public class Attack : Action
    {

        public override bool ExecuteAction()
        {
            int currentRange = Position.Distance(_executor.Position, _target.Position);
            if (currentRange > _executor.Inventory.EquipedWeapon.Range)
                throw new Exceptions.CustomException("Цель вне зоны поражения");
            var damage = _executor.Inventory.EquipedWeapon.Damage;
            _target.GetDamage(damage);
            return true;
        }

        public Attack(int AP, Entity entity) : base(AP, entity)
        {
            Name = "Attack";
            Discription = "Attack whit your weapon";

            NeedTarget = true;
        }
    }
}