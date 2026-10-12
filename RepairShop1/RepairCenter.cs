using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairShop1
{
   
    // فئة لإدارة مركز الصيانة
    public class RepairCenter
        {
            // قائمة لتخزين كائنات الأجهزة، باستخدام الواجهة IDevice لتحقيق تعدد الأشكال
            private readonly List<IDevice> _devices;

            public RepairCenter()
            {
                _devices = new List<IDevice>();
            }

            // دالة لإضافة جهاز جديد إلى المركز
            public void AddDevice(IDevice device)
            {
                if (device == null)
                {
                    throw new ArgumentNullException(nameof(device), "Cannot add a null device.");
                }

                _devices.Add(device);

                // الاشتراك في حدث تغيير حالة الجهاز
                device.OnStatusChanged += Device_OnStatusChanged;

                Console.WriteLine($"Device {device.DeviceId} added successfully.");
            }

            // معالج لحدث تغيير حالة الجهاز
            private void Device_OnStatusChanged(string deviceId, string newStatus)
            {
                Console.WriteLine($"[{DateTime.Now}] Notification: Device {deviceId} is now in status '{newStatus}'.");

                // هنا يمكن إضافة منطق إضافي مثل إرسال إشعارات أو تحديث قاعدة بيانات
            }

            // دالة لعرض جميع الأجهزة المسجلة
            public void DisplayAllDevices()
            {
                Console.WriteLine("\n--- Registered Devices in the Repair Center ---");

                if (!_devices.Any())
                {
                    Console.WriteLine("No devices are currently registered.");
                    return;
                }

                foreach (var device in _devices)
                {
                    Console.WriteLine(
                        $"Device ID: {device.DeviceId}, Type: {device.DeviceType}, " +
                        $"Status: {device.RepairStatus}, Estimated Repair Cost: {device.CalculateRepairCost():C}");
                }

                Console.WriteLine("-------------------------------------");
            }

            // دالة لتغيير حالة جهاز معين
            public void ChangeDeviceStatus(string deviceId, string newStatus)
            {
                var device = _devices.FirstOrDefault(d => d.DeviceId == deviceId);

                if (device != null)
                {
                    device.ChangeStatus(newStatus);
                }
                else
                {
                    Console.WriteLine($"Device with ID {deviceId} was not found.");
                }
            }

            // دالة لحساب إجمالي تكلفة الإصلاح لجميع الأجهزة
            public decimal CalculateTotalRepairCost()
            {
                return _devices.Sum(d => d.CalculateRepairCost());
            }

            // دالة للبحث عن جهاز بواسطة المعرف
            public IDevice GetDeviceById(string deviceId)
            {
                return _devices.FirstOrDefault(d => d.DeviceId == deviceId);
            }
        }

}
