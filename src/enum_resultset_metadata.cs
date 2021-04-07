// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

namespace MySqlSharp
{
    public enum enum_resultset_metadata
    {
        /** No metadata will be sent. */
        RESULTSET_METADATA_NONE = 0,
        /** The server will send all metadata. */
        RESULTSET_METADATA_FULL = 1
    }
}
