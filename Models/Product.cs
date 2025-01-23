using FusionAPI_Framework.Models;

namespace FusionAPI_Framework.Models
{
    public partial class Products
    {
        public int? id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Category { get; set; }
        public int? Stock { get; set; }

        // Foreign Key for Departments
        public int FKid { get; set; }
        public virtual Department? Department { get; set; }

        // Foreign Key for Labours
        public int? FKlabourid { get; set; }
        public virtual Labour? Labour { get; set; }
    }
}
