using System;

namespace IoT_Academy
{
    public class Element
    {
        private double latitude;
        private double longitude;
        private DateTime gpsTime;
        private int speed;
        private int angle;
        private int altitude;
        private int satellites;

        public double Latitude
        {
            get { return latitude; }
            set { latitude = value; }
        }

        public double Longitude
        {
            get { return longitude; }
            set { longitude = value; }
        }

        public DateTime GpsTime
        {
            get { return gpsTime; }
            set { gpsTime = value; }
        }

        public int Speed
        {
            get { return speed; }
            set { speed = value; }
        }

        public int Angle
        {
            get { return angle; }
            set { angle = value; }
        }

        public int Altitude
        {
            get { return altitude; }
            set { altitude = value; }
        }

        public int Satellites
        {
            get { return satellites; }
            set { satellites = value; }
        }
    }
}