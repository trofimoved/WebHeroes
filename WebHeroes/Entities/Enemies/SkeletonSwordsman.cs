using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Entities;
using WebHeroes.Data;
using WebHeroes.Code;
using WebHeroes.Actions;
using WebHeroes.Actions.AIActions;
using Action = WebHeroes.Actions.Action;


namespace WebHeroes.Entities.Enemies
{
    public class SkeletonSwordsman : Enemy
    {
        public SkeletonSwordsman() : base()
        {
            Status = new Status()
            {
                ActionPoints = 3
            };
            Inventory.EquipWeapon(ItemsData.GetWeaponFromData(x => x.Name == "Sword"));
        }

        public override IAction AIAction()
        {
            var action = Actions.First();

            if (Position.Distance(this.Position, _target.Position) > action.Range)
            {
                //TODO: переделать что не передовать всю доску целиком
                return new Moving(1, this).SetInputKey(this.MoveToPosition(_target.Position));
            }
            else
            {
                return action.SetTarget(_target);
            }
        }
    }
}