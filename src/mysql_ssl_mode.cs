// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;

namespace MySqlSharp
{
    public enum mysql_ssl_mode
    {
        SSL_MODE_DISABLED = 1,
        SSL_MODE_PREFERRED,
        SSL_MODE_REQUIRED,
        SSL_MODE_VERIFY_CA,
        SSL_MODE_VERIFY_IDENTITY
    }
}
