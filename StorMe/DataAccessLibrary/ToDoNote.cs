
namespace DataAccessLibrary
{
    using System;
    using System.Collections.Generic;
    
    public partial class ToDoNote
    {
        public int Id { get; set; }
        public string toDoName { get; set; }
        public int Note { get; set; }
        public bool isChecked { get; set; }
    }
}
