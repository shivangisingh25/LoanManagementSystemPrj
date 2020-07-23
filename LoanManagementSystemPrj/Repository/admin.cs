using LoanManagementSystemPrj.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LoanManagementSystemPrj.Repository
{
    public class admin : iadmin
    {
        LoanManagementContext db;
        public admin(LoanManagementContext _db)
        {
            db = _db;
        }
        public int AddDetail(Loan data)
        {
            if (db != null)
            {
                 db.Loan.Add(data);
                 db.SaveChanges();

                return data.Id;
            }

            return 0;
        }

        public int DeleteDetail(int Id)
        {
            int result = 0;

            if (db != null)
            {
                
                var post =  db.Loan.FirstOrDefault(x => x.Id == Id);

                if (post != null)
                {
                    
                    db.Loan.Remove(post);
                    result =  db.SaveChanges();
                    return 1;
                }
                return result;
            }

            return result;
        }

        public Loan GetDetail(int? Id)
        {
            if (db != null)
            {
                return (db.Loan.Where(x => x.Id == Id)).FirstOrDefault();
            }
            return null;
        }

        public List<Loan> GetDetails()
        {
            if (db != null)
            {
                return db.Loan.ToList();
            }

            return null;
        }

        public int  UpdateDetail(int id, string AccType, decimal LoanPremium)
        {
            if (db != null)
            {
                Loan val =  db.Loan.Where(x => x.Id == id).FirstOrDefault();
                Loan valc = db.Loan.Where(x => x.Id == id).FirstOrDefault();
                if (val != null)
                {
                    db.Loan.Remove(val);
                     db.SaveChanges();
                    if ((AccType != "") && (AccType != null))
                        valc.AccType = AccType;
                    if (LoanPremium!=0)
                        valc.LoanPremium = LoanPremium;
                     db.Loan.Add(valc);
                     db.SaveChanges();
                }
            }return 0;
        }
    }
}
