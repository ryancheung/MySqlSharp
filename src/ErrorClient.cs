// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

namespace MySqlSharp
{
    public enum ErrorClient
    {
        CR_MIN_ERROR = 2000, /* For easier client code */
        CR_MAX_ERROR = 2999,

        /* Do not add error numbers before CR_ERROR_FIRST. */
        /* If necessary to add lower numbers, change CR_ERROR_FIRST accordingly. */
        CR_ERROR_FIRST = 2000, /*Copy first error nr.*/
        CR_UNKNOWN_ERROR = 2000,
        CR_SOCKET_CREATE_ERROR = 2001,
        CR_CONNECTION_ERROR = 2002,
        CR_CONN_HOST_ERROR = 2003,
        CR_IPSOCK_ERROR = 2004,
        CR_UNKNOWN_HOST = 2005,
        CR_SERVER_GONE_ERROR = 2006,
        CR_VERSION_ERROR = 2007,
        CR_OUT_OF_MEMORY = 2008,
        CR_WRONG_HOST_INFO = 2009,
        CR_LOCALHOST_CONNECTION = 2010,
        CR_TCP_CONNECTION = 2011,
        CR_SERVER_HANDSHAKE_ERR = 2012,
        CR_SERVER_LOST = 2013,
        CR_COMMANDS_OUT_OF_SYNC = 2014,
        CR_NAMEDPIPE_CONNECTION = 2015,
        CR_NAMEDPIPEWAIT_ERROR = 2016,
        CR_NAMEDPIPEOPEN_ERROR = 2017,
        CR_NAMEDPIPESETSTATE_ERROR = 2018,
        CR_CANT_READ_CHARSET = 2019,
        CR_NET_PACKET_TOO_LARGE = 2020,
        CR_EMBEDDED_CONNECTION = 2021,
        CR_PROBE_SLAVE_STATUS = 2022,
        CR_PROBE_SLAVE_HOSTS = 2023,
        CR_PROBE_SLAVE_CONNECT = 2024,
        CR_PROBE_MASTER_CONNECT = 2025,
        CR_SSL_CONNECTION_ERROR = 2026,
        CR_MALFORMED_PACKET = 2027,
        CR_WRONG_LICENSE = 2028,

        /* new 4.1 error codes */
        CR_NULL_POINTER = 2029,
        CR_NO_PREPARE_STMT = 2030,
        CR_PARAMS_NOT_BOUND = 2031,
        CR_DATA_TRUNCATED = 2032,
        CR_NO_PARAMETERS_EXISTS = 2033,
        CR_INVALID_PARAMETER_NO = 2034,
        CR_INVALID_BUFFER_USE = 2035,
        CR_UNSUPPORTED_PARAM_TYPE = 2036,

        CR_SHARED_MEMORY_CONNECTION = 2037,
        CR_SHARED_MEMORY_CONNECT_REQUEST_ERROR = 2038,
        CR_SHARED_MEMORY_CONNECT_ANSWER_ERROR = 2039,
        CR_SHARED_MEMORY_CONNECT_FILE_MAP_ERROR = 2040,
        CR_SHARED_MEMORY_CONNECT_MAP_ERROR = 2041,
        CR_SHARED_MEMORY_FILE_MAP_ERROR = 2042,
        CR_SHARED_MEMORY_MAP_ERROR = 2043,
        CR_SHARED_MEMORY_EVENT_ERROR = 2044,
        CR_SHARED_MEMORY_CONNECT_ABANDONED_ERROR = 2045,
        CR_SHARED_MEMORY_CONNECT_SET_ERROR = 2046,
        CR_CONN_UNKNOW_PROTOCOL = 2047,
        CR_INVALID_CONN_HANDLE = 2048,
        CR_UNUSED_1 = 2049,
        CR_FETCH_CANCELED = 2050,
        CR_NO_DATA = 2051,
        CR_NO_STMT_METADATA = 2052,
        CR_NO_RESULT_SET = 2053,
        CR_NOT_IMPLEMENTED = 2054,
        CR_SERVER_LOST_EXTENDED = 2055,
        CR_STMT_CLOSED = 2056,
        CR_NEW_STMT_METADATA = 2057,
        CR_ALREADY_CONNECTED = 2058,
        CR_AUTH_PLUGIN_CANNOT_LOAD = 2059,
        CR_DUPLICATE_CONNECTION_ATTR = 2060,
        CR_AUTH_PLUGIN_ERR = 2061,
        CR_INSECURE_API_ERR = 2062,
        CR_FILE_NAME_TOO_LONG = 2063,
        CR_SSL_FIPS_MODE_ERR = 2064,
        CR_DEPRECATED_COMPRESSION_NOT_SUPPORTED = 2065,
        CR_COMPRESSION_WRONGLY_CONFIGURED = 2066,
        CR_KERBEROS_USER_NOT_FOUND = 2067,
        CR_LOAD_DATA_LOCAL_INFILE_REJECTED = 2068,
        CR_LOAD_DATA_LOCAL_INFILE_REALPATH_FAIL = 2069,
        CR_DNS_SRV_LOOKUP_FAILED = 2070,
        CR_ERROR_LAST = 2070,
    }
}
