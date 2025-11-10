using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using Graphics;

namespace Inputs
{

    public sealed class FlatMouse
    {
        public enum MouseButtons
        {
            Left,
            Right,
            Middle,
        }

        private static Lazy<FlatMouse> LazyInstance = new Lazy<FlatMouse>(() => new FlatMouse());

        public static FlatMouse Instance
        {
            get { return LazyInstance.Value; }
        }

        private MouseState currMouseState;
        private MouseState prevMouseState;
        private Point previousMousePosition;

        public Point MouseWindowPosition
        {
            get
            {
                return this.currMouseState.Position;
            }
        }

        public int PreviousScrollWheelValue { get; set; }
        public int CurrentScrollWheelValue { get; set; }
        public int ScrollDelta { get; set; }

        private FlatMouse()
        {
            this.currMouseState = Mouse.GetState();
            this.prevMouseState = this.currMouseState;
        }

        public void Update()
        {
            this.prevMouseState = this.currMouseState;
            this.currMouseState = Mouse.GetState();
            this.PreviousScrollWheelValue = this.CurrentScrollWheelValue;
            this.CurrentScrollWheelValue = this.currMouseState.ScrollWheelValue;
            this.ScrollDelta = this.CurrentScrollWheelValue - this.PreviousScrollWheelValue;
        }

        public bool IsLeftMouseButtonDown()
        {
            return this.currMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMouseButtonDown()
        {
            return this.currMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleMouseButtonDown()
        {
            return this.currMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsLeftMouseButtonPressed()
        {
            return this.currMouseState.LeftButton == ButtonState.Pressed && this.prevMouseState.LeftButton == ButtonState.Released;
        }

        public bool IsRightMouseButtonPressed()
        {
            return this.currMouseState.RightButton == ButtonState.Pressed && this.prevMouseState.RightButton == ButtonState.Released;
        }

        public bool IsMiddleMouseButtonPressed()
        {
            return this.currMouseState.MiddleButton == ButtonState.Pressed && this.prevMouseState.MiddleButton == ButtonState.Released;
        }

        public bool IsLeftMouseButtonReleased()
        {
            return this.currMouseState.LeftButton == ButtonState.Released && this.prevMouseState.LeftButton == ButtonState.Pressed;
        }

        public bool IsRightMouseButtonReleased()
        {
            return this.currMouseState.RightButton == ButtonState.Released && this.prevMouseState.RightButton == ButtonState.Pressed;
        }

        public bool IsMiddleMouseButtonReleased()
        {
            return this.currMouseState.MiddleButton == ButtonState.Released && this.prevMouseState.MiddleButton == ButtonState.Pressed;
        }

        public bool IsAnyMouseButtonPressed()
        {
            return IsLeftMouseButtonPressed() ||
                IsRightMouseButtonPressed() ||
                IsMiddleMouseButtonPressed();
        }

        public bool IsMouseButtonPressed(MouseButtons button)
        {
            return button switch
            {
                MouseButtons.Left => IsLeftMouseButtonPressed(),
                MouseButtons.Right => IsRightMouseButtonPressed(),
                MouseButtons.Middle => IsMiddleMouseButtonPressed(),
                _ => false
            };
        }

        public bool IsMouseButtonDown(MouseButtons button)
        {
            return button switch
            {
                MouseButtons.Left => IsLeftMouseButtonDown(),
                MouseButtons.Right => IsRightMouseButtonDown(),
                MouseButtons.Middle => IsMiddleMouseButtonDown(),
                _ => false
            };
        }

        public bool IsMouseButtonReleased(MouseButtons button)
        {
            return button switch
            {
                MouseButtons.Left => IsLeftMouseButtonReleased(),
                MouseButtons.Right => IsRightMouseButtonReleased(),
                MouseButtons.Middle => IsMiddleMouseButtonReleased(),
                _ => false
            };
        }

        public Vector2 GetMouseScreenPosition()
        {
            Rectangle screenDestinationRectangle = Graphics.Graphics.Screen.CalculateDestinationRectangle();
            Point mouseWindowPosition = this.MouseWindowPosition;
            float sx = mouseWindowPosition.X - screenDestinationRectangle.X;
            float sy = mouseWindowPosition.Y - screenDestinationRectangle.Y;
            sx /= (float)screenDestinationRectangle.Width;
            sy /= (float)screenDestinationRectangle.Height;
            float x = sx * (float)Graphics.Graphics.Screen.Width;
            float y = sy * (float)Graphics.Graphics.Screen.Height;

            return new Vector2(x, y);
        }

        public Vector2 GetMouseWorldPosition()
        {
            Viewport screenViewport = new Viewport(0, 0, Graphics.Graphics.Screen.Width, Graphics.Graphics.Screen.Height);
            Vector2 mouseScreenPosition = this.GetMouseScreenPosition();
            Ray mouseRay = this.CreateMouseRay(mouseScreenPosition, screenViewport, Graphics.Graphics.Camera);
            Plane worldPlane = new Plane(new Vector3(0, 0, 1f), 0f);
            float? dist = mouseRay.Intersects(worldPlane);
            Vector3 ip = mouseRay.Position + mouseRay.Direction * dist.Value;
            Vector2 result = new Vector2(ip.X, ip.Y);
            return result;
        }

        private Ray CreateMouseRay(Vector2 mouseScreenPosition, Viewport viewport, Camera camera)
        {
            Vector3 nearPoint = new Vector3(mouseScreenPosition, 0);
            Vector3 farPoint = new Vector3(mouseScreenPosition, 1);
            nearPoint = viewport.Unproject(nearPoint, camera.Projection, camera.View, Matrix.Identity);
            farPoint = viewport.Unproject(farPoint, camera.Projection, camera.View, Matrix.Identity);
            Vector3 direction = farPoint - nearPoint;
            direction.Normalize();
            Ray result = new Ray(nearPoint, direction);
            return result;
        }






        public List<MouseButtons> GetPressedButtons()
        {
            List<MouseButtons> pressedButtons = new List<MouseButtons>();

            // Check if each mouse button is currently pressed
            foreach (MouseButtons button in Enum.GetValues(typeof(MouseButtons)))
            {
                if (IsButtonDown(button))
                {
                    pressedButtons.Add(button);
                }
            }

            return pressedButtons;
        }

        private bool IsButtonDown(MouseButtons button)
        {
            switch (button)
            {
                case MouseButtons.Left:
                    return IsLeftMouseButtonDown();
                case MouseButtons.Right:
                    return IsRightMouseButtonDown();
                case MouseButtons.Middle:
                    return IsMiddleMouseButtonDown();
                // Add cases for other buttons if needed
                default:
                    return false;
            }
        }

        public bool IsMouseMoved()
        {
            Point currentMousePosition = MouseWindowPosition;
            bool hasMoved = currentMousePosition != previousMousePosition;
            previousMousePosition = currentMousePosition;

            return hasMoved;
        }
    }

}
