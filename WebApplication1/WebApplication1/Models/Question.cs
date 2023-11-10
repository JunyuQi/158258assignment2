using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Question
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int QuTypeID { get; set; }
        public virtual QuType QuType { get; set; }
    }
}