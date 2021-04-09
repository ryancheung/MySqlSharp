// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

namespace MySqlSharp
{
    public enum enum_mysql_timestamp_type
    {
        MYSQL_TIMESTAMP_NONE = -2,
        MYSQL_TIMESTAMP_ERROR = -1,

        /// Stores year, month and day components.
        MYSQL_TIMESTAMP_DATE = 0,

        /**
            Stores all date and time components.
            Value is in UTC for `TIMESTAMP` type.
            Value is in local time zone for `DATETIME` type.
        */
        MYSQL_TIMESTAMP_DATETIME = 1,

        /// Stores hour, minute, second and microsecond.
        MYSQL_TIMESTAMP_TIME = 2,

        /**
            A temporary type for `DATETIME` or `TIMESTAMP` types equipped with time
            zone information. After the time zone information is reconciled, the type is
            converted to MYSQL_TIMESTAMP_DATETIME.
        */
        MYSQL_TIMESTAMP_DATETIME_TZ = 3
    }
}
