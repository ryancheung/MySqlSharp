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

namespace MySqlSharp.Tests
{
    [TestClass]
    public unsafe class MySqlClientTests
    {
        static IntPtr PrepareMySqlConnection()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = "fel";
            string password = "fel";
            string database = "fel_auth";
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

            Span<byte> span = stackalloc byte[Encoding.UTF8.GetByteCount(charset) + 1];
            Encoding.UTF8.GetBytes(charset, span);

            fixed (byte* inputPtr = span)
            {
                var ret = mysql_options(mysqlInit, MYSQL_SET_CHARSET_NAME, inputPtr);
                Assert.AreEqual(0, ret);

                void* result = default;
                ret = mysql_get_option(mysqlInit, MYSQL_SET_CHARSET_NAME, ref result);
                Assert.AreEqual(0, ret);

                Assert.AreEqual(charset, Encoding.UTF8.GetString((byte*)result, Encoding.UTF8.GetByteCount(charset)));
            }

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_int()
        {
            var mysqlInit = mysql_init();
            int input = 9999;
            var ret = mysql_options(mysqlInit, MYSQL_OPT_CONNECT_TIMEOUT, &input);
            Assert.AreEqual(0, ret);

            int result = default;
            ret = mysql_get_option(mysqlInit, MYSQL_OPT_CONNECT_TIMEOUT, &result);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(input, result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_long()
        {
            var mysqlInit = mysql_init();
            ulong input = 9999;
            var ret = mysql_options(mysqlInit, MYSQL_OPT_MAX_ALLOWED_PACKET, &input);
            Assert.AreEqual(0, ret);

            ulong result = default;
            ret = mysql_get_option(mysqlInit, MYSQL_OPT_MAX_ALLOWED_PACKET, &result);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(input, result);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_options_set_get_bool()
        {
            var mysqlInit = mysql_init();
            byte bVal = 1;
            var ret = mysql_options(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, &bVal);
            Assert.AreEqual(0, ret);

            byte bResult = default;
            ret = mysql_get_option(mysqlInit, MYSQL_ENABLE_CLEARTEXT_PLUGIN, &bResult);
            Assert.AreEqual(0, ret);
            Assert.AreEqual(bVal, bResult);

            mysql_close(mysqlInit);
        }

        [TestMethod]
        public void Test_mysql_real_connect()
        {
            var mysqlInit = mysql_init();

            string host = "127.0.0.1";
            string user = "fel";
            string password = "fel";
            string database = "fel_auth";
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
            string user = "fel";
            string password = "foo";
            string database = "fel_auth";
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
    }
}
