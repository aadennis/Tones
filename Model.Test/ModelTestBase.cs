using System;
using System.Runtime.CompilerServices;

namespace Model.Tests {
    
    public class ModelTestBase {
        
        //available from .Net 4.5...
        protected void TraceExecutingMethod([CallerMemberName] string caller = null) {
            Console.ForegroundColor = ConsoleColor.Yellow;
            System.Console.WriteLine("[{0:H:mm:ss.fff}]: Executing test [{1}]", DateTime.UtcNow, caller);
        }
    }
}