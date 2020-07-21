using LoanManagementSystemPrj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementSystemPrj.Repository
{
    public class user : iuser
    {
        LoanManagementContext db;
        public user(LoanManagementContext _db)
        {
            db = _db;
        }
        public Loan GetDetail(int? inpId)
        {
            if (db != null)
            {
                return (db.Loan.Where(x => x.Id == inpId)).FirstOrDefault();
            }
            return null;
        }

        public int  UpdateDetail(int id, string PhnNo, string Address)
        {
            if (db != null)
            {
                Loan val =  db.Loan.Where(x => x.Id == id).FirstOrDefault();
                Loan valc =  db.Loan.Where(x => x.Id == id).FirstOrDefault();
                if (val != null)
                {
                    db.Loan.Remove(val);
                    db.SaveChanges();
                    if ((PhnNo != "") && (PhnNo != null))
                        valc.PhnNo = PhnNo;
                    if ((Address != "") && (Address != null))
                        valc.Address = Address;
                     db.Loan.Add(valc);
                     db.SaveChanges();
                }
            }return 0;
        }

    }
}
