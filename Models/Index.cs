using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NoteShell.Models;
using NoteShell.Services;
namespace NoteShell.Models.Index
{
    class Index
    {
        public Note currentNote = new Note();
        public void NoteShellInitials()
        {
            
            int response = 0;
            bool flagRes = false;
            do
            {
                Console.Clear();
                try
                {
                    Console.WriteLine("Welcome to NoteShell");
                    Console.WriteLine("1. Create Note");
                    Console.WriteLine("2. View Notes");
                    Console.WriteLine("3. View a Note");
                    Console.Write("Enter an option (1, 2 0r 3)> ");
                    flagRes = Int32.TryParse(Console.ReadLine(), out response);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            } while (flagRes == false);

            switch (response)
            {
                case 1:
                    Console.Write("Enter the note name> ");
                    string noteName = Console.ReadLine().Trim();
                    int id = 0;
                    CurrentNote(noteName, id);
                    break;
                case 2:
                    ViewNotes();
                    break;
                case 3: 
                    ViewNote();
                    break;
            }
        }
        #region
        public void CurrentNote(string noteName, int id)
        {
            
            Console.WriteLine("Instruction: Enter the content of the note" +
                "\n Press shift + enter to enter the next line \nAnd click enter when through");
            Console.WriteLine($"----------------------{noteName}-Document-------------------------");
            Console.WriteLine(currentNote.Content);
            string newContent = Console.ReadLine();
            string currentContent = (string)currentNote.Content;
            string content = (newContent + "\n" + currentContent);
            Console.Write("Do you wish to save document?(Y/N)> ");
            string reply = Console.ReadLine();
            
            if (reply.ToLower() == "y")
            {
                
                if (id == 0)
                {
                    Console.WriteLine("New note! click enter to save");
                }
                else
                {
                    Console.WriteLine($"Saving {noteName}");
                }
                Console.WriteLine($"{noteName} Saved");
                SaveNote(noteName, content, id);
            }
            else
            {
                Console.WriteLine($"{noteName} Discarded");
            }
        }
        #endregion
        public void SaveNote(string noteName, string content ,int id)
        {
            Note saveNote = new Note();
            //if the note's id = 0, a new id will be created.
            if(id == 0)
            {
                Console.WriteLine($"New note {noteName} saved");
            }
            else
            {
                saveNote.Id = id;
            }
            saveNote.NoteName = noteName;
            saveNote.Content = content;
            saveNote.TimeSaved = DateTime.Now;
            DBManager.SaveNote(saveNote);
            Console.WriteLine($"{noteName} saved");
        }
        public void ViewNote()
        {
            Console.WriteLine("Execise patient, Working in progress...");
        }
        public void ViewNotes()
        {
            //Finding all notes in database
            Console.WriteLine("Id" + " " + "File name");
            foreach (var note in DBManager.GetNotes() )
            {
                int noteId = note.Id;
                string noteName = note.NoteName;
                Console.WriteLine(noteId + " " + noteName);
            }
            Console.WriteLine("Enter the Id to select the note"); 
            ViewCurrentNote();
        }
        public void ViewCurrentNote()
        {
            //Selecting the note
            int noteId = Convert.ToInt32(Console.ReadLine());
            currentNote = DBManager.GetNote(noteId);
            Console.WriteLine("E. Edit note content \nD. Delete note \nB. Back to view all notes");
            Console.Write("Option avialable is E > ");
            string reply = Console.ReadLine();
            if (reply.ToLower().Equals("e"))
            {
                //display the content of the note
                
                CurrentNote(currentNote.NoteName, noteId);
            }
            else if (reply.ToLower().Equals("d"))
            {
                Console.WriteLine("Execise patient, Working in progress...");
            }
            else if (reply.ToLower().Equals("b"))
            {
                Console.WriteLine("Execise patient, Working in progress...");
            }
            else
            {
                Console.WriteLine("You've choose the wrong option");
                Console.WriteLine("Execise patient, Working in progress...");
            }
        }
    }
}
