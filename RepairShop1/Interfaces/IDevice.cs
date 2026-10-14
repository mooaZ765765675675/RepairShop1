using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairShop1.Interfaces
{

    // واجهة تحدد السلوكيات الأساسية لأي جهاز
    public interface IDevice
    {
        // خاصية معرف الجهاز للقراءة فقط
        string DeviceId { get; }
        // خاصية نوع الجهاز للقراءة فقط
        string DeviceType { get; }
        // خاصية حالة الصيانة للقراءة فقط
        string RepairStatus { get; }
        // دالة لحساب تكلفة الإصلاح مجردة ليتم تعريفها في الفئات الفرعية
        decimal CalculateRepairCost();
        // دالة لتغيير حالة الجهاز
        void ChangeStatus(string newStatus);
        // حدث يتم إطلاقه عند تغيير حالة الجهاز
        event Action<string, string> OnStatusChanged;
    }

}
