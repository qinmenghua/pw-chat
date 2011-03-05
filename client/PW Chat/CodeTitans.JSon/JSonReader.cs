#region License
/*
    Copyright (c) 2010, Paweł Hofman (CodeTitans)
    All Rights Reserved.

    Licensed under the Apache License version 2.0.
    For more information please visit:

    http://codetitans.codeplex.com/license
        or
    http://www.apache.org/licenses/


    For latest source code, documentation, samples
    and more information please visit:

    http://codetitans.codeplex.com/
*/
#endregion

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Text;
using CodeTitans.JSon.Objects;

namespace CodeTitans.JSon
{
    /// <summary>
    /// Reader class capable of parsing texts and providing them as JSON objects (or arrays of objects).
    /// </summary>
    public sealed class JSonReader : IJSonReader
    {
        /// <summary>
        /// Value of JSON 'null' keyword string.
        /// </summary>
        public const string NullString = "null";
        /// <summary>
        /// Value of JSON 'true' keyword string.
        /// </summary>
        public const string TrueString = "true";
        /// <summary>
        /// Value of JSON 'false' keyword string.
        /// </summary>
        public const string FalseString = "false";

        /// <summary>
        /// Digits being a part of hexadecimal number.
        /// </summary>
        private const string HexDigits = "0123456789abcdefABCDEF";

        #region TokenData

        /// <summary>
        /// Info class about token.
        /// </summary>
        private class TokenData<T>
        {
            public TokenData(T token, JSonReaderTokenType type)
            {
                Token = token;
                Type = type;
            }

            public TokenData(T token, JSonReaderTokenType type, object value, IJSonObject jValue)
            {
                Token = token;
                Type = type;
                Value = value;
                ValueAsJSonObject = jValue;
            }

            #region Propeties

            public T Token
            { get; private set; }

            public JSonReaderTokenType Type
            { get; private set; }

            public object Value
            { get; private set; }

            public IJSonObject ValueAsJSonObject
            { get; private set; }

            #endregion
        }

        #endregion

        private static readonly TokenData<char>[] AvailableTokens= new TokenData<char>[]
                                {
                                    new TokenData<char>('{', JSonReaderTokenType.ObjectStart),
                                    new TokenData<char>('}', JSonReaderTokenType.ObjectEnd),
                                    new TokenData<char>('[', JSonReaderTokenType.ArrayStart),
                                    new TokenData<char>(']', JSonReaderTokenType.ArrayEnd),
                                    new TokenData<char>(',', JSonReaderTokenType.Comma),
                                    new TokenData<char>(':', JSonReaderTokenType.Colon),
                                    new TokenData<char>('"', JSonReaderTokenType.String),
                                    new TokenData<char>('-', JSonReaderTokenType.Number),
                                };

        // HINT: all definitions should be lowercase!
        private static readonly TokenData<string>[] AvailableKeywords = new TokenData<string>[]
                                {
                                    new TokenData<string>(NullString, JSonReaderTokenType.Keyword, DBNull.Value, new JSonStringObject(null)),
                                    new TokenData<string>(FalseString, JSonReaderTokenType.Keyword, false, new JSonBooleanObject(false)),
                                    new TokenData<string>(TrueString, JSonReaderTokenType.Keyword, true, new JSonBooleanObject(true))
                                };

        private TextReader _input;
        private Stack<JSonReaderTokenInfo> _tokens;

        private int _line;
        private int _offset;
        private char _currentChar;
        private bool _eof;
        private bool _getTokenFromStack;
        private bool _returnJSonObject;

        /// <summary>
        /// Init constructor.
        /// </summary>
        public JSonReader()
        {
        }

        /// <summary>
        /// Returns reader to the original state.
        /// </summary>
        private void Reset(TextReader text, bool returnJSonObject)
        {
            if (text == null)
                throw new ArgumentNullException("text");

            _input = text;
            _tokens = new Stack<JSonReaderTokenInfo>();
            _line = 1;
            _offset = 1;
            _currentChar = '\0';
            _eof = false;
            _getTokenFromStack = false;
            _returnJSonObject = returnJSonObject;
        }

