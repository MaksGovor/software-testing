using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.DatabaseConnectionUtils;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using System;

namespace MaksGovor.DatabaseInteraction.Test
{
    [TestClass]
    public class TetsDatabaseInteraction
    {
        private const string Server = @"DESKTOP-NILDRFH";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"26052002";
        private const int ConnectionTimeout = 75;

        [TestMethod]
        public void TestMethod1()
        {
            try
            {
                AuthDatabaseUtils authDatabase = new AuthDatabaseUtils(
                    Server, Database, IsTrusted, Login, Password, ConnectionTimeout
                    );
                authDatabase.AddCredentials("eee2y", "ieeerrrrieeerrrrieeerrrrieeerrrrieeerrrrieeerrrrieeerrrrieeerrrrieeerrrr");
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }

        }
    }
}
