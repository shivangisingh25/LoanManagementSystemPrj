using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LoanManagementSystemPrj.Repository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanManagementSystemPrj.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class userController : ControllerBase
    {
        iuser udb;
        public userController(iuser _udb)
        {
            udb = _udb;
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
                var data =  udb.GetDetail(Id);
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

        [HttpPut]
        [Route("UpdateDetail")]
        public IActionResult UpdateDetail(int id, string PhnNo= "", string Address = "")
        {
            if (ModelState.IsValid)
            {
                try
                {
                     udb.UpdateDetail(id, PhnNo, Address);

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