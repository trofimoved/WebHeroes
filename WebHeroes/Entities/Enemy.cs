using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Actions;
using Action = WebHeroes.Actions.Action;

namespace WebHeroes.Entities
{
    public class EnemyBase : Entity
    {
        public EnemyBase() : base() { Type = EntityType.Enemy; Blocking = true; Status.Health = 50; Status.MaxHealth = 50; }

        public EnemyBase SetTarget(Entity entity)
        {
            _target = entity;
            return this;
        }
        public virtual void SetTarget(List<Entity> entities)
        {

        }
    }

    public class Enemy : EnemyBase
    {
        public virtual IAction AIAction(WebHeroes.Playground.Board board)
        {
            return new Action(0, this);
        }
    }
}