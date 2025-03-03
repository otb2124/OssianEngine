using System;
using Microsoft.Xna.Framework;
using Physics;
namespace Graphics
{
    public sealed class Camera
    {

        private Game game;
        private Screen screen;
        public Vector2 position;
        private Matrix view;
        private Matrix proj;

        private float aspectRatio;
        private float fieldOfView;
        private double baseZ;
        public double z;

        public int zoom;
        private bool isStickToPlayer = true;

        private float angle;
        private Vector2 up;

        private const float MinZ = 20f;
        private const float MaxZ = 350f;

        private const int MinZoom = 1;
        private const int MaxZoom = 32;

        public int counter = 0;
        public bool isMoving = false, hasMoved = false;

        public int ScreenResW, ScreenResH, ScreenW, ScreenH;

        public CameraOperator cameraOperator;

        public Vector2 Position
        {
            get { return position; }
        }

        public Matrix View
        {
            get { return view; }
        }

        public Matrix Projection
        {
            get { return proj; }
        }

        internal float AspectRatio
        {
            get { return aspectRatio; }
        }

        internal float FieldOfView
        {
            get { return fieldOfView; }
        }

        public double BaseZ
        {
            get { return baseZ; }
        }

        public double Z
        {
            get { return z; }
        }

        public int Zoom
        {
            get { return zoom; }
            set
            {
                zoom = FlatMath.Clamp(value, MinZoom, MaxZoom);
                z = baseZ * (1d / zoom);
            }
        }

        public Vector2 Up
        {
            get { return up; }
        }

        public float Angle
        {
            get { return angle; }
        }


        public Camera(Screen screen, Game game)
        {
            this.game = game;
            this.screen = screen ?? throw new ArgumentNullException("screen");
            position = Vector2.Zero;
            view = Matrix.Identity;
            proj = Matrix.Identity;

            aspectRatio = screen.Width / (float)screen.Height;
            fieldOfView = MathHelper.PiOver2;
            baseZ = 100;
            z = baseZ;

            angle = 0f;
            up = new Vector2(MathF.Sin(angle), MathF.Cos(angle));

            zoom = 1;

            ScreenResW = screen.Width;
            ScreenResH = screen.Height;
            ScreenW = Graphics.ResolutionX;
            ScreenH = Graphics.ResolutionY;

            Update();

        }


        

        public void Update()
        {

            Matrix translation = Matrix.CreateTranslation(-position.X, -position.Y, 0);

            // Calculate the view matrix by combining translation with the look-at matrix
            view = translation * Matrix.CreateLookAt(new Vector3(0, 0, (float)z), Vector3.Zero, new Vector3(up, 0f));

            // Update the projection matrix
            proj = Matrix.CreatePerspectiveFieldOfView(fieldOfView, aspectRatio, MinZ, MaxZ);
        }


        public double GetZFromHeight(double height)
        {
            double result = height * 0.5d / Math.Tan(fieldOfView * 0.5d);
            return result;
        }

        private double GetVisibleHeightFromZ(double z)
        {
            double result = z * Math.Tan(fieldOfView * 0.5f) * 2f;
            return result;
        }

        private double GetVisibleHeight()
        {
            double result = 2d * Math.Tan(fieldOfView * 0.5d) * z;
            return result;
        }

        

        public void ResetZ()
        {
            z = baseZ;
            Update();
        }

        public void Move(Vector2 amount)
        {
            position += amount;
        }

        public void MoveTo(Vector2 position)
        {
            this.position = position;
        }

        public void MoveZ(float amount)
        {
            double new_z = z + amount;

            if (new_z < MinZ ||
                new_z > MaxZ)
            {
                return;
            }

            z = new_z;
        }

        public void IncZoom()
        {
            int new_zoom = zoom + 1;

            if (new_zoom < MinZoom || new_zoom > MaxZoom)
            {
                return;
            }

            zoom = new_zoom;
            z = baseZ * (1d / zoom);

        }

        public void DecZoom()
        {
            int new_zoom = zoom - 1;

            if (new_zoom < MinZoom || new_zoom > MaxZoom)
            {
                return;
            }

            zoom = new_zoom;
            z = baseZ * (1d / zoom);

        }



        public void Rotate(float amount)
        {
            angle += amount;
            up = new Vector2(MathF.Sin(angle), MathF.Cos(angle));
        }

        public void GetExtents(out float width, out float height)
        {
            height = (float)GetVisibleHeight();
            width = height * aspectRatio;
        }

        public void GetExtents(out float left, out float right, out float bottom, out float top)
        {
            float halfWidth = (float)(aspectRatio * z * Math.Tan(fieldOfView * 0.5f));
            float halfHeight = (float)(z * Math.Tan(fieldOfView * 0.5f));
            float centerX = position.X;
            float centerY = position.Y;
            left = centerX - halfWidth;
            right = centerX + halfWidth;
            bottom = centerY - halfHeight;
            top = centerY + halfHeight;
        }


        public void GetExtents(out Vector2 min, out Vector2 max)
        {
            GetExtents(out float left, out float right, out float bottom, out float top);

            min = new Vector2(left, bottom);
            max = new Vector2(right, top);
        }


        public void MoveRight(float amount)
        {
            position.X += amount;
        }

        public void MoveUp(float amount)
        {
            position.Y += amount;
        }

    }
}

