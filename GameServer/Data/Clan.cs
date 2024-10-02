using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseGameServer.Data
{
    /// <summary>
    /// Rose Clan.
    /// </summary>
    public class Clan
    {
        int id;
        int logo;
        int back;
        int grade;
        int clanPoints;
        string name;
        string slogan;
        string news;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Clan()
        {

        }

        /// <summary>
        /// Get or set the id.
        /// </summary>
        public int Id
        {
            get { return id; }
            set { id = value; }
        }

        /// <summary>
        /// Get or set the logo.
        /// </summary>
        public int Logo
        {
            get { return logo; }
            set { logo = value; }
        }

        /// <summary>
        /// Get or set the back.
        /// </summary>
        public int Back
        {
            get { return back; }
            set { back = value; }
        }

        /// <summary>
        /// Get or set the grade.
        /// </summary>
        public int Grade
        {
            get { return grade; }
            set { grade = value; }
        }

        /// <summary>
        /// Get or set the clan points.
        /// </summary>
        public int ClanPoints
        {
            get { return clanPoints; }
            set { clanPoints = value; }
        }

        /// <summary>
        /// Get or set the name.
        /// </summary>
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        /// <summary>
        /// Get or set the slogan.
        /// </summary>
        public string Slogan
        {
            get { return slogan; }
            set { slogan = value; }
        }

        /// <summary>
        /// Get or set the news.
        /// </summary>
        public string News
        {
            get { return news; }
            set { news = value; }
        }
    }
}
