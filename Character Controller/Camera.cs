using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Character_Controller
{
    public class Camera
    {
        public float Zoom
        {
            get { return _zoom; }
            set { _zoom = value; if (_zoom < 0.1f) _zoom = 0.1f; }
        }
        private float _zoom;

        private Matrix _transform;

        public Vector2 Position
        {
            get { return _position; }
            set { _position = value; }
        }
        private Vector2 _position;

        public float Rotation
        {
            get { return _rotation; }
            set { _rotation = value; }
        }
        private float _rotation;

        public Camera()
        {
            _zoom = 1.0f;
            _rotation = 0.0f;
            _position = Vector2.Zero;
        }

        public Matrix GetTransformation(GraphicsDevice graphicsDevice)
        {
            return _transform =
                Matrix.CreateTranslation(new Vector3(-_position.X, -_position.Y, 0.0f)) *
                Matrix.CreateRotationZ(_rotation) *
                Matrix.CreateScale(new Vector3(_zoom, _zoom, 1)) *
                Matrix.CreateTranslation(new Vector3(graphicsDevice.Viewport.Width * 0.5f, graphicsDevice.Viewport.Height * 0.5f, 0f));
        }
    }
}
