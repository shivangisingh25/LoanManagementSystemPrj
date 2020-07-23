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
    public class userController : ControllerBase
    { 


        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(userController));
       
        iuser udb;
        public userController ( iuser _udb)
        {
        
            udb = _udb;
        } 

    
        
        
        [HttpGet]
        [Route("GetDetail")]
        public IActionResult GetDetail(int? Id)
        {
          _log4net.Info("userController Http GET");
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
            _log4net.Info("userController Http PUT");
            if (ModelState.IsValid)
            {
                try
                {
                    var result = udb.UpdateDetail(id, PhnNo, Address);

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