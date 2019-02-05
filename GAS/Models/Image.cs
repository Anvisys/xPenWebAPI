using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GAS.Models
{
    public class Image
    {

        public int UserID { get; set; }

        public byte[] ImageByte { get; set; }

        public String ImageString { get; set; }
    }
}