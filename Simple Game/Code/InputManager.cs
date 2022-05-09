using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Simple_Game
{
    static class InputManager
    {
        private static Vector2 _directionalInputVector;
        private static KeyboardState _keyboardState;

        public static void UpdateKeyboardState()
        {
            _keyboardState = Keyboard.GetState();
        }

        public static Vector2 GetDirectionalInputVector()
        {
            var newDirectionalInputVector = new Vector2(0f)
            {
                X = _keyboardState.IsKeyDown(Keys.Left) ? (_keyboardState.IsKeyDown(Keys.Right) ? _directionalInputVector.X : -1f) : _keyboardState.IsKeyDown(Keys.Right) ? 1f : 0f,
                Y = _keyboardState.IsKeyDown(Keys.Up) ? (_keyboardState.IsKeyDown(Keys.Down) ? _directionalInputVector.Y : -1f) : _keyboardState.IsKeyDown(Keys.Down) ? 1f : 0f
            };

            if (newDirectionalInputVector == Vector2.Zero)
                return Vector2.Zero;
        
            _directionalInputVector = newDirectionalInputVector;

            return _directionalInputVector;
        }

        public static bool IsKeyDown(Keys key)
        {
            return _keyboardState.IsKeyDown(key);
        }
    }
}
