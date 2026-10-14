
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using RepairShop1.Interfaces;

namespace RepairShop1.Services
{
    // فئة ثابتة للتحقق من صحة البيانات والإحصائيات
    public static class DataValidator
    {
        // دالة ثابتة للتحقق من صلاحية معرف الجهاز
        public static bool IsValidDeviceId(string deviceId)
        {
            // مثال: معرف الجهاز يجب أن يبدأ بـ 3 أحرف ثم 3 أرقام
            // Regex: ^[A-Z]{3}\d{3}$
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                return false;
            }

            return Regex.IsMatch(deviceId, @"^[A-Z]{3}\d{3}$");
        }


        // التحقق من أن رمز الجهاز يناسب نوعه
        public static bool IsValidDeviceId(string deviceId, string deviceType)
        {
            if (string.IsNullOrWhiteSpace(deviceId))
                return false;

            switch (deviceType)
            {
                case "Refrigerator":
                    return Regex.IsMatch(
                        deviceId, @"^FRG\d{3}$");

                case "Washing Machine":
                    return Regex.IsMatch(
                        deviceId, @"^WSH\d{3}$");

                case "Air Conditioner":
                    return Regex.IsMatch(
                        deviceId, @"^AC\d{4}$");

                default:
                    return false;
            }
        }


        // دالة ثابتة للتحقق من صلاحية تاريخ مثلاً، تاريخ الاستلام لا يكون في المستقبل
        public static bool IsValidDate(DateTime date)
        {
            return date <= DateTime.Today;
        }

        // دالة ثابتة لحساب عدد الأجهزة حسب حالة معينة
        // تتطلب تمرير قائمة الأجهزة من RepairCenter
        public static int GetDeviceCountByStatus(
            IEnumerable<IDevice> devices, string status)
        {
            if (devices == null)
            {
                return 0;
            }

            return devices.Count(d =>
                d.RepairStatus.Equals(status, StringComparison.OrdinalIgnoreCase));
        }

        // دالة ثابتة لحساب إجمالي الأجهزة التي تنتظر الصيانة
        public static int TotalPendingDevices(IEnumerable<IDevice> devices)
        {
            return GetDeviceCountByStatus(devices, "Waiting for repair");
        }
    }
}