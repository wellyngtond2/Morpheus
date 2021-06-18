using Dapper.Contrib.Extensions;
using System;

namespace Morpheus.Core.Models
{
    [Table("Person")]
    public class PersonModel
    {
        [ExplicitKey]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}