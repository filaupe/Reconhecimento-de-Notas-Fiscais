﻿using System.Text.RegularExpressions;

namespace PrototipoLeitorTesseract.Extensions
{
    public static class RegexAddons
    {
        static string ExtractData(string input, string pattern)
        {
            var regex = new Regex(pattern);
            var match = regex.Match(input);
            return match.Success ? match.Value : "Não Encontrado";
        }
        
        static string[] ExtractDatas(string input, string pattern)
        {
            var regex = new Regex(pattern);
            var matches = regex.Matches(input);
            return matches.Select(match => match.Value).ToArray();
        }

        public static string RegexPegarCNPJ(this string input, string pattern = @"\d{2}[.,]\d{3}[.,]\d{3}/\d{4}[-_]\d{2}")
        {
            input = input.RegexLocalizarCNPJ();
            return ExtractData(input, pattern);
        }
        
        public static string RegexLocalizarCNPJ(this string input, string pattern = @"(([A-Za-z]{4}[-])?)\b\d{2}[.,]\d{3}[.,]\d{3}/\d{4}[-_]\d{2}\b")
        {
            return ExtractData(input, pattern);
        }
        
        public static string RegexChaveCupom(this string input, string pattern = @"\b\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}\s\S{4,6}(\s\S{4,6})?(\s\S{4,6})?(\s\S{4,6})?\b")
        {
            return ExtractData(input, pattern);
        }
        
        public static string RegexLocalizarNumeroCupom(this string input, string pattern = @"\b(No\. (\S+))|(([Nn ])?(:)?(u[nm]ero)? (\d{3}\.\d{3}\.\d{3}))\b")
        {
            return ExtractData(input, pattern);
        }
        
        public static string RegexPegarNumeroCupom(this string input, string pattern = @" (.+)$")
        {
            input = input.RegexLocalizarNumeroCupom();
            return ExtractData(input, pattern);
        }
        
        public static string RegexDataHoraCompra(this string input, string pattern = @"\b\d{2}/\d{2}/\d{4} \d{2}:\d{2}:\d{2}\b")
        {
            return ExtractData(input, pattern);
        }
      
        public static string RegexLocalizarDadosProdutos(this string input, string pattern = @"\b((\d{1,2}\s\S{9})|(\d{3}\s\S{6}))\s(.*?)(?=([Tt1])?[0Oo][Tt1][Aa][Ll1i])")
        {
            return ExtractData(input, pattern);
        }

        public static string RegexRemoverDesnecessariosProduto(this string input, string pattern = @".*(\d+,\d+)")
        {
            return ExtractData(input, pattern);
        }

        public static string[] RegexPegarDadosProdutos(this string input, string pattern = @"((\d{1,2}\s\S{9}.*(?=\d{1,2}\s\S{9}))|(\d{1,2}\s\S{9}.*$))|((\d{3}\s\S{6}\s.*(?=\d{3}\s\S{6}\s))|(\d{3}\s\S{6}\s.*))")
        {
            input = input.RegexLocalizarDadosProdutos();
            string[] produtos = ExtractDatas(input, pattern);
            return produtos.Select(produto => produto.RegexRemoverDesnecessariosProduto()).ToArray();
        }
    }
}
