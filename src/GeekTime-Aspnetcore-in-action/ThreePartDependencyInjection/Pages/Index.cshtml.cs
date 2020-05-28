using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using ThreePartDependencyInjection.Services;

namespace ThreePartDependencyInjection.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        private readonly IMyService _myService;

        public IMyService MyService { get; set; }

        public IndexModel(ILogger<IndexModel> logger
            )
        {
            _logger = logger;
            //_myService = myService;
        }

        public void OnGet()
        {
            _myService.ShowCode();
            //MyService.ShowCode();
        }
    }
}
