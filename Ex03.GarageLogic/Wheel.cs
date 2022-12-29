﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ex03.GarageLogic
{
    public class Wheel
    {
        private readonly float r_MaxAirPressure;
        private readonly string r_ProducerName;
        private float m_CurrentAirPressure;

        public float MaxAirPressure
        {
            get { return r_MaxAirPressure; }
        }

        public string ProducerName
        {
            get { return r_ProducerName; }
        }

        public float CurrentAirPressure
        {
            get { return m_CurrentAirPressure; }

            set
            {
                if (value >= 0f && value <= r_MaxAirPressure)
                {
                    m_CurrentAirPressure = value;
                }
                else
                {
                    throw new Exception(); // todo
                }
            }
        }

        public Wheel(float i_MaxAirPressure, string i_ProducerName)
        {
            r_MaxAirPressure = i_MaxAirPressure;
            r_ProducerName = i_ProducerName;
        }

        public bool Inflate(float i_AirToAdd)
        {
            try
            {
                bool isPossible = false;
                CurrentAirPressure += i_AirToAdd;
                isPossible = true;

                return isPossible;
            }
            catch
            {
                return false;
            }
        }
    }
}