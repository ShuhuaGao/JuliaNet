using System;
using System.Runtime.InteropServices;


namespace JuliaNet
{
    using size_t = Int32; // to be compatible with `Array.Length` in C#

    /// <summary>
    /// P/Invoke the C API of Julia. 
    /// Check the <see href="https://github1s.com/JuliaLang/julia/blob/HEAD/src/julia.h">julia.h</see>.
    /// </summary>
    internal static class JlNative
    {
        public const string JULIALIB = "libjulia";

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jl_init__threading();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jl_is_initialized();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern string jl_ver_string();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jl_ver_major();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jl_ver_minor();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jl_ver_is_release();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern int jl_ver_patch();

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr jl_eval_string(string str);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern void jl_atexit_hook(int status);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr jl_symbol(string str);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_get_global(IntPtr module, IntPtr sym);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_call1(IntPtr function, IntPtr arg);

        // jl_value_t *jl_call2(jl_function_t *f JL_MAYBE_UNROOTED, jl_value_t *a JL_MAYBE_UNROOTED, jl_value_t *b JL_MAYBE_UNROOTED);
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_call2(IntPtr function, IntPtr arg1, IntPtr arg2);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_call3(IntPtr function, IntPtr arg1, IntPtr arg2, IntPtr arg3);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_box_float64(double x);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern double jl_unbox_float64(IntPtr v);

        // jl_value_t *jl_apply_array_type(jl_value_t *type, size_t dim)
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_apply_array_type(IntPtr type, size_t dim);

        // jl_array_t *jl_alloc_array_1d(jl_value_t *atype, size_t nr);
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_alloc_array_1d(IntPtr type, size_t n);

        // jl_array_t *jl_alloc_array_2d(jl_value_t *atype, size_t nr, size_t nc);
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_alloc_array_2d(IntPtr arrayType, size_t nr, size_t nc);


        // jl_value_t *jl_get_field(jl_value_t *o, const char *fld)
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        public static extern IntPtr jl_get_field(IntPtr obj, [MarshalAs(UnmanagedType.LPStr)] string field);

        // jl_value_t *jl_get_nth_field(jl_value_t *v, size_t i)
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_get_nth_field(IntPtr type, size_t i);


        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_gc_collect(int mode);

        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_ptr_to_array_1d(IntPtr arrayType, IntPtr data, size_t length, int own_buffer);

        // jl_array_t *jl_ptr_to_array(jl_value_t *atype, void *data, jl_value_t* dims, int own_buffer);
        [DllImport(JULIALIB, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr jl_ptr_to_array(IntPtr arrayType, IntPtr data, IntPtr dims, int own_buffer);

        /// <summary>
        /// Get a pointer to the data of a Julia array.
        /// Refer to <see href="https://github.com/ShuhuaGao/JuliaCSharp/tree/main/Embedding/EJArrays"/>
        /// </summary>
        /// <param name="jlArray">a Julia array, equivalent to `jl_array_t *` in C</param>
        /// <returns>a pointer to the internal data</returns>
        public static IntPtr jl_array_data(IntPtr jlArray) => Marshal.ReadIntPtr(jlArray);
    }
}
