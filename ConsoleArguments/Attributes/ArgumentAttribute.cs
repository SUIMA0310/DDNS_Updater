using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleArguments.Attributes {

    [AttributeUsage( AttributeTargets.Method | AttributeTargets.Property )]
    public class ArgumentAttribute : Attribute, IEquatable<ArgumentAttribute> {

        public char? Flag { get; set; } = null;
        public string FullName { get; set; } = null;

        public ArgumentAttribute(char flag) {

            Flag = flag;

        }

        public ArgumentAttribute(string fullName) {

            FullName = fullName;

        }

        public ArgumentAttribute(char flag, string fullName) {

            Flag = flag;
            FullName = fullName;

        }

        public override bool Equals(object obj) {
            return this.Equals( obj as ArgumentAttribute );
        }

        public bool Equals(ArgumentAttribute other) {
            return other != null &&
                    base.Equals( other ) &&
                    EqualityComparer<char?>.Default.Equals( this.Flag, other.Flag ) &&
                     this.FullName == other.FullName;
        }

        public override int GetHashCode() {
            var hashCode = -1628525050;
            hashCode = hashCode * -1521134295 + base.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<char?>.Default.GetHashCode( this.Flag );
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode( this.FullName );
            return hashCode;
        }
    }
}
