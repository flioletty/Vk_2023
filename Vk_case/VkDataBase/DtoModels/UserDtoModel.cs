using Newtonsoft.Json;
using VkDataBase.Models;

namespace VkDataBase.DtoModels
{
    [JsonObject]
    public class UserDtoModel
    {

        [JsonProperty("Login")]
        public string Login { get; set; }

        [JsonProperty("Password")]
        public string Password { get; set; }

        [JsonProperty("Created_date")]

        public DateTime CreatedDate { get; set; }

        [JsonProperty("Code_group")]
        public UserGroupCode CodeGroup { get; set; }

        [JsonProperty("GroupDescription")]
        public string GroupDescription { get; set; }

        [JsonProperty("Code_state")]
        public UserStateCode CodeState { get; set; }

        [JsonProperty("StateDescription")]
        public string StateDescription { get; set; }
    }
}
