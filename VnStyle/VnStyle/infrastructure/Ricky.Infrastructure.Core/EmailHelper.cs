using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;

namespace Ricky.Infrastructure.Core
{
    /// <summary>
    /// Email helpers functionality
    /// </summary>
    public class EmailHelper
    {   
        private readonly object _emailLock = new Object();

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        public void SendEmail(string to, string from, string subject, string body)
        {
            var message = new MailMessage(From(from), To(to)) { IsBodyHtml = true, Subject = subject, Body = body };

            lock (_emailLock)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = true;
                    smtpClient.Send(message);
                }
            }
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="subject"></param>
        /// <param name="body"></param>
        /// <param name="attachment"></param>
        /// <param name="fileName"></param>
        /// <param name="mimeType"></param>
        public void SendEmail(string to, string from, string subject, string body, Stream attachment, string fileName, string mimeType)
        {
            var message = new MailMessage(From(from), To(to)) { IsBodyHtml = true, Subject = subject, Body = body };

            if (attachment != null)
            {
                message.Attachments.Add(new Attachment(attachment, fileName, mimeType));
            }

            lock (_emailLock)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailContent"></param>
        /// <param name="files"></param>
        /// <param name="fileNames"></param>
        /// <param name="mailCCs"></param>
        /// <returns></returns>
        public bool SendEmail(string to, string from, string mailSubject, string mailContent, IList<Byte[]> files, IList<string> fileNames, IList<string> mailCCs)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                return false;
            }

            var message = new MailMessage(From(from), To(to)) { IsBodyHtml = true, Subject = mailSubject, Body = mailContent };

            // Mail CCs
            if (mailCCs != null && mailCCs.Any())
            {
                foreach (var mailCc in mailCCs)
                    message.CC.Add(mailCc);
            }

            // Attachment
            if (files != null && fileNames != null)
            {
                for (var i = 0; i < files.Count; i++)
                {
                    if (fileNames[i] == null) continue;
                    var fileName = Path.GetFileName(fileNames[i]);
                    message.Attachments.Add(new Attachment(new MemoryStream(files[i]), fileName));
                }
            }

            lock (_emailLock)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }

            return true;
        }

        /// <summary>
        /// Send email
        /// </summary>
        /// <param name="to"></param>
        /// <param name="from"></param>
        /// <param name="mailSubject"></param>
        /// <param name="mailContent"></param>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        /// <param name="mailCCs"></param>
        /// <returns></returns>
        public bool SendEmail(string to, string from, string mailSubject, string mailContent, Byte[] file, string fileName, IList<string> mailCCs)
        {
            if (string.IsNullOrWhiteSpace(from) || string.IsNullOrWhiteSpace(to))
            {
                return false;
            }
            var message = new MailMessage(From(from), To(to)) { IsBodyHtml = true, Subject = mailSubject, Body = mailContent };

            // Mail CCs
            if (mailCCs != null && mailCCs.Any())
            {
                foreach (var mailCc in mailCCs)
                    message.CC.Add(mailCc);
            }

            // Attachment
            if (file != null && !string.IsNullOrWhiteSpace(fileName))
            {
                fileName = Path.GetFileName(fileName);
                message.Attachments.Add(new Attachment(new MemoryStream(file), fileName));
            }

            lock (_emailLock)
            {
                using (var smtpClient = new SmtpClient())
                {
                    smtpClient.EnableSsl = false;
                    smtpClient.Send(message);
                }
            }

            return true;
        }

        /// <summary>
        /// Get from mail address
        /// </summary>
        /// <param name="fromEmail"></param>
        /// <returns></returns>
        public MailAddress From(string fromEmail)
        {
            return new MailAddress(fromEmail, "Beaute App");
        }

        /// <summary>
        /// Get to mail address
        /// </summary>
        /// <param name="toEmail"></param>
        /// <returns></returns>
        public MailAddress To(string toEmail)
        {
            return new MailAddress(toEmail);
        }

        /// <summary>
        /// Get display name
        /// </summary>
        /// <returns></returns>
        public string GetDisplayName()
        {
            //var configurationProvider = new ConfigurationProvider();
            //return configurationProvider.Sender ?? "STS Identity";
            return "Beaute App";
        }
    }
}