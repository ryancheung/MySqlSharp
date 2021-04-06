// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_DATA
    {
        public MYSQL_ROWS* data;
        public void* alloc;
        public ulong rows;
        public uint fields;
    }
}