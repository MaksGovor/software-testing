using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;
using System.Reflection;
using System.Text;

namespace MaksGovor.PasswordHashingUtils.Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        private PasswordHasher _passwordHasher;
        private string _defautlSalt;
        private uint _defautlModAdler32;
        private Type _typePH;
        private FieldInfo _saltInfo;
        private FieldInfo _modAdler32Info;

        [TestInitialize]
        public void Initialization()
        {
            _passwordHasher = new PasswordHasher();
            _defautlSalt = "put your soul(or salt) here";
            _defautlModAdler32 = 65521;
            _typePH = typeof(PasswordHasher);
            _saltInfo = _typePH.GetField("_salt", BindingFlags.Static | BindingFlags.NonPublic);
            _modAdler32Info = _typePH.GetField("_modAdler32", BindingFlags.Static | BindingFlags.NonPublic);
        }

        #region Test Init

        [DataTestMethod]
        [DataRow("", (uint)0, "salt is empty string and adlerMod32 = 0")]
        [DataRow(null, (uint)0, "salt = null and adlerMod32 = 0")]
        public void Test_Init_Route_0_1_5_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method 
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();

                Assert.AreEqual(_defautlSalt, saltValue, "Unexpected result of _salt on the way 0_1_5_7 when" + fact);
                Assert.AreEqual(_defautlModAdler32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_1_5_7 when" + fact);
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("", (uint)1, "salt is empty string and adlerMod32 = 1")]
        [DataRow(null, uint.MaxValue, "salt = null and adlerMod32 = maxValue (4,294,967,295)")]
        public void Test_Init_Route_0_1_6_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method afterwards
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();

                Assert.AreEqual(_defautlSalt, saltValue, "Unexpected result of _salt on the way 0_1_6_7 when" + fact);
                Assert.AreEqual(adlerMod32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_1_6_7 when" + fact);
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("notdefaultsalt", (uint)0, "salt is valid string and adlerMod32 = 0")]
        [DataRow("notdefaultsalt", uint.MinValue, "salt is valid string and adlerMod32 = minValue (0)")]
        public void Test_Init_Route_0_2_4_5_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method afterwards
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();

                Assert.AreEqual(salt, saltValue, "Unexpected result of _salt on the way 0_2_4_5_7 when" + fact);
                Assert.AreEqual(_defautlModAdler32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_2_4_5_7 when" + fact);
            }
            catch (Exception err) 
            { 
                Assert.Fail(err.Message); 
            }
        }

        [DataTestMethod]
        [DataRow("notdefaultsalt", (uint)1, "salt is valid string and adlerMod32 = 1")]
        [DataRow("notdefaultsalt", uint.MaxValue, "salt is valid string and adlerMod32 = maxValue (4,294,967,295)")]
        public void Test_Init_Route_0_2_4_6_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method afterwards
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();

                Assert.AreEqual(salt, saltValue, "Unexpected result of _salt on the way 0_2_4_6_7 when" + fact);
                Assert.AreEqual(adlerMod32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_2_4_6_7 when" + fact);
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("இந்தியா", (uint)0, "salt is non ascii character string and adlerMod32 = 0")]
        [DataRow("网络", (uint)0, "salt is valid string and adlerMod32 = 0")]
        public void Test_Init_Route_0_2_3_4_5_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method afterwards
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();
                string expected = Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt));

                Assert.AreEqual(expected, saltValue, "Unexpected result of _salt on the way 0_2_3_4_5_7 when" + fact);
                Assert.AreEqual(_defautlModAdler32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_2_3_4_5_7 when" + fact);

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("இந்தியா", (uint)1, "salt is non ascii character string and adlerMod32 = 1")]
        [DataRow("网络", uint.MaxValue, "salt is valid string and adlerMod32 = maxValue (4,294,967,295)")]
        public void Test_Init_Route_0_2_3_4_6_7(string salt, uint adlerMod32, string fact)
        {
            try
            {
                // Set the default values to test the method afterwards
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                PasswordHasher.Init(salt, adlerMod32);

                string saltValue = _saltInfo.GetValue(_passwordHasher).ToString();
                string adlerMod32Value = _modAdler32Info.GetValue(_passwordHasher).ToString();
                string expected = Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(salt));

                Assert.AreEqual(expected, saltValue, "Unexpected result of _salt on the way 0_2_3_4_6_7 when" + fact);
                Assert.AreEqual(adlerMod32.ToString(), adlerMod32Value, "Unexpected result of _modAdler32 on the way 0_2_3_4_6_7 when" + fact);

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion Test Init

        #region Test GetHash

        [TestMethod]
        public void Test_GetHash_Route_0_1_2_6_NullPassword()
        {
            try
            {
                // Set the default values to test the method 
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                const string password = null;
                const string salt = "notdefaultsalt";
                const uint adlerMod32 = 2701;

                string hash1 = PasswordHasher.GetHash(password);
                string hash2 = PasswordHasher.GetHash(password, salt, adlerMod32);

                Assert.IsNull(hash1, "Password hash must be null if the password is null when salt and adlerMod32 is default");
                Assert.IsNull(hash2, "Password hash must be null if the password is null when salt is valid string" +
                    " and adlerMod32 = 0 is default");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_GetHash_Route_0_1_3_5_6_NotNullPassword()
        {
            try
            {
                // Set the default values to test the method 
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);

                const string salt = "notdefaultsalt";
                const uint adlerMod32 = 2701;
                const string password = "camry3.5";

                string hash1 = PasswordHasher.GetHash(password);
                string hash2 = PasswordHasher.GetHash(password, salt, adlerMod32);

                Assert.IsNotNull(hash1, "Password hash must be not null if the password is not null when salt and adlerMod32 is default");
                Assert.IsNotNull(hash2, "Password hash must be not null if the password is not null when salt is valid string" +
                    " and adlerMod32 = 0 is default");
                Assert.AreNotEqual(hash1, hash2, "Hash identical passwords cannot be equivalent when salt and adlerMod are different");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_GetHash_Route_0_1_3_4_5_6_NonAsciiPassword()
        {
            try
            {
                // Set the default values to test the method 
                _saltInfo.SetValue(_passwordHasher, _defautlSalt);
                _modAdler32Info.SetValue(_passwordHasher, _defautlModAdler32);
                MethodInfo Adler32CheckSum = _typePH.GetMethod("Adler32CheckSum", BindingFlags.NonPublic | BindingFlags.Static);
                MethodInfo HashSha2 = _typePH.GetMethod("HashSha2", BindingFlags.NonPublic | BindingFlags.Static);

                const string salt = "notdefaultsalt";
                const uint adlerMod32 = 2701;
                const string password = "இந்தியா";

                string ASCIIpassword = Encoding.ASCII.GetString(Encoding.Unicode.GetBytes(password));
                string checkSum = Adler32CheckSum.Invoke(_passwordHasher, new object[] { ASCIIpassword, 0, 0 }).ToString();
                string hashManual = HashSha2.Invoke(_passwordHasher, new object[] { $"{_defautlSalt}{checkSum}{ASCIIpassword}" }).ToString();

                string hash1 = PasswordHasher.GetHash(password);
                string hash2 = PasswordHasher.GetHash(password, salt, adlerMod32);

                Assert.AreEqual(hash1, hashManual, "The manually made password and the password obtained from the method " +
                    "of non ASCII character transfer do not match");

                Assert.IsNotNull(hash1, "Password hash must be not null if the password is not null when salt and adlerMod32 is default");
                Assert.IsNotNull(hash2, "Password hash must be not null if the password is not null when salt is valid string" +
                    " and adlerMod32 = 0 is default");
                Assert.AreNotEqual(hash1, hash2, "Hash identical passwords cannot be equivalent when salt and adlerMod are different");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        # endregion Test GetHash
    }
}
