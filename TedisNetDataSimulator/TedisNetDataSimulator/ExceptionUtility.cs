using System;

namespace TedisNetDataSimulator
{
    /// <summary>
    /// Helper class to manage exception messages
    /// </summary>
    public static class ExceptionUtility
    {
        /// <summary>
        /// Returns the message of the exception, or the inner exception message if found, plus stack
        /// </summary>
        /// <param name="ex">Any exception</param>        
        public static string GetMessageFromException(Exception ex)
        {
            string message = GetMessageFromExceptionSimple(ex);
            message = message + ". Stack: " + ex.StackTrace;
            return message;
        }
        /// <summary>
        /// Returns the message of the exception, or the inner exception message if found, without stack
        /// </summary>
        /// <param name="ex">Any exception</param>        
        public static string GetMessageFromExceptionSimple(Exception ex)
        {
            Exception ex1 = ex;
            while (ex1.InnerException != null)
            {
                ex1 = ex1.InnerException;
            }
            return ex1.Message;
        }

    }
}
