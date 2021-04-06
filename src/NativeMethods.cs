// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    public static unsafe class NativeMethods
    {
        public static readonly List<string> MySqlLibrarySearchPaths = new();
        public const string MySqlLibraryName = "libmysqlclient";
        public static readonly string MySqlLibrarySuffix;

        static NativeMethods()
        {
            if (OperatingSystem.IsWindows())
                MySqlLibrarySuffix = ".dll";
            else if (OperatingSystem.IsMacOS())
                MySqlLibrarySuffix = ".dylib";
            else if (OperatingSystem.IsLinux())
                MySqlLibrarySuffix = ".so";

            var appDir = AppContext.BaseDirectory;

            MySqlLibrarySearchPaths.Add(appDir);

            if (OperatingSystem.IsWindows())
            {
                string arch = Environment.Is64BitProcess ? "win-x64" : "win-x86";
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, "runtimes", arch, "native"));
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, Environment.Is64BitProcess ? "x64" : "x86"));
            }
            else
            {
                string arch = OperatingSystem.IsMacOS() ? "osx" : "linux-" + (Environment.Is64BitProcess ? "x64" : "x86");
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, "runtimes", arch, "native"));
                MySqlLibrarySearchPaths.Add("/usr/lib");
                MySqlLibrarySearchPaths.Add("/usr/local/lib");
                MySqlLibrarySearchPaths.Add("/usr/local/mysql/lib");
            }

            NativeLibrary.SetDllImportResolver(typeof(NativeMethods).Assembly, ImportResolver);
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            IntPtr handle;
            var success = NativeLibrary.TryLoad(MySqlLibraryName, typeof(NativeMethods).Assembly,
                DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies,
                out handle);

            foreach (var path in MySqlLibrarySearchPaths)
            {
                success = NativeLibrary.TryLoad(Path.Combine(path, $"{MySqlLibraryName}{MySqlLibrarySuffix}"), out handle);
                if (success)
                    break;
            }

            return handle;
        }


        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern uint mysql_get_client_version();

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr mysql_init(IntPtr mysql = default);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern void mysql_close(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_options(IntPtr mysql, mysql_option option, void* args);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_get_option(IntPtr mysql, mysql_option option, void* args);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_get_option(IntPtr mysql, mysql_option option, ref void* args);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_errno(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mysql_real_connect(IntPtr mysql,
            [MarshalAs(UnmanagedType.LPStr)] string host,
            [MarshalAs(UnmanagedType.LPStr)] string user,
            [MarshalAs(UnmanagedType.LPStr)] string passwd,
            [MarshalAs(UnmanagedType.LPStr)] string db, uint port,
            [MarshalAs(UnmanagedType.LPStr)] string unix_socket, uint clientflag = 0);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_get_client_info")]
        public static extern IntPtr _mysql_get_client_info();
        public static string mysql_get_client_info()
        {
            return Marshal.PtrToStringAnsi(_mysql_get_client_info());
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_get_server_info")]
        public static extern IntPtr _mysql_get_server_info(IntPtr mysql);
        public static string mysql_get_server_info(IntPtr mysql)
        {
            return Marshal.PtrToStringAnsi(_mysql_get_server_info(mysql));
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_autocommit(IntPtr mysql, [MarshalAs(UnmanagedType.U1)] bool auto_mode);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern MYSQL_STMT* mysql_stmt_init(IntPtr mysql);
    }
}
