using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data;
using System.Text;
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
        public void TestGetHash_Push_to_DB_ValidFields(string login, string password, string salt = null, uint? adlerMod = null)
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

        [DataTestMethod]
        [DataRow("", "абвгд")]
        [DataRow(null, "passwd")]
        [DataRow("goodusername", null)]
        public void TestGetHash_Push_to_DB_InvalidFields(string login, string password)
        {
            try
            {
                string hash = PasswordHasher.GetHash(password);
                Assert.IsFalse(authDatabase.AddCredentials(login, hash),
                    $"The value should not be added with login: {login}");

                Assert.IsFalse(authDatabase.CheckCredentials(login, hash),
                    $"It is not possible to find the entry where the login: {login}");
            }
            catch (Exception err)
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
        public void Test_Push_Equal_Data()
        {
            try
            {
                const string login = "dadadadada";
                const string password = "minus3";
                string hash = PasswordHasher.GetHash(password);

                Assert.IsTrue(authDatabase.AddCredentials(login, hash), "The value is not added to the database");
                Assert.IsFalse(authDatabase.AddCredentials(login, hash), "It is not possible to add an existing user");
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
                const string password = "postgresPASSWORD";
                const string newlogin = "mssql";
                const string newpassword = "mssqlPASSWORD";
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

    [TestClass]
    public class TetsDatabaseInteractionStorage
    {
        private const string Server = @"DESKTOP-NILDRFH";
        private const string Database = @"IIG.CoSWE.StorageDB";
        private const bool IsTrusted = false;
        private const string Login = @"sa";
        private const string Password = @"26052002";
        private const int ConnectionTimeout = 75;
        private StorageDatabaseUtils storageDatabase;
        private const string filenameTxt = "filetest.txt";
        private const string filenameEmpty = "filetest_empty.txt";
        private const string filenameNull = "filetest_null.txt";
        private const string filenameJson = "filetest.json";
        private const string filenameXml = "filetest.xml";

        [TestInitialize]
        public void Initialization()
        {
            storageDatabase = new StorageDatabaseUtils(
                Server, Database, IsTrusted, Login, Password, ConnectionTimeout
            );
            try
            {
                const string txt = "some text\n\rsome text";
                const string json = "{ \"field\": \"value\" }";
                const string xml = "<xsl:strip-space elements=\" * \"/>";
                BaseFileWorker.Write(txt, filenameTxt);
                BaseFileWorker.Write("", filenameEmpty);
                BaseFileWorker.Write(null, filenameNull);
                BaseFileWorker.Write(json, filenameJson);
                BaseFileWorker.Write(xml, filenameXml);
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow(filenameTxt)]
        [DataRow(filenameJson)]
        [DataRow(filenameXml)]
        [DataRow(filenameEmpty)]
        [DataRow(filenameNull)]
        public void Test_ReadAll_Push_in_DB(string filename)
        {
            try
            {
                string textFromFile = BaseFileWorker.ReadAll(filename);
                byte[] fileContent = Encoding.ASCII.GetBytes(textFromFile);
                string filenameTxtFromDB;
                byte[] fileContentFromDB;

                Assert.IsTrue(storageDatabase.AddFile(filename, fileContent), 
                    $"The file {filename} was not added successfully");
                int? fileID = storageDatabase.GetIntBySql("SELECT MAX(FileID) FROM Files");
                Assert.IsTrue(storageDatabase.GetFile((int)fileID, out filenameTxtFromDB, out fileContentFromDB), 
                    $"File {filename} not found in db, although add method returned true");
                string textFromDB = Encoding.ASCII.GetString(fileContentFromDB);
                Assert.AreEqual(filename, filenameTxtFromDB,
                    "The names of the files returned by the method and the database do not match");
                Assert.AreEqual(textFromFile, textFromDB,
                    "The content of the files returned by the method and the database do not match");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow(filenameTxt)]
        [DataRow(filenameJson)]
        [DataRow(filenameXml)]
        [DataRow(filenameEmpty)]
        [DataRow(filenameNull)]
        public void Test_ReadLines_Push_in_DB(string filename)
        {
            try
            {
                string textFromFile = string.Join("", BaseFileWorker.ReadLines(filename));
                byte[] fileContent = Encoding.ASCII.GetBytes(textFromFile);
                string filenameTxtFromDB;
                byte[] fileContentFromDB;

                Assert.IsTrue(storageDatabase.AddFile(filename, fileContent),
                    $"The file {filename} was not added successfully");
                int? fileID = storageDatabase.GetIntBySql("SELECT MAX(FileID) FROM Files");
                Assert.IsTrue(storageDatabase.GetFile((int)fileID, out filenameTxtFromDB, out fileContentFromDB),
                    $"File {filename} not found in db, although add method returned true");
                string textFromDB = Encoding.ASCII.GetString(fileContentFromDB);
                Assert.AreEqual(filename, filenameTxtFromDB,
                    "The names of the files returned by the method and the database do not match");
                Assert.AreEqual(textFromFile, textFromDB,
                    "The content of the files returned by the method and the database do not match");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow(filenameTxt)]
        [DataRow(filenameEmpty)]
        [DataRow(filenameNull)]
        public void Test_ReadAll_and_Deleting_from_DB(string filename)
        {
            try
            {
                string textFromFile = BaseFileWorker.ReadAll(filename);
                byte[] fileContent = Encoding.ASCII.GetBytes(textFromFile);
                string filenameTxtFromDB = null;
                byte[] fileContentFromDB = null;

                Assert.IsTrue(storageDatabase.AddFile(filename, fileContent),
                    $"The file {filename} was not added successfully");
                int? fileID = storageDatabase.GetIntBySql("SELECT MAX(FileID) FROM Files");
                Assert.IsTrue(storageDatabase.DeleteFile((int)fileID),
                    $"Deleting a file by ID = {fileID} is not successful");

                Assert.IsFalse(storageDatabase.GetFile((int)fileID, out filenameTxtFromDB, out fileContentFromDB),
                    $"Even though the file by ID = {fileID} was deleted from the database, the method tells us that it is there.");
                Assert.IsNull(filenameTxtFromDB, 
                    $"Even though the file by ID = {fileID} was deleted from the database, the method returns some value of its name");
                Assert.IsNull(fileContentFromDB,
                    $"Although the file by ID = {fileID} is deleted from the database, the method returns some value of its content");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_ReadAll_GetFiles_from_DB_NotNull()
        {
            try
            {
                string textFromFile = BaseFileWorker.ReadAll(filenameTxt);
                byte[] fileContent = Encoding.ASCII.GetBytes(textFromFile);
                const int countFiles = 3;

                for (int i = 0; i < countFiles; i++) {
                    Assert.IsTrue(storageDatabase.AddFile(filenameTxt, fileContent),
                        $"The file filenameTxt was not added successfully for the {i + 1} time");
                }

                DataTable files = storageDatabase.GetFiles(filenameTxt);
                Assert.IsNotNull(files,
                    "GetFiles returns zero values even when there are files with the same name in the database");
                Assert.AreEqual(files.Rows.Count, countFiles,
                    "The number of records for a certain file name is not returned correctly");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            storageDatabase.ExecSql("DELETE FROM Files");
        }
    }
}
