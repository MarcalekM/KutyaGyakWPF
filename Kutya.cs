using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KutyaGyakWPF
{
    class Kutya
    {
        public DateTime ChipDatum { get; private set; }
        public Fajta Fajta { get; private set; }
        public Gazda Gazda { get; private set; }
        public int Id { get; private set; }
        public bool Kan { get; private set; }
        public string KepUrl { get; private set; }
        public int Kor { get; private set; }
        public string Nev { get; private set; }

        public override string ToString()
        {
            return $"\tNév\t\t: {Nev}\n\tKor\t\t: {Kor}\n\tGazda\t\t: {Gazda.Nev}\n\tChipellés dátuma: {ChipDatum:yyyy.MM.dd.}";
        }

        static public List<Kutya> LoadFromCsv(string forras)
        {
            List<Kutya> kutyaLista = new();
            string path = $@"../../../src/{forras}";
            using StreamReader sr = new(
                path: path,
                encoding: UTF8Encoding.UTF8);
            _ = sr.ReadLine();
            while (!sr.EndOfStream) kutyaLista.Add(new(sr.ReadLine()));
            return kutyaLista;
        }

        public Kutya(string sor)
        {
            var temp = sor.Split(';');
            Id = int.Parse(temp[0]);
            Nev = temp[1];
            Kan = temp[2] == "1";
            Kor = int.Parse(temp[8]);
            ChipDatum = DateTime.Parse(temp[9]);
            KepUrl = temp[10];
            Fajta = new Fajta(int.Parse(temp[3]), temp[4]);
            Gazda = new Gazda(int.Parse(temp[5]), temp[6], temp[7]);
        }

        public Kutya(int id, string nev, bool kan, int kor, DateTime chipDatum, string kepUrl, Fajta fajta, Gazda gazda)
        {
            Id = id;
            Nev = nev;
            Kan = kan;
            Kor = kor;
            ChipDatum = chipDatum;
            KepUrl = kepUrl;
            Fajta = fajta;
            Gazda = gazda;
        }
    }
}
