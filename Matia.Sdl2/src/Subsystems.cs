namespace Matia.Sdl2;

/// <summary>The subsystems flags to use in <see cref="Application"/>.</summary>
[Flags]
public enum Subsystems : uint
{
    /// <summary>No subsystems.</summary>
    None = 0x00000000U,

    /// <summary>The timer subsystem.</summary>
    Timer = 0x00000001U,
    /// <summary>The audio subsystem.</summary>
    Audio = 0x00000010U,
    /// <summary>The video subsystem.</summary>
    /// <remarks>Initializing this subsystem initializes <see cref="Events"/>.</remarks>
    Video = 0x00000020U,
    /// <summary>The joystick subsystem.</summary>
    /// <remarks>Initializing this subsystem initializes <see cref="Events"/>.</remarks>
    Joystick = 0x00000200U,
    /// <summary>The haptic subsystem.</summary>
    Haptic = 0x00001000U,
    /// <summary>The game controller subsystem.</summary>
    /// <remarks>
    ///     Initializing this subsystem initializes <see cref="Events"/> and <see cref="Joystick"/>.
    /// </remarks>
    GameController = 0x00002000U,
    /// <summary>The events subsystem.</summary>
    Events = 0x00004000U,
    /// <summary>The sensor subsystem.</summary>
    /// <remarks>Initializing this subsystem initializes <see cref="Events"/>.</remarks>
    Sensor = 0x00008000U,

    /// <summary>All subsystems.</summary>
    Everything = Timer | Audio | Video | Joystick | Haptic | GameController | Events | Sensor
}