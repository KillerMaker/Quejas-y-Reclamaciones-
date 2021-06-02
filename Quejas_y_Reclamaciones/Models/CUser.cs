using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Quejas_y_Reclamaciones.Models
{
    public class CUser
    {
        public int? id { get; set; }
        public string userName { get; set; }
        public string password { get; set; }
        public int userType { get; set; }

        public CUser(int? id,string userName, string password, int userType)
        {
            this.id = id;
            this.userName = userName;
            this.password = password;
            this.userType = userType;
        }
    }
}
