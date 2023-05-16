using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elma2
{
    internal class Order
    {
        public Int32 id { get; set; }
        public String status { get; set; }
        public String description { get; set; }
        public Int32 clientId { get; set; }
        public Int32 sosudNo { get; set; }
        public String clientType { get; set; }
        public DateTime orderDate { get; set; }
    }
}
