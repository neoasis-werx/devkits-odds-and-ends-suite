namespace DevKits.OddsAndEnds.Core.Text;

using System.Collections;
using System.Text;

/// <summary>
/// Options for handling Null String when Joining values.
/// </summary>
/// <remarks></remarks>
public enum NullStringOption
{
    /// <summary>
    /// Throw an exception on any null value, lets empty values pass as Placeholders.
    /// </summary>
    ThrowException,
    /// <summary>
    /// Insert the Word <see cref="StringUtils.NullAsWord" />.
    /// </summary>
    InsertNullAsWord,
    /// <summary>
    /// Leave an empty placeHolder.
    /// </summary>
    LeavePlaceHolder,
    /// <summary>
    /// Skip null values.
    /// </summary>
    SkipNull,
    /// <summary>
    /// Skip null or empty values.
    /// </summary>
    SkipNullOrEmpty
}




/// <summary>
/// A collection of String processing utilities.
/// </summary>
/// <remarks></remarks>
public static class StringUtils
{
    #region "Defaults Constants for Optional Parameters"


    /// <summary>
    /// Constant for the Null as word Place holder.
    /// </summary>

    public const string NullAsWord = "Null";
    /// <summary>
    /// <para>The Default abbreviation Threshold.</para>
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>

    public const int DefaultAbbreviationThreshold = 3;
    /// <summary>
    /// The Default wordBoundary Chars
    /// </summary>

    public const string DefaultWordBoundaryChars = "_";
    /// <summary>
    /// The Default BoundWordsOnly State/option.
    /// </summary>

    public const bool DefaultBoundedWordsOnly = false;
    #endregion

    #region Setting Enums

    /// <summary>
    /// Select the WordCase for Underscore converted words.
    /// </summary>
    /// <remarks></remarks>
    public enum WordCase
    {
        /// <summary>
        /// All upper case.
        /// </summary>
        Uppercase,
        /// <summary>
        /// All lower case
        /// </summary>
        LowerCase,
        /// <summary>
        /// Title case
        /// </summary>
        TitleCase
    }


    /// <summary>
    /// Trim Occurrences Options
    /// </summary>
    /// <remarks></remarks>
    public enum TrimOccurrence
    {
        /// <summary>
        /// Attempts to trim any consecutive prefix/suffix words. Multiple "_ID" in "LOG_ID_ID" =&gt; "LOG"  or  "-" in "LOG--" =&gt; = "LOG"
        /// </summary>
        All,
        /// <summary>
        /// Removes only one prefix/suffix after first found prefix/suffix. "_ID" in "LOG_ID_ID" =&gt; "LOG_ID" or  "-" in "LOG--" =&gt; "LOG-"
        /// </summary>
        First
    }

    #endregion

    #region Encoding

    /// <summary>
    /// Detects the byte order mark of a file and returns
    /// an appropriate encoding for the file.
    /// source:<see href="http://www.west-wind.com/Weblog/posts/197245.aspx">Detecting Text Encoding for StreamReader</see>
    /// </summary>
    /// <param name="sourceFile"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException">If The UTF-7 encoding is used. The UTF-7 encoding is insecure.<see href="https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0001"/></exception>
    /// <exception cref="ArgumentOutOfRangeException">if there is not enough bytes (6) to determine the encoding contains an invalid value.</exception>
    /// <exception cref="FileNotFoundException">The file cannot be found, file specified by <paramref name="sourceFile" /> does not exist.</exception>
    /// <exception cref="ArgumentNullException"><paramref name="sourceFile"/> is <see langword="null"/></exception>
    public static Encoding GetFileEncoding(string sourceFile)
    {
        if (sourceFile == null) throw new ArgumentNullException(nameof(sourceFile));
        if (!File.Exists(sourceFile)) throw new FileNotFoundException("The file cannot be found.", sourceFile);

        // *** Use Default of Encoding.Default (ANSI CodePage)
        var enc = Encoding.Default;

        // *** Detect byte order mark if any - otherwise assume default
        var buffer = new byte[5];
        using (FileStream file = new(sourceFile, FileMode.Open, FileAccess.Read, FileShare.Read))
        {
           _ = file.Read(buffer, 0, 5);
        }

        if (buffer[0] == 0xef && buffer[1] == 0xbb && buffer[2] == 0xbf)
        {
            enc = Encoding.UTF8;
        }

        // If the following "else if (buffer[0] equals 0xfe and buffer[1] equals 0xff)" Fixes Little Endian /Big Endian issues.
        // THEN enc = Encoding.Unicode

        else if (buffer[0] == 0xff && buffer[1] == 0xfe)
        {
            enc = Encoding.Unicode; // UTF-16le
        }
        else if (buffer[0] == 0xfe && buffer[1] == 0xff)// Fixes as per
        {
            enc = Encoding.BigEndianUnicode; // UTF-16be
        }
        else if (buffer[0] == 0 && buffer[1] == 0 && buffer[2] == 0xfe && buffer[3] == 0xff)
        {
            enc = Encoding.UTF32;
        }
        else if (buffer[0] == 0x2b && buffer[1] == 0x2f && buffer[2] == 0x76)
        {
            // enc = Encoding.UTF7  // IS INSECURE FORCE UTF8
            throw new InvalidOperationException("The UTF-7 encoding is insecure. https://learn.microsoft.com/en-us/dotnet/fundamentals/syslib-diagnostics/syslib0001");
        }

        return enc;
    }

