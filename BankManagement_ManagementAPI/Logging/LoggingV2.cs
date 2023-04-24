namespace BankManagement_ManagementAPI.Logging
{
    public class LoggingV2 :ILogging
    {
        public void Log(string message, string type)
        {
            if (type == "error")
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write("ERROR - " +message);
                Console.BackgroundColor= ConsoleColor.Black;
            }
        }
    }
}
