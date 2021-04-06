// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;

namespace MySqlSharp
{
    public enum enum_mysql_stmt_state
    {
        MYSQL_STMT_INIT_DONE = 1,
        MYSQL_STMT_PREPARE_DONE,
        MYSQL_STMT_EXECUTE_DONE,
        MYSQL_STMT_FETCH_DONE
    }
}
