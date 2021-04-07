// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_RES
    {
        public ulong row_count;
        public MYSQL_FIELD* fields;
        public MYSQL_DATA* data;
        public MYSQL_ROWS* data_cursor;
        public uint* lengths; /* column lengths of current row */
        public IntPtr handle;          /* for unbuffered reads */
        public void* methods;
        public IntPtr* row;         /* If unbuffered read */
        public IntPtr* current_row; /* buffer to current row */
        public void* field_alloc;
        public uint field_count;
        public uint current_field;
        public bool eof; /* Used by mysql_fetch_row */
        /* mysql_stmt_close() had to cancel this result */
        public bool unbuffered_fetch_cancelled;
        public enum_resultset_metadata metadata;
        public void* extension;
    }
}
