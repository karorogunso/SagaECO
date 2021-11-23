using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaDB
{
    public interface AccountDB
    {
        /// <summary>
        /// Add the given user to the database.
        /// </summary>
        /// <param name="user">User that needs to be added.</param>
        void WriteUser(Account user);

        /// <summary>
        /// Get a user from the database using the given name.
        /// </summary>
        /// <param name="name">Name of the user.</param>
        /// <returns>The user with the given name.</returns>
        Account GetUser(string name);

        bool CheckPassword(string user, string password, uint frontword, uint backword);
        int GetAccountID(string user);
        bool Connect();
        bool isConnected();
        List<Account> GetAllAccount();

    }
}
