using IoTMonitoring.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IoTMonitoring.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DevicesController : ControllerBase
    {
        private static List<Device> devices = new List<Device>();

        /// <summary>
        /// Retornar uma lista contendo os identificadores dos dispositivos cadastrados na plataforma
        /// </summary>
        /// <returns>Lista de identificadores dos dispositivos</returns>
        /// <response code="200">Requisição executada com sucesso</response>
        /// <response code="401">As credenciais fornecidas pelo usuário são inexistentes ou inválidas</response>
        [HttpGet]
        [ProducesResponseType(typeof(DeviceList), 200)]
        [ProducesResponseType(401)]
        public IActionResult GetDevices()
        {
            if (User?.Identity?.IsAuthenticated != true) // Supondo que a autenticação seja necessária
            {
                return Unauthorized(new { Mensagem = "As credenciais fornecidas pelo usuário são inexistentes ou inválidas" });
            }

            var deviceIdentifiers = devices.Select(d => d.Identifier).ToList();
            return Ok(new DeviceList { Devices = deviceIdentifiers });
        }

        /// <summary>
        /// Cadastrar um novo dispositivo na plataforma
        /// </summary>
        /// <param name="device">Detalhes do dispositivo sendo cadastrados</param>
        /// <returns>URL de acesso aos dados do dispositivo recém cadastrado</returns>
        /// <response code="201">Requisição realizada com sucesso</response>
        [HttpPost]
        [ProducesResponseType(typeof(Device), 201)]
        public IActionResult CreateDevice([FromBody] Device device)
        {
            device.Identifier = Guid.NewGuid().ToString();
            devices.Add(device);
            var locationUrl = Url.Action(nameof(GetDeviceById), new { id = device.Identifier });
            return Created(locationUrl, new { Message = "Requisição realizada com sucesso", Location = locationUrl });
        }

        /// <summary>
        /// Retornar os detalhes de um dispositivo
        /// </summary>
        /// <param name="id">Identificador do dispositivo para o qual os detalhes devem ser retornados</param>
        /// <returns>Detalhes do dispositivo</returns>
        /// <response code="200">Requisição realizada com sucesso</response>
        /// <response code="404">Dispositivo não encontrado</response>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(404)]
        public IActionResult GetDeviceById(string id)
        {
            var device = devices.FirstOrDefault(d => d.Identifier == id);
            if (device == null)
            {
                return NotFound(new { Message = "Dispositivo não encontrado" });
            }
            return Ok(new { Message = "Requisição realizada com sucesso", Device = device });
        }

        /// <summary>
        /// Atualizar os dados de um dispositivo
        /// </summary>
        /// <param name="id">Identificador do dispositivo para o qual os detalhes devem ser atualizados</param>
        /// <param name="updatedDevice">Detalhes atualizados do dispositivo</param>
        /// <returns>Detalhes atualizados do dispositivo</returns>
        /// <response code="200">Requisição realizada com sucesso</response>
        /// <response code="401">A solicitação não foi realizada pelo proprietário do dispositivo</response>
        /// <response code="404">Dispositivo não encontrado</response>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult UpdateDevice(string id, [FromBody] Device updatedDevice)
        {
            var device = devices.FirstOrDefault(d => d.Identifier == id);
            if (device == null)
            {
                return NotFound(new { Message = "Dispositivo não encontrado" });
            }

            device.Description = updatedDevice.Description;
            device.Manufacturer = updatedDevice.Manufacturer;
            device.Url = updatedDevice.Url;
            device.Commands = updatedDevice.Commands;

            return Ok(new { Message = "Requisição realizada com sucesso", Device = device });
        }

        /// <summary>
        /// Remover os detalhes de um dispositivo
        /// </summary>
        /// <param name="id">Identificador do dispositivo para o qual os detalhes devem ser removidos</param>
        /// <returns>Detalhes do dispositivo removido</returns>
        /// <response code="200">Requisição realizada com sucesso</response>
        /// <response code="401">A solicitação não foi realizada pelo proprietário do dispositivo</response>
        /// <response code="404">Dispositivo não encontrado</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(Device), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(404)]
        public IActionResult DeleteDevice(string id)
        {
            var device = devices.FirstOrDefault(d => d.Identifier == id);
            if (device == null)
            {
                return NotFound(new { Message = "Dispositivo não encontrado" });
            }

            devices.Remove(device);
            return Ok(new { Message = "Requisição realizada com sucesso", Device = device });
        }
    }
}
