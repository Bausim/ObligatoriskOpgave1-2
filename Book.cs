namespace Obligatorisk_Opgave_1
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int Price { get; set; }

 
      
        public override string ToString()
        {
            return $"Book Id: {Id} Title: {Title} Price: {Price}";
        }

        public void ValidateTitle()
        {
            if (Title == null)
            {
                throw new ArgumentNullException("Title cannot be null");
            }
            if (Title.Length < 3)
            {
                throw new ArgumentOutOfRangeException("Title must be minimum 3 characters");
            }
        }
        public void ValidatePrice()
        { 
            
            if (Price <= 0) 
            { 
                throw new ArgumentOutOfRangeException("Price must be 0 or above");
            }
            if (Price > 1200 )
            {
                throw new ArgumentOutOfRangeException("Max Price kan only be 1200");
            }
        }

        public void Validate()
        {
            ValidateTitle();
            ValidatePrice();
        }



    }
}