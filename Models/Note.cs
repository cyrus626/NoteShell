using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteShell.Models
{
    public class Note
    {
        public int Id { get; set; }
        public string NoteName { get; set; }
        public DateTime TimeSaved { get; set; }
        public string Content { get; set; }
    }
}