        /// <summary>
        /// Reads from the current input stream all the whitespaces.
        /// </summary>
        private void ReadWhiteChars()
        {
            do
            {
                ReadChar();

                if (!char.IsWhiteSpace(_currentChar))
                    break;
            }
            while (true);
        }

        /// <summary>
        /// Reads next character from the input source.
        /// Automaticaly updates current char, EOF and traced position.
        /// </summary>
        private char ReadChar()
        {
            int data = _input.Read();

            _eof = data == -1;
            
            if (!_eof)
            {
                _currentChar = (char)data;
                if (_currentChar == '\n')
                {
                    _offset = 1;
                    _line++;
                }
                else
                {
                    _offset++;
                }
            }
            else
            {
                _currentChar = '\0';
            }

            return _currentChar;
        }

        /// <summary>
        /// Reads next token from the input source.
        /// </summary>
        private JSonReaderTokenInfo ReadNextToken()
        {
            return ReadNextToken(true);
        }

        /// <summary>
        /// Reads next token from the input source with a possibility to treat already known character as the one read.
        /// Option required mostly for JSON elements that don't have a closing tokens (i.e.: ']' for arrays) and used for number and keywords.
        /// </summary>
        private JSonReaderTokenInfo ReadNextToken(bool fromCurrentChar)
        {
            // token was already read in advanced and put on the stack:
            if (_getTokenFromStack)
            {
                _getTokenFromStack = false;

                // reached end of input stream:
                if (_eof)
                    return new JSonReaderTokenInfo(string.Empty, JSonReaderTokenType.EndOfText, _line, _offset);

                if (_tokens.Count == 0)
                    throw new JSonReaderException ("Lack of tokens", (JSonReaderTokenInfo)null);
                
                return _tokens.Peek();
            }
            
            if (fromCurrentChar)
            {
                // clear the whitespace characters:
                ReadWhiteChars();
            }

            // reached end of input stream:
            if (_eof)
                return new JSonReaderTokenInfo(string.Empty, JSonReaderTokenType.EndOfText, _line, _offset);

            char tokenChar = _currentChar;
            string tokenString = tokenChar.ToString();
            JSonReaderTokenType tokenType = JSonReaderTokenType.Unknown;

            // check if this is one of the already known tokens...
            foreach (var tokenDef in AvailableTokens)
                if (tokenDef.Token == tokenChar)
                {
                    tokenType = tokenDef.Type;
                    break;
                }

            // is this the beginning of a keyword?
            if (tokenType == JSonReaderTokenType.Unknown && char.IsLetter(tokenChar))
                tokenType = JSonReaderTokenType.Keyword;

            // is this the beginning of the number?
            if (tokenType == JSonReaderTokenType.Unknown && (char.IsDigit(tokenChar) || tokenChar == '-'))
                tokenType = JSonReaderTokenType.Number;

            // if this is still unknown element...
            if (tokenType == JSonReaderTokenType.Unknown)
                throw new JSonReaderException("Invalid token found", tokenString, _line, _offset - 1);

            JSonReaderTokenInfo nextToken = new JSonReaderTokenInfo(tokenString, tokenType, _line, _offset - tokenString.Length);

            _tokens.Push(nextToken);

            // return it:
            return nextToken;
        }

        private JSonReaderTokenInfo PopTopToken()
        {
            if (_tokens.Count == 0)
                throw new JSonReaderException("Lack of tokens", (JSonReaderTokenInfo)null);

            return _tokens.Pop();
        }

        #region Object Creations

        private object CreateArray(List<object> data)
        {
            if (_returnJSonObject)
                return new JSonArray(data);

            return data.ToArray();
        }

        private object CreateObject(Dictionary<string, object> data)
        {
            if (_returnJSonObject)
                return new JSonDictionary(data);

            return data;
        }

        private object CreateKeyword(TokenData<string> keyword)
        {
            if (_returnJSonObject)
                return keyword.ValueAsJSonObject;

            return keyword.Value;
        }

        private object CreateString(string data)
        {
            if (_returnJSonObject)
                return new JSonStringObject(data);

            return data;
        }

