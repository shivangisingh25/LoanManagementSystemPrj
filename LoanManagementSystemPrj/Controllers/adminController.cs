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
        iadmin adb;
        public adminController(iadmin _adb)
        {
            adb = _adb;
        }
        [HttpGet]
        [Route("GetDetails")]
        public IActionResult GetDetails()
        {
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
        public IActionResult AddDetail([FromBody]Loan model)
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
        public IActionResult DeleteDetail(int? Id)
        {
            int result = 0;

            if (Id == null)
            {
                return BadRequest();
            }

            try
            {
                result =  adb.DeleteDetail(Id);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
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
                     adb.UpdateDetail(id,AccType, LoanPremium);

                    return Ok();
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
