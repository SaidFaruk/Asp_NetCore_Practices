using AspNetCore.DependencyInjection.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AspNetCore.DependencyInjection.Controller
{
    public class MyController
    {
        private readonly IMyService _myService;

        public MyController(IMyService myService)
        {
            _myService = myService;
        }
        public void Run()
        {
            _myService.DoWork();
        }
    }
}
