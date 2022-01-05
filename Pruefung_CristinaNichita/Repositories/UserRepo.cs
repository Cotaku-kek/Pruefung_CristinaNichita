using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using SQLite;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Linq;

using Pruefung_CristinaNichita.Models;
using Pruefung_CristinaNichita.ViewModels;

namespace Pruefung_CristinaNichita.Repositories
{
    public class UserRepo : IUserRepo
    {
        public UserRepo() { }

        private async Task CreateConnection()
        {
            if (this.connector != null)
            {
                return;
            }
            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbPath = Path.Combine(docPath, "UserData.db");
            connector = new SQLiteAsyncConnection(dbPath);
            await connector.CreateTableAsync<ForecastItem>();

            if (await connector.Table<ForecastItem>().CountAsync() == 0)
            {
                await connector.InsertAsync(new ForecastItem()
                {
                    Name = "Cristina",
                }
                );
            }
        }

        private SQLiteAsyncConnection connector;

    }


}
