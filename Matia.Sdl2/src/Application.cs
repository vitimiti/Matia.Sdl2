using System.Runtime.InteropServices;

using Matia.Sdl2.CustomMarshalers;
using Matia.Sdl2.Utilities;

namespace Matia.Sdl2;

/// <summary>Create an SDL application.</summary>
public sealed class Application : IDisposable
{
    /// <summary>Get the initialized <see cref="Subsystems"/>.</summary>
    public static Subsystems InitializedSystems => WasInit(Subsystems.Everything);
    /// <summary>Get the SDL revision string.</summary>
    /// <remarks>This is not an increasing number and only serves for logging purposes.</remarks>
    public static string Revision => GetRevision();

    /// <summary>The SDL version that is currently being used.</summary>
    public static Version Version
    {
        get
        {
            GetVersion(out var ver);
            return new(ver.Major, ver.Minor, ver.Patch);
        }
    }

    /// <summary>The application constructor.</summary>
    /// <param name="subsystems">
    ///     The <see cref="Subsystems"/> to initialize with the application.
    /// </param>
    /// <exception cref="ExternalException">When SDL fails to initialize.</exception>
    public Application(Subsystems subsystems)
    {
        var error = Init(subsystems);
        if (error < 0) throw new ExternalException(Error.Message, error);
    }

    /// <summary>The application destructor.</summary>
    /// <remarks>You should use <see cref="Dispose()"/> instead.</remarks>
    ~Application()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: false);
    }

    /// <summary>The dispose implementation.</summary>
    /// <remarks>Disposing the SDL application will safely quit SDL automatically.</remarks>
    public void Dispose()
    {
        // Do not change this code. Put cleanup code in 'Dispose(bool disposing)' method
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }

    /// <summary>Add a new system from <see cref="Subsystems"/>.</summary>
    /// <param name="subsystems">
    ///     The <see cref="Subsystems"/> to add to the already running application.
    /// </param>
    /// <exception cref="ExternalException">
    ///     When SDL is unable to initialize the subsystems.
    /// </exception>
    // [SuppressMessage(
    //     "Performance",
    //     "CA1822:Mark members as static",
    //     Justification = "The Application requires to be initialized to modify its subsystems for safety."
    // )]
    public void AddSubsystems(Subsystems subsystems)
    {
        var error = InitSubSystem(subsystems);
        if (error < 0) throw new ExternalException(Error.Message, error);
    }

    /// <summary>Remove a system from <see cref="Subsystems"/>.</summary>
    /// <param name="subsystems">
    ///     The <see cref="Subsystems"/> to quit from the already running application.
    /// </param>
    public void RemoveSubsystems(Subsystems subsystems)
    {
        QuitSubSystem(subsystems);
    }

    #region Private

    private bool _disposedValue;

    private void Dispose(bool disposing)
    {
        if (!_disposedValue)
        {
            if (disposing)
            {
            }

            Quit();
            _disposedValue = true;
        }
    }

    #endregion Private

    #region Imports

    [StructLayout(LayoutKind.Sequential)]
    private struct SdlVersion
    {
        public readonly byte Major;
        public readonly byte Minor;
        public readonly byte Patch;
    }

    [DllImport("SDL2", EntryPoint = "SDL_Init")]
    private static extern int Init(Subsystems flags);

    [DllImport("SDL2", EntryPoint = "SDL_InitSubSystem")]
    private static extern int InitSubSystem(Subsystems flags);

    [DllImport("SDL2", EntryPoint = "SDL_QuitSubSystem")]
    private static extern void QuitSubSystem(Subsystems flags);

    [DllImport("SDL2", EntryPoint = "SDL_WasInit")]
    private static extern Subsystems WasInit(Subsystems flags);

    [DllImport("SDL2", EntryPoint = "SDL_Quit")]
    private static extern void Quit();

    [DllImport("SDL2", EntryPoint = "SDL_GetVersion")]
    private static extern void GetVersion(out SdlVersion ver);

    [DllImport("SDL2", EntryPoint = "SDL_GetRevision", CharSet = CharSet.Unicode)]
    [return: MarshalAs(
        UnmanagedType.CustomMarshaler,
        MarshalTypeRef = typeof(StringUtf8Marshaler),
        MarshalCookie = StringUtf8Marshaler.LeaveAllocatedCookie
    )]
    private static extern string GetRevision();

    #endregion Imports
}