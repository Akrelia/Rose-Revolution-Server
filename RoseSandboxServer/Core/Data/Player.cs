using RevolutionShared.Data;
using RevolutionShared.Rose.Data;
using RoseSandboxServer.Core.Data.Entities;
using RoseSandboxServer.Core.World;
using RoseSandboxServer.Networking.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoseSandboxServer.Core.Data
{
    /// <summary>
    /// Player.
    /// </summary>
    public class Player : Entity
    {
        public string name;
        public string clanName;
        public byte clanGrade;
        public byte[] clanIcon;
        public long idClient; // Just store the client ID and not the whole client because it's way more clean and clusterized
        CharacterAppearance appearance;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="type"></param>
        /// <param name="dataID"></param>
        public Player(int id, Map map, WorldPosition position, string name, long idClient) : base(id, position, EntityType.Character, map)
        {
            this.name = name;
            this.position = position;
            this.idClient = idClient;
        }

        /// <summary>
        /// Update the player.
        /// </summary>
        /// <param name="context">Context.</param>
        public async Task Update(TickContext context)
        {
            // Anything related to player only
        }

        /// <summary>
        /// TO sub infos.
        /// </summary>
        /// <returns></returns>
        public override ServerEntityInfos ToSubInfos()
        {
            return null;
        }

        public CharacterInfos ToCharInfos()
        {
            return new CharacterInfos(name, clanName, clanIcon, clanGrade);
        }

        /// <summary>
        /// Get or set the player appearance.
        /// </summary>
        public CharacterAppearance Appearance
        {
            get { return appearance; }
            set { appearance = value; }
        }

        /// <summary>
        /// Player in format.
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return $"[{id}] {name}";
        }
    }
}
