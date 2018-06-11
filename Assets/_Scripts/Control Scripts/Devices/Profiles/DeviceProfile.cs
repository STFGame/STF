using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using UnityEngine;

namespace Controls
{
    public enum MapType { Button, Axis }

    public class DeviceProfile
    {
        private string controlName;

        private Dictionary<int, string> mapDictionary = new Dictionary<int, string>();

        #region Control Properties
        public const int AXIS_MULTIPLIER = 100;

        public int ButtonCount { get; private set; }
        public int AxisCount { get; private set; }

        public int Action1 { get; private set; }
        public int Action2 { get; private set; }
        public int Action3 { get; private set; }
        public int Action4 { get; private set; }

        public int L1 { get; private set; }
        public int R1 { get; private set; }
        public int L2A { get; private set; }
        public int R2A { get; private set; }

        public int Select { get; private set; }
        public int Start { get; private set; }

        public int LeftStick { get; private set; }
        public int RightStick { get; private set; }

        public int CenterButton { get; private set; }
        public int TouchPad { get; private set; }

        public int LeftH { get; private set; }
        public int LeftV { get; private set; }
        public int RightH { get; private set; }
        public int RightV { get; private set; }
        public int L2B { get; private set; }
        public int R2B { get; private set; }
        public int DPadH { get; private set; }
        public int DPadV { get; private set; }
        #endregion

        public DeviceProfile(string controlName)
        {
            this.controlName = controlName;

            SetupMap(this.controlName);
        }

        private void SetupMap(string controlName)
        {
            string profileName = ProfileName.GetProfileName(controlName);

            QueryProfile(profileName);
        }

        public string GetMap(int mapKey)
        {
            if (mapDictionary.ContainsKey(mapKey))
                return mapDictionary[mapKey];
            return null;
        }

        private void QueryProfile(string name)
        {
            #region Xml Search
            //Uses Linq to search through the xml doc to find the correct control to map to
            string xmlPath = System.IO.Path.Combine(Application.streamingAssetsPath, "Profiles.xml");

            XDocument doc = XDocument.Load(xmlPath);
            var controlMapping = from profile in doc.Root.Descendants(name)
                                 select new
                                 {
                                     buttons = from button in profile.Element("Buttons").Elements("Button")
                                               select new
                                               {
                                                   name = (string)button.Attribute("name"),
                                                   value = (string)button.Value,
                                               },
                                     axes = from axis in profile.Element("Axes").Elements("Axis")
                                            select new
                                            {
                                                name = (string)axis.Attribute("name"),
                                                value = (string)axis.Value,
                                            }
                                 };
            #endregion

            int buttonIndex = 0;
            int axisIndex = 0;
            //Fills dictionary with the control map
            foreach (var map in controlMapping)
            {
                foreach (var button in map.buttons)
                {
                    AddToMap(button.name, button.value, buttonIndex);
                    buttonIndex++;
                    ButtonCount = buttonIndex;
                }
                foreach (var axis in map.axes)
                {
                    AddToMap(axis.name, axis.value, axisIndex);
                    axisIndex++;
                    AxisCount = axisIndex;
                }
            }
        }

        private void AddToMap(string name, string value, int index)
        {
            #region Sets Button Indices
            if (name == "Action1")
            {
                Action1 = index;
                mapDictionary.Add(Action1, value);
            }
            else if (name == "Action2")
            {
                Action2 = index;
                mapDictionary.Add(Action2, value);
            }
            else if (name == "Action3")
            {
                Action3 = index;
                mapDictionary.Add(Action3, value);
            }
            else if (name == "Action4")
            {
                Action4 = index;
                mapDictionary.Add(Action4, value);
            }
            else if (name == "L1")
            {
                L1 = index;
                mapDictionary.Add(L1, value);
            }
            else if (name == "R1")
            {
                R1 = index;
                mapDictionary.Add(R1, value);
            }
            else if (name == "L2A")
            {
                L2A = index;
                mapDictionary.Add(L2A, value);
            }
            else if (name == "R2A")
            {
                R2A = index;
                mapDictionary.Add(R2A, value);
            }
            else if (name == "Select")
            {
                Select = index;
                mapDictionary.Add(Select, value);
            }
            else if (name == "Start")
            {
                Start = index;
                mapDictionary.Add(Start, value);
            }
            else if (name == "Left Stick")
            {
                LeftStick = index;
                mapDictionary.Add(LeftStick, value);
            }
            else if (name == "Right Stick")
            {
                RightStick = index;
                mapDictionary.Add(RightStick, value);
            }
            else if (name == "Center Button")
            {
                CenterButton = index;
                mapDictionary.Add(CenterButton, value);
            }
            else if (name == "Touch Pad")
            {
                TouchPad = index;
                mapDictionary.Add(TouchPad, value);
            }
            #endregion

            #region Set Axis Indices
            if (name == "LeftH")
            {
                LeftH = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "LeftV")
            {
                LeftV = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "RightH")
            {
                RightH = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "RightV")
            {
                RightV = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "L2B")
            {
                L2B = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "R2B")
            {
                R2B = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "DpadH")
            {
                DPadH = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            else if (name == "DpadV")
            {
                DPadV = index;
                index += AXIS_MULTIPLIER;
                mapDictionary.Add(index, value);
            }
            #endregion
        }

    }
}
