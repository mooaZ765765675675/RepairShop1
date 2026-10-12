using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepairShop1
{
    //   Device فئة الغسالة ترث من الفئة المجردة 
    public class WashingMachine : Device
    {
        // خاصية إضافية للغسالة: نوع التحميل علوي/أمامي
        public string LoadingType { get; private set; } // مثال: "Top" أو "Front"

        // خاصية إضافية: سعة الغسالة بالكيلوغرام
        public int CapacityKG { get; private set; }

        // منشئ (Constructor) للغسالة
        public WashingMachine(string deviceId, string loadingType, int capacityKG)
            : base(deviceId, "Washing Machine") // تمرير نوع الجهاز للفئة الأساسية
        {
            if (string.IsNullOrWhiteSpace(loadingType))
            {
                throw new ArgumentException(
                    "Loading type cannot be empty.",
                    nameof(loadingType));
            }

            if (capacityKG <= 0)
            {
                throw new ArgumentException(
                    "Washing machine capacity must be positive.",
                    nameof(capacityKG));
            }

            LoadingType = loadingType;
            CapacityKG = capacityKG;
        }

        // إعادة تعريف دالة حساب تكلفة الإصلاح الخاصة بالغسالة
        public override decimal CalculateRepairCost()
        {
            // تكلفة أساسية للغسالة
            decimal baseCost = 120.00m;

            // إضافة تكلفة بناءً على نوع التحميل
            if (LoadingType.Equals("Front", StringComparison.OrdinalIgnoreCase))
            {
                baseCost += 60.00m; // الغسالات ذات التحميل الأمامي قد تكون أكثر تعقيدًا
            }
            else if (LoadingType.Equals("Top", StringComparison.OrdinalIgnoreCase))
            {
                baseCost += 30.00m; // الغسالات ذات التحميل العلوي
            }

            // إضافة تكلفة بناءً على السعة
            baseCost += CapacityKG * 2.5m; // كل كيلوغرام يضيف 2.5 وحدة للوحدة النقدية

            return baseCost;
        }

    }
}