//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Incapsulation.Failures {

//    public class Common {
//        public static int IsFailureSerious(int failureType) {
//            if (failureType % 2 == 0) return 1;
//            return 0;
//        }


//        public static int Earlier(object[] v, int day, int month, int year) {
//            int vYear = (int)v[2];
//            int vMonth = (int)v[1];
//            int vDay = (int)v[0];
//            if (vYear < year) return 1;
//            if (vYear > year) return 0;
//            if (vMonth < month) return 1;
//            if (vMonth > month) return 0;
//            if (vDay < day) return 1;
//            return 0;
//        }
//    }

//    public class ReportMaker {

//        /// <summary>
//        /// </summary>
//        /// <param name="day"></param>
//        /// <param name="failureTypes">
//        /// 0 for unexpected shutdown, 
//        /// 1 for short non-responding, 
//        /// 2 for hardware failures, 
//        /// 3 for connection problems
//        /// </param>
//        /// <param name="deviceId"></param>
//        /// <param name="times"></param>
//        /// <param name="devices"></param>
//        /// <returns></returns>

//        public static List<string> FindDevicesFailedBeforeDateObsolete(
//            int day,
//            int month,
//            int year,
//            int[] failureTypes,
//            int[] deviceId,
//            object[][] times,
//            List<Dictionary<string, object>> devices) {

//            var problematicDevices = new HashSet<int>();
//            for (int i = 0; i < failureTypes.Length; i++)
//                if (Common.IsFailureSerious(failureTypes[i]) == 1 && Common.Earlier(times[i], day, month, year) == 1)
//                    problematicDevices.Add(deviceId[i]);

//            var result = new List<string>();
//            foreach (var device in devices)
//                if (problematicDevices.Contains((int)device["DeviceId"]))
//                    result.Add(device["Name"] as string);

//            return result;
//        }

//    }
//}

// ================================================================================================================================================================

/* Не работает
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace Incapsulation.Failures {

//    public class Common {

//        public static int IsFailureSerious(int failureType) { // метод проверяет, серьёзность сбоя
//            return failureType % 2 != 0 ? 1 : 0; // если сбой нечетный, то серьезный
//        }

//        public static int Earlier(DateTime v, DateTime date) { // метод проверяет, произошло ли событие раньше
//            return v < date ? 1 : 0;
//        }
//    }

//    public class Device { // устройство
//        public int Id { get; set; } // уникальный идентификатор
//        public string Name { get; set; } // имя
//    }

//    public class Failure {
//        public int Id { get; set; }
//        public DateTime Time { get; set; }
//        public int Type { get; set; }
//        public int DeviceId { get; set; }
//    }

//    public class ReportMaker {
//        // Новый метод для поиска устройств, имеющих серьёзные сбои до заданной даты.
//        public static List<string> FindDevicesFailedBeforeDate(
//            DateTime date, // Заданная дата.
//            int[] failureTypes, // Массив типов сбоев.
//            int[] deviceIds, // Массив идентификаторов устройств.
//            DateTime[] times, // Массив дат и времени, когда произошел сбой.
//            List<Device> devices, // Список всех устройств.
//            List<Failure> failures) // Список всех сбоев.
//        {
//            // Создаём пустой список неполадочных устройств.
//            var problematicDevices = new HashSet<int>();
//            // Для каждого сбоя проверяем тип и дату.
//            for (int i = 0; i < failureTypes.Length; i++) {
//                if (Common.IsFailureSerious(failureTypes[i]) == 1 && Common.Earlier(times[i], date) == 1) {
//                    // Если сбой является серьёзным и был до заданной даты, добавляем идентификатор его устройства в список неполадочных.
//                    problematicDevices.Add(deviceIds[i]);
//                }
//            }

//            // Создаём пустой список результатов.
//            var result = new List<string>();
//            // Для каждого устройства проверяем, есть ли его идентификатор в списке неполадочных.
//            // Если есть, добавляем имя устройства в список результатов.
//            foreach (var device in devices) {
//                if (problematicDevices.Contains(device.Id)) {
//                    result.Add(device.Name);
//                }
//            }

//            return result;
//        }

//        // Старый метод для поиска устройств, имеющих серьёзные сбои до заданной даты.
//        // Переопределяет старый метод исключительно для обратной совместимости.
//        public static List<string> FindDevicesFailedBeforeDateObsolete(
//            int day, // День заданной даты.
//            int month, // Месяц заданной даты.
//            int year, // Год заданной даты.
//            int[] failureTypes, // Массив типов сбоев.
//            int[] deviceId, // Массив идентификаторов устройств.
//            object[][] times, // Массив дат и времени в виде массивов из трёх элементов (день, месяц, год).
//            List<Dictionary<string, object>> devices) // Список всех устройств.
//        {
//            // Создаем новый DateTime из заданных дня, месяца и года.
//            var date = new DateTime(year, month, day);

//            // Создаем новый список устройств.
//            var devicesList = new List<Device>();
//            foreach (var device in devices) {
//                // Добавляем текущее устройство в список устройств.
//                devicesList.Add(new Device { Id = (int)device["DeviceId"], Name = (device["Name"] as string) });
//            }

//            // Создаем новый список сбоев.
//            var failuresList = new List<Failure>();
//            for (int i = 0; i < failureTypes.Length; i++) {
//                // Создаем новый DateTime из текущей даты и времени сбоя из массива times.
//                failuresList.Add(new Failure {
//                    Id = i,
//                    Type = failureTypes[i],
//                    Time = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]),
//                    DeviceId = deviceId[i]
//                });
//            }

//            // Вызываем новый метод FindDevicesFailedBeforeDate с заданными аргументами.
//            return FindDevicesFailedBeforeDate(date, failureTypes, deviceId, failuresList.Select(f => f.Time).ToArray(), devicesList, failuresList);
//        }
//    }
//}
*/

