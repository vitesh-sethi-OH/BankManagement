using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement_ManagementAPI.Controllers
{
    [Route("api/ManagementAPI")]
    [ApiController]
    public class ManagementAPIController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BankDTO>> GetBank()
        {
            return Ok(BankStore.bankList);
        }
        [HttpGet("accno", Name = "GetBank")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BankDTO> GetBank(int accno)
        {
            if (accno == 0)
            {
                return BadRequest();
            }
            var bank = BankStore.bankList.FirstOrDefault(u => u.AccNo == accno);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(bank);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<BankDTO> CreateBank([FromBody] BankDTO bankDTO)
        {
            if (bankDTO == null)
            {
                return BadRequest(bankDTO);
            }
            if (bankDTO.AccNo > 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            bankDTO.AccNo = BankStore.bankList.OrderByDescending(u => u.AccNo).FirstOrDefault().AccNo+1;
            BankStore.bankList.Add(bankDTO);

            return CreatedAtRoute("GetBank", new { accno = bankDTO.AccNo}, bankDTO);
            }
        }     
    }
