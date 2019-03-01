using System;
namespace Clouder.Server.Service.Cadl
{
    public static class CodeTemplates
    {
        public static string CodeWithOutput = @"
// - Will generate a method called '#method'
// - Method body (CADL or JS) can be edited later in a text editor
// - Output will be placed into #output 
// - Input arguments include #inputs
// - (see #cadl-help-syntax for more information on CADL syntax)
#output = code(#method, #inputs)
";
        public static string CodeWithoutOutput = @"
// - Will generate a method called '#method'
// - Method body (CADL or JS) can be edited later in a text editor
// - Input arguments include #inputs
// - (see #cadl-help-syntax for more information on CADL syntax)
code(#method, #inputs)
";
        public static string Enqueue = @"
// - Enqueues #variable into queue '#queue'
// - (see #cadl-help-syntax for more information on CADL syntax)
enqueue(#queue, #variable)
";
        public static string Iterate = "list.iterate((#variable) =>\n{";
        public static string If = "if (#condition)\n{";
        public static string Else = "}\nelse\n{";
        public static string End = "}";
        public static string Return = "return #expression";
        public static string Variable = "var #variableName=#definition";
    }
}
