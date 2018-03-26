using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Data;

namespace WebHeroes.Entities
{
    public class Inventory
    {
        private List<StackableItem> _baseItems;
        private Weapon _weapon;

        public int Armor { get; set; }
        public List<StackableItem> Items { get { return _baseItems; } }
        public Weapon EquipedWeapon { get { return _weapon; } }

        public void EquipWeapon(Weapon weapon)
        {
            _weapon = weapon;
        }



        public Inventory()
        {
            _baseItems = new List<StackableItem>();
            this.EquipWeapon(new Weapon()
            {
                ActionPoints = 1,
                Damage = 0,
                Name = "Fists",
                Description = "Just bare fists",
                Range = 1,
                Equped = true,
                Cost = 0
            });
        }
    }

    public class EquippableItem : BaseItem
    {
        public bool Equped { get; set; }
    }
    public class StackableItem : BaseItem
    {
        public int Stack { get; set; }
    }

    public class Weapon : EquippableItem
    {
        public int Damage { get; set; }
        public int ActionPoints { get; set; }
        public int Range { get; set; }
    }
    public class Armor
    {
        //public int Armor { get; set; }
    }

    public enum WeaponType
    {

    }

    public enum ArmorType
    {
        Head,
        Chest,
        Hands,
        Boots
    }
    /// <summary>
    /// Базовая сущность, описывает любой предмет в инвентаре
    /// </summary>
    public abstract class BaseItem
    {
        public int Id { get; set; }
        /// <summary>
        /// Правфила формирования Id:
        /// Тип(по названию класса) + Id;
        /// например для оружия будет weapon_1
        /// </summary>
        public string GlobalId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int Cost { get; set; }
    }

    public static class ItemsExtensions
    {
        public static void AddItem(this List<StackableItem> baseItems, StackableItem item)
        {
            var baseitem = baseItems.FirstOrDefault(x => x.GlobalId == item.GlobalId);
            if (baseitem != null)
            {
                baseItems.First(x => x.GlobalId == item.GlobalId).Stack += item.Stack;
            }
            else
            {
                baseItems.Add(item);
            }
        }
    }
}