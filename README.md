NativeToCompute
---------------

![SDIM4713](https://user-images.githubusercontent.com/343936/132483226-cf2db3e8-2baf-4609-8c48-2e4787c2b604.JPG)

This is a small Unity sample project showing how to directly copy data from
unmanaged memory to a GPU buffer ([ComputeBuffer]/[GraphicsBuffer]).

It uses [ConvertExistingDataToNativeArray] to wrap an unmanaged memory block
with a [NativeArray] struct. This approach is convenient to feed data from C++
native code to Unity APIs.

[ComputeBuffer]: https://docs.unity3d.com/ScriptReference/ComputeBuffer.html
[GraphicsBuffer]: https://docs.unity3d.com/ScriptReference/GraphicsBuffer.html
[ConvertExistingDataToNativeArray]:
  https://docs.unity3d.com/ScriptReference/Unity.Collections.LowLevel.Unsafe.NativeArrayUnsafeUtility.ConvertExistingDataToNativeArray.html
[NativeArray]: https://docs.unity3d.com/ScriptReference/Unity.Collections.NativeArray_1.html
