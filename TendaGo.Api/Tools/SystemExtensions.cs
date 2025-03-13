using System.Linq;

namespace System
{
    public static class DataExtensions
    {
        public static string GetMessage(this Exception ex)
        {
            return $"{ex?.Message}".Split(new[] { "Transaction" }, StringSplitOptions.RemoveEmptyEntries).FirstOrDefault();
        }

        public static string GetAllMessages(this Exception ex)
        {
            return $"{ex?.GetMessage()} {ex?.InnerException?.GetMessage()} {ex?.InnerException?.InnerException?.GetMessage()}";
        }
    }
}