using SQLite;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestMbb
{
    public class WorkOutItem
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string Name { get; set; }
        public string Game { get; set; }
        public string Description { get; set; }
        public int AmountTotal { get; set; }
        public int AmountLeft { get; set; }
        public bool Done { get; set; }
    }
}
