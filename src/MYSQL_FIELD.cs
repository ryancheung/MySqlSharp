// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System.Runtime.InteropServices;

namespace MySqlSharp
{
    [StructLayout(LayoutKind.Sequential)]
    public unsafe struct MYSQL_FIELD
    {
        public byte* name;               /* Name of column */
        public byte* org_name;           /* Original column name, if an alias */
        public byte* table;              /* Table of column if column was a field */
        public byte* org_table;          /* Org table name, if table was an alias */
        public byte* db;                 /* Database for table */
        public byte* catalog;            /* Catalog for table */
        public byte* def;                /* Default value (set by mysql_list_fields) */
        public ulong length;             /* Width of column (create length) */
        public ulong max_length;         /* Max width for selected set */
        public uint name_length;
        public uint org_name_length;
        public uint table_length;
        public uint org_table_length;
        public uint db_length;
        public uint catalog_length;
        public uint def_length;
        public uint flags;         /* Div flags */
        public uint decimals;      /* Number of decimals in field */
        public uint charsetnr;     /* Character set */
        public enum_field_types type; /* Type of field. See mysql_com.h for types */
        public void* extension;
    }
}
