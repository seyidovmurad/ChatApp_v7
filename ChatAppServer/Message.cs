using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatAppClient.Models
{
    public class Message
    {

        public string Text { get; set; }

        public DateTime Time { get; set; }


        public string Timer => Time.ToString("HH:mm");
    }
}
