using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeshWsServer
{
    public class User
    {
        [Key]
        public long id { get; set; }

        [Range(2, 30)]
        public string username { get; set; }

        [DataType(DataType.EmailAddress)]
        public string email { get; set; }

        [Range(3, 30)]
        public string firstName { get; set; }

        [Range(3, 30)]
        public string lastName { get; set; }

        [DataType(DataType.Password)]
        public string password { get; set; }

        public string avatar { get; set; }

        [DataType(DataType.Date)]
        public DateTime registerDate { get; private set; }

        [DefaultValue(false)]
        public bool confirm { get; set; }

        public User() { }

        public override string ToString()
        {
            return $"id:{id} username:{username} password:{password} ";
        }
    }
}
