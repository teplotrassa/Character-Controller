using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Character_Controller
{
    static class InputManager
    {
        private static Vector2 _directionalInputVector; 

        public static Vector2 GetDirectionalInputVector(KeyboardState keyboardState)
        {
            var newDirectionalInputVector = new Vector2(0f)
            {
                X = keyboardState.IsKeyDown(Keys.Left) ? (keyboardState.IsKeyDown(Keys.Right) ? _directionalInputVector.X : -1f) : keyboardState.IsKeyDown(Keys.Right) ? 1f : 0f,
                Y = keyboardState.IsKeyDown(Keys.Up) ? (keyboardState.IsKeyDown(Keys.Down) ? _directionalInputVector.Y : -1f) : keyboardState.IsKeyDown(Keys.Down) ? 1f : 0f
            };

            if (newDirectionalInputVector == Vector2.Zero)
                return Vector2.Zero;
        
            _directionalInputVector = newDirectionalInputVector;

            return _directionalInputVector;
        }
    }
}