        private object CreateDecimal<T>(T data) where T : struct, IConvertible
        {
            if (_returnJSonObject)
                return new JSonDecimalObject<T>(data);

            return data;
        }

        #endregion

        /// <summary>
        /// Converts an input string into a dictionary, array, string or number, depending on the JSON string structure.
        /// </summary>
        private object Read()
        {
            // if there is nothing to read:
            if (_input.Peek() == -1)
                return null;

            object result = null;
            JSonReaderTokenInfo currentToken;

            // analize the top level elements,
            // this could be an array, object, keyword, number, string:
            while ((currentToken = ReadNextToken()).Type != JSonReaderTokenType.EndOfText)
            {
                switch(currentToken.Type)
                {
                    case JSonReaderTokenType.ArrayStart:
                        if (result != null)
                            throw new JSonReaderException("Invalid second top level token", currentToken);

                        result = ReadArray();
                        break;

                    case JSonReaderTokenType.ArrayEnd:
                        throw new JSonReaderException("Lack of array opening token", currentToken);

                    case JSonReaderTokenType.ObjectStart:
                        if (result != null)
                            throw new JSonReaderException("Invalid second top level token", currentToken);

                        result = ReadObject();
                        break;

                    case JSonReaderTokenType.ObjectEnd:
                        throw new JSonReaderException("Lack of object opening token", currentToken);

                    case JSonReaderTokenType.Keyword:
                        if (result != null)
                            throw new JSonReaderException("Invalid second top level token", currentToken);

                        result = ReadKeyword();
                        break;

                    case JSonReaderTokenType.String:
                        if (result != null)
                            throw new JSonReaderException("Invalid second top level token", currentToken);

                        result = ReadString();
                        break;

                    case JSonReaderTokenType.Number:
                        if (result != null)
                            throw new JSonReaderException("Invalid second top level token", currentToken);

                        result = ReadNumber();
                        break;

                    default:
                        throw new JSonReaderException("Only one object allowed on top level", currentToken);
                }
            }

            if (_tokens.Count != 0)
                throw new JSonReaderException("Missing JSON tokens to close all item definitions", _tokens.Peek());

            return result;
        }

        /// <summary>
        /// Adds new element to an array.
        /// </summary>
        private static void AddValue(ICollection<object> result, object value, int commas, JSonReaderTokenInfo currentToken)
        {
            if (result.Count != commas)
                throw new JSonReaderException("Missing commas between array objects", currentToken);

            result.Add(value);
        }

        /// <summary>
        /// Read an array from input stream.
        /// </summary>
        private object ReadArray()
        {
            List<object> result = new List<object>();
            JSonReaderTokenInfo currentToken;
            int commas = 0;

            while ((currentToken = ReadNextToken()).Type != JSonReaderTokenType.EndOfText)
            {
                if (currentToken.Type == JSonReaderTokenType.ArrayEnd)
                {
                    JSonReaderTokenInfo prevToken = PopTopToken();

                    // if number of commas is greater than number of added elements,
                    // then value was not passed between:
                    if (commas > 0 && commas >= result.Count)
                        throw new JSonReaderException("Too many commas at closing array token", currentToken);
                    break;
                }

                switch (currentToken.Type)
                {
                    case JSonReaderTokenType.ArrayStart:
                        // read embedded array:
                        AddValue(result, ReadArray(), commas, currentToken);
                        break;

                    case JSonReaderTokenType.ObjectStart:
                        // read embedded object:
                        AddValue(result, ReadObject(), commas, currentToken);
                        break;

                    case JSonReaderTokenType.Keyword:
                        // add embedded value of reserved keyword:
                        AddValue(result, ReadKeyword(), commas, currentToken);
                        break;

                    case JSonReaderTokenType.Number:
                        // add embedded numeric value:
                        AddValue(result, ReadNumber(), commas, currentToken);
                        break;

                    case JSonReaderTokenType.String:
                        // add embedded string value:
                        AddValue(result, ReadString(), commas, currentToken);
                        break;

                    case JSonReaderTokenType.Comma:
                        // go to next array element:
                        currentToken = PopTopToken();
                        commas++;

                        // if number of commas is greater than number of added elements,
                        // then value was not passed between:
                        if (commas > result.Count)
                            throw new JSonReaderException("Missing value for array element", currentToken);
                        break;

                    default:
                        throw new JSonReaderException("Invalid token", currentToken);
                }
            }

