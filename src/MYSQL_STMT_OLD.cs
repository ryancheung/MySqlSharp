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
    public unsafe struct MYSQL_STMT_OLD
    {
        public const int MYSQL_ERRMSG_SIZE = 512;
        public const int SQLSTATE_LENGTH = 5;

        public MEM_ROOT mem_root; /* root allocations */
        public LIST list;                 /* list to keep track of all stmts */
        public IntPtr mysql;              /* connection handle */
        public MYSQL_BIND* params_;        /* input parameters */
        public MYSQL_BIND* bind;          /* output parameters */
        public MYSQL_FIELD* fields;       /* result set metadata */
        public MYSQL_DATA_OLD result;         /* cached result set */
        public MYSQL_ROWS* data_cursor;   /* current row in cached result */
        /*
            mysql_stmt_fetch() calls this function to fetch one row (it's different
            for buffered, unbuffered and cursor fetch).
        */
        public void* read_row_func;
        /* copy of mysql->affected_rows after statement execution */
        public ulong affected_rows;
        public ulong insert_id;          /* copy of mysql->insert_id */
        public CULong stmt_id;       /* Id for prepared statement */
        public CULong flags;         /* i.e. type of cursor to open */
        public CULong prefetch_rows; /* number of rows per one COM_FETCH */
        /*
            Copied from mysql->server_status after execute/fetch to know
            server-side cursor status for this statement.
        */
        public uint server_status;
        public uint last_errno;            /* error code */
        public uint param_count;           /* input parameter count */
        public uint field_count;           /* number of columns in result set */
        public enum_mysql_stmt_state state;   /* statement state */
        public fixed byte last_error[MYSQL_ERRMSG_SIZE]; /* error message */
        public fixed byte sqlstate[SQLSTATE_LENGTH + 1];

        /* Types of input parameters should be sent to server */
        public bool send_types_to_server;
        public bool bind_param_done;           /* input buffers were supplied */
        public byte bind_result_done; /* output buffers were supplied */
        /* mysql_stmt_close() had to cancel this result */
        public bool unbuffered_fetch_cancelled;
        /*
            Is set to true if we need to calculate field->max_length for
            metadata fields when doing mysql_stmt_store_result.
        */
        public bool update_max_length;
        public void* extension;
    }
}
