namespace FR.IdentityServer.Model.API
{
    public class Result
    {
        public Status Status { get; set; }
        public string Message { get; set; }
        public object Data { get; set; }
    }
}