﻿using Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class EncriptService : IEncriptService
    {
        public string GetSHA256(string password)
        {
            SHA256 sha256 = SHA256Managed.Create();
            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] stream = null;
            StringBuilder stringBuilder = new StringBuilder();
            stream = sha256.ComputeHash(encoding.GetBytes(password));
            for (int index = 0; index < stream.Length; index++) stringBuilder.AppendFormat("{0:x2}", stream[index]);
            return stringBuilder.ToString();

        }
    }
}