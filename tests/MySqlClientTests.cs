// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MySqlSharp.NativeMethods;
using static MySqlSharp.mysql_option;
using static MySqlSharp.ErrorClient;
using static MySqlSharp.ErrorServer;
using static MySqlSharp.enum_field_types;
using static MySqlSharp.enum_stmt_attr_type;

namespace MySqlSharp.Tests
{
    [TestClass]
    public unsafe class MySqlClientTests
    {
        const string TestUser = "root";
        const string TestPassword = "mysqlroot";
        const string TestDB = "mysql";

        static IntPtr PrepareMySqlConnection()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = TestUser;
            string password = TestPassword;
            string database = TestDB;
            uint port = 3306;

            return mysql_real_connect(mysqlInit, host, user, password, database, port, null);
        }

        [TestMethod]
        public void Test_mysql_get_client_version()
        {
            var version = mysql_get_client_version();
            var mysqlInit = mysql_init();
            mysql_close(mysqlInit);
            Assert.IsTrue(version > 0);
        }

        [TestMethod]
        public void Test_mysql_init_and_close()
        {
            var mysqlInit = mysql_init();
            Assert.IsTrue(mysqlInit != IntPtr.Zero);
            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_string()
        {
            var mysqlInit = mysql_init();
            string charset = "utf8";

            var ret = mysql_options(mysqlInit, MYSQL_SET_CHARSET_NAME, charset);
            Assert.AreEqual(0, ret);

            ret = mysql_get_option(mysqlInit, MYSQL_SET_CHARSET_NAME, out string result);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(charset, result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_int()
        {
            var mysqlInit = mysql_init();
            int input = 9999;
            var ret = mysql_options(mysqlInit, MYSQL_OPT_CONNECT_TIMEOUT, input);
            Assert.AreEqual(0, ret);

            ret = mysql_get_option(mysqlInit, MYSQL_OPT_CONNECT_TIMEOUT, out int result);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(input, result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_long()
        {
            var mysqlInit = mysql_init();
            long input = 9999;
            var ret = mysql_options(mysqlInit, MYSQL_OPT_MAX_ALLOWED_PACKET, input);
            Assert.AreEqual(0, ret);

            ret = mysql_get_option(mysqlInit, MYSQL_OPT_MAX_ALLOWED_PACKET, out long result);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(input, result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_bool()
        {
            var mysqlInit = mysql_init();

            var ret = mysql_options(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, true);
            Assert.AreEqual(0, ret);

            ret = mysql_get_option(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, out bool result);
            Assert.AreEqual(0, ret);
            Assert.IsTrue(result);

            ret = mysql_options(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, false);
            Assert.AreEqual(0, ret);
            ret = mysql_get_option(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, out result);
            Assert.IsFalse(result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_real_connect()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = TestUser;
            string password = TestPassword;
            string database = TestDB;
            uint port = 3306;

            mysqlInit = mysql_real_connect(mysqlInit, host, user, password, database, port, null);
            var errorCode = mysql_errno(mysqlInit);
            Assert.AreEqual(0, errorCode);
            Assert.AreNotEqual(IntPtr.Zero, mysqlInit);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_real_connect_with_wrong_password()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = TestUser;
            string password = "wrongpassword";
            string database = TestDB;
            uint port = 3306;

            mysql_real_connect(mysqlInit, host, user, password, database, port, null);
            var errorCode = mysql_errno(mysqlInit);
            Assert.AreNotEqual(ER_ACCESS_DENIED_ERROR, errorCode);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_autocommit()
        {
            var mysqlInit = PrepareMySqlConnection();

            var ret = mysql_autocommit(mysqlInit, true);
            Assert.IsFalse(ret);

            ret = mysql_autocommit(mysqlInit, false);
            Assert.IsFalse(ret);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_get_client_info()
        {
            var mysqlInit = PrepareMySqlConnection();

            var clientInfo = mysql_get_client_info();
            Assert.IsNotNull(clientInfo);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_get_server_info()
        {
            var mysqlInit = PrepareMySqlConnection();

            var serverInfo = mysql_get_server_info(mysqlInit);
            Assert.IsNotNull(serverInfo);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_init()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            Assert.AreNotEqual(IntPtr.Zero, (IntPtr)stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_close()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var ret = mysql_stmt_close(stmt);
            Assert.IsFalse(ret);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_prepare()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var sql = "SELECT 1";
            var ret = mysql_stmt_prepare(stmt, sql);
            Assert.AreEqual(0, ret);

            mysql_stmt_close(stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_param_count()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var sql = "select help_keyword_id, name from help_keyword where name = ?;";
            mysql_stmt_prepare(stmt, sql);

            Assert.AreEqual(1, mysql_stmt_param_count(stmt));

            mysql_stmt_close(stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_attr_get_set()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var sql = "select help_keyword_id, name from help_keyword where name = ?;";
            mysql_stmt_prepare(stmt, sql);

            mysql_stmt_attr_get(stmt, STMT_ATTR_UPDATE_MAX_LENGTH, out bool attr);
            Assert.IsFalse(attr);

            mysql_stmt_attr_set(stmt, STMT_ATTR_UPDATE_MAX_LENGTH, true);

            mysql_stmt_attr_get(stmt, STMT_ATTR_UPDATE_MAX_LENGTH, out attr);
            Assert.IsTrue(attr);

            mysql_stmt_close(stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_execute()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var sql = "select help_keyword_id, name from help_keyword where name = '+';";
            mysql_stmt_prepare(stmt, sql);

            var ret = mysql_stmt_execute(stmt);
            Assert.AreEqual(0, ret);

            var res = mysql_stmt_result_metadata(stmt);

            var fieldCount = mysql_stmt_field_count(stmt);
            Assert.AreEqual(2, fieldCount);

            if (mysql_more_results(mysqlInit))
                mysql_next_result(mysqlInit);

            mysql_stmt_store_result(stmt);

            var rowCount = mysql_stmt_num_rows(stmt);
            Assert.AreEqual(1, rowCount);

            mysql_stmt_close(stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_query()
        {
            var mysqlInit = PrepareMySqlConnection();

            var sql = "select help_keyword_id, name from help_keyword where name = '+';";
            var ret = mysql_query(mysqlInit, sql);

            Assert.AreEqual(0, ret);

            var res = mysql_store_result(mysqlInit);
            var affectedRows = mysql_affected_rows(mysqlInit);
            var fieldCount = mysql_field_count(mysqlInit);
            Assert.AreEqual(1, affectedRows);
            Assert.AreEqual(2, fieldCount);
        }

        [TestMethod]
        public void Test_mysql_ping()
        {
            var mysqlInit = PrepareMySqlConnection();

            Assert.AreEqual(0, mysql_ping(mysqlInit));

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_real_escape_string()
        {
            var mysqlInit = PrepareMySqlConnection();

            var str = "binary data: \0\r\n";

            var ret = mysql_real_escape_string(mysqlInit, ref str);
            Assert.AreEqual("binary data: \\0\\r\\n", str);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_set_character_set_set_invalid_charset()
        {
            var mysqlInit = PrepareMySqlConnection();

            var charset = "utf8foo";

            var ret = mysql_set_character_set(mysqlInit, charset);
            Assert.AreEqual((int)CR_CANT_READ_CHARSET, ret);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_set_character_set()
        {
            var mysqlInit = PrepareMySqlConnection();

            var charset = "utf8";

            var ret = mysql_set_character_set(mysqlInit, charset);
            Assert.AreEqual(0, ret);
            mysql_get_option(mysqlInit, MYSQL_SET_CHARSET_NAME, out string result);
            Assert.IsTrue(result.StartsWith(charset));

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_stmt_bind_param()
        {
            var mysqlInit = PrepareMySqlConnection();

            var stmt = mysql_stmt_init(mysqlInit);
            var sql = "select help_keyword_id, name from help_keyword where name = ?";
            var retPrep = mysql_stmt_prepare(stmt, sql);
            Assert.AreEqual(0, retPrep);

            var param1 = "+";
            MYSQL_BIND param = new();

            var len = (UIntPtr)Encoding.UTF8.GetByteCount(param1);
            param.buffer_type = MYSQL_TYPE_VAR_STRING;

            void* mem = Marshal.AllocHGlobal((int)len).ToPointer();
            Marshal.FreeHGlobal((IntPtr)param.buffer);
            param.buffer = mem;
            Encoding.UTF8.GetBytes(param1, new Span<byte>(param.buffer, (int)len));
            param.buffer_length = len;

            param.is_null_value = false;

            mem = Marshal.AllocHGlobal(sizeof(UIntPtr)).ToPointer();
            Marshal.FreeHGlobal((IntPtr)param.length);
            MemoryMarshal.Write(new Span<byte>(mem, sizeof(UIntPtr)), ref len);
            param.length = (UIntPtr*)mem;

            var retBind = mysql_stmt_bind_param(stmt, &param);
            Assert.IsFalse(retBind);

            var ret = mysql_stmt_execute(stmt);
            Assert.AreEqual(0, ret);

            // Clear bind param
            Marshal.FreeHGlobal((IntPtr)param.buffer);
            Marshal.FreeHGlobal((IntPtr)param.length);

            var res = mysql_stmt_result_metadata(stmt);

            var fieldCount = mysql_stmt_field_count(stmt);
            Assert.AreEqual(2, fieldCount);

            if (mysql_more_results(mysqlInit))
                mysql_next_result(mysqlInit);

            mysql_stmt_store_result(stmt);

            var rowCount = mysql_stmt_num_rows(stmt);
            Assert.AreEqual(1, rowCount);

            mysql_stmt_close(stmt);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_fetch_row()
        {
            var mysqlInit = PrepareMySqlConnection();

            var sql = "select help_keyword_id, name from help_keyword where name = '+';";
            var ret = mysql_query(mysqlInit, sql);

            Assert.AreEqual(0, ret);

            var res = mysql_store_result(mysqlInit);
            var affectedRows = mysql_affected_rows(mysqlInit);
            var fieldCount = mysql_field_count(mysqlInit);
            Assert.AreEqual(2, fieldCount);

            var row = mysql_fetch_row(res);
            UIntPtr* lengths = mysql_fetch_lengths(res);

            for (int i = 0; i < fieldCount; i++)
            {
                IntPtr fieldValuePtr = row[i];
                var fieldValueLen = (int)lengths[i];

                var fieldValueStr = Marshal.PtrToStringAnsi(fieldValuePtr);

                if (i == 0)
                    Assert.AreEqual("84", fieldValueStr);
                if (i == 1)
                    Assert.AreEqual("+", fieldValueStr);
            }

            // next row is empty
            row = mysql_fetch_row(res);
            Assert.AreEqual((IntPtr)0, (IntPtr)row);
        }

        [TestMethod]
        public void Test_mysql_error()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = TestUser;
            string password = "wrongpassword";
            string database = TestDB;
            uint port = 3306;

            mysql_real_connect(mysqlInit, host, user, password, database, port, null);
            var error = mysql_error(mysqlInit);
            Assert.IsTrue(error.Contains("Access denied"));

            mysql_close(mysqlInit);
        }
    }
}
