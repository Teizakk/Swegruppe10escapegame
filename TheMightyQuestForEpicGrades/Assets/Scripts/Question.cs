using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts
{
    public enum Difficulties { Easy, Medium, Hard }
    [Serializable]
    class Question
    {
        public Dictionary<String, String> Answers { get; set; }
        public String QuestionText { get; set; }
        public string ImagePath { get; set; }
        public List<String> Hints { get; set; }
        public Difficulties Difficulty { get; set; }
        public String Modul { get; set; }
        public Boolean inUse { get; set; }
        public int Level { get; set; }
    }
}
