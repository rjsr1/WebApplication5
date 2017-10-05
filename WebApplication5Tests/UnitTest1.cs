using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            List<String> teste = new List<string>();
            teste.Add("1");
            teste.Add("2");
            Arquivo arq = new Arquivo();
            arq.Arquivo_Nome = "teste";            
            CollectionAssert.Contains( arq.Pegar_Arquivos(3), "lista");
           
        }
    }
}
