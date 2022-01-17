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

        public static Vector2 GetDirectionalInputVector(KeyboardState keyboardState, ref bool directionChanged)
        {
            var newDirectionalInputVector = new Vector2(0f);

            if (keyboardState.IsKeyDown(Keys.Left) || keyboardState.IsKeyDown(Keys.Right))
                newDirectionalInputVector.X = keyboardState.IsKeyDown(Keys.Left) ? (keyboardState.IsKeyDown(Keys.Right) ? 2f : -1f) : keyboardState.IsKeyDown(Keys.Right) ? 1f : 0f;

            if (keyboardState.IsKeyDown(Keys.Up) || keyboardState.IsKeyDown(Keys.Down))
                newDirectionalInputVector.Y = keyboardState.IsKeyDown(Keys.Up) ? (keyboardState.IsKeyDown(Keys.Down) ? 2f : -1f) : keyboardState.IsKeyDown(Keys.Down) ? 1f : 0f;

            if (newDirectionalInputVector == Vector2.Zero)
                return Vector2.Zero;

            if (newDirectionalInputVector.X == 2f)
                newDirectionalInputVector.X = _directionalInputVector.X;
            if (newDirectionalInputVector.Y == 2f)
                newDirectionalInputVector.Y = _directionalInputVector.Y;

            if ((_directionalInputVector.X != 0f && newDirectionalInputVector.X != _directionalInputVector.X && newDirectionalInputVector.Y == 0f) ||
                (_directionalInputVector.Y != 0f && newDirectionalInputVector.Y != _directionalInputVector.Y && newDirectionalInputVector.X == 0f))
                directionChanged = true;
            
            _directionalInputVector = newDirectionalInputVector;

            return _directionalInputVector;
        }
    }
}
