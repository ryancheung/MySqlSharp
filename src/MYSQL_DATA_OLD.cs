// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    /// <summary>
    /// MYSQL_DATA struct for MySQL 5.7
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_DATA_OLD
    {
        public MYSQL_ROWS* data;
        public void* embedded_info;
        public MEM_ROOT alloc;
        public ulong rows;
        public uint fields;
        /* extra info for embedded library */
        public void *extension;
    }
}
