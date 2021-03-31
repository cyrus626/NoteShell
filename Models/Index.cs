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
                    Console.WriteLine("4. Exit the console");
                    Console.Write("Enter an option (1, 2, 3 or 4)> ");
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
                    ViewAllNotes();
                    ViewNoteById();
                    break;
                case 3:
                    ViewAllNotes();
                    ViewNoteByName();
                    break;
                case 4:
                    Console.WriteLine("Console is exited");
                    break;
            }
        }
        
        
        #region
        public void ViewNoteByName()
        {
            Console.Write("Enter the note name> ");
            string noteName = Console.ReadLine();
            //Finding all notes in that with the key word
            foreach (var note in DBManager.GetNotes())
            {
                if (note.NoteName.ToLower() == noteName.ToLower())
                {
                    currentNote = DBManager.GetNoteByName(noteName);
                    ViewCurrentNote(currentNote.Id);
                }

            }
            Console.WriteLine("Error: File not found!");
            NoteShellInitials();
        }
        #endregion
        #region
        public void ViewAllNotes()
        {
            Console.WriteLine("Id" + "  " + "File name" + "     " + "Time saved");
            var allNotes = DBManager.GetNotes();
            int numOfNotes = allNotes.Count;
            foreach (var note in allNotes)
            {
                int eachNoteId = note.Id;
                string noteName = note.NoteName;
                DateTime createdDate = note.TimeSaved;
                Console.WriteLine(eachNoteId + "    " + noteName + "        " + createdDate);
            }
        }
        #endregion
        #region
        public void ViewNoteById()
        {
            //Finding all notes in database
            
            Console.Write("Enter the Id number to select the note> ");
            string userResponse = Console.ReadLine();
            int noteId;
            bool flagNoteId = Int32.TryParse(userResponse, out noteId);
            if (userResponse.ToLower().Equals("b"))
            {
                NoteShellInitials();
            }
            else
            {
                if(flagNoteId == false)
                {
                    Console.Write("Wrong input try again");
                    Console.ReadKey();
                    ViewNoteById();
                }
                else
                {
                    ViewCurrentNote(noteId);
                }
            }
            
            
        }
        #endregion
        #region
        public void ViewCurrentNote(int noteId)
        {
            //Selecting the note
            #region
            foreach (var note in DBManager.GetNotes())
            {
                if (note.Id == noteId)
                {
                    currentNote = DBManager.GetNote(noteId);
                    Console.WriteLine($"{currentNote.NoteName} Last saved {currentNote.TimeSaved} ");
                    Console.Write("E. Edit note content \nD. Delete note \nB. Back to view all notes> ");
                    string reply = Console.ReadLine();
                    if (reply.ToLower().Equals("e"))
                    {
                        //display the content of the note
                        CurrentNote(currentNote.NoteName, noteId);
                        Console.WriteLine("Done :)");
                    }
                    else if (reply.ToLower().Equals("d"))
                    {
                        Console.WriteLine("The note will be deleted, do you wish to continue? (Y/N)> ");
                        string res = Console.ReadLine();
                        if(res.ToLower() == "y")
                        {
                            DBManager.DeleteNote(noteId);
                            Console.Write("Note was deleted \n click enter to continue");
                            Console.Read();
                        }
                        else
                        {
                            ViewCurrentNote(noteId);
                        }
                    }
                    else if (reply.ToLower().Equals("b"))
                    {
                        Console.WriteLine("     OK, Noted");
                        ViewAllNotes();
                        ViewNoteById();
                    }
                    else
                    {
                        Console.WriteLine("     You've choose the wrong option");
                        ViewCurrentNote(currentNote.Id);
                    }
                }
            }
            #endregion
            
            Console.WriteLine("Error: File not found!");
            NoteShellInitials();
        }
        #endregion
        #region
        public void CurrentNote(string noteName, int id)
        {

            Console.WriteLine("Instruction: Enter the content of the note" +
                "\n Press shift + enter to enter the next line \nAnd click enter when through");
            Console.WriteLine($"----------------------{noteName}-Document-------------------------");
            Console.WriteLine($"{currentNote.Content}");
            string newContent = Console.ReadLine();
            string content;
            if ((string)currentNote.Content != "")//If it is not empty.
            {
                string currentContent = (string)currentNote.Content;
                content = (currentContent + "\n" + newContent);//Adding up to the previous contents
            }
            else
            {
                content = newContent;
            }
            Console.Write("     Do you wish to save document?(Y/N)> ");
            string reply = Console.ReadLine();

            if (reply.ToLower() == "y")
            {

                if (id == 0)
                {
                    Console.WriteLine("     New note! click enter to save");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine($"    Saving {noteName}");
                }
                SaveNote(noteName, content, id);
            }
            else
            {
                Console.WriteLine($"    {noteName} Discarded");
            }
            
        }
        #endregion
        #region
        public void SaveNote(string noteName, string content, int id)
        {
            Note saveNote = new Note();
            //if the note's id = 0, a new id will be created.
            if (id == 0)
            {
                Console.WriteLine($"New note {noteName} saved");
            }
            else
            {
                saveNote.Id = id;
            }
            //Saving other segments of the note
            saveNote.NoteName = noteName;
            saveNote.Content = content;
            saveNote.TimeSaved = DateTime.Now;
            DBManager.SaveNote(saveNote);
            Console.WriteLine($"{noteName} saved");
            
        }
        #endregion
        
    }
}
