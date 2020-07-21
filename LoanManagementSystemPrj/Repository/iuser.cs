using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManagementSystemPrj.Models;


namespace LoanManagementSystemPrj.Repository
{
    public interface iuser
    {

        Loan GetDetail(int? inpId);

        int UpdateDetail(int id, string PhnNo, string Address);
    }
}
