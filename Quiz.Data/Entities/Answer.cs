using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Quiz.Data.Entities
{
    public class Answer : BaseEntity
    {
        public bool IsCorrect { get; set; }
    }
}
