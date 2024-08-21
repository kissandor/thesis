using MailKit.Net.Smtp;
using MimeKit;

namespace ExcelUploadServer.Data
{
    public class SendEmail
    {
        private const string MailFromEmailAddress = "uipathsearchrobot@gmail.com";
        private const string MailToEmailAddress = "uipathsearchrobotuser@gmail.com";

        public SendEmail(){}

        public void emailSender()
        {
            SmtpClient smtp = null; // Declare smtp here so it is accessible in the finally block.
            try
            {
                // Create a new email message
                var email = new MimeMessage();
                email.From.Add(new MailboxAddress("Server", MailFromEmailAddress));
                email.To.Add(new MailboxAddress("UiPath Robot", MailToEmailAddress));
                email.Subject = "Report";

                // Create the email body
                var builder = new BodyBuilder
                {
                    TextBody = "Hello"
                };
                email.Body = builder.ToMessageBody();

                // Setup the SMTP client
                smtp = new SmtpClient();
                smtp.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);

                // Authenticate with your Google account credentials
                smtp.Authenticate(MailFromEmailAddress, "ycqw brcc evxo pibs"); // Replace with your App Password

                // Send the email
                smtp.Send(email);
            }
            catch (Exception ex)
            {
                // Log or handle the error appropriately
                Console.WriteLine($"Error sending email: {ex.Message}");
            }
            finally
            {
                // Ensure the smtp client is disconnected and disposed properly
                if (smtp != null)
                {
                    smtp.Disconnect(true);
                    smtp.Dispose();
                }
            }
        }
    }
}
