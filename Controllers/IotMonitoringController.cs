using IoTMonitoring.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System;

namespace IoTMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class IotMonitoringController : ControllerBase
    {
        private readonly TelnetService _telnetService;

        public IotMonitoringController(TelnetService telnetService)
        {
            _telnetService = telnetService;
        }

        /// <summary>
        /// Enviar um comando para o dispositivo cadastrado.
        /// </summary>
        /// <param name="model">Modelo contendo o hostname, a porta e o comando a ser enviado.</param>
        /// <returns>Resposta do dispositivo.</returns>
        /// <response code="200">Comando executado com sucesso e resposta recebida.</response>
        /// <response code="400">Solicitação inválida. Hostname e comando são obrigatórios.</response>
        /// <response code="500">Hostname não disponível: Ocorreu falha na tentativa para se conectar ao dispositivo.</response>
        [HttpPost("sendCommand")]
        [ProducesResponseType(typeof(string), 200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> SendCommand([FromBody] TelnetCommandModel model)
        {
            if (string.IsNullOrEmpty(model.Hostname) || string.IsNullOrEmpty(model.Command))
            {
                return BadRequest("Chave:hostname e chave:command são obrigatórias.");
            }

            try
            {
                var response = await _telnetService.SendCommandAsync(model.Hostname, model.Port, model.Command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao host {model.Hostname}:{model.Port}");
                Console.WriteLine($"Mensagem de erro: {ex.Message}");
                Console.WriteLine($"Stack trace: {ex.StackTrace}");
                return StatusCode(500, "Hostname não disponível: Ocorreu falha na tentativa para se conectar ao dispositivo.");
            }
        }
    }

    public class TelnetCommandModel
    {
        public string Hostname { get; set; } = string.Empty;
        public int Port { get; set; } = 23; 
        public string Command { get; set; } = string.Empty;
    }
}
