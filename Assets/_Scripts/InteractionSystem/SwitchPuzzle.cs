namespace PuzzlePlatformer
{
    public class SwitchPuzzle : Puzzle
    {
        private readonly bool _isSwitch2On;
        private readonly bool _isSwitch3On;

        public SwitchPuzzle(bool isSwitch2On, bool isSwitch3On)
        {
            _isSwitch2On = isSwitch2On;
            _isSwitch3On = isSwitch3On;
        }

        public override bool HasSolvedPuzzle()
        {
            return _isSwitch2On && _isSwitch3On;
        }
    }
}