    /// <summary>
    /// Opens a stream reader with the appropriate text encoding applied.
    /// </summary>
    /// <param name="srcFile"></param>
    public static StreamReader OpenStreamReaderWithEncoding(string srcFile)
    {
        var enc = GetFileEncoding(srcFile);
        return new StreamReader(srcFile, enc);
    }

    /// <summary>
    /// Opens a stream reader with the appropriate text encoding applied.
    /// </summary>
    /// <param name="srcFile"></param>
    public static StreamReader OpenStreamReaderWithEncoding(FileInfo srcFile)
    {
        return OpenStreamReaderWithEncoding(srcFile.FullName);
    }

    #endregion

    #region CamelCase and Word Splitting


    /// <summary>
    /// Creates a CamelCase first letter uppercase, aka PascalCase, from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <param name="wordBoundaryChars">The word boundary chars used to split the word along with upperLower cased transitions.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    public static string PascalCase(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        return CamelCase(value, true, wordBoundaryChars, abbreviationThreshold);
    }

    /// <summary>
    /// Creates a CamelCase first letter lower-cased from the supplied string using the provided word boundary characters defaulted to _, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <param name="wordBoundaryChars">The word boundary chars used to split the word along with upperLower cased transitions.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns>CamelCase first letter lower-cased string.</returns>
    public static string CamelCase(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        return CamelCase(value, false, wordBoundaryChars, abbreviationThreshold);
    }

    /// <summary>
    /// Creates a CamelCase first letter lowercased or first letter uppercased based supplied <paramref name="firstUpper"/> from the supplied string using the provided word boundary characters, and a default <paramref name="abbreviationThreshold">Abbreviation threshold</paramref> of 3.
    /// <para>Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.</para>
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <param name="firstUpper">if set to <c>true</c> set the first letter to uppercase.</param>
    /// <param name="wordBoundaryChars">The word boundary chars used to split the word along with upperLower cased transitions.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    /// <remarks></remarks>
    /// <exception cref="ArgumentNullException">value</exception>
    public static string CamelCase(string value, bool firstUpper, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        if ((value == null))
            throw new ArgumentNullException(nameof(value));
        List<string> words = SplitCamelCaseOrBoundedWordsNoFormatNoChecks(value, wordBoundaryChars, abbreviationThreshold);
        var result = Concat(words, ForcedTitleCaseStringConverter!);

        string s = words[0];
        //Handle case that this is an abbreviation.
        if ((s.Length > 1 && char.IsUpper(s[0]) && char.IsUpper(s[1])))
        {
            return result;
        }

        return firstUpper ? result : LowerCaseFirstChar(result);
    }


    /// <summary>
    /// Creates a Underscore, WordCase.Uppercase word from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>
    /// <param name="value">The string to process.</param>
    /// <param name="wordBoundaryChars">The word boundary chars used to split the word along with upperLower cased transitions.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string UnderScore(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        return UnderScore(value, WordCase.Uppercase, wordBoundaryChars, abbreviationThreshold);
    }

    /// <summary>
    /// Creates a Underscore, WordCase.Uppercase word from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="caseType">The desired caseType of new Underscored word.</param>
    /// <param name="wordBoundaryChars">The word boundary chars used to split the word along with upperLower cased transitions.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    /// <remarks></remarks>
    /// <exception cref="ArgumentNullException">value</exception>
    /// <exception cref="ArgumentException">Should not get here no such Enum.</exception>
    public static string UnderScore(string value, WordCase caseType, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        if ((value == null))
            throw new ArgumentNullException(nameof(value));
        List<string> words = SplitCamelCaseOrBoundedWordsNoFormatNoChecks(value, wordBoundaryChars, abbreviationThreshold);

        switch (caseType)
        {
            case WordCase.Uppercase:
                return Join("_", words, UpperCaseStringConverter);
            case WordCase.LowerCase:
                return Join("_", words, LowerCaseStringConverter);
            case WordCase.TitleCase:
                return Join("_", words, TitleCaseStringConverter);
            default:
                throw new ArgumentException("Should not get here no such Enum.");
        }

    }


    /// <summary>
    /// Splits the camel case or bounded words from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="wordBoundaryChars">The word boundary chars.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    /// <exception cref="ArgumentNullException">value</exception>
    public static List<string> SplitCamelCaseOrBoundedWords(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        if ((value == null))
            throw new ArgumentNullException(nameof(value));
        return SplitCamelCaseOrBoundedWordsNoChecks(value, wordBoundaryChars, abbreviationThreshold);
    }

    /// <summary>
    /// Splits the CamelCaseWords from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// It is processed without null or boundary checks. TitleCases each word using <see cref="System.Globalization.TextInfo.ToTitleCase"/> from the current culture.
    /// Used internally by other function that handle these cases.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="wordBoundaryChars">The word boundary chars.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static List<string> SplitCamelCaseOrBoundedWordsNoChecks(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold)
    {
        // Dim charArray As Char() = value.ToCharArray()

        int length = value.Length;

        int startIndex = 0;

        //Track the capitals.
        int capsRunCount = 0;

        //Get the first Character set to last
        //Dim lastChar As Char = charArray(0)
        char lastChar = value[0];

        List<string> sbWords = new List<string>();

        //update capsRunCount if this is capital
        if ((char.IsUpper(lastChar)))
            capsRunCount += 1;
        if ((wordBoundaryChars.Contains(lastChar)))
            startIndex += 1;


        for (int charIndex = 1; charIndex <= length - 1; charIndex++)
        {
            //Dim c As Char = charArray(charIndex)
            char c = value[charIndex];

            //Check for Word Boundary character
            bool currentCharIsWordBoundary = wordBoundaryChars.Contains(c);
            bool currentCharIsUpper = char.IsUpper(c);
            bool lastCharIsUpper = char.IsUpper(lastChar);


            if ((currentCharIsUpper && lastCharIsUpper))
            {
                //Both are Upper this might be an Abbreviation
                capsRunCount += 1;

            }
            else if (lastCharIsUpper && char.IsLower(c) && capsRunCount > 1)
            {
                //
                // We are here because of a transition between uppercase to lowercase char and the capital run is greater than 1
                //
                // Make Sure we spare the last Capital as it is a part of the next word (-1)
                capsRunCount -= 1;
                string word = value.Substring(startIndex, charIndex - startIndex - 1);

                // The capital letter run is above our threshold for Abbreviation so Lower Case it to TitleCase.
                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();

                //Title Case our Stuff.
                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(ToTitleCase(word));
                }
                //Set the last position behind the Last capital as it is the start of a new Word.
                startIndex = charIndex - 1;
                capsRunCount = 0;
                //Reset the caps run.

            }
            else if ((char.IsLower(lastChar) && currentCharIsUpper) || (currentCharIsWordBoundary))
            {
                //
                //   Our most standard transition from a lowercase to an uppercase char
                // or
                //   because of we hit a word boundary Character.
                //
                string word = value.Substring(startIndex, charIndex - startIndex);

                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();
                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(ToTitleCase(word));
                }
                startIndex = charIndex;
                capsRunCount = currentCharIsUpper ? 1 : 0;
                //Starting a new the caps run.

                //This is a word boundary Character so exclude by advancing past it.
                if ((currentCharIsWordBoundary))
                    startIndex += 1;

            }
            lastChar = c;
        }
        string finalWord = value.Substring(startIndex);
        if (!(string.IsNullOrEmpty(finalWord)))
        {
            sbWords.Add(ToTitleCase(finalWord));
        }
        return sbWords;
    }

    /// <summary>
    /// Splits the CamelCaseWords from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// It is processed without null or boundary checks. does not format the list.
    /// Used internally by other function that handle these cases.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="wordBoundaryChars">The word boundary chars.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <param name="boundedWordsOnly"> </param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static List<string> SplitCamelCaseOrBoundedWordsNoFormatNoChecks(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold, bool boundedWordsOnly = DefaultBoundedWordsOnly)
    {

        //Dim charArray As Char() = value.ToCharArray()

        int length = value.Length;

        int startIndex = 0;

        //Track the capitals.
        int capsRunCount = 0;

        //Get the first Character set to last
        //Dim lastChar As Char = charArray(0)
        char lastChar = value[0];

        List<string> sbWords = new List<string>();

        //update capsRunCount if this is capital
        if ((char.IsUpper(lastChar)))
            capsRunCount += 1;
        if ((wordBoundaryChars.Contains(lastChar)))
            startIndex += 1;


        for (int charIndex = 1; charIndex <= length - 1; charIndex++)
        {
            //Dim c As Char = charArray(charIndex)
            char c = value[charIndex];

            //Check for Word Boundary character
            bool currentCharIsWordBoundary = wordBoundaryChars.Contains(c);
            bool currentCharIsUpper = char.IsUpper(c);
            bool lastCharIsUpper = char.IsUpper(lastChar);


            if ((currentCharIsUpper && lastCharIsUpper))
            {
                //Both are Upper this might be an Abbreviation
                capsRunCount += 1;

            }
            else if (!boundedWordsOnly && lastCharIsUpper && char.IsLower(c) && capsRunCount > 1)
            {
                //
                // We are here because of a transition between uppercase to lowercase char and the capital run is greater than 1
                //
                // Make Sure we spare the last Capital as it is a part of the next word (-1)
                capsRunCount -= 1;
                string word = value.Substring(startIndex, charIndex - startIndex - 1);

                // The capital letter run is above our threshold for Abbreviation so Lower Case it to TitleCase.
                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();

                //Title Case our Stuff.
                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(word);
                }
                //Set the last position behind the Last capital as it is the start of a new Word.
                startIndex = charIndex - 1;
                capsRunCount = 0;
                //Reset the caps run.

            }
            else if (!boundedWordsOnly && (char.IsLower(lastChar) && currentCharIsUpper) || (currentCharIsWordBoundary))
            {
                //
                //   Our most standard transition from a lowercase to an uppercase char
                // or
                //   because of we hit a word boundary Character.
                //
                string word = value.Substring(startIndex, charIndex - startIndex);

                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();

                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(word);
                }
                startIndex = charIndex;
                capsRunCount = currentCharIsUpper ? 1 : 0;
                //Starting a new the caps run.

                //This is a word boundary Character so exclude by advancing past it.
                if ((currentCharIsWordBoundary))
                    startIndex += 1;

            }
            lastChar = c;
        }
        string finalWord = value.Substring(startIndex);
        if (!(string.IsNullOrEmpty(finalWord)))
        {
            sbWords.Add(finalWord);
        }
        return sbWords;
    }


    /// <summary>
    /// Splits the CamelCaseWords from the supplied string using the provided word boundary characters, and a default Abbreviation threshold of 3.
    /// Capitalized letter runs less than or equal to the abbreviation threshold are treated as Abbreviations.
    /// It is processed without null or boundary checks. does not format the list.
    /// Used internally by other function that handle these cases.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <param name="wordBoundaryChars">The word boundary chars.</param>
    /// <param name="abbreviationThreshold">The abbreviation threshold. <see cref="DefaultAbbreviationThreshold"/></param>
    /// <param name="boundedWordsOnly">set to true, if you don't want to split via camel-casing.</param>
    /// <param name="converter">A per word converter function.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static List<string> SplitCamelCaseOrBoundedWordsNoChecksConverter(string value, string wordBoundaryChars = DefaultWordBoundaryChars, int abbreviationThreshold = DefaultAbbreviationThreshold, bool boundedWordsOnly = DefaultBoundedWordsOnly, Func<string, string>? converter = null)
    {
        converter = converter ?? PassThruConverter;

        int length = value.Length;

        int startIndex = 0;

        // Track the capitals.
        int capsRunCount = 0;

        // Get the first Character set to last
        char lastChar = value[0];

        List<string> sbWords = new List<string>();

        // update capsRunCount if this is capital
        if ((char.IsUpper(lastChar)))
            capsRunCount += 1;
        if ((wordBoundaryChars.Contains(lastChar)))
            startIndex += 1;


        for (int charIndex = 1; charIndex <= length - 1; charIndex++)
        {
            char c = value[charIndex];

            // Check for Word Boundary character
            bool currentCharIsWordBoundary = wordBoundaryChars.Contains(c);
            bool currentCharIsUpper = char.IsUpper(c);
            bool lastCharIsUpper = char.IsUpper(lastChar);

            if ((currentCharIsUpper && lastCharIsUpper))
            {
                // Both are Upper this might be an Abbreviation
                capsRunCount += 1;
            }
            else if (!boundedWordsOnly && lastCharIsUpper && char.IsLower(c) && capsRunCount > 1)
            {
                //
                // We are here because of a transition between uppercase to lowercase char and the capital run is greater than 1
                //
                // Make Sure we spare the last Capital as it is a part of the next word (-1)
                capsRunCount -= 1;
                string word = value.Substring(startIndex, charIndex - startIndex - 1);

                // The capital letter run is above our threshold for Abbreviation so Lower Case it to TitleCase.
                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();

                // Title Case our Stuff.
                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(converter(word));
                }
                // Set the last position behind the Last capital as it is the start of a new Word.
                startIndex = charIndex - 1;
                capsRunCount = 0;
                // Reset the caps run.

            }
            else if (!boundedWordsOnly && (char.IsLower(lastChar) && currentCharIsUpper) || (currentCharIsWordBoundary))
            {
                //
                //   Our most standard transition from a lowercase to an uppercase char
                // or
                //   because of we hit a word boundary Character.
                //
                string word = value.Substring(startIndex, charIndex - startIndex);

                if ((capsRunCount > abbreviationThreshold))
                    word = word.ToLower();

                if (!(string.IsNullOrEmpty(word)))
                {
                    sbWords.Add(converter(word));
                }
                startIndex = charIndex;
                capsRunCount = currentCharIsUpper ? 1 : 0;
                // Starting a new the caps run.

                // This is a word boundary Character so exclude by advancing past it.
                if ((currentCharIsWordBoundary))
                    startIndex += 1;

            }
            lastChar = c;
        }
        string finalWord = value.Substring(startIndex);
        if (!(string.IsNullOrEmpty(finalWord)))
        {
            sbWords.Add(converter(finalWord));
        }
        return sbWords;
    }

    #endregion

    #region String Casing

    /// <summary>
    /// Uppercase the first letter of the <paramref name="word"/>, Does not modify the remaining letters.
    /// </summary>
    /// <param name="word">The word.</param>
    /// <param name="nullSafe">Optional parameter, if true returns empty string for null. Default=True</param>
    /// <returns>The <paramref name="word"/> with its first letter uppercase.</returns>
    /// <exception cref="ArgumentNullException"><paramref name="word"/> is null.</exception>
    public static string UpperCaseFirstChar(string word, bool nullSafe = true)
    {
        if (String.IsNullOrWhiteSpace(word) && nullSafe)
        {
            return string.Empty;
        }

        ArgumentNullException.ThrowIfNull(word);

        if (word.Length > 1)
        {
            return Char.ToUpper(word[0]) + word.Substring(1);
        }
        return word.ToUpper();
    }

    /// <summary>
    /// Lowercases the first letter of the word. Does not modify the remaining letters.
    /// </summary>
    /// <param name="word">The word.</param>
    /// <param name="nullSafe">Optional parameter, if true returns empty string for null. Default=True</param>
    /// <returns>The word with its first letter lowercase.</returns>
    /// <remarks></remarks>
    /// <exception cref="ArgumentNullException">word</exception>
    public static string LowerCaseFirstChar(string word, bool nullSafe = true)
    {
        if (String.IsNullOrWhiteSpace(word) && nullSafe)
        {
            return string.Empty;
        }

        ArgumentNullException.ThrowIfNull(word);

        if (word.Length > 1)
        {
            return Char.ToLower(word[0]) + word.Substring(1);
        }
        return word.ToLower();
    }


    /// <summary>
    /// Converts a <paramref name="word"/> to a title case.
    /// </summary>
    /// <param name="word">The word.</param>
    /// <returns>word as a string.</returns>
    public static string ToTitleCase(string word)
    {
        return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(word);
    }

    #endregion

    #region String Joining

    /// <summary>
    /// Concatenates the specified values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values">The values.</param>
    /// <returns>String.</returns>
    public static String Concat<T>(IEnumerable<T> values)
    {
        return String.Concat(values);
    }

    /// <summary>
    /// Concatenates the specified values.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="values">The values.</param>
    /// <param name="converter">The converter.</param>
    /// <returns>String.</returns>
    public static String Concat<T>(IEnumerable<T> values, Converter<T, string> converter)
    {
        return Join(String.Empty, values, converter);
    }

    /// <summary>
    /// Join that takes an IEnumerable enumerable, using special null string handling, and joins with the specified separator.
    /// </summary>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The enumerable.</param>
    /// <param name="nullStringHandling">The null string handling.</param>
    /// <param name="wordForNull"> </param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string Join(string separator, IEnumerable values, NullStringOption nullStringHandling = NullStringOption.LeavePlaceHolder, string? wordForNull = NullAsWord)
    {
        return Join(separator, values.Cast<Object>(), DefaultObjectConverter, nullStringHandling, wordForNull!);
    }

    /// <summary>
    /// Joins.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The enumerable.</param>
    /// <param name="converter">The converter.</param>
    /// <returns>.</returns>
    public static string Join<T>(string separator, IEnumerable<T> values, Converter<T, string?> converter)
    {
        return Join(separator, values, converter, NullStringOption.LeavePlaceHolder);
    }


    /// <summary>
    /// Join that takes an IEnumerable enumerable, uses a converter to convert the type to a string, and joins with the specified separator.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="separator">The separator.</param>
    /// <param name="values">The enumerable.</param>
    /// <param name="converter">The converter.</param>
    /// <param name="nullStringHandling">The null string handling. <see cref="NullStringOption"/></param>
    /// <param name="wordForNull"> </param>
    /// <returns></returns>
    /// <remarks></remarks>s
    /// <exception cref="ArgumentNullException">values</exception>
    /// <exception cref="InvalidOperationException">Undefined NullStringOption should never get here.</exception>
    public static string Join<T>(string separator, IEnumerable<T> values, Converter<T, string?> converter, NullStringOption nullStringHandling, string wordForNull = NullAsWord)
    {

        // DCM I Know I can simplify this logic. It was a Brute force, under duress, creation that works.
        // StringUtils2 represents the start of my efforts to do so. Fortunately, I have test cases to verify my expected results.


        if ((values == null))
            throw new ArgumentNullException(nameof(values));

        var lastIndex = 0;

        var skipSeparator = string.IsNullOrEmpty(separator);

        // REMOVED SOME HOW NOT COMPATIBLE WHEN CONVERTED FROM VB TO C#.
        // Pre-allocate StringBuilder if implements ICollection(of String) or IList(of String)
        // Dramatically improves performance even with this loop.
        // Attempt at combining these two cases ICollection and IList, buys nothing and
        // in fact works against the optimization as it resorts to an IEnumerable.
        // REMOVED END

        var sb = new StringBuilder();

        //Cache outside the loop.
        var shouldSkipEmpty = nullStringHandling == NullStringOption.SkipNullOrEmpty;
        var shouldSkipNulls = nullStringHandling == NullStringOption.SkipNull || shouldSkipEmpty;


        foreach (T obj in values)
        {
            // Store this result for several comparisons
            var objectIsNothing = IsNull(obj);

            var objectIsEmpty = false;

            var objString = obj as string;

            if (objString != null)
            {
                objectIsEmpty = objString.Length == 0;
            }


            //Insert a separator if and only if there is something already in the StringBuilder.
            if (sb.Length != 0 && !skipSeparator && (!objectIsNothing || !shouldSkipNulls) && (!objectIsEmpty || !shouldSkipEmpty))
            {
                sb.Append(separator);
            }

            // Null handling
            if (!objectIsNothing)
            {
                if (converter == null)
                {
                    sb.Append(obj);
                }
                else
                {
                    sb.Append(converter(obj));
                }
            }
            else
            {
                switch (nullStringHandling)
                {
                    case NullStringOption.InsertNullAsWord:
                        sb.Append(wordForNull);
                        break;
                    case NullStringOption.LeavePlaceHolder:
                        sb.Append(string.Empty);
                        break;
                    case NullStringOption.SkipNull:
                        //DO NOTHING Append(Nothing) would do the right thing, but don't bother.
                        break;
                    case NullStringOption.SkipNullOrEmpty:
                        //DO NOTHING Append(Nothing) would do the right thing, but don't bother.
                        break;
                    case NullStringOption.ThrowException:
                        throw new ArgumentNullException("Item[" + lastIndex + "]");
                    default:
                        throw new InvalidOperationException("Undefined NullStringOption should never get here.");
                }

            }

            //Track the lastIndex of the listOfStrings. used for more descriptive error message.
            if (NullStringOption.ThrowException == nullStringHandling)
            {
                lastIndex += 1;
            }

        }
        return sb.ToString();

    }

    #endregion

    #region String Trimming

    /// <summary>
    /// Trims the first occurrence of specified <c>params</c> array of suffixes from the end of the supplied word ignoring the case of suffix, using <see cref="StringComparison.InvariantCultureIgnoreCase"/> and <see cref="TrimOccurrence.First" />. Stops processing after first found suffix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimEnd(aWord,"_ID","ID","D")</c>
    /// </para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="suffixes">The String ParamArray of suffixes to remove.</param>
    /// <returns>The string with the first found suffix removed from the end.</returns>
    /// <remarks></remarks>
    public static string TrimEnd(string word, params string[] suffixes)
    {
        return TrimEnd(word, true, TrimOccurrence.First, suffixes);
    }

    /// <summary>
    /// Trims specified parameter array of suffixes from the end of the supplied word ignoring the case of suffix, using <see cref="StringComparison.InvariantCultureIgnoreCase"/>. Stops processing after first found suffix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimEnd(aWord,"_ID","ID","D")</c>
    /// </para>
    /// <para>If <paramref name="occurrence"/> is <see cref="TrimOccurrence.All" /> then it will continue to attempt to remove consecutive occurrences of the found suffix</para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="occurrence">The occurrence to trim, <see cref="TrimOccurrence"/>. If <see cref="TrimOccurrence.All" /> then attempt to remove consecutive occurrences; else remove the first.</param>
    /// <param name="suffixes">The String ParamArray of suffixes to remove.</param>
    /// <returns>The string with the first found suffix removed from the end based on <paramref name="occurrence"/> </returns>
    /// <remarks></remarks>
    public static string TrimEnd(string word, TrimOccurrence occurrence, params string[] suffixes)
    {
        return TrimEnd(word, true, occurrence, suffixes);
    }

    /// <summary>
    /// Trims specified parameter array of suffixes from the end of the supplied word. Stops processing after first found suffix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimEnd(aWord,"_ID","ID","D")</c>
    /// </para>
    /// <para>If <paramref name="occurrence"/> is <see cref="TrimOccurrence.All" /> then it will continue to attempt to remove consecutive occurrences of the found suffix</para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="ignoreCase">if set to <c>true</c> ignore case using <see cref="StringComparison.InvariantCultureIgnoreCase" /> else use <see cref="StringComparison.InvariantCulture" />.</param>
    /// <param name="occurrence">The occurrence to trim, <see cref="TrimOccurrence"/>. If <see cref="TrimOccurrence.All" /> then attempt to remove consecutive occurrences; else remove the first.</param>
    /// <param name="suffixes">The String ParamArray of suffixes to remove.</param>
    /// <returns>The string with the first found suffix removed from the end based on <paramref name="occurrence"/> </returns>
    /// <remarks></remarks>
    public static string TrimEnd(string word, bool ignoreCase, TrimOccurrence occurrence, params string[] suffixes)
    {
        //Short-circuit if word is null or empty
        if ((string.IsNullOrEmpty(word)))
            return word;

        string result = word;
        var strComparison = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
        foreach (string suffix in suffixes)
        {
            if ((result.EndsWith(suffix, strComparison)))
            {
                if ((result.Length - suffix.Length <= 0))
                    return string.Empty;
                result = result.Substring(0, result.Length - suffix.Length);

                if ((occurrence == TrimOccurrence.All))
                {
                    //Remove all of them
                    while ((result.EndsWith(suffix, strComparison)))
                    {
                        if ((result.Length - suffix.Length <= 0))
                            return string.Empty;
                        result = result.Substring(0, result.Length - suffix.Length);
                    }
                }

                return result;
            }
        }
        return result;
    }


    /// <summary>
    /// Trims specified parameter array of prefixes from the start of the supplied word ignoring the case of prefix,  using <see cref="StringComparison.InvariantCultureIgnoreCase"/> and <see cref="TrimOccurrence.First" />. Stops processing after first found suffix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimStart(aWord,"TBL_","TBL","TB")</c>
    /// </para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="prefixes">The String ParamArray of prefixes to remove.</param>
    /// <returns>The string with the first found prefix removed from the end.</returns>
    /// <remarks></remarks>
    public static string TrimStart(string word, params string[] prefixes)
    {
        return TrimStart(word, true, TrimOccurrence.First, prefixes);
    }


    /// <summary>
    /// Trims specified parameter array of prefixes from the start of the supplied word ignoring the case of prefix, using <see cref="StringComparison.InvariantCultureIgnoreCase" />. Stops processing after first found suffix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimStart(aWord,"TBL_","TBL","TB")</c>
    /// </para>
    /// <para>If <paramref name="occurrence"/> is <see cref="TrimOccurrence.All" /> then it will continue to attempt to remove consecutive occurrences of the found prefix</para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="occurrence">The occurrence to trim, <see cref="TrimOccurrence"/>. If <see cref="TrimOccurrence.All" /> then attempt to remove consecutive occurrences; else remove the first.</param>
    /// <param name="prefixes">The String ParamArray of prefixes to remove.</param>
    /// <returns>The string with the first found prefix removed from the end based on <paramref name="occurrence"/> </returns>
    /// <remarks></remarks>
    public static string TrimStart(string word, TrimOccurrence occurrence, params string[] prefixes)
    {
        return TrimStart(word, true, occurrence, prefixes);
    }


    /// <summary>
    /// Trims specified parameter array of prefixes from the start of the supplied word. Stops processing after first found prefix.
    /// <para>
    /// Search is in declared order, so most specific case should be first <c>TrimStart(aWord,"TBL_","TBL","TB")</c>
    /// </para>
    /// <para>If <paramref name="occurrence"/> is <see cref="TrimOccurrence.All" /> then it will continue to attempt to remove consecutive occurrences of the found prefix</para>
    /// </summary>
    /// <param name="word">The word to trim.</param>
    /// <param name="ignoreCase">if set to <c>true</c> ignore case using <see cref="StringComparison.InvariantCultureIgnoreCase" /> else use <see cref="StringComparison.InvariantCulture" />.</param>
    /// <param name="occurrence">The occurrence to trim, <see cref="TrimOccurrence"/>. If <see cref="TrimOccurrence.All" /> then attempt to remove consecutive occurrences; else remove the first.</param>
    /// <param name="prefixes">The String ParamArray of prefixes to remove.</param>
    /// <returns>The string with the first found prefix removed from the end based on <paramref name="occurrence"/> </returns>
    /// <remarks></remarks>
    public static string TrimStart(string word, bool ignoreCase, TrimOccurrence occurrence, params string[] prefixes)
    {
        //Short-circuits if word is null or empty
        if ((string.IsNullOrEmpty(word)))
            return word;

        string result = word;
        var strComparison = ignoreCase ? StringComparison.InvariantCultureIgnoreCase : StringComparison.InvariantCulture;
        foreach (string prefix in prefixes)
        {
            if ((result.StartsWith(prefix, strComparison)))
            {
                if ((result.Length - prefix.Length <= 0))
                    return string.Empty;
                result = result.Substring(prefix.Length);

                if ((occurrence == TrimOccurrence.All))
                {
                    //Remove all of them
                    while ((result.StartsWith(prefix, strComparison)))
                    {
                        if ((result.Length - prefix.Length <= 0))
                            return string.Empty;
                        result = result.Substring(prefix.Length);
                    }
                }

                return result;
            }
        }
        return result;
    }

    #endregion

    #region String Converters


    /// <summary>
    /// A Pass Thru Converter which simply outputs the input.
    /// </summary>
    public static T PassThruConverter<T>(T input)
    {
        return input;
    }

    /// <summary>
    /// The Default object converter using ToString if not null else return null.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? DefaultObjectConverter(object? o)
    {
        return o?.ToString();
        // return Convert.ToString(0); //This handles more scenarios but return empty string which will defeat my null handling
    }


    /// <summary>
    /// The Default object converter using ToString if not null else return null.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? DefaultTypeConverter<T>(T? o)
    {
        return IsNotNull(o) ? o?.ToString() : null;
        // return Convert.ToString(0); //This handles more scenarios but return empty string which will defeat my null handling
    }

    /// <summary>
    /// The Default <see cref="String"/> converter pass through for a string.
    /// </summary>s
    /// <param name="o">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? DefaultStringConverter(string? o)
    {
        return o;
    }



    /// <summary>
    /// The titleCase object converter using ToString if not null else return null.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? TitleCaseObjectConverter(object? o)
    {
        return o != null ? ToTitleCase(o.ToString()!) : null;
    }

    /// <summary>
    /// The titleCase String converter using ToString if not null else return null.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? TitleCaseStringConverter(string? o)
    {
        return o != null ? ToTitleCase(o) : null;
    }


    /// <summary>
    /// The titleCase String converter using ToString if not null else return null.
    /// </summary>
    /// <param name="word">The word.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? ForcedTitleCaseStringConverter(string? word)
    {
        return word != null ? ToTitleCase(word.ToLower()) : null;
    }

    /// <summary>
    /// Uppercase the first letter of the <paramref name="word"/>, Does not modify the remaining letters.
    /// </summary>
    /// <param name="word">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? UpperCaseFirstCharStringConverter(string? word)
    {
        return word != null ? UpperCaseFirstChar(word) : null;
    }

    /// <summary>
    /// Uppercase the first letter of the <paramref name="word"/>, Does not modify the remaining letters.
    /// </summary>
    /// <param name="word">The o.</param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static string? LowerCaseFirstCharStringConverter(string? word)
    {
        return word != null ? LowerCaseFirstChar(word) : null;
    }



    /// <summary>
    /// Upper case string converter.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns>.</returns>
    public static string? UpperCaseStringConverter(string? o)
    {
        return o != null ? o.ToUpper() : null;
    }

    /// <summary>
    /// Lower case string converter.
    /// </summary>
    /// <param name="o">The o.</param>
    /// <returns>.</returns>
    public static string? LowerCaseStringConverter(string? o)
    {
        return o != null ? o.ToLower() : null;
    }

    #endregion


    /// <summary>
    /// Creates a new <see cref="String"/> by repeating n times.
    /// </summary>
    /// <throws cref="OutOfMemoryException">
    /// This method will throw if the capacity would overflow.
    /// </throws>
    /// <example>
    /// Basic usage:
    /// <code>
    /// Assert.Equal("abcabcabcabc", "abc".Repeat(4));
    /// </code>
    /// </example>
    /// <example>
    /// Throws upon overflow:
    /// <code>
    /// // this will throw at runtime
    /// var huge = "0123456789".Repeat(int.MaxValue);
    /// </code>
    /// </example>
    /// <remarks>[How to Repeat a String in C# | NimblePros Blog](https://blog.nimblepros.com/blogs/repeat-string-in-csharp/)</remarks>
    public static string Repeat(string text, uint n)
    {
        var textAsSpan = text.AsSpan();
        var span = new Span<char>(new char[textAsSpan.Length * (int)n]);
        for (var i = 0; i < n; i++)
        {
            textAsSpan.CopyTo(span.Slice(i * textAsSpan.Length, textAsSpan.Length));
        }

        return span.ToString();
    }

    /// <summary>
    /// String between tags.
    /// </summary>
    /// <param name="source">Source for the.</param>
    /// <param name="startTag">The start tag.</param>
    /// <param name="endTag">The end tag.</param>
    /// <returns>the string between the tags</returns>
    public static string? StringBetweenTags(string source, string startTag, string endTag)
    {

        var startIdx = source.IndexOf(startTag, StringComparison.Ordinal);
        if ((startIdx < 0))
        {
            return null;
        }

        var startPosition = startIdx + startTag.Length;
        var endPosition = source.IndexOf(endTag, startPosition, StringComparison.Ordinal);

        return (endPosition < 0) ? null : source.Substring(startPosition, endPosition - startPosition);
    }

    /// <summary>
    /// String after tag.
    /// </summary>
    /// <param name="source">Source for the.</param>
    /// <param name="startTag">The start tag.</param>
    /// <returns>the string after the startTag</returns>
    public static string? StringAfterTag(string source, string startTag)
    {
        var startIndex = source.IndexOf(startTag, StringComparison.Ordinal);
        if ((startIndex < 0))
        {
            return null;
        }

        var startPosition = startIndex + startTag.Length;

        return source.Substring(startPosition);
    }

    /// <summary>
    /// Query if 'value' is null.
    /// </summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    /// <param name="value">The value.</param>
    /// <returns>true if null, false if not.</returns>
    public static bool IsNull<T>(T value)
    {
        // ReSharper disable RedundantCast
        return !(value is ValueType) && null == (object?)value; //Box this value
                                                               // ReSharper restore RedundantCast
    }

    public static bool IsNotNull<T>(T value)
    {
        return !IsNull(value);
    }

}