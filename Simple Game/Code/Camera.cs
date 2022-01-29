using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Simple_Game
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

        public Rectangle GetVisibleArea(GraphicsDevice graphicsDevice)
        {
            var _screenSize = new Vector2(graphicsDevice.Viewport.Width, graphicsDevice.Viewport.Height);
            var inverseViewMatrix = Matrix.Invert(_transform);
            var tl = Vector2.Transform(Vector2.Zero, inverseViewMatrix);
            var tr = Vector2.Transform(new Vector2(_screenSize.X, 0), inverseViewMatrix);
            var bl = Vector2.Transform(new Vector2(0, _screenSize.Y), inverseViewMatrix);
            var br = Vector2.Transform(_screenSize, inverseViewMatrix);
            var min = new Vector2(
                MathHelper.Min(tl.X, MathHelper.Min(tr.X, MathHelper.Min(bl.X, br.X))),
                MathHelper.Min(tl.Y, MathHelper.Min(tr.Y, MathHelper.Min(bl.Y, br.Y))));
            var max = new Vector2(
                MathHelper.Max(tl.X, MathHelper.Max(tr.X, MathHelper.Max(bl.X, br.X))),
                MathHelper.Max(tl.Y, MathHelper.Max(tr.Y, MathHelper.Max(bl.Y, br.Y))));
            return new Rectangle((int)min.X, (int)min.Y, (int)(max.X - min.X), (int)(max.Y - min.Y));
        }
    }
}
