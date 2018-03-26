using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Newtonsoft.Json;

namespace WebHeroes.Code
{
    public class Input
    {
        public Input()
        {
            _inputSettings = _defaultSettings;
        }

        public InputKey Key(string value)
        {
            if (string.IsNullOrEmpty(value))
                return InputKey.None;
            return _inputSettings.Where(x => x.Value.Contains(value)).FirstOrDefault().Key;
        }


        private Dictionary<InputKey, string[]> _inputSettings;
        private Dictionary<InputKey, string[]> _defaultSettings
        {
            get
            {
                using (StreamReader sr = new StreamReader(AppDomain.CurrentDomain.BaseDirectory + "/Settings/defaultSettings.json"))
                {
                    return JsonConvert.DeserializeObject<Dictionary<InputKey, string[]>>(sr.ReadToEnd());
                }
            }
        }
    }

    public enum InputKey
    {
        None,
        Idle,
        UpLeft,
        UpRight,
        Left,
        Right,
        DownLeft,
        DownRight,

        Skip = 1000
    }
    public enum MovingInputKey
    {
        Up,
        Down,
        Left,
        Right,
    }

    public static class InputExtensions
    {
        public static bool IsMoving(this InputKey inputKey)
        {
            return MovingInputKey.IsDefined(typeof(MovingInputKey), inputKey.ToString());
        }
    }
    
}