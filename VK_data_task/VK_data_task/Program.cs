namespace VK_data_task
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var startDataPath = "C:\\Users\\User\\Downloads\\Telegram Desktop\\Задания_разраб_IDM\\Задания_разраб\\ЗадачаHRДанные\\Задача\\StartData.json";
            var employeesPath = "C:\\Users\\User\\Downloads\\Telegram Desktop\\Задания_разраб_IDM\\Задания_разраб\\ЗадачаHRДанные\\Задача\\Employees.json";
            var resultPath = "C:\\Users\\User\\Downloads\\Telegram Desktop\\Задания_разраб_IDM\\Задания_разраб\\ЗадачаHRДанные\\Задача";

            DataManipulationClass.UpdateData(startDataPath, employeesPath, resultPath);

        }
    }
}
