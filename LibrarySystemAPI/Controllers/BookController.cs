using AutoMapper;
using BenchmarkDotNet.Reports;
using LibrarySystemAPI.Dto;
using LibrarySystemAPI.Models;
using LibrarySystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using NSwag.Annotations;
using Prometheus;
using Swashbuckle.AspNetCore.Annotations;
using SwaggerOperationAttribute = Swashbuckle.AspNetCore.Annotations.SwaggerOperationAttribute;

namespace LibrarySystemAPI.Controllers
{
    [ApiController]
    [Route("api/v{version:ApiVersion}/[controller]")]
    [ApiVersion("1.0")] 
    public class BookController : Controller
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookController(IBookRepository bookRepository, IMapper mapper)
        {
           _bookRepository = bookRepository; 
           _mapper = mapper;    
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retrive all Books", Description = "Returns List of Books")]
        public IActionResult GetAllBooks()
        {
            var booksFromDb = _mapper.Map<List<BookDto>>(_bookRepository.GetAllBooks());
            if (booksFromDb == null)
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }

            return Ok(booksFromDb);
        }

        [HttpGet("{bookId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerOperation(Summary = "Retrieve a Single Book by passing the Id in url", Description ="Returns a Single book")]
        public IActionResult GetBook(int bookId)
        {
            //Check if book exists
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            //Retrieve book from database and mapp
            var bookFromDb = _bookRepository.GetBookById(bookId);
            var bookFromDbDto = _mapper.Map<BookDto>(bookFromDb);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            return Ok(bookFromDbDto);  
        }
        
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(500)]
        [ProducesResponseType(204)]
        [SwaggerOperation(Summary = "Add Book to collection", Description = "Ok response")]
        public IActionResult AddBook([FromBody] BookDto bookDto)
        {
            if (bookDto == null)
            {
                BadRequest();   
            }

            var existenceCheck = _bookRepository.GetAllBooks()
                .Where(b => b.Title.Trim().ToUpper() == bookDto.Title.Trim().ToUpper()).FirstOrDefault();

            if (existenceCheck != null)
            {
                ModelState.AddModelError("", "Book already exists");
                return StatusCode(204);
            }

            var dtoToBookMap = _mapper.Map<Book>(bookDto);

            if (!_bookRepository.AddBook(dtoToBookMap))
            {
                ModelState.AddModelError("", "Error adding Book to collection");
                return StatusCode(500, ModelState);
            }

            return Ok(dtoToBookMap);

        }

        [HttpPut("{bookId}")]
        [ProducesResponseType(202)]
        [ProducesResponseType(200)]
        [SwaggerOperation(Summary = "Update book in collection", Description = "201 response")]
        public IActionResult UpdateBook(int bookId, [FromBody] BookDto updatedBookDto)
        { 
            if (updatedBookDto == null) 
            { 
                return BadRequest();
            }

            if (bookId != updatedBookDto.Id)
            {
                return BadRequest();
            }

            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var bookDtoToBookMap = _mapper.Map<Book>(updatedBookDto);

            if (!_bookRepository.UpdateBook(bookDtoToBookMap))
            {
                ModelState.AddModelError("", "Error Updating book");
                return StatusCode(500, ModelState); 
            }

            return NoContent(); 
        }

        [HttpDelete("{bookId}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [SwaggerOperation(Summary = "Remove book from collection", Description = "Ok response")]
        public IActionResult DeleteBookFromStore(int bookId)
        {
            if (!_bookRepository.BookExists(bookId))
            {
                return NotFound();
            }
            
            var bookToDelete = _bookRepository.GetBookById(bookId);

            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            if (!_bookRepository.DeleteBook(bookToDelete))
            {
                ModelState.AddModelError("", "Error Deleting Book From Store");
                return BadRequest();
            }

            return NoContent();
        }


    }
}
