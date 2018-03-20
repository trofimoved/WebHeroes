using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebHeroes.Code;
using WebHeroes.Entities;
using WebHeroes.Playground;

namespace WebHeroes.Actions.AIActions
{
    public static class AIActions
    {
        public static InputKey MoveToPosition(this Enemy entity, Position position)
        {
            var delta = entity.Position - position;
            if (Math.Abs(delta.x) > Math.Abs(delta.y))
            {
                if (delta.x > 0)
                    return InputKey.Down;
                else
                    return InputKey.Up;
            }
            else
            {
                if (delta.y > 0)
                    return InputKey.Right;
                else
                    return InputKey.Left;
            }
            return InputKey.None;
        }
    }
}