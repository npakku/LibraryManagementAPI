using Microsoft.Identity.Client;

namespace LibrarySystemAPI.Models
{
    public class Book
    {
        public int Id { get; set; } 
        public string Title { get; set; }   
        public string Author { get; set; }  
        public string Genre {  get; set; }  
        public DateTime PublicationDate { get; set; }   
        public bool? AvailabilityStatus { get; set; }    
        public string? Edition { get; set; }
        public string? Summary {  get; set; }    
    }
}
