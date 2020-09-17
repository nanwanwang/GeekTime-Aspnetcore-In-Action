using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AOPDemo
{
    public class HomeController:Controller
    {
        private readonly ISystemClock _clock;
        public HomeController(ISystemClock clock)
        {
            this._clock = clock;
        }

        [HttpGet("/{id}")]
        public async Task Index(int id)
        {
            async Task<string[]> GetTimesAsync()
            {
                var times = new string[6];
                for (int index = 0; index < 3; index++)
                {
                    times[index] = $"Local: {_clock.GetCurrentTime(DateTimeKind.Local)}";
                    await Task.Delay(1000);
                }

                for (int index = 3; index < 6; index++)
                {
                    times[index] = $"UTC: {_clock.GetCurrentTime(DateTimeKind.Utc)}";
                    await Task.Delay(1000);
                }
                return times;
            }

            var currentTimes = await GetTimesAsync();
            var list = string.Join("", currentTimes.Select(x => $"<li>{x}</li>"));
            Response.ContentType = "text/html";
            await Response.WriteAsync(
                $@"<html><body>
                         <ul>
                         {list}
                         </ul>
                         </body>
                         </html>"
                
                );
        }
    }
}
