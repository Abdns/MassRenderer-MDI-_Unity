using System.Runtime.InteropServices;
using UnityEngine;

namespace VATBakerSystem
{
    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    public sealed class VATAtlasData
    {
        public Texture2D PositionAtlas;
        public Texture2D NormalAtlas;
        public int AtlasWidth;   
        public int AtlasHeight;  
        public VATAtlasSegmentsInfo[] vatAtlasSegs;
        public VATAtlasAnimationClip[] allClips;
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct VATAtlasSegmentsInfo
    {
        public float NormalizedOffsetX;
        public float NormalizedWidth;
        public int VertexCount;
        public int AnimationsFramesCount;
        public int ClipsStartIndex;
        public int ClipCount;
    }

    /// <summary>
    /// 
    /// </summary>
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct VATAtlasAnimationClip
    {
        public int VertexCount;
        public int FrameCount;
        public float Duration;
        public float NormalizedOffsetX;
        public float NormalizedOffsetY;
        public float NormalizedWidth;
        public float NormalizedLength;


    }
}