// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;

namespace MySqlSharp
{
    public enum mysql_protocol_type
    {
        MYSQL_PROTOCOL_DEFAULT,
        MYSQL_PROTOCOL_TCP,
        MYSQL_PROTOCOL_SOCKET,
        MYSQL_PROTOCOL_PIPE,
        MYSQL_PROTOCOL_MEMORY
    }
}
