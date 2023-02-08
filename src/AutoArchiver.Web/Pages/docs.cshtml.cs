using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArchiver.Web.Pages
{
    public class DocsModel : PageModel
    {
        public string? CacheBust { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public DocsModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

    }
}