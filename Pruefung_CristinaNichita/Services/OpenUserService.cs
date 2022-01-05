using System;
using System.Collections.Generic;
using System.Text;

using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.IO;

using SQLite;
using QuickType;
using Newtonsoft.Json;

using Pruefung_CristinaNichita.Models;



namespace Pruefung_CristinaNichita.Services
{
    public class OpenUserService : IUserService
    {

        public event EventHandler<ForecastItem> EventUser;

        public event EventHandler<ForecastItem> EventUserUpdated;
        public async Task<Forecast> GetForecast() 
        {
            var url = $"https://randomuser.me/api/1.3/?" +
                      $"inc=gender,name,dob,phone,id,picture&nat=de,ch&results=1";


            var httpClient = new HttpClient();
            var result = await httpClient.GetStringAsync(url);
            var data = JsonConvert.DeserializeObject<UserData>(result);

            var forecast = new Forecast()
            {
                //Info = data.Info.Seed,
                Items = data.Results.Select(x => new ForecastItem()
                {
                    Name = x.Name.First + " " + x.Name.Last,
                    Birthday = x.Dob.Date.ToString("d") + " (" + x.Dob.Age + ")",
                    Phone = x.Phone,
                    Gender = x.Gender,
                    Picture = x.Picture.Thumbnail

                }).ToList()

            };

            return forecast;
        }

        public async Task DeleteAllItems()
        {
            await CreateConnection();

            await connection.DeleteAllAsync<ForecastItem>();
        }


        public async Task AddUser(ForecastItem forecastItem)
        {
            await CreateConnection();
            await connection.InsertAsync(forecastItem);
            EventUser?.Invoke(this, forecastItem);
        }

        public async Task UpdateUser(ForecastItem forecastItem)
        {
            await CreateConnection();
            await connection.UpdateAsync(forecastItem);
            EventUserUpdated?.Invoke(this, forecastItem);
        }


        public async Task<List<ForecastItem>> GetUser()
        {
            await CreateConnection();
            return await connection.Table<ForecastItem>().OrderBy(x => x.Name).ToListAsync();
        }

        private SQLiteAsyncConnection connection;

        private async Task CreateConnection()
        {
            if (this.connection != null)
            {
                return;
            }

            var docPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var dbPath = Path.Combine(docPath, "UserData.db");
            connection = new SQLiteAsyncConnection(dbPath);
            await connection.CreateTableAsync<ForecastItem>();

            if (await connection.Table<ForecastItem>().CountAsync() == 0) 
            {
            }
        }



        public async Task AddOrUpdateUser(ForecastItem item)
        {
            var it = await this.GetUser();
            var userGet = it.Find(x => x.Id == item.Id);

            if (userGet == null)
            {
                if (it.Count < 30)
                {
                    await AddUser(item);
                }
            }

            else
            {
                await UpdateUser(item);
            }
        }

    }
}
