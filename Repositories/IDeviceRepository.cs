using IoTMonitoring.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoTMonitoring.Repositories
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Task<IEnumerable<Device>> GetByManufacturerAsync(string manufacturer);
    }
}
