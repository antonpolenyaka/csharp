using ReasignAddressesCAP32.Core;
using ReasignAddressesCAP32.Model;
using System.Linq;

namespace ReasignAddressesCAP32.Utils
{
    public static class DBUtils
    {
        public static Port CreatePortCCIEC104(TedisNetEntities context)
        {
            Port portCC = context.Ports.FirstOrDefault(p => p.ShortName.Contains("IEC104"));
            if (portCC == null)
            {
                portCC = new Port()
                {
                    DeviceId = 1, // 1 CC
                    ShortName = $"IEC104",
                    Name = $"CC:IEC104",
                    ProtocolId = 4, // 4 IEC104 - Endesa
                    DriverId = 9, // 9 IEC 104
                    Priority = 1,
                    IsMaster = true,
                    IpModeId = 1 // 1 TCP
                };
                context.Ports.Add(portCC);
            }
            return portCC;
        }

        public static Port CreatePortDeviceIEC104(TedisNetEntities context,
            int deviceId,
            string deviceName)
        {
            Port portDevice = new Port()
            {
                DeviceId = deviceId,
                ShortName = "IEC104",
                Name = $"{deviceName}:IEC104",
                Address = 1,
                ProtocolId = 4, // 4 IEC104 - Endesa
                DriverId = 9, // 9 IEC 104
                IpModeId = 1, // 1 TCP
                Priority = 1,
                IsMaster = false
            };
            context.Ports.Add(portDevice);
            return portDevice;
        }

        public static Channel CreateChannelAndSave(TedisNetEntities context,
            Port portCC,
            Port portDevice)
        {
            Channel channel = new Channel()
            {
                Name = $"{portCC.Name}->{portDevice.Name}",
                MasterPortId = portCC.Id,
                SlavePortId = portDevice.Id,
                ProtocolId = 4 // 4 IEC104 - Endesa
            };
            context.Channels.Add(channel);
            context.SaveChanges();
            return channel;
        }

        public static Device CreateDeviceAndSave(TedisNetEntities context,
            string deviceName,
            int deviceTypeId)
        {
            // Device
            Device device = new Device()
            {
                Name = deviceName,
                ShortName = deviceName,
                Address = 1,
                DeviceTypeId = deviceTypeId // 30 = Others
            };
            context.Devices.Add(device);
            context.SaveChanges();

            // Sys tags
            Tag sysStateTagCnc = new Tag()
            {
                ShortName = "SYS.CONNECTION",
                Name = $"{device.Name}/SYS.CONNECTION",
                Comments = $"Estado de conexión en dispositivo {device.ShortName}",
                DeviceId = device.Id,
                TagClassId = 9 // 9 SYS.Estado Dispositivo
            };
            context.Tags.Add(sysStateTagCnc);
            Tag sysPetTagCnc = new Tag()
            {
                ShortName = "SYS.PETCONNECTION",
                Name = $"{device.Name}/SYS.PETCONNECTION",
                Comments = $"Cambio de estado de conexión en dispositivo {device.ShortName}",
                DeviceId = device.Id,
                TagClassId = 10 // 10 SYS.Cambio Estado Dispositivo
            };
            context.Tags.Add(sysPetTagCnc);
            context.SaveChanges();

            device.ConnectionStateTagId = sysStateTagCnc.Id;
            device.ConnectionPetStateTagId = sysPetTagCnc.Id;
            context.SaveChanges();

            return device;
        }

        public static string GetUniqueDeviceName(TedisNetEntities context,
            string baseDeviceName)
        {
            string deviceName = baseDeviceName;
            int count = 0;
            while (context.Devices.Any(d => d.ShortName == deviceName))
            {
                count++;
                deviceName = $"{baseDeviceName} {count}";
            }
            return deviceName;
        }

        internal static Tag CreateTagIEC104AndSave(TedisNetEntities context,
            int ioa,
            string ioaFormat,
            string deviceName,
            int deviceId,
            int tagClassId,
            int? sourceValueTypeId,
            double? scale,
            TagClassType classType)
        {
            string prefix = classType.ToString();
            string tagShortName = $"{prefix}.{ioa.ToString(ioaFormat)}";
            Tag tag = new Tag()
            {
                Name = $"{deviceName}/{tagShortName}",
                ShortName = tagShortName,
                TelecontrolAddress = ioa,
                DeviceId = deviceId,
                TagClassId = tagClassId,
                StoreInterval = null,
                SourceValueTypeId = sourceValueTypeId
            };
            context.Tags.Add(tag);
            context.SaveChanges();

            if (classType == TagClassType.DO || classType == TagClassType.CDO || classType == TagClassType.AO)
            {
                context.Commands.Add(new Command() { TagId = tag.Id });
                context.SaveChanges();
            }

            if (classType == TagClassType.AI && scale.HasValue)
            {
                // Scale = 1 is incorrect, because we have input 1 and result 1, not appy any scale and not save it
                if (scale != 1)
                {
                    context.TagScales.Add(new TagScale()
                    {
                        InputValue = 1,
                        ResultValue = scale.Value,
                        TagId = tag.Id
                    });
                    context.SaveChanges();
                }
            }

            return tag;
        }
    }
}
