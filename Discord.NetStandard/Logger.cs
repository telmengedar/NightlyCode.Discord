using System;

namespace NightlyCode.Discord
{
    
    /// <summary>
    /// logger for discord system messages
    /// </summary>
    public static class Logger
    {
        public static event Action<object, string, string> InfoMessage;

        public static event Action<object, string, string> WarningMessage;

        public static event Action<object, string, Exception> ErrorMessage;
        

        public static void Info(object sender, string message, string details = null)
        {
            InfoMessage?.Invoke(sender, message, details);
        }

        public static void Warning(object sender, string message, string details = null)
        {
            WarningMessage?.Invoke(sender, message, details);
        }

        public static void Error(object sender, string message, Exception details = null)
        {
            ErrorMessage?.Invoke(sender, message, details);
        }
    }
}