using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairShop1
{
   

   
        // فئة مجردة توفر تطبيقًا جزئيًا للواجهة وتعرف خصائص مشتركة
        public abstract class Device : IDevice
        {
            // حقل خاص لمعرف الجهاز
            private readonly string _deviceId;
            // حقل خاص لنوع الجهاز
            private readonly string _deviceType;
            // حقل خاص لحالة الصيانة
            private string _repairStatus;

            // خاصية عامة للوصول إلى معرف الجهاز للقراءة فقط
            public string DeviceId => _deviceId;
            // خاصية عامة للوصول إلى نوع الجهاز للقراءة فقط
            public string DeviceType => _deviceType;
            // خاصية عامة للوصول إلى حالة الصيانة
            public string RepairStatus
            {
                get => _repairStatus;
            // ChangeStatus يمكن تعيينها داخليًا أو من خلال دالة 
            protected set => _repairStatus = value;
            }

            // حدث يتم إطلاقه عند تغيير حالة الجهاز
            public event Action<string, string> OnStatusChanged;

            // منشئ (Constructor) للفئة المجردة
            public Device(string deviceId, string deviceType)
            {
                if (string.IsNullOrWhiteSpace(deviceId))
                {
                    throw new ArgumentException("معرف الجهاز لا يمكن أن يكون فارغًا.", nameof(deviceId));
                }
                if (string.IsNullOrWhiteSpace(deviceType))
                {
                    throw new ArgumentException("نوع الجهاز لا يمكن أن يكون فارغًا.", nameof(deviceType));
                }

                _deviceId = deviceId;
                _deviceType = deviceType;
                _repairStatus = "في انتظار الصيانة"; // حالة مبدئية
            }

            // دالة مجردة لحساب تكلفة الإصلاح، يجب على الفئات الفرعية تعريفها
            public abstract decimal CalculateRepairCost();

            // دالة لتغيير حالة الجهاز وإطلاق الحدث
            public void ChangeStatus(string newStatus)
            {
                if (string.IsNullOrWhiteSpace(newStatus))
                {
                    throw new ArgumentException("الحالة الجديدة لا يمكن أن تكون فارغة.", nameof(newStatus));
                }

                string oldStatus = _repairStatus;
                _repairStatus = newStatus;
                // إطلاق الحدث لإعلام المشتركين بالتغيير
                OnStatusChanged?.Invoke(DeviceId, newStatus);
                Console.WriteLine($"تم تغيير حالة الجهاز {DeviceId} من '{oldStatus}' إلى '{newStatus}'.");
            }
        }
    
}
