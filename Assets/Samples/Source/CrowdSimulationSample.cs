using MassRendererSystem;
using MassRendererSystem.Data;
using UnityEngine;

/// <summary>
/// Sample MonoBehaviour demonstrating GPU-accelerated crowd simulation.
/// Shows how to set up and use MassRenderer with CrowdSimulation for animated character rendering.
/// Supports optional GPU frustum culling for improved performance.
/// </summary>
public class CrowdSimulationSample : MonoBehaviour
{
    [Header("Settings")]
    public RenderStaticData data;
    public ComputeShader computeCS;
    public Bounds renderBounds = new Bounds(Vector3.zero, Vector3.one * 10000f);
    public int instanceCount = 512;
    public bool useVat = true;
    public Material mdi;

    [Header("Frustum Culling")]
    public bool enableFrustumCulling = true;
    public ComputeShader frustumCullingShader;
    public float boundingSphereRadius = 2f;
    public Camera cullingCamera;

    [Header("Transform")]
    public Vector3 euler = new Vector3(0, 45, 0);
    public Vector3 scale = new Vector3(1.5f, 1.5f, 1.5f);

    private MassRenderer _renderer;
    private CrowdSimulation _crowdSimulation;

    private void Start()
    {
        MassRendererParams msParams = new MassRendererParams
        {
            IsVATEnable = useVat,
            InstanceCount = instanceCount,
            RenderBounds = renderBounds,
            ShaderType = MassRenderShaderType.Lit,
            IsFrustumCullingEnabled = enableFrustumCulling,
            FrustumCullingShader = frustumCullingShader,
            BoundingSphereRadius = boundingSphereRadius,
            CullingCamera = cullingCamera
        };

        _renderer = new MassRenderer(data, msParams, mdi);
        _renderer.Initialize();
        UpdateTransform();

        _crowdSimulation = new CrowdSimulation(instanceCount, _renderer.InstancesDataBuffer, data, computeCS);
        _crowdSimulation.Initialize();

        _renderer.RebuildDrawCommands(_crowdSimulation.InstanceCounts);
    }

    private void Update()
    {
        _crowdSimulation?.Simulate();
        _renderer?.Render();
    }

    private void OnDestroy()
    {
        _renderer?.Dispose();
        _crowdSimulation?.ReleaseBuffers();
    }

    private void OnValidate()
    {
        UpdateTransform();
    }

    private void UpdateTransform()
    {
        if (_renderer != null)
        {
            Matrix4x4 globalMatrix = Matrix4x4.TRS(transform.position, Quaternion.Euler(euler), scale);
            _renderer.SetGlobalTransform(globalMatrix);
        }
    }
}
