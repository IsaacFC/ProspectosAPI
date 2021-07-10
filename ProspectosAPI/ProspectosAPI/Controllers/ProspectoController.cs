using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Ionic.Zip;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ProspectosAPI.Models;

namespace ProspectosAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProspectoController : ControllerBase
    {
        private readonly ILogger<ProspectoController> _logger;
        private IHostingEnvironment _hostingEnvironment;
        private readonly ProspectoContext _context;

        public ProspectoController(ProspectoContext context)
        {
            _context = context;
        }

        // GET: api/Prospecto
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Prospectos>>> GetProspectos()
        {
            return await _context.Prospectos.ToListAsync();

        }

        // GET: api/Prospecto/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Prospectos>> GetProspectos(int id)
        {
            var prospectos = await _context.Prospectos.FindAsync(id);

            if (prospectos == null)
            {
                return NotFound();
            }

            return prospectos;
        }

        // PUT: api/Prospecto/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutProspectos(int id, Prospectos prospectos)
        {
            if (id != prospectos.IdProspecto)
            {
                return BadRequest();
            }

            _context.Entry(prospectos).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ProspectosExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("archivos/{id:int}")]
        public IActionResult DownloadFile (int id)
        {
            try
            {
                using (ZipFile zip = new ZipFile())
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), @"Docs", id.ToString());
                    // add this map file into the "images" directory in the zip archive
                    string[] dirs = Directory.GetFiles(filePath);
                    foreach (string dir in dirs)
                    {
                        zip.AddFile(dir, "Files");
                    }
                    string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));


                    zip.Save(filePath + "\\" + zipName);

                    byte[] fileBytes = System.IO.File.ReadAllBytes(filePath + "\\" + zipName);
                    var zipFile = File(fileBytes, "application/force-download", filePath + "\\" + zipName);
                    FileInfo file = new FileInfo(filePath + "\\" + zipName);
                    file.Delete();
                    return zipFile;
                }
            }
            catch (IOException e)
            {

                throw e;
            }
            


        }

        // POST: api/Prospecto
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Prospectos>> PostProspectos(Prospectos prospectos)
        {
            _context.Prospectos.Add(prospectos);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetProspectos", new { id = prospectos.IdProspecto }, prospectos);

        }

        /// <summary>
        /// An Example API Endpoint Accepting Multiple Files
        /// </summary>
        /// <param name="id"></param>
        /// <param name="certificates"></param>
        /// <returns></returns>
        [HttpPost("{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<ActionResult<List<CertificateSubmissionResult>>> SubmitCertificates(int id, [Required] List<IFormFile> certificates)
        {
            var result = new List<CertificateSubmissionResult>();

            if (certificates == null || certificates.Count == 0)
            {
                return BadRequest("No file is uploaded.");
            }

            foreach (var certificate in certificates)
            {
                var filePath = Path.Combine(@"Docs", id.ToString(), certificate.FileName);
                new FileInfo(filePath).Directory?.Create();

                await using var stream = new FileStream(filePath, FileMode.Create);
                await certificate.CopyToAsync(stream);
                //_logger.LogInformation($"The uploaded file [{certificate.FileName}] is saved as [{filePath}].");

                result.Add(new CertificateSubmissionResult { FileName = certificate.FileName, FileSize = certificate.Length });
            }

            return Ok(result);
        }

        // DELETE: api/Prospecto/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Prospectos>> DeleteProspectos(int id)
        {
            var prospectos = await _context.Prospectos.FindAsync(id);
            if (prospectos == null)
            {
                return NotFound();
            }

            _context.Prospectos.Remove(prospectos);
            await _context.SaveChangesAsync();

            return prospectos;
        }

        private bool ProspectosExists(int id)
        {
            return _context.Prospectos.Any(e => e.IdProspecto == id);
        }
    }
    public class StudentForm
    {
        [Required] public int FormId { get; set; }
        [Required] public string[] Courses { get; set; }
        [Required] public IFormFile StudentFile { get; set; }
    }

    public class StudentFormSubmissionResult
    {
        public int StudentId { get; set; }
        public int FormId { get; set; }
        public long FileSize { get; set; }
    }

    public class CertificateSubmissionResult
    {
        public string FileName { get; set; }
        public long FileSize { get; set; }
    }
}
