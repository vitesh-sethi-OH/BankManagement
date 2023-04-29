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
using System.Net;

namespace BankManagement_ManagementAPI.Controllers
{
    [Route("api/ManagementNumberAPI")]
    [ApiController]
    public class ManagementLockerAPIController : ControllerBase
    {

        private readonly ILogging _logger;

        //public ManagementAPIController(ILogging logger)
        //{
        //    _logger = logger;        
        //}
        protected APIResponse _response;
        private readonly IBankLockerRepository _dbBankLocker;
        private readonly IBankRepository _dbBank;
        private readonly IMapper _mapper;
        public ManagementLockerAPIController(IBankLockerRepository dbBankLocker, ILogging logger, IMapper mapper, IBankRepository _bank)
        {
            _mapper = mapper;
            _logger = logger;
            _dbBankLocker = dbBankLocker;
            this._response = new();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<APIResponse>> GetBank()
        {
            try
            {

                IEnumerable<BankLocker> bankLockerList = await _dbBankLocker.GetAllAsync();
                _logger.Log("Getting all bank details", "");
                _response.Result = _mapper.Map<List<BankLockerDTO>>(bankLockerList);
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

        [HttpGet("accno", Name = "GetBankLocker")]
        //[ProducesResponseType(200)]
        //[ProducesResponseType(404)]
        //[ProducesResponseType(400)]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> GetBankLocker(int accno)
        {
            try
            {

                if (accno == 0)
                {
                    _logger.Log("get bank details error" + accno, "error");
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var bankLocker = await _dbBankLocker.GetAsync(u => u.AccountNumber == accno);
                if (bankLocker == null)
                {
                    _response.StatusCode = HttpStatusCode.NotFound;
                    return NotFound(_response);
                }
                _response.Result = _mapper.Map<BankLockerDTO>(bankLocker);
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
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<APIResponse>> CreateBankLocker([FromBody] BankLockerCreateDTO createDTO)
        {
            try
            {

            if(await _dbBankLocker.GetAsync(u => u.AccountNumber ==createDTO.AccountNumber) != null)
                {
                    ModelState.AddModelError("CustomError", "Account Number Already Exists");
                    return BadRequest(ModelState);
                }

            if(await _dbBank.GetAsync(u=>u.AccNo == createDTO.BankId) == null)
                {
                    ModelState.AddModelError("CustomError", "Account Number Invalid");
                    return BadRequest(ModelState);
                }

            if (createDTO == null)
            {
                return BadRequest(createDTO);
            }
            //if (bankDTO.AccNo > 0)
            //{
            //    return StatusCode(StatusCodes.Status500InternalServerError);
            //}

            BankLocker bankLocker = _mapper.Map<BankLocker>(createDTO);

            //Bank model = new()
            //{
            //    //AccNo = bankDTO.AccNo,
            //    AadharCard = createDTO.AadharCard,
            //    AccName = createDTO.AccName,
            //    AccType = createDTO.AccType,
            //    PanCard = createDTO.PanCard,
            //    Address = createDTO.Address,
            //};

            await _dbBankLocker.CreateAsync(bankLocker);

            _response.Result = _mapper.Map<BankLockerDTO>(bankLocker);
            _response.StatusCode = HttpStatusCode.Created;
            return CreatedAtRoute("GetBank", new { accno = bankLocker.AccountNumber}, _response);
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages
                    = new List<string>() { ex.ToString() };

            }
            return _response;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
       
        [HttpDelete("{accno:int}", Name = "DeleteBankLocker")]

        public async Task<ActionResult<APIResponse>> DeleteBank(int accno)
        {
            try
            {

            if (accno == 0)
            {
                return BadRequest();
            }

            var bankLocker = await _dbBankLocker.GetAsync(u => u.AccountNumber == accno);   
            if(bankLocker == null)
            {
                return NotFound();
            }
            await _dbBankLocker.RemoveAsync(bankLocker);
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

        [HttpPut("{accno:int}", Name = "UpdateBankLocker")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<APIResponse>> UpdateBankLocker(int accno, [FromBody]BankLockerUpdateDTO updateDTO)
        {
            try
            {

            
            if(updateDTO==null || accno !=updateDTO.AccountNumber)
            {
                return BadRequest();
            }
                if (await _dbBank.GetAsync(u => u.AccNo == updateDTO.BankId) == null)
                {
                    ModelState.AddModelError("CustomError", "Account Number Invalid");
                    return BadRequest(ModelState);
                }

                //var bank = _db..FirstOrDefault(u => u.AccNo == accno);
                //bank.AccName = bankDTO.AccName;
                //bank.AadharCard = bankDTO.AadharCard;
                //bank.PanCard= bankDTO.PanCard;
                //bank.Address = bankDTO.Address;
                BankLocker model = _mapper.Map<BankLocker>(updateDTO);

            //Bank model = new()
            //{
            //    AccNo = updateDTO.AccNo,
            //    AadharCard = updateDTO.AadharCard,
            //    AccName = updateDTO.AccName,
            //    AccType = updateDTO.AccType,
            //    PanCard = updateDTO.PanCard,
            //    Address = updateDTO.Address,
            //};
           await _dbBankLocker.UpdateAsync(model);
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
    }     
}
