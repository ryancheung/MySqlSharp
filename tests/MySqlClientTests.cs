// MIT License - Copyright (C) ryancheung
// This file is subject to the terms and conditions defined in
// file 'LICENSE', which is part of this source code package.

using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MySqlSharp.Tests
{
    [TestClass]
    public class MySqlClientTests
    {
        [TestMethod]
        public void Test_mysql_get_client_version()
        {
            var version = NativeMethods.mysql_get_client_version();
            var mysql = NativeMethods.mysql_init();
            NativeMethods.mysql_close(mysql);
            Assert.IsTrue(version > 0);
        }

        [TestMethod]
        public void Test_mysql_init_and_close()
        {
            var mysql = NativeMethods.mysql_init();
            Assert.IsTrue(mysql != IntPtr.Zero);
            NativeMethods.mysql_close(mysql);
        }
    }
}
