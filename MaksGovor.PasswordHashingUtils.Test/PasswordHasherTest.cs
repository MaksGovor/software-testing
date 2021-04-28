using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.PasswordHashingUtils;
using System;

namespace MaksGovor.PasswordHashingUtils.Test
{
    [TestClass]
    public class PasswordHasherTest
    {
        private PasswordHasher _passwordHasher;
        private String _password;

        [TestInitialize]
        public void Initialization()
        {
            _passwordHasher = new PasswordHasher();
            _password = "camry3.5";
        }

    }
}
