namespace IoTMonitoring.Models
{
    /// <summary>
    /// Representa um dispositivo dentro da plataforma.
    /// </summary>
    public class Device
    {
        /// <summary>
        /// Identificador único do dispositivo.
        /// </summary>
        public string Identifier { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do dispositivo, incluindo detalhes de uso e capacidades.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Fabricante do dispositivo.
        /// </summary>
        public string Manufacturer { get; set; } = string.Empty;

        /// <summary>
        /// URL para acesso ao dispositivo ou sua documentação.
        /// </summary>
        public string Url { get; set; } = string.Empty;

        /// <summary>
        /// Lista de comandos que o dispositivo pode executar.
        /// </summary>
        public List<CommandDescription> Commands { get; set; } = new List<CommandDescription>();
    }

    /// <summary>
    /// Descrição de um comando que pode ser executado pelo dispositivo.
    /// </summary>
    public class CommandDescription
    {
        /// <summary>
        /// Operação que o comando executa.
        /// </summary>
        public string Operation { get; set; } = string.Empty;

        /// <summary>
        /// Descrição detalhada do comando e de sua operação.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Detalhes do comando, incluindo a string de comando e parâmetros.
        /// </summary>
        public Command Command { get; set; } = new Command();

        /// <summary>
        /// Resultado esperado após a execução do comando.
        /// </summary>
        public string Result { get; set; } = string.Empty;

        /// <summary>
        /// Formato dos dados retornados pelo comando.
        /// </summary>
        public string Format { get; set; } = string.Empty;
    }

    /// <summary>
    /// Comando específico que pode ser enviado para um dispositivo.
    /// </summary>
    public class Command
    {
        /// <summary>
        /// Sequência de bytes enviados como parte do comando.
        /// </summary>
        public string CommandString { get; set; } = string.Empty;

        /// <summary>
        /// Parâmetros que podem ser usados com o comando.
        /// </summary>
        public List<Parameter> Parameters { get; set; } = new List<Parameter>();
    }

    /// <summary>
    /// Parâmetro que pode ser passado junto a um comando.
    /// </summary>
    public class Parameter
    {
        /// <summary>
        /// Nome do parâmetro.
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// Descrição do parâmetro, incluindo sua função e valores aceitáveis.
        /// </summary>
        public string Description { get; set; } = string.Empty;
    }

    /// <summary>
    /// Lista de dispositivos registrados na plataforma.
    /// </summary>
    public class DeviceList
    {
        /// <summary>
        /// Lista dos identificadores dos dispositivos.
        /// </summary>
        public List<string> Devices { get; set; } = new List<string>();
    }
    
}
