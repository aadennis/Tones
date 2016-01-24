using Xunit;
using System;

namespace Model.Tests {
    
    public class FrequencyTests : ModelTestBase {
        
        [Fact] 
        public void FrequencyDictionaryHas2OctavesOfNotes() {
            TraceExecutingMethod();
            var frequencyCount  = new Model.ToneUtility().GetAllNotes().Count;
            Assert.Equal((12*2), frequencyCount);
        }   
        
        [Fact] 
        public void RequestForFrequencyC3returnsExpectedElements() {
            TraceExecutingMethod();
            var toneSet = new Model.ToneUtility();
            Assert.Equal("C.3",toneSet.GetNoteElements(131));
        }        
        
        [Fact]
        public void RequestForNotFoundFrequencyThrowsException() {
            TraceExecutingMethod();
            var toneSet = new Model.ToneUtility();
            Exception ex = Assert.Throws<ArgumentException>(() => toneSet.GetNoteElements(9999));
            Assert.Equal("[The frequency [9999] was not found in the set of available notes]", ex.Message);
        }
    }
}
