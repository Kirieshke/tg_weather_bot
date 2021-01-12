using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelegramBot
{
    class WindInfo
    {
        public float speed { get; set; }
        public float deg { get; set; }
      
        public string WindDirection(float deg)
        {
            if(deg >= 0 && deg <= 15)
            {
                return "North";
            }    
            else if(deg >= 16 && deg <= 85)
            {
                return "North-east";
            }
            else if (deg >= 86 && deg <= 95)
            {
                return "East";
            }
            
            else if (deg >= 95 && deg <= 170)
            {
                return "South-east";
            }
            else if (deg >= 170 && deg <= 200)
            {
                return "South";
            }
            else if (deg >= 201 && deg <= 260)
            {
                return "South-West";
            }
            else if (deg >= 261 && deg <= 300)
            {
                return "West";
            }
            else if (deg >= 301 && deg <= 345)
            {
                return "North-west";
            }
            else if (deg >= 346 && deg <= 360)
            {
                return "North";
            }
            return "";

        }
    }
}
