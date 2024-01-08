using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DungeonWalker.Components
{
    /// <summary>
    /// Reads all the scores from a file to be displayed in the game
    /// </summary>
    public class HighScoreComponent : DrawableGameComponent
    {
        private SpriteBatch sb;
        private SpriteFont regularFont;
        private Vector2 position;

        private List<String> scores;

        public List<string> Scores { get => scores; set => scores = value; }

        /// <summary>
        /// Constructor for HighScoreComponent
        /// </summary>
        /// <param name="game"> Game variable </param>
        /// <param name="sb"> SpriteBatch from Game1 </param>
        /// <param name="regularFont"> font that will be used to display the scores </param>
        public HighScoreComponent(Game game, SpriteBatch sb, SpriteFont regularFont) : base(game)
        {
            this.sb = sb;
            this.regularFont = regularFont;
            this.position = new Vector2(100, 100);
            ReadFromFileToList();
        }

        /// <summary>
        /// Reads all scores from a the highscores file and puts them onto a list
        /// </summary>
        public void ReadFromFileToList()
        {
            scores = new List<String>();
            try
            {
                // Create the highscores file if it does not exsist
                if (!File.Exists("highscores.txt"))
                {
                    File.Create("highscores.txt");
                }
                // read through everyline and save to a list
                StreamReader reader = new StreamReader("highscores.txt");
                string line = "";
                while (line != null)
                {
                    line = reader.ReadLine();
                    Scores.Add(line);
                }
                // Order the lines from highest to lowest based on score
                for (int i = 0; i < Scores.Count; i++)
                {
                    string tempString = Scores[i];
                    if (Scores[i] == null)
                    {
                        break;
                    }
                    if (Scores[i].Contains(":"))
                    {
                        int colonIndex = Scores[i].IndexOf(':');
                        int score = int.Parse(Scores[i].Substring(colonIndex + 2));
                    }

                }
                reader.Close();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
