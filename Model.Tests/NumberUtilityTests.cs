using Xunit;
using System;
namespace Model.Tests {
    
    public class NumberUtilityTests : ModelTestBase {

        const int MaxIterationsToTestForError = 1000;
        
        Model.NumberUtilities util;
        Random rand;
        
        public NumberUtilityTests() {
            TestInitialize();
        }
        
        [Fact]
        public void UpperAndLowerNumbersAreNeverTheSame() {
            TraceExecutingMethod();

           for (int i = 0; i < MaxIterationsToTestForError; i++) {
               var randomBoundaries = util.GetRandomInterval(1,30,7,rand);
               Assert.NotEqual(randomBoundaries[0],randomBoundaries[1]);
           }
        }
        
        [Fact]
        public void UpperAndLowerNumbersAreAlwaysWithinRequestedDistance() {
           const int maxDistance = 7;
           
           TraceExecutingMethod();
           for (int i = 0; i < MaxIterationsToTestForError; i++) {
               var randomBoundaries = util.GetRandomInterval(1,30,7,rand);
            //    System.Console.WriteLine(randomBoundaries[0]);
            //    System.Console.WriteLine(randomBoundaries[1]);
            //    System.Console.WriteLine("-----------------");
               Assert.True(maxDistance >= (randomBoundaries[1] - randomBoundaries[0]));
           }
        }
        
        [Fact]
        public void ReturnInputIfRequestedUpperAndLowerLimitsMatch() {
            var upperLimit = 7;
            var lowerLimit = upperLimit;
            
            TraceExecutingMethod();
            var randomBoundaries = util.GetRandomInterval(lowerLimit,upperLimit,70000,rand);
            Assert.Equal(randomBoundaries[0],randomBoundaries[1]);
        }
        
        [Fact]
        public void FrequencyC3ReturnsExpectedElements() {
            
        }
        
        private void TestInitialize() {
          util = new Model.NumberUtilities();
          rand = new Random();
        }
    }
}

// Example execution:
//     PS C:\temp\ps\Tones\Tones> dnx -p .\Model.Tests\project.json test
// xUnit.net DNX Runner (64-bit DNXCore 5.0)
//   Discovering: Model.Tests
//   Discovered:  Model.Tests
//   Starting:    Model.Tests
//   Finished:    Model.Tests
// === TEST EXECUTION SUMMARY ===
//    Model.Tests  Total: 3, Errors: 0, Failed: 0, Skipped: 0, Time: 0.164s