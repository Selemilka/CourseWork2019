using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CourseWork2019.Data;
using CourseWork2019.Models;
using CourseWork2019.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace CourseWork2019.Controllers
{
    public class QuizzesController : Controller
    {
        private readonly QuizContext _context;

        public QuizzesController(QuizContext context)
        {
            _context = context;
        }

        // GET: Quizzes
        public async Task<IActionResult> Index()
        {
            return View(await _context.Quizzes.ToListAsync());
        }

        // GET: Quizzes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            var questions = await _context.QuizQuestions.Where(s => s.QuizID == id).Select(sc => sc.Question).ToListAsync();

            return View(new QuizDetailsModel(quiz, questions));
        }

        //GET: Quizzes/AddQuestion/5
        public IActionResult AddQuestion(int? id)
        {
            if (id == null) return NotFound();
            ViewBag.ID = id;
            return View();
        }

        //POST: Quizzes/AddQuestion/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddQuestion([Bind("QuizID,QuestionID")] QuizQuestion quizQuestion)
        {
            if (await _context.Questions.FirstOrDefaultAsync(w => w.QuestionID == quizQuestion.QuestionID) != null)
            if (ModelState.IsValid)
            {
                _context.QuizQuestions.Add(quizQuestion);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quizQuestion);
        }

        // GET: Quizzes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Quizzes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("QuizID,QuizName")] Quiz quiz)
        {
            quiz.CreationDate = DateTime.Now;
            if (ModelState.IsValid)
            {
                _context.Add(quiz);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quizzes/PassRubric
        public async Task<IActionResult> PassRubric(int? id)
        {
            if (id == null) return NotFound();
            List<Question> questions = await _context.Questions
                                            .Where(w => w.RubricID == id)
                                                    .Include(w => w.Rubric)
                                                    .Include(w => w.Answers)
                                            .ToListAsync();

            if (!questions.Any()) return NotFound();

            questions.ForEach(w => w.Answers?.ToList().ForEach(r => r.IsCorrectAnswer = false));

            Quiz quiz = new Quiz();
            Rubric rubric = await _context.Rubrics.FirstOrDefaultAsync(w => w.RubricID == id);
            quiz.QuizName = rubric.RubricName;

            return View(new QuizDetailsModel(quiz, questions));
        }

        // POST: Quizzes/PassRubric
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> PassRubric(int? id, List<Answer> answers)
        {
            if (id == null) return NotFound();
            List<Question> questions = await _context.Questions
                                                       .Where(w => w.RubricID == id)
                                                           .Include(w => w.Answers)
                                                       .ToListAsync();
            if (!questions.Any()) return NotFound();
            int total = questions.Count();
            int correct = total;
            foreach (var i in questions)
            {
                foreach (var j in i.Answers)
                {
                    if (j.IsCorrectAnswer != answers.FirstOrDefault(w => w.AnswerID == j.AnswerID).IsCorrectAnswer)
                    {
                        --correct;
                        break;
                    }
                }
            }

            ViewBag.TotalQuestions = total;
            ViewBag.CorrectQuestions = correct;

            return View("Result", new ResultModel() { TotalQuestions = total, CorrectQuestions = correct });
        }

        // GET: Quizzes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FindAsync(id);
            if (quiz == null)
            {
                return NotFound();
            }
            return View(quiz);
        }

        // POST: Quizzes/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("QuizID,QuizName")] Quiz quiz)
        {
            if (id != quiz.QuizID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(quiz);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!QuizExists(quiz.QuizID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(quiz);
        }

        // GET: Quizzes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes
                .FirstOrDefaultAsync(m => m.QuizID == id);
            if (quiz == null)
            {
                return NotFound();
            }

            return View(quiz);
        }

        // POST: Quizzes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var quiz = await _context.Quizzes.FindAsync(id);
            _context.Quizzes.Remove(quiz);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Pass(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var quiz = await _context.Quizzes.FirstOrDefaultAsync(m => m.QuizID == id);

            if (quiz == null) return NotFound();

            var questions = await _context.QuizQuestions
                                    .Where(q => q.QuizID == id)
                                    .Select(m => m.Question)
                                        .Include(w => w.Answers)
                                    .OrderBy(w => w.QuestionID)
                                    .ToListAsync();

            if (!questions.Any()) return NotFound();

            questions.ForEach(w => w.Answers?.ToList().ForEach(r => r.IsCorrectAnswer = false));

            return View(new QuizDetailsModel(quiz, questions));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Pass(int id, List<Answer> answers)
        {
            List<Question> correctQuestions = await _context.QuizQuestions
                                                        .Where(w => w.QuizID == id)
                                                        .Select(w => w.Question)
                                                            .Include(w => w.Answers)
                                                        .OrderBy(w => w.QuestionID)
                                                        .ToListAsync();
            
            int total = correctQuestions.Count();
            int correct = total;

            foreach (var i in correctQuestions)
            {
                foreach (var j in i.Answers)
                {
                    bool checkCorrectAnswer = answers.FirstOrDefault(w => w.AnswerID == j.AnswerID).IsCorrectAnswer;
                    if (j.IsCorrectAnswer != checkCorrectAnswer)
                    {
                        --correct;
                        break;
                    }
                }
            }
            
            ViewBag.TotalQuestions = total;
            ViewBag.CorrectQuestions = correct;

            return View("Result", new ResultModel() { TotalQuestions = total, CorrectQuestions = correct });
        }

        private bool QuizExists(int id)
        {
            return _context.Quizzes.Any(e => e.QuizID == id);
        }

    }
}
