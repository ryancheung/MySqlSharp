// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;

namespace MySqlSharp
{
    /// <summary>
    /// MEM_ROOT is only required in MySQL 5.7
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MEM_ROOT
    {
        public void* free;                  /* blocks with free memory in it */
        public void* used;                  /* blocks almost without free memory */
        public void* pre_alloc;             /* preallocated block */
        /* if block have less memory it will be put in 'used' list */
        public nuint min_malloc;
        public nuint block_size;               /* initial block size */
        public uint block_num;          /* allocated blocks counter */
        /* 
            first free block in queue test counter (if it exceed 
            MAX_BLOCK_USAGE_BEFORE_DROP block will be dropped in 'used' list)
        */
        public uint first_block_usage;

        /*
            Maximum amount of memory this mem_root can hold. A value of 0
            implies there is no limit.
        */
        public nuint max_capacity;

        /* Allocated size for this mem_root */

        public nuint allocated_size;

        /* Enable this for error reporting if capacity is exceeded */
        public bool error_for_capacity_exceeded;

        public void* error_handler;

        public uint m_psi_key;
    }
}