            JSonReaderTokenInfo topToken = PopTopToken();
            if (topToken.Type != JSonReaderTokenType.ArrayStart || currentToken.Type == JSonReaderTokenType.EndOfText)
                throw new JSonReaderException("Missing close array token", topToken);

            return CreateArray(result);
        }

        /// <summary>
        /// Adds value to the dictionary.
        /// </summary>
        private static void AddValue(IDictionary<string, object> result, ref string name, object value, ref bool colonSpot, JSonReaderTokenInfo currentToken)
        {
            if (!colonSpot)
                throw new JSonReaderException("Missing colon between name and value definition", currentToken);
            
            if (name == null)
                throw new JSonReaderException("Missing value for object element", currentToken);
            
            if (result.ContainsKey(name))
                throw new JSonReaderException("Duplicated name in object", currentToken);
            
            result.Add(name, value);
            name = null;
            colonSpot = false;
        }

        /// <summary>
        /// Reads a dictonary from an input stream.
        /// </summary>
        private object ReadObject()
        {
            Dictionary<string, object> result = new Dictionary<string, object>();
            JSonReaderTokenInfo currentToken;
            string name = null;
            bool colonSpot = false;
            int commas = 0;

            while ((currentToken = ReadNextToken()).Type != JSonReaderTokenType.EndOfText)
            {
                if (currentToken.Type == JSonReaderTokenType.ObjectEnd)
                {
                    PopTopToken();

                    // if number of commas is greater than number of added elements,
                    // then value was not passed between:
                    if (commas > 0 && commas >= result.Count)
                        throw new JSonReaderException("Missing value for object element", currentToken);
                    break;
                }

                switch (currentToken.Type)
                {
                    case JSonReaderTokenType.ArrayStart:

                        // read embedded array:
                        AddValue(result, ref name, ReadArray(), ref colonSpot, currentToken);
                        break;

                    case JSonReaderTokenType.ObjectStart:

                        // read embedded object:
                        AddValue(result, ref name, ReadObject(), ref colonSpot, currentToken);
                        break;

                    case JSonReaderTokenType.Keyword:

                        // add embedded value of reserved keyword:
                        AddValue(result, ref name, ReadKeyword(), ref colonSpot, currentToken);
                        break;

                    case JSonReaderTokenType.Number:

                        // add embedded numeric value:
                        AddValue(result, ref name, ReadNumber(), ref colonSpot, currentToken);
                        break;

                    case JSonReaderTokenType.String:

                        if (name == null)
                        {
                            name = ReadString().ToString();

                            if (result.ContainsKey(name))
                                throw new JSonReaderException("Duplicated name in object", currentToken);

                            break;
                        }

                        // add embedded string:
                        AddValue(result, ref name, ReadString(), ref colonSpot, currentToken);
                        break;

                    case JSonReaderTokenType.Colon:
                        PopTopToken();

                        if (colonSpot)
                            throw new JSonReaderException("Duplicated colon found in object", currentToken);

                        if (name == null)
                            throw new JSonReaderException("Unexpected colon, when name not given", currentToken);

                        colonSpot = true;
                        break;

                    case JSonReaderTokenType.Comma:
                        // go to next array element:
                        currentToken = PopTopToken();
                        commas++;

                        // if number of commas is greater than number of added elements,
                        // then value was not passed between:
                        if (commas > result.Count)
                            throw new JSonReaderException("Missing value for object element", currentToken);
                        break;

                    default:
                        throw new JSonReaderException("Invalid token", currentToken);
                }
            }

            JSonReaderTokenInfo topToken = PopTopToken();
            if (topToken.Type != JSonReaderTokenType.ObjectStart || currentToken.Type == JSonReaderTokenType.EndOfText)
                throw new JSonReaderException("Missing close object token", topToken);

            return CreateObject(result);
        }

