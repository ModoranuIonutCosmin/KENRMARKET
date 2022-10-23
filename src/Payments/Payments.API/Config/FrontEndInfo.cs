namespace Payments.API.Config
{
    public class FrontEndInfo
    {
        public string BaseUrl { get; set; }
        public string OrderSuccessPath { get; set; }
        public string OrderFailurePath { get; set; }

        public string OrderSuccessUrl => BaseUrl + OrderSuccessPath;
        public string OrderFailureUrl => BaseUrl + OrderFailurePath;
    }
}
