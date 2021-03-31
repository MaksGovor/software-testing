using Microsoft.VisualStudio.TestTools.UnitTesting;
using IIG.FileWorker;
using System;

namespace MaksGovor.FileWorker.Test
{
    [TestClass]
    public class FileWorkerTest
    {
        [TestMethod]
        public void Test_Constructor()
        {
            try
            {
                BaseFileWorker baseFileWorker1 = new BaseFileWorker();
                BaseFileWorker baseFileWorker2 = new BaseFileWorker();

                Assert.IsNotNull(baseFileWorker1, "Instance of BaseFileWorker is null object!");
                Assert.AreNotEqual(baseFileWorker1, baseFileWorker2, "Different instances of BaseFileWorker are equal!");
            } catch(Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("\"Anything that can be written in JavaScript will be written in JavaScript.\"", 0)]
        [DataRow("- (c) Jeff Atwood, one of the creators of Stack Overflow", 1)]
        public void Test_ReadLines(string available, int index)
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                string received = BaseFileWorker.ReadLines(pathAbs)[index];

                Assert.AreEqual(received, available, "The read lines do not match the available result!");
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }

        }
    }
}
