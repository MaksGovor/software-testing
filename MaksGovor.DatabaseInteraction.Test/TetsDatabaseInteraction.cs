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
        [DataRow("u👽r1", "абвгд", "网络")]
        [DataRow("u络r2", ",*₴!:`'@/*+_?}{][|", "")]
        [DataRow(",`*!3", "👽👽", "\\\r")]
        [DataRow("юзер4", "genabukintop", "👽", (uint)0)]
        [DataRow("01005", "网络", "salt64last", (uint)1)]
        [DataRow("\r\\6", "qwerty1", ",*₴!:`'@/*+_?}{][|", uint.MaxValue)]
        [DataRow("u e 7", "", "a b c", (uint)4)]
        [DataRow("user8", "\r\n\\\"")]
        public void TestGetHash_Push_to_DB(string login, string password, string salt = null, uint? adlerMod = null)
        {
            try
            {
                string hash = PasswordHasher.GetHash(password, salt, adlerMod);
                Assert.IsTrue(authDatabase.AddCredentials(login, hash),
                    "The system does not add a non-existent user with next input data " +
                    $"login: {login}, password: ${password}, salt: {salt}, adlerMod: ${adlerMod}");

                Assert.IsTrue(authDatabase.CheckCredentials(login, hash),
                    "There must be a user on the system with " +
                    $"login: {login}, password: ${password}");
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void TestGetHash_Deleting_From_DB()
        {
            try
            {
                const string login = "McQueen";
                const string password = "passwd";
                string hash = PasswordHasher.GetHash(password);
                
                Assert.IsTrue(authDatabase.AddCredentials(login, hash),
                    "The system does not add a non-existent user with next input data " +
                    $"login: {login}, password: ${password}");

                Assert.IsTrue(authDatabase.DeleteCredentials(login, hash),
                    "Deleting an existing entry is not successful");

                Assert.IsFalse(authDatabase.CheckCredentials(login, hash),
                    "After deletion the record is still in the database");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void TestGetHash_Update_in_DB()
        {
            try
            {
                const string login = "postgres";
                const string password = "postgres";
                const string newlogin = "mssql";
                const string newpassword = "mssql";
                string hash = PasswordHasher.GetHash(password);
                string newhash = PasswordHasher.GetHash(newpassword);
                
                Assert.IsTrue(authDatabase.AddCredentials(login, hash),
                    "The system does not add a non-existent user with next input data " +
                    $"login: {login}, password: ${password}");

                Assert.IsTrue(authDatabase.UpdateCredentials(login, hash, newlogin, newhash),
                    "Updating an existing entry is not successful");
                Assert.IsTrue(authDatabase.CheckCredentials(newlogin, newhash),
                    "After updating, the new record did not appear in the database");
                Assert.IsFalse(authDatabase.CheckCredentials(login, hash),
                    "After updating the old record is still in the database");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            authDatabase.ExecSql("DELETE FROM Credentials");
        }
    }
}
