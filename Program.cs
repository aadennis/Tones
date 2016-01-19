using System.Collections.Generic;
using System;
using System.IO;
using System.Media;

namespace TonesAndStuff {
    
    static class Program {
        
        public static void Main() {
            //var x = new SomeOther();
            //x.WriteIt(131);
             var notes = new List<int>{
                60, 131, 139, 147, 156, 165, 175, 185, 196, 208, 220, 23, 247,
                262, 277, 294, 311, 330, 349, 370, 392, 415, 440, 466, 494 };
            foreach (var note in notes) {
                Beep.BeepBeep(1000, note, 1000);
            }
        }
    }
    
    // Full credit to this person: https://social.msdn.microsoft.com/profile/johnwein/
    // All I have done is to refactor (well, it will be), mostly in an effort to run it inside coreclr and VsCode. That failed, because
    // ultimately coreclr knows nothing about anything related to System.Media. Neither does dnx451, but at least the latter
    // lets you address an assembly in the GAC, using this in project.json:
    //  "frameworkAssemblies": {
    //          "System.Media": ""
    //      },
    public class Beep {
        public static void BeepBeep(int Amplitude, int Frequency, int Duration) {
            double A = ((Amplitude * (Math.Pow(2, 15))) / 1000) - 1;
            double DeltaFT = 2 * Math.PI * Frequency / 44100.0;
 
            int Samples = 441 * Duration / 10;
            int Bytes = Samples * 4;
            int[] Hdr = { 0X46464952, 36 + Bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, Bytes };
            using (MemoryStream MS = new MemoryStream(44 + Bytes)) {
                using (BinaryWriter BW = new BinaryWriter(MS)) {
                    for (int I = 0; I < Hdr.Length; I++) {
                        BW.Write(Hdr[I]);
                    }
                    for (int T = 0; T < Samples; T++) {
                        short Sample = System.Convert.ToInt16(A * Math.Sin(DeltaFT * T));
                        BW.Write(Sample);
                        BW.Write(Sample);
                    }
                    BW.Flush();
                    MS.Seek(0, SeekOrigin.Begin);
                    using (SoundPlayer SP = new SoundPlayer(MS)) {
                        SP.PlaySync();
                    }
                }
            }
        }
    }
    
    public class ToneGenerator {
        
        private readonly double _2Pi = 2 * Math.PI;
        private readonly double _amplitude = Math.Pow(2,15);
        private readonly int _duration = 1000;
        private readonly int _samples;
        private readonly int _bytes;
        private readonly List<int> _waveHeaderFileHeader;
        
        public ToneGenerator() {
            _samples = 441 * _duration / 10;
            _bytes = _samples * 4;
            _waveHeaderFileHeader = new List<int> {0X46464952, 36 + _bytes, 0X45564157, 0X20746D66, 16, 
                0X20001, 44100, 176400, 0X100004, 0X61746164, _bytes };
        }
        
        List<int> _notes = new List<int>{
                131, 139, 147, 156, 165, 175, 185, 196, 208, 220, 23, 247,
                262, 277, 294, 311, 330, 349, 370, 392, 415, 440, 466, 494 
        };
        
        public void WriteIt(int frequency) {
             double DeltaFT = _2Pi * frequency / 44100.0;
             
             using (var mStream = new MemoryStream(44 + _bytes)) {
                 using (var bWriter = new BinaryWriter(mStream)) {
                     foreach (var headerElement in _waveHeaderFileHeader) {
                         bWriter.Write(headerElement);
                     }
                     for (int t = 0; t < _samples; t++) {
                         var sample = System.Convert.ToInt64(_amplitude * Math.Sin(DeltaFT * t));
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
}