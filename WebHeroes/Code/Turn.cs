using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Entities;
using WebHeroes.Playground;
using WebHeroes.Actions;

namespace WebHeroes.Code
{
    /// <summary>
    /// Текущий ход
    /// </summary>
    public class Turn
    {
        private int _actionPoints;
        private Entity _entity;
        private GameLoop _gameLoop;

        public int ActionPoints { get { return _actionPoints; } }
        public int MaxActionPoints { get { return _entity.Status.ActionPoints; } }
        public int EntityId { get { return _entity.Id; } }
        public Entity CurrentEntity { get { return _entity; } }
        public bool IsPlayer { get { return _entity.Type == EntityType.Player; } }

        public void TurnTo(Entity entity)
        {
            _entity = entity;
        }

        public void UpdateEntity(Entity entity)
        {
            _entity = entity;
        }

        public void Start()
        {
            _actionPoints = _entity.Status.ActionPoints;
            //запустить наложенные эфекты
            //foreach (var effect in _entity.Status.Effects)
            //{

            //}
            if (_entity.Type == EntityType.Player)
            {
                //считывание ввода 
            }
            else
            {
                var enemy = _entity as Entities.Enemies.SkeletonSwordsman;
                enemy.SetTarget(_gameLoop.Scene.Entities.FirstOrDefault(x => x.Type == EntityType.Player));
                while(true)
                {
                    ExecuteAction(enemy.AIAction());
                }
                //запуск бота
            }
        }

        public void ProcessInput(InputKey inputKey)
        {
            if (inputKey == InputKey.None)
            {
                return;
            }
            if (inputKey == InputKey.Skip)
            {
                Over();
                return;
            }
            if (inputKey.IsMoving())
            {
                var move = new Moving(1, _entity);
                ExecuteAction(move.SetBoard(_gameLoop.Scene.Board).SetInputKey(inputKey));
            }
            else
            {
                //TODO другие действия по инпуту
            }
        }

        public void ProcessInput(string actionName, Position inputPosition)
        {
            var action = _entity.Actions.FirstOrDefault(x => x.Name == actionName);
            if (action.NeedTarget)
            {
                action.SetTarget(_gameLoop.Scene.Entities.FindByPosition(inputPosition));
            }
            if (action.NeedArea)
            {
                action.SetArea(inputPosition);
            }
            ExecuteAction(action);
        }
        public void ExecuteAction(IAction action, bool autoskip = false)
        {
            if (_actionPoints > 0 && action.ExecuteAction())
                _actionPoints -= action.ActionPoints();
            //если ходит враг, автоматически скипаем в конце хода
            if (_actionPoints <= 0 && (autoskip || _entity.Type == EntityType.Enemy))
                Over();
        }

        public void Over()
        {
            _gameLoop.NextTurn();
        }

        public Turn(GameLoop gameLoop)
        {
            _gameLoop = gameLoop;
        }
    }
}