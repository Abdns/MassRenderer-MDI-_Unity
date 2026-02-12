using MassRendererSystem;
using MassRendererSystem.Data;
using UnityEngine;

/// <summary>
/// Sample MonoBehaviour demonstrating grass/foliage mass rendering.
/// Shows how to set up and configure grass instances distributed across an area.
/// Supports optional GPU frustum culling for improved performance.
/// </summary>
public class GrassSimulationSample : MonoBehaviour
{
    [SerializeField] private RenderStaticData _data;
    [SerializeField] private int _instanceCount = 1000;
    [SerializeField] private bool _isUseVAT = true;
    [SerializeField] private Bounds _renderBounds = new Bounds(Vector3.zero, Vector3.one * 10000f);

    [Header("Grid Settings")]
    [SerializeField] private Vector2 _areaSize = new Vector2(50, 50);
    [Range(0f, 5f)]
    [SerializeField] private float _positionJitter = 0.5f;

    [Header("Frustum Culling")]
    [SerializeField] private bool _enableFrustumCulling = true;
    [SerializeField] private ComputeShader _frustumCullingShader;
    [SerializeField] private float _boundingSphereRadius = 1f;
    [SerializeField] private Camera _cullingCamera;

    [Header("Transform")]
    [SerializeField] private Vector3 position = Vector3.zero;
    [SerializeField] private Vector3 euler = Vector3.zero;
    [SerializeField] private Vector3 scale = Vector3.one;

    private MassRenderer _renderer;
    private GrassSimulation _grassSimulation;

    private void Start()
    {
        if (_data == null)
        {
            Debug.LogError("[GrassSimulationSample] RenderStaticData is not assigned!");
            return;
        }

        MassRendererParams msParams = new MassRendererParams
        {
            IsVATEnable = _isUseVAT,
            InstanceCount = _instanceCount,
            RenderBounds = _renderBounds,
            ShaderType = MassRenderShaderType.SimpleLit,
            IsFrustumCullingEnabled = _enableFrustumCulling,
            FrustumCullingShader = _frustumCullingShader,
            BoundingSphereRadius = _boundingSphereRadius,
            CullingCamera = _cullingCamera
        };

        _renderer = new MassRenderer(_data, msParams);
        _renderer.Initialize();
        UpdateTransform();

        _grassSimulation = new GrassSimulation(
            _data,
            _renderer.InstancesDataBuffer,
            _instanceCount,
            _areaSize,
            _positionJitter
        );

        _grassSimulation.InitBuffers();

        _renderer.RebuildDrawCommands(_grassSimulation.InstanceCounts);
    }

    private void Update()
    {
        _renderer?.Render();
    }

    private void OnDestroy()
    {
        _renderer?.Dispose();
    }

    private void OnValidate()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        if (_renderer != null)
        {
            Matrix4x4 globalMatrix = Matrix4x4.TRS(position, Quaternion.Euler(euler), scale);
            _renderer.SetGlobalTransform(globalMatrix);
        }
    }
}
