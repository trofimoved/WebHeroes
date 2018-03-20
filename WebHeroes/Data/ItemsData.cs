using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Entities;
using System.IO;
using Newtonsoft.Json;

namespace WebHeroes.Data
{
    public static class ItemsData
    {
        public static Weapon GetWeaponFromData(Func<Weapon,bool> query)
        {
            using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Data/Weapons.json"))
            {
                var weapons = JsonConvert.DeserializeObject<List<Weapon>>(sr.ReadToEnd());
                var weapon = weapons.FirstOrDefault(query);
                weapon.GlobalId = "weapon_" + weapon.Id;
                return weapon;
            }
        }

        public static Weapon GetRandomWeapon()
        {
            return GetWeaponFromData(x => x.Id == 1);
        }
    }
}