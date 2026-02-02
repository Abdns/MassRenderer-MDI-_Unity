using MassRendererSystem.Utils;
using System.Collections.Generic;
using UnityEngine;
using VATBakerSystem;
using Object = UnityEngine.Object;

namespace MassRendererSystem.Data
{
    public class RenderDataCreator : MonoBehaviour
    {
        [SerializeField] private PrototypeData[] _prototypes;
        [SerializeField] private VATBakerSettings _bakerSettings;

        public RenderStaticData GenerateAndSave(string savePath)
        {
            var renderData = RenderDataBuilder.BuildRenderData(_prototypes, _bakerSettings);

#if UNITY_EDITOR

            var subAssets = new List<Object>();

            if (renderData.MergedPrototypeMeshes != null)
                subAssets.Add(renderData.MergedPrototypeMeshes);

            if (renderData.TextureSkins != null)
                subAssets.Add(renderData.TextureSkins);

            if (renderData.PrototypeMeshes != null)
            {
                foreach (var mesh in renderData.PrototypeMeshes)
                {
                    if (mesh != null) subAssets.Add(mesh);
                }
            }

            if (renderData.AtlasData?.PositionAtlas != null)
            {
                renderData.AtlasData.PositionAtlas.name = "VAT_Position_Atlas";
                subAssets.Add(renderData.AtlasData.PositionAtlas);
            }

            if (renderData.AtlasData?.NormalAtlas != null)
            {
                renderData.AtlasData.NormalAtlas.name = "VAT_Normal_Atlas";
                subAssets.Add(renderData.AtlasData.NormalAtlas);
            }

            AssetSaver.SaveAsset(renderData, savePath, "RenderData", subAssets);
#endif

            return renderData;
        }
    }

}