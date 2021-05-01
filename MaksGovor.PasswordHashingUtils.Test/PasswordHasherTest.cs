using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;

namespace MaksGovor.PasswordHashingUtils.Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        private static string _password;
        private static string _defautlSalt;

        [TestInitialize]
        public void Initialization()
        {
            _password = "camry3.5";
            _defautlSalt = "mysalatherewegoagain";
        }

        [DataTestMethod]
        [DataRow("", (uint)0, "salt is empty string and adlerMod32 = 0")]
        [DataRow(null, (uint)0, "salt = null and adlerMod32 = 0")]
        public void Test_Init_0_1_4_5(string salt, uint adlerMod32, string fact)
        {
            try
            {
                PasswordHasher.Init(salt, adlerMod32);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected result on the way 0_1_4_5 when" + fact);
            }
        }

        [DataTestMethod]
        [DataRow("", (uint)1, "salt is empty string and adlerMod32 = 1")]
        [DataRow(null, uint.MaxValue, "salt = null and adlerMod32 = maxValue (4,294,967,295)")]
        public void Test_Init_0_1_3_5(string salt, uint adlerMod32, string fact)
        {
            try
            {
                PasswordHasher.Init(salt, adlerMod32);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected result on the way 0_1_3_5 when" + fact);
            }
        }

        [DataTestMethod]
        [DataRow("notdefaultsalt", (uint)0, "salt is valid string and adlerMod32 = 0")]
        [DataRow("notdefaultsalt", uint.MinValue, "salt is valid string and adlerMod32 = minValue (0)")]
        public void Test_Init_0_2_4_5(string salt, uint adlerMod32, string fact)
        {
            try
            {
                PasswordHasher.Init(salt, adlerMod32);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected result on the way 0_1_3_5 when" + fact);
            }
        }

        [DataTestMethod]
        [DataRow("notdefaultsalt", (uint)1, "salt is valid string and adlerMod32 = 1")]
        [DataRow("notdefaultsalt", uint.MaxValue, "salt is valid string and adlerMod32 = maxValue (4,294,967,295)")]
        public void Test_Init_0_2_3_5(string salt, uint adlerMod32, string fact)
        {
            try
            {
                PasswordHasher.Init(salt, adlerMod32);
            }
            catch (Exception)
            {
                Assert.Fail("Unexpected result on the way 0_1_3_5 when" + fact);
            }
        }
    }
}
