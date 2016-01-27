using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model.Tests;

namespace Model.Test {
    
    [TestClass]
    public class FrequencyTests : ModelTestBase {
        
        [TestMethod] 
        public void FrequencyDictionaryHas2OctavesOfNotes() {
            TraceExecutingMethod();
            var frequencyCount  = new ToneUtility().GetAllNotes().Count;
            Assert.AreEqual((12*2), frequencyCount);
        }   
        
        [TestMethod] 
        public void RequestForFrequencyC3ReturnsExpectedElements() {
            TraceExecutingMethod();
            var toneSet = new ToneUtility();
            Assert.AreEqual("C.3",toneSet.GetNoteElements(131));
        }

        [TestMethod]
        [ExpectedException(typeof (ArgumentException))]
        public void RequestForNotFoundFrequencyThrowsException()
        {


            TraceExecutingMethod();
            var toneSet = new ToneUtility();

            try {
                toneSet.GetNoteElements(9999);
            }
            catch (ArgumentException ex) {
                Assert.AreEqual("[The frequency [9999] was not found in the set of available notes]", ex.Message);
                throw;
            }
            
        }
    }
}
