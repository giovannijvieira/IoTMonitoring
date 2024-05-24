using IoTMonitoring.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IoTMonitoring.Repositories
{
    public class DeviceRepository : IDeviceRepository
    {
        private List<Device> _devices;

        public DeviceRepository()
        {
            _devices = new List<Device>();
            _devices.Add(new Device { Identifier = "Device1", Description = "First Device", Manufacturer = "XYZ Corp", Url = "http://example.com" });
        }

        public async Task<IEnumerable<Device>> GetAllAsync()
        {
            return await Task.FromResult(_devices);
        }

        public async Task<Device> GetByIdAsync(string identifier)
        {
            var device = _devices.FirstOrDefault(d => d.Identifier == identifier);
            if (device == null)
            {
                throw new KeyNotFoundException("Device not found with identifier: " + identifier);
            }
            return await Task.FromResult(device);
        }

        public async Task AddAsync(Device entity)
        {
            _devices.Add(entity);
            await Task.CompletedTask;
        }

        public async Task UpdateAsync(Device entity)
        {
            var device = _devices.FirstOrDefault(d => d.Identifier == entity.Identifier);
            if (device != null)
            {
                device.Description = entity.Description;
                device.Manufacturer = entity.Manufacturer;
                device.Url = entity.Url;
                device.Commands = entity.Commands;
            }
            await Task.CompletedTask;
        }

        public async Task DeleteAsync(string identifier)
        {
            var device = _devices.FirstOrDefault(d => d.Identifier == identifier);
            if (device != null)
            {
                _devices.Remove(device);
            }
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<Device>> GetByManufacturerAsync(string manufacturer)
        {
            var devices = _devices.Where(d => d.Manufacturer == manufacturer);
            return await Task.FromResult(devices);
        }
    }
}
