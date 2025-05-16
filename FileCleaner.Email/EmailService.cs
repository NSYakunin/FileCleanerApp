using System.Net;
using System.Net.Mail;

namespace FileCleaner.Email;

public class EmailService
{
    private readonly string _recipient;

    public EmailService(string recipient) =>
        _recipient = recipient;

    public void SendLog(string logFilePath)
    {
        var smtp = new SmtpClient("smtp.example.com")
        {
            Port = 587,
            Credentials = new NetworkCredential("user", "password"),
            EnableSsl = true
        };

        var mail = new MailMessage("from@example.com", _recipient)
        {
            Subject = "Лог удаления файлов",
            Body = "Отчет в прикрепленном файле."
        };

        mail.Attachments.Add(new Attachment(logFilePath));

        smtp.Send(mail);
    }
}