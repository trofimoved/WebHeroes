using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebHeroes.Entities
{
    public class Status
    {
        public double Health { get; set; }
        public double MaxHealth { get; set; }
        public int ActionPoints { get; set; }
        public Stats Stats { get; set; }
        /// <summary>
        /// Список действующих эффектов
        /// </summary>
        public List<Effect> Effects { get; set; }
    }
    public class Stats
    {
        public int Strength { get; set; }
        public int Dexterity { get; set; }
        public int Intelligence { get; set; }
    }
    public class Effect
    {
        public int Durration { get; set; }
        public EffectType Type { get; set; }
    }

    public enum EffectType
    {
        Damage,
        Heal,
        Stun,

    }
}