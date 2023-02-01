using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AutoArchiver.Web.Pages
{
    public class IndexModel : PageModel
    {
        //public string Email { get; set; }
        //public string? Message { get; set; }

        public string? CacheBust { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        //public async Task<IActionResult> OnPost(string email, string message)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OnPost(string? email, string? message)
        {
            // could do a lot of modelstate validation here
            if (string.IsNullOrEmpty(email))
                return LocalRedirect("/email-fail");

            var postmarkServerToken = AppConfiguration.LoadFromEnvironment().PostmarkServerToken;

            //var response = await Email.SendTemplate("forgot-password", email, "Thank you - we will get back to you!", postmarkServerToken);

            // send confirmation to the person who just submitted email on site
            {
                var ToEmailAddress = email;
                var subject = "Thank you";
                var text = "Thank you - we will get back to you soon";
                var html = "Thank you - we will get back to you soon";
                var foo = new AAEmail(ToEmailAddress, subject, text, html);
                var response = await Email.Send(foo, postmarkServerToken);
            }

            // send email to the admin 
            {
                var ToEmailAddress = "dave@hmsoftware.co.uk";
                var subject = "auto-archiver message";
                var text = $"sender is: {email}, message is: {message}";
                var html = $"sender is: {email}, message is: {message}";
                var foo = new AAEmail(ToEmailAddress, subject, text, html);
                var response = await Email.Send(foo, postmarkServerToken);
            }

            // PRG
            return LocalRedirect("/email-success");
        }
    }
}