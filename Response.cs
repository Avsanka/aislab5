using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISLab5
{
    public class Response
    {
        public int id { get; set; }
        public string home_town { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string bdate { get; set; }
        public Country country { get; set; }
        public string phone { get; set; }
        public string screen_name { get; set; }
        public int sex { get; set; }
        public int relation { get; set; }
    }

    public class Rootobject
    {
        public Response response { get; set; }
    }
}
