using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRent
{
    public static class Helper
    {
        public static CarRentalContext db = new CarRentalContext();
        public static bool IsCurrentUserAdmin;
        //public static string IsContextMenuEnabled;
        //public static string IsContextMenuVisible;
    }
}
