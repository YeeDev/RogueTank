using UnityEngine;

namespace Yee.Math
{
    public class MathY : MonoBehaviour
    {
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
    }
}