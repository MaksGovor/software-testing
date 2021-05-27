using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.DatabaseConnectionUtils;
using IIG.CoSFE.DatabaseUtils;
using IIG.PasswordHashingUtils;
using IIG.FileWorker;
using System;

namespace MaksGovor.DatabaseInteraction.Test
{
    [TestClass]
    public class TetsDatabaseInteractionAuth
    {
        private const string Server = @"DESKTOP-NILDRFH";
        private const string Database = @"IIG.CoSWE.AuthDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"26052002";
        private const int ConnectionTimeout = 75;
        private AuthDatabaseUtils authDatabase;

        [TestInitialize]
        public void Initialization()
        {
            authDatabase = new AuthDatabaseUtils(
                Server, Database, IsTrusted, Login, Password, ConnectionTimeout
            );
        }

        [DataTestMethod]
        [DataRow("user1", "genabukintop", "salt64last", (uint)1)]
        [DataRow("user2", "qwerty1", "", uint.MaxValue)]
        [DataRow("user3", "абвгд", "网络")]
        [DataRow("user4", ",*₴!::", "salt64last")]
        public void TestMethod1(string login, string password, string salt = null, uint? adlerMod = null)
        {
            try
            {
                string hash = PasswordHasher.GetHash(password, salt, adlerMod);
                Assert.IsTrue(authDatabase.AddCredentials(login, hash));
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }
    }
}
