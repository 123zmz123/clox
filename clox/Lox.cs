using System;
using System.IO;
using System.Text;

namespace clox
{
    class Lox
    {
        //private static Intepreter interpreter = new Interpreter()
        static bool hadError = false;
        static bool hadRuntimeError = false;
        public static void Main(string[] args)
        {
            try
            {
                test_it();
                // if (args.Length > 1)
                // {
                //     Console.WriteLine("Usage: clox [script]");
                //     Environment.Exit(8964);
                // }
                // else if (args.Length == 1)
                // {
                //     runFile(args[0]);
                // }
                // else // if run without any parameters
                // {
                //     runPrompt();
                // }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private static void runFile(string path)
        {
            try
            {
                byte[] bytes = File.ReadAllBytes(path);// maybe we need add more
                run(Encoding.UTF8.GetString(bytes, 0, bytes.Length));
            }
            catch (Exception)
            {

                throw;
            }
        }

        private static void runPrompt()
        {
            try
            {
                while(true)
                {
                    Console.WriteLine("> ");
                    var line = Console.ReadLine();
                    if (line == null) break;
                    Console.WriteLine("line get it\n"); // just remind 
                    run(line);

                }
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        private static void run(string src)
        {
            //TODO need implement the scan entry
        }
        
       public static void error(int line, string message)
        {
            report(line, "", message);
        }

        static void report(int line, string where, string message)
        {
            Console.WriteLine("Error: [" + line + "] Error" + where + ":" + message);
            Environment.Exit(64);
        }

        static void error()
        {
            // TODO Token stuff and report the error of it
        }

        static void runtimeError()
        {
            // TODO runtime Error
        }

        static void test_it()
        {
            var str_tst = "\"hello , world!\"33.461 print" ;
            var scan = new Scanner(str_tst);
            scan.scanTokens();

            foreach (var tk in scan.tokens)
            {
                Console.WriteLine(tk.toString());
            }

            while (true)
            {
            }
            
        }
    }
}
