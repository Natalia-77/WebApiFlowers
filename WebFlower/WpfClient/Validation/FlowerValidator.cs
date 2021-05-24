using System.Collections.Generic;


namespace WpfClient.Validation
{
    public class FlowerValidator
    {
        public List<string> Name { get; set; }
        public List<string> Family { get; set; }
        public List<string> Weight { get; set; }
        public List<string> Image { get; set; }

        public List<string>ErrorRes()
        {
            List<string> result = new List<string>();
            if (Name != null)
                result.AddRange(Name);
            if (Family != null)
                result.AddRange(Family);
            if (Weight != null)
                result.AddRange(Weight);
            if (Image != null)
                result.AddRange(Image);           

            return result;
        }

    }

    public class ErrorValid    
    {       
        public  FlowerValidator Errors { get; set; }
    }

    
}
