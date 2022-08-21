using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clox
{
    public class Token
    {
        public readonly TokenType type;
        public readonly String lexeme;
        public readonly Object literal;
        public readonly int line; // the location
        public Token(TokenType type, String lexeme, Object literal, int line)

        {
            this.type = type;
            this.lexeme = lexeme;
            this.literal = literal;
            this.line = line;
        }

        public String toString()
        {
            if (this.literal != null )
            {
                return type.ToString() + "  lexme: " + lexeme.ToString() + "  literal: " + literal.ToString();

            }
            else
            {
                return type.ToString() + "  lexme: " + lexeme.ToString() + "  literal: null"  ;
            }
            // or we can write as
            //return type + "" + lexeme + "" + literal;
        }


    }
}