// ================================================================================================================================================================

/* Работает, но плохо
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.Failures {
    public class Device {
        public int DeviceId { get; set; }
        public string Name { get; set; }
    }

    public class Failure {
        public int FailureType { get; set; }
        public int DeviceId { get; set; }
        public DateTime Time { get; set; }
    }

    public class ReportMaker {
        public static List<string> FindDevicesFailedBeforeDate(int day, int month, int year, IEnumerable<Failure> failures, IEnumerable<Device> devices) {
            var problematicDevices = failures
                .Where(f => Common.IsFailureSerious(f.FailureType) == 1 && f.Time.Date < new DateTime(year, month, day))
                .Select(f => f.DeviceId)
                .Distinct()
                .ToList();

            var result = devices
                .Where(d => problematicDevices.Contains(d.DeviceId))
                .Select(d => d.Name)
                .ToList();

            return result;
        }

        [Obsolete("Use FindDevicesFailedBeforeDate instead")]
        public static List<string> FindDevicesFailedBeforeDateObsolete(
            int day,
            int month,
            int year,
            int[] failureTypes,
            int[] deviceId,
            object[][] times,
            List<Dictionary<string, object>> devices) {
            var failures = new List<Failure>();
            for (int i = 0; i < failureTypes.Length; i++) {
                var failure = new Failure {
                    DeviceId = deviceId[i],
                    FailureType = failureTypes[i],
                    Time = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0])
                };
                failures.Add(failure);
            }

            var devicesList = devices.Select(d => new Device { DeviceId = (int)d["DeviceId"], Name = d["Name"] as string }).ToList();

            return FindDevicesFailedBeforeDate(day, month, year, failures, devicesList);
        }
    }

    public class Common {
        public static int IsFailureSerious(int failureType) {
            return failureType % 2 == 0 ? 1 : 0;
        }
    }
}
*/

// ================================================================================================================================================================


// ================================================================================================================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Incapsulation.Failures;

namespace Incapsulation.Failures {

    public class Device { // сущность устройства
        public int DeviceId { get; set; }
        public string Name { get; set; }
    }

    public class Failure { // сущность отказа устройства
        public int DeviceId { get; set; }
        public FailureType FailureType { get; set; }
        public DateTime Time { get; set; }
    }

    public enum FailureType { // типы неисправностей
        Minor, 
        Major
    }

    public class Common { // проблемы устройства
        public static int IsFailureSerious(int failureType) { // является ли отказ серьезным
            return failureType % 2 == 0 ? 1 : 0;
        }

        public static bool Earlier(DateTime v, DateTime date) { // произошел ли отказ раньше указанной даты
            return v.Date < date.Date;
        }
    }

    public class ReportMaker {
        public static List<string> FindDevicesFailedBeforeDateObsolete( // старый метод для обратной совместимости, вызывающий новый метод
            int day, // день
            int month, // месяц
            int year, // год
            int[] failureTypes, // массив типов отказов (0 неож. выкл.; 1 кратковр. нераб.; 2 отказ оборуд.; 3 проблем с подключ.)
            int[] deviceId, // ID устройств
            object[][] times, // массив времени отказов
            List<Dictionary<string, object>> devices // список устройств
            ) {

            var failures = new List<Failure>(); // список с отказами, полученными из входных параметров

            for (int i = 0; i < failureTypes.Length; i++) {
                DateTime time = new DateTime((int)times[i][2], (int)times[i][1], (int)times[i][0]); // день, месяц, год

                failures.Add(new Failure { // объект отказа для текущего устройства
                    DeviceId = deviceId[i], // идентификатор устройства
                    FailureType = (FailureType)failureTypes[i], // тип сбоя
                    Time = time, // время сбоя
                });
            }

            DateTime date = new DateTime(year, month, day); // дата
            var devicenames = FindDevicesFailedBeforeDate( // список устройств, на которых произошли серьезные отказы до указанной даты
                date,
                failures,
                devices.Select(d => new Device { DeviceId = (int)d["DeviceId"], Name = (string)d["Name"] }));

            return devicenames.ToList(); // список имен устройств, на которых произошли серьезные отказы до указанной даты
        }

        public static List<string> FindDevicesFailedBeforeDate( // новый метод поиска устройств, на которых произошли серьезные отказы до указанной даты
            DateTime date,
            IEnumerable<Failure> failures,
            IEnumerable<Device> devices
            ) {

            return devices // выбор устройств, на которых происходили ошибки из списка входных устройств
                .Where(d => failures.Any(
                    f => f.DeviceId == d.DeviceId &&
                    Common.Earlier(f.Time, date) &&  // был ли сбой раньше указанной даты
                    Common.IsFailureSerious((int)f.FailureType) == 1)) // серьезность сбоя
                .Select(d => d.Name) // выбираем имя устройства
                .ToList(); // добавляем в список
        }
    }
}


