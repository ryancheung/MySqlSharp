// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    /// <summary>
    /// MYSQL_TIME struct for MySQL 5.7
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_TIME_OLD
    {
        public uint year;
        public uint month;
        public uint day;
        public uint hour;
        public uint minute;
        public uint second;
        public CULong second_part; /**< microseconds */
        public bool neg;
        public enum_mysql_timestamp_type time_type;
    }
}
