using System.Collections.Generic;
using System.Management;

namespace CloneDataPCApp.Utilities
{
    public class USBDeviceInfo
    {
        public string DeviceID { get; private set; }
        public string Description { get; private set; }
        public string PortLocation { get; private set; }

        public USBDeviceInfo(string deviceID, string description, string port)
        {
            DeviceID = deviceID;
            Description = description;
            PortLocation = port;
        }

        public static List<USBDeviceInfo> GetUSBDevices()
        {
            List<USBDeviceInfo> devices = new List<USBDeviceInfo>();

            ManagementObjectCollection collection;

            using (var searcher = new ManagementObjectSearcher(@"Select * From Win32_PnPEntity"))
            {
                collection = searcher.Get();
            }

            foreach (var device in collection)
            {
                devices.Add(new USBDeviceInfo((string)device.GetPropertyValue("DeviceID"), (string)device.GetPropertyValue("Description"), ""));
            }
            collection.Dispose();

            return devices;
        }
    }
}