using System;
using System.Collections.Generic;
using System.Text;

using System.Threading.Tasks;
using Pruefung_CristinaNichita.Models;

namespace Pruefung_CristinaNichita.Services
{
    public interface IUserService
    {
        Task<Forecast> GetForecast();
    }
}
