using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Entities
{
    public class Question : BaseEntity
    {
        public int Category { get; set; }
    }
}
