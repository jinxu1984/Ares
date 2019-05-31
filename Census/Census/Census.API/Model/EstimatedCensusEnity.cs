using EPPlus.Core.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Census.API.Model
{
    public class EstimatedCensusEnity: CensusEntity
    {
        [ExcelTableColumn("Districts")]
        public int DistrictId { get; set; }

        [ExcelTableColumn("EstimatesPopulation")]
        public new double Population { get; set; }

        [ExcelTableColumn("EstimatesHouseholds")]
        public new double Households { get; set; }
    }
}
