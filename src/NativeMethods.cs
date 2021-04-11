// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Text;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    public static unsafe class NativeMethods
    {
        public static readonly List<string> MySqlLibrarySearchPaths = new();
        public const string MySqlLibraryName = "MYSQL";
        public static readonly string MySqlLibrarySuffix;

        static NativeMethods()
        {
            if (OperatingSystem.IsWindows())
                MySqlLibrarySuffix = ".dll";
            else if (OperatingSystem.IsMacOS())
                MySqlLibrarySuffix = ".dylib";
            else if (OperatingSystem.IsLinux())
                MySqlLibrarySuffix = ".so";
            else
                MySqlLibrarySuffix = ".so";

            var appDir = AppContext.BaseDirectory;

            MySqlLibrarySearchPaths.Add(appDir);

            if (OperatingSystem.IsWindows())
            {
                string ProgramFiles = Environment.Is64BitProcess ?
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles) :
                    Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);

                string arch = Environment.Is64BitProcess ? "win-x64" : "win-x86";
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, "runtimes", arch, "native"));
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, Environment.Is64BitProcess ? "x64" : "x86"));
                MySqlLibrarySearchPaths.Add(Path.Combine(ProgramFiles, "MySQL", "MySQL Server 8.0", "lib"));
                MySqlLibrarySearchPaths.Add(Path.Combine(ProgramFiles, "MySQL", "MySQL Server 5.7", "lib"));
                MySqlLibrarySearchPaths.Add(Path.Combine(ProgramFiles, "MySQL", "MySQL Server 8.0", "lib", "opt"));
                MySqlLibrarySearchPaths.Add(Path.Combine(ProgramFiles, "MySQL", "MySQL Server 5.7", "lib", "opt"));
                MySqlLibrarySearchPaths.Add(Path.Combine(ProgramFiles, "MySQL", "lib"));
                MySqlLibrarySearchPaths.Add("C:/MySQL/lib/debug");
            }
            else
            {
                string arch = OperatingSystem.IsMacOS() ? "osx" : "linux-" + (Environment.Is64BitProcess ? "x64" : "x86");
                MySqlLibrarySearchPaths.Add(Path.Combine(appDir, "runtimes", arch, "native"));
                MySqlLibrarySearchPaths.Add("/usr/lib");
                MySqlLibrarySearchPaths.Add("/usr/lib/mysql");
                MySqlLibrarySearchPaths.Add("/usr/local/lib");
                MySqlLibrarySearchPaths.Add("/usr/local/lib/mysql");
                MySqlLibrarySearchPaths.Add("/usr/local/mysql/lib");
            }

            NativeLibrary.SetDllImportResolver(typeof(NativeMethods).Assembly, ImportResolver);
        }

        private static IntPtr ImportResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName != MySqlLibraryName) return default;

            // Correct libraryName dependent on platforms
            if (OperatingSystem.IsWindows())
                libraryName = "libmysql";
            else if (OperatingSystem.IsMacOS())
                libraryName = "libmysqlclient";
            else if (OperatingSystem.IsLinux())
                libraryName = "libmysqlclient";
            else
                libraryName = "libmysqlclient";

            IntPtr handle;
            var success = NativeLibrary.TryLoad(libraryName, typeof(NativeMethods).Assembly,
                DllImportSearchPath.ApplicationDirectory | DllImportSearchPath.UserDirectories | DllImportSearchPath.UseDllDirectoryForDependencies,
                out handle);

            foreach (var path in MySqlLibrarySearchPaths)
            {
                success = NativeLibrary.TryLoad(Path.Combine(path, $"{libraryName}{MySqlLibrarySuffix}"), out handle);
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
        private static extern int mysql_options(IntPtr mysql, mysql_option option, void* args);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_options(IntPtr mysql, mysql_option option, [MarshalAs(UnmanagedType.LPStr)] string args);

        public static int mysql_options(IntPtr mysql, mysql_option option, bool args)
        {
            byte val = args ? (byte)1 : (byte)0;

            return mysql_options(mysql, option, &val);
        }

        public static int mysql_options(IntPtr mysql, mysql_option option, int args)
        {
            return mysql_options(mysql, option, &args);
        }

        public static int mysql_options(IntPtr mysql, mysql_option option, long args)
        {
            return mysql_options(mysql, option, &args);
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int mysql_get_option(IntPtr mysql, mysql_option option, void* args);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int mysql_get_option(IntPtr mysql, mysql_option option, ref IntPtr args);

        public static int mysql_get_option(IntPtr mysql, mysql_option option, out string args)
        {
            IntPtr result = default;
            var ret = mysql_get_option(mysql, option, ref result);
            args = Marshal.PtrToStringAnsi(result) ?? string.Empty;
            return ret;
        }

        public static int mysql_get_option(IntPtr mysql, mysql_option option, out bool args)
        {
            byte temp = 0;
            var ret = mysql_get_option(mysql, option, &temp);
            args = temp == 1;
            return ret;
        }

        public static int mysql_get_option(IntPtr mysql, mysql_option option, out int args)
        {
            int temp = 0;
            var ret = mysql_get_option(mysql, option, &temp);
            args = temp;
            return ret;
        }

        public static int mysql_get_option(IntPtr mysql, mysql_option option, out long args)
        {
            long temp = 0;
            var ret = mysql_get_option(mysql, option, &temp);
            args = temp;
            return ret;
        }



        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_errno(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern IntPtr mysql_real_connect(IntPtr mysql,
            [MarshalAs(UnmanagedType.LPStr)] string host,
            [MarshalAs(UnmanagedType.LPStr)] string user,
            [MarshalAs(UnmanagedType.LPStr)] string passwd,
            [MarshalAs(UnmanagedType.LPStr)] string db, uint port,
            [MarshalAs(UnmanagedType.LPStr)] string unix_socket, UIntPtr clientflag = default);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_get_client_info")]
        public static extern IntPtr _mysql_get_client_info();
        public static string mysql_get_client_info()
        {
            return Marshal.PtrToStringAnsi(_mysql_get_client_info()) ?? string.Empty;
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_get_server_info")]
        public static extern IntPtr _mysql_get_server_info(IntPtr mysql);
        public static string mysql_get_server_info(IntPtr mysql)
        {
            return Marshal.PtrToStringAnsi(_mysql_get_server_info(mysql)) ?? string.Empty;
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_autocommit(IntPtr mysql, [MarshalAs(UnmanagedType.U1)] bool auto_mode);



        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern MYSQL_STMT* mysql_stmt_init(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_stmt_close(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_stmt_bind_param(MYSQL_STMT* stmt, MYSQL_BIND* bnd);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_stmt_bind_result(MYSQL_STMT *stmt, MYSQL_BIND *bnd);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_stmt_free_result(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        private static extern int mysql_stmt_prepare(MYSQL_STMT *stmt, [MarshalAs(UnmanagedType.LPStr)] string query, UIntPtr length);

        public static int mysql_stmt_prepare(MYSQL_STMT *stmt, string query)
        {
            return mysql_stmt_prepare(stmt, query, (UIntPtr)Encoding.UTF8.GetByteCount(query));
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_stmt_execute(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern MYSQL_RES* mysql_stmt_result_metadata(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_stmt_store_result(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern long mysql_stmt_num_rows(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern long mysql_stmt_affected_rows(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_stmt_field_count(MYSQL_STMT *stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, EntryPoint = "mysql_stmt_param_count")]
        private static extern UIntPtr _mysql_stmt_param_count(MYSQL_STMT *stmt);

        public static int mysql_stmt_param_count(MYSQL_STMT *stmt) => (int)_mysql_stmt_param_count(stmt);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, EntryPoint = "mysql_stmt_attr_set")]
        private static extern bool _mysql_stmt_attr_set(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, void* attr);

        public static bool mysql_stmt_attr_set(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, bool attr)
        {
            byte val = attr ? (byte)1 : (byte)0;
            return _mysql_stmt_attr_set(stmt, attr_type, &val);
        }

        public static bool mysql_stmt_attr_set(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, int attr)
        {
            UIntPtr val = (UIntPtr)attr;
            return _mysql_stmt_attr_set(stmt, attr_type, &val);
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, EntryPoint = "mysql_stmt_attr_get")]
        private static extern bool _mysql_stmt_attr_get(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, void *attr);

        public static bool mysql_stmt_attr_get(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, out bool attr)
        {
            byte temp = 0;
            var ret = _mysql_stmt_attr_get(stmt, attr_type, &temp);
            attr = temp == 1;
            return ret;
        }

        public static bool mysql_stmt_attr_get(MYSQL_STMT *stmt, enum_stmt_attr_type attr_type, out int attr)
        {
            UIntPtr temp = default;
            var ret = _mysql_stmt_attr_get(stmt, attr_type, &temp);
            attr = (int)temp;
            return ret;
        }



        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_query(IntPtr mysql, [MarshalAs(UnmanagedType.LPStr)] string sql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern long mysql_affected_rows(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_field_count(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern MYSQL_RES* mysql_store_result(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.U1)]
        public static extern bool mysql_more_results(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_next_result(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern void mysql_free_result(MYSQL_RES *result);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern MYSQL_FIELD* mysql_fetch_fields(MYSQL_RES* res);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern IntPtr* mysql_fetch_row(MYSQL_RES* result);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern UIntPtr* mysql_fetch_lengths(MYSQL_RES* result);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_stmt_fetch(MYSQL_STMT *stmt);


        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern int mysql_ping(IntPtr mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall)]
        public static extern UIntPtr mysql_real_escape_string(IntPtr mysql, IntPtr to, [MarshalAs(UnmanagedType.LPStr)] string from, UIntPtr length);
        public static int mysql_real_escape_string(IntPtr mysql, ref string from)
        {
            if (string.IsNullOrEmpty(from)) return 0;

            var byteCount = Encoding.UTF8.GetByteCount(from);
            var memAllocSize = byteCount * 2 + 1;

            var mem = Marshal.AllocHGlobal(memAllocSize);

            try
            {
                var ret = mysql_real_escape_string(mysql, mem, from, (UIntPtr)byteCount);
                from = Marshal.PtrToStringAnsi(mem) ?? string.Empty;
                return (int)ret;
            }
            finally
            {
                Marshal.FreeHGlobal(mem);
            }
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi)]
        public static extern int mysql_set_character_set(IntPtr mysql, [MarshalAs(UnmanagedType.LPStr)] string csname);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_error")]
        private static extern IntPtr _mysql_error(IntPtr mysql);
        public static string mysql_error(IntPtr mysql)
        {
            return Marshal.PtrToStringAnsi(_mysql_error(mysql)) ?? string.Empty;
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Ansi, EntryPoint = "mysql_stmt_error")]
        private static extern IntPtr _mysql_stmt_error(MYSQL_STMT* stmt);
        public static string mysql_stmt_error(MYSQL_STMT* stmt)
        {
            return Marshal.PtrToStringAnsi(_mysql_stmt_error(stmt)) ?? string.Empty;
        }

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, EntryPoint = "mysql_get_server_version")]
        private static extern UIntPtr _mysql_get_server_version(IntPtr mysql);
        public static int mysql_get_server_version(IntPtr mysql) => (int)_mysql_get_server_version(mysql);

        [DllImport(MySqlLibraryName, CallingConvention = CallingConvention.StdCall, EntryPoint = "mysql_thread_safe")]
        private static extern uint _mysql_thread_safe();
        public static bool mysql_thread_safe() => _mysql_thread_safe() == 1 ? true : false;
    }
}
