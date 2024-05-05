using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bislerium_Coursework_Service.Model
{
    public class EmailConfiguration
    {
        public String From { get; set; } = null!;

        public String SmtpServer { get; set; } = null!;

        public int Port { get; set; }

        public String UserName { get; set; } = null!;

        public String Password { get; set; } = null!;

    }
}
