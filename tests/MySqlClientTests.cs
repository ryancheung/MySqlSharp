// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using static MySqlSharp.NativeMethods;
using static MySqlSharp.mysql_option;

namespace MySqlSharp.Tests
{
    [TestClass]
    public unsafe class MySqlClientTests
    {
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
    }
}
