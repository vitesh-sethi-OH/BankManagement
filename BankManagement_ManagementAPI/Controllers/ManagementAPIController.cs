using AutoMapper;
using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Logging;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;
using BankManagement_ManagementAPI.Repository.IRepository;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BankManagement_ManagementAPI.Controllers
{
    [Route("api/ManagementAPI")]
    [ApiController]
    public class ManagementAPIController : ControllerBase
    {

        private readonly ILogging _logger;

        //public ManagementAPIController(ILogging logger)
        //{
        //    _logger = logger;        
        //}

        private readonly IBankRepository _dbBank;
        private readonly IMapper _mapper;
        public ManagementAPIController(IBankRepository dbBank, ILogging logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _dbBank = dbBank;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<BankDTO>>> GetBank()
        {
            IEnumerable<Bank> bankList = await _dbBank.GetAllAsync();
            _logger.Log("Getting all bank details", "");
            return Ok(_mapper.Map<List<BankDTO>>(bankList));
        }

        [HttpGet("accno", Name = "GetBank")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankDTO>> GetBank(int accno)
        {
            if (accno == 0)
            {
                _logger.Log("get bank details error" + accno, "error");
                return BadRequest();
            }
            var bank = await _dbBank.GetAsync(u => u.AccNo == accno);
            if (bank == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<BankDTO>(bank));
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BankDTO>> CreateBank([FromBody] BankCreateDTO createDTO)
        {
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (bankDTO.AccNo > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Bank model = _mapper.Map<Bank>(createDTO);

            //Bank model = new()
            //{
            //    //AccNo = bankDTO.AccNo,
            //    AadharCard = createDTO.AadharCard,
            //    AccName = createDTO.AccName,
            //    AccType = createDTO.AccType,
            //    PanCard = createDTO.PanCard,
            //    Address = createDTO.Address,
            //};

            await _dbBank.CreateAsync(model);
            return CreatedAtRoute("GetBank", new { accno = model.AccNo}, model);
            }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        [HttpDelete("{accno:int}", Name = "DeleteBank")]

        public async Task<IActionResult> DeleteBank(int accno)
        {
            if (accno == 0)
            {
                return BadRequest();
            }

            var bank = await _dbBank.GetAsync(u => u.AccNo == accno);   
            if(bank == null)
            {
                return NotFound();
            }
            await _dbBank.RemoveAsync(bank);
            return NoContent();
        }

        [HttpPut("{accno:int}", Name = "UpdateBank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateBank(int accno, [FromBody]BankUpdateDTO updateDTO)
        {
            if(updateDTO==null || accno !=updateDTO.AccNo)
            {
                return BadRequest();
            }

            //var bank = _db..FirstOrDefault(u => u.AccNo == accno);
            //bank.AccName = bankDTO.AccName;
            //bank.AadharCard = bankDTO.AadharCard;
            //bank.PanCard= bankDTO.PanCard;
            //bank.Address = bankDTO.Address;
            Bank model = _mapper.Map<Bank>(updateDTO);

            //Bank model = new()
            //{
            //    AccNo = updateDTO.AccNo,
            //    AadharCard = updateDTO.AadharCard,
            //    AccName = updateDTO.AccName,
            //    AccType = updateDTO.AccType,
            //    PanCard = updateDTO.PanCard,
            //    Address = updateDTO.Address,
            //};
           await _dbBank.UpdateAsync(model);
            return NoContent();
        }

        [HttpPatch("{accno:int}", Name = "UpdatePartialBank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]

        public async Task<IActionResult> UpdatePartialBank(int accno, JsonPatchDocument<BankUpdateDTO> patchDTO)
        {
            if (patchDTO == null || accno == 0)
            {
                return BadRequest();
            }

            var bank = await _dbBank.GetAsync(u => u.AccNo == accno, tracked:false);
            
       
            BankUpdateDTO bankDTO = _mapper.Map<BankUpdateDTO>(bank);

            //BankUpdateDTO bankDTO = new()
            //{
            //    AccNo = bank.AccNo,
            //    AadharCard = bank.AadharCard,
            //    AccName = bank.AccName,
            //    AccType = bank.AccType,
            //    PanCard = bank.PanCard,
            //    Address = bank.Address,
            //};

            if (bank == null)
            {
                return BadRequest();
            }

            patchDTO.ApplyTo(bankDTO, ModelState);
            Bank model = _mapper.Map<Bank>(bankDTO);
            //Bank model = new()
            //{
            //    AccNo = bankDTO.AccNo,
            //    AadharCard = bankDTO.AadharCard,
            //    AccName = bankDTO.AccName,
            //    AccType = bankDTO.AccType,
            //    PanCard = bankDTO.PanCard,
            //    Address = bankDTO.Address,
            //};
            
            await _dbBank.UpdateAsync(model);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return NoContent();
        }

    }     
}
