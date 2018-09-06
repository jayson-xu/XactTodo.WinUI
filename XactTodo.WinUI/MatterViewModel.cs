using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using XactTodo.Models;
using XactTodo.Utils;

namespace XactTodo
{
    public class MatterViewModel
    {
        private readonly HttpClient client;
        private readonly Matter matter;

        public MatterViewModel()
        {
            client = HttpClientFactory.CreateClient();
            this.matter = new Matter("")
            {
                CreationTime = DateTime.Now,
            };
        }

        public MatterViewModel(Matter matter)
        {
            this.matter = matter.Clone();
        }

        public Matter Matter => matter;

        internal bool Save()
        {
            var subject = this.matter.Subject;
            throw new NotImplementedException();
        }
    }
}
