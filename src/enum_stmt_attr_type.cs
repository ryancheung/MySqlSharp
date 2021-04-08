// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

namespace MySqlSharp
{
    public enum enum_stmt_attr_type
    {
        /// <summary>
        /// When doing mysql_stmt_store_result calculate max_length attribute<para/>
        /// of statement metadata. This is to be consistent with the old API,<para/>
        /// where this was done automatically.<para/>
        /// In the new API we do that only by request because it slows down<para/>
        /// mysql_stmt_store_result sufficiently.
        /// </summary>
        STMT_ATTR_UPDATE_MAX_LENGTH,

        /// <summary>
        /// unsigned long with combination of cursor flags (read only, for update, etc)
        /// </summary>
        STMT_ATTR_CURSOR_TYPE,

        /// <summary>
        /// Amount of rows to retrieve from server per one fetch if using cursors.<para/>
        /// Accepts unsigned long attribute in the range 1 - ulong_max
        /// </summary>
        STMT_ATTR_PREFETCH_ROWS
    }
}
