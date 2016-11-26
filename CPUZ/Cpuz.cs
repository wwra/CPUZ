namespace CPUZ
{
    using System;
    using System.Management;

    internal static class Cpuz
    {
        public static void GetAll()
        {
            GetManagmentObject("CIM_Keyboard", new[] { "Name", "SystemName" });
            GetManagmentObject("CIM_PointingDevice", new[] { "Name", "Manufacturer" });
            GetManagmentObject("Win32_CDROMDrive", new[] { "Name", "Manufacturer" });
            GetManagmentObject("Win32_DiskDrive",
                new[] { "Name", "Manufacturer", "InterfaceType", "MediaType", "TotalCylinders", "TotalSectors" });
            GetManagmentObject("Win32_BIOS", new[] { "Name", "Manufacturer", "SMBIOSBIOSVersion", "Version" });
            GetManagmentObject("Win32_Bus", new[] { "Name", "BusType" });
            GetManagmentObject("Win32_CacheMemory", new[] { "Name", "CacheType", "MaxCacheSize" });
            GetManagmentObject("Win32_MotherboardDevice", new[] { "Name", "PrimaryBusType" });
            GetManagmentObject("Win32_Processor", new[] { "Name", "Manufacturer", "Architecture", "Family" });
            GetManagmentObject("Win32_SoundDevice", new[] { "Name", "Manufacturer" });
            GetManagmentObject("Win32_USBController", new[] { "Name", "Manufacturer" });
            GetManagmentObject("Win32_NetworkAdapter", new[] { "Name", "Manufacturer", "MACAddress" });
            GetManagmentObject("CIM_Printer", new[] { "Name", "PrintProcessor" });
            GetManagmentObject("CIM_DesktopMonitor", new[] { "Name", "MonitorManufacturer", "MonitorType" });
            GetManagmentObject("Win32_VideoController",
                new[] { "Name", "DriverVersion", "VideoModeDescription", "VideoProcessor" });
        }

        private static void GetManagmentObject(string fromWmiClass, string[] data)
        {
            Header(fromWmiClass);
            //TODO: Error when SELECT specific values: string.Join(",", data)
            WqlObjectQuery wqlQuery = new WqlObjectQuery("SELECT * FROM " + fromWmiClass);
            var searcher = new ManagementObjectSearcher(wqlQuery).Get();
            foreach (ManagementObject o in searcher)
            {
                o.ShowInfo(data);
            }
        }

        private static void ShowInfo(this ManagementObject searcher, string[] data)
        {
            foreach (string value in data)
            {
                Console.WriteLine("{0,-20}{1,-10}", value, searcher[value]);
            }
            Console.WriteLine("--------------------");
        }

        private static void Header(string header)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("{0}:", header);
            Console.ResetColor();
        }
    }
}