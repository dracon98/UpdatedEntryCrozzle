using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrozzleApplication;
using System.Diagnostics;
using System.Collections.Generic;
using System.IO;

namespace unitTest1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            // Arrange.
            string myString = "false";
            string myString2 = "falsee";
            bool test = false;

            Boolean expectedReturn1 = true;
            Boolean expectedReturn2 = false;
            
            // Act.
            Boolean actualReturn1 = Validator.IsBoolean(myString, out test);
            Boolean actualReturn2 = Validator.IsBoolean(myString2, out test);

            // Assert.
            Assert.AreEqual(expectedReturn1, actualReturn1, "failed ...");
            Assert.AreEqual(expectedReturn2, actualReturn2, "failed ...");
            
           
        }

        [TestMethod]
        public void TestMethod2()
        {
            // Arrange.
            string myString = "25";
            string myString2 = "25.32";
            string myString3 = "-25";
            int test = 0;

            Boolean expectedReturn1 = true;
            Boolean expectedReturn2 = false;
            Boolean expectedReturn3 = true;

            // Act.
            Boolean actualReturn1 = Validator.IsInt32(myString, out test);
            Boolean actualReturn2 = Validator.IsInt32(myString2, out test);
            Boolean actualReturn3 = Validator.IsInt32(myString3, out test);


            // Assert.
            Assert.AreEqual(expectedReturn1, actualReturn1, "failed ...");
            Assert.AreEqual(expectedReturn2, actualReturn2, "failed ...");
            Assert.AreEqual(expectedReturn3, actualReturn3, "failed ...");

        }
        [TestMethod]
        public void TestMethod3()
        {
            // Arrange.
            string myString = "#2ecc71";
            string myString2 = "##ecc71";
            string myString3 = "#2ecc7g";


            Boolean expectedReturn1 = true;
            Boolean expectedReturn2 = false;
            Boolean expectedReturn3 = false;

            // Act.
            Boolean actualReturn1 = Validator.IsHexColourCode(myString);
            Boolean actualReturn2 = Validator.IsHexColourCode(myString2);
            Boolean actualReturn3 = Validator.IsHexColourCode(myString3);


            // Assert.
            Assert.AreEqual(expectedReturn1, actualReturn1, "failed ...");
            Assert.AreEqual(expectedReturn2, actualReturn2, "failed ...");
            Assert.AreEqual(expectedReturn3, actualReturn3, "failed ...");
        }

        [TestMethod]
        public void TestMethod4()
        {
            // Arrange.
            string myKeyValueData1 = "MAXIMUM = 100";
            string myKeyValueData2 = "MAXIMUM=";
            string myKeyValueData3 = "MAXIMUM= ";
            string myKeyPattern = @"[a-zA-Z0-9]";

            KeyValue _keyValue = new KeyValue(myKeyValueData1);

            Boolean expectedReturn1 = true;
            Boolean expectedReturn2 = false;
            Boolean expectedReturn3 = true;

            // Act.
            Boolean actualReturn1 = KeyValue.TryParse(myKeyValueData1, myKeyPattern, out _keyValue);
            Boolean actualReturn2 = KeyValue.TryParse(myKeyValueData2, myKeyPattern, out _keyValue);
            Boolean actualReturn3 = KeyValue.TryParse(myKeyValueData3, myKeyPattern, out _keyValue);

            // Assert.
            Assert.AreEqual(expectedReturn1, actualReturn1, "failed ...");
            Assert.AreEqual(expectedReturn2, actualReturn2, "failed ...");
            Assert.AreEqual(expectedReturn3, actualReturn3, "failed ...");
            
        }

        [TestMethod]
        public void TestMethod5()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myCrozzleFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest5\Test 1 Crozzle_invalidScore.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            string myCrozzleFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest5\Test 1 Crozzle_scoreNewWords.txt";
            string myWordlistFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest5\Test 1 Wordlist_scoreNewWords.txt";
            
            //CASE 1
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);
            
            //CASE 2 Removed a row from crozzle file
            Crozzle crozzle2;
            Crozzle.TryParse(myCrozzleFile2, config, wordlist, out crozzle2);

            //CASE 3 Adding two new words to crozzle files
            WordList wordlist3;
            WordList.TryParse(myWordlistFile3, config, out wordlist3);
            Crozzle crozzle3;
            Crozzle.TryParse(myCrozzleFile3, config, wordlist3, out crozzle3);

            //the expected output should be 303
            string expectedOriginalScore = "303";

            //the expected output of the edited crozzle file should be "INVALID CROZZLE" as row is missing 
            string expectedInvalidScore = "INVALID CROZZLE";

            //the expected output should be 336 with the new words of JACKIE and TOMORY
            string expectedNewWordsScore = "336";

            // Act.
            crozzle.Validate();
            crozzle2.Validate();
            crozzle3.Validate();
            string actualOriginalScore = crozzle.Score();
            string actualInvalidScore = crozzle2.Score();
            string actualNewWordsScore = crozzle3.Score();

            // Assert.
            Assert.AreEqual(expectedOriginalScore, actualOriginalScore, "failed ...");
            Assert.AreEqual(expectedInvalidScore, actualInvalidScore, "failed ...");
            Assert.AreEqual(expectedNewWordsScore, actualNewWordsScore, "failed ...");

        }

        [TestMethod]
        public void TestMethod6()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);
            CrozzleSequences crozzleSequences = new CrozzleSequences(crozzle.CrozzleRows, crozzle.CrozzleColumns, config);

            //SECOND FILES - Files changed so that there are two "RON" with valid config file max duplcates capped a 2.
            string myCrozzleFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest6\Test 1 Crozzle_checkDuplicates.txt";
            string myWordlistFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest6\Test 1 Wordlist_checkDuplicates.txt";
            string myConfigFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest6\Test 1 Configuration_checkDuplicates.txt";
            Configuration config2;
            Configuration.TryParse(myConfigFile2, out config2);
            WordList wordlist2;
            WordList.TryParse(myWordlistFile2, config2, out wordlist2);
            Crozzle crozzle2;
            Crozzle.TryParse(myCrozzleFile2, config2, wordlist2, out crozzle2);
            CrozzleSequences crozzleSequences2 = new CrozzleSequences(crozzle2.CrozzleRows, crozzle2.CrozzleColumns, config2);

            //Third Test - Files changed so that there are two "RON" but with config max duplicates capped at 1.
            string myCrozzleFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest6\Test 1 Crozzle_checkDuplicates.txt";
            string myWordlistFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest6\Test 1 Wordlist_checkDuplicates.txt";
            string myConfigFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            Configuration config3;
            Configuration.TryParse(myConfigFile3, out config3);
            WordList wordlist3;
            WordList.TryParse(myWordlistFile3, config3, out wordlist3);
            Crozzle crozzle3;
            Crozzle.TryParse(myCrozzleFile3, config3, wordlist3, out crozzle3);
            CrozzleSequences crozzleSequences3 = new CrozzleSequences(crozzle3.CrozzleRows, crozzle3.CrozzleColumns, config3);


            bool expectedFalseErrors = false;
            bool expectedFalseErrorsNew = false;
            bool expectedTrueErrorsNew = true;
           
            // Act.
            crozzleSequences.CheckDuplicateWords(1, 1);
            crozzleSequences2.CheckDuplicateWords(1, 2);
            crozzleSequences3.CheckDuplicateWords(1, 1);
            bool actualFalseErrors = crozzleSequences.ErrorsDetected;
            bool actualFalseErrorsNew = crozzleSequences2.ErrorsDetected;
            bool actualTrueErrorsNew = crozzleSequences3.ErrorsDetected;

            // Assert.
            Assert.AreEqual(expectedFalseErrors, actualFalseErrors, "failed ...");
            Assert.AreEqual(expectedFalseErrorsNew, actualFalseErrorsNew, "failed ...");
            Assert.AreEqual(expectedTrueErrorsNew, actualTrueErrorsNew, "failed ...");
        }

        [TestMethod]
        public void TestMethod7()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myCrozzleFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest7\Test 1 Crozzle_validateFalse.txt";
            string myWordlistFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest7\Test 1 Wordlist_validateFalse.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);

            //Unit test 2 and 3 - Word AL is removed from Crozzle file. Errors are RONALD is removed and group counts exceed config limits
            WordList wordlist2;
            WordList.TryParse(myWordlistFile2, config, out wordlist2);
            Crozzle crozzle2;
            Crozzle.TryParse(myCrozzleFile2, config, wordlist2, out crozzle2);

            bool expectedTrueValidityOriginal = true;
            bool expectedFalseValidityNew = false;
            string expectedErrorMessageNew = "code 11002: the number of horizontal words intersecting AL is 0, but this is not within [1, 100]";

            // Act.
            crozzle.Validate();
            crozzle2.Validate();
            bool actualTrueValidityOriginal = crozzle.CrozzleValid;
            bool actualFalseValidityNew = crozzle2.CrozzleValid;
            string actualErrorMessageNew = crozzle2.CrozzleGridErrors[0]; 
            

            // Assert.
            Assert.AreEqual(expectedTrueValidityOriginal, actualTrueValidityOriginal, "failed ...");
            Assert.AreEqual(expectedFalseValidityNew, actualFalseValidityNew, "failed ...");
            Assert.AreEqual(expectedErrorMessageNew, actualErrorMessageNew, "failed ...");

        }

        [TestMethod]
        public void TestMethod8()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            string myCrozzleFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\Test 1 Crozzle_scoreNewWords.txt";
            string myWordlistFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\Test 1 Wordlist_scoreNewWords.txt";
            string myConfigFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\Test 1 Configuration.txt";


            //TEST 1 - Saving files after the validate method
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);
            crozzle.Validate();

            //Save toStringHTML() result to text file
            StreamWriter writer = new StreamWriter(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html.txt");
            writer.Write(crozzle.ToStringHTML());
            writer.Close();

            //Load the HTML file to a string
            StreamReader reader = new StreamReader(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html.txt");
            string expectedAfterValidate = reader.ReadToEnd();
            reader.Close();

            //TEST 2 - Saving files before the validate method
            Crozzle crozzle2;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle2);

            StreamWriter writer2 = new StreamWriter(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html2.txt");
            writer2.Write(crozzle2.ToStringHTML());
            writer2.Close();

            StreamReader reader2 = new StreamReader(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html2.txt");
            string expectedBeforeValidate = reader2.ReadToEnd();
            reader2.Close();

            //TEST 3 - Files with new Score and seeing that the expected HTML score matches that of the actual new HTML score
            Configuration config3;
            Configuration.TryParse(myConfigFile2, out config3);
            WordList wordlist3;
            WordList.TryParse(myWordlistFile2, config3, out wordlist3);
            Crozzle crozzle3;
            Crozzle.TryParse(myCrozzleFile2, config3, wordlist3, out crozzle3);
            crozzle3.Validate();

            StreamWriter writer3 = new StreamWriter(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html3.txt");
            writer3.Write(crozzle3.ToStringHTML());
            writer3.Close();

            StreamReader reader3 = new StreamReader(@"C:\SIT323_Assignment1\Old Program and Old Files\unitTest8\html3.txt");
            string expectedNewHTML = reader3.ReadToEnd();
            reader3.Close();


            // Act.
            string actualAfterValidate = crozzle.ToStringHTML();
            string actualBeforeValidate = crozzle2.ToStringHTML();
            string actualNewHTML = crozzle3.ToStringHTML();
            crozzle2.Validate();

            // Assert.
            Assert.AreEqual(expectedAfterValidate, actualAfterValidate, "failed ...");
            Assert.AreEqual(expectedBeforeValidate, actualBeforeValidate, "failed ...");
            Assert.AreEqual(expectedNewHTML, actualNewHTML, "failed ...");

        }

        [TestMethod]
        public void TestMethod9()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            string myCrozzleFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Crozzle_groupCount2.txt";
            string myWordlistFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Wordlist_groupCount2.txt";
            string myConfigFile2 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Configuration_groupCount2.txt";
            string myCrozzleFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Crozzle_groupCount4.txt";
            string myWordlistFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Wordlist_groupCount4.txt";
            string myConfigFile3 = @"C:\SIT323_Assignment1\Old Program and Old Files\unitTest9\Test 1 Configuration_groupCount4.txt";

            //test 1 - Original files
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);
            CrozzleMap crozzleMap = new CrozzleMap(crozzle.CrozzleRows, crozzle.CrozzleColumns);

            //Test 2 - Files edited into two groups of connected words. Words removed are Mark and Jack
            Configuration config2;
            Configuration.TryParse(myConfigFile2, out config2);
            WordList wordlist2;
            WordList.TryParse(myWordlistFile2, config2, out wordlist2);
            Crozzle crozzle2;
            Crozzle.TryParse(myCrozzleFile2, config2, wordlist2, out crozzle2);
            CrozzleMap crozzleMap2 = new CrozzleMap(crozzle2.CrozzleRows, crozzle2.CrozzleColumns);

            //test 3 - Files edited into four groups. 
            Configuration config3;
            Configuration.TryParse(myConfigFile3, out config3);
            WordList wordlist3;
            WordList.TryParse(myWordlistFile3, config3, out wordlist3);
            Crozzle crozzle3;
            Crozzle.TryParse(myCrozzleFile3, config3, wordlist3, out crozzle3);
            CrozzleMap crozzleMap3 = new CrozzleMap(crozzle3.CrozzleRows, crozzle3.CrozzleColumns);

            int expectedGroupCount = 1;
            int expectedNewGroupCount1 = 2;
            int expectedNewGroupCount2 = 4;

            // Act.
            int ActualGroupCount = crozzleMap.GroupCount();
            int ActualNewGroupCount1 = crozzleMap2.GroupCount();
            int ActualNewGroupCount2 = crozzleMap3.GroupCount();

            // Assert.
            Assert.AreEqual(expectedGroupCount, ActualGroupCount, "failed ...");
            Assert.AreEqual(expectedNewGroupCount1, ActualNewGroupCount1, "failed ...");
            Assert.AreEqual(expectedNewGroupCount2, ActualNewGroupCount2, "failed ...");
        }
        [TestMethod]
        public void TestMethod10()
        {
            // Arrange.
            string configFileValid = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            Configuration config;
            
            bool expectedTrueFileReturn = true;
            int expectedMaxUniqueWords = 1000;
            bool expectedTrueUppercase = true;

            // Act.
            bool actualTrueFileReturn = Configuration.TryParse(configFileValid, out config);
            int actualMaxUniqueWords = config.MaximumNumberOfUniqueWords;
            bool actualTrueUppercase = config.Uppercase;

            // Assert.
            Assert.AreEqual(expectedTrueFileReturn, actualTrueFileReturn, "failed ...");
            Assert.AreEqual(expectedMaxUniqueWords, actualMaxUniqueWords, "failed ...");
            Assert.AreEqual(expectedTrueUppercase, actualTrueUppercase, "failed ...");

        }

        [TestMethod]
        public void TestMethod11()
        {
            // Arrange.
            string configFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            string WordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            Configuration config;
            Configuration.TryParse(configFile, out config);
            WordList wordlist;
            WordList.TryParse(WordlistFile, config, out wordlist);

            bool expectedContainedWord = true;
            int expectedFieldNumber = 31;
            bool expectedValid = true;

            // Act.
            bool actualContainedWord = wordlist.Contains("BETTY");
            int actualFieldNumber = wordlist.List.Count;
            bool actualValid = wordlist.Valid;

            // Assert.
            Assert.AreEqual(expectedContainedWord, actualContainedWord, "failed ...");
            Assert.AreEqual(expectedFieldNumber, actualFieldNumber, "failed ...");
            Assert.AreEqual(expectedValid, actualValid, "failed ...");
        }

        [TestMethod]
        public void TestMethod12()
        {
            // Arrange.
            string myCrozzleFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Crozzle.txt";
            string myWordlistFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Wordlist.txt";
            string myConfigFile = @"C:\SIT323_Assignment1\Old Program and Old Files\Test 1 Configuration.txt";
            Configuration config;
            Configuration.TryParse(myConfigFile, out config);
            WordList wordlist;
            WordList.TryParse(myWordlistFile, config, out wordlist);
            Crozzle crozzle;
            Crozzle.TryParse(myCrozzleFile, config, wordlist, out crozzle);

            int expectedReturnRows = 10;
            int expectedReturnColumns = 15;
            string expectedWordListPath = "C:\\SIT323_Assignment1\\Old Program and Old Files\\Test 1 Wordlist.txt";
            
            // Act.
            int actualReturnRows = crozzle.Rows;
            int actualReturnColumns = crozzle.Columns;
            string actualWordlistPath = crozzle.WordListPath;
            
            // Assert.
            Assert.AreEqual(expectedReturnRows, actualReturnRows, "failed ...");
            Assert.AreEqual(expectedReturnColumns, actualReturnColumns, "failed ...");
            Assert.AreEqual(expectedWordListPath, actualWordlistPath, "failed ...");
        }


    }
}
