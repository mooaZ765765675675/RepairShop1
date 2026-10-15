using System;

namespace RepairShop1.Models
{

    // فئة مكيف الهواء ترث من الفئة المجردة Device
    public class AirConditioner : Device
    {
        // خاصية إضافية لمكيف الهواء: قدرة التبريد بالوحدة الحرارية البريطانية (BTU)
        public int BTUs { get; private set; }

        // خاصية إضافية: نوع المكيف سبليت/شباك/مركزي
        public string ACType { get; private set; }

        // منشئ (Constructor) لمكيف الهواء
        public AirConditioner(string deviceId, int btus, string acType)
            : base(deviceId, "Air Conditioner") // تمرير نوع الجهاز للفئة الأساسية
        {
            if (btus <= 0)
            {
                throw new ArgumentException(
                    "Cooling capacity (BTU) must be positive.",
                    nameof(btus));
            }

            if (string.IsNullOrWhiteSpace(acType))
            {
                throw new ArgumentException(
                    "Air conditioner type cannot be empty.",
                    nameof(acType));
            }

            BTUs = btus;
            ACType = acType;
        }

        // إعادة تعريف دالة حساب تكلفة الإصلاح الخاصة بمكيف الهواء
        public override decimal CalculateRepairCost()
        {
            // تكلفة أساسية لمكيف الهواء
            decimal baseCost = 200.00m;

            // إضافة تكلفة بناءً على قدرة التبريد
            baseCost += BTUs / 1000 * 15.00m; // كل 1000 BTU تضيف 15 وحدة للوحدة النقدية

            // إضافة تكلفة بناءً على نوع المكيف
            if (ACType.Equals("Split", StringComparison.OrdinalIgnoreCase))
            {
                baseCost += 50.00m;
            }
            else if (ACType.Equals("Central", StringComparison.OrdinalIgnoreCase))
            {
                baseCost += 150.00m; // المكيف المركزي قد يكون أكثر تكلفة في الإصلاح
            }
            else if (ACType.Equals("Window", StringComparison.OrdinalIgnoreCase))
            {
                baseCost += 20.00m;
            }

            return baseCost;
        }
    }


}
