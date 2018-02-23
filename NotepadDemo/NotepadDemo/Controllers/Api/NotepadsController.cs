using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NotepadDemo.Data;
using NotepadDemo.Models.NotepadModels;

namespace NotepadDemo.Controllers.Api
{
    /// <summary>
    /// 记事本Webapi控制器
    /// </summary>
    [Produces("application/json")]
    [Route("api/Notepads")]
    public class NotepadsController : Controller
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="context"></param>
        public NotepadsController(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 返回全部的记录
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<Notepad> GetNotepad()
        {
            return _context.Notepad;
        }

        /// <summary>
        /// 根据ID返回单条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetNotepad([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notepad = await _context.Notepad.SingleOrDefaultAsync(m => m.Id == id);

            if (notepad == null)
            {
                return NotFound();
            }

            return Ok(notepad);
        }

        /// <summary>
        /// 修改一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <param name="notepad"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotepad([FromRoute] long id, [FromBody] Notepad notepad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != notepad.Id)
            {
                return BadRequest();
            }

            _context.Entry(notepad).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotepadExists(id))
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

        /// <summary>
        /// 增加一条记录
        /// </summary>
        /// <param name="notepad"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> PostNotepad([FromBody] Notepad notepad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Notepad.Add(notepad);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetNotepad", new { id = notepad.Id }, notepad);
        }

        /// <summary>
        /// 删除一条记录
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotepad([FromRoute] long id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var notepad = await _context.Notepad.SingleOrDefaultAsync(m => m.Id == id);
            if (notepad == null)
            {
                return NotFound();
            }

            _context.Notepad.Remove(notepad);
            await _context.SaveChangesAsync();

            return Ok(notepad);
        }

        private bool NotepadExists(long id)
        {
            return _context.Notepad.Any(e => e.Id == id);
        }
    }
}