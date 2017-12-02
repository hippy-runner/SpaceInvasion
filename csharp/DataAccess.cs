using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace SpaceInvasion
{
    public class DataAccess
    {
        private string location;

        public DataAccess()
        {
            location = Directory.GetCurrentDirectory() + "\\resources\\data\\gameData.xml";

            if (!File.Exists(this.location))
            {
                this.CreateDataSource();
            }
        }

        private void CreateDataSource()
        {
            Exception exception_ex = new Exception();

            XmlDocument data = null;
            XmlNode declaration = null;
            XmlProcessingInstruction style = null;
            XmlElement root = null;
            XmlAttribute @created = null;

            try
            {
                data = new XmlDocument();

                declaration = data.CreateNode(XmlNodeType.XmlDeclaration, "", "");
                data.AppendChild(declaration);

                style = data.CreateProcessingInstruction("xml-stylesheet", "type=\"text/xsl\" href=\"data.xsl\"");
                data.AppendChild(style);

                root = data.CreateElement("data");

                @created = data.CreateAttribute("created");
                @created.Value = DateTime.Now.ToString();
                root.Attributes.Append(@created);

                data.AppendChild(root);
                data.Save(this.location);
            }
            catch (Exception ex)
            {
                exception_ex = ex;
            }
        }

        public void SaveData(GameData gameData)
        {
            Exception exception_ex = new Exception();

            XmlDocument data = new XmlDocument();
            XmlNode root = null;
            XmlElement entry = null;
            XmlAttribute @time = null;
            XmlAttribute @score = null;
            XmlAttribute @lives = null;
            XmlAttribute @level = null;
            XmlAttribute @hits = null;
            XmlAttribute @misses = null;
            XmlAttribute @hitPercent = null;

            try
            {
                data.Load(this.location);
                root = data.DocumentElement;

                entry = data.CreateElement("entry");

                @time = data.CreateAttribute("time");
                @time.Value = gameData.time.ToString();
                entry.Attributes.Append(@time);

                @score = data.CreateAttribute("score");
                @score.Value = gameData.score.ToString();
                entry.Attributes.Append(@score);

                @lives = data.CreateAttribute("lives");
                @lives.Value = gameData.lives.ToString();
                entry.Attributes.Append(@lives);

                @level = data.CreateAttribute("level");
                @level.Value = gameData.level.ToString();
                entry.Attributes.Append(@level);

                @hits = data.CreateAttribute("hits");
                @hits.Value = gameData.hits.ToString();
                entry.Attributes.Append(@hits);

                @misses = data.CreateAttribute("misses");
                @misses.Value = gameData.misses.ToString();
                entry.Attributes.Append(@misses);

                @hitPercent = data.CreateAttribute("hitPercent");
                @hitPercent.Value = gameData.hitPercent.ToString();
                entry.Attributes.Append(@hitPercent);

                root.PrependChild(entry);
                data.Save(this.location);
            }
            catch (Exception ex)
            {
                exception_ex = ex;
            }
        }

        private List<GameData> GetAllData()
        {
            Exception exception_ex = null;

            List<GameData> gameData_li = new List<GameData>();
            GameData tempGameData = null;
            XmlDocument data = new XmlDocument();
            XmlNodeList entries = null;
            XmlAttributeCollection gameData = null;

            try
            {
                data.Load(this.location);
                entries = data.GetElementsByTagName("entry");

                for (int i = 0; i < entries.Count; i++)
                {
                    gameData = entries[i].Attributes;
                    tempGameData = new GameData();

                    for (int j = 0; j < gameData.Count; j++)
                    {
                        switch (gameData[j].Name)
                        {
                            case "time":
                                tempGameData.time = DateTime.Parse(gameData[j].Value);
                                break;
                            case "score":
                                tempGameData.score = long.Parse(gameData[j].Value);
                                break;
                            case "lives":
                                tempGameData.lives = int.Parse(gameData[j].Value);
                                break;
                            case "level":
                                tempGameData.level = int.Parse(gameData[j].Value);
                                break;
                            case "hits":
                                tempGameData.hits = float.Parse(gameData[j].Value);
                                break;
                            case "misses":
                                tempGameData.misses = float.Parse(gameData[j].Value);
                                break;
                            default:
                                break;
                        }
                    }

                    gameData_li.Add(tempGameData);
                    tempGameData = null;
                }
            }
            catch (Exception ex)
            {
                exception_ex = ex;
            }

            return gameData_li;
        }

        private int CompareByScore(GameData data1, GameData data2)
        {
            int returnValue_i = 0;

            if (data1 == null)
            {
                if (data2 == null)
                {
                    returnValue_i = 0;
                }
                else
                {
                    returnValue_i = 1;
                }
            }
            else
            {
                if (data2 == null)
                {
                    returnValue_i = -1;
                }
                else
                {
                    returnValue_i = -(data1.score.CompareTo(data2.score));
                }
            }

            return returnValue_i;
        }

        public long GetHighScore()
        {
            List<GameData> gameData_li = this.GetAllData();
            long highScore_l = 0;

            if (gameData_li.Count > 0)
            {
                gameData_li.Sort(CompareByScore);
                highScore_l = gameData_li[0].score;
            }

            return highScore_l;
        }
    }
}
