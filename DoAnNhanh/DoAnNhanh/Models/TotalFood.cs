using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;


namespace DoAnNhanh.Models
{
    public class TotalFood
    {
     
       public string name { get; set; }
        public int ma { get; set; }
        public int TotalFd { get; set; }
    }
}