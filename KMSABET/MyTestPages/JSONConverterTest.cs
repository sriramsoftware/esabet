using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KMSABET.MyTestPages
{
    public class JSONConverterTest
    {
        public void convertJSONToObject() {
            string json = @"{
              'Name': 'Bad Boys',
              'ReleaseDate': '1995-4-7T00:00:00',
              'Genres': [
                'Action',
                'Comedy'
              ]
            }";

            Movie m = JsonConvert.DeserializeObject<Movie>(json);

            int name = m.QID;
            // Bad Boys

        }
    }
}