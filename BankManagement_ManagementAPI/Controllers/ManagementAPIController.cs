using AutoMapper;
using BankManagement_ManagementAPI.Data;
using BankManagement_ManagementAPI.Logging;
using BankManagement_ManagementAPI.Models;
using BankManagement_ManagementAPI.Models.DTO;
using BankManagement_ManagementAPI.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

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
        protected APIResponse _response;
        private readonly IBankRepository _dbBank;
        private readonly IMapper _mapper;
        public ManagementAPIController(IBankRepository dbBank, ILogging logger, IMapper mapper)
        {
            _mapper = mapper;
            _logger = logger;
            _dbBank = dbBank;
            this._response = new();
        }

        [HttpGet]
        [Authorize]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBank()
        {
            try
            {

                IEnumerable<Bank> bankList = await _dbBank.GetAllAsync();
                _logger.Log("Getting all bank details", "");
                _response.Result = _mapper.Map<List<BankDTO>>(bankList);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess= false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };
                    
            }
            return _response;
        }
        [Authorize(Roles = "admin")]
        [HttpGet("accno", Name = "GetBank")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBank(int accno)
        {
            try
            {

                if (accno == 0)
                {
                    _logger.Log("get bank details error" + accno, "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var bank = await _dbBank.GetAsync(u => u.AccNo == accno);
                if (bank == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<BankDTO>(bank);
                _response.StatusCode = HttpStatusCode.OK;
                return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };

            }
            return _response;

        }

        [HttpPost]
        [Authorize(Roles ="admin")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateBank([FromBody] BankCreateDTO createDTO)
        {
            try
            {

            
            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (bankDTO.AccNo > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            Bank bank = _mapper.Map<Bank>(createDTO);

            //Bank model = new()
            //{
            //    //AccNo = bankDTO.AccNo,
            //    AadharCard = createDTO.AadharCard,
            //    AccName = createDTO.AccName,
            //    AccType = createDTO.AccType,
            //    PanCard = createDTO.PanCard,
            //    Address = createDTO.Address,
            //};

            await _dbBank.CreateAsync(bank);

            _response.Result = _mapper.Map<BankDTO>(bank);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetBank", new { accno = bank.AccNo}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        [HttpDelete("{accno:int}", Name = "DeleteBank")]
        [Authorize(Roles = "CUSTOM")]
        public async Task<ActionResult<APIResponse>> DeleteBank(int accno)
        {
            try
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
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess= true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [HttpPut("{accno:int}", Name = "UpdateBank")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateBank(int accno, [FromBody]BankUpdateDTO updateDTO)
        {
            try
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
            _response.StatusCode = HttpStatusCode.NoContent;
            _response.IsSuccess= true;
            return Ok(_response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };

            }
            return _response;
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
