using System;
using System.Collections.Generic;

namespace FundaAssignment.WebApi.Domain.FundaModels
{
    public class Koopwoningen
    {
        public int AccountStatus { get; set; }
        public bool EmailNotConfirmed { get; set; }
        public bool ValidationFailed { get; set; }
        public object ValidationReport { get; set; }
        public int Website { get; set; }
        public List<Huis> Objects { get; set; }
        public Paging Paging { get; set; }
        public int TotaalAantalObjecten { get; set; }
    }
}
