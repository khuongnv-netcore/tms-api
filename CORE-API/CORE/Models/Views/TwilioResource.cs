using System;
namespace CORE_API.CORE.Models.Views
{
    public class SendSmsToUserInputResource
    {
        public string NumberPhone { get; set; }
        public string Body { get; set; }
    }

    public class SendSmsToUserOutputResource
    {
        public string Status { get; set; }
        public string NumberPhone { get; set; }
        public string Body { get; set; }
        public int? ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
    }
}
