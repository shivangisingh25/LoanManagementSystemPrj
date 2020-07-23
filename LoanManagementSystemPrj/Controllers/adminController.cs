using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManagementSystemPrj.Models;
using LoanManagementSystemPrj.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystemPrj.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class adminController : ControllerBase
    {

        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(adminController));
    
        iadmin adb;
        public adminController( iadmin _adb)
        {
            adb = _adb;
        }
        
         
        [HttpGet]
        [Route("GetDetails")]
        public IActionResult GetDetails()
        {
            _log4net.Info("adminController Http GET ALL");
            try
            {
                var loans =  adb.GetDetails();
                if (loans == null)
                {
                    return NotFound();
                }

                return Ok(loans);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetDetail(int? Id)
        {
            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                var data =  adb.GetDetail(Id);
                if (data == null)
                {
                    return NotFound();
                }
                return Ok(data);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddDetail")]
        public IActionResult AddDetail(Loan model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var Id =  adb.AddDetail(model);
                    if (Id > 0)
                    {
                        return Ok(Id);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteDetail")]
        public IActionResult DeleteDetail(int Id)
        {
            

            if (Id == null)
            {
                return BadRequest(Id);
            }

            try
            {
                var result =  adb.DeleteDetail(Id);
                if (result == 0)
                {
                    return NotFound(result);
                }
                return Ok(result);
            }
            catch (Exception)
            {

                return BadRequest(Id);
            }
        }

        [HttpPut]
        [Route("UpdateDetail")]
        public IActionResult UpdateDetail(int id, string AccType = "", decimal LoanPremium=0)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var result = adb.UpdateDetail(id,AccType, LoanPremium);

                    return Ok(result);
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

    }
}
