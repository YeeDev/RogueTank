using UnityEngine;

namespace Yee.Math
{
    public class MathY : MonoBehaviour
    {
        public static bool CheckEqualAxis(Vector3 v1, Vector3 v2)
        {
            bool xEquals = Mathf.RoundToInt(v1.x) == Mathf.RoundToInt(v2.x);
            bool zEquals = Mathf.RoundToInt(v1.z) == Mathf.RoundToInt(v2.z);

            return xEquals || zEquals;
        }

        public static bool CheckEqualAxisY(Vector3 v1, Vector3 v2)
        {
            bool xEquals = Mathf.RoundToInt(v1.x) == Mathf.RoundToInt(v2.x);
            bool yEquals = Mathf.RoundToInt(v1.y) == Mathf.RoundToInt(v2.y);
            bool zEquals = Mathf.RoundToInt(v1.z) == Mathf.RoundToInt(v2.z);

            return xEquals || yEquals || zEquals;
        }

        public static float CalculateFlatAngle(Vector3 v1, Vector3 v2)
        {
            float angle = Vector3.Angle(FlatY(v1), FlatY(v2));
            return angle;
        }

        public static float CalculateForwardAngle(Transform t, Transform target)
        {
            float forwardAngle = Vector3.Angle(t.forward, FlatY(t.position, target.position) - t.position);
            float leftAngle = Vector3.Angle(-t.right, FlatY(t.position, target.position) - t.position);
            float angle = forwardAngle > 90 ? 360 - leftAngle : leftAngle;

            return angle;
        }

        public static Vector3 FlatY(Vector3 v)
        {
            Vector3 flatted = v;
            flatted.y = 0;
            return flatted;
        }

        public static Vector3 FlatY(Vector3 t, Vector3 toFlat)
        {
            toFlat.y = t.y;
            return toFlat;
        }

        public static Vector3 Flat2D(Vector3 v)
        {
            Vector3 flatted = v;
            flatted.z = 0;
            return flatted;
        }

        public static Vector3 Flat2D(Vector3 t, Vector3 toFlat)
        {
            toFlat.z = t.z;
            return toFlat;
        }
    }
}