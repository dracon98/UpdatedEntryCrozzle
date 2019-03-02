using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Diagnostics;

namespace CrozzleApplication
{
    class ConfigurationFileItem
    {
        #region constants - symbols
        const String NoConfigItem = "NO_CONFIG_ITEM";
        //CHANGES
        const String LogfileStart = "LOGFILE";
        const String LogfileName = "DEFAULT";
        const String LogfileEnd = "END-LOGFILE";
        //changes
        const String SequenceStartFile = "SEQUENCES-IN-FILE";
        const String MinimumNumberOfUniqueWords = "SEQUENCES-IN-FILE-MINIMUM";
        const String MaximumNumberOfUniqueWords = "SEQUENCES-IN-FILE-MAXIMUM";
        const String SequenceEndFile= "END-SEQUENCES-IN-FILE";
        //changes
        const String CrozzleOutputStart = "CROZZLE-OUTPUT";
        const String InvalidCrozzleScore = "INVALID-CROZZLE-SCORE";
        const String Uppercase = "UPPERCASE";
        const String Style = "STYLE";
        const String BGcolourEmptyTD = "BGCOLOUR-EMPTY-TD";
        const String BGcolourNonEmptyTD = "BGCOLOUR-NON-EMPTY-TD";
        const String CrozzleOutputEnd = "END-CROZZLE-OUTPUT";
        //changes
        const String CrozzleSizeStart = "CROZZLE-SIZE";
        const String MinimumNumberOfRows = "CROZZLE-SIZE-MINIMUM-ROWS";
        const String MaximumNumberOfRows = "CROZZLE-SIZE-MAXIMUM-ROWS";
        const String MinimumNumberOfColumns = "CROZZLE-SIZE-MINIMUM-COLUMNS";
        const String MaximumNumberOfColumns = "CROZZLE-SIZE-MAXIMUM-COLUMNS";
        const String CrozzleSizeEnd = "END-CROZZLE-SIZE";
        //changes
        const String SequencesStartCrozzle = "SEQUENCES-IN-CROZZLE";
        const String MinimumHorizontalWords = "SEQUENCES-IN-CROZZLE-MINIMUM-HORIZONTAL";
        const String MinimumVerticalWords = "SEQUENCES-IN-CROZZLE-MINIMUM-VERTICAL";
        const String MaximumHorizontalWords = "SEQUENCES-IN-CROZZLE-MAXIMUM-HORIZONTAL";
        const String MaximumVerticalWords = "SEQUENCES-IN-CROZZLE-MAXIMUM-VERTICAL";
        const String SequencesEndCrozzle = "END-SEQUENCES-IN-CROZZLE";

        //changes
        const String IntersectionsStart = "INTERSECTIONS-IN-SEQUENCES";
        const String MinimumIntersectionsInHorizontalWord = "INTERSECTIONS-IN-SEQUENCES-MINIMUM-HORIZONTAL";
        const String MaximumIntersectionsInHorizontalWord = "INTERSECTIONS-IN-SEQUENCES-MAXIMUM-HORIZONTAL";
        const String MinimumIntersectionsInVerticalWord = "INTERSECTIONS-IN-SEQUENCES-MINIMUM-VERTICAL";
        const String MaximumIntersectionsInVerticalWord = "INTERSECTIONS-IN-SEQUENCES-MAXIMUM-VERTICAL";
        const String IntersectionsEnd = "END-INTERSECTIONS-IN-SEQUENCES";

        //changes
        const String DuplicateSequencesStart = "DUPLICATE-SEQUENCES";
        const String MinimumNumberOfTheSameWord = "DUPLICATE-SEQUENCES-MINIMUM";
        const String MaximumNumberOfTheSameWord = "DUPLICATE-SEQUENCES-MAXIMUM";
        const String DuplicateSequencesEnd = "END-DUPLICATE-SEQUENCES";
        //changes
        const String ValidGroupsStart = "VALID-GROUPS";
        const String MinimumNumberOfGroups = "VALID-GROUPS-MINIMUM";
        const String MaximumNumberOfGroups = "VALID-GROUPS-MAXIMUM";
        const String ValidGroupsEnd = "VALID-GROUPS-END-VALID-GROUPS";

