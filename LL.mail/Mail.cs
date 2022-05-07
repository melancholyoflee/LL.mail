namespace LL.mail
{
    using System.Net;
    using System.Net.Mail;
    public class Mail
    {
        private string? _Host;
        private int _Port;
        private string? _mailID;
        private string? _mailPwd;
        private Dictionary<string, Stream>? _files;
        private bool _IsBodyHtml = true;
    
        public Mail mailConfig(string Host, int Port)
        {
            _Host = Host;
            _Port = Port;
            return this;
        }
        public Mail mailConfig(string Host, int Port, string MailID, string MailPwd)
        {
            _Host = Host;
            _Port = Port;
            _mailID = MailID;
            _mailPwd = MailPwd;
            return this;
        }
        public void SendMail(MailModel ReceiveMail)
        {
            if (String.IsNullOrEmpty(_Host)) return;
            if (_Port == 0) return;

            MailMessage message = new MailMessage();
            message.From = new System.Net.Mail.MailAddress(ReceiveMail.FROM);
            if (ReceiveMail.TO.Count > 0)
            {
                foreach (var item in ReceiveMail.TO)
                {
                    message.To.Add(item);
                }
            }

            message.IsBodyHtml = _IsBodyHtml;
            message.BodyEncoding = System.Text.Encoding.UTF8;
            message.Subject = ReceiveMail.SUBJECT;
            message.Body = ReceiveMail.BODY;

            if (ReceiveMail.CC != null)
            {
                foreach (var item in ReceiveMail.CC)
                {
                    if (!string.IsNullOrEmpty(item.Trim()))
                    {
                        message.Bcc.Add(new MailAddress(item.Trim()));
                    }

                }
            }

            if (ReceiveMail.BCC != null)
            {
                foreach (var item in ReceiveMail.BCC)
                {
                    if (!string.IsNullOrEmpty(item.Trim()))
                    {
                        message.Bcc.Add(new MailAddress(item.Trim()));
                    }

                }
            }

            if (ReceiveMail.filePaths != null)
            {
                for (int i = 0; i < ReceiveMail.filePaths.Count; i++)
                {
                    if (!string.IsNullOrEmpty(ReceiveMail.filePaths[i].Trim()))
                    {
                        Attachment file = new Attachment(ReceiveMail.filePaths[i].Trim());
                        message.Attachments.Add(file);
                    }

                }
            }

            if (_files != null && _files.Count > 0)
            {
                foreach (string fileName in _files.Keys)
                {
                    Attachment attfile = new Attachment(_files[fileName], fileName);
                    message.Attachments.Add(attfile);
                }
            }

            using (SmtpClient smtpClient = new SmtpClient(_Host, _Port))
            {
                if (!string.IsNullOrEmpty(_mailID) && !string.IsNullOrEmpty(_mailPwd))
                {
                    smtpClient.Credentials = new NetworkCredential(_mailID, _mailPwd);
                }
                smtpClient.Send(message);
            };

            
        }
        public void ExcuteMail()
        {
            if (MailBox.MAILS is not null)
            {
                foreach (var item in MailBox.MAILS)
                {
                    SendMail(item);
                }
            }
        }
        public Mail IsHtml(bool IsBodyHtml)
        {
            _IsBodyHtml = IsBodyHtml;
            return this;
        }
       
    }
  

}