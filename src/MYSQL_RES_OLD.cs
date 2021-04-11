// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    /// <summary>
    /// MYSQL_RES struct for MySQL 5.7
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_RES_OLD
    {
        public ulong row_count;
        public MYSQL_FIELD* fields;
        public MYSQL_DATA_OLD* data;
        public MYSQL_ROWS* data_cursor;
        public CULong* lengths; /* column lengths of current row */
        public IntPtr handle;          /* for unbuffered reads */
        public void* methods;
        public IntPtr* row;         /* If unbuffered read */
        public IntPtr* current_row; /* buffer to current row */
        public MEM_ROOT field_alloc;
        public uint field_count;
        public uint current_field;
        public bool eof; /* Used by mysql_fetch_row */
        /* mysql_stmt_close() had to cancel this result */
        public bool unbuffered_fetch_cancelled;
        public void* extension;
    }
}
