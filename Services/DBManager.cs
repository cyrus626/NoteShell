using System;
using LiteDB;
using NoteShell.Models;
using System.Collections.Generic;
using System.Linq;

namespace NoteShell.Services
{
	public class DBManager
	{
		//Connecting to LiteDb
		static string DBPath { get; set; } = "NoteShell.db";
		static LiteDatabase NoteDB { get; } = new LiteDatabase(DBPath);
		
		public static void SaveNote(Note newNote)
        {
			var collection = NoteDB.GetCollection<Note>(nameof(Note));
			collection.Upsert(newNote);
		}
		public static Note GetNote(int noteId)
        {
			var collection = NoteDB.GetCollection<Note>(nameof(Note));
			return collection.FindOne(note => note.Id == noteId );
        }
		public static Note GetNoteByName(string noteName)
        {
			var collection = NoteDB.GetCollection<Note>(nameof(Note));
			return collection.FindOne(note => note.NoteName == noteName);
		}
		public static void DeleteNote(int noteId)
        {
			var collection = NoteDB.GetCollection<Note>(nameof(Note));
			collection.Delete(noteId);
        }
		public static List<Note> GetNotes()
        {
			var collection = NoteDB.GetCollection<Note>(nameof(Note));
			return collection.FindAll().ToList();
		}
	}
}
