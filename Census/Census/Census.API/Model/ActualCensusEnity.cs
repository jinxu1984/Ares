using EPPlus.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Model
{
    public class ActualCensusEntity : CensusEntity
    {
        [ExcelTableColumn("ActualPopulation")]
        public new double Population { get; set; }

        [ExcelTableColumn("ActualHouseholds")]
        public new double Households { get; set; }
    }
}
