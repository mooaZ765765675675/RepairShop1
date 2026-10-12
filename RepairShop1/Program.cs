using System;
using RepairShop1;

class Program
{
    static void Main(string[] args)
    {
        // إنشاء كائن من مركز الصيانة
        RepairCenter center = new RepairCenter();

        // إنشاء أجهزة مختلفة وإضافتها إلى المركز
        Refrigerator fridge1 = new Refrigerator("FRG001", 300);

        WashingMachine wash1 = new WashingMachine("WSH002", "Front", 7);

        AirConditioner ac1 = new AirConditioner("AC003", 18000, "Split");

        center.AddDevice(fridge1);
        center.AddDevice(wash1);
        center.AddDevice(ac1);

        // عرض جميع الأجهزة
        center.DisplayAllDevices();

        // تغيير حالة جهاز
        fridge1.ChangeStatus("Under Repair");
        center.ChangeDeviceStatus("WSH002", "Repaired");

        // عرض الأجهزة بعد تغيير الحالات
        center.DisplayAllDevices();

        // حساب وعرض إجمالي تكلفة الإصلاح
        Console.WriteLine(
            $"\nTotal repair cost for all devices: {center.CalculateTotalRepairCost():C}");


        Console.ReadKey();
    }
}