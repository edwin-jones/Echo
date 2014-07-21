
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using OpenHardwareMonitor;
using OpenHardwareMonitor.Hardware;

namespace Echo
{
    public class SystemMonitor
    {

        public static SystemMonitor Instance = new SystemMonitor();

        private SystemMonitor()
        {
            myComputer.Open();
        }

        static MySettings settings = new MySettings(new Dictionary<string, string>
            {
                { "/intelcpu/0/temperature/0/values", "H4sIAAAAAAAEAOy9B2AcSZYlJi9tynt/SvVK1+B0oQiAYBMk2JBAEOzBiM3mkuwdaUcjKasqgcplVmVdZhZAzO2dvPfee++999577733ujudTif33/8/XGZkAWz2zkrayZ4hgKrIHz9+fB8/Iu6//MH37x79i9/+NX6N3/TJm9/5f/01fw1+fosnv+A/+OlfS37/jZ/s/Lpv9fff6Ml/NTef/yZPnozc5679b+i193//TQZ+/w2Dd+P9/sZeX/67v/GTf/b3iP3u4/ObBL//73+i+f039+D8Zk/+xz/e/P6beu2TQZju8yH8f6OgzcvPv/U3/Rb8+z/0f/9b/+yfaOn8079X6fr6Cws7ln/iHzNwflPv99/wyS/+xY4+v/evcJ+733+jJ5//Cw7/4ndy9Im3+U2e/Fbnrk31C93vrt/fyPvdb+N//hsF7/4/AQAA//9NLZZ8WAIAAA==" }
            });

        Computer myComputer = new Computer(settings)
        {
            MainboardEnabled = true,
            CPUEnabled = true,
            RAMEnabled = true,
            GPUEnabled = true,
            FanControllerEnabled = true,
            HDDEnabled = true
        };

        public Single GetCPU0Speed()
        {
            Single result = 0f;

            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Clock)
                        {
                            result = (float)sensor.Value;
                            return result;
                        }
                    }
                }
            }

            return result;
        }




        public Single GetCPU0Temp()
        {
            Single result = 0f;

            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.CPU)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            result = (float)sensor.Value;
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        public Single GetGPU0Speed()
        {
            Single result = 0f;

            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Clock)
                        {
                            result = (float)sensor.Value;
                            return result;
                        }
                    }
                }
            }

            return result;
        }

        public Single GetGPU0Temp()
        {
            Single result = 0f;

            foreach (var hardwareItem in myComputer.Hardware)
            {
                if (hardwareItem.HardwareType == HardwareType.GpuAti || hardwareItem.HardwareType == HardwareType.GpuNvidia)
                {
                    hardwareItem.Update();
                    foreach (IHardware subHardware in hardwareItem.SubHardware)
                    {
                        subHardware.Update();
                    }

                    foreach (var sensor in hardwareItem.Sensors)
                    {
                        if (sensor.SensorType == SensorType.Temperature)
                        {
                            result = (float)sensor.Value;
                            return result;
                        }
                    }
                }
            }

            return result;
        }
    }

    public class MySettings : ISettings
    {
        private IDictionary<string, string> settings = new Dictionary<string, string>();

        public MySettings(IDictionary<string, string> settings)
        {
            this.settings = settings;
        }

        public bool Contains(string name)
        {
            return settings.ContainsKey(name);
        }

        public string GetValue(string name, string value)
        {
            string result;
            if (settings.TryGetValue(name, out result))
                return result;
            else
                return value;
        }

        public void Remove(string name)
        {
            settings.Remove(name);
        }

        public void SetValue(string name, string value)
        {
            settings[name] = value;
        }
    }

    //public class OHW
    //{
    //    private static OHW m_Instance;
    //    public static OHW Instance
    //    {
    //        get
    //        {
    //            if (m_Instance == null)
    //                m_Instance = new OHW();

    //            return m_Instance;
    //        }
    //    }
    //    private OHW()
    //    {
    //        m_Instance = this;
    //    }



    //}
}
