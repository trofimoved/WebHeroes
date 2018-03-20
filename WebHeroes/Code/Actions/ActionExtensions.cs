using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Entities;
using WebHeroes.Code;

namespace WebHeroes.Actions
{
    public static class ActionExtensions
    {
        //??
        public static void BindAction(this List<Action> actions, Action action, InputKey inputKey)
        {
            var bindedaction = actions.FirstOrDefault(x => x.BindInputKey == inputKey);
            if (bindedaction != null)
                actions.First(x => x.BindInputKey == inputKey).BindInputKey = InputKey.None;
            actions.First(x => x.Name == action.Name).BindInputKey = inputKey;
        }
    }
}