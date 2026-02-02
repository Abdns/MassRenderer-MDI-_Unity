using System;
using System.Runtime.InteropServices;
using UnityEngine;

namespace MassRendererSystem.Data
{
    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PrototypesMeshSegment
    {
        public int BaseVertex;
        public int StartIndex;
        public int IndexCount;
        public int MeshIndex;
    }

    [System.Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public struct PrototypesRenderData
    {
        [SerializeField]
        public int[] skinOffsets;
        [SerializeField]
        public int[] skinsForMeshCount;
        [SerializeField]
        public PrototypesMeshSegment[] mergedMeshData;

    }
}

