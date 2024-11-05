using UnityEngine;

namespace PickAColor
{
    public static class Extentions
    {
        public static string ToRGBHex(this Color color) => $"{color.r.To16()}{color.g.To16()}{color.b.To16()}";

        private static string To16(this float f) => $"{Mathf.Clamp((int)(f * 255), 0, 255):X2}";
    }
}