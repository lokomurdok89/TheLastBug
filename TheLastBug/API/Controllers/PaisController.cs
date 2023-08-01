using Application.Utils;
using Domain;
using Microsoft.AspNetCore.Mvc;


namespace API.Controllers
{
    [Route("[controller]")]
    public class PaisController : Controller
    {
        private readonly ILogger<PaisController> _logger;
        private readonly IUtils _utils;
  
        public PaisController(ILogger<PaisController> logger,IUtils utils)
        {
            _logger = logger;
            _utils = utils;
        }

         // GET: api/Pais
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Pais>>> GetPaises()
    {
        _logger.LogInformation("Func: GetPaises");
        return await _utils.ListPaisAsync();
    }

    // GET: api/Pais/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Pais>> GetPais(int id)
    {
        _logger.LogInformation("Func: GetPais{id}",id);
       return await _utils.DetailPaisAsync(id);
    }

    // POST: api/Pais
    [HttpPost]
    public async Task<ActionResult<Pais>> PostPais(Pais pais)
    {
        _logger.LogInformation("Func: PostPais{pais}",pais);
        await _utils.AddPaisAsync(pais);

        return Ok(new { nombrePais = pais.NombrePais, id = pais.PaisID });
    }

    // PUT: api/Pais/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutPais(int id, Pais pais)
    {
        _logger.LogInformation("Func: PutPais {pais} {id}",pais, id);
        await _utils.UpdatePaisAsync(pais,id);        
        return Ok(new { nombrePais = pais.NombrePais, id });
    }

    // DELETE: api/Pais/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePais(int id)
    {
        _logger.LogInformation("Func: DeletePais {id}",id);
        await _utils.DeletePaisAsync(id);
        return Ok();
    }

   
    }
}