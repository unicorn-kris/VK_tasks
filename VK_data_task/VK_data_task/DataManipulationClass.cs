using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace VK_data_task
{
    internal static class DataManipulationClass
    {
        internal static void UpdateData(string fileForUpdatePath, string actualDataFilePath, string outputFilePath)
        {
            var dataForUpdate = new List<DataIDMClass>();
            var dataActual = new List<DataClass>();
            var dataResult = new List<DataIDMClass>();

            using (StreamReader oldData = new StreamReader(fileForUpdatePath))
            {
                string json = oldData.ReadToEnd();
                dataForUpdate = JsonConvert.DeserializeObject<List<DataIDMClass>>(json);
            }

            using (StreamReader newData = new StreamReader(actualDataFilePath))
            {
                string json = newData.ReadToEnd();
                dataActual = JsonConvert.DeserializeObject<List<DataClass>>(json);
            }

            foreach (var newDataItem in dataActual)
            {
                if (dataForUpdate.Any(x => x.PersonID == newDataItem.PersonID))
                {
                    var oldItem = dataForUpdate.Where(x => x.PersonID == newDataItem.PersonID).First();

                    CompareData(newDataItem, oldItem);
                }
                else if (dataResult.Any(x => x.PersonID == newDataItem.PersonID))
                {
                    var oldResultItem = dataResult.Where(x => x.PersonID == newDataItem.PersonID).First();

                    CompareData(newDataItem, oldResultItem);
                }
                else if (newDataItem.DateFired == null)
                {
                    dataResult.Add(new DataIDMClass
                    {
                        Login = newDataItem.Login,
                        DateHired = newDataItem.DateHired,
                        DateFired = newDataItem.DateFired,
                        Specialization = newDataItem.Specialization,
                        Disabled = false,
                        PersonID = newDataItem.PersonID
                    });
                }
            }

            foreach (var newDataItem in dataForUpdate)
            {
                dataResult.Add(newDataItem);
            }

            using (StreamWriter newData = new StreamWriter(outputFilePath + "\\FinalData.json"))
            {
                string json = JsonConvert.SerializeObject(dataResult);
                newData.Write(json);
            }

        }

        private class DataClass
        {
            public string Login { get; set; }

            public int? ID { get; set; }

            public int PersonID { get; set; }

            public string Specialization { get; set; }

            [JsonProperty("date_hired")]
            public DateTime DateHired { get; set; }

            [JsonProperty("date_fired")]
            public DateTime? DateFired { get; set; }
        }

        private class DataIDMClass
        {
            public string Login { get; set; }

            public int PersonID { get; set; }

            public string Specialization { get; set; }

            [JsonProperty("date_hired")]
            public DateTime DateHired { get; set; }

            [JsonProperty("date_fired")]
            public DateTime? DateFired { get; set; }

            public bool Disabled { get; set; }
        }

        private static void CompareData(DataClass newDataItem, DataIDMClass oldItem)
        {
            if (newDataItem.DateHired.CompareTo(oldItem.DateHired) > 0)
            {
                if (!oldItem.Specialization.Equals(newDataItem.Specialization))
                {
                    oldItem.Specialization = newDataItem.Specialization;
                }

                oldItem.DateHired = newDataItem.DateHired;
                oldItem.DateFired = newDataItem.DateFired;
            }
            else if (newDataItem.DateHired.CompareTo(oldItem.DateHired) == 0 && 
                newDataItem.DateFired.HasValue && !oldItem.DateFired.HasValue)
                {
                    oldItem.DateFired = newDataItem.DateFired;
                }
            if (oldItem.DateFired.HasValue)
            {
                oldItem.Disabled = true;
            }
            else
            {
                oldItem.Disabled = false;
            }
        }
    }
}
