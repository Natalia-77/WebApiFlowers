using System.Collections.Generic;


namespace WpfClient.Validation
{
    public class FlowerValidator
    {
        public List<string> Name { get; set; }
        public List<string> Family { get; set; }
        public List<int> Weight { get; set; }
        public List<string> Image { get; set; }

    }

    public class ErrorValid    
    {
       
        public  FlowerValidator Errors { get; set; }
    }

    
}
