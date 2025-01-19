using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using ClassProject.Validations;

namespace ClassProject;

public class Book_Initial
{
    public int Id { get; set; }
    [Required, JsonPropertyName("Name"), StringLength(25)]

    public string FullName { get; set; }

    public int NumberOfPages { get; set; }

    public string Author { get; set; }
    [PublicationDateInPast]
    public DateOnly PublicationDate { get; set; }
    [Required, NoHorror]
    public BookGenre Genre { get; set; }

    public override string ToString()
    {
        return
            $"Id: {Id}, Name: {FullName}, Pages: {NumberOfPages}, Author: {Author}, Published: {PublicationDate}, Genre: {Genre}";
    }


    public enum BookGenre
    {
        Fantasy,
        ScienceFiction,
        Mystery,
        Romance,
        Horror,
        Adventure
    }

}

