using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace Pruefung_CristinaNichita.Models
{
    public class ForecastItem
    {

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Info { get; set; }
        public string Name { get; set; }
        public string Birthday { get; set; }
        public string Phone { get; set; }
        public string Gender { get; set; }
        public Uri Picture { get; set; }                            //Uri = Uniform Resource Identifier

    }
}
