using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Programowanko.Models;

namespace Programowanko.Services
{
    public class ContentParser
    {
        string[] Content;

        public ContentParser(string [] Content)
        {
            this.Content = Content;
        }

        public List<Measurement> Parse()
        {
            List<Measurement> measurments = new List<Measurement>();
            try
            {
                int i = 0;
                float f;
               
                foreach (String s in Content)
                {
                    if(float.TryParse(s, out f));
                    measurments.Add(new Measurement(f,++i));
                }
            }
            catch (Exception e) {
                Console.WriteLine("Błąd formatowania");
                return new List<Measurement>();
            }

            return measurments;
        }
        

    }
}
