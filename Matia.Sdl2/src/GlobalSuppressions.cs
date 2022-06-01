using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage(
    "Performance",
    "CA1822:Mark members as static",
    Justification = "The Application requires to be initialized to modify its subsystems for safety.",
    Scope = "type",
    Target = "~T:Matia.Sdl2.Application"
)]