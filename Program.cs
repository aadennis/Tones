using System.Collections.Generic;
using System;
using System.IO;
using System.Media;
using System.Linq;


namespace TonesAndStuff {
    
    static class Program {
        
        public static void Main() {
            ToneGenerator toneGenerator = new ToneGenerator();
            toneGenerator.SingAllAvailableNotes();
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
        private readonly Dictionary<int, string> _notes = new Dictionary<int, string>() {
            {131,"C.3"}, {139,"C#.3"}, {147,"D.3"}, {156,"D#.3"}, {165,"E.3"}, {175,"F.3"}, 
            {185,"F#.3"}, {196,"G.3"}, {208,"G#.3"}, {220,"A.3"}, {233,"A#.3"}, {247,"B.3"},
            {262,"C.4"}, {277,"C#.4"}, {294,"D.4"}, {311,"D#.4"}, {330,"E.4"}, {349,"F.4"}, 
            {370,"F#.4"}, {392,"G.4"}, {415,"G#.4"}, {440,"A.4"}, {466,"A#.4"}, {494,"B.4"} 
        };
        
        public ToneGenerator() {
            System.Console.WriteLine(_amplitude);
            _samples = 441 * _duration / 10;
            _bytes = _samples * 4;
            _waveHeaderFileHeader = new List<int>{0X46464952, 36 + _bytes, 0X45564157, 0X20746D66, 16, 0X20001, 44100, 176400, 0X100004, 0X61746164, _bytes};
            
        }

        public void SingAllAvailableNotes() {
            
             double DeltaFT;
             GetRandomInterval();
             int frequency;
             string noteName;
             foreach (var note in _notes) {
                frequency = note.Key;
                noteName = note.Value;
                System.Console.WriteLine("[{0}][{1}]", frequency, noteName);
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
            var upperNoteLimit = _notes.Keys.Count;
            var rand = new Random();
            var randomNoteIndex1 = rand.Next(1,upperNoteLimit);
            var notes = new List<int>{rand.Next(1,upperNoteLimit)};
            notes.Add(rand.Next(1,upperNoteLimit));
          
            notes.Sort();
            
            System.Console.WriteLine("Todays interval is [{0},{1}]", notes[0], notes[1]);
            System.Console.WriteLine((_notes.Where(x2 => x2.Key.Equals(131))).Single());
            var x = new Dictionary<string,string>();
            x.Add("MyKey","adfadMyVafaf");
            return x;

        }
    }
}