// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;

namespace MySqlSharp
{
    public enum mysql_option
    {
        MYSQL_OPT_CONNECT_TIMEOUT,
        MYSQL_OPT_COMPRESS,
        MYSQL_OPT_NAMED_PIPE,
        MYSQL_INIT_COMMAND,
        MYSQL_READ_DEFAULT_FILE,
        MYSQL_READ_DEFAULT_GROUP,
        MYSQL_SET_CHARSET_DIR,
        MYSQL_SET_CHARSET_NAME,
        MYSQL_OPT_LOCAL_INFILE,
        MYSQL_OPT_PROTOCOL,
        MYSQL_SHARED_MEMORY_BASE_NAME,
        MYSQL_OPT_READ_TIMEOUT,
        MYSQL_OPT_WRITE_TIMEOUT,
        MYSQL_OPT_USE_RESULT,
        MYSQL_REPORT_DATA_TRUNCATION,
        MYSQL_OPT_RECONNECT,
        MYSQL_PLUGIN_DIR,
        MYSQL_DEFAULT_AUTH,
        MYSQL_OPT_BIND,
        MYSQL_OPT_SSL_KEY,
        MYSQL_OPT_SSL_CERT,
        MYSQL_OPT_SSL_CA,
        MYSQL_OPT_SSL_CAPATH,
        MYSQL_OPT_SSL_CIPHER,
        MYSQL_OPT_SSL_CRL,
        MYSQL_OPT_SSL_CRLPATH,
        MYSQL_OPT_CONNECT_ATTR_RESET,
        MYSQL_OPT_CONNECT_ATTR_ADD,
        MYSQL_OPT_CONNECT_ATTR_DELETE,
        MYSQL_SERVER_PUBLIC_KEY,
        MYSQL_ENABLE_CLEARTEXT_PLUGIN,
        MYSQL_OPT_CAN_HANDLE_EXPIRED_PASSWORDS,
        MYSQL_OPT_MAX_ALLOWED_PACKET,
        MYSQL_OPT_NET_BUFFER_LENGTH,
        MYSQL_OPT_TLS_VERSION,
        MYSQL_OPT_SSL_MODE,
        MYSQL_OPT_GET_SERVER_PUBLIC_KEY,
        MYSQL_OPT_RETRY_COUNT,
        MYSQL_OPT_OPTIONAL_RESULTSET_METADATA,
        MYSQL_OPT_SSL_FIPS_MODE,
        MYSQL_OPT_TLS_CIPHERSUITES,
        MYSQL_OPT_COMPRESSION_ALGORITHMS,
        MYSQL_OPT_ZSTD_COMPRESSION_LEVEL,
        MYSQL_OPT_LOAD_DATA_LOCAL_DIR
    }
}
