using System.Collections.Generic;
using System;
using System.IO;
using System.Media;


namespace TonesAndStuff {
    
    static class Program {
        
        public static void Main() {
            ToneGenerator toneGenerator = new ToneGenerator();
            toneGenerator.WriteIt();
        }
    }
    
    // Full credit to this person: https://social.msdn.microsoft.com/profile/johnwein/
    // All I have done is to refactor, mostly in an effort to run it inside coreclr and VsCode. That failed, because
    // ultimately coreclr knows nothing about anything related to System.Media. Neither does dnx451, but at least the latter
    // lets you address an assembly in the GAC, using this in project.json:
    //  "frameworkAssemblies": {
    //          "System.Media": ""
    //      },
    
    
    public class ToneGenerator {
        
        private readonly double _2Pi = 2 * Math.PI;
        private readonly double _amplitude = (Math.Pow(2,15)) - 1;
        private readonly int _duration = 1000;
        private readonly int _samples;
        private readonly int _bytes;
        private readonly List<int> _waveHeaderFileHeader;
        private readonly List<int> _notes = new List<int>{
                131, 139, 147, 156, 165, 175, 185, 196, 208, 220, 233, 247,
                262, 277, 294, 311, 330, 349, 370, 392, 415, 440, 466, 494 
        };
        
        public ToneGenerator() {
            System.Console.WriteLine(_amplitude);
            _samples = 441 * _duration / 10;
            _bytes = _samples * 4;
            _waveHeaderFileHeader = new List<int>{0X46464952, 36 + _bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, _bytes};
            
        }

        public void WriteIt() {
            
             double DeltaFT;
             GetRandomInterval();
             foreach (var frequency in _notes) {
            
                DeltaFT = 2 * Math.PI * frequency / 44100.0;
                
                using (MemoryStream mStream = new MemoryStream(44 + _bytes)) {
                    using (BinaryWriter bWriter = new BinaryWriter(mStream)) {
                        foreach (var headerElement in _waveHeaderFileHeader) {
                            bWriter.Write(headerElement);
                        }
                        for (int t = 0; t < _samples; t++) {
                            var sample = System.Convert.ToInt16(_amplitude * Math.Sin(DeltaFT * t));
                            bWriter.Write(sample);
                            bWriter.Write(sample);
                        } 
                        bWriter.Flush();
                        mStream.Seek(0, SeekOrigin.Begin);
                        using (SoundPlayer ss = new SoundPlayer(mStream)) {
                            ss.PlaySync();
                        }
                    }
                }
            }
        }
        /// <summary>
        ///  Today: 
        ///  return/(well, print) a random integer between 1 and the length of the notes array (2 octaves)
        ///  Soon:
        ///  Return a pair of random frequences in notes array, where the first is always lower than
        ///  the second, and there is not more than an octave between the notes inclusive in any 
        /// returned pair.
        /// </summary>
        public Dictionary<string,string> GetRandomInterval() {
            var xx = new Random().Next(1,_notes.Count);
            System.Console.WriteLine(_notes[0]);
            System.Console.WriteLine(xx);
            var x = new Dictionary<string,string>();
            x.Add("MyKey","adfadMyVafaf");
            return x;

        }
    }
}