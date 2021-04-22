using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;

namespace MaksGovor.PasswordHashingUtils.Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            try
            {
                PasswordHasher passwordHasher1 = new PasswordHasher();
                PasswordHasher passwordHasher2 = new PasswordHasher();

                Assert.IsNotNull(passwordHasher1, "Instance of PasswordHasher is null object!");
                Assert.AreNotEqual(passwordHasher1, passwordHasher2, "Different instances of PasswordHasher are equal!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }
    }
}
