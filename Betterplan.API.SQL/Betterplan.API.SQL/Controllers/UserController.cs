using Betterplan.API.Data.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Betterplan.API.SQL.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private readonly IDataRepository _dataRepository;

        public UserController(IDataRepository dataRepository)
        {
            _dataRepository = dataRepository;
        }

        [Route("/{id:int}")]
        [HttpGet]
        public async Task<IActionResult> GetUser (int id) 
        {
            return Ok(await _dataRepository.GetUser(id));
        }

 
        [Route("/{id:int}/summary")]
        [HttpGet]
        public async Task<IActionResult> GetSummary(int id)
        {
            return Ok(await _dataRepository.GetSummaries(id));
        }

        [Route("/{id}/summary/{date}")]
        [HttpGet]
        public async Task<IActionResult> GetSummaryByDate(int id, DateTime date)
        {
            return Ok(await _dataRepository.GetSumaryByDate(id, date));
        }

        [Route("/{id:int}/goals")]
        [HttpGet]
        public async Task<IActionResult> GetGoals(int id)
        {
            return Ok(await _dataRepository.GetGoals(id));
        }

        [Route("/{id}/goals/{goalid}")]
        [HttpGet]
        public async Task<IActionResult> GetGoalDetail(int id, int goalid)
        {
            return Ok(await _dataRepository.GetGoalDetail(id, goalid));
        }
    }
}
