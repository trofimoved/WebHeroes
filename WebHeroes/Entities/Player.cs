using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Data;
using WebHeroes.Playground;
using WebHeroes.Actions;

namespace WebHeroes.Entities
{
    public class PlayerBase : Entity
    {
        public PlayerBase() : base()
        {
            Type = EntityType.Player;
            Blocking = true;
            Status = new Status()
            {
                MaxHealth = 100,
                Health = 100,
                ActionPoints = 3,
                Effects = new List<Effect>(),
            };
            Position = new Position(5, 1);
            Actions.Add(new Attack(1, this) { Name = "Mortal Strike" });

            Inventory = new Inventory();
            Inventory.EquipWeapon(ItemsData.GetRandomWeapon());
        }
    }
    public class Player : PlayerBase
    {
        public Player() : base()
        {
        }
    }
}