        const String IntersectingPointsPerLetter = "INTERSECTING-POINTS";
        const String NonIntersectingPointsPerLetter = "NON-INTERSECTING-POINTS";

        const String ColonSymbol = ":";
        const String AtoZ = @"^[A-Z]$";
        private static readonly Char[] PointSeparators = new Char[] { '-' };
        #endregion

        #region properties - errors
        public static List<String> Errors { get; set; }
        #endregion

        #region properties
        public String OriginalItem { get; set; }
        public Boolean Valid { get; set; }
        public String Name { get; set; }
        public KeyValue KeyValue { get; set; }
        #endregion

        #region properties - testing the type of the configuration item
        public Boolean IsLogFile
        {
            get { return (Regex.IsMatch(Name, @"^" + LogfileName + @"$")); }
        }

        public Boolean IsMinimumNumberOfUniqueWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfUniqueWords + @"$")); }
        }

        public Boolean IsMaximumNumberOfUniqueWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfUniqueWords + @"$")); }
        }

        public Boolean IsInvalidCrozzleScore
        {
            get { return (Regex.IsMatch(Name, @"^" + InvalidCrozzleScore + @"$")); }
        }

        public Boolean IsUppercase
        {
            get { return (Regex.IsMatch(Name, @"^" + Uppercase + @"$")); }
        }

        public Boolean IsStyle
        {
            get { return (Regex.IsMatch(Name, @"^" + Style + @"$")); }
        }

        public Boolean IsBGcolourEmptyTD
        {
            get { return (Regex.IsMatch(Name, @"^" + BGcolourEmptyTD + @"$")); }
        }

        public Boolean IsBGcolourNonEmptyTD
        {
            get { return (Regex.IsMatch(Name, @"^" + BGcolourNonEmptyTD + @"$")); }
        }

        public Boolean IsMinimumNumberOfRows
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfRows + @"$")); }
        }

        public Boolean IsMaximumNumberOfRows
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfRows + @"$")); }
        }

        public Boolean IsMinimumNumberOfColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfColumns + @"$")); }
        }

        public Boolean IsMaximumNumberOfColumns
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfColumns + @"$")); }
        }

        public Boolean IsMinimumHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumHorizontalWords + @"$")); }
        }

        public Boolean IsMaximumHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumHorizontalWords + @"$")); }
        }

        public Boolean IsMinimumVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumVerticalWords + @"$")); }
        }

        public Boolean IsMaximumVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumVerticalWords + @"$")); }
        }

        public Boolean IsMinimumIntersectionsInHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumIntersectionsInHorizontalWord + @"$")); }
        }

        public Boolean IsMaximumIntersectionsInHorizontalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumIntersectionsInHorizontalWord + @"$")); }
        }

        public Boolean IsMinimumIntersectionsInVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumIntersectionsInVerticalWord + @"$")); }
        }

        public Boolean IsMaximumIntersectionsInVerticalWords
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumIntersectionsInVerticalWord + @"$")); }
        }

        public Boolean IsMinimumNumberOfTheSameWord
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfTheSameWord + @"$")); }
        }

        public Boolean IsMaximumNumberOfTheSameWord
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfTheSameWord + @"$")); }
        }

        public Boolean IsMinimumNumberOfGroups
        {
            get { return (Regex.IsMatch(Name, @"^" + MinimumNumberOfGroups + @"$")); }
        }

        public Boolean IsMaximumNumberOfGroups
        {
            get { return (Regex.IsMatch(Name, @"^" + MaximumNumberOfGroups + @"$")); }
        }

        public Boolean IsIntersecting
        {
            get { return (Regex.IsMatch(Name, @"^" + IntersectingPointsPerLetter + @"-[A-Z].*$")); } 
        }

        public Boolean IsNonIntersecting
        {
            get { return (Regex.IsMatch(Name, @"^" + NonIntersectingPointsPerLetter + @"-[A-Z].*$")); }
        }
        #endregion      

        #region constructors
        public ConfigurationFileItem(String originalItemData)
        {
            OriginalItem = originalItemData;
            Valid = false;
            Name = null;
            KeyValue = null;
        }
        #endregion

        #region parsing
        public static Boolean TryParse(String configurationFileItem, out ConfigurationFileItem aConfigurationFileItem)
        {
            Errors = new List<String>();
            configurationFileItem = configurationFileItem.Trim();
            aConfigurationFileItem = new ConfigurationFileItem(configurationFileItem);
            
            // Discard comment.
            if (configurationFileItem.Contains("//"))
            {
                int index = configurationFileItem.IndexOf("//");
                configurationFileItem = configurationFileItem.Remove(index);
                configurationFileItem = configurationFileItem.Trim();
            }
            // New UPDATE
            // add name for unrecognized line
            if (Regex.IsMatch(configurationFileItem, @"^\s*$"))
            {
                // Check for only 0 or more white spaces.
                aConfigurationFileItem.Name = NoConfigItem;
            }
            // New UPDATE
            // add name for unrecognized line
            else if (configurationFileItem.Contains(LogfileStart))
            {
                aConfigurationFileItem.Name = LogfileStart;
            }
            // New UPDATE
            // add name for unrecognized line
            else if (configurationFileItem.Contains(CrozzleOutputStart))
            {
                aConfigurationFileItem.Name = CrozzleOutputStart;
            }
            // New UPDATE
            // add name for unrecognized line
            // and deleted
            else if (configurationFileItem.Contains("-END-"))
            {
                BufferClass.Identifier = "";
                int index = configurationFileItem.IndexOf("-END-");
                configurationFileItem = configurationFileItem.Remove(index);
                configurationFileItem = configurationFileItem.Trim();
                aConfigurationFileItem.Name = "END";
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + SequenceStartFile + "$"))
            {
                BufferClass.Identifier = SequenceStartFile;
                aConfigurationFileItem.Name = SequenceStartFile;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + CrozzleSizeStart + "$"))
            {
                BufferClass.Identifier = CrozzleSizeStart;
                aConfigurationFileItem.Name = CrozzleSizeStart;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + SequencesStartCrozzle + "$"))
            {
                BufferClass.Identifier = SequencesStartCrozzle;
                aConfigurationFileItem.Name = SequencesStartCrozzle;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + IntersectionsStart + "$"))
            {
                BufferClass.Identifier = IntersectionsStart;
                aConfigurationFileItem.Name = IntersectionsStart;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + DuplicateSequencesStart + "$"))
            {
                BufferClass.Identifier = DuplicateSequencesStart;
                aConfigurationFileItem.Name = DuplicateSequencesStart;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + ValidGroupsStart + "$"))
            {
                BufferClass.Identifier = ValidGroupsStart;
                aConfigurationFileItem.Name = ValidGroupsStart;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + IntersectingPointsPerLetter + "$"))
            {
                BufferClass.Identifier = IntersectingPointsPerLetter;
                aConfigurationFileItem.Name = IntersectingPointsPerLetter;
            }
            // New UPDATE
            // Put data to buffer
            else if (Regex.IsMatch(configurationFileItem, @"^" + NonIntersectingPointsPerLetter + "$"))
            {
                BufferClass.Identifier = NonIntersectingPointsPerLetter;
                aConfigurationFileItem.Name = NonIntersectingPointsPerLetter;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + LogfileName + @".*"))
            {
                // Get the LogFile key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, LogfileName, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = LogfileName;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfUniqueWords + @".*"))
            {
                // Get the MinimumNumberOfUniqueWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfUniqueWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfUniqueWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfUniqueWords + @".*"))
            {
                // Get the MaximumNumberOfUniqueWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfUniqueWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfUniqueWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + InvalidCrozzleScore + @".*"))
            {
                // Get the CrozzleInvalidScoreValue key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, InvalidCrozzleScore, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = InvalidCrozzleScore;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + Uppercase + @".*"))
            {
                // Get the CrozzleUppercase key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Uppercase, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Uppercase;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + Style + @".*"))
            {
                // Get the CrozzleHtmlStyle key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, Style, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = Style;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + BGcolourEmptyTD + @".*"))
            {
                // Get the CrozzleHtmlTdBgcolourSpace key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, BGcolourEmptyTD, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = BGcolourEmptyTD;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + BGcolourNonEmptyTD + @".*"))
            {
                // Get the CrozzleHtmlTdBgcolourNoSpace key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, BGcolourNonEmptyTD, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = BGcolourNonEmptyTD;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfRows + @".*"))
            {
                // Get the MinimumNumberOfRows key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfRows, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfRows;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfRows + @".*"))
            {
                // Get the MaximumNumberOfRows key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfRows, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfRows;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfColumns + @".*"))
            {
                // Get the MinimumNumberOfColumns key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfColumns, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfColumns;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfColumns + @".*"))
            {
                // Get the MaximumNumberOfColumns key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfColumns, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfColumns;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumHorizontalWords + @".*"))
            {
                // Get the MinimumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumHorizontalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumHorizontalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumVerticalWords + @".*"))
            {
                // Get the MinimumVerticalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumVerticalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumVerticalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumHorizontalWords + @".*"))
            {
                // Get the MaximumHorizontalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumHorizontalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumHorizontalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumVerticalWords + @".*"))
            {
                // Get the MaximumVerticalWords key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumVerticalWords, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumVerticalWords;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumIntersectionsInHorizontalWord + @".*"))
            {
                // Get the MinimumIntersectionsInHorizontalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumIntersectionsInHorizontalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumIntersectionsInHorizontalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumIntersectionsInHorizontalWord + @".*"))
            {
                // Get the MaximumIntersectionsInHorizontalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumIntersectionsInHorizontalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumIntersectionsInHorizontalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumIntersectionsInVerticalWord + @".*"))
            {
                // Get the MinimumIntersectionsInVerticalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumIntersectionsInVerticalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumIntersectionsInVerticalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumIntersectionsInVerticalWord + @".*"))
            {
                // Get the MaximumIntersectionsInVerticalWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumIntersectionsInVerticalWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumIntersectionsInVerticalWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfTheSameWord + @".*"))
            {
                // Get the MinimumNumberOfTheSameWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfTheSameWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfTheSameWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfTheSameWord + @".*"))
            {
                // Get the MaximumNumberOfTheSameWord key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfTheSameWord, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfTheSameWord;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MinimumNumberOfGroups + @".*"))
            {
                // Get the MinimumNumberOfGroups key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MinimumNumberOfGroups, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MinimumNumberOfGroups;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + MaximumNumberOfGroups + @".*"))
            {
                // Get the MaximumNumberOfGroups key-value pair.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, MaximumNumberOfGroups, out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                aConfigurationFileItem.Name = MaximumNumberOfGroups;
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + IntersectingPointsPerLetter + @"-[A-Z].*$"))
            {
                // Get the key-value pair for the intersecting points.
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, @"[A-Z]", out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                String[] pointAZ = configurationFileItem.Split('=');
                aConfigurationFileItem.Name = pointAZ[0];
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else if (Regex.IsMatch(configurationFileItem, @"^" + NonIntersectingPointsPerLetter + @"-[A-Z].*$"))
            {
                // Get the key-value pair for the non-intersecting points.
                // Updated
                KeyValue aKeyValue;
                if (!KeyValue.TryParse(configurationFileItem, @"[A-Z]", out aKeyValue))
                    Errors.AddRange(KeyValue.Errors);
                String[] pointAZ = configurationFileItem.Split('=');
                aConfigurationFileItem.Name = pointAZ[0];
                aConfigurationFileItem.KeyValue = aKeyValue;
            }
            else
                Errors.Add(String.Format(ConfigurationFileItemErrors.SymbolError, configurationFileItem));

            aConfigurationFileItem.Valid = Errors.Count == 0;
            return (aConfigurationFileItem.Valid);
        }
        #endregion
    }
}