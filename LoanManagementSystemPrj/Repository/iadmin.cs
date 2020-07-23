using LoanManagementSystemPrj.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementSystemPrj.Repository
{
    public interface iadmin
    {
        List<Loan> GetDetails();
        Loan GetDetail(int? Id);
        int AddDetail(Loan data);
        int DeleteDetail(int Id);
        int UpdateDetail(int id, string AccType, decimal LoanPremium);


    }
}
