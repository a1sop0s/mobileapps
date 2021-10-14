using NotesApp.CommandLine.Models;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace NotesApp.CommandLine
{
    class Program
    {
        private static void DisplayNotes(List<Note> n)
        {
            if (n.Count > 0)
            {
                for (int i = 0; i < n.Count; i++)
                {
                    Console.WriteLine($"Index: {i}");
                    Console.WriteLine($"Title: {n[i].Heading}");
                    Console.WriteLine($"Content: {n[i].Content}");
                    Console.WriteLine($"Date: {n[i].ChangeDateTime}");
                    for (int j = 0; j < ("Content: ".Length + n[i].Content.Length + 5); j++) Console.Write("-");
                    Console.WriteLine();
                }
            } else Console.WriteLine("No notes to display!");
        }
        
        static void Main(string[] args)
        {
            var run = true;
            var notes = new List<Note>();
            
            while (run)
            {
                Console.Write(  
                 @"
                1.Display notes
                2.Add note
                3.Edit note
                4.Delete note
                5.Exit
                >  ");
                var answer = int.Parse(Console.ReadLine() ?? "1");
                Console.Clear();

                switch (answer)
                {
                    case 1: // Display
                        Console.WriteLine("Here's your notes:\n");
                        DisplayNotes(notes);
                        break;
                    case 2: // Add
                        Console.WriteLine("Add note\n------------");
                        Console.Write("Title\n> ");
                        var title = Console.ReadLine();
                        Console.WriteLine("---");
                        Console.Write("Content\n> ");
                        var body = Console.ReadLine();

                        var note = new Note(Guid.NewGuid(), title, body, DateTime.Now);
                        notes.Add(note);
                        break;
                    case 3: // Edit
                        DisplayNotes(notes);
                        Console.Write("Please type the index of the note you'd like to change\n> ");
                        var toEdit = int.Parse(Console.ReadLine() ?? "0");
                        
                        Console.Write("Which field would you like to change? (Heading / Content)\n> ");
                        var fieldChoice = Console.ReadLine()?.ToLower();
                        
                        switch (fieldChoice)
                        {
                            case "heading": case "title":
                                Console.Write("What would you like the new value of Heading to be?\n> ");
                                notes[toEdit].Heading = Console.ReadLine();
                                break;
                            case "content": case "body":
                                Console.Write("What would you like the new value of Content to be?\n> ");
                                notes[toEdit].Content = Console.ReadLine();
                                break;
                        }
                        
                        Console.WriteLine($"Successfully edited the note with the index of {toEdit}");
                        break;
                    case 4: // Delete
                        DisplayNotes(notes);
                        Console.Write("Which note would you like to delete?\n> ");
                        var toRemove = int.Parse(Console.ReadLine() ?? "0");
                        Console.WriteLine();
                        
                        notes.RemoveAt(toRemove);
                        Console.WriteLine($"Successfully deleted the note with the index of {toRemove}");
                        break;
                    case 5: // Exit
                        Console.WriteLine("Goodbye.");
                        run = false;
                        break;
                    default:
                        Console.WriteLine();
                        break;
                }
            }            
        }
    }
}
