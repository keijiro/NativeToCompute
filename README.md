NativeToCompute
---------------

This is a small Unity sample project showing how to copy data from a native
memory block into a GPU buffer ([ComputeBuffer]/[GraphicsBuffer]).

It uses [ConvertExistingDataToNativeArray] to wrap a memory block with a
[NativeArray] struct. This approach is convenient to feed data from C++ native
code to Unity APIs.

[ComputeBuffer]: https://docs.unity3d.com/ScriptReference/ComputeBuffer.html
[GraphicsBuffer]: https://docs.unity3d.com/ScriptReference/GraphicsBuffer.html
[ConvertExistingDataToNativeArray]:
  https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray.html
[NativeArray]: https://docs.unity3d.com/ScriptReference/Unity.Collections.NativeArray_1.html
