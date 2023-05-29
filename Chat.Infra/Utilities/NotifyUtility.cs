using System.Text;
using Chat.Application.Service.Utilities;
using Chat.Domain.Attributes;

namespace Chat.Infra.Utilities
{
    [Bean]
    public class NotifyUtility : INotifyUtility
    {

        public string GetTitleNotify(string input)
        {
            return GetTextForNotify(input, 25);
        }

        public string GetBodyNotify(string input)
        {
            return GetTextForNotify(input, 47) + "...";
        }

        private string GetTextForNotify(string input, int Length)
        {
            if (input.Length < Length)
                return input;

            var splitted = input.Substring(0, Length).Split(' ');
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < splitted.Length - 1; i++)
            {
                sb.Append(splitted[i] + " ");
            }
            return sb.ToString();
        }
    }
}