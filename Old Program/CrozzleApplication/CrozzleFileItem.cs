using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CrozzleApplication
{
    class CrozzleFileItem
    {
        #region constants - symbols
        //Changes
        const String FileDependencies = "FILE-DEPENDENCIES";
        const String EndFileDependencies = "END-FILE-DEPENDENCIES";
        const String CrozzleSize = "CROZZLE-SIZE";
        const String EndCrozzleSize = "END-CROZZLE-SIZE";
        const String HorizontalSequences = "HORIZONTAL-SEQUENCES";
        const String VerticalSequences = "VERTICAL-SEQUENCES";
        const String EndHorizontalSequences = "END-HORIZONTAL-SEQUENCES";
        const String HorizontalSequencesLine = "HORIZONTAL-SEQUENCES-SEQUENCE";
        const String VerticalSequencesLine = "VERTICAL-SEQUENCES-SEQUENCE";
        const String EndVerticalSequences = "END-VERTICAL-SEQUENCES";
        const String SequenceSymbol = "SEQUENCE";
        const String LocationSymbol = "LOCATION";
        const String ConfigDataSymbol = "CONFIG-DATA";
        const String SequenceDataSymbol = "SEQUENCE-DATA";
        const String SizeSymbol = "SIZE";
        const String AtoZ = @"^[A-Z]$";
        const String NoCrozzleItem = "NO_CROZZLE_ITEM";
        /////////PREVIOUS
        /*const String ConfigurationFileSymbol = "CONFIGURATION_FILE";
        const String WordListFileSymbol = "WORDLIST_FILE";
        const String RowsSymbol = "ROWS";
        const String ColumnsSymbol = "COLUMNS";
        const String RowSymbol = "ROW";
        const String ColumnSymbol = "COLUMN";
        const String ColonSymbol = ":";*/

        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region properties
        private String OriginalItem { get; set; }
        public Boolean Valid { get; set; } = false;
        public String Name { get; set; }
        public KeyValue KeyValue { get; set; }
        public List<String> ListData { get; set; }
        #endregion

        #region properties - testing the type of the crozzle file item
        public Boolean IsConfigurationFile
        {
            get { return (Regex.IsMatch(Name, @"^" + ConfigDataSymbol + "$")); }
        }

        public Boolean isSequenceFile
        {
            get { return (Regex.IsMatch(Name, @"^" + SequenceDataSymbol + "$")); }
        }

        public Boolean isSize
        {
            get { return (Regex.IsMatch(Name, @"^" + SizeSymbol + "$")); }
        }
        public Boolean isHorizontal
        {
            get { return (Regex.IsMatch(Name, @"^" + HorizontalSequencesLine + "$")); }
        }
        public Boolean isVertical
        {
            get { return (Regex.IsMatch(Name, @"^" + VerticalSequencesLine + "$")); }
        }
        public Boolean isLocation
        {
            get { return (Regex.IsMatch(Name, @"^" + LocationSymbol + "$")); }
        }
        #endregion

        #region constructors
        public CrozzleFileItem(String originalItemData)
        {
            OriginalItem = originalItemData;
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String crozzleFileItem, out CrozzleFileItem aCrozzleFileItem)
        {
            Debug.WriteLine(crozzleFileItem);
            Errors = new List<String>();
            aCrozzleFileItem = new CrozzleFileItem(crozzleFileItem);
            // Discard comment.
            if (crozzleFileItem.Contains("//"))
            {
                int index = crozzleFileItem.IndexOf("//");
                crozzleFileItem = crozzleFileItem.Remove(index);
                crozzleFileItem = crozzleFileItem.Trim();
                aCrozzleFileItem.Name = "COMMENTED";
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aCrozzleFileItem.Name = NoCrozzleItem;
            }
            else if (crozzleFileItem.Contains("-END-"))
            {
                // New UPDATE
                // discard -end-
                BufferClass.Identifier = "";
                int index = crozzleFileItem.IndexOf("-END-");
                crozzleFileItem = crozzleFileItem.Remove(index);
                crozzleFileItem = crozzleFileItem.Trim();
                aCrozzleFileItem.Name = "END";
            }
            else if (crozzleFileItem.Contains(FileDependencies))
            {
                // New UPDATE
                // giving name for unrecognised line
                aCrozzleFileItem.Name = FileDependencies;
            }
            else if (crozzleFileItem.Contains(CrozzleSize))
            {
                // New UPDATE
                // giving name for unrecognised line
                aCrozzleFileItem.Name = CrozzleSize;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + VerticalSequences + "$"))
            {
                // Check for check if the list is vertical sequences
                BufferClass.Identifier = VerticalSequences;
                aCrozzleFileItem.Name = VerticalSequences;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + HorizontalSequences + "$"))
            {
                // Check for check if the list is horizontal sequences
                BufferClass.Identifier = HorizontalSequences;
                aCrozzleFileItem.Name = HorizontalSequences;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + ConfigDataSymbol + ".*"))
            {
                // Get the CONFIGURATION_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, ConfigDataSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = ConfigDataSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            
            else if (Regex.IsMatch(crozzleFileItem, @"^" + SequenceDataSymbol + ".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, SequenceDataSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = SequenceDataSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + HorizontalSequencesLine + ".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, HorizontalSequencesLine, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = HorizontalSequencesLine;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + VerticalSequencesLine + ".*"))
            {
                // Get the WORDLIST_FILE key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, VerticalSequencesLine, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = VerticalSequencesLine;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + SizeSymbol + ".*"))
            {
                // Get the number of rows for the crozzle.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, SizeSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = SizeSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + SequenceSymbol + ".*"))
            {
                // Get data for a horizontal word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, SequenceSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = SequenceSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(crozzleFileItem, @"^" + LocationSymbol + ".*"))
            {
                // Get data for a vertical word.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(crozzleFileItem, LocationSymbol, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aCrozzleFileItem.Name = LocationSymbol;
                aCrozzleFileItem.KeyValue = aKeyValue;
            }
            else
                Errors.Add(String.Format(CrozzleFileItemErrors.SymbolError, crozzleFileItem));

            aCrozzleFileItem.Valid = Errors.Count == 0;
            return (aCrozzleFileItem.Valid);
        }
        #endregion
    }
}