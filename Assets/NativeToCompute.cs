#define USE_GRAPHICS_BUFFER

using Unity.Collections;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;
using System.Linq;

using Marshal = System.Runtime.InteropServices.Marshal;

public sealed class NativeToCompute : MonoBehaviour
{
    // Shader/material pair for visualization
    [SerializeField] Shader _shader = null;
    Material _material;

    // GPU buffer
#if USE_GRAPHICS_BUFFER
    GraphicsBuffer _buffer;
#else
    ComputeBuffer _buffer;
#endif

    void Start()
    {
#if USE_GRAPHICS_BUFFER
        _buffer = new GraphicsBuffer
          (GraphicsBuffer.Target.Structured, 256, sizeof(float));
#else
        _buffer = new ComputeBuffer(256, sizeof(float));
#endif

        LoadData(_buffer);

        _material = new Material(_shader);
        _material.SetBuffer("_Data", _buffer);
    }

    void OnDestroy()
    {
        Destroy(_material);
        _buffer.Dispose();
    }

    void OnRenderObject()
    {
        _material.SetPass(0);
        Graphics.DrawProceduralNow(MeshTopology.Triangles, 6, 1);
    }

    //
    // Load data to a GPU buffer
    //
    // 1. Allocate a native memory block (HGlobal).
    // 2. Initialize it as a float array.
    // 3. Wrap it with a NativeArray.
    // 4. Load it into a GPU buffer using SetData.
    //
#if USE_GRAPHICS_BUFFER
    unsafe static void LoadData(GraphicsBuffer buffer)
#else
    unsafe static void LoadData(ComputeBuffer buffer)
#endif
    {
        // Allocate a native memory block.
        var memory = Marshal.AllocHGlobal(256 * sizeof(float));

#if ENABLE_UNITY_COLLECTIONS_CHECKS
        // Safety handle: Needed to support SetData via NativeArray.
        var handle = AtomicSafetyHandle.Create();
#endif

        try
        {
            // Copy a float array to the memory block.
            var data = Enumerable.Range(0, 256).Select(i => i / 255.0f);
            Marshal.Copy(data.ToArray(), 0, memory, 256);

            // Wrap the memory block using NativeArray.
            var view = NativeArrayUnsafeUtility
              .ConvertExistingDataToNativeArray<float>
              ((void*)memory, 256, Allocator.None);

#if ENABLE_UNITY_COLLECTIONS_CHECKS
            // Bind the safety handle to allow read-access from SetData.
            NativeArrayUnsafeUtility
              .SetAtomicSafetyHandle(ref view, handle);
#endif

            // Load the data into the GPU buffer.
            buffer.SetData(view);
        }
        finally
        {
            // Clean-up
            Marshal.FreeHGlobal(memory);
#if ENABLE_UNITY_COLLECTIONS_CHECKS
            AtomicSafetyHandle.Release(handle);
#endif
        }
    }
}
