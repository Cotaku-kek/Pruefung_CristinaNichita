using System;
using System.Collections.Generic;
using System.Text;

using SQLite;

namespace Pruefung_CristinaNichita.Models
{
    public class Forecast
    {
       //[PrimaryKey, AutoIncrement]
       //public string Info { get; set; }

        //public string Name { get; set; }
        public List<ForecastItem> Items { get; set; }
    }

}
