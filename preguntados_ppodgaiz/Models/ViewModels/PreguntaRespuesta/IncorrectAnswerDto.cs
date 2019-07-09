using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace preguntados_ppodgaiz.Models.ViewModels.PreguntaRespuesta
{
    public class IncorrectAnswerDto
    {
        private string _answer;
        public string Answer
        {
            get
            {
                return _answer;
            }
            set
            {
                this._answer = HttpUtility.UrlDecode(value);
            }
        }
    }
}