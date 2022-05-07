namespace LL.mail
{
    public class MailModel
    {
        public string BODY { get;  set; }
        public string SUBJECT { get;  set; }
        public List<string> filePaths { get; set; }
        public List<string> TO { get; set; }
        public List<string> CC { get; set;}
        public List<string> BCC { get; set; }
        public string FROM { get; set; }
    }

    public static  class MailBox
    {
        public static List<MailModel> MAILS;
    }
}