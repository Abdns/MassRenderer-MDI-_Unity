using UnityEngine;

namespace VATBakerSystem
{
    [CreateAssetMenu(menuName = "VAT/Baker Settings")]
    public class VATBakerSettings : ScriptableObject
    {
        [Min(1f)]
        public float samplesPerSecond = 60f;
        public VATPrecision precision = VATPrecision.Float;
        public FilterMode filter = FilterMode.Bilinear;
        public TextureWrapMode wrap = TextureWrapMode.Clamp;
        public TextureFormat TextureFormat => precision == VATPrecision.Float
        ? TextureFormat.RGBAFloat
        : TextureFormat.RGBAHalf;
    }

    public enum VATPrecision : byte
    {
        Half = 0,
        Float = 1
    }
}
