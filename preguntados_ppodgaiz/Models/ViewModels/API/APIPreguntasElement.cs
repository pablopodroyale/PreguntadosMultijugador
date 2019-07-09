using preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.API
{
    public class APIPreguntasElement
    {
        private string _category;
        public string category

        {
            get
            {
                return _category;
            }
            set
            {
                _category = HttpUtility.UrlDecode(value);
            }

        }

        public string type { get; set; }
        public string last { get; set; }
        public string difficulty { get; set; }
        private string _question;
        public string question
        {
            get
            {
                return _question;
            }
            set
            {
                _question = HttpUtility.UrlDecode(value);
            }

        }
        private string _correct_answer;
        public string correct_answer
        {
            get
            {
                return _correct_answer;
            }
            set
            {
                _correct_answer = HttpUtility.UrlDecode(value);
            }

        }
        private List<string> _incorrect_answers;
        public List<string> incorrect_answers
        {
            get
            {
                return _incorrect_answers;
            }
            set
            {
                this._incorrect_answers = value.Select(p => HttpUtility.UrlDecode(p)).ToList();
                
            }
        }

        public APIPreguntasElement()
        {
            
        }
    }


}