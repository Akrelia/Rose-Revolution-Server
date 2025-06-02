using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Data
{
    public struct Vector3
    {
        public float x;
        public float y;
        public float z;

        public static readonly Vector3 Zero = new Vector3(0f, 0f, 0f);
        public static readonly Vector3 One = new Vector3(1f, 1f, 1f);
        public static readonly Vector3 Up = new Vector3(0f, 1f, 0f);
        public static readonly Vector3 Down = new Vector3(0f, -1f, 0f);
        public static readonly Vector3 Left = new Vector3(-1f, 0f, 0f);
        public static readonly Vector3 Right = new Vector3(1f, 0f, 0f);
        public static readonly Vector3 Forward = new Vector3(0f, 0f, 1f);
        public static readonly Vector3 Back = new Vector3(0f, 0f, -1f);

        public Vector3(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static Vector3 operator +(Vector3 a, Vector3 b) =>
            new Vector3(a.x + b.x, a.y + b.y, a.z + b.z);

        public static Vector3 operator -(Vector3 a, Vector3 b) =>
            new Vector3(a.x - b.x, a.y - b.y, a.z - b.z);

        public static Vector3 operator -(Vector3 a) =>
            new Vector3(-a.x, -a.y, -a.z);

        public static Vector3 operator *(Vector3 a, float d) =>
            new Vector3(a.x * d, a.y * d, a.z * d);

        public static Vector3 operator *(float d, Vector3 a) => a * d;

        public static Vector3 operator /(Vector3 a, float d) =>
            new Vector3(a.x / d, a.y / d, a.z / d);

        public float Magnitude =>
            (float)Math.Sqrt(x * x + y * y + z * z);

        public float SqrMagnitude =>
            x * x + y * y + z * z;

        public Vector3 Normalized =>
            this / Magnitude;

        public static float Dot(Vector3 a, Vector3 b) =>
            a.x * b.x + a.y * b.y + a.z * b.z;

        public static Vector3 Cross(Vector3 a, Vector3 b) =>
            new Vector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );

        public static float Distance(Vector3 a, Vector3 b) =>
            (a - b).Magnitude;

        public override string ToString() =>
            $"({x:0.###}, {y:0.###}, {z:0.###})";

        public override bool Equals(object obj) =>
            obj is Vector3 other &&
            x == other.x && y == other.y && z == other.z;

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = hash * 23 + x.GetHashCode();
                hash = hash * 23 + y.GetHashCode();
                hash = hash * 23 + z.GetHashCode();
                return hash;
            }
        }

        public static bool operator ==(Vector3 lhs, Vector3 rhs) =>
            lhs.Equals(rhs);

        public static bool operator !=(Vector3 lhs, Vector3 rhs) =>
            !lhs.Equals(rhs);
    }
}