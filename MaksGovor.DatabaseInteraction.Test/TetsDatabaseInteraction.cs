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
            //authDatabase.ExecSql("DELETE FROM Credentials");
        }

        [DataTestMethod]
        [DataRow("user1👽", "абвгд", "网络")]
        [DataRow("user2络", ",*₴!::", "")]
        [DataRow("user3,*₴!::", "👽👽", "\\ \r")]
        [DataRow("user4", "genabukintop", "👽", (uint)0)]
        [DataRow("user5", "网络", "salt64last", (uint)1)]
        [DataRow("user6", "qwerty1", "*******!!**!!@", uint.MaxValue)]
        [DataRow("user7", "", "0102010102021012122013021")]
        [DataRow("user8", "\r \n \\ \" ", "")]
        public void TestMethod1(string login, string password, string salt = null, uint? adlerMod = null)
        {
            try
            {
                string hash = PasswordHasher.GetHash(password, salt, adlerMod);
                Assert.IsTrue(authDatabase.AddCredentials(login, hash),
                    "The system does not add a non-existent user with next input data " +
                    $"login: {login}, password: ${password}, salt: {salt}, adlerMod: ${adlerMod}"
                    );

                Assert.IsTrue(authDatabase.CheckCredentials(login, hash),
                    "There must be a user on the system with " +
                    $"login: {login}, password: ${password}");
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }


    }
}
