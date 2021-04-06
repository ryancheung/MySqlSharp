// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System.Runtime.InteropServices;

namespace MySqlSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_BIND
    {
        public uint* length; /* output length pointer */
        public bool* is_null;         /* Pointer to null indicator */
        public void* buffer;          /* buffer to get/put data */
        /* set this if you want to track data truncations happened during fetch */
        public bool* error;
        public byte* row_ptr; /* for the current data position */
        public void* store_param_func;
        public void* fetch_result;
        public void* skip_result;
        /* output buffer length, must be set when fetching str/binary */
        public uint buffer_length;
        public uint offset;              /* offset position for char/binary fetch */
        public uint length_value;        /* Used if length is 0 */
        public uint param_number;         /* For null count and error messages */
        public uint pack_length;          /* Internal length for packed data */
        public enum_field_types buffer_type; /* buffer type */
        public bool error_value;                  /* used if error is 0 */
        public bool is_unsigned;                  /* set if integer type is unsigned */
        public bool long_data_used;               /* If used with mysql_send_long_data */
        public bool is_null_value;                /* Used if is_null is 0 */
        public void* extension;
    }
}
