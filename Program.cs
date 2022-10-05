using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;

#region system.net.mail
//using (var smtpClient = GerarSmtp())
//{
//    using (MailMessage mail = new MailMessage("atendimento.gcnsystems@gmail.com", "atendimento.gcnsystems@gmail.com" /*"lucasandrade595@hotmail.com"*/))
//    {
//        var teste = $"<script> var uri=https://localhost:7217/Email/Confirmation/2; $(document).ready(function(){{$.getJSON(uri).done(function(data){{ }}); }}); </script>";
//        mail.Subject = $"teste";
//        mail.IsBodyHtml = true;
//        mail.Body = $"<h1>Teste HAHA!<h1>" +
//            $"<script src='http://ajax.aspnetcdn.com/ajax/jQuery/jquery-2.0.3.min.js'></script>" +
//            $"<script> var uri=https://localhost:7217/Email/Confirmation/2; $(document).ready(function(){{$.getJSON(uri).done(function(data){{ }}); }}); </script>";

//                    //$"<img src=https://localhost:7217/Email/Confirmation/2 width=1 height=1>" +
//                    //$"<form action=https://localhost:7217/Email/Confirmation/2> <input type=submit value=Go to Google /></form>";

//        mail.Headers.Add("Disposition-Notification-To", "atendimento.gcnsystems@gmail.com");
//        mail.Headers.Add("Return-Receipt-To", "atendimento.gcnsystems@gmail.com");
//        mail.Headers.Add("Return-Receipt-To", "atendimento.gcnsystems@gmail.com");
//        mail.Headers.Add("Return-Receipt-To", "atendimento.gcnsystems@gmail.com");
//        mail.Headers.Add("Disposition-Notification-To", "<atendimento.gcnsystems@gmail.com>");
//        mail.DeliveryNotificationOptions = DeliveryNotificationOptions.OnSuccess;

//        //mail.Bcc.Add("atendimento.gcnsystems@gmail.com");
//        //mail.Attachments.Add(new Attachment($@"{CaminhoDiretorio}"));
//        await smtpClient.SendMailAsync(mail);
//    }
//}

//static SmtpClient GerarSmtp()
//{
//    SmtpClient smtpClient = new SmtpClient("smtp.gmail.com", 587);
//    smtpClient.EnableSsl = true;
//    smtpClient.UseDefaultCredentials = false;
//    smtpClient.Credentials = new NetworkCredential("atendimento.gcnsystems@gmail.com", "ywsapzqvymzjmgak");
//    return smtpClient;
//}

//http://www.yoursite.com/beacon.aspx?emailId=xxx 

#endregion

#region biblioteca MailKit
using (var message = new MimeMessage())
{
    message.From.Add(new MailboxAddress("No Reply", "atendimento.gcnsystems@gmail.com"));
    message.To.Add(new MailboxAddress("Lucas Chagas de Andrade", "lucasandrade595@hotmail.com"));
    message.Subject = "Test Sending to More email";
    var bodyBuilder = new BodyBuilder
    {
        HtmlBody = "Test email" +
        $"<h1>Test</h1>" +
        $"<img src=https://localhost:7217/Email/Confirmation/2 width=1 height=1>"
    };

    // Ask for email confirmation
    message.Headers.Add("Disposition-Notification-To", "atendimento.gcnsystems@gmail.com");
    message.Body = bodyBuilder.ToMessageBody();

    await GerarSmtp(message);
}

static async Task GerarSmtp(MimeMessage message)
{
    using (var smtpClient = new SmtpClient())
    {
        var supportsDsn = smtpClient.Capabilities.HasFlag(SmtpCapabilities.Dsn);
        smtpClient.CheckCertificateRevocation = false;

        await smtpClient.ConnectAsync("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
        smtpClient.Authenticate("atendimento.gcnsystems@gmail.com", "ywsapzqvymzjmgak");
        smtpClient.DeliveryStatusNotificationType = DeliveryStatusNotificationType.Full;

        var send = await smtpClient.SendAsync(message);
        await smtpClient.DisconnectAsync(true);
    }
}

#endregion