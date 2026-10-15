using System;
using RepairShop1.Interfaces;
using RepairShop1.Services;
using RepairShop1.Models;

namespace RepairShop1 
{ 

    public class Program
    {
        // إنشاء كائن من مركز الصيانة لإدارة الأجهزة
        private static readonly RepairCenter repairCenter = new();

        // الدالة الرئيسية التي يبدأ منها تشغيل البرنامج
        static void Main()
        {
            // إضافة أجهزة تجريبية عند بدء التشغيل
            repairCenter.AddDevice(new Refrigerator("FRG001", 300));
            repairCenter.AddDevice(new WashingMachine("WSH002", "Front", 7));
            repairCenter.AddDevice(new AirConditioner("AC0003", 18000, "Split"));

            // عرض القائمة الرئيسية
            ShowMainMenu();
        }

        // دالة عرض القائمة الرئيسية للتطبيق
        static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("===================================");
                Console.WriteLine("   Home Appliance Repair System");
                Console.WriteLine("===================================");

                Console.WriteLine("\nPlease choose an option:");
                Console.WriteLine("1. Display all devices");
                Console.WriteLine("2. Add a new device");
                Console.WriteLine("3. Update device status");
                Console.WriteLine("4. Find a device");
                Console.WriteLine("5. Calculate total repair cost");
                Console.WriteLine("6. Count devices by status");
                Console.WriteLine("7. Display total number of devices");
                Console.WriteLine("0. Exit");

                Console.Write("\nEnter your choice: ");
                string choice = Console.ReadLine() ?? "";

