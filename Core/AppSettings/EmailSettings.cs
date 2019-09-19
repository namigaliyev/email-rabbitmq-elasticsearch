namespace Core.AppSettings
{
    public class EmailSettings
    {
        public string Receiver { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public string Port { get; set; }
        public bool EnableSsl { get; set; }
    }
}