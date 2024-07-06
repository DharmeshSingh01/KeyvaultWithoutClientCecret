using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNetAPIDS.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TeamController : ControllerBase
    {
        private readonly ITeamRepository _teamRepository;
        public TeamController(ITeamRepository teamRepository)
        {
            _teamRepository = teamRepository;
        }
        [HttpGet( Name ="GetTeams")]
        public async Task<IActionResult> GetTeams()
        {
            try
            {
                var result = await _teamRepository.GetTeams();
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById(int id)
        {
            try
            {
                var result = await _teamRepository.GetTeamById(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPost]
        public async Task<IActionResult> AddTeam(Team team)
        {
            try
            {
                var result= await _teamRepository.AddTeam(team);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpPut(Name ="UpdateTeam")]
        public async Task<IActionResult> UpdateTeam(Team team)
        {
            try
            {
                 var result= await _teamRepository.UpdateTeam(team);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTeam(int id)
        {
            try
            {
                var result= await _teamRepository.DeleteTeam(id);
                return Ok(result);
            }
            catch(Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
