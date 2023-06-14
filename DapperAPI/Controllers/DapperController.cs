using DapperAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DapperAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DapperController : ControllerBase
    {
        private readonly IDapperRepository dapperRepository;

        public DapperController(IDapperRepository _dapperRepository)
        {
            dapperRepository = _dapperRepository;
        }

        [HttpGet("GetPlayer")]
        public async Task<IActionResult> GetPlayer(int playerId)
        {
            try
            {
                var player = await dapperRepository.GetPlayer(playerId);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }


        [HttpGet("GetPlayers")]
        public async Task<IActionResult> GetPlayers()
        {
            try
            {
                var player = await dapperRepository.GetPlayers();

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpGet("GetPlayerInfo")]
        public async Task<IActionResult> GetPlayerInfo(int id)
        {
            try
            {
                var player = await dapperRepository.GetPlayerInfo(id);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPost("CreatePlayer")]
        public async Task<IActionResult> CreatePlayer(Player newPlayer)
        {
            try
            {
                var player = await dapperRepository.CreatePlayer(newPlayer);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpPut("UpdatePlayer")]
        public async Task<IActionResult> UpdatePlayer(Player newPlayer)
        {
            try
            {
                var player = await dapperRepository.UpdatePlayer(newPlayer);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

        [HttpDelete("DeletePlayer")]
        public async Task<IActionResult> DeletePlayer(Player newPlayer)
        {
            try
            {
                var player = await dapperRepository.DeletePlayer(newPlayer.PlayerId);

                return Ok(player);
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }

    }
}
