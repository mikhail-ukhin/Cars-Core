using System.Collections.Generic;

namespace CarsCore.Models
{
    public class Make
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IList<Model> Models { get; set; }

        public Make()
        {
            Models = new List<Model>();
        }
    }
}