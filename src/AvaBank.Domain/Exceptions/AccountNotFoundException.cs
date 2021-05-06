using Liquid.Core.Exceptions;

namespace AvaBank.Domain.Exceptions
{
    /// <summary>
    /// Occurs when the account searched is not found.
    /// </summary>
    /// <seealso cref="Liquid.Core.Exceptions.LightCustomException" />
    public class AccountNotFoundException : LightCustomException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AccountNotFoundException"/> class.
        /// </summary>
        public AccountNotFoundException() : base("Account not found.", ExceptionCustomCodes.NotFound)
        {
        }
    }
}