                switch (choice)
                {
                    case "1":
                        DisplayAllDevicesOption();
                        break;

                    case "2":
                        AddDeviceOption();
                        break;

                    case "3":
                        UpdateDeviceStatusOption();
                        break;

                    case "4":
                        FindDeviceOption();
                        break;

                    case "5":
                        CalculateTotalRepairCostOption();
                        break;
                    case "6":
                        CountDevicesByStatusOption();
                        break;
                    case "7":
                        DisplayTotalDevicesOption();
                        break;

                    case "0":
                        Console.WriteLine(
                            "\nThank you for using our system. Goodbye!");
                        return;

                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        // دالة عرض جميع الأجهزة المسجلة في مركز الصيانة
        static void DisplayAllDevicesOption()
        {
            Console.Clear();
            Console.WriteLine("--- All Devices ---");

            repairCenter.DisplayAllDevices();
        }

        // دالة إضافة جهاز جديد إلى مركز الصيانة
        static void AddDeviceOption()
        {
            Console.Clear();
            Console.WriteLine("--- Add a New Device ---");

            Console.WriteLine("Choose the device type:");
            Console.WriteLine("1. Refrigerator");
            Console.WriteLine("2. Washing Machine");
            Console.WriteLine("3. Air Conditioner");

            Console.Write("Enter the device type number: ");
            string deviceTypeChoice = Console.ReadLine() ?? "";

            // تحديد البادئة المطلوبة حسب نوع الجهاز
            string requiredPrefix = deviceTypeChoice switch
            {
                "1" => "FRG",
                "2" => "WSH",
                "3" => "AC",
                _ => ""
            };

            if (requiredPrefix == "")
            {
                Console.WriteLine("Invalid device type.");
                return;
            }

            // إدخال معرف الجهاز والتحقق من صحته
            Console.Write(
                $"Enter device ID starting with {requiredPrefix}: ");

            string deviceId = (Console.ReadLine() ?? "")
                .Trim()
                .ToUpperInvariant();

            if (!DataValidator.IsValidDeviceId(deviceId) ||
                !deviceId.StartsWith(requiredPrefix))
            {
                Console.WriteLine(
                    $"Invalid device ID. It must start with {requiredPrefix} " +
                    "and follow the required format.");
                return;
            }

            // منع إضافة جهاز يحمل معرفًا مستخدمًا سابقًا
            if (repairCenter.GetDeviceById(deviceId) != null)
            {
                Console.WriteLine(
                    "A device with this ID already exists.");
                return;
            }

            IDevice? newDevice = null;

            // إنشاء الجهاز وفقًا لنوعه والبيانات المطلوبة
            switch (deviceTypeChoice)
            {
                case "1":
                    Console.Write(
                        "Enter refrigerator cooling capacity: ");

                    if (int.TryParse(
                        Console.ReadLine(), out int coolingCapacity)
                        && coolingCapacity > 0)
                    {
                        newDevice = new Refrigerator(
                            deviceId, coolingCapacity);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Invalid cooling capacity.");
                        return;
                    }
                    break;

                case "2":
                    Console.WriteLine("Choose the loading type:");
                    Console.WriteLine("1. Front");
                    Console.WriteLine("2. Top");
                    Console.Write("Enter loading type: ");

                    string loadingChoice = Console.ReadLine() ?? "";

                    string loadingType = loadingChoice == "1" ? "Front" :
                                         loadingChoice == "2" ? "Top" : "";

                    if (loadingType == "")
                    {
                        Console.WriteLine("Invalid loading type.");
                        return;
                    }

                    Console.Write(
                        "Enter washing machine capacity in kilograms: ");

                    if (int.TryParse(
                        Console.ReadLine(), out int capacityKG)
                        && capacityKG > 0)
                    {
                        newDevice = new WashingMachine(
                            deviceId, loadingType, capacityKG);
                    }
                    else
                    {
                        Console.WriteLine(
                            "Invalid washing machine capacity.");
                        return;
                    }
                    break;

                case "3":
                    Console.Write("Enter cooling capacity in BTU: ");

                    if (!int.TryParse(
                        Console.ReadLine(), out int btus)
                        || btus <= 0)
                    {
                        Console.WriteLine("Invalid BTU value.");
                        return;
                    }

                    Console.WriteLine(
                        "Choose the air conditioner type:");
                    Console.WriteLine("1. Split");
                    Console.WriteLine("2. Central");
                    Console.WriteLine("3. Window");
                    Console.Write("Enter air conditioner type: ");

                    string acChoice = Console.ReadLine() ?? "";

                    string acType = acChoice == "1" ? "Split" :
                                    acChoice == "2" ? "Central" :
                                    acChoice == "3" ? "Window" : "";

                    if (acType == "")
                    {
                        Console.WriteLine(
                            "Invalid air conditioner type.");
                        return;
                    }

                    newDevice = new AirConditioner(
                        deviceId, btus, acType);
                    break;
            }

            // إضافة الجهاز بعد نجاح التحقق من جميع البيانات
            if (newDevice != null)
            {
                repairCenter.AddDevice(newDevice);
                Console.WriteLine(
                    "Device added successfully!");
            }
        }

        // دالة تحديث حالة جهاز مسجل في المركز
        static void UpdateDeviceStatusOption()
        {
            Console.Clear();
            Console.WriteLine("--- Update Device Status ---");

            Console.Write("Enter device ID: ");

            string deviceId = (Console.ReadLine() ?? "")
                .Trim()
                .ToUpperInvariant();

            IDevice? device = repairCenter.GetDeviceById(deviceId);

            if (device == null)
            {
                Console.WriteLine(
                    $"Device with ID {deviceId} was not found.");
                return;
            }

            Console.WriteLine(
                $"Current status: {device.RepairStatus}");

            Console.WriteLine("\nAvailable statuses:");
            Console.WriteLine("1. Waiting for repair");
            Console.WriteLine("2. Under Inspection");
            Console.WriteLine("3. Waiting for Spare Parts");
            Console.WriteLine("4. Under Repair");
            Console.WriteLine("5. Repaired");

            Console.Write("\nEnter the new status number: ");

            string statusChoice = Console.ReadLine() ?? "";

            // تحويل اختيار المستخدم إلى الحالة المناسبة
            string newStatus = statusChoice switch
            {
                "1" => "Waiting for repair",
                "2" => "Under Inspection",
                "3" => "Waiting for Spare Parts",
                "4" => "Under Repair",
                "5" => "Repaired",
                _ => ""
            };

            if (newStatus == "")
            {
                Console.WriteLine("Invalid status choice.");
                return;
            }

            // تحديث حالة الجهاز
            repairCenter.ChangeDeviceStatus(deviceId, newStatus);

            Console.WriteLine(
                "Device status updated successfully!");
        }

        // دالة البحث عن جهاز باستخدام معرفه
        static void FindDeviceOption()
        {
            Console.Clear();
            Console.WriteLine("--- Find a Device ---");

            Console.Write("Enter device ID: ");

            string deviceId = (Console.ReadLine() ?? "")
                .Trim()
                .ToUpperInvariant();

            IDevice? device = repairCenter.GetDeviceById(deviceId);

            if (device == null)
            {
                Console.WriteLine(
                    $"Device with ID {deviceId} was not found.");
                return;
            }

            Console.WriteLine("\n--- Device Details ---");
            Console.WriteLine($"Device ID: {device.DeviceId}");
            Console.WriteLine($"Device Type: {device.DeviceType}");
            Console.WriteLine($"Repair Status: {device.RepairStatus}");
            Console.WriteLine(
                $"Estimated repair cost: {device.CalculateRepairCost():C}");
        }

        // دالة حساب إجمالي تكاليف إصلاح جميع الأجهزة
        static void CalculateTotalRepairCostOption()
        {
            Console.Clear();
            Console.WriteLine("--- Calculate Total Repair Cost ---");

            decimal totalCost =
                repairCenter.CalculateTotalRepairCost();

            Console.WriteLine(
                $"Total repair cost: {totalCost:C}");
        }
       
        // دالة عرض عدد الأجهزة حسب حالة الصيانة
        static void CountDevicesByStatusOption() 
        {
            Console.Clear();
            Console.WriteLine("--- Count Devices by Status ---");

            Console.WriteLine("1. Waiting for repair");
            Console.WriteLine("2. Under Inspection");
            Console.WriteLine("3. Waiting for Spare Parts");
            Console.WriteLine("4. Under Repair");
            Console.WriteLine("5. Repaired");

            Console.Write("\nEnter the status number: ");
            string choice = Console.ReadLine() ?? "";

            string status = choice switch
            {
                "1" => "Waiting for repair",
                "2" => "Under Inspection",
                "3" => "Waiting for Spare Parts",
                "4" => "Under Repair",
                "5" => "Repaired",
                _ => ""
            };

            if (status == "")
            {
                Console.WriteLine("Invalid status choice.");
                return;
            }

            // حساب عدد الأجهزة حسب الحالة المختارة
            int count = DataValidator.GetDeviceCountByStatus(
                repairCenter.GetAllDevices(), status);

            Console.WriteLine($"Number of devices with status '{status}': {count}");

            // عرض عدد الأجهزة التي تنتظر الصيانة
            int pendingCount = DataValidator.TotalPendingDevices(
                repairCenter.GetAllDevices());

            Console.WriteLine(
                $"Total devices waiting for repair: {pendingCount}");
        }

        private static void DisplayTotalDevicesOption()
        {
            Console.WriteLine(
                $"Total number of devices: {RepairCenter.TotalDevices}");
        }
    }
}
