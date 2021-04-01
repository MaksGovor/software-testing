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
            } catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #region GetFileName

        [TestMethod]
        public void Test_GetFileName_Existing_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                string received = BaseFileWorker.GetFileName(pathAbs);
                
                const string available = "testfile.txt";
                Assert.AreEqual(received, available, "The filename retrieved from GetFileName " +
                    "do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_GetFileName_NoExisting_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\NoExist.txt";
                string received = BaseFileWorker.GetFileName(pathAbs);

                Assert.IsNull(received, "The filename retrieved from GetFileName " +
                    "must be NULL on a path that does not exist!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }


        #endregion GetFileName

        #region GetFullPath 

        [TestMethod]
        public void Test_GetFullPath_Existing_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                string received = BaseFileWorker.GetFullPath(pathAbs);

                Assert.AreEqual(received, pathAbs, "The full path retrieved from GetFullPath " +
                    "must be NULL on a path that does not exist!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_GetFullPath_NoExisting_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\NoExist.txt";
                string received = BaseFileWorker.GetFullPath(pathAbs);

                Assert.IsNull(received, "The filename retrieved from GetFileName " +
                    "must be NULL on a path that does not exist!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion GetFullPath

        #region GetPath

        [TestMethod]
        public void Test_GetPath_Existing_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                string received = BaseFileWorker.GetPath(pathAbs);

                const string available = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test";

                Assert.AreEqual(received, available, "The path retrieved from GetPath " +
                    "do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_GetPath_NoExisting_Way()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\NoExist.txt";
                string received = BaseFileWorker.GetPath(pathAbs);

                Assert.IsNull(received, "The filename retrieved from GetFileName " +
                    "must be NULL on a path that does not exist!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion GetPath

        #region ReadLines

        [DataTestMethod]
        [DataRow("\"Anything that can be written in JavaScript will be written in JavaScript.\"", 0)]
        [DataRow("- (c) Jeff Atwood, one of the creators of Stack Overflow", 1)]
        public void Test_ReadLines_by_TXT_File(string available, int index)
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


        [DataTestMethod]
        [DataRow("<note>", 0)]
        [DataRow("<to>Tove</to>", 1)]
        [DataRow("<from>Jani</from>", 2)]
        [DataRow("<heading>Reminder</heading>", 3)]
        [DataRow("<body>Don't forget me this weekend!</body>", 4)]
        [DataRow("</note>", 5)]
        public void Test_ReadLines_by_XML_File(string available, int index)
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.xml";
                string received = BaseFileWorker.ReadLines(pathAbs)[index];

                Assert.AreEqual(received, available, "The read lines from XML file do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [DataTestMethod]
        [DataRow("{", 0)]
        [DataRow("  \"server\": {", 1)]
        [DataRow("    \"transport\": \"http\",", 2)]
        [DataRow("    \"address\": \"127.0.0.1\",", 3)]
        [DataRow("    \"port\": 8000", 4)]
        [DataRow("  }", 5)]
        [DataRow("}", 6)]
        public void Test_ReadLines_by_JSON_File(string available, int index)
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.json";
                string received = BaseFileWorker.ReadLines(pathAbs)[index];

                Assert.AreEqual(received, available, "The read lines from JSON file do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }
        #endregion ReadLines

        #region ReadAll

        [TestMethod]
        public void Test_ReadAll_by_TXT_File()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                string received = BaseFileWorker.ReadAll(pathAbs);

                const string available = "\"Anything that can be written in JavaScript will be written in JavaScript.\"" +
                    "\r\n- (c) Jeff Atwood, one of the creators of Stack Overflow";

                Assert.AreEqual(received, available, "The read text from TXT file do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_ReadAll_by_XML_File()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.xml";
                string received = BaseFileWorker.ReadAll(pathAbs);
                const string available = "<note>" +
                                         "\r\n<to>Tove</to>" +
                                         "\r\n<from>Jani</from>" +
                                         "\r\n<heading>Reminder</heading>" +
                                         "\r\n<body>Don't forget me this weekend!</body>" +
                                         "\r\n</note>";

                Assert.AreEqual(received, available, "The read text from XML file do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_ReadAll_by_JSON_File()
        {
            try
            {
                const string pathAbs = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.json";
                string received = BaseFileWorker.ReadAll(pathAbs);

                const string available = "{" +
                                        "\r\n  \"server\": {" +
                                        "\r\n    \"transport\": \"http\"," +
                                        "\r\n    \"address\": \"127.0.0.1\"," +
                                        "\r\n    \"port\": 8000" +
                                        "\r\n  }" +
                                        "\r\n}";

                Assert.AreEqual(received, available, "The read text from JSON file do not match the available result!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion ReadAll

        #region MkDir

        [TestMethod]
        public void Test_MkDir_by_Directory_Name()
        {
            try
            {
                const string dirName = "scopicsDoc";
                string received = BaseFileWorker.MkDir(dirName);

                const string available = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\bin\\Debug\\scopicsDoc";
                Assert.IsNotNull(received, "The directory was not created or an error occurred while creating it!");
                Assert.AreEqual(received, available, "The path in which the directory was created does not match the expected path!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_MkDir_by_Directory_FullPath()
        {
            try
            {
                const string dirName = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\scopicsDoc";
                string received = BaseFileWorker.MkDir(dirName);

                const string available = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\scopicsDoc";
                Assert.IsNotNull(received, "The directory was not created or an error occurred while creating it!");
                Assert.AreEqual(received, available, "The path in which the directory was created does not match the expected path!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion MkDir

        #region TryCopy

        [TestMethod]
        public void Test_TryCopy_TXT_Indecated_Attempts_NoRewrite_ReturnsTrue()
        {
            try
            {
                const string pathFrom = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                const string pathTo = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile(1).txt";
                const bool rewrite = false;
                const int tries = 3;

                bool received = BaseFileWorker.TryCopy(pathFrom, pathTo, rewrite, tries);

                Assert.IsTrue(received, "Copying a TXT file without overwriting with 3 attempts was unsuccessful");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryCopy_JSON_Indecated_Attempts_NoRewrite_ReturnsTrue()
        {
            try
            {
                const string pathFrom = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.json";
                const string pathTo = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile(1).json";
                const bool rewrite = false;
                const int tries = 3;

                bool received = BaseFileWorker.TryCopy(pathFrom, pathTo, rewrite, tries);

                Assert.IsTrue(received, "Copying a JSON file without overwriting with 3 attempts was unsuccessful");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryCopy_JSONtoTXT_Indecated_Attempts_NoRewrite_ReturnsTrue()
        {
            try
            {
                const string pathFrom = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.json";
                const string pathTo = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile(json).txt";
                const bool rewrite = false;
                const int tries = 3;

                bool received = BaseFileWorker.TryCopy(pathFrom, pathTo, rewrite, tries);

                Assert.IsTrue(received, "Copying a JSON file to TXT file without overwriting with 3 attempts was  unsuccessful");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryCopy_TXT_NoIndecated_Attempts_Rewrite_ReturnsTrue()
        {
            try
            {
                const string pathFrom = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                const string pathTo = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile(1).txt";
                const bool rewrite = true;

                bool received = BaseFileWorker.TryCopy(pathFrom, pathTo, rewrite);

                Assert.IsTrue(received, "Copying a TXT file with overwriting without specifying attempts (1 attempt) was unsuccessful");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryCopy_Zero_Number_Attempts_ReturnsFalse()
        {
            try
            {
                const string pathFrom = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile.txt";
                const string pathTo = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\testfile(1).txt";
                const int tries = 0;

                bool received = BaseFileWorker.TryWrite(pathFrom, pathTo, tries);

                Assert.IsFalse(received, "The file cannot be copied with 0 write attempts!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion TryCopy

        #region TryWrite

        [TestMethod]
        public void Test_TryWrite_TXT_File_Indecated_Attempts_ReturnsTrue()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.txt";
                const string text = "I want to eat a mars bar on Mars.";
                const int tries = 3;

                bool received = BaseFileWorker.TryWrite(text, pathOfNewFile, tries);

                Assert.IsTrue(received, "(TryWrite) File TXT was not written with attempts - 3 or an error occurred while trying to write!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryWrite_TXT_File_NoIndecated_Attempts_ReturnsTrue()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.txt";
                const string text = "I want to eat a mars bar on Mars.";

                bool received = BaseFileWorker.TryWrite(text, pathOfNewFile);

                Assert.IsTrue(received, "(TryWrite) File TXT was not written or an error occurred while trying to write!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryWrite_JSON_File_NoIndecated_Attempts_ReturnsTrue()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.json";
                const string text = "{" +
                    "\r\n  \"dream\": \"I want to eat a mars bar on Mars.\"" +
                    "\r\n}";

                bool received = BaseFileWorker.TryWrite(text, pathOfNewFile);

                Assert.IsTrue(received, "(TryWrite) File JSON was not written or an error occurred while trying to write!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_TryWrite_Zero_Number_Attempts_ReturnsFalse()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.json";
                const string text = "I want to eat a mars bar on Mars.";
                const int tries = 0;

                bool received = BaseFileWorker.TryWrite(text, pathOfNewFile, tries);

                Assert.IsFalse(received, "(TryWrite) The file cannot be written with 0 write attempts!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion TryWrite

        #region Write

        [TestMethod]
        public void Test_Write_TXT_File()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.txt";
                const string text = "I want to eat a mars bar on Mars.";

                bool received = BaseFileWorker.Write(text, pathOfNewFile);

                Assert.IsTrue(received, "(Write) File TXT was not written or an error occurred while trying to write!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        [TestMethod]
        public void Test_Write_JSON_File()
        {
            try
            {
                const string pathOfNewFile = "D:\\GitHub\\software-testing\\MaksGovor.FileWorker.Test\\writedFile.json";
                const string text = "{" +
                    "\r\n  \"dream\": \"I want to eat a mars bar on Mars.\"" +
                    "\r\n}";

                bool received = BaseFileWorker.Write(text, pathOfNewFile);

                Assert.IsTrue(received, "(Write) File JSON was not written or an error occurred while trying to write!");
            }
            catch (Exception err)
            {
                Assert.Fail(err.Message);
            }
        }

        #endregion Write
    }
}
