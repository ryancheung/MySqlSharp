// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_TIME
    {
        public uint year;
        public uint month;
        public uint day;
        public uint hour;
        public uint minute;
        public uint second;
        public UIntPtr second_part; /**< microseconds */
        public bool neg;
        public enum_mysql_timestamp_type time_type;
        /// The time zone displacement, specified in seconds.
        public int time_zone_displacement;
    }
}
