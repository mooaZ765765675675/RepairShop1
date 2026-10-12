using System;

namespace RepairShop1
{


    //  Device فئة الثلاجة ترث من الفئة المجردة 
    public class Refrigerator : Device
        {
            // خاصية إضافية للثلاجة  
            public int CoolingCapacity { get; private set; }// السعة 

            // منشئ (Constructor) للثلاجة
            public Refrigerator(string deviceId, int coolingCapacity)
                : base(deviceId, "Refrigerator") // تمرير نوع الجهاز للفئة الأساسية
            {
                if (coolingCapacity <= 0)
                {
                    throw new ArgumentException(
                        "Cooling capacity must be positive.",
                        nameof(coolingCapacity));
                }

                CoolingCapacity = coolingCapacity;
            }

            // إعادة تعريف دالة حساب تكلفة الإصلاح الخاصة بالثلاجة
            public override decimal CalculateRepairCost()
            {
                // مثال بسيط لحساب التكلفة بناءً على السعة
                // يمكن إضافة منطق أكثر تعقيدًا هنا بناءً على حالة الجهاز وقطع الغيار
                decimal baseCost = 150.00m; // تكلفة أساسية للثلاجة
                decimal capacityFactor = CoolingCapacity * 0.5m; // عامل يعتمد على السعة

                return baseCost + capacityFactor;
            }
        }
    
}
