using System;

namespace BaseLayer.Model
{
    public class User : IEquatable<User>
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public bool Equals(User other)
        {
            //Check whether the compared object is null.
            if (Object.ReferenceEquals(other, null)) return false;

            //Check whether the compared object references the same data.
            if (Object.ReferenceEquals(this, other)) return true;

            //Check whether the users' properties are equal.
            return Id.Equals(other.Id) && Name.Equals(other.Name);
        }

        public override int GetHashCode()
        {

            //Get hash code for the Name field if it is not null.
            int hashUserName = Name == null ? 0 : Name.GetHashCode();

            //Get hash code for the Id field.
            int hashUserId = Id.GetHashCode();

            //Calculate the hash code for the user.
            return hashUserName ^ hashUserId;
        }
    }
}