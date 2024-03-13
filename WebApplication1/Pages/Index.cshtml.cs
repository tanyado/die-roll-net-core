using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Numpy;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Pages
{
      
    public class RollSettings()
    {
        public double Die1_Face { get; set; }
        public double Die1_Factor { get; set; }
        public double Die2_Face { get; set; }
        public double Die2_Factor { get; set; }
        public int Rolls { get; set; }
    }

    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }

        public void OnPost()
        {
            var face1 = Convert.ToDouble(Request.Form["face1"]);
            var factor1 = Convert.ToDouble(Request.Form["factor1"]);

            var face2 = Convert.ToDouble(Request.Form["face2"]);
            var factor2 = Convert.ToDouble(Request.Form["factor2"]);

            var rolls = Convert.ToInt32(Request.Form["rolls"]);

            var rollSettings = new RollSettings
            {
                Die1_Face = Convert.ToDouble(Request.Form["face1"]),
                Die1_Factor = Convert.ToDouble(Request.Form["factor1"]),
                Die2_Face = Convert.ToDouble(Request.Form["face2"]),
                Die2_Factor = Convert.ToDouble(Request.Form["factor2"]),
                Rolls = Convert.ToInt32(Request.Form["rolls"])
        };


            System.IO.File.AppendAllText("data/output.csv", $"NEW SESSION:" +  DateTime.Now + Environment.NewLine);

            (NDarray, NDarray) outcome_tuple = new ();
            for (int i = 0; i < rollSettings.Rolls; i++)
            {
                outcome_tuple.Item1 = RollTheDie(rollSettings.Die1_Face, rollSettings.Die1_Factor);
                outcome_tuple.Item2 = RollTheDie(rollSettings.Die2_Face, rollSettings.Die2_Factor);
                System.IO.File.AppendAllText("data/output.csv", $"{outcome_tuple}" + Environment.NewLine);

            }


        }

        private NDarray RollTheDie(double face, double factor)
        {
            // Math Example:
            //[1, 2, 3, 4, 5, 6]
            //[1 / 7, 1 / 7, 1 / 7, 1 / 7 ,1 / 7 ,2 / 7] 2x
            //[1 / 8, 1 / 8, 1 / 8, 1 / 8, 1 / 8, 3 / 8,] 3x

            int[] faces = [1, 2, 3, 4, 5, 6];
            double faceindex = face - 1;
            double weighted = factor / (6 + factor - 1);
            double nonweighted = 1 / (6 + factor - 1);

            double[] probs = new double[6];
            for (int i = 0; i < 6; i++)
            {
                if (i == faceindex)
                {
                    probs[i] = weighted;
                }
                else
                {
                    probs[i] = nonweighted;
                }
            }

            // Normalize the probabilities 
            double sum = 0.0;
            for (int i=0; i<6; i++)
            {
                sum += probs[i];
            }

            for (int i = 0; i < 6; i++)
            {
                probs[i] = probs[i] / sum;
            }


            NDarray outcome = np.random.choice(faces, [1], false, np.array(probs));
            return outcome;

        }

    }
}
