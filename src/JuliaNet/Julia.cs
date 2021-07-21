using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace JuliaNet
{
    using static JlNative;

    /// <summary>
    /// A wrapper class providing access to Julia.
    /// </summary>
    public static class Julia
    {
        public static void Initialize() => jl_init__threading();

        public static bool IsInitialized => jl_is_initialized() != 0;

        public static void Exit(int status) => jl_atexit_hook(status);

        public static string VersionString => jl_ver_string();

        /// <summary>
        /// Get the version of Julia.
        /// </summary>
        public static Version Version => new(jl_ver_major(), jl_ver_minor(), jl_ver_patch());

        /// <summary>
        /// Check whether the current Julia is a stable release (i.e., not an alpha or beta version).
        /// </summary>
        public static bool IsReleaseVersion => jl_ver_is_release() != 0;

    }
}
