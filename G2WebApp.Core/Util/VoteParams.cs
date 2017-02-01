using System;

namespace G2WebApp.Core.Util
{
    public class VoteParams
    {
        public int EntityID { get; set; }
        public int VoteValue { get; set; }
        public string Username { get; set; }

        public VoteParams(int EntityID, int VoteValue, string Username)
        {
            if (VoteValue < -1 || VoteValue > 1) throw new ArgumentOutOfRangeException(nameof(VoteValue));
            if(string.IsNullOrEmpty(Username)) throw new ArgumentException(nameof(Username));

            this.EntityID = EntityID;
            this.VoteValue = VoteValue;
            this.Username = Username;
        }
    }
}
