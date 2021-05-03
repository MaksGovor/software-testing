using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;
using System.Reflection;

namespace MaksGovor.PasswordHashingUtils.Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        private string _password;
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
            _password = "camry3.5";
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

        [TestMethod]
        public void Test_Init()
        {
            try
            {

                Assert.ThrowsException<OverflowException>(() => PasswordHasher.Init("", 0));

            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion Test Init
    }
}
