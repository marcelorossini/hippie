using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
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
    }
}
