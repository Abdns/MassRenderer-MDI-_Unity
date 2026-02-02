using System.Runtime.InteropServices;
using UnityEngine;
using Object = UnityEngine.Object;

namespace VATBakerSystem
{
    [System.Serializable]
    public sealed class VATBakeRequest
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private SkinnedMeshRenderer _skinnedMeshRenderer;

        public Animator Animator => _animator;
        public SkinnedMeshRenderer SkinnedMeshRenderer => _skinnedMeshRenderer;
    }

    public sealed class VATBakeResult
    {
        private Texture2D _positionMap;
        private Texture2D _normalMap;
        private int _totalFrames;
        private int _vertexCount;
        private VATAnimationClip[] _clipInfos;

        public Texture2D PositionMap => _positionMap;
        public Texture2D NormalMap => _normalMap;
        public int TotalFrames => _totalFrames;
        public int VertexCount => _vertexCount;
        public VATAnimationClip[] ClipInfos => _clipInfos; 

        public VATBakeResult(
            Texture2D positionMap,
            Texture2D normalMap,
            int totalFrames,
            int vertexCount, 
            VATAnimationClip[] clipInfos)
        {
            _positionMap = positionMap;
            _normalMap = normalMap;
            _clipInfos = clipInfos;
            _totalFrames = totalFrames;
            _vertexCount = vertexCount;
        }

        public void Dispose()
        {
            if (PositionMap != null)
            {
                Object.DestroyImmediate(PositionMap);
            }
            if (NormalMap != null)
            {
                Object.DestroyImmediate(NormalMap);
            }
        }
    }

    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct VATAnimationClip
    {
        public int FrameCount;
        public int StartFrame;
        public float Duration;
        public float NormalizedStart;
        public float NormalizedLength;
    }
}
