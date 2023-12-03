using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LoginInterface
{
    // Interface for the db observers
    internal interface IDbObserver
    {
        // Update all of the observers of a change in the database
        void Update(string message);
    }

    // Interface for the db being observed
    internal interface IDBConnector
    {

        // Register an observer
        void AddDbObserver(IDbObserver observer);

        // Remove an observer from the list
        void RemoveDbObserver(IDbObserver observer);

        // Cause the observers to update the users
        void NotifyDbObservers(string message);
    }
}
