﻿using Murder.Components;

namespace Murder.Core.Geometry
{
    public readonly struct Circle
    {
        public readonly float X;
        public readonly float Y;
        public readonly float Radius;
        internal Vector2 Center => new Vector2(X, Y);

        public Circle(float radius)
        {
            X = 0;
            Y = 0;
            Radius = radius;
        }
        public Circle(float x, float y, float radius)
        {
            X = x;
            Y = y;
            Radius = radius;
        }

        public Circle AddPosition(PositionComponent position) => new Circle(X + position.X, Y + position.Y, Radius);
        public Circle AddPosition(Point position) => new Circle(X + position.X, Y + position.Y, Radius);
        public bool Contains(Point point) => (new Vector2(X, Y) - point).LengthSquared() < MathF.Pow(Radius, 2);
    }
}
