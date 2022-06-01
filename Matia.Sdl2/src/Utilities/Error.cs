using System.Runtime.InteropServices;
using Matia.Sdl2.CustomMarshalers;

namespace Matia.Sdl2.Utilities;

internal static class Error
{
    public static string Message => GetError();

    #region Imports

    [DllImport("SDL2", EntryPoint = "SDL_GetError", CharSet = CharSet.Unicode)]
    [return: MarshalAs(
        UnmanagedType.CustomMarshaler,
        MarshalTypeRef = typeof(StringUtf8Marshaler),
        MarshalCookie = StringUtf8Marshaler.LeaveAllocatedCookie
    )]
    private static extern string GetError();

    #endregion
}