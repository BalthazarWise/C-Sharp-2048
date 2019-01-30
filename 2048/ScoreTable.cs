using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace _2048
{
    [Serializable]
    public class ScoreTable
    {
        public ScoreTable()
        {
            BestScore = 0;
        }
        public ScoreTable(int bestScore)
        {
            BestScore = bestScore;
            SaveBestScore(this);
        }
        public int BestScore { get; set; }

        public static void SaveBestScore(ScoreTable bestScore)
        {
            XmlSerializer formatter = new XmlSerializer(typeof(ScoreTable));
            using (FileStream fs = new FileStream("bestScore.xml", FileMode.Create))
            {
                formatter.Serialize(fs, bestScore);
            }

        }
        public static ScoreTable LoadBestScore()
        {
            ScoreTable bestScore = new ScoreTable();
            XmlSerializer serializer = new XmlSerializer(typeof(ScoreTable));
            using (var fs = new FileStream("bestScore.xml", FileMode.Open, FileAccess.Read))
            {
                bestScore = (ScoreTable)serializer.Deserialize(fs);
                fs.Flush();
                fs.Close();
            }
            return bestScore;
        }
    }
}
