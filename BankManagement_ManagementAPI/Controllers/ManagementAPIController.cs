using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Logging;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace BankManagement_ManagementAPI.Controllers
{
    [Route("api/ManagementAPI")]
    [ApiController]
    public class ManagementAPIController : ControllerBase
    {

        private readonly ILogging _logger;

        public ManagementAPIController(ILogging logger)
        {
            _logger = logger;        
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BankDTO>> GetBank()
        {
            _logger.Log("Getting all bank details", "");
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
                _logger.Log("get bank details error" + accno, "error");
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

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        [HttpDelete("{accno:int}", Name = "DeleteBank")]

        public IActionResult DeleteBank(int accno)
        {
            if (accno == 0)
            {
                return BadRequest();
            }

            var bank = BankStore.bankList.FirstOrDefault(u => u.AccNo == accno);   
            if(bank == null)
            {
                return NotFound();
            }

            BankStore.bankList.Remove(bank);
            return NoContent();
        }

        [HttpPut("{accno:int}", Name = "UpdateBank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateBank(int accno, [FromBody]BankDTO bankDTO)
        {
            if(bankDTO==null || accno !=bankDTO.AccNo)
            {
                return BadRequest();
            }

            var bank = BankStore.bankList.FirstOrDefault(u => u.AccNo == accno);
            bank.AccName = bankDTO.AccName;
            bank.AadharCard = bankDTO.AadharCard;
            bank.PanCard= bankDTO.PanCard;
            bank.Address = bankDTO.Address;

            return NoContent();
        }

        [HttpPatch("{accno:int}", Name = "UpdatePartialBank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public IActionResult UpdatePartialBank(int accno, JsonPatchDocument<BankDTO> patchDTO)
        {
            if (patchDTO == null || accno == 0)
            {
                return BadRequest();
            }

            var bank = BankStore.bankList.FirstOrDefault(u => u.AccNo == accno);

            if(bank == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(bank, ModelState);
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }     
}
