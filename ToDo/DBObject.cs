using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ToDo
{
    public class DBObject
    {
        public static void Initial(IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                AppDbContent content = scope.ServiceProvider.GetRequiredService<AppDbContent>();
            }
        }
    }
}
