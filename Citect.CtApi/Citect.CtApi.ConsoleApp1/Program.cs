using Citect;
using System;
using System.Linq;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var ctApi = new CtApi(true))
            {
                var equipments = ctApi.Find("Equip", "", "Cluster1", "NAME", "COMMENT", "IODEVICE", "PAGE");
                foreach (var equip in equipments)
                {
                    try
                    {
                        if (equip["NAME"] == "IOServer.CAPCONVEYOR_PM800")
                        {

                        }

                        var tag = ctApi.Cicode($@"EquipGetProperty(""{equip["NAME"]}"", ""TAGPREFIX"", 0, ""Cluster1"")");
                      

                    }
                    catch (Exception e)
                    {
                    }
                }
            }
        }
    }
}
