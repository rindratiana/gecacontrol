using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GECA_Control.Models
{
    public class DoMove
    {
        public string Key { get; set; }
        public int Value { get; set; }

        public DoMove(string key, int value)
        {
            Key = ControlKey(key);
            Value = value;
        }
        public override string ToString()
        {
            return $"{Key}{Value}";
        }
        /// <summary>
        /// Handles the key input from the rider.
        /// </summary>
        /// <param name="key">The key entered by the rider.</param>
        /// <returns>The valid key.</returns>
        /// <exception cref="ArgumentException">Thrown when the key is invalid.</exception>
        private string ControlKey(string key)
        {
            string[] keyArray = { "u","d","l","r" };
            bool inArray = keyArray.Where(x=>x.ToUpper() == key.ToUpper()).Any();
            if(inArray) return key;
            else throw new ArgumentException("One or more keys are invalid.");
        }
    }
}
