﻿using GTA.Math;
using JustCauseRebelDrops.Classes;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace JustCauseRebelDrops
{
    internal class Util
    {
        /// <summary>
        /// Verifies that all of the mod's files are there
        /// </summary>
        public static void VerifyFileStructure()
        {
            if (!Directory.Exists(Globals.ResourceDir)) Directory.CreateDirectory(Globals.ResourceDir);
            if (!Directory.Exists(Globals.CustomVehicleDir)) Directory.CreateDirectory(Globals.CustomVehicleDir);
            if (!File.Exists(Globals.ConfigFile))
            {
                ModConfig config = new ModConfig();
                File.WriteAllText(Globals.ConfigFile, JsonConvert.SerializeObject(config, Formatting.Indented));
            }
            if (!File.Exists(Globals.VehicleFile))
            {
                VehicleConfig vehicles = new VehicleConfig();
                vehicles.CivilianVehicles.AddRange(Globals.DefaultAir);
                vehicles.CivilianVehicles.AddRange(Globals.DefaultLand);
                vehicles.CivilianVehicles.AddRange(Globals.DefaultSea);
                vehicles.MilitaryVehicles.AddRange(Globals.DefaultMilAir);
                vehicles.MilitaryVehicles.AddRange(Globals.DefaultMilLand);
                vehicles.MilitaryVehicles.AddRange(Globals.DefaultMilSea);
                File.WriteAllText(Globals.VehicleFile, JsonConvert.SerializeObject(vehicles, Formatting.Indented));
            }
            if (!File.Exists(Globals.WeaponFile))
            {
                WeaponConfig weapons = new WeaponConfig();
                File.WriteAllText(Globals.WeaponFile, JsonConvert.SerializeObject(weapons, Formatting.Indented));
            }
            if (!File.Exists(Globals.HitSound)) Main.PlaySound = false;
            if (!File.Exists(Globals.CallSound)) Main.PlaySound = false;
            if(!File.Exists(Globals.CustomVehicleDir + "\\CustomTemplate.json.n")) File.WriteAllText(Globals.CustomVehicleDir + "\\CustomTemplate.json.n", JsonConvert.SerializeObject(new CustomVehicleConfig()
            {
                CategoryName = "Example",
                Vehicles = new List<DropVehicle>()
                {
                    new DropVehicle()
                    {
                        DisplayName = "Mallard Example",
                        ModelName = "stunt",
                        Type = VehicleType.Plane
                    }
                }
            }, Formatting.Indented));
            if(!File.Exists(Globals.CustomVehicleDir + "\\CustomTemplate.xml.n"))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(CustomVehicleConfig));
                FileStream stream = new FileStream(Globals.CustomVehicleDir + "\\CustomTemplate.xml.n", FileMode.OpenOrCreate);
                serializer.Serialize(stream, new CustomVehicleConfig()
                {
                    CategoryName = "Example",
                    Vehicles = new List<DropVehicle>()
                    {
                        new DropVehicle()
                        {
                            DisplayName = "Mallard Example",
                            ModelName = "stunt",
                            Type = VehicleType.Plane
                        }
                    }
                });
                stream.Close();
            }
        }

        /// <summary>
        /// Used for the cargo plane positioning. I don't really understand how this works, only how to use it
        /// </summary>
        /// <param name="A"></param>
        /// <param name="B"></param>
        /// <param name="x"></param>
        /// <returns></returns>
        public static Vector3 LerpByDistance(Vector3 A, Vector3 B, float x)
        {
            Vector3 P = x * Vector3.Normalize(B - A) + A;
            return P;
        }
    }
}
