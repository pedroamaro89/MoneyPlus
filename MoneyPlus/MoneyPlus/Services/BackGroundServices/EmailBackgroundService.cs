using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.V4.Pages.Account.Internal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MoneyPlus.Data;
using MoneyPlus.Repositories;
using MoneyPlus.Services.Models;
using System.Diagnostics;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using static System.Formats.Asn1.AsnWriter;

namespace MoneyPlus.Services.BackGroundServices;

public class EmailBackgroundService : BackgroundService
{
    //static int count = 5;
    private readonly MoneyPlusContext ctx;

    // Configurações
    TimeSpan IntervalBetweenJobs = TimeSpan.FromHours(24);
    //TimeSpan IntervalBetweenJobs = TimeSpan.FromSeconds(30);
    public IServiceProvider _serviceProvider { get; }

	public EmailBackgroundService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken ct)
    {
        using var scope = _serviceProvider.CreateScope();
        var ctx = scope.ServiceProvider.GetRequiredService<MoneyPlusContext>();

        while (!ct.IsCancellationRequested)
        {
            await DoWorkAsync();

            await Task.Delay(IntervalBetweenJobs);
        }
    }

    private async Task DoWorkAsync()
    {
        try
        {
			// Código que deve ser executado   
			SendEmailAsync();
        }
        catch (Exception e)
        {
			Logger.WriteLog("Error sending email");
		}
    }


    private async Task SendEmailAsync() // não consegui usar o Smtp Client da google, fiz pelo método de guardar os registos na BD como se fossem emails enviados. A cada 24h o servidor cria um registo na tabela EmailLogs.
    {
        using var scope = _serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<MoneyPlusContext>();
		var userRepository = scope.ServiceProvider.GetRequiredService<UserRepository>();
		var users = context.Users.ToList();

        foreach (var item in users)
        {
			var totalBalance = userRepository.GetTotalBalanceByUser(item.Id);

			var EmailLog = new EmailLogs();

            EmailLog.Subject = "Daily Balance Report";
            EmailLog.Body = $"Hi, your current balance is {totalBalance}€";
            EmailLog.EmailFrom = "admin@gmail.com";
            EmailLog.EmailTo = item.Email;
            EmailLog.Date = DateTime.Now;

            context.EmailLogs.Add(EmailLog);
            await context.SaveChangesAsync();
            Logger.WriteLog($"Email sent to: {EmailLog.EmailTo}");
		}


        /* using (MailMessage mm = new MailMessage())
		 {

			  if (fuAttachment.HasFile)
			  {
				  string FileName = Path.GetFileName(fuAttachment.PostedFile.FileName);
				  mm.Attachments.Add(new Attachment(fuAttachment.PostedFile.InputStream, FileName));
			  }*/

        /*mm.From = new MailAddress("pmbamaro@gmail.com");
		mm.To.Add("pmb_amaro@hotmail.com");
		mm.Subject = "Hello World";
		mm.Body = "<h1>Hello</h1>" +
			 "<p> Your exact amount is ::::::::::: </p>";

		mm.IsBodyHtml = false;
		SmtpClient smtp = new SmtpClient();
		smtp.Host = "smtp.gmail.com";
		smtp.EnableSsl = true;
		NetworkCredential NetworkCred = new NetworkCredential("pmbamaro@gmail.com", "");
		smtp.UseDefaultCredentials = true;
		smtp.Credentials = NetworkCred;
		smtp.Port = 587;
		try
		{
			smtp.Send(mm);
		}
		catch (Exception ex)
		{
			throw ex;
		}
		// ClientScript.RegisterStartupScript(GetType(), "alert", "alert('Email sent.');", true);
	}*/
    }
}
