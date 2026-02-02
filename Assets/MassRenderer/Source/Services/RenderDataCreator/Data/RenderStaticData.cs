using System.Collections.Generic;
using UnityEngine;
using VATBakerSystem;

namespace MassRendererSystem.Data
{
    [CreateAssetMenu(fileName = "NewRenderStaticData", menuName = "Mass Renderer/Static Data")]
    public class RenderStaticData : ScriptableObject
    {
        [Header("Meshes")]
        [SerializeField] private List<Mesh> _prototypeMeshes;
        [SerializeField] private Mesh _mergedPrototypeMeshes;

        [Header("Textures")]
        [SerializeField] private Texture2DArray _textureSkins;

        [Header("Atlas Data")]
        [SerializeField] private VATAtlasData _atlasData;

        [Header("Configuration")]
        [SerializeField] private PrototypesRenderData _prototypesData;

        public IReadOnlyList<Mesh> PrototypeMeshes => _prototypeMeshes;
        public Mesh MergedPrototypeMeshes => _mergedPrototypeMeshes;
        public Texture2DArray TextureSkins => _textureSkins;
        public VATAtlasData AtlasData => _atlasData;
        public PrototypesRenderData PrototypesData => _prototypesData;

        public void Initialize(List<Mesh> prototypeMeshes, Mesh mergedMesh, Texture2DArray textureSkins, VATAtlasData atlasData, PrototypesRenderData meshConfig)
        {
            _prototypeMeshes = prototypeMeshes;
            _mergedPrototypeMeshes = mergedMesh;
            _textureSkins = textureSkins;
            _atlasData = atlasData;
            _prototypesData = meshConfig;
        }

        public (int start, int end) GetSkinTextureIndexRange(int meshIndex)
        {
            if (!IsValidIndex(meshIndex)) return (0, 0);

            int skinCount = _prototypesData.skinsForMeshCount[meshIndex];
            int offset = _prototypesData.skinOffsets[meshIndex];

            return (offset, offset + skinCount);
        }

        public (int start, int end) GetAnimationIndexRange(int meshIndex)
        {
            if (_atlasData == null || _atlasData.vatAtlasSegs == null || !IsValidIndex(meshIndex))
                return (0, 0);

            VATAtlasSegmentsInfo segment = _atlasData.vatAtlasSegs[meshIndex];

            return (segment.ClipsStartIndex, segment.ClipsStartIndex + segment.ClipCount);
        }

        private bool IsValidIndex(int index)
        {
            if (_prototypesData.skinsForMeshCount == null)
                return false;

            return index >= 0 && index < _prototypesData.skinsForMeshCount.Length;
        }
    }
}