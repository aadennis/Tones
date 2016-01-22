using Xunit;
using Model;
using System;

namespace Model.Tests {
    
    public class NumberUtilityTests {
        
        Model.NumberUtilities util;
        
        public NumberUtilityTests() {
            TestInitialize();
        }
        
        [Fact]
        public void UpperAndLowerNumbersAreNeverTheSame() {
            Assert.Equal(1,2);
        }

        private void TestInitialize() {
          util = new Model.NumberUtilities();
        }
    }
}