        /// <summary>
        /// Reads a keyword from the input stream.
        /// </summary>
        private object ReadKeyword()
        {
            JSonReaderTokenInfo topToken = PopTopToken();

            // top token contains the first letter of current keyword:
            StringBuilder buffer = new StringBuilder(topToken.Text);

            do
            {
                ReadChar();

                if (char.IsLetter(_currentChar))
                {
                    buffer.Append(_currentChar);
                }
                else
                {
                    // since keyword has no closing token (as arrays or strings), it might
                    // happen that we read too many chars... so put that new char as a token on the
                    // stack and instruct reader that token is already there...
                    ReadNextToken(char.IsWhiteSpace(_currentChar));
                    _getTokenFromStack = true;
                    break;
                }
            }
            while (true);

            string keyword = buffer.ToString().ToLower(CultureInfo.InvariantCulture);

            foreach (var k in AvailableKeywords)
                if (k.Token == keyword)
                    return CreateKeyword(k);

            // token has not been found:
            throw new JSonReaderException("Unknown keyword", keyword, _line, _offset - keyword.Length);
        }

        private void AddUnicodeChar(StringBuilder output, StringBuilder number, bool clearNumber)
        {
            if (number.Length < 4)
                throw new JSonReaderException("Invalid escape definition of Unicode character", _currentChar.ToString(), _line, _offset);

            // Unicode number might have only 4 digits:
            if (number.Length > 4)
                throw new JSonReaderException("Too long Unicode number definition", number.ToString(), _line, _offset - number.Length);

            int charNumber;

            if (!NumericHelper.TryParseHexInt32(number.ToString(), out charNumber))
                throw new JSonReaderException("Invalid value for escaped Unicode character", number.ToString(), _line, _offset - number.Length);

            output.Append(Convert.ToChar(charNumber));
            if (clearNumber)
                number.Remove(0, number.Length);

        }

        private bool ReadStringUnicodeCharacter(StringBuilder output, StringBuilder number)
        {
            if (HexDigits.IndexOf(_currentChar) >= 0)
            {
                number.Append(_currentChar);

                if (number.Length == 4)
                {
                    AddUnicodeChar(output, number, true);
                    return false;
                }

                // continune reading escape definition
                return true;
            }
            else
            {
                AddUnicodeChar(output, number, true);
                return false;
            }
        }

        /// <summary>
        /// Reads the string from input stream.
        /// </summary>
        private object ReadString()
        {
            StringBuilder buffer = new StringBuilder();
            StringBuilder number = new StringBuilder();
            bool escape = false;
            bool unicodeNumber = false;

            do
            {
                ReadChar();

                // verify if not an invalid character was found in text:
                if (_eof)
                    throw new JSonReaderException("Unexpected end of text while reading a string", PopTopToken());
                if (_currentChar == '\r' || _currentChar == '\n')
                    throw new JSonReaderException("Unexpected new line character in the middle of a string", buffer.ToString(), _line, _offset);

                if (unicodeNumber)
                {
                    unicodeNumber = ReadStringUnicodeCharacter(buffer, number);
                    continue;
                }
                
                if (_currentChar == '\\' && !escape)
                {
                    escape = true;
                }
                else
                {
                    if (escape)
                    {
                        switch(_currentChar)
                        {
                            case 'n':
                                buffer.Append('\n');
                                break;
                            case 'r':
                                buffer.Append('\r');
                                break;
                            case 't':
                                buffer.Append('\t');
                                break;
                            case '/':
                                buffer.Append('/');
                                break;
                            case '\\':
                                buffer.Append('\\');
                                break;
                            case 'f':
                                buffer.Append('\f');
                                break;
                            case 'U':
                            case 'u':
                                unicodeNumber = true;
                                break;
                            case '"':
                                buffer.Append('"');
                                break;
                            case '\'':
                                buffer.Append('\'');
                                break;
                            default:
                                throw new JSonReaderException("Unknown escape combination", _currentChar.ToString(), _line, _offset - 2);
                        }

                        escape = false;
                    }
                    else
                    {
                        if (_currentChar == '"')
                            break;
                        
                        buffer.Append(_currentChar);
                    }
                }
            }
            while (true);

