using System;

namespace Shared.model
{
    public class TimeSlot
    {
        public int Id { get; set; }
        public DateTime StartSlot { get; set; }
        public DateTime EndSlot { get; set; }
        public int CarId { get; set; }
        public Car Car { get; set; }
    }
}