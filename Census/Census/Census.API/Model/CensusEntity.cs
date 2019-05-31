using EPPlus.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Model
{
    public class CensusEntity
    {
        [ExcelTableColumn("State")]
        public int StateId { get; set; }

        public double Population { get; set; }

        public double Households { get; set; }
    }
}
