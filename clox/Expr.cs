using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace clox
{
    interface IVisitor<R>
    {
        R visitAssignExpr(Assign expr);
        R visitBinaryExpr(Binary expr);
        R visitCallExpr(Call expr);
        R visitGetExpr(Get expr);

        R visitGroupingExpr(Grouping expr);

        R visitLiteralExpr(Literal expr);
        R visitLogicalExpr(Logical expr);
        R visitSetExpr(Set expr);
        R visitSuperExpr(Super expr);
        R visitThisExpr(This expr);
        R visitUnaryExpr(Unary expr);
        R visitVariableExpr(Variable expr);
        R visitBlockStmt(Block stmt);
        R visitClassStmt(Class stmt);
        R visitExpressionStmt(Expression stmt);
        R visitFunctionStmt(Function stmt);
        R visitIfStmt(If stmt);
        R visitPrintStmt(Print stmt);
        R visitReturnStmt(Return stmt);
        R visitVarStmt(Var stmt);
        R visitWhileStmt(While stmt);


    }
    public class Expr
    {
    }

    public class Assign : Expr
    {
        public readonly Token name;
        public readonly Expr value;
        Assign(Token name, Expr value)
        {
            this.name = name;
            this.value = value;
        }

       R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitAssignExpr(this);
        }
    }

    public class Binary : Expr
    {
        public readonly Expr left;
        public readonly Token op;
        public readonly Expr right;

        Binary(Expr left, Token op, Expr right)
        {
            this.left = left;
            this.op = op;
            this.right = right;
        }

        R accept<R>(IVisitor<R> visitor)
        {
           return visitor.visitBinaryExpr(this);
        }
    }

    public class Call: Expr
    {
        public Expr callee;
        public Token paren;
        public List<Expr> arguments;

        Call(Expr callee, Token paren, List<Expr> arguments)
        {
            this.callee = callee;
            this.paren = paren;
            this.arguments = arguments;
        }

        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitCallExpr(this);
        }
    }

    public class Get: Expr
    {
        public readonly Expr obj;
        public readonly Token name;

        Get(Expr obj, Token name)
        {
            this.obj = obj;
            this.name = name;
        }

        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitGetExpr(this);
        }
    }

    public class Grouping: Expr
    {
        public readonly Expr expression;
        Grouping(Expr expression)
        {
            this.expression = expression;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitGroupingExpr(this);
        }
    }

    public class Literal : Expr
    {
        public readonly Object value;

        Literal(Object value)
        {
            this.value = value;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitLiteralExpr(this);
        }
    }

    public class Logical: Expr
    {
        public readonly Expr left;
        public readonly Token operate;
        public readonly Expr right;

        Logical(Expr left, Token operate, Expr right)
        {
            this.left = left;
            this.operate = operate;
            this.right = right;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitLogicalExpr(this);
        }
    }

    public class Set: Expr
    {
        public readonly Expr obj;
        public readonly Token name;
        public readonly Expr value;

        Set(Expr obj, Token name, Expr value)
        {
            this.obj = obj;
            this.name = name;
            this.value = value;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitSetExpr(this);
        }
    }

    public class Super: Expr
    {
        public readonly Token keyword;
        public readonly Token method;

        Super(Token keyword, Token method)
        {
            this.keyword = keyword;
            this.method = method;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitSuperExpr(this);
        }
    }

    public class This : Expr
    {
        public readonly Token keyword;
        
        This(Token keyword)
        {
            this.keyword = keyword;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitThisExpr(this);
        }
    }

    public class Unary: Expr
    {
        public readonly Token operation;
        public readonly Expr right;

        Unary(Token operation, Expr right)
        {
            this.operation = operation;
            this.right = right;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitUnaryExpr(this);
        }
    }

    public class Variable: Expr
    {
        public readonly Token name;

        Variable(Token name)
        {
            this.name = name;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitVariableExpr(this);
        }
    }

    class Stmt
    {
    }

    class Block : Stmt
    {
        public readonly List<Stmt> statements;

        Block(List<Stmt> statements)
        {
            this.statements = statements;
        }

    }

    class Class: Stmt
    {
        public readonly Token name;
        public readonly Variable superclass;
        public readonly List<Function> methods;

        Class(Token name, Variable superclass, List<Function> methods)
        {
            this.name = name;
            this.superclass = superclass;
            this.methods = methods;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitClassStmt(this);
        }
    }

    class Expression: Stmt
    {
        public readonly Expr expression;
        Expression(Expr expression)
        {
            this.expression = expression;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitExpressionStmt(this);
        }
    }

    class Function: Stmt
    {
        public readonly Token name;
        public readonly List<Token> parameters;
        public readonly List<Stmt> body;

        Function(Token name, List<Token> parameters, List<Stmt> body)
        {
            this.name = name;
            this.parameters= parameters;
            this.body = body;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitFunctionStmt(this);
        }
    }

    class If : Stmt
    {
        public readonly Expr condition;
        public readonly Stmt thenBranch;
        public readonly Stmt elseBranch;

        If(Expr condition, Stmt thenBranch, Stmt elseBranch)
        {
            this.condition = condition;
            this.thenBranch = thenBranch;
            this.elseBranch = elseBranch;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitIfStmt(this);
        }
    }

    class Print: Stmt
    {
        public readonly Expr expression;
        Print(Expr expression)
        {
            this.expression = expression;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitPrintStmt(this);
        }
    }

    class Return: Stmt
    {
        public readonly Token keyword;
        public readonly Expr value;

        Return(Token keyword, Expr value)
        {
            this.keyword = keyword;
            this.value = value;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitReturnStmt(this);
        }
    }

    class Var : Stmt
    {
        public readonly Token name;
        public readonly Expr initializer;

        Var(Token name, Expr initializer)
        {
            this.name = name;
            this.initializer = initializer;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitVarStmt(this);
        }

    }

    class While : Stmt
    {
        public readonly Expr condition;
        public readonly Stmt body;

        While(Expr condition, Stmt body)
        {
            this.condition = condition;
            this.body = body;
        }
        R accept<R>(IVisitor<R> visitor)
        {
            return visitor.visitWhileStmt(this);
        }
    }
}
