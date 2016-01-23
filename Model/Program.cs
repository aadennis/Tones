namespace Model {

    class Program {
        public static void Main() {
            ToneGenerator toneGenerator = new ToneGenerator();
            toneGenerator.PlayAllNotes();
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

        public void PlayAllNotes() {
             var toneUtility = new Model.ToneUtility();
             var notes = toneUtility.GetAllNotes();
             foreach (var note in notes) {
                 toneUtility.PlayNote(note.Key, note.Value);
             }
        }
    }
}
