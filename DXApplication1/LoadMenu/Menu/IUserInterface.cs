using System.Collections.Generic;

namespace LoadMenu.Menu
{
    public interface IUserInterface
    {
        IEnumerable<MenuItem> GetUserInterface();
    }
}
