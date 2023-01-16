using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku
{
    public class UnsolveableBoardException : Exception
    {
        public UnsolveableBoardException(string message)
        : base(message) { }
    }

    public class FileDoesntExistException : Exception
    {
        public FileDoesntExistException(string message)
        : base(message) { }
    }

    public class WrongFileTypeException : Exception
    {
        public WrongFileTypeException(string message)
        : base(message) { }
    }

    public class EmptyBoardDataException : Exception
    {
        public EmptyBoardDataException(string message)
        : base(message) { }
    }

    public class InvalidBoardSizeException : Exception
    {
        public InvalidBoardSizeException(string message)
        : base(message) { }
    }

    public class InvalidCharacterException : Exception
    {
        public InvalidCharacterException(string message)
        : base(message) { }
    }

    public class SameNumberInTheSameRowException : Exception
    {
        public SameNumberInTheSameRowException(string message)
        : base(message) { }
    }

    public class SameNumberInTheSameColumnException : Exception
    {
        public SameNumberInTheSameColumnException(string message)
        : base(message) { }
    }

    public class SameNumberInTheSameBoxException : Exception
    {
        public SameNumberInTheSameBoxException(string message)
        : base(message) { }
    }

}
