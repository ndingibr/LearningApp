using System;

namespace LearningApp.Core.Entities
{
    public class People
    {
        public int Id { get; set; }
        public double Device_Id { get; set; }
        public string Device_Number { get; set; }
        public double Person_Id { get; set; }
        public DateTime Activate_Date{ get; set; }
        public string Address{ get; set; }
        public decimal Longitude{ get; set; }
        public decimal Latitude { get; set; }
        public string Police_Station { get; set; }

    }
}
