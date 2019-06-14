namespace UserDefinedFunctions
{
    public class SubstitutionCipher
    {
        /// <summary>
        /// The SubstitutionCipher functionality in this class was blatently stolen from the example
        /// in the Corepoint SDK folder.It has been separated into a class and re-engineered slightly
        /// to work with the current methodology.
        /// 
        /// The method performs character substitution on a string by replacing the characters in
        /// the 'sourceArgument' string with the pattern specified in 'options'.
        /// 
        /// Characters 'abc' are the source characters and 'def' are the result characters. All
        /// instances of 'a' in 'SourceArgment' string will be replaced with 'd'.
        /// </summary>
        /// <param name="sourceArgument">The data to be formatted.</param>
        /// <param name="options">
        /// Comma separated source and result strings.
        ///  Format: <chars>,<replacement chars>
        ///  Example: 'abc,def'
        /// Note: The source and result strings must be same length.
        /// </param>
        /// <param name="resultData">The formatted data is returned to this output variable.</param>
        /// <param name="errorMessage">Errors are returned to this output variable.</param>
        public static void SubstituteStrings(string sourceArgument, string options, out string resultData, out string errorMessage)
        {
            resultData = "";
            errorMessage = "";

            string[] splitOptions = options.Split(',');

            if (splitOptions.Length != 2)
            {
                errorMessage = "Invalid option string, please verify your source and result strings are balanced and have at least 1 character.";
                resultData = "Error";
                return;
            }

            if (splitOptions[0].Length != splitOptions[1].Length)
            {
                errorMessage = "The lengths of source and result strings must match";
                resultData = "Error";
                return;
            }

            for (int i = 0; i < sourceArgument.Length; i++)
            {
                char nextCharacter = sourceArgument[i];
                int position = splitOptions[0].IndexOf(nextCharacter);
                if (position >= 0)
                {
                    nextCharacter = splitOptions[1][position];
                }
                resultData += nextCharacter;
                errorMessage = "";
            }
        }
    }
}
