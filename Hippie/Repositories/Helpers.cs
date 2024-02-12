using Hippie.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace Hippie.Repositories
{
    public static class Helpers
    {
        private static FormLoading _formLoading;
        static Helpers()
        {
            FormLoading formLoading = new FormLoading();
            _formLoading = formLoading;
        }

        public static string RemoveAccents(this string text)
        {
            StringBuilder sbReturn = new StringBuilder();
            var arrayText = text.Normalize(NormalizationForm.FormD).ToCharArray();
            foreach (char letter in arrayText)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(letter) != UnicodeCategory.NonSpacingMark)
                    sbReturn.Append(letter);
            }
            return sbReturn.ToString();
        }

        public static void ShowLoading(bool show = true)
        {
            if (show)
            {
                Task.Factory.StartNew(() => {
                    _formLoading.ShowDialog();
                });
            }
            else
                _formLoading.Hide();
        }

        public static List<DatatableFile> FlexibleReadFiles(List<FileInfo> files) 
        {
            var listAux = new List<DatatableFile>();
            foreach (var item in files)
            {
                listAux.Add(FlexibleFileInterpreter(Path.GetFileName(item.FullName), item.FullName, new DirectoryInfo(Path.GetDirectoryName(item.FullName)).Name));
            }
            return listAux;
        }

        enum TypeLetter { Character = 0, Number = 2 };
        public static DatatableFile FlexibleFileInterpreter(string originalText, string fullName, string directoryName)
        {
            string text = originalText;
            text = text.ToLower().Trim();
            text = text.Replace(" ", "");
            text = text.Replace(".", "");
            text = text.Replace("-", "");
            text = text.Replace("/", "");

            char[] arrayOfChar = text.ToCharArray();
            string type = "";
            string code = "";
            TypeLetter? oldChar = null;

            // Exibir cada letra do array
            foreach (char letter in arrayOfChar)
            {
                var isAlpha = IsAlpha(letter.ToString());
                var isNumber = IsNumber(letter.ToString());
                TypeLetter? currentChar = null;

                if (isAlpha) currentChar = TypeLetter.Character;
                else if (isNumber) currentChar = TypeLetter.Number;

                if (currentChar == TypeLetter.Character && oldChar == TypeLetter.Number) break;

                if (isAlpha) type += letter.ToString();
                if (isNumber) code += letter.ToString();


                oldChar = currentChar;
            }

            return new DatatableFile() { Type = type, Code = type+code, Filename = originalText, FullName = fullName, Order = int.Parse(code), Directory = directoryName };
        }

        private static bool IsAlpha(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z]+$");
        }

        private static bool IsNumber(string str)
        {
            int n;
            return int.TryParse(str, out n);
        }
    }
}
