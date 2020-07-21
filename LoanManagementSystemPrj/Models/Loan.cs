using System;
using System.Collections.Generic;

namespace LoanManagementSystemPrj.Models
{
    public partial class Loan
    {
        public int Id { get; set; }
        public int AccNo { get; set; }
        public string Name { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string PhnNo { get; set; }
        public string AccType { get; set; }
        public decimal? AccBal { get; set; }
        public decimal? LoanAmt { get; set; }
        public decimal? LoanPremium { get; set; }
    }
}
