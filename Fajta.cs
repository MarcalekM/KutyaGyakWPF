using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaGyakWPF
{
    class Fajta
    {
        public int Id { get; private set; }
        public string FajtaNev { get; private set; }

        public Fajta(int id, string fajtaNev)
        {
            Id = id;
            FajtaNev = fajtaNev;
        }
    }
}