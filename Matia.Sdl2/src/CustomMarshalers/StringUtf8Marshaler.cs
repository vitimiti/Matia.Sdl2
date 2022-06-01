using System.Runtime.InteropServices;

namespace Matia.Sdl2.CustomMarshalers;

internal sealed class StringUtf8Marshaler : ICustomMarshaler
{
    public const string LeaveAllocatedCookie = "LeaveAllocated";

    public static ICustomMarshaler? GetInstance(string cookie)
    {
        return cookie switch
        {
            LeaveAllocatedCookie => _allocatedInstance ?? new StringUtf8Marshaler(true),
            _ => _defaultInstance ?? new StringUtf8Marshaler(false)
        };
    }

    public StringUtf8Marshaler(bool isAllocated)
    {
        _isAllocated = isAllocated;
    }

    public void CleanUpManagedData(object ManagedObj)
    {
        if (ManagedObj is not string)
        {
            throw new ArgumentException(
                $"{nameof(ManagedObj)} was expected to be of type {typeof(string)} but was instead of type {ManagedObj.GetType()}",
                nameof(ManagedObj)
            );
        }
    }

    public void CleanUpNativeData(IntPtr pNativeData)
    {
        if (!_isAllocated) Marshal.FreeCoTaskMem(pNativeData);
    }

    public int GetNativeDataSize()
    {
        return -1;
    }

    public IntPtr MarshalManagedToNative(object ManagedObj)
    {
        if (ManagedObj is not string str)
        {
            throw new ArgumentException(
                $"{nameof(ManagedObj)} was expected to be of type {typeof(string)} but was instead of type {ManagedObj.GetType()}",
                nameof(ManagedObj)
            );
        }

        return Marshal.StringToCoTaskMemUTF8(str);
    }

    public object MarshalNativeToManaged(IntPtr pNativeData)
    {
        return Marshal.PtrToStringUTF8(pNativeData) ?? string.Empty;
    }

    #region Private

    private readonly bool _isAllocated;

    private static readonly ICustomMarshaler? _defaultInstance;
    private static readonly ICustomMarshaler? _allocatedInstance;

    #endregion
}