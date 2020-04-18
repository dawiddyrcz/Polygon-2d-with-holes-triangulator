namespace PolygonWithHolesTriangulator
{
    internal static class MathHelper
    {
        public static float Max(float d1, float d2)
        {
            if (d1 >= d2) return d1;
            else return d2;
        }

        public static float Min(float d1, float d2)
        {
            if (d1 <= d2) return d1;
            else return d2;
        }

        public static float Clamp(float value, float min, float max)
        {
            var m = Max(value, min);
            return Min(m, max);
        }
    }
}