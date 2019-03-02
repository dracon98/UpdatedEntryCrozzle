using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.IO;
using System.Diagnostics;
using System.Text;

namespace CrozzleApplication
{
    public class WordList
    {
        #region constants
        private static readonly Char[] WordSeparators = new Char[] { ',' };
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        public String FileErrors
        {
            get
            {
                int errorNumber = 1;
                String errors = "START PROCESSING FILE: " + WordlistFileName + "\r\n";

                foreach (String error in WordList.Errors)
                    errors += "error " + errorNumber++ + ": " + error + "\r\n";
                errors += "END PROCESSING FILE: " + WordlistFileName + "\r\n";

                return (errors);
            }
        }

        public String FileErrorsHTML
        {
            get
            {
                int errorNumber = 1;
                String errors = "<p style=\"font-weight:bold\">START PROCESSING FILE: " + WordlistFileName + "</p>";

                foreach (String error in WordList.Errors)
                    errors += "<p>error " + errorNumber++ + ": " + error + "</p>";
                errors += "<p style=\"font-weight:bold\">END PROCESSING FILE: " + WordlistFileName + "</p>";

                return (errors);
            }
        }
        #endregion

        #region properties - filenames
        public String WordlistPath { get; set; }
        public String WordlistFileName { get; set; }
        public String WordlistDirectoryName { get; set; }
        #endregion

        #region properties
        public String[] OriginalList { get; set; }
        public Boolean Valid { get; set; } = false;
        public List<String> List { get; set; }

        public int Count
        {
            get { return (List.Count); }
        }
        #endregion

        #region constructors
        public WordList(String path, Configuration aConfiguration)
        {
            WordlistPath = path;
            WordlistFileName = Path.GetFileName(path);
            WordlistDirectoryName = Path.GetDirectoryName(path);
            List = new List<String>();
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String path, Configuration aConfiguration, out WordList aWordList)
        {
            Errors = new List<String>();
            // path for wordlist
            aWordList = new WordList(path, aConfiguration);
            StreamReader fileIn = new StreamReader(path);
            List<Sequence> WordListSequence = new List<Sequence>();
            // Read all the line inside the file 
            int row = 0;
            while (!fileIn.EndOfStream)
            {
                // New UPDATE
                // Split the original wordlist from the file usin specific regex
                // Which will skip the comma ',' inside the double-quotes
                Regex CSVParser = new Regex(",(?=(?:[^\"]*\"[^\"]*\")*(?![^\"]*\"))");
                aWordList.OriginalList = CSVParser.Split(fileIn.ReadLine());
                
                // Check each field in the wordlist.
                // which in the new class
                //int fieldNumber = 0;
                Sequence wordList = new Sequence();
                wordList.Word = aWordList.OriginalList[0];
                wordList.Score = aWordList.OriginalList[1];
                wordList.Length = aWordList.OriginalList[2];
                wordList.Ascii = aWordList.OriginalList[3];
                wordList.Total = aWordList.OriginalList[4];
                WordListSequence.Add(wordList);
                if (row > 0)
                    Configuration.PointsPerWord.Add(aWordList.OriginalList[0] + "," + aWordList.OriginalList[1]);
                row++;
            }
            int totalPointPerWord = 0;
            int totalLengthWord = 0;
            int totalASCIIPoint = 0;
            int totalAccPoint = 0;
            // Repeat for all the word listed in the group
            for (int i = 1; i < WordListSequence.Capacity; i++)
            {
                if (Sequence.Test(WordListSequence[i].Word, WordListSequence[0].Word))
                {
                    if (Regex.IsMatch(WordListSequence[i].Word, Configuration.allowedCharacters))
                        aWordList.Add(WordListSequence[i].Word);
                    else
                        Errors.Add(String.Format(WordListErrors.AlphabeticError, WordListSequence[i].Word, i - 1));
                }
                else
                    Errors.Add(String.Format(WordListErrors.UnidentifiedSequenceWordError, WordListSequence[i].Word));

                // Check the total of character of the word
                // Initialisation
                int countChar;
                Validator.IsInt32(WordListSequence[i].Length, out countChar);
                if (WordListSequence[i].Word.Length != countChar)
                    Errors.Add(String.Format(WordListErrors.CharacterWordLengthError, WordListSequence[i].Word));
                int asciiNum = 0;
                byte[] asciiBytes = Encoding.ASCII.GetBytes(WordListSequence[i].Word);
                // Accumulating the ASCII score for each character inside a word
                foreach (byte ascii in asciiBytes)
                {
                    asciiNum += (int)ascii;
                }
                int asciiString;
                Validator.IsInt32(WordListSequence[i].Ascii, out asciiString);
                // If and error management
                if (asciiNum != asciiString)
                    Errors.Add(String.Format(WordListErrors.AsciiNumberofWordError, WordListSequence[i].Word));

                // Check the Total validation number of the word\
                // Initialisation
                int intScore;
                Validator.IsInt32(WordListSequence[i].Score, out intScore);
                int Total = asciiNum + intScore + countChar;
                int intTotal;
                // If and error management
                Validator.IsInt32(WordListSequence[i].Total, out intTotal);
                if (Total != intTotal)
                    Errors.Add(String.Format(WordListErrors.TotalNumberValidationError, WordListSequence[i].Word));
                // calculating the total point each row
                totalPointPerWord += intScore;
                totalLengthWord += countChar;
                totalASCIIPoint += asciiNum;
                totalAccPoint += Total;
            }
            // Check the accumulation at the top for point row
            int rulesPoint;
            Validator.IsInt32(WordListSequence[0].Score, out rulesPoint);
            if (totalPointPerWord != rulesPoint)
                Errors.Add(String.Format(WordListErrors.TotalPointPerRowError));
            
            // Check the accumulation at the top for length row
            int rulesLength;
            Validator.IsInt32(WordListSequence[0].Length, out rulesLength);
            if (totalLengthWord != rulesLength)
                Errors.Add(String.Format(WordListErrors.TotalLengthPointError));

            // Check the accumulation at the top for ascii row
            int rulesASCII;
            Validator.IsInt32(WordListSequence[0].Ascii, out rulesASCII);
            if (totalASCIIPoint != rulesASCII)
                Errors.Add(String.Format(WordListErrors.TotalAsciiPointError));

            // Check the accumulation at the top for accumulation point row
            int rulesAccPoint;
            Validator.IsInt32(WordListSequence[0].Total, out rulesAccPoint);
            if (totalAccPoint != rulesAccPoint)
                Errors.Add(String.Format(WordListErrors.TotalAccumulationPointError));

            // Check the minimmum word limit.
            if (aWordList.Count < aConfiguration.MinimumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MinimumSizeError, aWordList.Count, aConfiguration.MinimumNumberOfUniqueWords));

            // Check the maximum word limit.
            if (aWordList.Count > aConfiguration.MaximumNumberOfUniqueWords)
                Errors.Add(String.Format(WordListErrors.MaximumSizeError, aWordList.Count, aConfiguration.MaximumNumberOfUniqueWords));

            aWordList.Valid = Errors.Count == 0;
            return (aWordList.Valid);
        }
        #endregion

        #region list functions
        public void Add(String letters)
        {
            List.Add(letters);
        }

        public Boolean Contains(String letters)
        {
            return (List.Contains(letters));
        }
        #endregion
    }
}