            // as the string might finish with a Unicode character...
            if (unicodeNumber)
                AddUnicodeChar(buffer, number, false);

            // remove the beggining of the string token " from the top of the tokens stack
            PopTopToken();

            return CreateString(buffer.ToString());
        }

        /// <summary>
        /// Read number from an input stream.
        /// </summary>
        private object ReadNumber()
        {
            JSonReaderTokenInfo topToken = PopTopToken();

            // top token contains the first letter of current number:
            StringBuilder buffer = new StringBuilder(topToken.Text);

            do
            {
                ReadChar();

                if (char.IsDigit(_currentChar) || _currentChar == '-' || _currentChar == '+' || _currentChar == '.'
                        || _currentChar == 'e' || _currentChar == 'E')
                {
                    buffer.Append(_currentChar);
                }
                else
                {
                    // verify what kind of character is just after the number, if it's a letter
                    // then it is an error:
                    if (!_eof)
                    {
                        if (_currentChar != ',' && !char.IsWhiteSpace(_currentChar) && _currentChar != ']' && _currentChar != '}')
                        {
                            buffer.Append(_currentChar);
                            throw new JSonReaderException("Invalid number", buffer.ToString(), _line, _offset - buffer.Length);
                        }
                    }

                    // since number has no closing token (as arrays or strings), it might
                    // happen that we read too many chars... so put that new char as a token on the
                    // stack and instruct reader that token is already there...
                    ReadNextToken(char.IsWhiteSpace(_currentChar));
                    _getTokenFromStack = true;
                    break;
                }
            }
            while (true);

            string number = buffer.ToString();
            Int64 resultInt64;
            UInt64 resultUInt64;
            double resultDouble;

            // if the number starts with '-' sign, then it might be Int64, also when it fits the range of Int64 values, we prefere Int64 to be used;
            // otherwise try UInt64, finally if both reading failed, assume it's Double:

            if (number.Length > 0 && number.IndexOf('.') == -1)
            {
                if (NumericHelper.TryParseInt64(number, out resultInt64) && string.Compare(resultInt64.ToString(CultureInfo.InvariantCulture), number, StringComparison.Ordinal) == 0)
                    return CreateDecimal(resultInt64);

                if (number[0] != '-' && NumericHelper.TryParseUInt64(number, out resultUInt64) && string.Compare(resultUInt64.ToString(CultureInfo.InvariantCulture), number, StringComparison.Ordinal) == 0)
                    return CreateDecimal(resultUInt64);
            }

            if (NumericHelper.TryParseDouble(number, NumberStyles.Float, out resultDouble))
                return CreateDecimal(resultDouble);

            // number had some invalid format:
            throw new JSonReaderException("Invalid number format", number, _line, _offset - number.Length);
        }

        #region IJSonReader Members

        /// <summary>
        /// Converts a JSON string from given input into a tree of .NET arrays, dictionaries, strings and decimals.
        /// </summary>
        public object Read(TextReader input)
        {
            Reset(input, false);
            return Read();
        }

        /// <summary>
        /// Converts a JSON string from given input into a tree of .NET arrays, dictionaries, strings and decimals.
        /// </summary>
        public object Read(string input)
        {
            Reset(new StringReader(input), false);
            return Read();
        }

        /// <summary>
        /// Converts a JSON string from given input into a tree of JSON-specific objects.
        /// It then allows easier deserialization for objects implementing IJSonReadable interface as those objects exposes
        /// more functionality then the standard .NET ones.
        /// </summary>
        public IJSonObject ReadAsJSonObject(TextReader input)
        {
            Reset(input, true);
            return Read() as IJSonObject;
        }

        /// <summary>
        /// Converts a JSON string from given input into a tree of JSON-specific objects.
        /// It then allows easier deserialization for objects implementing IJSonReadable interface as those objects exposes
        /// more functionality then the standard .NET ones.
        /// </summary>
        public IJSonObject ReadAsJSonObject(string input)
        {
            Reset(new StringReader(input), true);
            return Read() as IJSonObject;
        }

        #endregion
    }
}
