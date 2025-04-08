using AspNetCore.DependencyInjection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.DependencyInjection.Services
{
    public class MyService : IMyService
    {
        private readonly Guid _id;
        public MyService()
        {
            _id = Guid.NewGuid();
        }

        public void DoWork()
        {
            Console.WriteLine($" MyService is doing work with ID: {_id}");
        }
    }
}
