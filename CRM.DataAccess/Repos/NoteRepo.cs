using CRM.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.DataAccess.Repos
{
    public class NoteRepo
    {
        public static Note Save(Note note)
        {
            note.Created = DateTime.Now;

            try
            {
                using (var db = new Context())
                {
                    db.Notes.Add(note);
                    var result = db.SaveChanges();

                    db.Entry(note).Reference(n => n.Account).Load();

                    return result > 0 ? note : new Note();
                }
            }
            catch (Exception ex)
            {
                return new Note();
            }
        }

        public static Note Get(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Notes.Single(n => n.NoteID == id);
                }
            }
            catch (Exception ex)
            {
                return new Note();
            }
        }

        public static bool Delete(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    var note = db.Notes.Where(n => n.NoteID == id).First();
                    db.Notes.Remove(note);
                    var result = db.SaveChanges();

                    return result > 0;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public static List<Note> GetNotesForCustomer(int id)
        {
            try
            {
                using (var db = new Context())
                {
                    return db.Notes.Where(n => n.CustomerID == id)
                        .Include(n => n.Account)
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                return new List<Note>();
            }
        }
    }
}
