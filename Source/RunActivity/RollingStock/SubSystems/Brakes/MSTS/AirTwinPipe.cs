﻿// COPYRIGHT 2009, 2010, 2011, 2012, 2013, 2014 by the Open Rails project.
// 
// This file is part of Open Rails.
// 
// Open Rails is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
// 
// Open Rails is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
// 
// You should have received a copy of the GNU General Public License
// along with Open Rails.  If not, see <http://www.gnu.org/licenses/>.

using System;
using ORTS.Common;

namespace ORTS
{
    public class AirTwinPipe : AirSinglePipe
    {
        protected float prevControlPressurePSI=0f;
        //protected bool locomotive;
        public AirTwinPipe(TrainCar car)
            : base(car)
        {
            TwoPipes = true;
            DebugType = "2P";
            (Car as MSTSWagon).DistributorPresent = true;
            (Car as MSTSWagon).EmergencyReservoirPresent = false;
            //locomotive= Car as MSTSLocomotive != null ?  true : false;
        }

        /*public override void UpdateTripleValveState(float controlPressurePSI)
        {
            if (controlPressurePSI < AutoCylPressurePSI - (TripleValveState != ValveState.Release ? 2.2f : 0f) 
                || controlPressurePSI < 2.2f) // The latter is a UIC regulation (0.15 bar)
                TripleValveState = ValveState.Release;
            else if (controlPressurePSI > AutoCylPressurePSI + (TripleValveState != ValveState.Apply ? 2.2f : 0f) && BrakeLine3PressurePSI<1000)
                TripleValveState = ValveState.Apply;
            else
                TripleValveState = ValveState.Lap;
        }*/
        public override void UpdateTripleValveState(float controlPressurePSI)
        {
                //alternative code to make bailoff work on twin pipe
                if (controlPressurePSI < 2.2f || BrakePipePressureChanging && prevControlPressurePSI > controlPressurePSI)
                    TripleValveState = ValveState.Release;
                else if (BrakePipePressureChanging && prevControlPressurePSI < controlPressurePSI)
                    TripleValveState = ValveState.Apply;
                else
                    TripleValveState = ValveState.Lap;

                prevControlPressurePSI = controlPressurePSI;
        }
    }
}
