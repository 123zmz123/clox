using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;


namespace clox
{
   public class Scanner
    {
        private readonly String source;
        private int current = 0;
        private int start = 0;
        private int line = 1;
        public List<Token> tokens = new List<Token>();
        private Dictionary<string, TokenType> keywords = new Dictionary<string, TokenType>() {
            {"and",   TokenType.AND },
            {"class", TokenType.CLASS},
            {"else",  TokenType.ELSE },
            {"false", TokenType.FALSE },
            {"for",   TokenType.FOR },
            {"fun",   TokenType.FUN },
            {"if",    TokenType.IF },
            {"nil",   TokenType.NIL},
            {"or",    TokenType.OR },
            {"print", TokenType.PRINT },
            {"return",TokenType.RETURN },
            {"super", TokenType.SUPER },
            {"this",  TokenType.THIS },
            {"true",  TokenType.TRUE },
            {"var" ,  TokenType.VAR},
            {"while", TokenType.WHILE },
        };
        public Scanner(String source)
        {
            this.source = source; // or this is the text
        }
        // return the current character and pos++
        private char advance()
        {
            return this.source[current++];
        }

        private void addToken(TokenType type)
        {
            addToken(type, null);
        }

        private void addToken(TokenType type, Object literal)
        {
            String text = source.Substring(start,current-start);
            tokens.Add(new Token(type, text, literal, line));
        }

        // if current >= length then it was at the end of the src file
        private bool isAtEnd()
        {
            return current >= source.Length;
        }
        // get the current charecter
        private char peek()
        {
            if (isAtEnd()) return '\0';
            return source[current];
        }

        private char peekNext()
        {
            if (current + 1 >= source.Length) return '\0';
            return source[current + 1];
        }

        private bool match(char expected)
        {
            if (isAtEnd()) return false;
            if (source[current] != expected) return false;
            current++;
            return true;
        }

        private void get_string()
        {
            while (peek() != '"' && !isAtEnd())
            {
                if (peek() == '\n') line++;
                advance();
            }

            if (isAtEnd())
            {
                Lox.error(line, "Unterminated string");
                return; // this will never come here
            }

            advance(); // skip the end of string , the character were "

            var value = source.Substring(start + 1, current - 1 - start - 1);
            addToken(TokenType.STRING, value);

        }

        private bool isDigit(char c)
        {
            return c >= '0' && c <= '9';
        }

        private void number()
        {
            while (isDigit(peek())) advance();

            if(peek()=='.' && isDigit(peekNext()))
            {
                advance();
                while (isDigit(peek())) advance();
            }
            var num_str = source.Substring(start, current - start);
            addToken(TokenType.NUMBER, Double.Parse(num_str));
        }

        private void identifier()
        {
            while (isAlphaNumeric(peek())) advance();
            var text = source.Substring(start, current - start);

            if (keywords.ContainsKey(text))
            {
                var type = keywords[text];
                addToken(type);
            }
            else 
            {
                addToken(TokenType.IDENTIFIER);
            }
        }

        private bool isAlpha(char c)
        {
            return (c >= 'a' && c <= 'z') ||
                   (c >= 'A' && c <= 'Z') ||
                    c == '_';
        }

        private bool isAlphaNumeric(char c)
        {
            return isAlpha(c) || isDigit(c);
        }
        public List<Token> scanTokens()
        {
            while (!isAtEnd())
            {
                start = current;
                scanToken();
            }
            tokens.Add(new Token(TokenType.EOF, "", null, line));
            return tokens;
        }

        // this is the entry for scanner
        private void scanToken()
        {
            char c = advance();
            switch (c)
            {
                case '(': 
                    addToken(TokenType.LEFT_PAREN); 
                    break;

                case ')':
                    addToken(TokenType.RIGHT_PAREN);
                    break;

                case '{':
                    addToken(TokenType.LEFT_BRACE);
                    break;

                case '}':
                    addToken(TokenType.RIGHT_BRACE);
                    break;

                case ',':
                    addToken(TokenType.COMMA);
                    break;

                case '-':
                    addToken(TokenType.MINUS);
                    break;

                case '+':
                    addToken(TokenType.PLUS);
                    break;

                case ';':
                    addToken(TokenType.SEMICOLON);
                    break;

                case '*':
                    addToken(TokenType.STAR);
                    break;

                case '!':
                    addToken(match('=')? TokenType.BANG_EQUAL:TokenType.BANG);
                    break;

                case '=':
                    addToken(match('=')? TokenType.EQUAL_EQUAL:TokenType.EQUAL);
                    break;

                case '<':
                    addToken(match('=')? TokenType.LESS_EQUAL:TokenType.EQUAL);
                    break;

                case '>':
                    addToken(match('=')? TokenType.GREATER_EQUAL:TokenType.GREATER);
                    break;

                case '/':
                    if (match('/')) 
                    {
                        while (peek() != '\n' && !isAtEnd()) advance();
                    }
                    else
                    {
                        addToken(TokenType.SLASH);
                    }
                    break;

                case ' ':
                case '\r':
                case '\t':
                    break;
                case '\n':
                    line++;
                    break;
                case '"':
                    get_string();
                    break;
                default:
                    if (isDigit(c))
                    {
                        number();
                    }
                    else if (isAlpha(c))
                    {
                        identifier();
                    }
                    else
                    {
                        Lox.error(line, "unexpected character.");
                    }
                    break;

            }
        }
    

    }
}
